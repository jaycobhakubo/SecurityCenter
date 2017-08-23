// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2007 GameTech
// International, Inc.

namespace GTI.Modules.SecurityCenter.Data
{
    using System;
    using System.Text;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using GTI.Modules.Shared;
    using System.Data;

    internal struct Address
    {
        public int StaffID; //one staffid, one addressid;
        public int AddressID;
        public string Address1;
        public string Address2;
        public string City;
        public string State;
        public string Zip;
        public string Country;    
    }

    internal class GetStaffList : ServerMessage
    {
        #region Member Variables
        private int mOperatorID = Constants.GETALL_INSERT;
        private int mStaffID = Constants.GETALL_INSERT;
        private StaffData mStaffData;
        private Dictionary<int, PositionData> mPositionsByStaffID; 
        private ArrayList  mAddressList;
        private const int MinResponseMessageLength = 4; //return code 4 bytes
        private bool mLoadPosition = false;
        #endregion
        #region Constructors

        public GetStaffList(int operatorID, bool loadPosition)
            : this(operatorID, Constants.GETALL_INSERT, loadPosition)
        {
        }

        public GetStaffList(int operatorID, int staffID, bool loadPosition)
        {
            m_id = Constants.GET_STAFF_LIST;
            mOperatorID = operatorID;
            mStaffID = staffID;
            mStaffData = new StaffData();
            mPositionsByStaffID = new Dictionary<int,PositionData>(); //staffid
            mAddressList = new ArrayList();
            mLoadPosition = loadPosition;
        }
        #endregion
        #region Public Properties
        #endregion

        #region Override Base
        protected override void PackRequest()
        {
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);
                        
            //staffID <0 for all, bigger than 0, the specific staff only
            requestWriter.Write(mStaffID);
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
            if(responseStream.Length < MinResponseMessageLength)
                throw new MessageWrongSizeException("GetStaffList.UnpackResponse");

            // Try to unpack the data.           
            // Seek past return code, it has been handled by base class
                responseReader.BaseStream.Seek(sizeof(int), SeekOrigin.Begin);
            //data part
                UInt16  staffCount = responseReader.ReadUInt16();
                DataRow staffRow;
                int tempLen = 0;
                int staffID;
                Address tempAddress;
                for (int iStaff = 0; iStaff < staffCount; iStaff++)
                {
                    staffRow = mStaffData.StaffTable.NewRow();
                    staffID = responseReader.ReadInt32();
                    staffRow[StaffData.STAFF_TALBE_COLUMN_STAFFID] = staffID.ToString();
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                        staffRow[StaffData.STAFF_TALBE_COLUMN_LASTNAME] = new string(responseReader.ReadChars(tempLen));

                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                        staffRow[StaffData.STAFF_TALBE_COLUMN_FIRSTNAME] = new string(responseReader.ReadChars(tempLen));

                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    {
                        staffRow[StaffData.STAFF_TALBE_COLUMN_BIRTHDATE] = new string(responseReader.ReadChars(tempLen));
                    }

                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    {
                        staffRow[StaffData.STAFF_TALBE_COLUMN_HIREDATE] = new string(responseReader.ReadChars(tempLen));
                    }
                
                    staffRow[StaffData.STAFF_TALBE_COLUMN_LOGINNUMBER] = responseReader.ReadInt32().ToString();
                    
                    tempLen = responseReader.ReadByte();
                    staffRow[StaffData.STAFF_TALBE_COLUMN_ISACTIVE] = (tempLen == 0) ? "0" : "1";                    
                    //staffRow[StaffData.STAFF_TALBE_COLUMN_ISACTIVE] = tempLen.ToString();
                    
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    staffRow[StaffData.STAFF_TALBE_COLUMN_HOMEPHONE] = new string(responseReader.ReadChars(tempLen));
                    
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    staffRow[StaffData.STAFF_TALBE_COLUMN_OTHERPHONE] = new string(responseReader.ReadChars(tempLen));
                    
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                        staffRow[StaffData.STAFF_TALBE_COLUMN_SSN] = new string (responseReader.ReadChars (tempLen));
                    
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    staffRow[StaffData.STAFF_TALBE_COLUMN_MAGSTRIPNUMBER] = new string(responseReader.ReadChars(tempLen));
                    
                    tempLen = responseReader.ReadInt32();
                    staffRow[StaffData.STAFF_TALBE_COLUMN_ADDRESSID] = tempLen.ToString();
                    
                    tempAddress = new Address ();
                    tempAddress.StaffID = staffID;
                    tempAddress.AddressID = tempLen;
                   
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    tempAddress.Address1 = new string(responseReader.ReadChars(tempLen));
                   
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    tempAddress.Address2 = new string(responseReader.ReadChars(tempLen));
                    
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    tempAddress.City = new string(responseReader.ReadChars(tempLen));
                    
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    tempAddress.State = new string(responseReader.ReadChars(tempLen));
                    
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    tempAddress.Zip = new string (responseReader.ReadChars (tempLen));
                    
                    tempLen = responseReader.ReadInt16();
                    if (tempLen > 0)
                    tempAddress.Country = new string (responseReader.ReadChars (tempLen));

                    tempLen = responseReader.ReadByte();
                    staffRow[StaffData.STAFF_TALBE_COLUMN_HAND] = (tempLen == 0) ? "0" : "1";

                    tempLen = responseReader.ReadByte();
                        staffRow[StaffData.STAFF_TABLE_COLUMN_ACCTLOCKED] = (tempLen==0)?"0":"1";
                        //staffRow[StaffData.STAFF_TABLE_COLUMN_ACCTLOCKED] = "1";
                        
                    mAddressList.Add (tempAddress);


                    //staffRow[StaffData.STAFF_TALBE_COLUMN_PASSWORDID] = responseReader.ReadInt32().ToString();
                    //tempLen = responseReader.ReadInt16();
                    //staffRow[StaffData.STAFF_TALBE_COLUMN_PASSWORD] = new string(responseReader.ReadChars(tempLen));                                   
                    mStaffData.StaffTable.Rows.Add(staffRow);

                    //get positions for this staff
                    if (mLoadPosition == true)
                        GetPositionsOfStaff(staffID);
                   
                }
            // Close the streams.
                responseReader.Close();
        }
        #endregion
        private void GetPositionsOfStaff(int staffID)
        {
            GetStaffPositions staffPostions = new GetStaffPositions(staffID);
            staffPostions.Send();
            if (staffPostions.StaffPositioinData != null)
            {
                mPositionsByStaffID.Add(staffID, staffPostions.StaffPositioinData);
            }
        }
        #region Properties
        public StaffData Staff

