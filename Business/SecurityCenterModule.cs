// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Diagnostics;
using GTI.Modules.Shared;
using GTI.Controls;
using GTI.Modules.SecurityCenter.Properties;

namespace GTI.Modules.SecurityCenter
{
    /// <summary>
    /// The implementation of the IGTIModule COM interface.
    /// </summary>
    [
        ComVisible(true),
        Guid("04E26A95-4F28-458b-8A66-F4C677D386C2"),
        ClassInterface(ClassInterfaceType.None),
        ComSourceInterfaces(typeof(_IGTIModuleEvents)),
        ProgId("GTI.Modules.SecurityCenter.SecurityCenterModule")
    ]
    public sealed class SecurityCenterModule : IGTIModule
    {
        #region Win32 Interop Declarations
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_SHOWWINDOW = 0x0040;

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        #endregion

        #region Constants and Data Types
        private const string ModuleName = "GameTech Elite SecurityCenter Module";
        #endregion

        #region Events
        /// <summary>
        /// The signature of the 'Stopped' COM connection point handler.
        /// </summary>
        /// <param name="moduleId">The id of the stopped module.</param>
        public delegate void IGTIModuleStoppedEventHandler(int moduleId);

        /// <summary>
        /// The event that will translate to the COM connection point.
        /// </summary>
        public event IGTIModuleStoppedEventHandler Stopped;

        /// <summary>
        /// Occurs when something wants to stop itself.
        /// </summary>
        internal event EventHandler StopMe;

        internal event EventHandler BringToFront;

        // PDTS 966
        /// <summary>
        /// Occurs when a server initiated message was received from the 
        /// server.
        /// </summary>
        internal event MessageReceivedEventHandler ServerMessageReceived; 
        #endregion

        #region Member Variables
//        private Operator m_currentOp;
        private Staff m_currentStaff;

        private object m_syncRoot = new object();
        private int m_moduleId = 0;
        private bool m_isStopped = true;
        private Thread m_posThread = null;
        SplashScreen mSplashScreen = null;
        SecurityCenterMDIParent sc;

        #endregion

        #region Member Methods
        /// <summary>
        /// Starts the module.  If the module is already started nothing
        /// happens.  This method will block if another thread is currently
        /// executing it.
        /// </summary>
        /// <param name="moduleId">The id to be given to the module.</param>
        public void StartModule(int moduleId)
        {
            lock (m_syncRoot)
            {
                // Don't start again if we are already started.
                if (!m_isStopped)
                    return;
                               
                // Assign the id.
                m_moduleId = moduleId;

                // Create a thread to run.
                m_posThread = new Thread(Run);

                // Change the thread regional settings to the current OS 
                // globalization info.
                m_posThread.CurrentUICulture = CultureInfo.CurrentCulture;
                //ttp 50053, support copy position function
                m_posThread.SetApartmentState(ApartmentState.STA);

                // Start it.
                m_posThread.Start();

                // Mark the module as started.
                m_isStopped = false;
            }
        }

        /// <summary>
        /// Shows or hides the Windows Taskbar.
        /// </summary>
        /// <param name="show">Whether to show or hide the Taskbar.</param>
        private static void ShowTaskbar(bool show)
        {
            IntPtr hWnd = FindWindow("Shell_TrayWnd", "");

            uint windowState = SWP_SHOWWINDOW;

            if (!show)
                windowState = SWP_HIDEWINDOW;

            SetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0, windowState);
        }

        /// <summary>
        /// Gets the specified staff from the server.
        /// </summary>
        /// <param name="staffId">The id of the staff to retrieve.</param>
        /// <returns>A Staff object.</returns>
        private Staff GetStaff(int staffId)
        {
            GetStaffDataMessage staffDataMsg = new GetStaffDataMessage(staffId);

            try
            {
                staffDataMsg.Send();
            }
            catch (Exception e)
            {
                ReformatException(e);
            }

            // We only care about the first staff if more than one was 
            // returned.
            Staff[] staffList = staffDataMsg.StaffList;

            if (staffList == null || staffList.Length == 0)
                throw new ModuleException(Resources.StaffNotFound);

            return staffList[0];
        }

