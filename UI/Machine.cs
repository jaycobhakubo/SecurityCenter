#region Copyright
// This is an unpublished work protected under the copyright laws of the United
// States and other countries.  All rights reserved.  Should publication occur
// the following will apply:  ©2010 GameTech International, Inc.
#endregion

//Start Rally TA10562
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GTI.Modules.SecurityCenter.Data;
using GTI.Modules.SecurityCenter.Properties;
using GTI.Modules.Shared;

namespace GTI.Modules.SecurityCenter
{
    /// <summary>
    /// Represents the MachineForm 
    /// </summary>
    public partial class MachineForm : GradientForm
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the MachineForm class
        /// </summary>
        public MachineForm()
        {
            InitializeComponent();
            PopulateMachineList();
            EnableButtons();
        }
        #endregion

        #region Member Methods
        /// <summary>
        /// Gets the Machine's staff status
        /// </summary>
        private void PopulateMachineList()
        {
            try
            {
                PMachineList = GetStaffMachineStatus.MachineStatusList();
            }
            catch(Exception ex)
            {
                MessageForm.Show(this,ex.Message,Resources.Machine);
            }
        }     

        /// <summary>
        /// Refresh the MachineListview
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">the event args</param>
        private void MachineRefreshButton_Click(object sender, EventArgs e)
        {
            PopulateMachineList();
            MachineListView.Refresh();        
        }

        /// <summary>
        /// Forces the staff to Logout of the Machine
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">the event args</param>
        private void MachineLogoutButton_Click(object sender, EventArgs e)
        {         
            if (MachineListView.SelectedIndices.Count > 0)
            {
                var selectedIndex = MachineListView.SelectedIndices[0];
                var topItem = MachineListView.TopItem;
                var lastItem = MachineListView.Items;

                if (MessageForm.Show(Resources.ConfirmLogOut, Resources.ConfirmLogoutTitle, MessageFormTypes.YesNo, 0) == DialogResult.Yes)
                {
                    var MachineItem = (Machine)MachineListView.SelectedItems[0].Tag;
                    MachineListView.SelectedItems[0].Remove();
                    try
                    {
                        SetStaffMachineLogout.Save(MachineItem);
                    }
                    catch(Exception ex)
                    {
                        MessageForm.Show(this,ex.Message,Resources.ConfirmLogoutTitle);                           
                    }
                    if (lastItem.Count > 0 )
                    {
                        if (selectedIndex > 0)
                        {
                            selectedIndex = selectedIndex - 1;                            
                        }
                        MachineListView.EnsureVisible(selectedIndex);
                        MachineListView.SelectedIndices.Add(selectedIndex);                     
                    }
                }          
            }            
        }

        /// <summary>
        /// Occurs when the selected index is changed on the MachineListview
        /// </summary>
        /// <param name="sender">The Sender</param>
        /// <param name="e">the event args</param>
        private void MachineListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnableButtons();
        }

        /// <summary>
        /// Determines which buttons to be enabled
        /// </summary>
        private void EnableButtons()
        {
            contextMenuLogout.Visible = MachineListView.SelectedIndices.Count > 0;
            
        }
        #endregion

        #region Member Properties
        /// <summary>
        /// Populate's the forms MachineList
        /// </summary>
        public List<Machine> PMachineList
        {
            set
            {
                //Clears the MachineList
                MachineListView.Items.Clear();

                //Populates the Machine List
                foreach (Machine MachineItem in value)
                {
                    ListViewItem lvi = MachineListView.Items.Add(MachineItem.MachineID.ToString());
                    lvi.SubItems.Add(MachineItem.MachineClientID);
                    lvi.SubItems.Add(MachineItem.MachineLoginDate.ToString());
                    lvi.SubItems.Add(MachineItem.staffdata.LastName + ", " + MachineItem.staffdata.FirstName);
                    lvi.SubItems.Add(MachineItem.operatorData.Name);
                    lvi.Tag = MachineItem;
                }
            }
        }
        #endregion
    }
}
//End Rally TA10562

