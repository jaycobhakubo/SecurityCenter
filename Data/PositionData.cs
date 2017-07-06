using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace GTI.Modules.SecurityCenter.Data
{
    internal class PositionData
    {
        #region Constants
        public const string POSITION_TABLE_NAME = "Position";
        public const string POSITION_COLUMN_POSITIONID = "PositionID";
        public const string POSITION_COLUMN_POSITIONNAME = "PositionName";
        public const string POSITION_COLUMN_OPERATORID = "OperatorID";
        public const string POSITION_COLUMN_ACTIVITYFLAG = "Activity";
        #endregion

#region Variables
        private DataTable mPosition;
        private Dictionary <int,ModulesData> mModulesData;//positionID, modules of position
#endregion

        public PositionData()
        {
            CreateTable();
            mModulesData = new Dictionary<int, ModulesData>();
        }
        public Dictionary<int, ModulesData> PermitedModules
        {
            get { return mModulesData; }
        }
        public void CreateTable()
        {
            if (mPosition == null)
            {
                mPosition = new DataTable(POSITION_TABLE_NAME);
            }
            else return;

            //set the column
            DataColumn positionIDColumn = new DataColumn(POSITION_COLUMN_POSITIONID);
            positionIDColumn.DataType = Type.GetType("System.Int32");
            mPosition.Columns.Add(positionIDColumn);
            //DataColumn[] key = new DataColumn[1];
            //key[0] = positionIDColumn;
            //mPosition.PrimaryKey = key;

            DataColumn positionNameColumn = new DataColumn(POSITION_COLUMN_POSITIONNAME);
            positionNameColumn.DataType = Type.GetType("System.String");
            mPosition.Columns.Add(positionNameColumn);

            DataColumn statusColumn = new DataColumn(Constants.Status);
            statusColumn.DataType = Type.GetType("System.String");
            mPosition.Columns.Add(statusColumn);

            DataColumn activityColumn = new DataColumn(POSITION_COLUMN_ACTIVITYFLAG);
            activityColumn.DataType = Type.GetType("System.Boolean");
            mPosition.Columns.Add(activityColumn);

        }
        public DataTable PositionTable
        {
            get { return mPosition; }
        }

        public bool GetPositionActivityByID(int id)
        {
            if (mPosition == null) return false;
            foreach (DataRow row in mPosition.Rows)
            {
                if (row[POSITION_COLUMN_POSITIONID] != null
                    && id == int.Parse(row[POSITION_COLUMN_POSITIONID].ToString()))
                {
                    return (bool)row[POSITION_COLUMN_ACTIVITYFLAG];
                }
            }
            return false;
        }

        public DataRow GetPositionRowByID(int id)
        {
            if (mPosition == null) return null;
            foreach (DataRow row in mPosition.Rows)
            {
                if (row[POSITION_COLUMN_POSITIONID] != null
                    && id == int.Parse(row[POSITION_COLUMN_POSITIONID].ToString()))
                {
                    return row;
                }
            }
            return null;    
        }
        public string GetPositionNameByID(int id)
        {
            if (mPosition == null) return null;
            foreach (DataRow row in mPosition.Rows )
            {
                if (row[POSITION_COLUMN_POSITIONID] != null
                    && id == int.Parse(row[POSITION_COLUMN_POSITIONID].ToString()))
                {
                    return row[POSITION_COLUMN_POSITIONNAME].ToString();
                }
            }
            return null;        
        }
    }
}
