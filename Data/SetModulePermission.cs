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
    class SetModulePermission : ServerMessage 
    {
        private const int MinResponseMessageLength = 4; //return code 4 bytes
        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Register Receipt Id
            //requestWriter.Write(mOperatorID);

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
            //todo: data part

            // Close the streams.
            responseReader.Close();
        }
        #endregion
    
        //constructors
    
    }
}
