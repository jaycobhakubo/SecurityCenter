#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  ©2010 GameTech International, Inc.
#endregion

//Start Rally TA1056
using System;
using System.IO;
using System.Text;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    /// <summary>
    /// Represents the SetStaffMachineLogout
    /// </summary>
    internal class SetStaffMachineLogout:ServerMessage
    {
        #region Constants
        private const int MinResponseMessageLength = 4;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SetStaffMachineLogout class.
        /// </summary>
        public SetStaffMachineLogout()
            : this(new Machine())
        {

        }

        /// <summary>
        /// Initializes a new instance of the SetStaffMachineLogout class.
        /// </summary>
        /// <param name="machine">The Machine info to send to the server</param> 
        public SetStaffMachineLogout(Machine machine)
        {
            m_id = 18196;
            Machine = machine;                   
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Saves the Machine's Staff status to the server
        /// </summary>
        /// <param name="machine">The MachineID and staffID to be saved</param>
        internal static void Save(Machine machine)
        {
            var msg = new SetStaffMachineLogout(machine);
            try
            {
                msg.Send();
            }
            catch(ServerCommException ex)
            {
                throw new Exception("SetMachineStaffLogOut failed: "+ex.Message);
            }           
            
        }

        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        /// <param name="requestWriter">The binary stream writer that should
        /// be used to write any request data necessary.</param>
        protected override void PackRequest()
        {
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream,Encoding.Unicode);

            //MachineID
            requestWriter.Write(Machine.MachineID);

            //StaffID
            requestWriter.Write(Machine.staffdata.Id);

            m_requestPayload = requestStream.ToArray();

            requestWriter.Close();
        }

        /// <summary>
        /// Parses the response received from the server.
        /// </summary>
        /// <param name="responseReader">The binary stream reader that should
        /// be used to read any response data necessary.</param>
        protected override void UnpackResponse()
        {
            base.UnpackResponse();            
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or sets the Machine
        /// </summary>
        private Machine Machine 
        {
            get; 
            set; 
        }
        #endregion

    }
}
//End Rally TA10562
