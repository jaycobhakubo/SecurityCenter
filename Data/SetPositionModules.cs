using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    class SetPositionModules : ServerMessage 
    {
        private int mPositionID=-1;
        private int[] mModuleIDs;

        public SetPositionModules(int positionID, int[] moduleIDs)
        {
            m_id = Constants.SET_POSITION_MODULES;
            mPositionID = positionID;
            mModuleIDs = new int[moduleIDs.Length];
            moduleIDs.CopyTo(mModuleIDs, 0);
        }
        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            requestWriter.Write(mPositionID);
            ushort moduleCount =(ushort) mModuleIDs.Length;
            requestWriter.Write(moduleCount);
            for (int iModule = 0; iModule < moduleCount; iModule++)
            {
                requestWriter.Write(mModuleIDs[iModule]);
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