        /// <summary>
        /// Creates the Security Center object and blocks until the Security Center is told to close
        /// or the user closes the Security Center.
        /// </summary>
        private void Run()
        {
            
            //SecurityMDI sc = null;
            sc = new SecurityCenterMDIParent();

            try
            {                
                if (mSplashScreen == null)
                {
                    SetSplashScreen();
                }
                mSplashScreen.Show();
                mSplashScreen.Status = Properties.Resources.splashInfoLoadStationSettings;

                //get intial module level data
                ModuleComm comm = new ModuleComm();
                Configuration.operatorID = comm.GetOperatorId();
                Configuration.LoginStaffID = comm.GetStaffId();
                Configuration.mMachineID = comm.GetMachineId();
                GetWorkstationSettings();
                GetPasswordSettings();

                // Create and initialize new object.
                mSplashScreen.Status = Properties.Resources.splashInfoLoadModuleLevelData;
                sc.LoadData();

                // Listen for the event where something wants the Security Center to stop.
                StopMe += new EventHandler(sc.ExitSecurityCenter);
                BringToFront += new EventHandler(BringToFrontEvent);

                // Initialize Logger
                Utilities.InitLog();
                Utilities.Log("SecurityCenter start running...", LoggerLevel.Information);

                //Start Fix RALY DE 2991
                //// Get our current operator
                //try
                //{
                //    m_currentOp = GetOperator(comm.GetOperatorId());
                //}
                //catch (Exception e)
                //{
                //    MessageForm.Show(mSplashScreen, string.Format(CultureInfo.CurrentCulture, Resources.GetOperatorDataFailed, e.Message), Resources.SecurityCenterName);

                //    Utilities.Log("Get operator data failed: " + e.Message, LoggerLevel.Severe);
                //    return;
                //}
                //End FIX RALLY DE 2991

                // Get our Staff and module permissions
                try
                {
                    m_currentStaff = GetStaff(comm.GetStaffId());
                }
                catch (Exception e)
                {
                    MessageForm.Show(mSplashScreen, string.Format(CultureInfo.CurrentCulture, Resources.GetStaffDataFailed, e.Message), Resources.SecurityCenterName);

                    Utilities.Log("Get staff info failed: " + e.Message, LoggerLevel.Severe);
                    return;
                }

                // Get our Permissions
                try
                {
                    GetStaffPermissionList();
                }
                catch (Exception e)
                {
                    MessageForm.Show(mSplashScreen, string.Format(CultureInfo.CurrentCulture, Resources.GetStaffPermsFailed, e.Message), Resources.SecurityCenterName);

                    Utilities.Log("Get staff module features failed: " + e.Message, LoggerLevel.Severe);
                    return;
                }

                mSplashScreen.CloseForm();
                mSplashScreen.Dispose();

                sc.CurentStaff = m_currentStaff;
                Application.Run(sc);            
            }
            catch (Exception e)
            {
                Utilities.Log("Exception.Message =" + e.Message, LoggerLevel.Severe);
                Utilities.Log("Exception.Stack=" + e.StackTrace, LoggerLevel.Severe);
                MessageForm.Show(Properties.Resources.errorStart, Properties.Resources.securityCenter);
            }
            finally 
            { // Shutdown
                if (mSplashScreen !=null )
                {
                    mSplashScreen.Close ();
                    mSplashScreen.Dispose();
                }
                if (sc != null)
                {
                    sc.Close();
                    sc = null;
                }

                OnStop();
                lock (m_syncRoot)
                {
                    // Mark the module as stopped.
                    m_isStopped = true;
                }
                Utilities.Log("Leaving SecurityCenter...", LoggerLevel.Information);
            }
        }
        private void SetMDIFormNormal()
        {
            sc.WindowState = FormWindowState.Normal;
            sc.Activate();
        }
        private void BringToFrontEvent(object sender, EventArgs e)
        {
             Utilities.LogInfoIN();;
            
            if (sc != null)
            {
                if (sc.InvokeRequired)
                {
                    Logger.LogInfo("mReportMDI is not null, calling ComeToFrontEvent", (new StackFrame(true)).GetFileName(), (new StackFrame(true)).GetFileLineNumber());
                            
                    sc.Invoke(new MethodInvoker (SetMDIFormNormal));
                }
                else
                {
                    SetMDIFormNormal();
                }
            }
        }
        /// <summary>
        /// Gets the settings from the server.
        /// </summary>
        private void GetWorkstationSettings()
        {
            // Send message for global settings.
            // Rally DE130
            GetSettingsMessage settingsMsg = new GetSettingsMessage(Configuration.mMachineID, Configuration.operatorID, SettingsCategory.GlobalSystemSettings);
            try
            {
                settingsMsg.Send();
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message, Properties.Resources.securityCenter);
                throw ex;
            }

            // Set the workstation id.
            //m_workstationId = settingsMsg.WorkstationId;

