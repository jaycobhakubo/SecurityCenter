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
        private bool mIsEditPosition = false;   //END RALLY DE 6739
        private bool mIsNewPosition = false;


        #endregion
        #region Constructor

        public SecurityCenterMDIParent()
        {
            Utilities.LogInfoIN();;
            InitializeComponent();
            Clipboard.Clear(); //clear other people's dirty board         //ttp 50053, support copy position function  
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
      
        #endregion
        #region Methods

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
                    if (strTemp.Equals(strFormName))
                    {
                        blnFound = true;
                        frmTest.SuspendLayout();
                        SetMDIFormValues(frmTest);
                        frmTest.BringToFront();
                        frmTest.ResumeLayout();
                        frmTest.PerformLayout();
                        break;
                    }
                }
            }
            this.ResumeLayout(true);
            this.PerformLayout();

            return blnFound;
        }

        private void CloseUIStaff()
        {
            if (mInitStaffForm != null)
            {
                if (mInitStaffForm.IsDisposed == false)
                {
                    mInitStaffForm.Close();
                }
            }

            if (mNewStaffForm != null)
            {
                if (mNewStaffForm.IsDisposed == false)
                {
                    mNewStaffForm.Close();
                }
            }
        }




        /// <summary>
        /// Closes the MachineForm
        /// </summary>
        private void checkMachineModified()
        {
            if (mMachineForm != null)
            {
                if (mMachineForm.IsDisposed == false)
                {
                    mMachineForm.Close();
                }
            }
        }
        //End Rally TA10562

       

        //ttp 50053, support copy position function
        //private bool IsPositionBoard()
        //{
        //    string text = Clipboard.GetText();
        //    if (text != string.Empty && text.StartsWith(Configuration.POSITIONPREFIX))
        //    { return true; }
        //    else return false;

        //}
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
            {
                if (mPositionForm.IsDisposed == false)
                {
                    mPositionForm.Close();
                }
            }
        }

        //ttp 50053, support copy position function
        private bool IsPositionBoard()
        {
            string text = Clipboard.GetText();
            if (text != string.Empty && text.StartsWith(Configuration.POSITIONPREFIX))
            { return true; }
            else return false;
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

        public void LoadData()
        {
            Utilities.LogInfoIN(); ;
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



        private void ReloadInitStaff()
        {
            this.Cursor = Cursors.WaitCursor;
            WaitForm waiting = new WaitForm();
            waiting.Message = Properties.Resources.splashInfoLoadStaffPositionModule;
            waiting.WaitImage = Properties.Resources.Waiting;
            waiting.CancelButtonVisible = true;
            waiting.ProgressBarVisible = false;
            mInitStaffForm.ReloadStaffPositionListBox(mInitStaffForm.SelectedStaffId);
            LoadStaffPosition(waiting, Configuration.operatorID);
            waiting.ShowDialog(); //Block until we are done

            try
            {

                mStaffList = new GetStaffList(Configuration.operatorID, true);//Why do we want the staff list here
                mStaffList.Send(); //we have got all staff datas

                this.SuspendLayout();
                mInitStaffForm.MdiParent = this;
                ShowStaffForm();
                mInitStaffForm.Show();

                Application.DoEvents();
                this.ResumeLayout(true);
                this.PerformLayout();

            }
            catch (Exception ex)
            {
                Logger.LogSevere((new StackFrame(true)).GetMethod().ToString(), (new StackFrame(true)).GetFileName() + "--" + ex.Message, (new StackFrame(true)).GetFileLineNumber());
                MessageForm.Show(Properties.Resources.errorFailedLoadInitStaff, Properties.Resources.securityCenter);
            }
            finally
            {
                if (!waiting.IsDisposed) waiting.CloseForm();
                this.Cursor = Cursors.Default;
            }

        }

        private void MakeupMDI()
        {
            if (this.MdiChildren.Length > 0)
            {
                Form child = this.MdiChildren[0];
                child.WindowState = FormWindowState.Maximized;
                //   child.Dock = DockStyle.Fill;
                child.BringToFront();
            }
        }

        private void SetMDIFormValues(Form frmTemp)
        {
            frmTemp.AutoScroll = false;
            frmTemp.ControlBox = false;
            frmTemp.MdiParent = this;
            frmTemp.WindowState = FormWindowState.Maximized;
            //  frmTemp.Dock = DockStyle.Fill;
            frmTemp.Visible = true;
        }


        //NewStaff Form
        private void ShowStaffForm() //Just created this one
        {
            this.SuspendLayout();
            if (mInitStaffForm == null || mInitStaffForm.IsDisposed)
            {
                mInitStaffForm = new NewStaff();
            }
            
            mInitStaffForm.MdiParent = this;
            mInitStaffForm.WindowState = FormWindowState.Maximized;
            mInitStaffForm.Show();
            this.Text = Properties.Resources.titleSecurityCenter;
            this.ResumeLayout(true);
            this.PerformLayout();
        }

        //NewStaff Form
        private void ShowInitStaff()
        {
            this.SuspendLayout();
            mInitStaffForm = new NewStaff();
            mInitStaffForm.MdiParent = this;
            mInitStaffForm.WindowState = FormWindowState.Maximized;
            mInitStaffForm.FormClosed += new FormClosedEventHandler(mNewStaffForm_FormClosed);
            mInitStaffForm.Show();
            this.Text = Properties.Resources.titleSecurityCenter;
            this.ResumeLayout(true);
            this.PerformLayout();

        }


        //NewStaff Form
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
            //mNewStaffForm.Dock = DockStyle.Fill;
            mNewStaffForm.FormClosed += new FormClosedEventHandler(mNewStaffForm_FormClosed);
            mNewStaffForm.Show();            //childForm.Text = "Window " + childFormNumber++;
            this.Text = Properties.Resources.titleSecurityCenter;
            this.ResumeLayout(true);
            this.PerformLayout();
        }


        //Position Form
        public void ShowPositionForm(bool isNewPosition)
        {
            if (IsFormLoaded("Position"))
            {
                mPositionForm.IsLoading = false;        //Bring the form to the front
                return;
            }

            this.SuspendLayout();
            mPositionForm = new Position(isNewPosition);            // Create a new instance of the child form.
            mPositionForm.MdiParent = this;            // Make it a child of this MDI form before showing it.   
            mPositionForm.WindowState = FormWindowState.Maximized;
            mPositionForm.FormClosed += new FormClosedEventHandler(PositionForm_FormClosed);
            mPositionForm.IsLoading = false;
            mPositionForm.Show();
            this.Text = Properties.Resources.titleSecurityCenter;
            this.ResumeLayout(true);
            this.PerformLayout();
        }

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

        #endregion
        #region Events

        private void SecurityCenterMDIParent_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            //this.editStaffToolStripMenuItem.Enabled = false;
            //ShowInitStaff();
            ShowStaffForm();

            if (!m_curStaff.CheckModuleFeature(EliteModule.SecurityCenter, 11))
            {
                psotitionMenu.Enabled = false;
                psotitionMenu.Visible = false;
            }
        }
        
            #region Click

        //ttp 50053, support copy position function
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ActiveMdiChild is Position)
            {
                //copy the data to the clipboard
                ((Position)(this.ActiveMdiChild)).Copy();

                // Rally DE1778
                if (((Position)(this.ActiveMdiChild)).IsNewPosition == true)
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

        private void newPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mInitStaffForm != null)
            {
                if (mInitStaffForm.IsDisposed == false)
                {
                    mInitStaffForm.Close();
                }
            }

            if (mNewStaffForm != null)
            {
                if (mNewStaffForm.IsDisposed == false)
                {
                    mNewStaffForm.Close();
                }
            }

            checkPositionModified();

            if (mIsEditPosition == true)
            {
                mPositionForm.Close();
            }
            checkMachineModified();
            mIsEditPosition = false;
            mIsNewPosition = true;
            SetNewPostionContextMenu(true);
            ShowPositionForm(true);
        }

        private void editPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mInitStaffForm != null)
            {
                if (mInitStaffForm.IsDisposed == false)
                {
                    mInitStaffForm.Close();
                }
            }

            if (mNewStaffForm != null)
            {
                if (mNewStaffForm.IsDisposed == false)
                {
                    mNewStaffForm.Close();
                }
            }
            //checkPositionModified();
            checkMachineModified();
            if (mIsNewPosition == true)
            {
                mPositionForm.Close();
            }
            mIsNewPosition = false;
            mIsEditPosition = true;
            SetNewPostionContextMenu(false);
            ShowPositionForm(false);

        }

        private void manageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (mInitStaffForm == null || mInitStaffForm.IsDisposed == true)
            {
                checkPositionModified();
                checkMachineModified();
                ShowStaffForm();
                //ShowNewStaff();
                editPositionToolStripMenuItem.Enabled = true;
                newPositionToolStripMenuItem.Enabled = true;
            }
        }

      

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ExitSecurityCenter(object sender, EventArgs e)
        {
            ExitToolsStripMenuItem_Click(sender, e);
            //clean up if any
        }


     
            #endregion            
            #region Open

        //Start Rally TA10562
            /// <summary>
            /// Occurs when the Machine MenuItem is clicked and Brings up the MachineForm
            /// </summary>
            /// <param name="sender">The sender</param>
            /// <param name="e">The event args</param>
            private void MachineMenu_Click(object sender, EventArgs e)
            {
                CloseUIStaff();
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

            private void newStaffToolStripMenuItem_Click(object sender, EventArgs e)
            {
                if (mInitStaffForm == null || mInitStaffForm.IsDisposed == true)
                {
                    checkPositionModified();
                    checkMachineModified();
                    ShowStaffForm();
                    //ShowNewStaff();
                    editPositionToolStripMenuItem.Enabled = true;
                    newPositionToolStripMenuItem.Enabled = true;
                }
                else
                {
                    ShowStaffForm();
                }
            }
            #endregion
            #region Close

            //POSITION 
            private void PositionForm_FormClosed(object sender, EventArgs e)
            {
                if (((Position)sender).DialogResult == DialogResult.Yes)
                {
                    if (mPositionForm.IsNewPosition == false)
                    {
                        //skip on save
                        if (mPositionForm.IsPositionNameChanged == true) //If new position is false then its modified
                        {
                            //ttp 50053, support copy position function
                            EnableCopyMenu(false);
                            EnablePasteMenu(false);
                            CheckPositionsCount();//RALLY DE 6739

                            if (mInitStaffForm != null && mInitStaffForm.IsDisposed != true)
                            {
                                WaitForm waiting = new WaitForm();
                                LoadStaffPosition(waiting, Configuration.operatorID);
                                waiting.ShowDialog();
                                mInitStaffForm.ReloadStaffPositionListBox(mInitStaffForm.SelectedStaffId);
                                Application.DoEvents();

                                mInitStaffForm.WindowState = FormWindowState.Maximized;
                                //mInitStaffForm.Dock = DockStyle.Fill;
                                mInitStaffForm.BringToFront();

                                Application.DoEvents();
                                this.ResumeLayout(true);
                                this.PerformLayout();
                            }
                            else
                            {
                                WaitForm waiting2 = new WaitForm();
                                LoadStaffPosition(waiting2, Configuration.operatorID);
                                waiting2.ShowDialog();
                                MakeupMDI();
                            }
                        }
                    }
                    //mInitStaffForm.ReloadUIStaffPositionCmbx();
                    CheckPositionsCount();
                    ShowStaffForm();
                }
                else
                {
                    //MakeupMDI();
                    ShowStaffForm();
                }
            }

            //STAFF 
            private void mNewStaffForm_FormClosed(object sender, FormClosedEventArgs e)
            {
                newStaffToolStripMenuItem.Enabled = true;
                //if (isAddedStaff == true)
                //{   //reload the Init Staff
                //    ReloadInitStaff();
                //}
                //else 
                if (mInitStaffForm != null && mInitStaffForm.IsDisposed != true)
                {
                    ShowStaffForm();
                    //mInitStaffForm.SuspendLayout();
                    //this.SuspendLayout();
                    //mInitStaffForm.WindowState = FormWindowState.Maximized;
                    //mInitStaffForm.ResumeLayout();
                    //mInitStaffForm.PerformLayout();
                    //this.ResumeLayout();
                    //this.PerformLayout();
                }
                else
                {
                    MakeupMDI();
                }

            }


            //MACHINE
            void mMachineForm_FormClosed(object sender, FormClosedEventArgs e)
            {
                if (mInitStaffForm != null && mInitStaffForm.IsDisposed != true)
                {
                    // ShowStaffForm();
                }
                else
                {
                    MakeupMDI();
                }
            }

                 #endregion
            #region Backgroundworkers

            private void LoadStaffPosition(WaitForm waitingForm, int operatorID)
            {

                mWaitingForm = waitingForm;
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

            //private void CancelBackgroundWork(object sender, EventArgs e)
            //{
            //    m_worker.CancelAsync();
            //}

            private void DoLoadStaffPositionData(object sender, DoWorkEventArgs doEA)
            {
                GetStaffList workerStaffList = new GetStaffList((int)doEA.Argument, true);
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

            private void CancelBackgroundWork(object sender, EventArgs e)
            {
                m_worker.CancelAsync();
            }

            #endregion
        #endregion
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
    }
}

