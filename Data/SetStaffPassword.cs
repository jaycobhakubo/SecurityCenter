using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    class SetStaffPassword : ServerMessage
    {
        private int mStaffLoginNumber;
        private string mPassword;

        #region Override Base
        public SetStaffPassword(int staffLoginNumber, string password)
        {
            m_id = Constants.SET_STAFF_PASSWORD;
            mStaffLoginNumber = staffLoginNumber;
            mPassword = password;        
        }

        protected override void PackRequest()
        {
            //// Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // DE1765 - Unable to log into system with a newly created user.
            // Staff Login to change.
            requestWriter.Write(mStaffLoginNumber);

            // Staff Login making the change.
            requestWriter.Write(Configuration.StaffLoginNumber);

            // Password
            requestWriter.Write(SecurityHelper.HashPassword(mPassword));
            
            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();
        }
       
        #endregion
    }
}
