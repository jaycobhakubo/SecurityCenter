using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace GTI.Modules.SecurityCenter.Data
{
    class ModuleFeaturesData
    {
        #region Constants
        public const string MODULESFEATURES_TABLE_NAME = "ModuleFeatures";
        public const string MODULESFEATURES_COLUMN_MODULEFEATUREID = "ModuleFeatureId";
        public const string MODULESFEATURES_COLUMN_MODULEID = "ModuleID";
        public const string MODULESFEATURES_COLUMN_MODULEFEATURESNAME = "ModuleFeatureName";
        public const string MODULESFEATURES_COLUMN_MODULEFEATURESDESCRIPTION = "ModuleFeatureDescription";
        #endregion
        #region Variables
        private DataTable mModuleFeatures;
        #endregion
        #region Constructors
            public ModuleFeaturesData()
            {
                CreateTable();
            }
        #endregion

        public DataRow FeatureRowByFeatureID(int featureID)
        {
            foreach (DataRow featureRow in mModuleFeatures.Rows)
            {
                if (featureID == int.Parse(featureRow[MODULESFEATURES_COLUMN_MODULEFEATUREID].ToString()))
                {
                    return featureRow;
                }
            }
            return null;
        }

        public bool IsFeatureInList(int featureID)
        {
            foreach (DataRow featureRow in mModuleFeatures.Rows)
            {
                if (featureID == int.Parse(featureRow[MODULESFEATURES_COLUMN_MODULEFEATUREID].ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        public DataTable ModuleFeatureTable
        {
            get { return mModuleFeatures; }
            set { mModuleFeatures = value; }
        }
        public DataView GetModuleFeatureDataViewByModuleID(int id)
        {
            DataView view = new DataView(mModuleFeatures);
            view.RowFilter = MODULESFEATURES_COLUMN_MODULEID + " = " + "'" + id.ToString() +"'";
            return view;
        }
        private void CreateTable()
        {
            if (mModuleFeatures == null)
            {
                mModuleFeatures = new DataTable(MODULESFEATURES_TABLE_NAME);
            }
            else return;

            //set the column
            DataColumn iDColumn = new DataColumn(MODULESFEATURES_COLUMN_MODULEFEATUREID);
            iDColumn.DataType = Type.GetType("System.Int32");
            mModuleFeatures.Columns.Add(iDColumn);
            //DataColumn[] key = new DataColumn[1];
            //key[0] = iDColumn;
            //mModuleFeatures.PrimaryKey = key;

            DataColumn moduleID = new DataColumn(MODULESFEATURES_COLUMN_MODULEID);
            moduleID.DataType = Type.GetType("System.String");
            mModuleFeatures.Columns.Add(moduleID);

            DataColumn name = new DataColumn(MODULESFEATURES_COLUMN_MODULEFEATURESNAME);
            name.DataType = Type.GetType("System.String");
            mModuleFeatures.Columns.Add(name);

            DataColumn description = new DataColumn(MODULESFEATURES_COLUMN_MODULEFEATURESDESCRIPTION);
            description.DataType = Type.GetType("System.String");
            mModuleFeatures.Columns.Add(description);

            DataColumn statusColumn = new DataColumn(Constants.Status);
            statusColumn.DataType = Type.GetType("System.String");
            mModuleFeatures.Columns.Add(statusColumn);
        }
    }
}
