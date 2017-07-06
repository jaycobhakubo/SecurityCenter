using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using GTI.Modules.Shared;
namespace GTI.Modules.SecurityCenter.Data
{
    class GetPositionModule : ServerMessage 
    {
        private int mPositionID=0;
        private ModulesData mPositionModules;
        private const int MinResponseMessageLength = 6; //return code 4 bytes
        public GetPositionModule(int positionID)
        {
            m_id = Constants.GET_POSITION_MOUDLES;
            mPositionID = positionID;
            mPositionModules = new ModulesData();
        }
        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Register Receipt Id
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
                throw new MessageWrongSizeException("GetModuleList.UnpackResponse");

            // Try to unpack the data.
            // Seek past return code, it has been handled by base class
            responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);
            //data part
            UInt16 tempLen = 0, moduleCount = (UInt16 )responseReader.ReadUInt16();
            DataRow moduleRow;
            for (int iModule = 0; iModule < moduleCount; iModule++)
            {
                moduleRow = mPositionModules.ModuleTable.NewRow();
                moduleRow[ModulesData.MODULES_COLUMN_MODULEID] = responseReader.ReadInt32().ToString ();
                moduleRow[Constants.Status] = Constants.Status_OLD;
                
                tempLen = responseReader.ReadUInt16();
                if (tempLen > 0)
                {
                    moduleRow[ModulesData.MODULES_COLUMN_MODULENAME] = new string (responseReader.ReadChars(tempLen));
                }
                tempLen = responseReader.ReadUInt16();
                if (tempLen > 0)
                {
                    moduleRow[ModulesData.MODULES_COLUMN_MODULEDESCRIPTION] = new string(responseReader.ReadChars(tempLen));
                }

                mPositionModules.ModuleTable.Rows.Add(moduleRow);
            
            }
            // Close the streams.
            responseReader.Close();
        }
        #endregion
        public ModulesData PositionModules
        {
            get { return mPositionModules; }
        }
    }
}
