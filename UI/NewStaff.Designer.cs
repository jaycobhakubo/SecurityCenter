namespace GTI.Modules.SecurityCenter
{
    partial class NewStaff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewStaff));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.inactiveRadioButton = new System.Windows.Forms.RadioButton();
            this.allRadioButton = new System.Windows.Forms.RadioButton();
            this.activeRadioButton = new System.Windows.Forms.RadioButton();
            this.positionComboBox = new System.Windows.Forms.ComboBox();
            this.psotionLabel1 = new System.Windows.Forms.Label();
            this.staffListView = new GTI.Controls.GTIListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.newStaffGroupBox = new System.Windows.Forms.GroupBox();
            this.checkBoxLeftHanded = new System.Windows.Forms.CheckBox();
            this.checkBoxlocked = new System.Windows.Forms.CheckBox();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.HireDateLabel = new System.Windows.Forms.Label();
            this.hireDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.magNumberTextBox = new GTI.Controls.ImageLabel();
            this.DOBDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.SwipCardImageButton = new GTI.Controls.ImageButton();
            this.SSNTextBox = new System.Windows.Forms.TextBox();
            this.verifiedPasswordTextBox = new System.Windows.Forms.TextBox();
            this.magLabel = new System.Windows.Forms.Label();
            this.dateBirthlabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.assignPositionButton = new GTI.Controls.ImageButton();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.positionListBox = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.otherPhoneTextBox = new System.Windows.Forms.TextBox();
            this.SSNlabel = new System.Windows.Forms.Label();
            this.homePhoneTextBox = new System.Windows.Forms.TextBox();
            this.loginNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.fNamelabel = new System.Windows.Forms.Label();
            this.zipTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.loginNumberLabel = new System.Windows.Forms.Label();
            this.countryTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.firstNameTextBox = new System.Windows.Forms.TextBox();
            this.countrylabel = new System.Windows.Forms.Label();
            this.lNamelabel = new System.Windows.Forms.Label();
            this.lastNameTextBox = new System.Windows.Forms.TextBox();
            this.address1label = new System.Windows.Forms.Label();
            this.ziplabel = new System.Windows.Forms.Label();
            this.adress1TextBox = new System.Windows.Forms.TextBox();
            this.stateTextBox = new System.Windows.Forms.TextBox();
            this.adress2label = new System.Windows.Forms.Label();
            this.statelabel = new System.Windows.Forms.Label();
            this.adress2TextBox = new System.Windows.Forms.TextBox();
            this.cityTextBox = new System.Windows.Forms.TextBox();
            this.citylabel = new System.Windows.Forms.Label();
            this.newSTaffImageButton = new GTI.Controls.ImageButton();
            this.saveStaffImageButton = new GTI.Controls.ImageButton();
            this.mCloseButton = new GTI.Controls.ImageButton();
            this.groupBox1.SuspendLayout();
            this.newStaffGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.inactiveRadioButton);
            this.groupBox1.Controls.Add(this.allRadioButton);
            this.groupBox1.Controls.Add(this.activeRadioButton);
            this.groupBox1.Controls.Add(this.positionComboBox);
            this.groupBox1.Controls.Add(this.psotionLabel1);
            this.groupBox1.Controls.Add(this.staffListView);
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(374, 579);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Staff";
            // 
            // inactiveRadioButton
            // 
            this.inactiveRadioButton.AutoSize = true;
            this.inactiveRadioButton.Location = new System.Drawing.Point(267, 19);
            this.inactiveRadioButton.Name = "inactiveRadioButton";
            this.inactiveRadioButton.Size = new System.Drawing.Size(85, 26);
            this.inactiveRadioButton.TabIndex = 2;
            this.inactiveRadioButton.Text = "Inactive";
            this.inactiveRadioButton.UseVisualStyleBackColor = true;
            this.inactiveRadioButton.CheckedChanged += new System.EventHandler(this.inactiveRadioButton_CheckedChanged);
            // 
            // allRadioButton
            // 
            this.allRadioButton.AutoSize = true;
            this.allRadioButton.Location = new System.Drawing.Point(144, 19);
            this.allRadioButton.Name = "allRadioButton";
            this.allRadioButton.Size = new System.Drawing.Size(45, 26);
            this.allRadioButton.TabIndex = 1;
            this.allRadioButton.TabStop = true;
            this.allRadioButton.Text = "All";
            this.allRadioButton.UseVisualStyleBackColor = true;
            this.allRadioButton.CheckedChanged += new System.EventHandler(this.allRadioButton_CheckedChanged);
            // 
            // activeRadioButton
            // 
            this.activeRadioButton.AutoSize = true;
            this.activeRadioButton.Checked = true;
            this.activeRadioButton.Location = new System.Drawing.Point(10, 19);
            this.activeRadioButton.Name = "activeRadioButton";
            this.activeRadioButton.Size = new System.Drawing.Size(74, 26);
            this.activeRadioButton.TabIndex = 0;
            this.activeRadioButton.TabStop = true;
            this.activeRadioButton.Text = "Active";
            this.activeRadioButton.UseVisualStyleBackColor = true;
            this.activeRadioButton.CheckedChanged += new System.EventHandler(this.activeRadioButton_CheckedChanged);
            // 
            // positionComboBox
            // 
            this.positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.positionComboBox.FormattingEnabled = true;
            this.positionComboBox.Items.AddRange(new object[] {
            "All"});
            this.positionComboBox.Location = new System.Drawing.Point(112, 48);
            this.positionComboBox.Name = "positionComboBox";
            this.positionComboBox.Size = new System.Drawing.Size(256, 30);
            this.positionComboBox.TabIndex = 4;
            this.positionComboBox.SelectedIndexChanged += new System.EventHandler(this.positionComboBox_SelectedIndexChanged);
            // 
            // psotionLabel1
            // 
            this.psotionLabel1.AutoSize = true;
            this.psotionLabel1.Location = new System.Drawing.Point(6, 51);
            this.psotionLabel1.Name = "psotionLabel1";
            this.psotionLabel1.Size = new System.Drawing.Size(67, 22);
            this.psotionLabel1.TabIndex = 3;
            this.psotionLabel1.Text = "Position";
            // 
            // staffListView
            // 
            this.staffListView.AllowEraseBackground = true;
            this.staffListView.BackColor = System.Drawing.Color.White;
            this.staffListView.BackgroundImageTiled = true;
            this.staffListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.staffListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.staffListView.ForeColor = System.Drawing.Color.Black;
            this.staffListView.FullRowSelect = true;
            this.staffListView.GridLines = true;
            this.staffListView.HideSelection = false;
            this.staffListView.LabelWrap = false;
            this.staffListView.Location = new System.Drawing.Point(4, 84);
            this.staffListView.MultiSelect = false;
            this.staffListView.Name = "staffListView";
            this.staffListView.OwnerDraw = true;
            this.staffListView.Size = new System.Drawing.Size(364, 476);
            this.staffListView.SortColumn = 0;
            this.staffListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.staffListView.TabIndex = 5;
            this.staffListView.UseCompatibleStateImageBehavior = false;
            this.staffListView.View = System.Windows.Forms.View.Details;
            this.staffListView.SelectedIndexChanged += new System.EventHandler(this.staffListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "numeric";
            this.columnHeader1.Text = "Login";
            this.columnHeader1.Width = 75;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "alpha";
            this.columnHeader2.Text = "First Name";
            this.columnHeader2.Width = 141;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "alpha";
            this.columnHeader3.Text = "Last Name";
            this.columnHeader3.Width = 141;
            // 
            // newStaffGroupBox
            // 
            this.newStaffGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.newStaffGroupBox.Controls.Add(this.checkBoxLeftHanded);
            this.newStaffGroupBox.Controls.Add(this.checkBoxlocked);
            this.newStaffGroupBox.Controls.Add(this.checkBoxActive);
            this.newStaffGroupBox.Controls.Add(this.HireDateLabel);
            this.newStaffGroupBox.Controls.Add(this.hireDateTimePicker);
            this.newStaffGroupBox.Controls.Add(this.magNumberTextBox);
            this.newStaffGroupBox.Controls.Add(this.DOBDateTimePicker);
            this.newStaffGroupBox.Controls.Add(this.SwipCardImageButton);
            this.newStaffGroupBox.Controls.Add(this.SSNTextBox);
            this.newStaffGroupBox.Controls.Add(this.verifiedPasswordTextBox);
            this.newStaffGroupBox.Controls.Add(this.magLabel);
            this.newStaffGroupBox.Controls.Add(this.dateBirthlabel);
            this.newStaffGroupBox.Controls.Add(this.label3);
            this.newStaffGroupBox.Controls.Add(this.passwordTextBox);
            this.newStaffGroupBox.Controls.Add(this.assignPositionButton);
            this.newStaffGroupBox.Controls.Add(this.passwordLabel);
            this.newStaffGroupBox.Controls.Add(this.positionListBox);
            this.newStaffGroupBox.Controls.Add(this.label4);
            this.newStaffGroupBox.Controls.Add(this.otherPhoneTextBox);
            this.newStaffGroupBox.Controls.Add(this.SSNlabel);
            this.newStaffGroupBox.Controls.Add(this.homePhoneTextBox);
            this.newStaffGroupBox.Controls.Add(this.loginNumericUpDown);
            this.newStaffGroupBox.Controls.Add(this.fNamelabel);
            this.newStaffGroupBox.Controls.Add(this.zipTextBox);
            this.newStaffGroupBox.Controls.Add(this.label2);
            this.newStaffGroupBox.Controls.Add(this.loginNumberLabel);
            this.newStaffGroupBox.Controls.Add(this.countryTextBox);
            this.newStaffGroupBox.Controls.Add(this.label1);
            this.newStaffGroupBox.Controls.Add(this.firstNameTextBox);
            this.newStaffGroupBox.Controls.Add(this.countrylabel);
            this.newStaffGroupBox.Controls.Add(this.lNamelabel);
            this.newStaffGroupBox.Controls.Add(this.lastNameTextBox);
            this.newStaffGroupBox.Controls.Add(this.address1label);
            this.newStaffGroupBox.Controls.Add(this.ziplabel);
            this.newStaffGroupBox.Controls.Add(this.adress1TextBox);
            this.newStaffGroupBox.Controls.Add(this.stateTextBox);
            this.newStaffGroupBox.Controls.Add(this.adress2label);
            this.newStaffGroupBox.Controls.Add(this.statelabel);
            this.newStaffGroupBox.Controls.Add(this.adress2TextBox);
            this.newStaffGroupBox.Controls.Add(this.cityTextBox);
            this.newStaffGroupBox.Controls.Add(this.citylabel);
            this.newStaffGroupBox.Location = new System.Drawing.Point(403, 7);
            this.newStaffGroupBox.Name = "newStaffGroupBox";
            this.newStaffGroupBox.Size = new System.Drawing.Size(603, 578);
            this.newStaffGroupBox.TabIndex = 1;
            this.newStaffGroupBox.TabStop = false;
            this.newStaffGroupBox.Text = "Staff Information";
            // 
            // checkBoxLeftHanded
            // 
            this.checkBoxLeftHanded.AutoSize = true;
            this.checkBoxLeftHanded.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.checkBoxLeftHanded.Location = new System.Drawing.Point(28, 476);
            this.checkBoxLeftHanded.Name = "checkBoxLeftHanded";
            this.checkBoxLeftHanded.Size = new System.Drawing.Size(118, 26);
            this.checkBoxLeftHanded.TabIndex = 42;
            this.checkBoxLeftHanded.Text = "Left handed";
            this.checkBoxLeftHanded.UseVisualStyleBackColor = true;
            // 
            // checkBoxlocked
            // 
            this.checkBoxlocked.AutoSize = true;
            this.checkBoxlocked.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.checkBoxlocked.Location = new System.Drawing.Point(159, 443);
            this.checkBoxlocked.Name = "checkBoxlocked";
            this.checkBoxlocked.Size = new System.Drawing.Size(84, 26);
            this.checkBoxlocked.TabIndex = 41;
            this.checkBoxlocked.Text = "Locked";
            this.checkBoxlocked.UseVisualStyleBackColor = true;
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.checkBoxActive.Location = new System.Drawing.Point(28, 443);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(77, 26);
            this.checkBoxActive.TabIndex = 40;
            this.checkBoxActive.Text = "Active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // HireDateLabel
            // 
            this.HireDateLabel.AutoSize = true;
            this.HireDateLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.HireDateLabel.Location = new System.Drawing.Point(24, 505);
            this.HireDateLabel.Name = "HireDateLabel";
            this.HireDateLabel.Size = new System.Drawing.Size(81, 22);
            this.HireDateLabel.TabIndex = 35;
            this.HireDateLabel.Text = "Hire Date";
            // 
            // hireDateTimePicker
            // 
            this.hireDateTimePicker.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.hireDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hireDateTimePicker.Location = new System.Drawing.Point(28, 533);
            this.hireDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.hireDateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.hireDateTimePicker.Name = "hireDateTimePicker";
            this.hireDateTimePicker.Size = new System.Drawing.Size(250, 26);
            this.hireDateTimePicker.TabIndex = 36;
            this.hireDateTimePicker.ValueChanged += new System.EventHandler(this.hireDateTimePicker_ValueChanged);
            this.hireDateTimePicker.Enter += new System.EventHandler(this.DateTimePicker_Enter);
            // 
            // magNumberTextBox
            // 
            this.magNumberTextBox.BackColor = System.Drawing.Color.Transparent;
            this.magNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.magNumberTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.magNumberTextBox.ForeColor = System.Drawing.Color.Black;
            this.magNumberTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.magNumberTextBox.Location = new System.Drawing.Point(317, 533);
            this.magNumberTextBox.MinimumSize = new System.Drawing.Size(20, 20);
            this.magNumberTextBox.Name = "magNumberTextBox";
            this.magNumberTextBox.Size = new System.Drawing.Size(168, 26);
            this.magNumberTextBox.TabIndex = 38;
            this.magNumberTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // DOBDateTimePicker
            // 
            this.DOBDateTimePicker.CalendarFont = new System.Drawing.Font("Trebuchet MS", 12F);
            this.DOBDateTimePicker.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.DOBDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DOBDateTimePicker.Location = new System.Drawing.Point(317, 267);
            this.DOBDateTimePicker.Margin = new System.Windows.Forms.Padding(2);
            this.DOBDateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DOBDateTimePicker.Name = "DOBDateTimePicker";
            this.DOBDateTimePicker.Size = new System.Drawing.Size(250, 26);
            this.DOBDateTimePicker.TabIndex = 23;
            this.DOBDateTimePicker.ValueChanged += new System.EventHandler(this.DOBDateTimePicker_ValueChanged);
            this.DOBDateTimePicker.Enter += new System.EventHandler(this.DateTimePicker_Enter);
            // 
            // SwipCardImageButton
            // 
            this.SwipCardImageButton.BackColor = System.Drawing.Color.Transparent;
            this.SwipCardImageButton.FocusColor = System.Drawing.Color.Black;
            this.SwipCardImageButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.SwipCardImageButton.ForeColor = System.Drawing.Color.Black;
            this.SwipCardImageButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("SwipCardImageButton.ImageNormal")));
            this.SwipCardImageButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("SwipCardImageButton.ImagePressed")));
            this.SwipCardImageButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.SwipCardImageButton.Location = new System.Drawing.Point(490, 518);
            this.SwipCardImageButton.Margin = new System.Windows.Forms.Padding(2);
            this.SwipCardImageButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.SwipCardImageButton.Name = "SwipCardImageButton";
            this.SwipCardImageButton.RepeatRate = 150;
            this.SwipCardImageButton.RepeatWhenHeldFor = 750;
            this.SwipCardImageButton.Size = new System.Drawing.Size(77, 55);
            this.SwipCardImageButton.TabIndex = 39;
            this.SwipCardImageButton.Text = "S&wipe Card";
            this.SwipCardImageButton.UseVisualStyleBackColor = false;
            this.SwipCardImageButton.Click += new System.EventHandler(this.SwipCardImageButton_Click);
            // 
            // SSNTextBox
            // 
            this.SSNTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.SSNTextBox.Location = new System.Drawing.Point(28, 269);
            this.SSNTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.SSNTextBox.Name = "SSNTextBox";
            this.SSNTextBox.Size = new System.Drawing.Size(250, 26);
            this.SSNTextBox.TabIndex = 21;
            // 
            // verifiedPasswordTextBox
            // 
            this.verifiedPasswordTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.verifiedPasswordTextBox.Location = new System.Drawing.Point(317, 463);
            this.verifiedPasswordTextBox.MaxLength = 255;
            this.verifiedPasswordTextBox.Name = "verifiedPasswordTextBox";
            this.verifiedPasswordTextBox.PasswordChar = '*';
            this.verifiedPasswordTextBox.Size = new System.Drawing.Size(250, 26);
            this.verifiedPasswordTextBox.TabIndex = 32;
            // 
            // magLabel
            // 
            this.magLabel.AutoSize = true;
            this.magLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.magLabel.Location = new System.Drawing.Point(313, 505);
            this.magLabel.Name = "magLabel";
            this.magLabel.Size = new System.Drawing.Size(150, 22);
            this.magLabel.TabIndex = 37;
            this.magLabel.Text = "Mag. Card Number";
            // 
            // dateBirthlabel
            // 
            this.dateBirthlabel.AutoSize = true;
            this.dateBirthlabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.dateBirthlabel.Location = new System.Drawing.Point(313, 243);
            this.dateBirthlabel.Name = "dateBirthlabel";
            this.dateBirthlabel.Size = new System.Drawing.Size(107, 22);
            this.dateBirthlabel.TabIndex = 22;
            this.dateBirthlabel.Text = "Date of Birth";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(313, 438);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 22);
            this.label3.TabIndex = 31;
            this.label3.Text = "Re-Enter Password*";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.passwordTextBox.Location = new System.Drawing.Point(317, 400);
            this.passwordTextBox.MaxLength = 255;
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(250, 26);
            this.passwordTextBox.TabIndex = 30;
            // 
            // assignPositionButton
            // 
            this.assignPositionButton.FocusColor = System.Drawing.Color.Black;
            this.assignPositionButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.assignPositionButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.assignPositionButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("assignPositionButton.ImageNormal")));
            this.assignPositionButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("assignPositionButton.ImagePressed")));
            this.assignPositionButton.Location = new System.Drawing.Point(28, 396);
            this.assignPositionButton.Margin = new System.Windows.Forms.Padding(0);
            this.assignPositionButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.assignPositionButton.Name = "assignPositionButton";
            this.assignPositionButton.RepeatRate = 150;
            this.assignPositionButton.RepeatWhenHeldFor = 750;
            this.assignPositionButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.assignPositionButton.Size = new System.Drawing.Size(250, 30);
            this.assignPositionButton.TabIndex = 26;
            this.assignPositionButton.Text = "Assign Positions...";
            this.assignPositionButton.Click += new System.EventHandler(this.assignPositionButton_Click);
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.passwordLabel.Location = new System.Drawing.Point(313, 375);
            this.passwordLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(89, 22);
            this.passwordLabel.TabIndex = 29;
            this.passwordLabel.Text = "Password*";
            // 
            // positionListBox
            // 
            this.positionListBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.positionListBox.FormattingEnabled = true;
            this.positionListBox.ItemHeight = 22;
            this.positionListBox.Location = new System.Drawing.Point(28, 327);
            this.positionListBox.Margin = new System.Windows.Forms.Padding(0);
            this.positionListBox.Name = "positionListBox";
            this.positionListBox.Size = new System.Drawing.Size(250, 70);
            this.positionListBox.TabIndex = 25;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(24, 303);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 22);
            this.label4.TabIndex = 24;
            this.label4.Text = "Assigned Positions";
            // 
            // otherPhoneTextBox
            // 
            this.otherPhoneTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.otherPhoneTextBox.Location = new System.Drawing.Point(317, 210);
            this.otherPhoneTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.otherPhoneTextBox.Name = "otherPhoneTextBox";
            this.otherPhoneTextBox.Size = new System.Drawing.Size(250, 26);
            this.otherPhoneTextBox.TabIndex = 19;
            this.otherPhoneTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            // 
            // SSNlabel
            // 
            this.SSNlabel.AutoSize = true;
            this.SSNlabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.SSNlabel.Location = new System.Drawing.Point(24, 243);
            this.SSNlabel.Name = "SSNlabel";
            this.SSNlabel.Size = new System.Drawing.Size(216, 22);
            this.SSNlabel.TabIndex = 20;
            this.SSNlabel.Text = "Government Issued Number";
            // 
            // homePhoneTextBox
            // 
            this.homePhoneTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.homePhoneTextBox.Location = new System.Drawing.Point(28, 210);
            this.homePhoneTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.homePhoneTextBox.Name = "homePhoneTextBox";
            this.homePhoneTextBox.Size = new System.Drawing.Size(250, 26);
            this.homePhoneTextBox.TabIndex = 17;
            this.homePhoneTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            // 
            // loginNumericUpDown
            // 
            this.loginNumericUpDown.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.loginNumericUpDown.Location = new System.Drawing.Point(317, 327);
            this.loginNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.loginNumericUpDown.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.loginNumericUpDown.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.loginNumericUpDown.Name = "loginNumericUpDown";
            this.loginNumericUpDown.Size = new System.Drawing.Size(250, 26);
            this.loginNumericUpDown.TabIndex = 28;
            this.loginNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.loginNumericUpDown.ValueChanged += new System.EventHandler(this.loginNumericUpDown_ValueChanged);
            // 
            // fNamelabel
            // 
            this.fNamelabel.AutoSize = true;
            this.fNamelabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.fNamelabel.Location = new System.Drawing.Point(24, 22);
            this.fNamelabel.Name = "fNamelabel";
            this.fNamelabel.Size = new System.Drawing.Size(99, 22);
            this.fNamelabel.TabIndex = 0;
            this.fNamelabel.Text = "First Name*";
            // 
            // zipTextBox
            // 
            this.zipTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.zipTextBox.Location = new System.Drawing.Point(317, 157);
            this.zipTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.zipTextBox.Name = "zipTextBox";
            this.zipTextBox.Size = new System.Drawing.Size(125, 26);
            this.zipTextBox.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(313, 186);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 22);
            this.label2.TabIndex = 18;
            this.label2.Text = "Phone 2";
            // 
            // loginNumberLabel
            // 
            this.loginNumberLabel.AutoSize = true;
            this.loginNumberLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.loginNumberLabel.Location = new System.Drawing.Point(313, 303);
            this.loginNumberLabel.Name = "loginNumberLabel";
            this.loginNumberLabel.Size = new System.Drawing.Size(123, 22);
            this.loginNumberLabel.TabIndex = 27;
            this.loginNumberLabel.Text = "Login Number*";
            // 
            // countryTextBox
            // 
            this.countryTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.countryTextBox.Location = new System.Drawing.Point(447, 157);
            this.countryTextBox.Name = "countryTextBox";
            this.countryTextBox.Size = new System.Drawing.Size(120, 26);
            this.countryTextBox.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(24, 186);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 22);
            this.label1.TabIndex = 16;
            this.label1.Text = "Phone 1";
            // 
            // firstNameTextBox
            // 
            this.firstNameTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.firstNameTextBox.Location = new System.Drawing.Point(28, 46);
            this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.firstNameTextBox.Name = "firstNameTextBox";
            this.firstNameTextBox.Size = new System.Drawing.Size(250, 26);
            this.firstNameTextBox.TabIndex = 1;
            // 
            // countrylabel
            // 
            this.countrylabel.AutoSize = true;
            this.countrylabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.countrylabel.Location = new System.Drawing.Point(443, 133);
            this.countrylabel.Name = "countrylabel";
            this.countrylabel.Size = new System.Drawing.Size(69, 22);
            this.countrylabel.TabIndex = 14;
            this.countrylabel.Text = "Country";
            // 
            // lNamelabel
            // 
            this.lNamelabel.AutoSize = true;
            this.lNamelabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.lNamelabel.Location = new System.Drawing.Point(313, 22);
            this.lNamelabel.Name = "lNamelabel";
            this.lNamelabel.Size = new System.Drawing.Size(96, 22);
            this.lNamelabel.TabIndex = 2;
            this.lNamelabel.Text = "Last Name*";
            // 
            // lastNameTextBox
            // 
            this.lastNameTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.lastNameTextBox.Location = new System.Drawing.Point(317, 46);
            this.lastNameTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.lastNameTextBox.Name = "lastNameTextBox";
            this.lastNameTextBox.Size = new System.Drawing.Size(250, 26);
            this.lastNameTextBox.TabIndex = 3;
            // 
            // address1label
            // 
            this.address1label.AutoSize = true;
            this.address1label.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.address1label.Location = new System.Drawing.Point(24, 78);
            this.address1label.Name = "address1label";
            this.address1label.Size = new System.Drawing.Size(83, 22);
            this.address1label.TabIndex = 4;
            this.address1label.Text = "Address 1";
            // 
            // ziplabel
            // 
            this.ziplabel.AutoSize = true;
            this.ziplabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.ziplabel.Location = new System.Drawing.Point(313, 133);
            this.ziplabel.Name = "ziplabel";
            this.ziplabel.Size = new System.Drawing.Size(76, 22);
            this.ziplabel.TabIndex = 12;
            this.ziplabel.Text = "Zip Code";
            // 
            // adress1TextBox
            // 
            this.adress1TextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.adress1TextBox.Location = new System.Drawing.Point(28, 103);
            this.adress1TextBox.Name = "adress1TextBox";
            this.adress1TextBox.Size = new System.Drawing.Size(250, 26);
            this.adress1TextBox.TabIndex = 5;
            // 
            // stateTextBox
            // 
            this.stateTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.stateTextBox.Location = new System.Drawing.Point(173, 157);
            this.stateTextBox.Name = "stateTextBox";
            this.stateTextBox.Size = new System.Drawing.Size(105, 26);
            this.stateTextBox.TabIndex = 11;
            // 
            // adress2label
            // 
            this.adress2label.AutoSize = true;
            this.adress2label.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.adress2label.Location = new System.Drawing.Point(313, 78);
            this.adress2label.Name = "adress2label";
            this.adress2label.Size = new System.Drawing.Size(83, 22);
            this.adress2label.TabIndex = 6;
            this.adress2label.Text = "Address 2";
            // 
            // statelabel
            // 
            this.statelabel.AutoSize = true;
            this.statelabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.statelabel.Location = new System.Drawing.Point(171, 132);
            this.statelabel.Name = "statelabel";
            this.statelabel.Size = new System.Drawing.Size(48, 22);
            this.statelabel.TabIndex = 10;
            this.statelabel.Text = "State";
            // 
            // adress2TextBox
            // 
            this.adress2TextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.adress2TextBox.Location = new System.Drawing.Point(317, 103);
            this.adress2TextBox.Name = "adress2TextBox";
            this.adress2TextBox.Size = new System.Drawing.Size(250, 26);
            this.adress2TextBox.TabIndex = 7;
            // 
            // cityTextBox
            // 
            this.cityTextBox.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.cityTextBox.Location = new System.Drawing.Point(28, 157);
            this.cityTextBox.Name = "cityTextBox";
            this.cityTextBox.Size = new System.Drawing.Size(139, 26);
            this.cityTextBox.TabIndex = 9;
            // 
            // citylabel
            // 
            this.citylabel.AutoSize = true;
            this.citylabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold);
            this.citylabel.Location = new System.Drawing.Point(24, 132);
            this.citylabel.Name = "citylabel";
            this.citylabel.Size = new System.Drawing.Size(39, 22);
            this.citylabel.TabIndex = 8;
            this.citylabel.Text = "City";
            // 
            // newSTaffImageButton
            // 
            this.newSTaffImageButton.BackColor = System.Drawing.Color.Transparent;
            this.newSTaffImageButton.FocusColor = System.Drawing.Color.Black;
            this.newSTaffImageButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.newSTaffImageButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("newSTaffImageButton.ImageNormal")));
            this.newSTaffImageButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("newSTaffImageButton.ImagePressed")));
            this.newSTaffImageButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.newSTaffImageButton.Location = new System.Drawing.Point(578, 601);
            this.newSTaffImageButton.Margin = new System.Windows.Forms.Padding(2);
            this.newSTaffImageButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.newSTaffImageButton.Name = "newSTaffImageButton";
            this.newSTaffImageButton.RepeatRate = 150;
            this.newSTaffImageButton.RepeatWhenHeldFor = 750;
            this.newSTaffImageButton.Size = new System.Drawing.Size(103, 30);
            this.newSTaffImageButton.TabIndex = 2;
            this.newSTaffImageButton.Text = "&New";
            this.newSTaffImageButton.UseVisualStyleBackColor = false;
            this.newSTaffImageButton.Click += new System.EventHandler(this.newSTaffImageButton_Click);
            // 
            // saveStaffImageButton
            // 
            this.saveStaffImageButton.BackColor = System.Drawing.Color.Transparent;
            this.saveStaffImageButton.FocusColor = System.Drawing.Color.Black;
            this.saveStaffImageButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.saveStaffImageButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("saveStaffImageButton.ImageNormal")));
            this.saveStaffImageButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("saveStaffImageButton.ImagePressed")));
            this.saveStaffImageButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.saveStaffImageButton.Location = new System.Drawing.Point(720, 601);
            this.saveStaffImageButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveStaffImageButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.saveStaffImageButton.Name = "saveStaffImageButton";
            this.saveStaffImageButton.RepeatRate = 150;
            this.saveStaffImageButton.RepeatWhenHeldFor = 750;
            this.saveStaffImageButton.Size = new System.Drawing.Size(103, 30);
            this.saveStaffImageButton.TabIndex = 3;
            this.saveStaffImageButton.Text = "&Save";
            this.saveStaffImageButton.UseVisualStyleBackColor = false;
            this.saveStaffImageButton.Click += new System.EventHandler(this.saveStaffImageButton_Click);
            // 
            // mCloseButton
            // 
            this.mCloseButton.BackColor = System.Drawing.Color.Transparent;
            this.mCloseButton.FocusColor = System.Drawing.Color.Black;
            this.mCloseButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold);
            this.mCloseButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("mCloseButton.ImageNormal")));
            this.mCloseButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("mCloseButton.ImagePressed")));
            this.mCloseButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mCloseButton.Location = new System.Drawing.Point(867, 601);
            this.mCloseButton.Margin = new System.Windows.Forms.Padding(2);
            this.mCloseButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.mCloseButton.Name = "mCloseButton";
            this.mCloseButton.RepeatRate = 150;
            this.mCloseButton.RepeatWhenHeldFor = 750;
            this.mCloseButton.Size = new System.Drawing.Size(103, 30);
            this.mCloseButton.TabIndex = 4;
            this.mCloseButton.Text = "&Close";
            this.mCloseButton.UseVisualStyleBackColor = false;
            this.mCloseButton.Visible = false;
            this.mCloseButton.Click += new System.EventHandler(this.mCloseButton_Click);
            // 
            // NewStaff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1018, 643);
            this.ControlBox = false;
            this.Controls.Add(this.newSTaffImageButton);
            this.Controls.Add(this.saveStaffImageButton);
            this.Controls.Add(this.mCloseButton);
            this.Controls.Add(this.newStaffGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(208)))), ((int)(((byte)(201)))));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "NewStaff";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClose);
            this.Load += new System.EventHandler(this.NewStaff_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.newStaffGroupBox.ResumeLayout(false);
            this.newStaffGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.loginNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label psotionLabel1;
        private System.Windows.Forms.ComboBox positionComboBox;
        private GTI.Controls.GTIListView staffListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox newStaffGroupBox;
        private System.Windows.Forms.TextBox SSNTextBox;
        private System.Windows.Forms.TextBox otherPhoneTextBox;
        private System.Windows.Forms.TextBox homePhoneTextBox;
        private System.Windows.Forms.TextBox zipTextBox;
        private System.Windows.Forms.DateTimePicker DOBDateTimePicker;
        private System.Windows.Forms.TextBox lastNameTextBox;
        private System.Windows.Forms.TextBox firstNameTextBox;
        private System.Windows.Forms.TextBox countryTextBox;
        private System.Windows.Forms.Label countrylabel;
        private System.Windows.Forms.Label ziplabel;
        private System.Windows.Forms.TextBox stateTextBox;
        private System.Windows.Forms.Label statelabel;
        private System.Windows.Forms.TextBox cityTextBox;
        private System.Windows.Forms.Label citylabel;
        private System.Windows.Forms.TextBox adress2TextBox;
        private System.Windows.Forms.Label adress2label;
        private System.Windows.Forms.TextBox adress1TextBox;
        private System.Windows.Forms.Label address1label;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label SSNlabel;
        private System.Windows.Forms.Label dateBirthlabel;
        private System.Windows.Forms.Label lNamelabel;
        private System.Windows.Forms.Label fNamelabel;
        private GTI.Controls.ImageLabel magNumberTextBox;
        private GTI.Controls.ImageButton SwipCardImageButton;
        private System.Windows.Forms.TextBox verifiedPasswordTextBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox positionListBox;
        private GTI.Controls.ImageButton assignPositionButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.NumericUpDown loginNumericUpDown;
        private System.Windows.Forms.DateTimePicker hireDateTimePicker;
        private System.Windows.Forms.Label magLabel;
        private System.Windows.Forms.Label loginNumberLabel;
        private System.Windows.Forms.Label HireDateLabel;
        private GTI.Controls.ImageButton mCloseButton;
        private GTI.Controls.ImageButton saveStaffImageButton;
        private GTI.Controls.ImageButton newSTaffImageButton;
        private System.Windows.Forms.RadioButton inactiveRadioButton;
        private System.Windows.Forms.RadioButton allRadioButton;
        private System.Windows.Forms.RadioButton activeRadioButton;
        private System.Windows.Forms.CheckBox checkBoxActive;
        private System.Windows.Forms.CheckBox checkBoxlocked;
        private System.Windows.Forms.CheckBox checkBoxLeftHanded;
    }
}