using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    class GetModuleFeatures : ServerMessage 
    {
        private int mModuleID = Constants.GETALL_INSERT;
        private int MinResponseMessageLength = 6;
        private ModuleFeaturesData mModuleFeatureData;
        public GetModuleFeatures() : this (Constants.GETALL_INSERT)
        { }
        public GetModuleFeatures(int moduleID)
        {
            m_id = Constants.GET_MODULE_FEATUREs;
            mModuleID = moduleID;
            mModuleFeatureData = new ModuleFeaturesData();
        }
        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);
            requestWriter.Write(mModuleID);
            m_requestPayload = requestStream.ToArray();            // Set the bytes to be sent.
            requestWriter.Close();          // Close the streams.
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
            UInt16 featureCount = responseReader.ReadUInt16();
            Int16 tempLen = 0;
            DataRow featureRow;
            for (int iFeature = 0; iFeature < featureCount; iFeature++)
            {
                featureRow = mModuleFeatureData.ModuleFeatureTable.NewRow();
                featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATUREID] = responseReader.ReadInt32().ToString();
                featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEID] = responseReader.ReadInt32().ToString();
                featureRow[Constants.Status] = Constants.Status_OLD;
                tempLen = responseReader.ReadInt16();
                if (tempLen > 0)
                {
                    featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESNAME] = new string(responseReader.ReadChars(tempLen));
                }

                tempLen = responseReader.ReadInt16();
                if (tempLen > 0)
                {
                    featureRow[ModuleFeaturesData.MODULESFEATURES_COLUMN_MODULEFEATURESDESCRIPTION] = new string(responseReader.ReadChars(tempLen));
                }

                mModuleFeatureData.ModuleFeatureTable.Rows.Add(featureRow);

            }
            // Close the streams.
            responseReader.Close();
        }
        #endregion

        public ModuleFeaturesData ModuleFeatures
        {
            get { return mModuleFeatureData; }
        }

    }
}
