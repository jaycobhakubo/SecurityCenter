using System;
using System.Collections.Generic;
using System.Text;
using GTI.Modules.Shared;
using System.Drawing;
using System.Threading;
using System.Globalization;

namespace GTI.Modules.SecurityCenter
{
    class Configuration
    {
        //local configuration
        //ttp 50053, support copy position function
        internal const string POSITIONPREFIX = "--*Position--??";
        internal const string POSITIONDELIMINATOR = "*&?";
        //common configuration
        private static bool mForceEnglish = false;
        private static string mFontName = "Tahoma";
        private static Font mUIUniversualFont = new System.Drawing.Font(mFontName, 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private static Size mScreenSize = new Size(1024, 768);
        internal static int operatorID = -1;
        internal static int mMachineID = 0;
        internal static int LoginStaffID = -1;
        internal static int StaffLoginNumber = -1;
        internal static MSRSettings mMSRSettings = new MSRSettings();
        internal static bool m_showCursor = false;
        //Logger related
        public static bool mIsLogStarted = false;
        public static bool m_enableLogging =false;
//        private static int mLogLevel = 0; //for all
        public static string mLoggerName = "SecurityCenter_Logger";
        public static int mWindowLogLevel = -1; //not set yet, it should be 0-7
        public static int mFileLogLevel = -1;
        public static int mConsoleLogLevel = -1;
        public static int mDebugLogLevel = -1;
        public static int mDatabaseLogLevel = -1;
        public static int mSocketLogLevel = -1;
        public static int mUDPLogLevel = -1;
        public static int mEventLogLevel = -1;
        public static string mLogServerName = "";
        public static string mLogDatabaseName = "";
        public static string mLogUserName = "";
        public static string mLogPassword = "";
        public static string mLogSocketIPAddress = "";
        public static int mLogSocketPort = -1;
        public static string mUPDIPAdress = "";
        public static int mUDPPort = -1;
        public static long mRecycleDays = 7;
        public static int mMinimumPasswordLength = -1;
        public static int mAutomaticUnlockTime = -1;
        public static int mPinExpireDays = -1;
        public static int mPreviousPasswordNumber = -1;
        public static int mPasswordLockoutAttempts = -1;
        public static bool mPasswordComplexitySetting ;

        #region Public Properties
        public static void EnableFileLog(int level,long recycleDays)
        {
            mFileLogLevel = level;
            //mLogFileName = fileName;
            mRecycleDays = recycleDays;
            Logger.EnableFileLog(level, recycleDays);
        }
        public static void EnableConsoleLog(int level)
        {
            mConsoleLogLevel = level;
            Logger.EnableConsoleLog(level);
        }
        public static void EnableDebugLog(int level)
        {
            mDebugLogLevel = level;
            Logger.EnableDebugLog(level);
        }
        public static void EnableDatabaseLog(int level, string serverName, string dbName, string users, string pwd)
        {
            mDatabaseLogLevel = level;
            mLogServerName = serverName;
            mLogPassword = pwd;
            mLogUserName = users;
            mLogDatabaseName = dbName;
            Logger.EnableDatabaseLog(level, serverName, dbName, users, pwd);
        }
        public static void EnableSocketLog(int level, string ipAddress, int port)
        {
            mSocketLogLevel = level;
            mLogSocketIPAddress = ipAddress;
            mLogSocketPort = port;
            Logger.EnableSocketLog(level, ipAddress, port);            
        }
        public static void EnableUdpLog(int level, string udpAdress, int port)
        {
            mUDPLogLevel = level;
            mUPDIPAdress = udpAdress ;
            mUDPPort = port;
            Logger.EnableUdpLog(level, udpAdress, port);
        }
        public static void EnableEventLog(int level)
        {
            mEventLogLevel = level;
            Logger.EnableEventLog(level);
        }
            #endregion

        #region Public Methods
       
        public static bool ForceEnglish()
        {
            mForceEnglish = true;
            return ForceLocale("en-US");
        }
        public static bool ForceLocale(string locale)
        {
            try
            {               
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static void LoadSetting(SettingValue setting)
        {
            
                Setting param = (Setting)setting.Id;

                switch (param)
                {
                    case Setting.MagneticCardFilters:
                        mMSRSettings.setFilters(setting.Value);
                        break;

                    case Setting.MSRReadTriggers:
                        mMSRSettings.setReadTriggers(setting.Value);
                        break;

                    case Setting.ShowMouseCursor:
                        m_showCursor = Convert.ToBoolean(setting.Value);
                        break;

                    case Setting.ForceEnglish:
                        mForceEnglish = Convert.ToBoolean(setting.Value);
                        break;
                    case Setting.LoggingLevel:
                        mFileLogLevel = int.Parse(setting.Value);
#if DEBUG
                        mFileLogLevel = 0;
#endif

                        break;
                    case Setting.LogRecycleDays:
                        mRecycleDays = int.Parse(setting.Value);
                        break;

                    case Setting.EnableLogging:
                        m_enableLogging = Convert.ToBoolean(setting.Value);
#if DEBUG
                        m_enableLogging = true;
#endif
                        break;  
                       
                    case Setting.MinimumPasswordLength:
                        mMinimumPasswordLength = int.Parse(setting.Value);
                        break;

                    case Setting.AutomaticUnlockTime:
                        mAutomaticUnlockTime = int.Parse(setting.Value);
                        break;

                    case Setting.PinExpireDays:
                        mPinExpireDays = int.Parse(setting.Value);
                        break;

                    case Setting.PreviousPasswordNumber:
                        mPreviousPasswordNumber = int.Parse(setting.Value);
                        break;

                    case Setting.PasswordLockoutAttempts:
                        mPasswordLockoutAttempts = int.Parse(setting.Value);
                        break;

                    case Setting.PasswordComplexitySetting:
                        mPasswordComplexitySetting = Convert.ToBoolean(setting.Value);
                        break;


                }           
            
        }
#endregion 
    }
}
