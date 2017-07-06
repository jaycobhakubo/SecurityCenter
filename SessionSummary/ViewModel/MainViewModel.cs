#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.Windows.Input;
using GameTech.Elite.Base;
using GameTech.Elite.Client.Modules.SessionSummary.Business;
using GameTech.Elite.UI;

namespace GameTech.Elite.Client.Modules.SessionSummary.ViewModel
{
    /// <summary>
    /// Main View model class for displaying the session summary module
    /// </summary>
    internal class MainViewModel : ViewModelBase
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// <param name="controller">The ISessionSummaryController instance that owns this view model.</param>
        public MainViewModel(ISessionSummaryController controller)
        {
            if (controller == null)
                throw new ArgumentNullException("controller");

            Controller = controller;
            SummaryVM = new SummaryViewModel(controller.SummaryController);

            FileExitCommand = new RelayCommand((parameter) => Exit());
            EditSessionCostsCommand = new RelayCommand((parameter) => SessionCosts());
            HelpAboutCommand = new RelayCommand((parameter) => About());

        }
        #endregion

        #region Member Methods

        /// <summary>
        /// the avaialable session costs
        /// </summary>
        private void SessionCosts()
        {
            Controller.NavigateTo(SessionSummaryView.SessionCosts, null);
        }

        /// <summary>
        /// Exits the program
        /// </summary>
        private void Exit()
        {
            Controller.StartExit();
        }

        /// <summary>
        /// Opens the about window
        /// </summary>
        private void About()
        {
            Controller.NavigateTo(SessionSummaryView.About, null);
        }

        #endregion

        #region Member Properties
        /// <summary>
        /// Get the view models controller
        /// </summary>
        public ISessionSummaryController Controller
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the main view models sub view model summary view model
        /// </summary>
        public SummaryViewModel SummaryVM
        {
            get;
            private set;
        }

        #endregion

        #region Member ICommand Properties

        /// <summary>
        /// Gets the command that corresponds to exit the application
        /// </summary>
        public ICommand FileExitCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to corresponds to editing session costs
        /// </summary>
        public ICommand EditSessionCostsCommand
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the command to open the about window
        /// </summary>
        public ICommand HelpAboutCommand
        {
            get;
            private set;
        }
        #endregion
    }
}
