#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.Globalization;
using GameTech.Elite.Base;
using GameTech.Elite.UI;

namespace GameTech.Elite.Client.Modules.SessionSummary.Business
{
    /// <summary>
    /// Contains system settings that pertain to Session Summary.
    /// </summary>
    internal class SessionSummarySettings : EliteModuleSettings
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SessionSummarySettings class.
        /// </summary>
        public SessionSummarySettings()
        {
            DisplayMode = DisplayMode.Windowed;
            UseAcceleration = true;
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Parses the specified setting and loads it into the object, if
        /// valid.
        /// </summary>
        /// <param name="setting">The system setting to parse.</param>
        /// <exception cref="GameTech.Elite.Base.InvalidModuleSettingException">
        /// The specified setting was invalid.</exception>
        public override void LoadSetting(SettingValue setting)
        {
            try
            {
                switch(setting.Id)
                {
                    case Setting.UseHardwareAcceleration:
                        UseAcceleration = Convert.ToBoolean(setting.Value, CultureInfo.InvariantCulture);
                        break;

                    default:
                        base.LoadSetting(setting);
                        break;
                }
            }
            catch(InvalidModuleSettingException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new InvalidModuleSettingException(setting, ex);
            }
        }

        /// <summary>
        /// Parses the specified setting and loads it into the object, if
        /// valid.
        /// </summary>
        /// <param name="setting">The license setting to parse.</param>
        /// <exception cref="GameTech.Elite.Base.InvalidModuleSettingException">
        /// The specified setting was invalid.</exception>
        public override void LoadSetting(LicenseSettingValue setting)
        {
            try
            {
                switch(setting.Id)
                {
                    default:
                        base.LoadSetting(setting);
                        break;
                }
            }
            catch(InvalidModuleSettingException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new InvalidModuleSettingException(setting, ex);
            }
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or sets the display mode of Session Summary.
        /// </summary>
        public DisplayMode DisplayMode
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets whether to use hardware acceleration for rendering.
        /// </summary>
        public bool UseAcceleration
        {
            get;
            set;
        }

        #endregion
    }
}