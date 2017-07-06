using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    class SetPositionModuleFeatures : ServerMessage
    {
        private int mPositionID;
        private int[] mModuleFeatureIDs;
       
        public SetPositionModuleFeatures(int positionID, int[] moduleFeatureIDs)
        {
            m_id = Constants.SET_POSITION_MODULEFEATURES;
            mPositionID = positionID;
            mModuleFeatureIDs = new int[moduleFeatureIDs.Length];
            moduleFeatureIDs.CopyTo(mModuleFeatureIDs, 0);        
        }
        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            requestWriter.Write(mPositionID);
            UInt16 counter =( UInt16) mModuleFeatureIDs.Length;
            requestWriter.Write(counter);
            for (int iFeature = 0; iFeature < counter; iFeature++)
            { 
                requestWriter.Write (mModuleFeatureIDs[iFeature]);
            }
            // Set the bytes to be sent.
            m_requestPayload = requestStream.ToArray();

            // Close the streams.
            requestWriter.Close();

        }

        protected override void UnpackResponse()
        {
            base.UnpackResponse();
        }
        #endregion
    }
}
