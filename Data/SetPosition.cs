// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    class SetPosition : ServerMessage
    {
        
        int mPositionID;
        string mPositionName;
        private bool mPositionActivity;
        private const int MinResponseMessageLength = 8; //return code 4 bytes
        public SetPosition(int positionID, string positionName, bool positionActivity)
        {
            m_id = Constants.SET_POSITION_DATA;
            mPositionID = positionID;
            mPositionName = positionName;
            mPositionActivity = positionActivity;
        }
        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            requestWriter.Write(Configuration.operatorID);
            requestWriter.Write(mPositionID);
            requestWriter.Write((Int16)(mPositionName.Length));
            requestWriter.Write(mPositionName.ToCharArray ());
            requestWriter.Write(mPositionActivity);
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

            // Seek past return code, it has been handled by base class
            responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);
            //data part
            int returnedId = responseReader.ReadInt32();
            if (mPositionID == 0 && returnedId < 1)
            { 
             //failed to create a new position
                throw new ServerCommException("Failed to create a new position");
            }
            else if (mPositionID > 0 && mPositionID != returnedId)
            {
                //failed to update an existing position
                throw new ServerCommException("Failed to update an existing position");
            }
            else //, cool
            {
                mPositionID = returnedId;
            }
            // Close the streams.
            responseReader.Close();
        }
        #endregion
        public int PositionID
        {
            get { return mPositionID; }
        }
    }
}
