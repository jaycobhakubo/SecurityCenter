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
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // inactiveRadioButton
            // 
            resources.ApplyResources(this.inactiveRadioButton, "inactiveRadioButton");
            this.inactiveRadioButton.Name = "inactiveRadioButton";
            this.inactiveRadioButton.UseVisualStyleBackColor = true;
            this.inactiveRadioButton.CheckedChanged += new System.EventHandler(this.inactiveRadioButton_CheckedChanged);
            // 
            // allRadioButton
            // 
            resources.ApplyResources(this.allRadioButton, "allRadioButton");
            this.allRadioButton.Name = "allRadioButton";
            this.allRadioButton.TabStop = true;
            this.allRadioButton.UseVisualStyleBackColor = true;
            this.allRadioButton.CheckedChanged += new System.EventHandler(this.allRadioButton_CheckedChanged);
            // 
            // activeRadioButton
            // 
            resources.ApplyResources(this.activeRadioButton, "activeRadioButton");
            this.activeRadioButton.Checked = true;
            this.activeRadioButton.Name = "activeRadioButton";
            this.activeRadioButton.TabStop = true;
            this.activeRadioButton.UseVisualStyleBackColor = true;
            this.activeRadioButton.CheckedChanged += new System.EventHandler(this.activeRadioButton_CheckedChanged);
            // 
            // positionComboBox
            // 
            this.positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.positionComboBox.FormattingEnabled = true;
            this.positionComboBox.Items.AddRange(new object[] {
            resources.GetString("positionComboBox.Items")});
            resources.ApplyResources(this.positionComboBox, "positionComboBox");
            this.positionComboBox.Name = "positionComboBox";
            this.positionComboBox.SelectedIndexChanged += new System.EventHandler(this.positionComboBox_SelectedIndexChanged);
            // 
            // psotionLabel1
            // 
            resources.ApplyResources(this.psotionLabel1, "psotionLabel1");
            this.psotionLabel1.Name = "psotionLabel1";
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
            resources.ApplyResources(this.staffListView, "staffListView");
            this.staffListView.MultiSelect = false;
            this.staffListView.Name = "staffListView";
            this.staffListView.OwnerDraw = true;
            this.staffListView.SortColumn = 0;
            this.staffListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.staffListView.UseCompatibleStateImageBehavior = false;
            this.staffListView.View = System.Windows.Forms.View.Details;
            this.staffListView.SelectedIndexChanged += new System.EventHandler(this.staffListView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "numeric";
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "alpha";
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "alpha";
            resources.ApplyResources(this.columnHeader3, "columnHeader3");
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
            resources.ApplyResources(this.newStaffGroupBox, "newStaffGroupBox");
            this.newStaffGroupBox.Name = "newStaffGroupBox";
            this.newStaffGroupBox.TabStop = false;
            // 
            // checkBoxLeftHanded
            // 
            resources.ApplyResources(this.checkBoxLeftHanded, "checkBoxLeftHanded");
            this.checkBoxLeftHanded.Name = "checkBoxLeftHanded";
            this.checkBoxLeftHanded.UseVisualStyleBackColor = true;
            // 
            // checkBoxlocked
            // 
            resources.ApplyResources(this.checkBoxlocked, "checkBoxlocked");
            this.checkBoxlocked.Name = "checkBoxlocked";
            this.checkBoxlocked.UseVisualStyleBackColor = true;
            // 
            // checkBoxActive
            // 
            resources.ApplyResources(this.checkBoxActive, "checkBoxActive");
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // HireDateLabel
            // 
            resources.ApplyResources(this.HireDateLabel, "HireDateLabel");
            this.HireDateLabel.Name = "HireDateLabel";
            // 
            // hireDateTimePicker
            // 
            resources.ApplyResources(this.hireDateTimePicker, "hireDateTimePicker");
            this.hireDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.hireDateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.hireDateTimePicker.Name = "hireDateTimePicker";
            this.hireDateTimePicker.ValueChanged += new System.EventHandler(this.hireDateTimePicker_ValueChanged);
            this.hireDateTimePicker.Enter += new System.EventHandler(this.DateTimePicker_Enter);
            // 
            // magNumberTextBox
            // 
            this.magNumberTextBox.BackColor = System.Drawing.Color.Transparent;
            this.magNumberTextBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.magNumberTextBox, "magNumberTextBox");
            this.magNumberTextBox.ForeColor = System.Drawing.Color.Black;
            this.magNumberTextBox.Name = "magNumberTextBox";
            // 
            // DOBDateTimePicker
            // 
            resources.ApplyResources(this.DOBDateTimePicker, "DOBDateTimePicker");
            this.DOBDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.DOBDateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.DOBDateTimePicker.Name = "DOBDateTimePicker";
            this.DOBDateTimePicker.ValueChanged += new System.EventHandler(this.DOBDateTimePicker_ValueChanged);
            this.DOBDateTimePicker.Enter += new System.EventHandler(this.DateTimePicker_Enter);
            // 
            // SwipCardImageButton
            // 
            this.SwipCardImageButton.BackColor = System.Drawing.Color.Transparent;
            this.SwipCardImageButton.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.SwipCardImageButton, "SwipCardImageButton");
            this.SwipCardImageButton.ForeColor = System.Drawing.Color.Black;
            this.SwipCardImageButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("SwipCardImageButton.ImageNormal")));
            this.SwipCardImageButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("SwipCardImageButton.ImagePressed")));
            this.SwipCardImageButton.Name = "SwipCardImageButton";
            this.SwipCardImageButton.RepeatRate = 150;
            this.SwipCardImageButton.RepeatWhenHeldFor = 750;
            this.SwipCardImageButton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.SwipCardImageButton.UseVisualStyleBackColor = false;
            this.SwipCardImageButton.Click += new System.EventHandler(this.SwipCardImageButton_Click);
            // 
            // SSNTextBox
            // 
            resources.ApplyResources(this.SSNTextBox, "SSNTextBox");
            this.SSNTextBox.Name = "SSNTextBox";
            // 
            // verifiedPasswordTextBox
            // 
            resources.ApplyResources(this.verifiedPasswordTextBox, "verifiedPasswordTextBox");
            this.verifiedPasswordTextBox.Name = "verifiedPasswordTextBox";
            // 
            // magLabel
            // 
            resources.ApplyResources(this.magLabel, "magLabel");
            this.magLabel.Name = "magLabel";
            // 
            // dateBirthlabel
            // 
            resources.ApplyResources(this.dateBirthlabel, "dateBirthlabel");
            this.dateBirthlabel.Name = "dateBirthlabel";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // passwordTextBox
            // 
            resources.ApplyResources(this.passwordTextBox, "passwordTextBox");
            this.passwordTextBox.Name = "passwordTextBox";
            // 
            // assignPositionButton
            // 
            this.assignPositionButton.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.assignPositionButton, "assignPositionButton");
            this.assignPositionButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.assignPositionButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("assignPositionButton.ImageNormal")));
            this.assignPositionButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("assignPositionButton.ImagePressed")));
            this.assignPositionButton.Name = "assignPositionButton";
            this.assignPositionButton.RepeatRate = 150;
            this.assignPositionButton.RepeatWhenHeldFor = 750;
            this.assignPositionButton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.assignPositionButton.Click += new System.EventHandler(this.assignPositionButton_Click);
            // 
            // passwordLabel
            // 
            resources.ApplyResources(this.passwordLabel, "passwordLabel");
            this.passwordLabel.Name = "passwordLabel";
            // 
            // positionListBox
            // 
            resources.ApplyResources(this.positionListBox, "positionListBox");
            this.positionListBox.FormattingEnabled = true;
            this.positionListBox.Name = "positionListBox";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // otherPhoneTextBox
            // 
            resources.ApplyResources(this.otherPhoneTextBox, "otherPhoneTextBox");
            this.otherPhoneTextBox.Name = "otherPhoneTextBox";
            this.otherPhoneTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            // 
            // SSNlabel
            // 
            resources.ApplyResources(this.SSNlabel, "SSNlabel");
            this.SSNlabel.Name = "SSNlabel";
            // 
            // homePhoneTextBox
            // 
            resources.ApplyResources(this.homePhoneTextBox, "homePhoneTextBox");
            this.homePhoneTextBox.Name = "homePhoneTextBox";
            this.homePhoneTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPhone_KeyPress);
            // 
            // loginNumericUpDown
            // 
            resources.ApplyResources(this.loginNumericUpDown, "loginNumericUpDown");
            this.loginNumericUpDown.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.loginNumericUpDown.Name = "loginNumericUpDown";
            this.loginNumericUpDown.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.loginNumericUpDown.ValueChanged += new System.EventHandler(this.loginNumericUpDown_ValueChanged);
            // 
            // fNamelabel
            // 
            resources.ApplyResources(this.fNamelabel, "fNamelabel");
            this.fNamelabel.Name = "fNamelabel";
            // 
            // zipTextBox
            // 
            resources.ApplyResources(this.zipTextBox, "zipTextBox");
            this.zipTextBox.Name = "zipTextBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // loginNumberLabel
            // 
            resources.ApplyResources(this.loginNumberLabel, "loginNumberLabel");
            this.loginNumberLabel.Name = "loginNumberLabel";
            // 
            // countryTextBox
            // 
            resources.ApplyResources(this.countryTextBox, "countryTextBox");
            this.countryTextBox.Name = "countryTextBox";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // firstNameTextBox
            // 
            resources.ApplyResources(this.firstNameTextBox, "firstNameTextBox");
            this.firstNameTextBox.Name = "firstNameTextBox";
            // 
            // countrylabel
            // 
            resources.ApplyResources(this.countrylabel, "countrylabel");
            this.countrylabel.Name = "countrylabel";
            // 
            // lNamelabel
            // 
            resources.ApplyResources(this.lNamelabel, "lNamelabel");
            this.lNamelabel.Name = "lNamelabel";
            // 
            // lastNameTextBox
            // 
            resources.ApplyResources(this.lastNameTextBox, "lastNameTextBox");
            this.lastNameTextBox.Name = "lastNameTextBox";
            // 
            // address1label
            // 
            resources.ApplyResources(this.address1label, "address1label");
            this.address1label.Name = "address1label";
            // 
            // ziplabel
            // 
            resources.ApplyResources(this.ziplabel, "ziplabel");
            this.ziplabel.Name = "ziplabel";
            // 
            // adress1TextBox
            // 
            resources.ApplyResources(this.adress1TextBox, "adress1TextBox");
            this.adress1TextBox.Name = "adress1TextBox";
            // 
            // stateTextBox
            // 
            resources.ApplyResources(this.stateTextBox, "stateTextBox");
            this.stateTextBox.Name = "stateTextBox";
            // 
            // adress2label
            // 
            resources.ApplyResources(this.adress2label, "adress2label");
            this.adress2label.Name = "adress2label";
            // 
            // statelabel
            // 
            resources.ApplyResources(this.statelabel, "statelabel");
            this.statelabel.Name = "statelabel";
            // 
            // adress2TextBox
            // 
            resources.ApplyResources(this.adress2TextBox, "adress2TextBox");
            this.adress2TextBox.Name = "adress2TextBox";
            // 
            // cityTextBox
            // 
            resources.ApplyResources(this.cityTextBox, "cityTextBox");
            this.cityTextBox.Name = "cityTextBox";
            // 
            // citylabel
            // 
            resources.ApplyResources(this.citylabel, "citylabel");
            this.citylabel.Name = "citylabel";
            // 
            // newSTaffImageButton
            // 
            this.newSTaffImageButton.BackColor = System.Drawing.Color.Transparent;
            this.newSTaffImageButton.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.newSTaffImageButton, "newSTaffImageButton");
            this.newSTaffImageButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("newSTaffImageButton.ImageNormal")));
            this.newSTaffImageButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("newSTaffImageButton.ImagePressed")));
            this.newSTaffImageButton.Name = "newSTaffImageButton";
            this.newSTaffImageButton.RepeatRate = 150;
            this.newSTaffImageButton.RepeatWhenHeldFor = 750;
            this.newSTaffImageButton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.newSTaffImageButton.UseVisualStyleBackColor = false;
            this.newSTaffImageButton.Click += new System.EventHandler(this.newSTaffImageButton_Click);
            // 
            // saveStaffImageButton
            // 
            this.saveStaffImageButton.BackColor = System.Drawing.Color.Transparent;
            this.saveStaffImageButton.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.saveStaffImageButton, "saveStaffImageButton");
            this.saveStaffImageButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("saveStaffImageButton.ImageNormal")));
            this.saveStaffImageButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("saveStaffImageButton.ImagePressed")));
            this.saveStaffImageButton.Name = "saveStaffImageButton";
            this.saveStaffImageButton.RepeatRate = 150;
            this.saveStaffImageButton.RepeatWhenHeldFor = 750;
            this.saveStaffImageButton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.saveStaffImageButton.UseVisualStyleBackColor = false;
            this.saveStaffImageButton.Click += new System.EventHandler(this.saveStaffImageButton_Click);
            // 
            // mCloseButton
            // 
            this.mCloseButton.BackColor = System.Drawing.Color.Transparent;
            this.mCloseButton.FocusColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.mCloseButton, "mCloseButton");
            this.mCloseButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("mCloseButton.ImageNormal")));
            this.mCloseButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("mCloseButton.ImagePressed")));
            this.mCloseButton.Name = "mCloseButton";
            this.mCloseButton.RepeatRate = 150;
            this.mCloseButton.RepeatWhenHeldFor = 750;
            this.mCloseButton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.mCloseButton.UseVisualStyleBackColor = false;
            this.mCloseButton.Click += new System.EventHandler(this.mCloseButton_Click);
            // 
            // NewStaff
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.ControlBox = false;
            this.Controls.Add(this.newSTaffImageButton);
            this.Controls.Add(this.saveStaffImageButton);
            this.Controls.Add(this.mCloseButton);
            this.Controls.Add(this.newStaffGroupBox);
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.GradientEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(208)))), ((int)(((byte)(201)))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewStaff";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
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