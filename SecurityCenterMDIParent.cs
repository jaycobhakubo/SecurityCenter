using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Data.Sql;
using System.Diagnostics;
using GTI.Modules.SecurityCenter.Data;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter
{
    public partial class SecurityCenterMDIParent : Form
    {
        //private int childFormNumber = 0;
        #region Variables

        private Staff m_curStaff;
        private NewStaff mInitStaffForm;
        private NewStaff mNewStaffForm;
        private Position mPositionForm;
        private MachineForm mMachineForm;
        private GetStaffList mStaffList;
        private GetModuleList mModuleList;
        private GetModuleFeatures mModuleFeatureList;
        private WaitForm mWaitingForm;
        private BackgroundWorker m_worker;

        #endregion

        #region Constructors
        public SecurityCenterMDIParent()
        {
            Utilities.LogInfoIN();;
            InitializeComponent();
            //ttp 50053, support copy position function
            Clipboard.Clear(); //clear other people's dirty board         
            Utilities.LogInfoLeave();;

        }
        #endregion

        #region Data Properties
        public Staff CurentStaff
        {
            set { m_curStaff = value; }
        }   

        internal GetStaffList StaffList
        {
            get { return mStaffList; }
        }
        //internal GetPositionList PositionList
        //{
        //    get { return mPositionList; }
        //}
        internal GetModuleList ModuleList
        {
            get { return mModuleList; }
        }
        internal void ComeToFront(object sender, EventArgs e)
        {
            this.Activate();
        }
        internal GetModuleFeatures ModuleFeatureList
        {
            get { return mModuleFeatureList; }
        }
        public void LoadData()
        {
            Utilities.LogInfoIN();;
            LoadStaff();   //load staff, position, modules, and modulefeatures table// Prepare and send the message to get the counts.
            mModuleList = new GetModuleList();            //modules and features 
            mModuleList.Send();
            mModuleFeatureList = new GetModuleFeatures();
            mModuleFeatureList.Send();
            CheckPositionsCount();//RALLY DE 6739
            Utilities.LogInfoLeave();          
        }
        internal void LoadStaff()
        { 
            mStaffList = new GetStaffList(Configuration.operatorID, true);
            mStaffList.Send(); //we have got all staff datas
            Configuration.StaffLoginNumber = mStaffList.GetLoginNumberByStaffID(Configuration.LoginStaffID);       
        }
        #endregion
        public void ExitSecurityCenter(object sender, EventArgs e)
        {
            ExitToolsStripMenuItem_Click(sender, e);
            //clean up if any
        }      
        
        private void SecurityCenterMDIParent_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            //this.editStaffToolStripMenuItem.Enabled = false;
            ShowInitStaff();

            if (!m_curStaff.CheckModuleFeature(EliteModule.SecurityCenter, 11))
            {
                psotitionMenu.Enabled = false;
                psotitionMenu.Visible = false;
            }
        }
        public void ShowNewStaff()
        {
            EnableCopyMenu(false);            // Rally DE1778 - If the user cannot copy or paste anything then don't display the edit feature.
            EnablePasteMenu(false);

            if (IsFormLoaded("NewStaff"))
            {
                return;                //Bring the form to the front
            }

            this.SuspendLayout();
            mNewStaffForm = new NewStaff();            // Create a new instance of the child form.
            mNewStaffForm.MdiParent = this;       // Make it a child of this MDI form before showing it.
            mNewStaffForm.WindowState = FormWindowState.Maximized;
            mNewStaffForm.FormClosed += new FormClosedEventHandler(mNewStaffForm_FormClosed);
            mNewStaffForm.Show();            //childForm.Text = "Window " + childFormNumber++;
            this.Text = Properties.Resources.titleSecurityCenter;           
            this.ResumeLayout(true);
            this.PerformLayout();        
        }
        public void ShowPositionForm(bool isNewPosition)
        {
           
            if (IsFormLoaded("Position"))
            {
                //Bring the form to the front
                mPositionForm.IsLoading = false;
                return;
            } 
            
            this.SuspendLayout();
            // Create a new instance of the child form.
            mPositionForm = new Position(isNewPosition);
            //mPositionFormNumber++;
            // Make it a child of this MDI form before showing it.                
            mPositionForm.MdiParent = this;
            mPositionForm.WindowState = FormWindowState.Maximized;
            mPositionForm.FormClosed += new FormClosedEventHandler(PositionForm_FormClosed);
            //mPositionForm.Text = Properties.Resources.Position + mPositionFormNumber;
            mPositionForm.IsLoading = false;
            mPositionForm.Show();
            this.Text = Properties.Resources.titleSecurityCenter;
            this.ResumeLayout(true);
            this.PerformLayout();
        }
        //ttp 50053, support copy position function
        public void EnableCopyMenu(bool isEnable)
        {
            copyToolStripMenuItem.Enabled = isEnable; 
            
        }
        //ttp 50053, support copy position function
        public void EnablePasteMenu(bool isEnable)
        {
            pasteToolStripMenuItem.Enabled = isEnable;
        }

        //START RALLY DE 6739 support disabling edit position when there are no positions
        public void EnablePositionEditMenu(bool isEnable)
        {
            editPositionToolStripMenuItem.Enabled = isEnable;
        }
        //END RALLY DE 6739

        private void PositionForm_FormClosed(object sender, EventArgs e)
        {
            //ttp 50053, support copy position function
            EnableCopyMenu ( false);
            EnablePasteMenu (false) ;
            CheckPositionsCount();//RALLY DE 6739
            //editPositionToolStripMenuItem.Enabled = true;
            //newPositionToolStripMenuItem.Enabled = true;
            if (((Position)sender).DialogResult != DialogResult.Cancel &&
                ((Position)sender).DialogResult != DialogResult.None )
            {
                ReloadInitStaff();
            }
            else if (mInitStaffForm != null && mInitStaffForm.IsDisposed != true)
            {
                this.SuspendLayout();
                mInitStaffForm.WindowState = FormWindowState.Maximized;
                mInitStaffForm.StartPosition = FormStartPosition.CenterParent;
                mInitStaffForm.BringToFront();
                this.ResumeLayout(true);
                this.PerformLayout();
            }
            else
            {
                MakeupMDI();
            }
           
            
        }
        private void ShowInitStaff()
        {
            this.SuspendLayout();      
            mInitStaffForm = new NewStaff();
            mInitStaffForm.MdiParent = this;
            //mInitStaffForm.OnStaffSelected += new StaffSelectedEventHandler(InitStaff_Staff_Click);
                          
           // mInitStaffForm.TopMost = true; 
           // mInitStaffForm.Text = Properties.Resources.ViewStaff;
            mInitStaffForm.Show();
            mInitStaffForm.WindowState = FormWindowState.Maximized;
            mInitStaffForm.StartPosition = FormStartPosition.CenterParent;
            mInitStaffForm.FormClosed +=new FormClosedEventHandler(mNewStaffForm_FormClosed);
            this.Text = Properties.Resources.titleSecurityCenter;
            Application.DoEvents();
            this.ResumeLayout(true);
            this.PerformLayout();                   
        }
        private void MakeupMDI()
        {
            if (this.MdiChildren.Length > 0)
            {
                Form child = this.MdiChildren[0];
                child.WindowState = FormWindowState.Maximized;
                child.BringToFront();
            }
        }
        private void mNewStaffForm_FormClosed(object sender, FormClosedEventArgs  e)
        {            
            newStaffToolStripMenuItem.Enabled = true;
            //if (isAddedStaff == true)
            //{   //reload the Init Staff
            //    ReloadInitStaff();
            //}
            //else 
            if (mInitStaffForm != null && mInitStaffForm.IsDisposed != true)
            {
                mInitStaffForm.SuspendLayout();
                this.SuspendLayout();
                mInitStaffForm.WindowState = FormWindowState.Maximized;
                mInitStaffForm.ResumeLayout();
                mInitStaffForm.PerformLayout();
                this.ResumeLayout();
                this.PerformLayout();
            }
            else
            {
                MakeupMDI();
            }
           
        }

        private void ReloadInitStaff()
        {
            this.Cursor = Cursors.WaitCursor;
            WaitForm waiting = new WaitForm();            
            waiting.Message = Properties.Resources.splashInfoLoadStaffPositionModule;
            
            //waiting.StartPosition = FormStartPosition.CenterParent;
            waiting.WaitImage = Properties.Resources.Waiting;
            waiting.CancelButtonVisible = true;
            waiting.ProgressBarVisible = false;
            LoadStaffPosition(waiting, Configuration.operatorID);
            waiting.ShowDialog(); //Block until we are done

            try
            {
               //mStaffList = new GetStaffList(Configuration.operatorID, true);
               //mStaffList.Send(); //we have got all staff datas
               if( mInitStaffForm != null)
                {
                    mInitStaffForm.Close();
                }                
                ShowInitStaff ();
            }
            catch (Exception ex)
            {
                Logger.LogSevere((new StackFrame(true)).GetMethod().ToString(), (new StackFrame(true)).GetFileName() + "--" + ex.Message, (new StackFrame(true)).GetFileLineNumber());
                MessageForm.Show(Properties.Resources.errorFailedLoadInitStaff, Properties.Resources.securityCenter);
            }
            finally 
            {
                if (! waiting.IsDisposed) waiting.CloseForm();
                this.Cursor = Cursors.Default;
            }            
        }

        private void LoadStaffPosition(WaitForm waitingForm, int operatorID)
        {

            mWaitingForm= waitingForm;
            // Set the wait message.
            mWaitingForm.Message = Properties.Resources.splashInfoLoadStaffPositionModule;
            mWaitingForm.CancelButtonClick += new EventHandler(CancelBackgroundWork);
            // Create the worker thread and run it.
            m_worker = new BackgroundWorker();
            m_worker.WorkerReportsProgress = true;
            m_worker.WorkerSupportsCancellation = true;
            m_worker.DoWork += new DoWorkEventHandler(DoLoadStaffPositionData);
            m_worker.ProgressChanged += new ProgressChangedEventHandler(mWaitingForm.ReportProgress);
            m_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DoLoadStaffPositionDataCompleted);
            m_worker.RunWorkerAsync(operatorID);
        
        }

        private void CancelBackgroundWork(object sender, EventArgs e)
        {
            m_worker.CancelAsync();
        }
        private void DoLoadStaffPositionData(object sender, DoWorkEventArgs doEA)
        {
            GetStaffList workerStaffList = new GetStaffList((int) doEA.Argument, true);
            workerStaffList.Send(); //we have got all staff datas 
            doEA.Result = workerStaffList;
        }
        private void DoLoadStaffPositionDataCompleted(object sender, RunWorkerCompletedEventArgs RunEA)
        {
            if (RunEA.Error != null)
            {
                MessageForm.Show(RunEA.Error.Message, Properties.Resources.securityCenter);
            }
            else if (RunEA.Cancelled == false)
            {
                mStaffList = (GetStaffList)RunEA.Result;
            }
            mWaitingForm.CloseForm();
            mWaitingForm = null;
            
        
        }
        //private void InitStaff_Staff_Click(object sender, int selectedIndex)
        //{
        //    if (selectedIndex > -1)
        //    {
        //        this.editStaffToolStripMenuItem.Enabled = true;
        //    }
        //    else
        //    {
        //        this.editStaffToolStripMenuItem.Enabled = false;
        //    }
        //}
        
        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool IsFormLoaded(string strFormName)
        {
            this.SuspendLayout();
            bool blnFound = false;
           
            if (this.MdiChildren.Length > 0)
            {
                Form[] mdiChild = this.MdiChildren;
                foreach (Form frmTest in mdiChild)
                {
                    string strTemp = frmTest.Name;
                    //strTemp = strTemp.IndexOf(strFormName).ToString();
                    if (strTemp.Equals (strFormName))
                    {
                        blnFound = true;
                        //frmTest.SuspendLayout();
                        //SetMDIFormValues(frmTest);
                        frmTest.WindowState = FormWindowState.Maximized;
                        frmTest.Dock = DockStyle.Fill;
                        frmTest.BringToFront();
                        //frmTest.ResumeLayout();
                        //frmTest.PerformLayout();
                        break;
                    }
                }
            }
            this.ResumeLayout(true);
            this.PerformLayout();

            return blnFound;
        }
        private void SetMDIFormValues(Form frmTemp)
        {
            frmTemp.AutoScroll = false;
            frmTemp.ControlBox = false;
            frmTemp.MdiParent = this;
            frmTemp.WindowState = FormWindowState.Maximized;
            frmTemp.Dock = DockStyle.Fill;
            frmTemp.Visible = true;
        }
        //START RALLY DE 6739
        private void CheckPositionsCount()
        {
            GetPositionList positionList = new GetPositionList(Constants.GETALL_INSERT);
            try
            {
                positionList.Send();
            }
            catch (Exception ex)
            {
                MessageForm.Show(Properties.Resources.errorFailedToGetData + " " + ex.Message, Properties.Resources.securityCenter);
                return;
            }
            DataView mPositionsView = new DataView(positionList.Positions.PositionTable);

            if (mPositionsView.Count > 0)
            {
                EnablePositionEditMenu(true);
            }
            else
            {
                EnablePositionEditMenu(false);
            }
            
        }
        //END RALLY DE 6739
        

        private void newPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPositionModified();            
            SetNewPostionContextMenu(true);
            ShowPositionForm(true);
           
        }
        //ttp 50053, support copy position function
        private bool IsPositionBoard()
        {
            string text = Clipboard.GetText();
            if (text != string.Empty && text.StartsWith (Configuration.POSITIONPREFIX)) 
            {return true;}
            else return false; 

        }
        private void checkPositionModified()
        {
            if (mPositionForm != null && mPositionForm.IsModified)
            {
                if (MessageForm.Show(Properties.Resources.warningCancel, Properties.Resources.securityCenter, MessageFormTypes.YesNo)
                                            == DialogResult.Yes)
                {
                    mPositionForm.IsModified = false;
                }
                else
                    return;
            }
            if (mPositionForm != null)
                mPositionForm.Close();
        }
        internal void SetNewPostionContextMenu(bool isNew)
        {
            if (isNew)
            {
                //ttp 50053, support copy position function
                if (IsPositionBoard())
                {
                    EnablePasteMenu(true);
                }
                //editPositionToolStripMenuItem.Enabled = false;
                //newPositionToolStripMenuItem.Enabled = true;
            }
            else
            {
                //ttp 50053, support copy position function
                EnableCopyMenu(true);
                EnablePasteMenu(false);

                //editPositionToolStripMenuItem.Enabled = true;
                //newPositionToolStripMenuItem.Enabled = false;
            }
        }
        private void editPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPositionModified(); 
            SetNewPostionContextMenu(false);
            if (IsFormLoaded("Position"))
            {
                //Bring the form to the front
                return;
            }
            ShowPositionForm(false);
            
        }

        private void newStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            checkPositionModified();
            checkMachineModified();
            //ShowNewStaff();
            editPositionToolStripMenuItem.Enabled = true;
            newPositionToolStripMenuItem.Enabled = true;
        }

       

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //about window
            AboutBox about = new AboutBox();
            about.AssemblyDescription = AssemblyDescription;
            about.AssemblyProduct = AssemblyProduct;
            about.AssemblyVersion = AssemblyVersion;
            about.AssemblyTitle = AssemblyTitle;
            about.ShowDialog();
        }

        #region Assembly Attribute Accessors        

        public string AssemblyTitle
        {
            get
            {
                // Get all Title attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                // If there is at least one Title attribute
                if (attributes.Length > 0)
                {
                    // Select the first one
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    // If it is not an empty string, return it
                    if (titleAttribute.Title != "")
                        return titleAttribute.Title;
                }
                // If there was no Title attribute, or if the Title attribute was the empty string, return the .exe name
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
           
            get
            {
                // Get all Description attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                // If there aren't any Description attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Description attribute, return its value
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
           
            get
            {
                 // Get all Product attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                // If there aren't any Product attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Product attribute, return its value
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {              
                // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
           
            get
            {
               // Get all Company attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                // If there aren't any Company attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Company attribute, return its value
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
        //ttp 50053, support copy position function
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is Position)
            { 
                //copy the data to the clipboard
                ((Position)(this.ActiveMdiChild)).Copy();

                // Rally DE1778
                if(((Position)(this.ActiveMdiChild)).IsNewPosition == true)
                    EnablePasteMenu(true);
            }
        }
        //ttp 50053, support copy position function
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is Position)
            {
                Position position = (Position)this.ActiveMdiChild;
                if (position.IsNewPosition == true)
                {
                    position.Paste();//paste the data from clipboard to current form
                }
            }
        }

        //Start Rally TA10562
        /// <summary>
        /// Occurs when the Machine MenuItem is clicked and Brings up the MachineForm
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        private void MachineMenu_Click(object sender, EventArgs e)
        {
            if (IsFormLoaded("MachineForm"))
            {
                return;
            }
            this.SuspendLayout();
            mMachineForm = new MachineForm();           
            mMachineForm.MdiParent = this;
            mMachineForm.WindowState = FormWindowState.Maximized;
            mMachineForm.FormClosed += new FormClosedEventHandler(mMachineForm_FormClosed);
            mMachineForm.Show();
            this.Text = Properties.Resources.titleSecurityCenter;
            this.ResumeLayout(true);
            this.PerformLayout();            
        }

        /// <summary>
        /// Occurs when the MachineForm is closed and brings up the StaffForm
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The event args</param>
        void mMachineForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (mInitStaffForm != null && mInitStaffForm.IsDisposed != true)
            {
                this.SuspendLayout();
                mInitStaffForm.WindowState = FormWindowState.Maximized;
                mInitStaffForm.StartPosition = FormStartPosition.CenterParent;
                mInitStaffForm.BringToFront();
                this.ResumeLayout(true);
                this.PerformLayout();
            }
            else
            {
                MakeupMDI();
            }
        }

        /// <summary>
        /// Closes the MachineForm
        /// </summary>
        private void checkMachineModified()
        {
            if (mMachineForm != null)
            {
                mMachineForm.Close();
            }
        }
        //End Rally TA10562

    }
}
