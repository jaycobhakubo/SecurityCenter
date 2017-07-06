#region Copyright
// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.
#endregion 

using System;
using System.IO;
using System.Text;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    /// <summary>
    /// Represents the setstaff message.
    /// </summary>
    class SetStaff : ServerMessage
    {
        #region Member Variables
        private Staff mstaff;
        private bool ModifyAcctlockstatus;
        private int mStaffID;
        private int mAddressID;
        private int mOperatorID;
        private int MinResponseMessageLength = 12;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the setstaffmessage class.
        /// </summary>
        /// <param name="staffID">the staffID</param>
        /// <param name="addressID">the addressID</param>
        /// <param name="staff">The staff info</param>
        /// <param name="ModifyAcctlock">True when we are chanding the status if lock else false</param>
        public SetStaff(ref int staffID, ref int addressID, Staff staff,bool ModifyAcctlock)
        {
            mstaff = staff;
            ModifyAcctlockstatus = ModifyAcctlock;
            m_id = Constants.SET_STAFF;
            mOperatorID = (Int16 ) Configuration.operatorID;
            mStaffID = staffID;
            mAddressID = addressID;
        }
        #endregion

        #region Override Base
        /// <summary>
        /// Prepares the request to be sent to the server.
        /// </summary>
        protected override void PackRequest()
        {
            //// Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            //Staff ID
            requestWriter.Write(mStaffID);
            
            //LastName
            WriteString(requestWriter,mstaff.LastName);

            //FirstName
            WriteString(requestWriter,mstaff.FirstName);

            //Date of Birth
            if (mstaff.BirthDate != DateTime.MinValue)
            {   
                WriteDateTime(requestWriter, mstaff.BirthDate);
            }
            else
            {
                requestWriter.Write((ushort)0);
            }

            //Hire Date
            if (mstaff.HireDate != DateTime.MinValue)
            {   
                WriteDateTime(requestWriter, mstaff.HireDate);
            }
            else
            {
                requestWriter.Write((ushort)0);
            }

            //Login #
            requestWriter.Write(mstaff.LoginNumber);

            //IsActive
            requestWriter.Write((byte)(mstaff.IsActive ? 1 : 0));

            //Home Ph
            WriteString(requestWriter,mstaff.HomePhone);

            //Other Ph
            WriteString(requestWriter,mstaff.OtherPhone);

            //SSN
            WriteString(requestWriter,mstaff.GovIssuedIdNumber);

            //Magnetic Card #
            WriteString(requestWriter,mstaff.MagneticCardNumber);
          
            //Address ID
            requestWriter.Write(mAddressID);

            //Address1
            WriteString(requestWriter,mstaff.Address.Address1);

            //Address2
            WriteString(requestWriter,mstaff.Address.Address2);

            //City
            WriteString(requestWriter,mstaff.Address.City);

            //State
            WriteString(requestWriter,mstaff.Address.State);

            //Zipcode
            WriteString(requestWriter,mstaff.Address.Zipcode);
            
            //Country
            WriteString(requestWriter,mstaff.Address.Country);

            //Left Handed
            requestWriter.Write((Int32)(mstaff.LeftHanded? 1:0));

            //Is the Account status Modified
            requestWriter.Write((byte)(ModifyAcctlockstatus == true ? 1 : 0));

            //Account Locked
            requestWriter.Write((byte)(mstaff.AcctLocked ? 1 : 0));

            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();

        }

        /// <summary>
        /// Parses the response received from the server.
        /// </summary>
        protected override void UnpackResponse()
        {
            base.UnpackResponse();
           
            // Create the streams we will be reading from.
            MemoryStream responseStream = new MemoryStream(m_responsePayload);
            BinaryReader responseReader = new BinaryReader(responseStream, Encoding.Unicode);

            // Check the response length.
            if (responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("GetStaffList.UnpackResponse");

            // Try to unpack the data.

            // Seek past return code, it has been handled by base class
            int returnCode = responseReader.ReadInt32();
            if (returnCode != (int)GTIServerReturnCode.Success)
            {
                throw new ServerException((GTIServerReturnCode)returnCode, "Server Error Code: " + returnCode.ToString());

            }
           
            //data part: staffID, addressID
            // Staff Id
            int staffId = responseReader.ReadInt32();
            if ( (mStaffID == 0 && staffId < 1) || (mStaffID > 0 && mStaffID != staffId))
            {
                responseReader.Close();
                throw new ServerCommException("A staff ID was not set correctly");
            }
            else
            {
                mStaffID = staffId;
            }

            //AddressID
            int addressID = responseReader.ReadInt32();       
            mAddressID = addressID;          
            responseReader.Close();
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Gets the StaffID
        /// </summary>
        public int StaffID
        {
            get { return mStaffID; }
        }

        /// <summary>
        /// Gets the AddressID
        /// </summary>
        public int AddressID
        {
            get { return mAddressID; }
        }
        #endregion
    }
}
