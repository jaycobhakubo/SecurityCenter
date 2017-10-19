using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using GTI.Modules.Shared;
using GTI.Modules.SecurityCenter.Data;
using System.Linq;

namespace GTI.Modules.SecurityCenter
{
    internal partial class AssignPositions : GradientForm
    {
        PositionData mAssignedPositions;
        PositionData mAvailablePositions;
        private bool mIsDirtyForm = false;
        public AssignPositions(ref PositionData assignedPositions)
        {
            InitializeComponent();
            mAssignedPositions = assignedPositions;
            CustomInitializeComponent();
        }

        private void CustomInitializeComponent()
        { 
            this.mCancelButton.Click +=new EventHandler(this.cancelButton_Click);
            this.mOKButton.Click += new EventHandler(this.OKButton_Click);
            this.assignButton.Click +=new EventHandler(assignButton_Click);
            this.removeButton.Click +=new EventHandler(removeButton_Click);            
        }

        private void AssignPositions_Load(object sender, EventArgs e)
        {
            GetPositionList positionList = new GetPositionList(-1);
            //load available positions to the available lists
            try
            {
                positionList.Send();
            }
            catch (Exception ex)
            {
                Utilities.Log(Properties.Resources.errorFailedToGetData + " " + ex.Message, LoggerLevel.Severe);
                MessageForm.Show(this, Properties.Resources.errorFailedToGetData, Properties.Resources.securityCenter);
                return;
            }
            mAvailablePositions = positionList.Positions;
            LoadPositionsToLists();

        }

        private void LoadPositionsToLists()
        {
             Utilities.LogInfoIN();;
           
            assignedPostionslistBox.Items.Clear();
            availablePositionListBox.Items.Clear();
            var AssignedPositionInOrder = mAssignedPositions.PositionTable.Rows.Cast<DataRow>().OrderBy(y => y[PositionData.POSITION_COLUMN_POSITIONNAME]);
            //there is no dub on two lists
            ListViewItem tmpItem;
            foreach (DataRow position in AssignedPositionInOrder)
            {
                tmpItem = new ListViewItem(position[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                tmpItem.Tag = position[PositionData.POSITION_COLUMN_POSITIONID].ToString();
                this.assignedPostionslistBox.Items.Add(tmpItem);
            }
            bool assigned = false;
            var AvailablePositionInOrder = mAvailablePositions.PositionTable.Rows.Cast<DataRow>().OrderBy(y => y[PositionData.POSITION_COLUMN_POSITIONNAME]);
            foreach (DataRow position in AvailablePositionInOrder)
            {
                assigned = false;
                foreach (DataRow position2 in AssignedPositionInOrder)
                {
                    if (position[PositionData.POSITION_COLUMN_POSITIONID].ToString().
                        Equals(position2[PositionData.POSITION_COLUMN_POSITIONID].ToString()))
                    {
                        assigned = true;
                        break;
                    }
                }
                if (assigned == true) continue;

                //FIX: RALLY DE1573 Only Show active postions START
                if ((bool)position[PositionData.POSITION_COLUMN_ACTIVITYFLAG])
                {
                    tmpItem = new ListViewItem(position[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                    tmpItem.Tag = position[PositionData.POSITION_COLUMN_POSITIONID].ToString();
                    availablePositionListBox.Items.Add(tmpItem);
                }
                //FIX: RALLY DE1573 Only Show active postions END
            }
             Utilities.LogInfoLeave();
           
        }
        private void OKButton_Click(object sender, EventArgs e)
        {
            //repack the assignedpositions
            DataRow tempRow;
            mAssignedPositions.PositionTable.Rows.Clear();
            foreach (ListViewItem item in assignedPostionslistBox.Items)
            {
                tempRow = mAssignedPositions.PositionTable.NewRow();
                tempRow[PositionData.POSITION_COLUMN_POSITIONID] = item.Tag;
                tempRow[PositionData.POSITION_COLUMN_POSITIONNAME] = item.Text;
                mAssignedPositions.PositionTable.Rows.Add(tempRow);            
            }
            this.DialogResult = DialogResult.OK;
            this.Close();

        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            if (mIsDirtyForm == false)
            {
                this.DialogResult = DialogResult.Cancel;   //RALLY DE9182
                this.Close();
                return;                                    //RALLY DE9181    
            }           
            DialogResult result = MessageForm.Show(Properties.Resources.warningCancel, Properties.Resources.securityCenter, MessageFormTypes.YesNo);
            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Cancel;   //RALLY DE9182
                this.Close();             
            }
        }

        private void assignButton_Click(object sender, EventArgs e)
        {
            int selectionItems = availablePositionListBox.SelectedItems.Count;
            ListViewItem tempItem;
            for (int i = 0; i < selectionItems; i++)
            {
                tempItem =(ListViewItem) availablePositionListBox.SelectedItems[0].Clone();
                assignedPostionslistBox.Items.Add(tempItem);
                availablePositionListBox.SelectedItems[0].Remove ();
                mIsDirtyForm = true;
            }
        }

        private void removeButton_Click(object sender, EventArgs e)
        {
 
            int selectionItems = assignedPostionslistBox.SelectedItems.Count;
            ListViewItem tempItem;
            for (int i = 0; i < selectionItems; i++)
            {
                tempItem = (ListViewItem)assignedPostionslistBox.SelectedItems[0].Clone();
                availablePositionListBox.Items.Add(tempItem);
                assignedPostionslistBox.SelectedItems[0].Remove();
                mIsDirtyForm = true; 
            }
        }

                
    }
}