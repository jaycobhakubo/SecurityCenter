// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

namespace GTI.Modules.SecurityCenter
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Text;
    using System.Linq;
    using System.Diagnostics;
    using System.Windows.Forms;
    using GTI.Modules.SecurityCenter.Data;
    using GTI.Modules.Shared;
    using GTI.Modules.SecurityCenter.Properties;

    public partial class Position : GradientForm 
    {
        private const string TreeNodeSeparator="-";
        private bool mIsNewPosition=false;
        private int[] mPositionIDs;
        private int mCurrentPositionIndex=-1;
        private GetModuleList mGetModuleList;
        private GetModuleFeatures mGetModuleFeatureList;
        private PositionData mPositionsData;
        private GetPositionList mPositionList;
        private bool mNeedReload = false;
        private bool mActivityChecked;
        private DataRow mNewPostionRow;  //ttp 50053

        public Position(bool isNewPosition)
        {
            mIsNewPosition = isNewPosition;
            mNeedReload = false;
            InitializeComponent();
            CustomInitializeComponent();
            mNewPostionRow = null;     //ttp 50053, support copy position function

            if (isNewPosition)
            {
                positionActivityFlagCheckbox.Checked = true;
                allRadioButton.Checked = true;   //this makes sure the item is visible regardless of the activity
                activeRadioButton.Visible = false;     //disable the radio buttons as they are not applicable in this case
                inactiveRadioButton.Visible = false;
                allRadioButton.Visible = false;
            }
        }

        private void CustomInitializeComponent()
        {
            this.cancelbutton.Click += new EventHandler(cancelbutton_Click);
            this.saveButton.Click += new EventHandler(saveButton_Click);
            this.saveNewImageButton.Click += new System.EventHandler(this.saveNewImageButton_Click);
            this.cmbx_PositionName.Validating += new CancelEventHandler(cmbx_PositionName_Validating); 
            this.cmbx_PositionName.Validated += new EventHandler(cmbx_PositionName_Validated); 
            this.cmbx_PositionName.SelectedIndexChanged += new System.EventHandler(this.cmbx_PositionName_SelectedIndexChanged);
            this.cmbx_PositionName.KeyUp +=new KeyEventHandler(cmbx_PositionName_KeyUp);
            treeView1.AfterCheck += new TreeViewEventHandler(treeView1_AfterCheck);                    
        }

        internal bool IsNewPosition        //ttp 50053, support copy position function
        {
            get { return mIsNewPosition; }
        }
        internal PositionData CurrentPositionData
        {
            get { return mPositionsData; }
        }

        internal void Copy()        //ttp 50053, support copy position function
        {
            string copyText;
            copyText = Configuration.POSITIONPREFIX +Configuration.POSITIONDELIMINATOR + cmbx_PositionName.Text;
            if (mCurrentPositionIndex > -1 && mPositionIDs.Length > mCurrentPositionIndex)
            {
                copyText += Configuration.POSITIONDELIMINATOR + mPositionIDs[mCurrentPositionIndex].ToString();
            }
            Clipboard.SetText(copyText);
        }

        internal void Paste()        //ttp 50053, support copy position function
        {
            string pasteText = Clipboard.GetText();
            try
            {
                if (pasteText == string.Empty) return;
                else //remove prefix
                {
                    pasteText = pasteText.Substring(pasteText.IndexOf(Configuration.POSITIONDELIMINATOR) + Configuration.POSITIONDELIMINATOR.Length);
                }

                SetNewPosition(Resources.CopyOf + pasteText.Substring(0, pasteText.IndexOf(Configuration.POSITIONDELIMINATOR)));     // FIX: DE1778
                cmbx_PositionName_Validated(this, null);
                CheckTreeNodeByPositionID(int.Parse(pasteText.Substring(pasteText.IndexOf(Configuration.POSITIONDELIMINATOR) + Configuration.POSITIONDELIMINATOR.Length)));

            }
            catch (Exception ex)
            {
                Utilities.Log(ex.Message, LoggerLevel.Severe);
                MessageForm.Show(this, ex.Message, Properties.Resources.securityCenter);
            }
            finally
            { Application.DoEvents(); }
        }
        #region Events

        private void Position_Load(object sender, EventArgs e)
        {
            mGetModuleList = ((SecurityCenterMDIParent)this.MdiParent).ModuleList;
            mGetModuleFeatureList = ((SecurityCenterMDIParent)this.MdiParent).ModuleFeatureList;
            LoadPositions();

            if (treeView1.Nodes != null && treeView1.Nodes.Count > 1)            //set the focus to the treeview fist node, that the scrollbar will be on top there is 
            {
                treeView1.SelectedNode = treeView1.Nodes[0];
                treeView1.Select();
            }
            IsModified = false;
        }

        private void LoadPositions()
        {
            Utilities.LogInfoIN();
            mPositionList = new GetPositionList(Constants.GETALL_INSERT);
            try
            {
                mPositionList.Send();
            }
            catch (Exception ex)
            {
                MessageForm.Show(Properties.Resources.errorFailedToGetData + " " + ex.Message, Properties.Resources.securityCenter);
                return;
            }
            mPositionsData = mPositionList.Positions; // ((SecurityCenterMDIParent)this.MdiParent).PositionList.Positions;          
           
            LoadModulesAndFeatures();

            if (mIsNewPosition == true)//ttp 50053, support copy position function
            {
                SetNewPosition(Properties.Resources.NewPosition);
            }
            else
            {
                cmbx_PositionName.DropDownStyle = ComboBoxStyle.DropDown ;
                LoadPositionToComboBox(0);
            }
            Utilities.LogInfoLeave();
        }

        private void SetNewPosition(string positionName)        //ttp 50053, support copy position function
        {
            cmbx_PositionName.DropDownStyle = ComboBoxStyle.Simple;
            cmbx_PositionName.Items.Clear();
            cmbx_PositionName.Items.Add(positionName); //index 0
            cmbx_PositionName.SelectedIndex = 0;
        }

        private void LoadPositionToComboBox(int index)
        {
            Utilities.LogInfoIN();
           
            if (mPositionsData == null ||
                mPositionsData.PositionTable == null )
            {
                return;
            }

            DataView mPositionsView = new DataView(mPositionsData.PositionTable);
            mPositionIDs = new int[mPositionsView.Count];
            mPositionsView.Sort = PositionData.POSITION_COLUMN_POSITIONNAME;
            cmbx_PositionName.Items.Clear();
            int count = 0;
            bool hasInactive = false;

            foreach (DataRowView viewRow in mPositionsView)
            {

                if (allRadioButton.Checked == true)                //if all is checked
                {
                    this.cmbx_PositionName.Items.Add(viewRow.Row[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                    mPositionIDs[count++] = int.Parse(viewRow.Row[PositionData.POSITION_COLUMN_POSITIONID].ToString());
                }

                else if (activeRadioButton.Checked == true)   //if active is checked
                {
                    if ((bool)viewRow.Row[PositionData.POSITION_COLUMN_ACTIVITYFLAG])
                    {
                        this.cmbx_PositionName.Items.Add(
                            viewRow.Row[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                        mPositionIDs[count++] =
                            int.Parse(viewRow.Row[PositionData.POSITION_COLUMN_POSITIONID].ToString());
                    }
                }

                else if (inactiveRadioButton.Checked == true)              //if not active is checked
                {
                    if (!(bool)viewRow.Row[PositionData.POSITION_COLUMN_ACTIVITYFLAG])
                    {
                        this.cmbx_PositionName.Items.Add(
                            viewRow.Row[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                        mPositionIDs[count++] =
                            int.Parse(viewRow.Row[PositionData.POSITION_COLUMN_POSITIONID].ToString());
                        hasInactive = true;
                    }
                }

           //START RALLY DE10127
                if (!(bool)viewRow.Row[PositionData.POSITION_COLUMN_ACTIVITYFLAG])
                {
                    hasInactive = true;
                }
            }

            if (hasInactive == false)
            {
                if (inactiveRadioButton.Checked == false)
                {
                    inactiveRadioButton.Enabled = false;
                }              
            }
            else
            {
                inactiveRadioButton.Enabled = true;
            }
            //END RALLY DE10127

            if (cmbx_PositionName.Items.Count > index && index > -1) //load permission of modules and features for this position
            {
                cmbx_PositionName.SelectedIndex = index; //always default of first
                if (mPositionIDs != null && mPositionIDs.Length > 0)
                {
                    bool activity = mPositionsData.GetPositionActivityByID(mPositionIDs[index]);
                    positionActivityFlagCheckbox.Checked = activity;
                }
            }

            Utilities.LogInfoLeave();
           
        }

        private void cmbx_PositionName_SelectedIndexChanged(object sender, EventArgs e)
        {
    
            if (mIsNewPosition == true) return;
            isLoading = true;

            if (cmbx_PositionName.SelectedIndex > -1 &&            //save change if any
                mCurrentPositionIndex >= 0 && mPositionIDs[mCurrentPositionIndex] > 0 &&
                IsPositionModified(mPositionIDs[mCurrentPositionIndex]) == true )
            {
                DialogResult result = DoUWantToSaveDialog(mPositionIDs[mCurrentPositionIndex]);

                //FIX: RALLY 1573 added postion activity START
                if ( result  == DialogResult.Yes)
                {
                    //if the save result is false then the combo box and selection needs
                    //to be reset
                    bool saveResult = SaveCurrentPosition(mPositionIDs[mCurrentPositionIndex]);
                    if (saveResult == false)
                    {
                        mActivityChecked = false;
                        cmbx_PositionName.SelectedIndex = mCurrentPositionIndex;
                        return;
                    }
                    //reload the combo box if the radio buttons are checked and the activity is changed

                    if((activeRadioButton.Checked || inactiveRadioButton.Checked) && mActivityChecked)
                    {
                        //reset the checked activity flag
                        mActivityChecked = false;

                        //get positionID of new item checked
                        DataRow row = mPositionsData.GetPositionRowByID(mPositionIDs[cmbx_PositionName.SelectedIndex]);
                        int positionID = (int)row[PositionData.POSITION_COLUMN_POSITIONID];

                        //load combobox which refreshes mPositionID's
                        LoadPositionToComboBox(0);

                        //get index of new selection by using positionID to match
                        for(int i=0; i<mPositionIDs.Length; i++)
                        {
                            //found the new index
                           if(mPositionIDs[i] == positionID)
                           {
                               //this should cause this event to fire again so 
                               //we will return
                               cmbx_PositionName.SelectedIndex = i;
                               
                               return;
                           }
                        }

                    }

                    //this means the all radio button is checked
                    else if(mActivityChecked)
                    {
                        mActivityChecked = false;
                    }
                }
                
                else if (result == DialogResult.No)
                {

                    if (mActivityChecked)       //check the activity flag
                    {
                        //get the data and toggle it back to the original setting
                        DataRow row = mPositionsData.GetPositionRowByID(mPositionIDs[mCurrentPositionIndex]);
                        positionActivityFlagCheckbox.Checked = !(bool) row[PositionData.POSITION_COLUMN_ACTIVITYFLAG];
                        row[PositionData.POSITION_COLUMN_ACTIVITYFLAG] = positionActivityFlagCheckbox.Checked;
                        row[Constants.Status] = Constants.Status_OLD;
                        mActivityChecked = false;
                    }
                }
                
                else if (result == DialogResult.Cancel)
                {
                    //check the activity flag
                    if (mActivityChecked)
                    {
                        //get the data and toggle it back to the original setting
                        DataRow row = mPositionsData.GetPositionRowByID(mPositionIDs[mCurrentPositionIndex]);
                        positionActivityFlagCheckbox.Checked = !(bool)row[PositionData.POSITION_COLUMN_ACTIVITYFLAG];
                        row[PositionData.POSITION_COLUMN_ACTIVITYFLAG] = positionActivityFlagCheckbox.Checked;
                        row[Constants.Status] = Constants.Status_OLD;
                        mActivityChecked = false;
                    }
                    return;
                }
                //FIX: RALLY 1573 added position activity END
            }

            mCurrentPositionIndex = cmbx_PositionName.SelectedIndex; //update index 
            
            //FIX: RALLY 1573 added postion activity START
            bool active = mPositionsData.GetPositionActivityByID(mPositionIDs[mCurrentPositionIndex]);
            positionActivityFlagCheckbox.Checked = active;
            //FIX: RALLY 1573 added postion activity END
            
            //load the current position's permission
            if (mCurrentPositionIndex > -1 && mPositionIDs[mCurrentPositionIndex] > 0)
            {
                CheckTreeNodeByPositionID(mPositionIDs[mCurrentPositionIndex]);
            }
            
            treeView1.Focus ();
            if (treeView1.Nodes.Count > 0)
            {
                treeView1.Select();
                treeView1.SelectedNode = treeView1.Nodes[0];
            }

            isLoading = false; 
        }
        //handle enter key
        private void cmbx_PositionName_KeyUp(object sender, KeyEventArgs ke)
        {
            if (ke.KeyCode == Keys.Enter)
            {
                cmbx_PositionName_Validated(sender, ke);
            }
        
        }

        // Rally DE1885
        private void cmbx_PositionName_Validating(object sender, CancelEventArgs e)
        {
            if(cmbx_PositionName.Text.Equals("") == true || IsExistingName(cmbx_PositionName.Text) == true)
            {
                //DE12313 (20150223): Check if it's a new position or if Edit.
                if (IsNewPosition == true)
                {
                    // We do not allow to have a position name called "" or current position.
                    MessageForm.Show(Properties.Resources.warningPositionName, Properties.Resources.securityCenter);
                    e.Cancel = true;
                }
            }
        }

        private void cmbx_PositionName_Validated(object sender, EventArgs e)
        {
            // Rally DE1885
           /*if (cmbx_PositionName.Text.Equals("") == true ||
              // cmbx_PositionName.Text.Equals(Properties.Resources.NewPosition) ||
               IsExistingName(cmbx_PositionName.Text) == true )
            {//do nothing, we do not allow to have a position name called New Position, "" or current position
                MessageForm.Show(Properties.Resources.warningPositionName, Properties.Resources.securityCenter);
               cmbx_PositionName.Focus(); 
               return;
            }
            else*/ if (mIsNewPosition)
            {//new position
                //ttp 50053
               // cmbx_PositionName.Items.Add(cmbx_PositionName.Text);
                //DataRow newPostion = mPositionsData.PositionTable.NewRow();
                mNewPostionRow = mPositionsData.PositionTable.NewRow();
                mNewPostionRow[PositionData.POSITION_COLUMN_POSITIONNAME] = cmbx_PositionName.Text;
                mNewPostionRow[PositionData.POSITION_COLUMN_POSITIONID] = 0;
                mNewPostionRow[PositionData.POSITION_COLUMN_ACTIVITYFLAG] = positionActivityFlagCheckbox.Checked;
                mNewPostionRow[Constants.Status] = Constants.Status_New;
                
                //if (mPositionsData == null)
                //    mPositionsData = new PositionData();
                //mPositionsData.PositionTable.Rows.Add(newPostion);
                mCurrentPositionIndex = -1; //new position
                //LoadModulesAndFeatures();
            }
            else if (cmbx_PositionName.SelectedIndex < 0)//modifying a position
            {
                DataRow row = mPositionsData.GetPositionRowByID(mPositionIDs[mCurrentPositionIndex]);
                if (row != null)
                {
                    IsPositionNameChanged = (row[PositionData.POSITION_COLUMN_POSITIONNAME].ToString() != cmbx_PositionName.Text);
        
                    row[PositionData.POSITION_COLUMN_POSITIONNAME] = cmbx_PositionName.Text; // mCurrentPositionName;
                    row[Constants.Status] = Constants.Status_Modified;
                    row[PositionData.POSITION_COLUMN_ACTIVITYFLAG] = positionActivityFlagCheckbox.Checked;
                    //LoadModulesAndFeatures();
                    //CheckTreeNodeByPositionID(mPositionIDs[mCurrentPositionIndex]);  //Rally DE9836
                }
                
            }
            //LoadPositionToComboBox();
        }

        private bool IsExistingName(string newName)
        {
            //if (cmbx_PositionName.SelectionLength < 0)
              //  return false;

            if (newName.Length <= 0)
                return false;

            foreach (DataRow positionRow in mPositionsData.PositionTable.Rows)
            {
                if (newName.Equals(positionRow[PositionData.POSITION_COLUMN_POSITIONNAME].ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }
                /*if (newName.Equals(positionRow[PositionData.POSITION_COLUMN_POSITIONNAME].ToString()) &&
                    newName.Equals (cmbx_PositionName.Items[cmbx_PositionName.SelectedIndex].ToString()) != true)
                {
                    return true;
                 * 
                 * The second part of this conditional, when tested by itself, returns an error and causes the application to crash.
                }*/
            }
            return false;
        }

        private ModulesData getLatestModulesFromServer(int positionID)
        {
            ModulesData permitedModules;
            ModuleFeaturesData permitedFeatures;

            GetPositionModule getPermitedModules = new GetPositionModule(positionID);
            GetPositionModuleFeatures getPermitedFeatures = new GetPositionModuleFeatures(positionID);
            try
            {
                getPermitedFeatures.Send();
                getPermitedModules.Send();
            }
            catch (Exception ex)
            {
                Utilities.Log("Failed to get permited featureas and modules, exception messge=" + ex.Message, LoggerLevel.Severe);
                Utilities.Log("Exception.Stack=" + ex.StackTrace, LoggerLevel.Severe);
                MessageForm.Show(this, Properties.Resources.errorFailedToGetData, Properties.Resources.securityCenter);
                permitedModules = null;
                permitedFeatures = null;
                return null;
            }

            permitedModules = getPermitedModules.PositionModules;
            permitedFeatures = getPermitedFeatures.ModuleFeatures;

            UpdatePositionsModuleFeature(positionID, permitedModules, permitedFeatures);
            return permitedModules;
        }

        //US4347 (20151105) As a technician, I want all of the permissions for a permission group to be checked when clicking on the permission group so that I can quickly check all of the permissions.
        private bool isLoading = true;
        public bool IsLoading
        {
            get { return isLoading; }
            set { isLoading = value; }
        }

        private void CheckTreeNodeByPositionID(int positionID)
        {
            // Rally DE2356 - Edit position does not show user currently enabled options.
            
            if (positionID < 1 || treeView1.Nodes.Count == 0)
            {
                return;
            }

            ModulesData permitedModules;
            ModuleFeaturesData permitedFeatures;
            
            permitedModules = getLatestModulesFromServer(positionID);
           
            foreach (TreeNode moduleNode in treeView1.Nodes)
            {
                //default is false
                moduleNode.Checked = false;
                int moduleID = (int)moduleNode.Tag;

                if (permitedModules != null && permitedModules.IsModuleInList(moduleID))
                {
                    //found module
                    moduleNode.Checked = true;
                    permitedFeatures = permitedModules.FindFeaturesByModuleID(moduleID);
                    
                    //populate the features if there are no feature nodes then this will skip
                    foreach (TreeNode featureNode in moduleNode.Nodes)
                    {
                        //default is false
                        featureNode.Checked = false;
                        int featureID = (int) featureNode.Tag;

                        //found feature in list
                        if (permitedFeatures != null && permitedFeatures.IsFeatureInList(featureID))
                        {
                            featureNode.Checked = true;
                        }
                    }
                }
            }
            IsModified = false;
            isLoading = false;
        }

        private void CheckTreeNodeByPositionID2(int positionID)
        {
            if (mGetModuleList == null ||
                mGetModuleList.Modules == null ||
                mGetModuleList.Modules.ModuleTable == null ||
                mGetModuleList.Modules.ModuleTable.Rows.Count == 0 ||
                positionID < 1
               )
            {
                return;
            }
            //it is ok if there is no features at all

            //get the permissions and go through the whole tree view, this is time consuming
            //the strategy will be that we cache it to local by the assumption there is only one instance running
            //we go through our cach first, and then go to the server asking
            //now, assume there is no performance issue at all

            DataView modules;
            //DataView features=null;
            ModulesData permitedModules;
            
                GetPositionModule getPermitedModules = new GetPositionModule(positionID);
                GetPositionModuleFeatures getPermitedFeatures = new GetPositionModuleFeatures(positionID);
                try
                {
                    getPermitedFeatures.Send();
                    getPermitedModules.Send();
                }
                catch (Exception ex)
                {
                    Utilities.Log("Failed to get permited featureas and modules, exception messge=" + ex.Message, LoggerLevel.Severe);
                    Utilities.Log("Exception.Stack=" + ex.StackTrace, LoggerLevel.Severe);
                    MessageForm.Show(this, Properties.Resources.errorFailedToGetData, Properties.Resources.securityCenter);
                    return;
                }
                permitedModules = getPermitedModules.PositionModules;
                ModuleFeaturesData permitedFeatures = getPermitedFeatures.ModuleFeatures;
                modules = new DataView(permitedModules.ModuleTable);
                //features = new DataView(permitedFeatures.ModuleFeatureTable);
                UpdatePositionsModuleFeature(positionID, permitedModules, permitedFeatures);
            
            modules.Sort = ModulesData.MODULES_COLUMN_MODULENAME;
            DataView featuresByModuleID = new DataView();

            //check modules and its features 
            //all tree nodes, modules, features are sorted by the name in the same way, the performance should be optimized
            // the logic is complicated here with lots of assumption, modification with caution
            //it seems N*N*N*N worst scenario, but actually not, it should be TreeView.Nodes.Count
            TreeNode moduleNode = null;
            TreeNode featureNode;
            
            // Rally DE2356 - Edit position does not show user currently enabled options.
            if(treeView1.GetNodeCount(false) == 0)
                return;
            
            foreach(DataRowView moduleRow in modules)
            {
                moduleNode = treeView1.Nodes[0]; // Get the first module.

                while(moduleNode != null)
                {
                    try
                    {
                        if(moduleNode.Tag.ToString().Equals(moduleRow.Row[ModulesData.MODULES_COLUMN_MODULEID].ToString()))
                        {
                            moduleNode.Checked = true;

                            // Get the module's features.
                            ModuleFeaturesData  permittedModuleData = permitedModules.FindFeaturesByModuleID(int.Parse(moduleNode.Tag.ToString()));
                            if (permittedModuleData != null)
                            {
                                featuresByModuleID =
                                    new DataView(permittedModuleData.ModuleFeatureTable);
                                featuresByModuleID.Sort = ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESNAME;
                            }
                            foreach(DataRowView featureRow in featuresByModuleID)
                            {
                                featureNode = moduleNode.FirstNode;

                                while(featureNode != null)
                                {
                                    try
                                    {
                                        if(featureNode.Tag.ToString().Equals(featureRow.Row[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATUREID].ToString()))
                                        {
                                            featureNode.Checked = true;
                                            break;
                                        }
                                        else
                                        {
                                            featureNode = featureNode.NextNode;
                                        }
                                    }
                                    catch(Exception ex)
                                    {
                                        Utilities.Log("Failed to check feature nodes, exception message is " + ex.Message, LoggerLevel.Severe);
                                        Utilities.Log("Exception stack=" + ex.StackTrace, LoggerLevel.Severe);
                                        MessageForm.Show(Properties.Resources.errorGeneral + ex.StackTrace, Properties.Resources.securityCenter);
                                    }
                                }
                            }

                            break;
                        }
                        else
                        {
                            moduleNode.Checked = false;
                            moduleNode = moduleNode.NextNode;
                        }
                    }
                    catch(Exception ex)
                    {
                        Utilities.Log("Failed to check nodes, exception message is " + ex.Message, LoggerLevel.Severe);
                        Utilities.Log("Exception stack=" + ex.StackTrace, LoggerLevel.Severe);
                        MessageForm.Show(Properties.Resources.errorGeneral + ex.StackTrace, Properties.Resources.securityCenter);
                    }
                }
               
            } //end first foreach loop

        } //end of function


        private void UpdatePositionsModuleFeature(int positionID, ModulesData permitedModules, ModuleFeaturesData permitedFeatures)
        {
            Utilities.LogInfoIN();
            ModuleFeaturesData features;
            DataRow tempRow;

            mPositionsData.PermitedModules.Clear();
            foreach (DataRow mouldeRow in permitedModules.ModuleTable.Rows)
            {
                features = new ModuleFeaturesData();
                foreach (DataRow featureRow in permitedFeatures.ModuleFeatureTable.Rows)
                {
                   
                    if (int.Parse(featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEID].ToString()) ==
                        int.Parse(mouldeRow[ModulesData.MODULES_COLUMN_MODULEID].ToString()))
                    {
                        tempRow = features.ModuleFeatureTable.NewRow();
                        tempRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATUREID] =
                            featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATUREID].ToString() ;

                        tempRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEID] =
                            featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEID].ToString();

                        tempRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESNAME ] =
                            featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESNAME].ToString();

                        tempRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESDESCRIPTION] =
                            featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESDESCRIPTION].ToString();
                        tempRow[Constants.Status ] =
                            featureRow[Constants.Status].ToString();
                        
                        features.ModuleFeatureTable.Rows.Add(tempRow);
                    }
                }
                permitedModules.Features.Add(int.Parse(mouldeRow[ModulesData.MODULES_COLUMN_MODULEID].ToString()), features);
            }
            mPositionsData.PermitedModules.Add(positionID, permitedModules);
            Utilities.LogInfoLeave();
        }
        private void LoadModulesAndFeatures()
        {
             Utilities.LogInfoIN();
           
            if (mGetModuleList == null ||
                mGetModuleList.Modules == null ||
                mGetModuleList.Modules.ModuleTable == null)
            {
                return;
            }
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear(); //clear it before loading
           
            ModuleTreeNode moduleNode;
            TreeNode featureNode;
            bool hasFeatures = true;
             if (mGetModuleFeatureList == null ||
               mGetModuleFeatureList.ModuleFeatures == null ||
               mGetModuleFeatureList.ModuleFeatures.ModuleFeatureTable  == null ||
               mGetModuleFeatureList.ModuleFeatures.ModuleFeatureTable.Rows.Count==0)
            {
                hasFeatures = false;
            }
            DataView modules = new DataView(mGetModuleList.Modules.ModuleTable);
            modules.Sort = ModulesData.MODULES_COLUMN_MODULENAME;
            DataView features;
            foreach (DataRowView row in modules)
            { 
                //add modules to the treenode1 and its features to its node
                moduleNode = new ModuleTreeNode(row[ModulesData.MODULES_COLUMN_MODULENAME].ToString());
                moduleNode.Tag = row[ModulesData.MODULES_COLUMN_MODULEID];
                moduleNode.Checked = false; //default false
                moduleNode.isModified = false;
                if (hasFeatures == true)
                {
                    features = mGetModuleFeatureList.ModuleFeatures.GetModuleFeatureDataViewByModuleID(int.Parse(moduleNode.Tag.ToString()));
                    if (Convert.ToInt32(moduleNode.Tag) != 247)
                    {
                        features.Sort = ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESNAME;
                    }

                    foreach (DataRowView feature in features)
                    {
                        featureNode = new TreeNode(feature[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESNAME].ToString());
                        featureNode.Tag = feature[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATUREID];
                        featureNode.Checked = false;
                        moduleNode.Nodes.Add(featureNode);
                    }
                }
                treeView1.Nodes.Add(moduleNode);
             }

             this.treeView1.ExpandAll();
             treeView1.EndUpdate();

              Utilities.LogInfoLeave();;
           
        }

 

        /// <summary>
        /// AfterCheck, Permission of modules and features will be mapped and marked in current Position ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        bool checkALLsub = false;
        int countsub = 0;
        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //Utilities.LogInfoIN(); too much node, noise information
            Utilities.Log(string.Format("Checking node ID={0},name={1}", e.Node.Tag.ToString(), e.Node.Text), LoggerLevel.Information);
            if (mPositionsData == null ||
                mPositionsData.PermitedModules == null ||
                int.Parse (e.Node.Tag.ToString()) == -1)
            {
                return;
            }
            
            ModulesData moduleData;
            DataRow tempDataRow;
            int positionID = 0;
            
            if (mCurrentPositionIndex >= 0)
            {
                positionID = mPositionIDs[mCurrentPositionIndex];
            }
            
            else 
            {    
                positionID =0;
            }

            mPositionsData.PermitedModules.TryGetValue(positionID, out moduleData);
            
            if (moduleData == null)
            { //new position
                moduleData = new ModulesData();
                mPositionsData.PermitedModules.Add(Constants.GETALL_INSERT, moduleData);
            }
            
            if (e.Node is ModuleTreeNode)
            {
                //Update position's module list             
                DataRow rowModule = moduleData.FindModuleByModuleID(int.Parse(e.Node.Tag.ToString()));
                if (rowModule != null)
                {   //existing one
                    if (e.Node.Checked == false)
                    {//remove it                         
                       //moduleData.ModuleTable.Rows.Remove(rowModule);   
                        rowModule[Constants.Status] = Constants.Status_Deleted; 
                    }
                    else //checked== true
                    {                      
                      rowModule[Constants.Status] = Constants.Status_OLD; 
                    }
                }
                else
                {
                    //add a new one
                    if (e.Node.Checked == true)
                    {
                        rowModule = moduleData.ModuleTable.NewRow();
                        rowModule[Constants.Status] = Constants.Status_New;
                        rowModule[ModulesData.MODULES_COLUMN_MODULEID] = e.Node.Tag.ToString();
                        rowModule[ModulesData.MODULES_COLUMN_MODULENAME] = e.Node.Text;
                        moduleData.ModuleTable.Rows.Add(rowModule);
                    }             
                }
            
                //ui
                if (e.Node.Checked == false)
                {
                    //uncheck all of its features node
                    foreach (TreeNode featureNode in e.Node.Nodes)
                    {
                        featureNode.Checked = false;

                    }
                }
               
                ((ModuleTreeNode)e.Node).isModified = true;

             
                //This will check all subnodes of the main nodes of the treeview. 
                if (isLoading == false)//Only applies if the user selected new position.
                {
                    if (e.Node.Checked == true)
                    {
                        if (checkALLsub == false)
                        {
                            foreach (TreeNode tn in e.Node.Nodes)
                            {
                                if (tn.Checked == true)
                                {
                                    countsub = countsub + 1;
                                    break;
                                }
                            }

                            if (countsub == 0)//count the subtree check
                            { checkALLsub = true; }
                            else
                            {
                                countsub = 0;
                            }
                        }

                        if (checkALLsub == true)
                        {
                            if (countsub != e.Node.Nodes.Count)
                            {
                                countsub = countsub + 1;
                                e.Node.Nodes[countsub - 1].Checked = true;
                            }
                            else
                            {
                                checkALLsub = false;
                                countsub = 0;
                            }
                        }


                    }
                }
                

            }
            else //feature node
            {
        

                ModuleFeaturesData featuresOfModule = moduleData.FindFeaturesByModuleID(int.Parse(e.Node.Parent.Tag.ToString()));
                if (featuresOfModule != null)
                { 
                    tempDataRow = featuresOfModule.FeatureRowByFeatureID(int.Parse(e.Node.Tag.ToString()));

                    if (tempDataRow != null)
                    {
                        if (e.Node.Checked == false)
                        { //remove it
                            //featuresOfModule.ModuleFeatureTable.Rows.Remove(tempDataRow);
                            tempDataRow[Constants.Status] = Constants.Status_Deleted;
                        }
                        else
                        {
                            tempDataRow[Constants.Status] = Constants.Status_OLD;
                            ((ModuleTreeNode)e.Node.Parent).Checked = true;      //RALLY DE9176
                        }

                    }
                    else
                    {
                        //add a new one
                        if (e.Node.Checked == true)
                        {
                            tempDataRow = featuresOfModule.ModuleFeatureTable.NewRow();
                            tempDataRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEID] = e.Node.Parent.Tag.ToString();
                            tempDataRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATUREID] = e.Node.Tag.ToString();
                            tempDataRow[Constants.Status] = Constants.Status_New;
                            featuresOfModule.ModuleFeatureTable.Rows.Add(tempDataRow);
                            ((ModuleTreeNode)e.Node.Parent).Checked = true;    //RALLY DE9176
                        }   
                    
                    }                                  
                }
                else //featuresOfModule is null
                {
                    featuresOfModule = new ModuleFeaturesData();
                    moduleData.Features.Add(int.Parse(e.Node.Parent.Tag.ToString()), featuresOfModule);                    
                    //add a new one
                    if (e.Node.Checked == true)
                    {                        
                        DataRow featureRow = featuresOfModule.ModuleFeatureTable.NewRow();
                        featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEID] = e.Node.Parent.Tag.ToString();
                        featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATUREID] = e.Node.Tag.ToString();
                        featureRow[Constants.Status] = Constants.Status_New;
                        featuresOfModule.ModuleFeatureTable.Rows.Add(featureRow);
                        ((ModuleTreeNode)e.Node.Parent).Checked = true;    //RALLY DE9176
                    }                                   
                    
                }
            ((ModuleTreeNode)e.Node.Parent).isModified = true;
            }
            IsModified = true;
           // Utilities.LogInfoLeave();
        }      
        private void cancelbutton_Click(object sender, EventArgs e)
        {
            if (MessageForm.Show(Properties.Resources.warningCancel, Properties.Resources.securityCenter, MessageFormTypes.YesNo)
                                        == DialogResult.Yes)
            {
                if (!mNeedReload)
                    this.DialogResult = DialogResult.Cancel;
                else
                    this.DialogResult = DialogResult.Yes;
                this.Close();
                IsModified = false;               
            }
        }        
        private void saveButton_Click(object sender, EventArgs e)
        {
            IsModified = false;
            Utilities.LogInfoIN();

            // Rally DE1885
            if(!ValidateChildren(ValidationConstraints.Enabled | ValidationConstraints.Visible))
                return;

            int positionID = 0;
            //ttp 50053, support copy/paste position function
            //add the new position if it is
            if (IsNewPosition == true)
            {
                if (mPositionsData == null)
                    mPositionsData = new PositionData();
                mPositionsData.PositionTable.Rows.Add(mNewPostionRow);
            }
            //end of ttp 50053
            if (mCurrentPositionIndex >= 0 && 
                mPositionIDs !=null && 
                mCurrentPositionIndex < mPositionIDs.Length )
            {
                positionID = mPositionIDs[mCurrentPositionIndex];
            }
            //save change if any
            
                if (IsPositionModified(positionID) == true)
                {
                    SaveCurrentPosition(positionID);
                }
                this.Close();
                Utilities.LogInfoLeave();
              

        }
        private void saveNewImageButton_Click(object sender, EventArgs e)
        {
            Utilities.LogInfoIN();
            int positionID = 0;
            if (mCurrentPositionIndex > -1 && 
                mPositionIDs !=null &&
                mCurrentPositionIndex < mPositionIDs.Length)
            {
                positionID = mPositionIDs[mCurrentPositionIndex];
            }
            //save change if any
            if (IsPositionModified(positionID) == true)
            {               
                SaveCurrentPosition(positionID);
                mNeedReload = true; 
            }
            mIsNewPosition = true;
            ((SecurityCenterMDIParent)this.MdiParent).SetNewPostionContextMenu(true);
            LoadPositions();
            Utilities.LogInfoLeave();
        }

        private DialogResult DoUWantToSaveDialog(int positionID)
        {
            DialogResult result = System.Windows.Forms.DialogResult.Cancel;
            //3 cases:  1 module/feature modified = true activty modified = false
            //          2 module/feature modified = false activty modified = true
            //          3 module/feature modified = true activty modified = true

            bool featureOrModuleModified = IsFeatureOrModuleModified(positionID);

            // case 1
            if (featureOrModuleModified && !mActivityChecked)
            {
                result = MessageForm.Show(Properties.Resources.DoYouWantToSaveCase1, Properties.Resources.securityCenter,
                                          MessageFormTypes.YesNoCancel, 0);
            }

            // case 2
            else if (!featureOrModuleModified && mActivityChecked)
            {
                result = MessageForm.Show(Properties.Resources.DoYouWantToSaveCase2, Properties.Resources.securityCenter,
                                          MessageFormTypes.YesNoCancel, 0);
            }

            //case 3
            else if (featureOrModuleModified && mActivityChecked)
            {
                result = MessageForm.Show(Properties.Resources.DoYouWantToSaveCase3, Properties.Resources.securityCenter,
                                          MessageFormTypes.YesNoCancel, 0);
            }

            return result;
        }

        private bool IsFeatureOrModuleModified(int positionID)
        {
            ModulesData permitedModules;
            ModuleFeaturesData permitedFeatures;
            mPositionsData.PermitedModules.TryGetValue(positionID, out permitedModules);
            if (permitedModules != null)
            {
                foreach (DataRow moduleRow in permitedModules.ModuleTable.Rows)
                {
                    if (moduleRow[Constants.Status].ToString().Equals(Constants.Status_Modified) ||
                        moduleRow[Constants.Status].ToString().Equals(Constants.Status_Deleted) ||
                        moduleRow[Constants.Status].ToString().Equals(Constants.Status_New))
                    {
                        return true;
                    }
                    permitedModules.Features.TryGetValue(int.Parse(moduleRow[ModulesData.MODULES_COLUMN_MODULEID].ToString()), out permitedFeatures);
                    if (permitedFeatures != null)
                    {
                        foreach (DataRow featureRow in permitedFeatures.ModuleFeatureTable.Rows)
                        {
                            if (featureRow[Constants.Status].ToString().Equals(Constants.Status_Modified) ||
                                featureRow[Constants.Status].ToString().Equals(Constants.Status_Deleted) ||
                                featureRow[Constants.Status].ToString().Equals(Constants.Status_New)
                               )
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        private bool IsPositionModified(int positionID)
        {
            
            //go through current position's name and its permsion to see if there is modification
            if (mPositionsData == null)
            {
                return false;
            }
            DataRow tempRow =mPositionsData.GetPositionRowByID(positionID);
            if (tempRow == null)
            {
                return false;
            }
            else if ( tempRow [Constants.Status].ToString() == Constants.Status_New ||
                      tempRow [Constants.Status].ToString() == Constants.Status_Modified
                    )
            { 
                return true; 
            }
            
            if(IsFeatureOrModuleModified(positionID))
            {
                return true;
            }
            
            return false;
        }
        private bool SaveCurrentPosition(int positionID)
        {
             Utilities.LogInfoIN();
           
            DataRow tempRow;
            int newPositionID = -1;
            this.Cursor = Cursors.WaitCursor;
            SetPosition savePosition = null;
            
            try
            {
                //save current position as its ID
                //go through current position's name and its permsion to see if there is modification
                tempRow = mPositionsData.GetPositionRowByID(positionID);
                if ( tempRow != null &&
                     (  tempRow[Constants.Status].ToString() == Constants.Status_New ||
                        tempRow [Constants.Status].ToString() == Constants.Status_Modified)
                    )
                {
                    //save or update the position
                    savePosition = new SetPosition(positionID, tempRow [PositionData.POSITION_COLUMN_POSITIONNAME].ToString(), (bool) tempRow[PositionData.POSITION_COLUMN_ACTIVITYFLAG]);
                    savePosition.Send();
                    newPositionID = savePosition.PositionID;

                    DataRow positionRow = mPositionsData.GetPositionRowByID(positionID);
                    positionRow[Constants.Status] = Constants.Status_OLD;
                    
                }
            }
            catch (Exception ex)
            {
                Utilities.Log ("Failed to save current position, exception =" + ex.Message, LoggerLevel.Severe);
                Utilities.Log("Exception Stack " + ex.StackTrace, LoggerLevel.Severe);

                if(savePosition != null && savePosition.ReturnCode == -86)
                {
                    DataRow positionRow = mPositionsData.GetPositionRowByID(positionID);
                    positionRow[PositionData.POSITION_COLUMN_ACTIVITYFLAG] = true;
                    positionRow[Constants.Status] = Constants.Status_OLD;
                    MessageForm.Show(Properties.Resources.activitySettingError, Properties.Resources.securityCenter);
                }
                
                else
                {
                    MessageForm.Show(Properties.Resources.errorFailedSavePosition, Properties.Resources.securityCenter);
                }
                
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            ModulesData permitedModules;
            ModuleFeaturesData permitedFeatures;
            mPositionsData.PermitedModules.TryGetValue(positionID, out permitedModules);
            
            if (permitedModules != null)
            {
                ArrayList moduleIDs = new ArrayList ();
                ArrayList featureIDs = new ArrayList();                
                foreach (DataRow moduleRow in permitedModules.ModuleTable.Rows)
                {
                    if ( false == moduleRow[Constants.Status].ToString().Equals(Constants.Status_Deleted))
                    {
                        moduleIDs.Add (int.Parse (moduleRow [ModulesData.MODULES_COLUMN_MODULEID].ToString ()));
                    }

                    permitedModules.Features.TryGetValue(int.Parse(moduleRow[ModulesData.MODULES_COLUMN_MODULEID].ToString()), out permitedFeatures);

                    if (permitedFeatures != null)
                    {                               
                        foreach (DataRow featureRow in permitedFeatures.ModuleFeatureTable.Rows)
                        {
                            if (false == featureRow[Constants.Status].ToString().Equals(Constants.Status_Deleted))
                            {
                                featureIDs.Add (int.Parse (featureRow [ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATUREID].ToString ()));
                            }
                        }
                    }
                }

                this.Cursor = Cursors.WaitCursor;
                //save the modules and features 
                try
                {
                    if (newPositionID > 0 && positionID == 0)
                    {
                        positionID = newPositionID;
                    }
                    
                   SetPositionModules saveModules = new SetPositionModules(positionID, (int[])moduleIDs.ToArray(typeof(int)));
                   saveModules.Send();
                  
                   SetPositionModuleFeatures saveFeatures = new SetPositionModuleFeatures(positionID, (int[])featureIDs.ToArray(typeof(int)));
                   saveFeatures.Send();
  
                }
                catch (Exception ex)
                {
                    Utilities.Log("Failed to save current postion's permited modules or features, exception =" + ex.Message, LoggerLevel.Severe);
                    Utilities.Log("Exception Stack " + ex.StackTrace, LoggerLevel.Severe);
                    MessageForm.Show(Properties.Resources.errorFailedToSaveModuleFeatures, Properties.Resources.securityCenter);
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
             Utilities.LogInfoLeave();
            return true;           
        }

        #endregion

        private void CheckChanged()
        {
            if (cmbx_PositionName.SelectedIndex > -1 &&
                       mCurrentPositionIndex >= 0 && mPositionIDs[mCurrentPositionIndex] > 0 &&
                       IsPositionModified(mPositionIDs[mCurrentPositionIndex]) == true)
            {
                //something has changed make sure that the change is handled before we move on
                cmbx_PositionName_SelectedIndexChanged(null, null);
            }
        }

        private void allRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckChanged();
            LoadPositionToComboBox(0);
        }

        private void activeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckChanged();
            LoadPositionToComboBox(0);
        }

        private void inactiveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckChanged();
            LoadPositionToComboBox(0);
        }

        private void positionActivityFlagCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            //this just checks existing positions.  The flag should not be set on new positions
            //check to see if it is a valid position
            if(cmbx_PositionName.SelectedIndex < 0 || cmbx_PositionName.Items.Count == 0)
            {
                return;
            }
            mActivityChecked = true;
            if (!mIsNewPosition)
            {
                //get the position id
                int positionID = mPositionIDs[cmbx_PositionName.SelectedIndex];
                DataRow tempRow = mPositionsData.GetPositionRowByID(positionID);

                //set the temp row to status modifed and check the flag
                tempRow[PositionData.POSITION_COLUMN_ACTIVITYFLAG] = positionActivityFlagCheckbox.Checked;
                tempRow[Constants.Status] = Constants.Status_Modified;
                //send the position id, the name and the activity flag to setPosition
            }
        }
		public bool IsModified { get; set; }
        public bool IsPositionNameChanged { get; set; }
       
    }
}