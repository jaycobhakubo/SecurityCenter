using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace GTI.Modules.SecurityCenter.Data
{
    class ModulesData
    {
        #region Constants
        public const string MODULES_TABLE_NAME = "Modules";
        public const string MODULES_COLUMN_MODULEID = "ModuleID";
        public const string MODULES_COLUMN_MODULENAME = "ModuleName";
        public const string MODULES_COLUMN_MODULEDESCRIPTION = "ModuleDescription";
        #endregion
        #region Variables
        private DataTable mModules;
        private Dictionary <int,ModuleFeaturesData>  mFeatures; //by module ID
        #endregion
        #region Constructors
            public ModulesData()
            {
                CreateTable();
                mFeatures = new Dictionary<int, ModuleFeaturesData>();
            }
        #endregion

        public Dictionary <int,ModuleFeaturesData> Features
        {
            get { return mFeatures; }
            set { mFeatures = value; }
        }
        public ModuleFeaturesData FindFeaturesByModuleID(int moduleID)
        {
            ModuleFeaturesData tempFeatures;
            mFeatures.TryGetValue(moduleID, out tempFeatures);
            return tempFeatures;
        }
        public DataRow FindModuleByModuleID(int moduleID)
        {
            foreach (DataRow row in mModules.Rows)
            {
                if (moduleID == int.Parse(row[MODULES_COLUMN_MODULEID].ToString ()))
                {
                    return row;
                }
            }
            return null;
        }
        public bool IsModuleInList(int moduleID)
        {
            foreach (DataRow row in mModules.Rows)
            {
                if (moduleID == int.Parse(row[MODULES_COLUMN_MODULEID].ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        public DataTable ModuleTable
        {
            get { return mModules; }
            set { mModules = value; }
        }
        private void CreateTable()
        {
            if (mModules == null)
            {
                mModules = new DataTable(MODULES_TABLE_NAME);
            }
            else return;

            //set the column
            DataColumn moduleIDColumn = new DataColumn(MODULES_COLUMN_MODULEID);
            moduleIDColumn.DataType = Type.GetType("System.Int32");
            mModules.Columns.Add(moduleIDColumn);
            //DataColumn[] key = new DataColumn[1];
            //key[0] = moduleIDColumn;
            //mModules.PrimaryKey = key;

            DataColumn moduleName = new DataColumn(MODULES_COLUMN_MODULENAME );
            moduleName.DataType = Type.GetType("System.String");
            mModules.Columns.Add(moduleName);

            DataColumn moduleDescription = new DataColumn(MODULES_COLUMN_MODULEDESCRIPTION);
            moduleDescription.DataType = Type.GetType("System.String");
            mModules.Columns.Add(moduleDescription);

            DataColumn statusColumn = new DataColumn(Constants.Status);
            statusColumn.DataType = Type.GetType("System.String");
            mModules.Columns.Add(statusColumn);
        }
    }
}
