using System;
using System.Collections.Generic;
using System.Text;
using GTI.Modules.Shared;
using System.IO;
using System.Data;

namespace GTI.Modules.SecurityCenter.Data
{
    internal class GetStaffPositions : ServerMessage 
    {
        #region Member Variables
        private int mOperatorID = Constants.GETALL_INSERT;
        private int mStaffID = Constants.GETALL_INSERT;
        private int mPositionID = Constants.GETALL_INSERT;
        private PositionData mPositionData;
        private const int MinResponseMessageLength = 6; //return code 4 bytes  + 2 bytes count
        #endregion

        #region Constructors

        public GetStaffPositions(int staffID) : this(staffID, Constants.GETALL_INSERT) { }
        public GetStaffPositions(int staffID, int positionID)
        {
            m_id = Constants.GET_STAFF_POSITION ;
            mOperatorID = Configuration.operatorID;
            mStaffID = staffID; 
            mPositionID = positionID;
            mPositionData = new PositionData ();
        }
        #endregion

        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            //staffID, the specific staff only
            requestWriter.Write(mStaffID);

            //positionID <0 for all, otherwise for 1
            requestWriter.Write(mPositionID);

            // Register operatorID Id
            requestWriter.Write(mOperatorID);
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
                throw new MessageWrongSizeException("GetStaffList.UnpackResponse");

            // Try to unpack the data.           
            // Seek past return code, it has been handled by base class
            responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);
            //data part
            int positionCount = responseReader.ReadInt16();
            DataRow positionRow;
            int tempLen = 0;
            for (int iPosition = 0; iPosition < positionCount; iPosition++)
            {
                positionRow = mPositionData.PositionTable.NewRow();
                tempLen = responseReader.ReadInt32();
                positionRow[PositionData.POSITION_COLUMN_POSITIONID] = tempLen.ToString();
                positionRow[Constants.Status] = Constants.Status_OLD;
                tempLen = responseReader.ReadInt16();
                if (tempLen > 0)
                {
                    positionRow[PositionData.POSITION_COLUMN_POSITIONNAME] = new string(responseReader.ReadChars(tempLen));
                }
                mPositionData.PositionTable.Rows.Add(positionRow);
            }
            //// Close the streams.
            responseReader.Close();
        }

        public PositionData StaffPositioinData
        {
            get { return mPositionData; }
        }
        public string PositionNameByID(int positionID)
        {
            foreach (DataRow row in mPositionData.PositionTable.Rows)
            {
                if (int.Parse(row[PositionData.POSITION_COLUMN_POSITIONID].ToString()) == positionID)
                {
                    return row[PositionData.POSITION_COLUMN_POSITIONNAME].ToString();
                }
            }
            return null;
        }
        #endregion
    }
}
