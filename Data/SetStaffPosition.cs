using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    class SetStaffPosition: ServerMessage 
    {
        private int mStaffID;
        private int[] mPositionIDs;
        private int MinResponseMessageLength = 4;
        public SetStaffPosition(int staffID, int[] positionIDs)
        {
            m_id = Constants.SET_STAFF_POSITION;
            mStaffID = staffID;
            mPositionIDs = new int[positionIDs.Length];
            positionIDs.CopyTo(mPositionIDs, 0);
        }
        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            requestWriter.Write(Configuration.operatorID);
            requestWriter.Write(mStaffID);
            ushort count = (ushort) mPositionIDs.Length;
            requestWriter.Write (count);
            for (int i = 0; i < count; i++)
            {
                requestWriter.Write(mPositionIDs[i]);
            }
            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();

        }

        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            // Create the streams we will be reading from.
            MemoryStream responseStream = new MemoryStream(m_responsePayload);
            BinaryReader responseReader = new BinaryReader(responseStream, Encoding.Unicode);

            // Check the response length.
            if (responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("GetModuleList.UnpackResponse");
            // Try to unpack the data.
            // Close the streams.
            responseReader.Close();
        }
        #endregion
    }
}
