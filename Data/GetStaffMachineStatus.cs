#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  ©2010 GameTech International, Inc.
#endregion

//Start Rally TA1056
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    /// <summary>
    /// Represents the GetStaffMachineStatus 
    /// </summary>
    internal class GetStaffMachineStatus:ServerMessage
    {
        #region Constants
        private const int MinResponseMessageLength = 6;
        #endregion   

        #region constructor
        /// <summary>
        /// Initializes a new instance of the GetStaffMachineStatus class
        /// </summary>
        /// 
        public GetStaffMachineStatus()
        {
            m_id = 18195;
            MachineList = new List<Machine>();
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Gets the Machine's staff Logged in status
        /// </summary>
        /// <returns></returns>
        public static List<Machine> MachineStatusList()
        {
            var msg=new GetStaffMachineStatus();
            try
            {
                msg.Send();
            }
            catch(ServerCommException ex)
            {
                throw new Exception("Get Machine Status: "+ex.Message);
            }
            return msg.MachineList;
        }

        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        /// <param name="requestWriter">The binary stream writer that should
        /// be used to write any request data necessary.</param>
        protected override void PackRequest()
        {            
        }

        /// <summary>
        /// Parses the response received from the server.
        /// </summary>
        /// <param name="responseReader">The binary stream reader that should
        /// be used to read any response data necessary.</param>
        protected override void UnpackResponse()
        {
            base.UnpackResponse();
            MemoryStream responseStream = new MemoryStream(m_responsePayload);
            BinaryReader responseReader = new BinaryReader(responseStream,Encoding.Unicode);

            if (responseStream.Length < MinResponseMessageLength)
            {
                throw new MessageWrongSizeException("Get Machine Status");
            }

            try
            {
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);

                UInt16 MachineCount = responseReader.ReadUInt16();
                MachineList.Clear();

                for (ushort x = 0; x < MachineCount; x++)
                {
                    Machine machine = new Machine();
                    Staff staff = new Staff();
                    Operator operatordata = new Operator();

                    //MachineID
                    machine.MachineID = responseReader.ReadInt32();

                    //MachineClientID
                    machine.MachineClientID = ReadString(responseReader);

                    //Machine Description
                    machine.MachineDescription = ReadString(responseReader);

                    //Machine LoginDate
                    machine.MachineLoginDate = ReadDateTime(responseReader) ?? DateTime.MinValue;

                    //StaffID
                    staff.Id = responseReader.ReadInt32();

                    //Staff FirstName
                    staff.FirstName = ReadString(responseReader);

                    //Staff LastName
                    staff.LastName = ReadString(responseReader);

                    //Operator ID
                    operatordata.Id = responseReader.ReadInt32();

                    //Operator Name
                    operatordata.Name = ReadString(responseReader);

                    machine.staffdata = staff;
                    machine.operatorData = operatordata;
                    MachineList.Add(machine);

                }
            }
            catch (EndOfStreamException e)
            {
                throw new MessageWrongSizeException("Get Machine status", e);
            }
            catch (Exception e)
            {
                throw new ServerException("Get Machine status", e);
            }
            finally
            {
                if (responseReader != null)
                {
                    responseReader.Close();
                }
            }
        
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets or sets the MachineList
        /// </summary>
        public List<Machine> MachineList 
        {
            get; 
            set;
        }
        #endregion
    }

    /// <summary>
    /// Represents the Machine struct
    /// </summary>
    public struct Machine
    {       
        public string MachineClientID;
        public int MachineID;
        public string MachineDescription;
        public DateTime MachineLoginDate;
        public Staff staffdata;
        public Operator operatorData;
    }
}
//End Rally TA10562
