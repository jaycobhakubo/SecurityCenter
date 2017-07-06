using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
namespace GTI.Modules.SecurityCenter.Data
{
    class StaffData
    {
        #region Table Constants
        internal const string STAFF_TABLE_NAME = "Staff";
        internal const string STAFF_TALBE_COLUMN_STAFFID = "StaffID";
        internal const string STAFF_TALBE_COLUMN_LASTNAME = "LastName";
        internal const string STAFF_TALBE_COLUMN_FIRSTNAME = "FirstName";
        internal const string STAFF_TALBE_COLUMN_DEPARTMENT = "Department";
        internal const string STAFF_TALBE_COLUMN_BIRTHDATE = "BirthDate";
        internal const string STAFF_TALBE_COLUMN_HIREDATE = "HireDate";
        internal const string STAFF_TALBE_COLUMN_LOGINNUMBER = "LoginNumber";
        internal const string STAFF_TALBE_COLUMN_HAND = "Hand";
        internal const string STAFF_TALBE_COLUMN_ISACTIVE = "IsActive";
        internal const string STAFF_TALBE_COLUMN_EXPIRE = "Expire";
        internal const string STAFF_TALBE_COLUMN_LOCKOUTSECONDS = "LockoutSeconds";
        internal const string STAFF_TALBE_COLUMN_LOGINFAILTIME = "LoginFailTime";
        internal const string STAFF_TALBE_COLUMN_HOMEPHONE = "HomePhone";
        internal const string STAFF_TALBE_COLUMN_OTHERPHONE = "OtherPhone";
        internal const string STAFF_TALBE_COLUMN_SSN = "SSN";
        internal const string STAFF_TALBE_COLUMN_MAGSTRIPNUMBER = "MagStripNumber";
        internal const string STAFF_TALBE_COLUMN_ADDRESSID = "AddressID";
       // internal const string STAFF_TALBE_COLUMN_OPERATORID = "OperatorID";
        //internal const string STAFF_TALBE_COLUMN_POSITIONID = "PositionID";
        internal const string STAFF_TALBE_COLUMN_PASSWORDID = "PasswordID";
        internal const string STAFF_TALBE_COLUMN_PASSWORD = "Password";
        internal const string STAFF_TABLE_COLUMN_ACCTLOCKED = "AcctLocked";
        internal const string STAFF_TABLE_COLUMN_FAILEDLOGINATTEMPTS = "FailedLoginAttempts";
        
        #endregion

        private DataTable staffTable;
        public StaffData()
        {
            CreateTable();
        }
        public void CreateTable()
        {
            if (staffTable == null)
            {
                staffTable = new DataTable(STAFF_TABLE_NAME);
            }
            else return;
            //set the column
            DataColumn staffIDColumn = new DataColumn(STAFF_TALBE_COLUMN_STAFFID);
            staffIDColumn.DataType = Type.GetType("System.String");            
            staffTable.Columns.Add(staffIDColumn);
            DataColumn[] key= new DataColumn [1];
            key[0] = staffIDColumn;
            staffTable.PrimaryKey =key ;

            DataColumn lastNameColumn = new DataColumn(STAFF_TALBE_COLUMN_LASTNAME);
            lastNameColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(lastNameColumn);

            DataColumn firstNameColumn = new DataColumn(STAFF_TALBE_COLUMN_FIRSTNAME);
            lastNameColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(firstNameColumn);

            DataColumn departmentColumn = new DataColumn(STAFF_TALBE_COLUMN_DEPARTMENT);
            lastNameColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(departmentColumn);

            DataColumn birthDateColumn = new DataColumn(STAFF_TALBE_COLUMN_BIRTHDATE);
            lastNameColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(birthDateColumn);

            DataColumn hireDateColumn = new DataColumn(STAFF_TALBE_COLUMN_HIREDATE);
            lastNameColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(hireDateColumn);

            DataColumn pinColumn = new DataColumn(STAFF_TALBE_COLUMN_LOGINNUMBER);
            pinColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(pinColumn);

            DataColumn handColumn = new DataColumn(STAFF_TALBE_COLUMN_HAND);
            handColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(handColumn);

            DataColumn isActiveColumn = new DataColumn(STAFF_TALBE_COLUMN_ISACTIVE);
            isActiveColumn.DataType = Type.GetType("System.String");             
            staffTable.Columns.Add(isActiveColumn);

            DataColumn expireColumn = new DataColumn(STAFF_TALBE_COLUMN_EXPIRE);
            expireColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(expireColumn);

            DataColumn lockoutSecondsColumn = new DataColumn(STAFF_TALBE_COLUMN_LOCKOUTSECONDS);
            lockoutSecondsColumn.DataType = Type.GetType("System.String");            
            staffTable.Columns.Add(lockoutSecondsColumn);

            DataColumn loginfFailTimeColumn = new DataColumn(STAFF_TALBE_COLUMN_LOGINFAILTIME);
            loginfFailTimeColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(loginfFailTimeColumn);

            DataColumn homePhoneColumn = new DataColumn(STAFF_TALBE_COLUMN_HOMEPHONE);
            homePhoneColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(homePhoneColumn);

            DataColumn otherPhoneColumn = new DataColumn(STAFF_TALBE_COLUMN_OTHERPHONE);
            otherPhoneColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(otherPhoneColumn);

            DataColumn SSNColumn = new DataColumn(STAFF_TALBE_COLUMN_SSN);
            SSNColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(SSNColumn);

            DataColumn magNUmberColumn = new DataColumn(STAFF_TALBE_COLUMN_MAGSTRIPNUMBER);
            magNUmberColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(magNUmberColumn);

            DataColumn addressIDColumn = new DataColumn(STAFF_TALBE_COLUMN_ADDRESSID);
            addressIDColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(addressIDColumn);

            //DataColumn operatorIDColumn = new DataColumn(STAFF_TALBE_COLUMN_OPERATORID);
            //operatorIDColumn.DataType = Type.GetType("System.String");
            //staffTable.Columns.Add(operatorIDColumn);

            //DataColumn positionIDColumn = new DataColumn(STAFF_TALBE_COLUMN_POSITIONID);
            //positionIDColumn.DataType = Type.GetType("System.String");
            //staffTable.Columns.Add(positionIDColumn);
            DataColumn passwordIDColumn = new DataColumn(STAFF_TALBE_COLUMN_PASSWORDID);
            passwordIDColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(passwordIDColumn);

            DataColumn passwordColumn = new DataColumn(STAFF_TALBE_COLUMN_PASSWORD);
            passwordColumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(passwordColumn);

            DataColumn acctlockedcolumn = new DataColumn(STAFF_TABLE_COLUMN_ACCTLOCKED);
            acctlockedcolumn.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(acctlockedcolumn);

            DataColumn FailedLoginAttempts = new DataColumn(STAFF_TABLE_COLUMN_FAILEDLOGINATTEMPTS);
            FailedLoginAttempts.DataType = Type.GetType("System.String");
            staffTable.Columns.Add(FailedLoginAttempts);

        }

        public DataTable StaffTable
        {
            get { return staffTable; }
        }
        public DataRow GetStaffRowByStaffID (int staffID)
        {            
                foreach (DataRow staff in StaffTable.Rows)
                {
                    if (!object.ReferenceEquals(staff[STAFF_TALBE_COLUMN_STAFFID], null))
                    {
                        if (int.Parse(staff[STAFF_TALBE_COLUMN_STAFFID].ToString()) == staffID)
                        {
                            return staff;
                        }
                    }
                }
                return null;
        }
    }
}
