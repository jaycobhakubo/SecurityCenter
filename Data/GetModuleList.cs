// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    class GetModuleList : ServerMessage
    {
        private ModulesData mModuleData;
        private int mModuleID = Constants.GETALL_INSERT;
        private const int MinResponseMessageLength = 6; //return code 4 bytes

        public GetModuleList() : this(Constants.GETALL_INSERT) { }
        public GetModuleList(int moduleID)
        {
            this.m_id = Constants.GET_MODULE_DATA;
            mModuleID = moduleID;
            mModuleData = new ModulesData();
        }

        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            requestWriter.Write(mModuleID);

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
            int moduleCount = responseReader.ReadInt16();
            Int16 tempLen = 0;
            DataRow moduleRow;
            for (int iModule = 0; iModule < moduleCount; iModule++)
            {
                moduleRow = mModuleData.ModuleTable.NewRow();
                moduleRow [ModulesData.MODULES_COLUMN_MODULEID] = responseReader.ReadInt32().ToString ();
                moduleRow[Constants.Status] = Constants.Status_OLD;
                
                tempLen = responseReader.ReadInt16();
                if (tempLen > 0)
                {
                    moduleRow[ModulesData.MODULES_COLUMN_MODULENAME] = new string(responseReader.ReadChars(tempLen));
                }

                tempLen = responseReader.ReadInt16();
                if (tempLen > 0)
                {
                    moduleRow[ModulesData.MODULES_COLUMN_MODULEDESCRIPTION] = new string(responseReader.ReadChars(tempLen));
                }

                mModuleData.ModuleTable.Rows.Add(moduleRow);
            
            }
            // Close the streams.
            responseReader.Close();
        }
        #endregion
        public ModulesData Modules
        {
            get { return mModuleData; }
            set { mModuleData = value; }
        }
    }
}
