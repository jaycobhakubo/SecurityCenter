using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter.Data
{
    internal class GetNextAvailableStaffLoginNumber : ServerMessage
    {
        private int mNextStaffLoginNumber;
        internal GetNextAvailableStaffLoginNumber()
        {
            m_id = 18105;            
        }
        #region Override Base
        protected override void PackRequest()
        {          

        }

        protected override void UnpackResponse()
        {
            base.UnpackResponse();

            // Create the streams we will be reading from.
            MemoryStream responseStream = new MemoryStream(m_responsePayload);
            BinaryReader responseReader = new BinaryReader(responseStream, Encoding.Unicode);

            // Try to unpack the data.           
            // Seek past return code, it has been handled by base class
            responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);
            //data part
            mNextStaffLoginNumber = responseReader.ReadInt32();
                       
            // Close the streams.
            responseReader.Close();
        }
        #endregion

        internal int NextAavaiableStaffLoginNumber
        {
            get { return mNextStaffLoginNumber; }
        }
    }
}
