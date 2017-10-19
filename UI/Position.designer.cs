namespace GTI.Modules.SecurityCenter
{
    partial class Position
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Position));
            this.positionComboBox = new System.Windows.Forms.ComboBox();
            this.positionNameLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.positionActivityFlagCheckbox = new System.Windows.Forms.CheckBox();
            this.inactiveRadioButton = new System.Windows.Forms.RadioButton();
            this.allRadioButton = new System.Windows.Forms.RadioButton();
            this.activeRadioButton = new System.Windows.Forms.RadioButton();
            this.cancelbutton = new GTI.Controls.ImageButton();
            this.saveButton = new GTI.Controls.ImageButton();
            this.saveNewImageButton = new GTI.Controls.ImageButton();
            this.permissionGroupBox = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox1.SuspendLayout();
            this.permissionGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // positionComboBox
            // 
            this.positionComboBox.BackColor = System.Drawing.Color.White;
            this.positionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
            this.positionComboBox.ForeColor = System.Drawing.Color.Black;
            this.positionComboBox.FormattingEnabled = true;
            this.positionComboBox.IntegralHeight = false;
            this.positionComboBox.ItemHeight = 22;
            this.positionComboBox.Items.AddRange(new object[] {
            "New Position",
            "Sr. Engineer",
            "Engineer I",
            "Engineer II",
            "Engineer III"});
            this.positionComboBox.Location = new System.Drawing.Point(12, 73);
            this.positionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.positionComboBox.MaxLength = 100;
            this.positionComboBox.Name = "positionComboBox";
            this.positionComboBox.Size = new System.Drawing.Size(358, 31);
            this.positionComboBox.TabIndex = 1;
            // 
            // positionNameLabel
            // 
            this.positionNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.positionNameLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.positionNameLabel.ForeColor = System.Drawing.Color.Black;
            this.positionNameLabel.Location = new System.Drawing.Point(3, 13);
            this.positionNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.positionNameLabel.Name = "positionNameLabel";
            this.positionNameLabel.Size = new System.Drawing.Size(117, 19);
            this.positionNameLabel.TabIndex = 0;
            this.positionNameLabel.Text = "Position Name:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.positionActivityFlagCheckbox);
            this.groupBox1.Controls.Add(this.inactiveRadioButton);
            this.groupBox1.Controls.Add(this.positionNameLabel);
            this.groupBox1.Controls.Add(this.allRadioButton);
            this.groupBox1.Controls.Add(this.positionComboBox);
            this.groupBox1.Controls.Add(this.activeRadioButton);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(255, 9);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(0);
            this.groupBox1.Size = new System.Drawing.Size(507, 115);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // positionActivityFlagCheckbox
            // 
            this.positionActivityFlagCheckbox.AutoSize = true;
            this.positionActivityFlagCheckbox.BackColor = System.Drawing.Color.Transparent;
            this.positionActivityFlagCheckbox.Location = new System.Drawing.Point(405, 75);
            this.positionActivityFlagCheckbox.Name = "positionActivityFlagCheckbox";
            this.positionActivityFlagCheckbox.Size = new System.Drawing.Size(75, 26);
            this.positionActivityFlagCheckbox.TabIndex = 5;
            this.positionActivityFlagCheckbox.Text = "Active";
            this.positionActivityFlagCheckbox.UseVisualStyleBackColor = false;
            this.positionActivityFlagCheckbox.Click += new System.EventHandler(this.positionActivityFlagCheckbox_CheckedChanged);
            // 
            // inactiveRadioButton
            // 
            this.inactiveRadioButton.AutoSize = true;
            this.inactiveRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.inactiveRadioButton.Location = new System.Drawing.Point(210, 39);
            this.inactiveRadioButton.Name = "inactiveRadioButton";
            this.inactiveRadioButton.Size = new System.Drawing.Size(85, 26);
            this.inactiveRadioButton.TabIndex = 8;
            this.inactiveRadioButton.Text = "Inactive";
            this.inactiveRadioButton.UseVisualStyleBackColor = true;
            this.inactiveRadioButton.CheckedChanged += new System.EventHandler(this.inactiveRadioButton_CheckedChanged);
            // 
            // allRadioButton
            // 
            this.allRadioButton.AutoSize = true;
            this.allRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.allRadioButton.Location = new System.Drawing.Point(124, 39);
            this.allRadioButton.Name = "allRadioButton";
            this.allRadioButton.Size = new System.Drawing.Size(45, 26);
            this.allRadioButton.TabIndex = 7;
            this.allRadioButton.TabStop = true;
            this.allRadioButton.Text = "All";
            this.allRadioButton.UseVisualStyleBackColor = true;
            this.allRadioButton.CheckedChanged += new System.EventHandler(this.allRadioButton_CheckedChanged);
            // 
            // activeRadioButton
            // 
            this.activeRadioButton.AutoSize = true;
            this.activeRadioButton.Checked = true;
            this.activeRadioButton.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.activeRadioButton.Location = new System.Drawing.Point(12, 39);
            this.activeRadioButton.Name = "activeRadioButton";
            this.activeRadioButton.Size = new System.Drawing.Size(74, 26);
            this.activeRadioButton.TabIndex = 6;
            this.activeRadioButton.TabStop = true;
            this.activeRadioButton.Text = "Active";
            this.activeRadioButton.UseVisualStyleBackColor = true;
            this.activeRadioButton.CheckedChanged += new System.EventHandler(this.activeRadioButton_CheckedChanged);
            // 
            // cancelbutton
            // 
            this.cancelbutton.BackColor = System.Drawing.Color.Transparent;
            this.cancelbutton.CausesValidation = false;
            this.cancelbutton.FocusColor = System.Drawing.Color.Black;
            this.cancelbutton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelbutton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("cancelbutton.ImageNormal")));
            this.cancelbutton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("cancelbutton.ImagePressed")));
            this.cancelbutton.Location = new System.Drawing.Point(854, 588);
            this.cancelbutton.MinimumSize = new System.Drawing.Size(30, 30);
            this.cancelbutton.Name = "cancelbutton";
            this.cancelbutton.RepeatRate = 150;
            this.cancelbutton.RepeatWhenHeldFor = 750;
            this.cancelbutton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.cancelbutton.Size = new System.Drawing.Size(132, 30);
            this.cancelbutton.TabIndex = 4;
            this.cancelbutton.Text = "&Cancel";
            this.cancelbutton.UseVisualStyleBackColor = false;
            // 
            // saveButton
            // 
            this.saveButton.BackColor = System.Drawing.Color.Transparent;
            this.saveButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.saveButton.FocusColor = System.Drawing.Color.Black;
            this.saveButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("saveButton.ImageNormal")));
            this.saveButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("saveButton.ImagePressed")));
            this.saveButton.Location = new System.Drawing.Point(707, 588);
            this.saveButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.saveButton.Name = "saveButton";
            this.saveButton.RepeatRate = 150;
            this.saveButton.RepeatWhenHeldFor = 750;
            this.saveButton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.saveButton.Size = new System.Drawing.Size(132, 30);
            this.saveButton.TabIndex = 3;
            this.saveButton.Text = "&Save";
            this.saveButton.UseVisualStyleBackColor = false;
            // 
            // saveNewImageButton
            // 
            this.saveNewImageButton.BackColor = System.Drawing.Color.Transparent;
            this.saveNewImageButton.DialogResult = System.Windows.Forms.DialogResult.Retry;
            this.saveNewImageButton.Enabled = false;
            this.saveNewImageButton.FocusColor = System.Drawing.Color.Black;
            this.saveNewImageButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveNewImageButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("saveNewImageButton.ImageNormal")));
            this.saveNewImageButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("saveNewImageButton.ImagePressed")));
            this.saveNewImageButton.Location = new System.Drawing.Point(560, 588);
            this.saveNewImageButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.saveNewImageButton.Name = "saveNewImageButton";
            this.saveNewImageButton.RepeatRate = 150;
            this.saveNewImageButton.RepeatWhenHeldFor = 750;
            this.saveNewImageButton.SecondaryTextPadding = new System.Windows.Forms.Padding(5);
            this.saveNewImageButton.Size = new System.Drawing.Size(132, 30);
            this.saveNewImageButton.TabIndex = 2;
            this.saveNewImageButton.Text = "Save and &New";
            this.saveNewImageButton.UseVisualStyleBackColor = false;
            this.saveNewImageButton.Visible = false;
            // 
            // permissionGroupBox
            // 
            this.permissionGroupBox.BackColor = System.Drawing.Color.Transparent;
            this.permissionGroupBox.Controls.Add(this.treeView1);
            this.permissionGroupBox.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.permissionGroupBox.ForeColor = System.Drawing.Color.Black;
            this.permissionGroupBox.Location = new System.Drawing.Point(23, 127);
            this.permissionGroupBox.Name = "permissionGroupBox";
            this.permissionGroupBox.Padding = new System.Windows.Forms.Padding(0);
            this.permissionGroupBox.Size = new System.Drawing.Size(964, 455);
            this.permissionGroupBox.TabIndex = 1;
            this.permissionGroupBox.TabStop = false;
            this.permissionGroupBox.Text = "Permissions";
            // 
            // treeView1
            // 
            this.treeView1.BackColor = System.Drawing.Color.White;
            this.treeView1.CheckBoxes = true;
            this.treeView1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ForeColor = System.Drawing.Color.Black;
            this.treeView1.Location = new System.Drawing.Point(12, 30);
            this.treeView1.Margin = new System.Windows.Forms.Padding(0);
            this.treeView1.Name = "treeView1";
            this.treeView1.PathSeparator = "-";
            this.treeView1.Size = new System.Drawing.Size(940, 412);
            this.treeView1.TabIndex = 0;
            // 
            // Position
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1016, 676);
            this.ControlBox = false;
            this.Controls.Add(this.permissionGroupBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.saveNewImageButton);
            this.Controls.Add(this.cancelbutton);
            this.Controls.Add(this.saveButton);
            this.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Position";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Position";
            this.Load += new System.EventHandler(this.Position_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.permissionGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox positionComboBox;
        private System.Windows.Forms.Label positionNameLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private GTI.Controls.ImageButton saveButton;
        private GTI.Controls.ImageButton cancelbutton;
        private GTI.Controls.ImageButton saveNewImageButton;
        private System.Windows.Forms.GroupBox permissionGroupBox;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.CheckBox positionActivityFlagCheckbox;
        private System.Windows.Forms.RadioButton inactiveRadioButton;
        private System.Windows.Forms.RadioButton allRadioButton;
        private System.Windows.Forms.RadioButton activeRadioButton;
    }
}