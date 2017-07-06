using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    class GetPositionList : ServerMessage
    {
        private PositionData mPositions;
        private int mPositionID;
        private const int MinResponseMessageLength = 6; //return code 4 bytes + POSTIONcount 2 bytes

        public GetPositionList(int postioinID)
        {
            m_id = Constants.GET_POSITION_DATA;
            mPositionID = postioinID;
            mPositions = new PositionData();
        }
        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Register OperattorID
            requestWriter.Write(Configuration.operatorID);
            //postionID <0, get all, otherwise get the one specific position
            requestWriter.Write(mPositionID);
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
                throw new MessageWrongSizeException("GetPositionList.UnpackResponse");

            // Try to unpack the data.
            // Seek past return code, it has been handled by base class
            responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);
            //data part
            int positionCount = responseReader.ReadInt16();
            Int16 tempLen = 0;
            int positionId = 0;
            DataRow positionRow;
            for (int iPosition=0; iPosition < positionCount; iPosition++)
            {
                positionRow = mPositions.PositionTable.NewRow();
                positionId = responseReader.ReadInt32();
                positionRow[PositionData.POSITION_COLUMN_POSITIONID] = positionId.ToString();
                positionRow[Constants.Status] = Constants.Status_OLD;

                tempLen = responseReader.ReadInt16();
                if (tempLen > 0)
                {
                    positionRow[PositionData.POSITION_COLUMN_POSITIONNAME] = new string(responseReader.ReadChars(tempLen));
                }
                positionRow[PositionData.POSITION_COLUMN_ACTIVITYFLAG] = responseReader.ReadBoolean();
                mPositions.PositionTable.Rows.Add(positionRow);
                
            }
            // Close the streams.
            responseReader.Close();
        }
        #endregion

        public PositionData Positions
        {
            get { return mPositions; }
        }

    } 
}