        {
            get { return mStaffData; }
            set { mStaffData = value; }
        }

        //private void BuildStaffPositionList()
        //{//todo: we will use one messge to load all staff position, rather than one by one
        
        //}

        public PositionData PositionDatasByStaffID(int staffID)
        {
            PositionData positions;
            //if (mPositionsByStaffID.Count == 0)
            //{
            //    BuildStaffPositionList();
            //}
            mPositionsByStaffID.TryGetValue(staffID,out positions);
            //if we can not find it , load the postion on demand now only once
            if (positions == null)
            {
                GetPositionsOfStaff(staffID);
                mPositionsByStaffID.TryGetValue(staffID, out positions);
            }
            return positions;
        }
        
        public ArrayList AddressList
        {
            get { return mAddressList; }
            set { mAddressList = value; }
        }
        public string GetAddress1ByAddressID(int addressID)
        {
            if (mAddressList == null) return null;
            foreach (object o in mAddressList)
            {
                if (((Address)o).AddressID == addressID)
                {
                    return ((Address)o).Address1;
                }
            }
            return null;
        }
        public Address? GetAddressByAddressID(int addressID)
        {

            if (mAddressList == null) return null;
            foreach (object o in mAddressList)
            {
                if (((Address)o).AddressID == addressID)
                {
                    return ((Address)o);
                }
            }
            return null;
        
        }
        public Address? GetAddressByStaffID(int staffId)
        {

            if (mAddressList == null) return null;
            foreach (object o in mAddressList)
            {
                if (((Address)o).StaffID  == staffId)
                {
                    return ((Address)o);
                }
            }
            return null;

        }

        public int GetLoginNumberByStaffID(int staffID)
        {
            foreach (DataRow staff in mStaffData.StaffTable.Rows)
            {
                if (int.Parse(staff[StaffData.STAFF_TALBE_COLUMN_STAFFID].ToString()) == staffID)
                {
                    return int.Parse(staff[StaffData.STAFF_TALBE_COLUMN_LOGINNUMBER].ToString());
                }
            }
            return 0;
        }
#endregion

    }

}
