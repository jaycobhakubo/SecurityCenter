#region Copyright
// This is an unpublished work protected under the copyright laws of the
// United States and other countries.  All rights reserved.  Should
// publication occur the following will apply:  © 2008 GameTech
// International, Inc.

//NewStaff.cs
//
#endregion

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Linq;
using GTI.Modules.Shared;
using GTI.Modules.SecurityCenter.Data;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace GTI.Modules.SecurityCenter
{

    public partial class NewStaff : GradientForm
    {
        #region Constants

        private const int NEW_ID = 0;//this is the staff ID for a new staff
        private const string FIND_UPPER = @"^.*(?=.*[A-Z]).*$";     // US4887 regex for finding upper case characters
        private const string FIND_LOWER = @"^.*(?=.*[a-z]).*$";     // US4887 regex for finding lower case characters
        private const string FIND_NUMBER = @"^.*(?=.*\d).*$";       // US4887 regex for finding numerical characters
        private const string FIND_SPECIAL = @"(?=.*[!@#$%^+=])";    // US4887 regex for finding special characters

        #endregion

        #region Private Members

        GTI.Modules.SecurityCenter.Data.Address? mSelectedStaffAddress;
        private int mCurrentSelectedListViewIndex = 0; 
        private int mStaffID = NEW_ID;//a new staff
        private int mAddressID = NEW_ID;           
        private bool isReloading; //DE10178      
        private bool ModifyingAccountlock;
        private bool mIsKioskStaff = false;
        private bool mIsDirtyForm = false;
        private PositionData mAssignedPositions;
        private DataTable mStaffTable;
        private DataRow mCurrentSelectedStaffRow;      
        private WaitForm mWaitingForm;
        private MagneticCardReader mMagCardReader; // PDTS 1064

        string mPosition = "";
        Int16 m_StaffStatus = 1; //Active:1 ; All: -1; 0:Inctive
        static bool mUnchanged = false;

        #endregion

        public NewStaff()
        {
            InitializeComponent();
            // Rally DE1569 - Fields on screen allow both #'s and letters and data does not save properly when this occurs.
            //passwordTextBox.KeyPress += new KeyPressEventHandler(passwordTextBox_KeyPress);
            //verifiedPasswordTextBox.KeyPress += new KeyPressEventHandler(passwordTextBox_KeyPress);
            mMagCardReader = new MagneticCardReader(Configuration.mMSRSettings); // PDTS 1064
        }

        #region Events

        private void NewStaff_Load(object sender, EventArgs e)
        {
            //load staff who is active
            Utilities.LogInfoIN();
            LoadPositionToComboBox();

            LoadDataToListView(1, "All");
            positionComboBox.SelectedIndex = 0;

            if (staffListView.Items.Count > 0)
            {
                staffListView.Reset();
                FocusStaffListView(0);

            }

            //RALLY DE 4806 Allow entry of new staff on loadup if there are no staff
            else
            {
                InitNewStaffInformation();
                mIsDirtyForm = true;
                mCurrentSelectedStaffRow = null;
                firstNameTextBox.Focus(); // DE2283 - Set cursor to First Name.
            }

            //activeRadioButton.Checked = true;
            Utilities.LogInfoLeave();
            mMagCardReader.BeginReading();

            // Rally DE1569
            SetMaxTextLengths();
        }

        // Rally DE1569
        private void SetMaxTextLengths()
        {
            firstNameTextBox.MaxLength = StringSizes.MaxNameLength;
            lastNameTextBox.MaxLength = StringSizes.MaxNameLength;
            adress1TextBox.MaxLength = StringSizes.MaxAddressLength;
            adress2TextBox.MaxLength = StringSizes.MaxAddressLength;
            cityTextBox.MaxLength = StringSizes.MaxCityStateZipCountryLength;
            stateTextBox.MaxLength = StringSizes.MaxCityStateZipCountryLength;
            zipTextBox.MaxLength = StringSizes.MaxCityStateZipCountryLength;
            countryTextBox.MaxLength = StringSizes.MaxCityStateZipCountryLength;
            homePhoneTextBox.MaxLength = StringSizes.MaxPhoneNumberLength;
            otherPhoneTextBox.MaxLength = StringSizes.MaxPhoneNumberLength;
            SSNTextBox.MaxLength = StringSizes.MaxGovIssuedIdNumLength;
            passwordTextBox.MaxLength = StringSizes.MaxPinNumLength;
            verifiedPasswordTextBox.MaxLength = StringSizes.MaxPinNumLength;
        }

        //public bool checkpasswordsettings;
        // Rally DE1569
        /// <summary>
        /// We validate and only allow white space and number being typed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="ke"></param>
        private void passwordTextBox_KeyPress(object sender, KeyPressEventArgs ke)
        {
            //allow back space and 0 to 9
            /*
            if ((ke.KeyCode >= Keys.D0 && ke.KeyCode <= Keys.D9)
                || (ke.KeyCode >= Keys.NumPad0 && ke.KeyCode <= Keys.NumPad9)
                || ke.KeyCode == Keys.Back)
            {
                return;
            }
            else
            {

                ke.SuppressKeyPress = true;
            }
            */
            //checkpasswordsettings = true;
            if (!char.IsControl(ke.KeyChar))
            {
                TextBox password = sender as TextBox;

                if (password != null)
                {
                    // Check to see if this will create an invalid integer.
                    int result;

                    ke.Handled = !int.TryParse(password.Text + ke.KeyChar, NumberStyles.None, CultureInfo.CurrentCulture, out result);
                }
            }
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            //only allow numbers, back space, space, or -+() to be entered into the text box
            bool boolReturn = false;
            int isNumber = 0;

            if (!char.IsControl(e.KeyChar))
            {
                switch (e.KeyChar)
                {
                    case (char)8:   //back space
                        boolReturn = false;
                        break;
                    case (char)32:  //space
                        boolReturn = false;
                        break;
                    case (char)40:  //(
                        boolReturn = false;
                        break;
                    case (char)41:  //)
                        boolReturn = false;
                        break;
                    case (char)43:  //+
                        boolReturn = false;
                        break;
                    case (char)45:  //-
                        boolReturn = false;
                        break;
                    default:
                        boolReturn = !int.TryParse(e.KeyChar.ToString(), out isNumber);
                        break;
                }
            }

            e.Handled = boolReturn;
        }

        private void mCloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void newSTaffImageButton_Click(object sender, EventArgs e)
        {//create a new staff data
            InitNewStaffInformation();
            mIsDirtyForm = true;
            mCurrentSelectedStaffRow = null;
            firstNameTextBox.Focus(); // DE2283 - Set cursor to First Name.
            SetWhetherControlsLocked(); // DE13019 - If fields are disabled, need to re-enable them
        }

        private void saveStaffImageButton_Click(object sender, EventArgs e)
        {
            if (ValidateStaff() == true)
            {
                if (SaveStaff() == true)
                {
                    ReloadFormAfterSave();
                }
                //todo: reload it the stafflistview if it is a new staff and set focus on it             
                //UpdateNewStaffRow();
            }
            //}
        }

        private void SwipCardImageButton_Click(object sender, EventArgs e)
        {
            // PDTS 1064
            MagCardForm magForm = new MagCardForm(mMagCardReader);
            if (magForm.ShowDialog() == DialogResult.OK)
            {
                magNumberTextBox.Text = magForm.MagCardNumber;
                mIsDirtyForm = true;
            }
            FocusStaffListView(mCurrentSelectedListViewIndex);
        }

        private void FocusStaffListView(int index)
        {
            staffListView.Select();
            if (staffListView.Items.Count > index && index > -1)
                staffListView.Items[index].Selected = true;
        }

        private bool IsSaveStaffInformationChange()
        {
            //then do a clean check to save if it is dirty form
            bool saved = false;
            //then there is change, ask if user want to save the change
            DialogResult result = MessageForm.Show(this, Properties.Resources.warningSave, Properties.Resources.securityCenter, MessageFormTypes.YesNoCancel);
            mUnchanged = true;
            if (result == DialogResult.Yes)
            {//save the new staff information and move on
                if (ValidateStaff())
                {
                    if (SaveStaff())
                    {
                        //LoadDataToListView(m_StaffStatus, mPosition);
                        ReloadFormAfterSave();

                        mIsDirtyForm = false;
                        saved = true;
                    }
                }
            }
            else if (result == DialogResult.No)
            { //return true if user do not want to save at all
                ReloadFormAfterSave();
                mIsDirtyForm = false;
                saved = true;
            }
            else //cancel, go back whatever it is before
            {
                mIsDirtyForm = false;
                saved = false;
            }
            return saved;
        }


        private void staffListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //checkpasswordsettings = false;
            //there is only one item selected once, we do not allow multi-selection
            if (staffListView.SelectedItems.Count == 0)
            {
                return;
            }

            //be very careful to change logic here, 
            //we handle Cancel change, this event will fire twice for a change
            if ((mIsDirtyForm == true || IsStaffInformationModified() == true) &&
                mUnchanged == false)
            {
                //if(passwordTextBox.Text.Trim().Length >0  || verifiedPasswordTextBox.Text.Trim().Length>0)
                //checkpasswordsettings = true;
                isReloading = true;
                if (IsSaveStaffInformationChange() == false)
                {//set it back before do anything because it is still dirty       
                    staffListView.Select();
                    staffListView.SelectedItems[0].Selected = false;
                    staffListView.Items[mCurrentSelectedListViewIndex].Selected = true;
                    isReloading = false;
                    return;
                }
                else
                {
                    //db commited succesfully ->  need to update the ListViewControls.
                    mCurrentSelectedListViewIndex = staffListView.SelectedIndices[0];//Get the selected staff
                    LoadDataToListView(m_StaffStatus, mPosition);//;->reload the listview controls
                    staffListView.Items[mCurrentSelectedListViewIndex].Selected = true;//reselect the selected staff
                    mUnchanged = false;
                    isReloading = false;
                    return;
                }

               // isReloading = false;
            }
            else if ((mIsDirtyForm == true || IsStaffInformationModified() == true) &&
                      mUnchanged == true &&
                      mCurrentSelectedListViewIndex != staffListView.SelectedIndices[0])
            {
                staffListView.Select();
                staffListView.SelectedItems[0].Selected = false;
                staffListView.Items[mCurrentSelectedListViewIndex].Selected = true;
                mUnchanged = false;
                return;
            }
            else if (mIsDirtyForm == true || IsStaffInformationModified() == true)
                return;//do not reload if it is dirty

          //  int count = staffListView.Items.Count;
            mCurrentSelectedListViewIndex = staffListView.SelectedIndices[0];
            int staffID = int.Parse(staffListView.SelectedItems[0].Tag.ToString());
            DataRow[] selectedStaffRow = mStaffTable.Select(StaffData.STAFF_TALBE_COLUMN_STAFFID + " = '" + staffID.ToString() + "'");
            if (selectedStaffRow.Length == 1)
            {// there is only one
                mCurrentSelectedStaffRow = selectedStaffRow[0];
            }
            else
            {//it should never happy at all 
                MessageForm.Show(this, Properties.Resources.errorUnexpected);
                mCurrentSelectedStaffRow = null;
                return;
            }

            LoadAStaffInformation(mCurrentSelectedStaffRow);
            SelectedStaffId = Convert.ToInt32(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_STAFFID]);
            if (mLoginId > 0)
            {
                SetWhetherControlsLocked();
            }

            DisableControlForKioskLogin(mLoginId);
        }
        /// <summary>
        /// Enables or Diables the Controls Based on the Account Lock status.
        /// </summary>
        private void SetWhetherControlsLocked()
        {
       
                foreach (Control c in newStaffGroupBox.Controls)
                {
                    if (checkBoxlocked.Checked && !(c.Name == "checkBoxlocked"))
                        c.Enabled = false;
                    else
                        c.Enabled = true;
                }

                if (mLoginId > 0)
                {
                    loginNumericUpDown.Visible = true;
                    loginNumberLabel.Visible = true;

                }
            
        }

        private void assignPositionButton_Click(object sender, EventArgs e)
        {
            if (mCurrentSelectedStaffRow == null || mAssignedPositions == null) //there is no position assigned for this staff yet
            {
                mAssignedPositions = new PositionData();
            }
            AssignPositions position = new AssignPositions(ref mAssignedPositions);
            try
            {
                //position.Parent = this;
                //position.MdiParent = this.MdiParent;
                position.ShowDialog();
            }
            catch (Exception ex)
            {
                Utilities.Log("Exception message:" + ex.Message, LoggerLevel.Severe);
                Utilities.Log("Exception stack:" + ex.StackTrace, LoggerLevel.Severe);
                MessageForm.Show(ex.Message, Properties.Resources.securityCenter);
            }
            //nothing changed if it is canceled
            if (position.DialogResult == DialogResult.Cancel) return;

            //it is dirty and reload position if the OK button clicked
            mIsDirtyForm = true;
            //refresh positions in the list
            positionListBox.Items.Clear();
            var PositionInOrder = mAssignedPositions.PositionTable.Rows.Cast<DataRow>().OrderBy(y => y[PositionData.POSITION_COLUMN_POSITIONNAME]);

            foreach (DataRow p in PositionInOrder)
            {
                //tmpItem = new ListViewItem(p[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                //tmpItem.Tag = p[PositionData.POSITION_COLUMN_POSITIONNAME].ToString();
                positionListBox.Items.Add(p[PositionData.POSITION_COLUMN_POSITIONNAME]);
            }
            //FIX RALLY DE 3193 -- commented out the following code line it is making a null refrence exception
            //FocusStaffListView(mCurrentSelectedListViewIndex);
            //END FIX RALLY DE 3193 this brings up a save dialog which it should not according to the "sandbox veiw"
        }

        private void activeRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (activeRadioButton.Checked == true)
            {
                m_StaffStatus = 1;
                LoadDataToListView(1, positionComboBox.SelectedItem.ToString());
            }
        }

        private void allRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (allRadioButton.Checked == true)
            {
                m_StaffStatus = -1;
                LoadDataToListView(m_StaffStatus, positionComboBox.SelectedItem.ToString());
            }
        }

        private void inactiveRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (inactiveRadioButton.Checked == true)
            {
                m_StaffStatus = 0;
                LoadDataToListView(m_StaffStatus, positionComboBox.SelectedItem.ToString());
            }
        }


        private void positionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (inactiveRadioButton.Checked == true)
            {
                LoadDataToListView(0, positionComboBox.SelectedItem.ToString());
            }
            else if (activeRadioButton.Checked == true)
            {
                LoadDataToListView(1, positionComboBox.SelectedItem.ToString());
            }
            else
            {
                LoadDataToListView(0, positionComboBox.SelectedItem.ToString());
            }
            FocusStaffListView(mCurrentSelectedListViewIndex);
            mPosition = positionComboBox.SelectedItem.ToString();
        }

        #endregion //Events


        #region Private Utilities
        /// <summary>
        /// automatic get a login number available from the server
        /// </summary>
        private void SetNextAvailableStaffLoginNumber()
        {
            GetNextAvailableStaffLoginNumber nextLoginNo = new GetNextAvailableStaffLoginNumber();
            try
            {
                nextLoginNo.Send();
            }
            catch (Exception ex)
            {
                MessageForm.Show(Properties.Resources.errorFailedToGetData + " " + ex.Message, Properties.Resources.securityCenter);
            }         
            loginNumericUpDown.Value = (decimal)nextLoginNo.NextAavaiableStaffLoginNumber;//Setting new staff 
            mLoginId = Convert.ToInt32(loginNumericUpDown.Value);
        }

        /// <summary>
        /// Load staff data to the list view based on the filter: staff status and position
        /// </summary>
        /// <param name="activeFilter"></param>
        private void LoadDataToListView(Int16 activeFilter, string position)
        {
            Cursor.Current = Cursors.WaitCursor;
            Utilities.LogInfoIN();

            try
            {
                if (((SecurityCenterMDIParent)this.MdiParent).StaffList == null ||
                    ((SecurityCenterMDIParent)this.MdiParent).StaffList.Staff == null ||
                    ((SecurityCenterMDIParent)this.MdiParent).StaffList.Staff.StaffTable == null)
                {
                    return;
                }
                mStaffTable = ((SecurityCenterMDIParent)this.MdiParent).StaffList.Staff.StaffTable;
                string tempString = string.Empty;
                if (activeFilter == 0 || activeFilter == 1)
                {
                    tempString = "IsActive=" + activeFilter.ToString();
                }
                DataRow[] staffRows = mStaffTable.Select(tempString, StaffData.STAFF_TALBE_COLUMN_LASTNAME);
                System.Windows.Forms.ListViewItem tempItem;
                //clear it out
                staffListView.Items.Clear();
                staffListView.BeginUpdate();
                foreach (DataRow row in staffRows)
                {
                    //we would not show up gametech predefied staff, it is not editable
                    if (int.Parse(row[StaffData.STAFF_TALBE_COLUMN_STAFFID].ToString().Trim()) < 3)
                        continue;
                    tempString = GetPositionStringsOfStaffID(int.Parse(row[StaffData.STAFF_TALBE_COLUMN_STAFFID].ToString()));
                    //filter for the position, we will only the load the select position
                    if (!tempString.Contains(position) && !position.ToUpper().Equals("ALL"))
                        continue;
                    tempItem = new ListViewItem(new string[] 
                                                    { //loginID, firstname, last name
                                                        row[StaffData.STAFF_TALBE_COLUMN_LOGINNUMBER].ToString(),
                                                        row[StaffData.STAFF_TALBE_COLUMN_FIRSTNAME].ToString(),
                                                        row [StaffData.STAFF_TALBE_COLUMN_LASTNAME].ToString()
                                                    },
                                                -1);
                    tempItem.Tag = row[StaffData.STAFF_TALBE_COLUMN_STAFFID].ToString();
                    tempItem.Font = staffListView.Font;
                    staffListView.Items.Add(tempItem);
                }
            }
            catch (Exception ex)
            {
                MessageForm.Show(this, Properties.Resources.errorGeneral + ex.Message);
            }
            finally
            {
                staffListView.EndUpdate();
                Application.DoEvents();
                Cursor.Current = Cursors.Default;
                Utilities.LogInfoLeave();
            }
        }

        /// <summary>
        /// Return all positions delimited by comma for a given staffID
        /// </summary>
        /// <param name="staffID"></param>
        /// <returns></returns>
        private string GetPositionStringsOfStaffID(int staffID)
        {
            string positionStrings = String.Empty;
            string separator = ",";
            PositionData positions = ((SecurityCenterMDIParent)this.MdiParent).StaffList.PositionDatasByStaffID(staffID);
            if (positions != null)
            {
                foreach (DataRow positionRow in positions.PositionTable.Rows)
                {
                    positionStrings += positionRow[PositionData.POSITION_COLUMN_POSITIONNAME].ToString() + separator;
                }
            }
            if (!string.IsNullOrEmpty(positionStrings))
                positionStrings = positionStrings.Substring(0, positionStrings.Length - 1);

            return positionStrings;
        }


        public void ReloadStaffPositionListBox(int staffID)
        {
            LoadListBoxPosition(staffID);
        }

        public void ReloadUIStaffPositionCmbx()
        {
            LoadPositionToComboBox();

            if (positionComboBox.SelectedIndex != 0)
            {
                positionComboBox.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// Load all positions to the position combobox
        /// </summary>
        private void LoadPositionToComboBox()
        {
            Utilities.LogInfoIN();
            GetPositionList positionList = new GetPositionList(-1);
            //load available positions to the available lists
            try
            {
                positionList.Send();
            }
            catch (Exception ex)
            {
                Utilities.Log(Properties.Resources.errorFailedToGetData + " " + ex.Message, LoggerLevel.Severe);
                Utilities.LogInfoLeave();
                MessageForm.Show(this, Properties.Resources.errorFailedToGetData, Properties.Resources.securityCenter);
                return;
            }
            PositionData mAvailablePositions = positionList.Positions;

            //clear up before load
            positionComboBox.Items.Clear();
            positionComboBox.Items.Add("All");

            var PositionInOrder = mAvailablePositions.PositionTable.Rows.Cast<DataRow>().OrderBy(y => y[PositionData.POSITION_COLUMN_POSITIONNAME]);

            foreach (DataRow position in PositionInOrder)
            {
                //FIX: RALLY DE1573 Only Show active postions START
                if ((bool)position[PositionData.POSITION_COLUMN_ACTIVITYFLAG])
                {
                    positionComboBox.Items.Add(position[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                }
                //FIX: RALLY DE1573 Only Show active postions END
            }
            Utilities.LogInfoLeave();
        }

        private void DisableControlForKioskLogin(int loginId)
        {
            if (loginId < 0)
            {  
                if (loginNumericUpDown.Visible == true)
                {                                  
                    foreach (Control ctrl in newStaffGroupBox.Controls)
                    {
                        if (ctrl is TextBox)
                        {
                            if (ctrl.Name == "firstNameTextBox" || ctrl.Name == "lastNameTextBox")
                            {
                                continue;
                            }
                        }

                        if (ctrl is Label)
                        {
                            if (ctrl.Name == "lNamelabel" || ctrl.Name == "fNamelabel")
                            {
                                continue;
                            }
                        }                      
                   if (ctrl.Enabled == true)     ctrl.Enabled = false;
                    }

                    loginNumericUpDown.Visible = false;
                    loginNumberLabel.Visible = false;
                }
            }
            else
            {
                if (loginNumericUpDown.Visible != true)
                {

                    foreach (Control ctrl in newStaffGroupBox.Controls)
                    {
                      if (ctrl.Enabled == false)  ctrl.Enabled = true;
                    }
                    loginNumericUpDown.Visible = true;
                    loginNumberLabel.Visible = true;
                }
            }          
        }


        private int mLoginId; 
        
        private void LoadAStaffInformation(DataRow staffRowByID)
        {
            Utilities.LogInfoIN();
            //error check
            if (object.ReferenceEquals(staffRowByID, null)) return;
            firstNameTextBox.Text = staffRowByID[StaffData.STAFF_TALBE_COLUMN_FIRSTNAME].ToString();
            lastNameTextBox.Text = staffRowByID[StaffData.STAFF_TALBE_COLUMN_LASTNAME].ToString();
            mLoginId = int.Parse(staffRowByID[StaffData.STAFF_TALBE_COLUMN_LOGINNUMBER].ToString());

            if (mLoginId < 0)
            {
                loginNumericUpDown.Value = 0;
                mIsKioskStaff = true;
            }
            else
            {
                loginNumericUpDown.Value = mLoginId;
                mIsKioskStaff = false;
            }

            //DisableControlForKioskLogin(int.Parse(staffRowByID[StaffData.STAFF_TALBE_COLUMN_LOGINNUMBER].ToString()));

                //passwordNumericUpDown.Value = int.Parse(mStaffRowByID[StaffData.STAFF_TALBE_COLUMN_PASSWORD].ToString ());
            homePhoneTextBox.Text = staffRowByID[StaffData.STAFF_TALBE_COLUMN_HOMEPHONE].ToString();
            otherPhoneTextBox.Text = staffRowByID[StaffData.STAFF_TALBE_COLUMN_OTHERPHONE].ToString();
            SSNTextBox.Text = staffRowByID[StaffData.STAFF_TALBE_COLUMN_SSN].ToString();
            magNumberTextBox.Text = staffRowByID[StaffData.STAFF_TALBE_COLUMN_MAGSTRIPNUMBER].ToString();
            passwordTextBox.Text = string.Empty; //init to empty we can not know because it is hashed
            verifiedPasswordTextBox.Text = string.Empty;
            int staffID = int.Parse(staffRowByID[StaffData.STAFF_TALBE_COLUMN_STAFFID].ToString());
            mSelectedStaffAddress = ((SecurityCenterMDIParent)this.MdiParent).StaffList.GetAddressByStaffID(staffID);
            if (mSelectedStaffAddress != null)
            {
                adress1TextBox.Text = ((GTI.Modules.SecurityCenter.Data.Address)mSelectedStaffAddress).Address1;
                adress2TextBox.Text = ((GTI.Modules.SecurityCenter.Data.Address)mSelectedStaffAddress).Address2;
                cityTextBox.Text = ((GTI.Modules.SecurityCenter.Data.Address)mSelectedStaffAddress).City;
                stateTextBox.Text = ((GTI.Modules.SecurityCenter.Data.Address)mSelectedStaffAddress).State;
                zipTextBox.Text = ((GTI.Modules.SecurityCenter.Data.Address)mSelectedStaffAddress).Zip;
                countryTextBox.Text = ((GTI.Modules.SecurityCenter.Data.Address)mSelectedStaffAddress).Country;
                //mAddressID = ((GTI.Modules.SecurityCenter.Data.Address)staffAddress).AddressID;
            }

            checkBoxActive.Checked = staffRowByID[StaffData.STAFF_TALBE_COLUMN_ISACTIVE].ToString().Equals("1") ? true : false;
            checkBoxlocked.Checked = staffRowByID[StaffData.STAFF_TABLE_COLUMN_ACCTLOCKED].ToString().Equals("1") ? true : false;
            checkBoxLeftHanded.Checked = staffRowByID[StaffData.STAFF_TALBE_COLUMN_HAND].ToString().Equals("1") ? true : false;

            string tmpDate;

            if (!string.IsNullOrEmpty(staffRowByID[StaffData.STAFF_TALBE_COLUMN_HIREDATE].ToString()))
            {
                tmpDate = staffRowByID[StaffData.STAFF_TALBE_COLUMN_HIREDATE].ToString(); //.
                //Substring(0, mStaffRowByID[StaffData.STAFF_TALBE_COLUMN_HIREDATE].ToString().IndexOf('/'));

                hireDateTimePicker.Value = DateTime.Parse(tmpDate, CultureInfo.InvariantCulture);
            }
            else
            {
                hireDateTimePicker.Value = hireDateTimePicker.MinDate;
            }

            if (!string.IsNullOrEmpty(staffRowByID[StaffData.STAFF_TALBE_COLUMN_BIRTHDATE].ToString()))
            {
                tmpDate = staffRowByID[StaffData.STAFF_TALBE_COLUMN_BIRTHDATE].ToString();
                DOBDateTimePicker.Value = DateTime.Parse(tmpDate, CultureInfo.InvariantCulture);
            }
            else
            {
                DOBDateTimePicker.Value = DOBDateTimePicker.MinDate;
            }

            LoadListBoxPosition(staffID);          
        }

        private void LoadListBoxPosition(int staffID)
        {
            positionListBox.Items.Clear();
            mAssignedPositions = ((SecurityCenterMDIParent)this.MdiParent).StaffList.PositionDatasByStaffID(staffID);
            if (mAssignedPositions != null &&
                mAssignedPositions.PositionTable != null &&
                mAssignedPositions.PositionTable.Rows.Count > 0)
            {

                var PositionInOrder = mAssignedPositions.PositionTable.Rows.Cast<DataRow>().OrderBy(y => y[PositionData.POSITION_COLUMN_POSITIONNAME]);

                foreach (DataRow position in PositionInOrder)
                {
                    //tmpItem = new ListViewItem(position[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                    //tmpItem.Tag = position[PositionData.POSITION_COLUMN_POSITIONNAME].ToString();
                    positionListBox.Items.Add(position[PositionData.POSITION_COLUMN_POSITIONNAME].ToString());
                }
            }
            Utilities.LogInfoLeave();
        }

        private bool IsStaffInformationModified()
        {
            if (mIsDirtyForm == true) return true;
            if (mCurrentSelectedStaffRow == null) return false;
            //compare the current content with the selected content
            ModifyingAccountlock = false;
            if (!IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TABLE_COLUMN_ACCTLOCKED].ToString(), (checkBoxlocked.Checked == true ? 1 : 0).ToString()))
            {
                ModifyingAccountlock = true;
                mIsDirtyForm = true;
                return mIsDirtyForm;
            }

            var x = mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_LOGINNUMBER].ToString();
            if (Convert.ToInt32(x) > 0)
            {
                 mIsKioskStaff = false;
                if (!IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_FIRSTNAME].ToString(), firstNameTextBox.Text)
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_LASTNAME].ToString(), lastNameTextBox.Text)
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_HOMEPHONE].ToString(), homePhoneTextBox.Text)
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_LOGINNUMBER].ToString(), loginNumericUpDown.Value.ToString())
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_ISACTIVE].ToString(), (checkBoxActive.Checked == true ? 1 : 0).ToString())
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_HAND].ToString(), (checkBoxLeftHanded.Checked == true ? 1 : 0).ToString())
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_OTHERPHONE].ToString(), otherPhoneTextBox.Text)
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_SSN].ToString(), SSNTextBox.Text)
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_MAGSTRIPNUMBER].ToString(), magNumberTextBox.Text)
                    || IsAddressAndPasswordModified(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_STAFFID].ToString()) == true
                    )
                {
                    mIsDirtyForm = true;
                    return mIsDirtyForm;
                }
            }
            else
            {
                mIsKioskStaff = true;
                if (!IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_FIRSTNAME].ToString(), firstNameTextBox.Text)
                    || !IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_LASTNAME].ToString(), lastNameTextBox.Text)                  
                    )
                {
                    mIsDirtyForm = true;
                    return mIsDirtyForm;
                }
            }

            if (!IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_BIRTHDATE].ToString(), DOBDateTimePicker.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)))
            {
                //the dates do not match the currently selected row. 

                bool stringNullOrEmpty = string.IsNullOrEmpty(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_BIRTHDATE].ToString());
                bool valueEqualMinDate = DOBDateTimePicker.Value == DOBDateTimePicker.MinDate;

                //check for null dates and implied null value
                if (!(stringNullOrEmpty && valueEqualMinDate))
                {
                    mIsDirtyForm = true;
                    return true;
                }

                if (stringNullOrEmpty && valueEqualMinDate == false)
                {
                    mIsDirtyForm = true;
                    return true;
                }
            }

            if (!IsSameString(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_HIREDATE].ToString(), hireDateTimePicker.Value.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)))
            {
                //the dates do not match the currently selected row. 

                bool stringNullOrEmpty = string.IsNullOrEmpty(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_HIREDATE].ToString());
                bool valueEqualMinDate = hireDateTimePicker.Value == hireDateTimePicker.MinDate;

                //check for null dates and implied null value
                if (!(stringNullOrEmpty && valueEqualMinDate))
                {
                    mIsDirtyForm = true;
                    return true;
                }

                if (stringNullOrEmpty && valueEqualMinDate == false)
                {
                    mIsDirtyForm = true;
                    return true;
                }
            }
            return false;//it is not modified even get here
        }

        private bool IsAddressAndPasswordModified(string staffID)
        {
            if (mIsDirtyForm == true || staffID == string.Empty) return true;
            int staffid = 0;
            try
            {
                staffid = int.Parse(staffID);
            }
            catch (Exception ex)
            {
                MessageForm.Show(this, ex.Message, Properties.Resources.securityCenter);
                return false;
            }
            //verifying if address modified
            GTI.Modules.SecurityCenter.Data.Address? staffAddress = ((SecurityCenterMDIParent)this.MdiParent).StaffList.GetAddressByStaffID(staffid);
            if (staffAddress != null)
            {
                if (!IsSameString(adress1TextBox.Text, ((GTI.Modules.SecurityCenter.Data.Address)staffAddress).Address1) ||
                    !IsSameString(adress2TextBox.Text, ((GTI.Modules.SecurityCenter.Data.Address)staffAddress).Address2) ||
                    !IsSameString(cityTextBox.Text, ((GTI.Modules.SecurityCenter.Data.Address)staffAddress).City) ||
                    !IsSameString(stateTextBox.Text, ((GTI.Modules.SecurityCenter.Data.Address)staffAddress).State) ||
                    !IsSameString(zipTextBox.Text, ((GTI.Modules.SecurityCenter.Data.Address)staffAddress).Zip) ||
                    !IsSameString(countryTextBox.Text, ((GTI.Modules.SecurityCenter.Data.Address)staffAddress).Country)
                   )
                    return true;
            }
            //varifying is password modified
            if (passwordTextBox.Text != string.Empty) return true;

            return mIsDirtyForm;//false if get here

        }
        /// <summary>
        /// To compare two strings test1 and test2, which contains when either is null or empty string
        /// </summary>
        /// <param name="test1"></param>
        /// <param name="test2"></param>
        /// <returns>return true if not same or both null or empty, else return false if same</returns>
        private bool IsSameString(string test1, string test2)
        {
            if (String.IsNullOrEmpty(test1) && String.IsNullOrEmpty(test2)) //treat them same
                return true;
            else
                return String.Equals(test1, test2, StringComparison.CurrentCultureIgnoreCase);
        }

        private bool ValidateStaff()
        {
            bool isValidated = false;

            if (((passwordTextBox.Text == null ||
                  passwordTextBox.Text.Trim() == string.Empty) &&
                  mCurrentSelectedStaffRow == null
                ) && mIsKioskStaff == false)
            {
                MessageForm.Show(Properties.Resources.warningPasswordBeforeSave, Properties.Resources.securityCenter);
                return false;
            }

            if ((passwordTextBox.Text.Length > 0 || verifiedPasswordTextBox.Text.Length > 0) && mIsKioskStaff == false)
            {
                if (passwordTextBox.Text.Trim().Length < Configuration.mMinimumPasswordLength)
                {
                    if (Configuration.mPasswordComplexitySetting)
                    {
                        MessageForm.Show(String.Format(Properties.Resources.Passwords, Configuration.mMinimumPasswordLength, Configuration.mPreviousPasswordNumber, Configuration.mPinExpireDays), Properties.Resources.securityCenter);
                        return false;
                    }
                    else
                    {
                        MessageForm.Show(String.Format(Properties.Resources.MinimumPasswordLength, Configuration.mMinimumPasswordLength), Properties.Resources.securityCenter);
                        return false;
                    }
                }

                if (Configuration.mPasswordComplexitySetting)
                {
                    if (!ValidatePassword(passwordTextBox.Text))
                    {
                        MessageForm.Show(String.Format(Properties.Resources.Passwords, Configuration.mMinimumPasswordLength, Configuration.mPreviousPasswordNumber, Configuration.mPinExpireDays), Properties.Resources.securityCenter);
                        return false;
                    }

                }
                if ((verifiedPasswordTextBox.Text == null ||
                    verifiedPasswordTextBox.Text.Trim() == string.Empty))
                {
                    MessageForm.Show(Properties.Resources.warningverifyingPassword, Properties.Resources.securityCenter);
                    return false;
                }

            }

            if (!IsSameString(passwordTextBox.Text, verifiedPasswordTextBox.Text)) 
            {
                MessageForm.Show(Properties.Resources.warningPasswordNotSame, Properties.Resources.securityCenter);
                return false;
            }
            //START RALLY DE 1572
            if (DOBDateTimePicker.Value != DOBDateTimePicker.MinDate && hireDateTimePicker.Value != hireDateTimePicker.MinDate &&
                DOBDateTimePicker.Value > hireDateTimePicker.Value)
            {
                MessageForm.Show(Properties.Resources.warningDOBAfterDateOfHire, Properties.Resources.securityCenter);
                return false;
            }
            //END RALLY DE 1572
            //validating
            if ((lastNameTextBox.Text == null ||
                lastNameTextBox.Text.Trim() == "" ||
                firstNameTextBox.Text == null ||
                firstNameTextBox.Text.Trim() == "" ||
                loginNumericUpDown.Value == 0
               ) && mLoginId > 0)
            {
                
                    isValidated = false;
                    MessageForm.Show(Properties.Resources.warningRequiredFiledBeforeSave, Properties.Resources.securityCenter);
                
            }
            else           
                 if (lastNameTextBox.Text == null ||
                lastNameTextBox.Text.Trim() == "" ||
                firstNameTextBox.Text == null ||
                firstNameTextBox.Text.Trim() == "")
                   
                 {
                       isValidated = false;
                    MessageForm.Show(Properties.Resources.warningRequiredFiledBeforeSave, Properties.Resources.securityCenter);
                 }
            else
            {
                isValidated = true;
            }
            //valid phone number


            return isValidated;
        }

        /// <summary>
        /// Method to Validate the Password when complexity is required
        /// </summary>
        /// <param name="password">the Password that entered</param>
        /// <returns>true if password meets requirement else false</returns>
        static bool ValidatePassword(string password)
        {
            // US4887
            bool foundUpper = false, foundLower = false, foundNumber = false, foundSpecial = false;
            int count = 0;

            if (Regex.IsMatch(password, FIND_UPPER))
            {
                foundUpper = true;
                count++;
            }
            if (Regex.IsMatch(password, FIND_LOWER))
            {
                foundLower = true;
                count++;
            }
            if (Regex.IsMatch(password, FIND_NUMBER))
            {
                foundNumber = true;
                count++;
            }
            if (Regex.IsMatch(password, FIND_SPECIAL))
            {
                foundSpecial = true;
                count++;
            }

            return count >= 2; // need 2 of 4

            #region OLD REGEX

            //string Pattern = @"^.*(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).*$";
            //if (Regex.IsMatch(passWord, Pattern))
            //{
            //    return true;
            //}
            //string Pattern1 = @"^.*(?=.*\d)(?=.*[a-z])(?=.*[!@#$%^+=]).*$";
            //if (Regex.IsMatch(passWord, Pattern1))
            //{
            //    return true;
            //}

            //string Pattern2 = @"^.*(?=.*\d)(?=.*[A-Z])(?=.*[!@#$%^+=]).*$";
            //if (Regex.IsMatch(passWord, Pattern2))
            //{
            //    return true;
            //}

            //string Pattern3 = @"^.*(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^+=]).*$";
            //if (Regex.IsMatch(passWord, Pattern3))
            //{
            //    return true;
            //}
            //return false;

            #endregion
        }



        private bool IsValidatedLoginNumberAndPassword()
        {
            //password has to be number
            foreach (char c in passwordTextBox.Text)
            {
                if (Char.IsDigit(c) == false)
                    return false;
            }
            return true;
        }

        private bool SaveStaff()
        {
            //nothing changed, do not bother to save
            if (mIsDirtyForm == false && IsStaffInformationModified() == false) return true;
            Utilities.LogInfoIN();

            this.Cursor = Cursors.WaitCursor;

            Staff savestaff = new Staff();
            if (lastNameTextBox.Text.Trim().Length > 0)
                savestaff.LastName = lastNameTextBox.Text.ToString();
            if (firstNameTextBox.Text.Trim().Length > 0)
                savestaff.FirstName = firstNameTextBox.Text.ToString();
            if (cityTextBox.Text.Trim().Length > 0)
                savestaff.Address.City = cityTextBox.Text;
            if (DOBDateTimePicker.Value != DOBDateTimePicker.MinDate)
                savestaff.BirthDate = DOBDateTimePicker.Value;

            if (hireDateTimePicker.Value != hireDateTimePicker.MinDate)
                savestaff.HireDate = hireDateTimePicker.Value;

            if (mLoginId > 0)
            {
                savestaff.LoginNumber = (Int32)loginNumericUpDown.Value;
            }
            else
            {
                savestaff.LoginNumber = mLoginId;
            }
                
                savestaff.IsActive = checkBoxActive.Checked;

            if (homePhoneTextBox.Text.Trim().Length > 0)
                savestaff.HomePhone = homePhoneTextBox.Text.ToString();

            if (otherPhoneTextBox.Text.Trim().Length > 0)
                savestaff.OtherPhone = otherPhoneTextBox.Text.ToString();

            if (SSNTextBox.Text.Trim().Length > 0)
                savestaff.GovIssuedIdNumber = SSNTextBox.Text.ToString();

            if (magNumberTextBox.Text.Trim().Length > 0)
                savestaff.MagneticCardNumber = magNumberTextBox.Text.ToString();

            if (adress1TextBox.Text.Trim().Length > 0)
                savestaff.Address.Address1 = adress1TextBox.Text.ToString();

            if (adress2TextBox.Text.Trim().Length > 0)
                savestaff.Address.Address2 = adress2TextBox.Text.ToString();

            if (cityTextBox.Text.Trim().Length > 0)
                savestaff.Address.City = cityTextBox.Text.ToString();

            if (stateTextBox.Text.Trim().Length > 0)
                savestaff.Address.State = stateTextBox.Text.ToString();

            if (zipTextBox.Text.Trim().Length > 0)
                savestaff.Address.Zipcode = zipTextBox.Text.ToString();

            if (countryTextBox.Text.Trim().Length > 0)
                savestaff.Address.Country = countryTextBox.Text.ToString();

            savestaff.LeftHanded = checkBoxLeftHanded.Checked;
            savestaff.AcctLocked = checkBoxlocked.Checked;

            //pack the data and call setstaff message
            // Create the streams we will be writing to.
            MemoryStream requestStream = new MemoryStream();
            BinaryWriter requestWriter = new BinaryWriter(requestStream, Encoding.Unicode);

            // Staff Id
            mStaffID = NEW_ID; //a new staff
            mAddressID = NEW_ID;
            if (mCurrentSelectedStaffRow != null) //existing staff
            {
                mStaffID = int.Parse(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_STAFFID].ToString());
                mAddressID = int.Parse(mCurrentSelectedStaffRow[StaffData.STAFF_TALBE_COLUMN_ADDRESSID].ToString());
            }

            SetStaff staff = null;
            SetStaffPassword pwd = null;
            try
            {
                //staff = new SetStaff(ref mStaffID, ref mAddressID, requestStream.ToArray());
                staff = new SetStaff(ref mStaffID, ref mAddressID, savestaff, ModifyingAccountlock);
                staff.Send();
                if (mStaffID < 1)
                {
                    mStaffID = staff.StaffID;
                }
                if (mAddressID < 1)
                {
                    mAddressID = staff.AddressID;
                }
                //save the password
                if (passwordTextBox.Text != "" && passwordTextBox.Text.Length > 0)
                {
                    pwd = new SetStaffPassword((int)loginNumericUpDown.Value, (passwordTextBox.Text));
                    pwd.Send();
                }
                //save positions as well
                //DE1567 - Send position data even if zero, this will delete the existing ones
                if (mAssignedPositions != null) // && mAssignedPositions.PositionTable.Rows.Count > 0)
                {
                    int positionCount = 0;
                    int[] positionIDs = new int[mAssignedPositions.PositionTable.Rows.Count];
                    for (positionCount = 0; positionCount < mAssignedPositions.PositionTable.Rows.Count; positionCount++)
                    {
                        positionIDs[positionCount] = Int16.Parse(mAssignedPositions.PositionTable.Rows[positionCount][PositionData.POSITION_COLUMN_POSITIONID].ToString());

                    }
                    SetStaffPosition staffPositions = new SetStaffPosition(mStaffID, positionIDs);
                    staffPositions.Send();
                }
            }
            catch (Exception ex)
            {
                string errorMessage;

                if (staff.ReturnCode == 1)
                {
                    errorMessage = Properties.Resources.warningDupLoginNumber;
                }
                else if (staff.ReturnCode == 2)
                {
                    errorMessage = Properties.Resources.warningDupMagCard;
                }
                else if (staff.ReturnCode == Constants.DuplicateDataError)
                {
                    errorMessage = Properties.Resources.errorFailedSaveStaffDup;
                }
                else if (pwd.ReturnCode == (int)GTIServerReturnCode.PasswordReuseError)
                {
                    if (Configuration.mPasswordComplexitySetting)
                    {
                        errorMessage = String.Format(Properties.Resources.Passwords, Configuration.mMinimumPasswordLength, Configuration.mPreviousPasswordNumber, Configuration.mPinExpireDays);
                    }
                    else if (Configuration.mMinimumPasswordLength > 1)
                        errorMessage = String.Format(Properties.Resources.MPasswordReuseError, Configuration.mMinimumPasswordLength, Configuration.mPreviousPasswordNumber);
                    else
                        errorMessage = String.Format(Properties.Resources.PasswordReuseError, Configuration.mPreviousPasswordNumber);
                }
                else if (pwd.ReturnCode == (int)GTIServerReturnCode.AccountLocked)
                {
                    errorMessage = String.Format(Properties.Resources.Passwords, Configuration.mMinimumPasswordLength, Configuration.mPreviousPasswordNumber, Configuration.mPinExpireDays);
                }
                else
                {
                    errorMessage = Properties.Resources.errorFailedSaveStaff + ", " + ex.Message;
                }
                Utilities.Log(errorMessage, LoggerLevel.Severe);
                Utilities.Log("Excption Stack:" + ex.StackTrace, LoggerLevel.Severe);

                MessageForm.Show(errorMessage, Properties.Resources.securityCenter);

                Utilities.LogInfoLeave();
                return false;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
            mIsDirtyForm = false; //it is clean after save
            Utilities.LogInfoLeave();
            return true;
        }

        private void InitNewStaffInformation()
        {
            SetNextAvailableStaffLoginNumber();
            //Last Name
            lastNameTextBox.Text = string.Empty;
            firstNameTextBox.Text = string.Empty;
            //birth Date
            DOBDateTimePicker.Value = DOBDateTimePicker.MinDate;
            //hire date
            hireDateTimePicker.Value = hireDateTimePicker.MinDate;
            //is active member
            checkBoxActive.Checked = true;
            checkBoxlocked.Checked = false;

            //phone 1
            homePhoneTextBox.Text = string.Empty;
            //phone 2
            otherPhoneTextBox.Text = string.Empty;
            //governemnt issued identity number
            SSNTextBox.Text = string.Empty;
            //Mag number
            magNumberTextBox.Text = string.Empty;
            //address
            adress1TextBox.Text = string.Empty;
            adress2TextBox.Text = string.Empty;
            cityTextBox.Text = string.Empty;
            stateTextBox.Text = string.Empty;
            zipTextBox.Text = string.Empty;
            countryTextBox.Text = string.Empty;
            //the password
            passwordTextBox.Text = string.Empty;
            verifiedPasswordTextBox.Text = string.Empty;
            magNumberTextBox.Text = string.Empty;
            positionListBox.Items.Clear();
            // FIX: DE1593 - Position got automatically set on a save without user selecting a position.
            mAssignedPositions = null;
            // END: DE1593
        }


        /// <summary>
        /// Reload the form after saving a modified or new staff
        /// </summary>
        private void ReloadFormAfterSave()
        {
            //ttp 50307
            mWaitingForm = new WaitForm();
            mWaitingForm.Message = Properties.Resources.splashInfoLoadStaffPositionModule;
            mWaitingForm.StartPosition = FormStartPosition.CenterParent;
            mWaitingForm.WaitImage = Properties.Resources.Waiting;
            mWaitingForm.CancelButtonVisible = false;
            mWaitingForm.ProgressBarVisible = false;

            BackgroundWorker m_worker = new BackgroundWorker();
            m_worker.WorkerReportsProgress = true;
            m_worker.WorkerSupportsCancellation = true;
            m_worker.DoWork += new DoWorkEventHandler(DoLoadStaffPositionData);
            //m_worker.ProgressChanged += new ProgressChangedEventHandler(mWaitingForm.ReportProgress);
            m_worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(DoLoadStaffPositionDataCompleted);
            m_worker.RunWorkerAsync();
            mWaitingForm.ShowDialog(); //Block until we are done

            mWaitingForm.Dispose();
            mWaitingForm = null;

            mCurrentSelectedStaffRow = null;

            if (!isReloading)
            {
                positionComboBox_SelectedIndexChanged(this, EventArgs.Empty);
            }
        }

        //ttp 50307
        private void DoLoadStaffPositionData(object sender, DoWorkEventArgs doEA)
        {
            ((SecurityCenterMDIParent)this.MdiParent).LoadStaff();
        }

        private void DoLoadStaffPositionDataCompleted(object sender, RunWorkerCompletedEventArgs RunEA)
        {
            if (RunEA.Error != null)
            {
                MessageForm.Show(this, RunEA.Error.Message, Properties.Resources.securityCenter);
            }

            mWaitingForm.CloseForm();
        }

        //end of ttp 50307
        #endregion //private utilities

        private void loginNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (loginNumericUpDown.Value < 2 && loginNumericUpDown.Value != 0)
            {
                SetNextAvailableStaffLoginNumber();
            }
        }

        // PDTS 1064
        private void FormClose(object sender, FormClosingEventArgs e)
        {
            // Stop reading from the mag card since we are closing the form.
            mMagCardReader.EndReading();
        }

        private bool busy;
        private void hireDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (!busy)
            {
                busy = true;
                //This is kind of a trick to display  a blank space if the database value is null
                //datetimepickers do not allow null or empty values so the value is set to the
                //min date and then formated to show blank.  
                if (hireDateTimePicker.Value == hireDateTimePicker.MinDate)
                {

                    hireDateTimePicker.Format = DateTimePickerFormat.Custom;
                    hireDateTimePicker.CustomFormat = "'Not Set'";
                }
                else
                {

                    hireDateTimePicker.Format = DateTimePickerFormat.Short;
                }
                busy = false;
            }
        }

        private void DOBDateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            //This is kind of a trick to display  a blank space if the database value is null
            //datetimepickers do not allow null or empty values so the value is set to the
            //min date and then formated to show blank.  
            if (DOBDateTimePicker.Value == DOBDateTimePicker.MinDate)
            {
                DOBDateTimePicker.Format = DateTimePickerFormat.Custom;
                DOBDateTimePicker.CustomFormat = "'Not Set'";
            }
            else
            {
                DOBDateTimePicker.Format = DateTimePickerFormat.Short;
            }
        }



        private void DateTimePicker_Enter(object sender, EventArgs e)
        {
            DateTimePicker dtPicker = sender as DateTimePicker;
            if (dtPicker != null && dtPicker.Value == dtPicker.MinDate)
            {
                dtPicker.Value = DateTime.Today;
            }
        }

        public int SelectedStaffId
        {
            get;
            set;
        }
    }
}