            // Loop through each setting and parse the value.
            SettingValue[] stationSettings = settingsMsg.Settings;
            try
            {
                foreach (SettingValue setting in stationSettings)
                {
                    Configuration.LoadSetting(setting);
                }
            }
            catch (Exception ex)
            {
                MessageForm.Show(Properties.Resources.errorParse + ": " +ex.Message, Properties.Resources.securityCenter);
                throw ex;
            }
        }

        private void GetPasswordSettings()
        {
            GetSettingsMessage pswdsettingmsg = new GetSettingsMessage(Configuration.mMachineID, Configuration.operatorID, SettingsCategory.Security);
            try
            {
                pswdsettingmsg.Send();
            }
            catch(Exception ex)
            {
                MessageForm.Show(ex.Message,Properties.Resources.securityCenter);
                throw ex;
            }
            SettingValue[] stationsettings = pswdsettingmsg.Settings;
            try
            {
                foreach (SettingValue setting in stationsettings)
                {
                    Configuration.LoadSetting(setting);
                }
            }
            catch (Exception ex)
            {
                MessageForm.Show(ex.Message,Properties.Resources.securityCenter);
                throw ex;
            }
                
        }

        private void SetSplashScreen()
        {
            if (mSplashScreen == null)
            {
                mSplashScreen = new SplashScreen();
            }
            mSplashScreen.ApplicationName = Resources.assemblyTitle; //RALLY DE 6830 incorrect name on splash screen      
            mSplashScreen.Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            
            //set the labe, and picture images 
        }
        /// <summary>
        /// This method blocks until the module is stopped.  If the module is 
        /// already stopped nothing happens.
        /// </summary>
        public void StopModule()
        {
            if (m_posThread != null)
            {
                // Send the stop event to module's controller.
                EventHandler stopHandler = StopMe;

                if (stopHandler != null)
                    stopHandler(this, new EventArgs());

                m_posThread.Join();
            }
        }

        /// <summary>
        /// Signals the COM connection point that we have stopped.
        /// </summary>
        internal void OnStop()
        {
            IGTIModuleStoppedEventHandler handler = Stopped;

            if (handler != null)
                handler(m_moduleId);
        }

        /// <summary>
        /// Returns the name of this GTI module.
        /// </summary>
        /// <returns>The module's name.</returns>
        public string QueryModuleName()
        {
            return ModuleName;
        }
        /// <summary>
        /// Used by EliteMCP to reload the SecurityCenter without restart a new instance
        /// </summary>
        public void ComeToFront()
        {
            EventHandler handler = BringToFront;

            if (handler != null)
                handler(this, new EventArgs());
        }

        // PDTS 966
        /// <summary>
        /// Tells the module that a server initiated message was received.
        /// </summary>
        /// <param name="commandId">The id of the message received.</param>
        /// <param name="messageData">The payload data of the message or null 
        /// if the message has no data.</param>
        public void MessageReceived(int commandId, object msgData)
        {
            MessageReceivedEventArgs args = new MessageReceivedEventArgs(commandId, msgData);

            MessageReceivedEventHandler handler = ServerMessageReceived;

            if(handler != null)
                handler(this, args);
        }

        /// <summary>
        /// Gets the current staff's permissions (module & features) from the 
        /// server.
        /// </summary>
        private void GetStaffPermissionList()
        {
            // First get all the modules this staff has permission to.
            GetStaffModulesMessage modMsg = new GetStaffModulesMessage(m_currentStaff.Id, 0);

            try
            {
                modMsg.Send();
            }
            catch (Exception e)
            {
                ReformatException(e);
            }

            // Parse which values we retrieved from the server.
            foreach (int moduleId in modMsg.ModuleList)
            {
                m_currentStaff.AddModule((EliteModule)moduleId);
            }

            // Get all the receipt mgmt permissions for the staff.
            GetStaffModuleFeaturesMessage permMsg = new GetStaffModuleFeaturesMessage(m_currentStaff.Id, (int)EliteModule.SecurityCenter, 0);

            try
            {
                permMsg.Send();
            }
            catch (Exception e)
            {
                ReformatException(e);
            }

            // Parse which values we retrieved from the server.
            foreach (int moduleFeatureId in permMsg.ModuleFeatureList)
            {
                m_currentStaff.AddModuleFeature(EliteModule.SecurityCenter, moduleFeatureId);
            }
        }

       

        /// <summary>
        /// Based on the exception passed in, this method will translate
        /// the error message to localized text and rethrow the exception as
        /// a POSException.  If the exception type is not recognized, then
        /// the exception is rethrown as is.
        /// </summary>
        /// <param name="ex">The exception to reformat.</param>
        internal static void ReformatException(Exception ex)
        {
            if (ex is MessageWrongSizeException)
                throw new ModuleException(string.Format(CultureInfo.CurrentCulture, Resources.MessagePayloadWrongSize, ex.Message), ex);
            else if (ex is ServerCommException)
                throw new ModuleException(Resources.ServerCommFailed, ex);
            else if (ex is ServerException && ex.InnerException != null)
                throw new ModuleException(string.Format(CultureInfo.CurrentCulture, Resources.InvalidMessageResponse, ex.Message), ex.InnerException);
            else if (ex is ServerException)
            {
                int errorCode = (int)((ServerException)ex).ReturnCode;
                throw new ModuleException(string.Format(CultureInfo.CurrentCulture, Resources.ServerErrorCode, errorCode), ex);
            }
            else
                throw ex;
        }
        #endregion
    }
}
