using System;
using System.Collections.Generic;
using System.Text;

namespace GTI.Modules.SecurityCenter.Data
{
    internal class Constants
    {
        internal const int GET_STAFF_LIST = 18023; //Get the staff list
        internal const int SET_STAFF = 18048;

        internal const int GET_POSITION_DATA = 18046;
        internal const int SET_POSITION_DATA = 18060;

        internal const int GET_STAFF_POSITION = 18059;
        internal const int SET_STAFF_POSITION = 18061;

        internal const int GET_MODULE_DATA = 25008;
        //there is no set

        internal const int GET_MODULE_FEATUREs = 25011;
        //there is no pair set

        internal const int SET_STAFF_PASSWORD = 18193;

        internal const int GET_POSITION_MOUDLES = 18063;
        internal const int SET_POSITION_MODULES = 18064;


        internal const int GET_POSITION_MOUDLEFEATURES = 18065;
        internal const int SET_POSITION_MODULEFEATURES = 18066;
        internal const int SET_STAFF_ACCOUNTSTATUS = 18048;

        internal const int DuplicateDataError=1;  //a error used in SetStaff if there is duplicate login number
        internal const int GETALL_INSERT = 0; // a common value for sql get all or insert new
       
        internal const string Status = "Status";
        internal const string Status_New = "New";
        internal const string Status_Modified = "Modified";
        internal const string Status_Deleted = "Deleted";
        internal const string Status_OLD = "Old";     
    }
}
