#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  © 2011 GameTech International, Inc.
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel;
using GameTech.Elite.Base;

namespace GameTech.Elite.Client.Modules.SessionSummary.Business
{
    /// <summary>
    /// The interface representing the summary controller.
    /// </summary>
    internal interface ISummaryController : INotifyPropertyChanged
    {
        #region Member Events
        /// <summary>
        /// Occurs when the controller returns a list of sessions based on a
        /// particular date.
        /// </summary>
        event EventHandler<RequestSessionsEventArgs> RequestSessionsCompleted;
        /// <summary>
        /// Occurs when the controller returns a summary for a session base on
        /// a date and session number
        /// </summary>
        event EventHandler<RequestSummaryEventArgs> RequestSummaryCompleted;
        /// <summary>
        /// Occurs when the controller return for a request get for a calculated summary
        /// </summary>
        event EventHandler<RequestCalcdSummaryEventArgs> RequestCalculatedSummaryCompleted;
        /// <summary>
        /// Occurs when the controller returns from a save session summary event
        /// </summary>
        event AsyncCompletedEventHandler RequestSetSummaryCompleted;

        #endregion

        #region Member Methods
        /// <summary>
        /// Requests a list of session on the specified date.
        /// </summary>
        /// <param name="date">The date of the session list wanted.</param>
        void RequestSessions(DateTime date);

        /// <summary>
        /// Requests a summary for the session on the specified date
        /// </summary>
        /// <param name="date">date of the session</param>
        /// <param name="session">session number to retrieve the summary for</param>
        void RequestSummary(DateTime date, short session);

        /// <summary>
        /// Requests a system calculated summary for the session on the specified date
        /// </summary>
        /// <param name="date">date of the session</param>
        /// <param name="session">session number to retrieve the summary for</param>
        void RequestCalcdSummary(DateTime date, short session);

        /// <summary>
        /// Requests a session summary be saved
        /// </summary>
        /// <param name="summary">The session summary to save</param>
        void RequestSetSummary(Summary summary);

        /// <summary>
        /// select session costs.
        /// </summary>
        void SelectSessionCosts(Summary summary);

        /// <summary>
        /// Generates a session summary report and displays it.
        /// </summary>
        /// <param name="date">The date of the session.</param>
        /// <param name="session">The number of the session.</param>
        void GenerateReport(DateTime date, short session);
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets whether the controller is performing a long running operation.
        /// </summary>
        bool IsBusy
        {
            get;
        }

        /// <summary>
        /// Reports any progress when the controller is performing a long
        /// running operation.
        /// </summary>
        string ProgressText
        {
            get;
        }

        /// <summary>
        /// Gets the current gaming date time
        /// </summary>
        DateTime GamingDate
        {
            get;
        }

        /// <summary>
        /// Gets or sets the list of staff
        /// </summary>
        IEnumerable<Staff> StaffMembers
        {
            get;
        }

        /// <summary>
        /// Gets the Id of the staff member currently logged in
        /// </summary>
        Staff StaffMember
        {
            get;
        }

        #endregion
    }

    /// <summary>
    /// Provides data for the GetSessionListCompleted event.
    /// </summary>
    internal class RequestSessionsEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the RequestSessionsEventArgs class.
        /// </summary>
        /// <param name="sessions">The list of sessions.</param>
        public RequestSessionsEventArgs(IList<short> sessions)
        {
            Sessions = sessions;
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets the list of sessions, or null if an error occurred.
        /// </summary>
        public IList<short> Sessions
        {
            get;
            protected set;
        }
        #endregion
    }

    /// <summary>
    /// Provides data for the request summary event
    /// </summary>
    internal class RequestSummaryEventArgs : EventArgs
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the RequestSummaryEventArgs class.
        /// </summary>
        /// <param name="summary">The session summary</param>
        public RequestSummaryEventArgs(Summary calcdSummary, Summary savedSummary)
        {
            CalcdSummary = calcdSummary;
            SavedSummary = savedSummary;
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets the caculated summary.
        /// </summary>
        public Summary CalcdSummary
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets the saved summary.
        /// </summary>
        public Summary SavedSummary
        {
            get;
            protected set;
        }
        #endregion
    }

    /// <summary>
    /// Provides data for the request calc summary event
    /// </summary>
    internal class RequestCalcdSummaryEventArgs : EventArgs
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RequestCalcdSummaryEventArgs class.
        /// </summary>
        /// <param name="summary">The session summary</param>
        public RequestCalcdSummaryEventArgs(Summary summary)
        {
            Summary = summary;
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets the calculated summary
        /// </summary>
        public Summary Summary
        {
            get;
            private set;
        }
        #endregion
    }

}
