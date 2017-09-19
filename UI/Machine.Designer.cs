namespace GTI.Modules.SecurityCenter
{
    partial class MachineForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MachineForm));
            this.MachineListView = new GTI.Controls.GTIListView();
            this.MachineIDHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MachineClientIDHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.MachineLoginTimeHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.StaffNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OperatorNameHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMachineLogOut = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.contextMenuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MachineRefreshButton = new GTI.Controls.ImageButton();
            this.MachineLogoutButton = new GTI.Controls.ImageButton();
            this.contextMachineLogOut.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MachineListView
            // 
            this.MachineListView.AllowEraseBackground = true;
            this.MachineListView.BackColor = System.Drawing.SystemColors.Window;
            this.MachineListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MachineIDHeader,
            this.MachineClientIDHeader,
            this.MachineLoginTimeHeader,
            this.StaffNameHeader,
            this.OperatorNameHeader});
            this.MachineListView.ContextMenuStrip = this.contextMachineLogOut;
            this.MachineListView.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MachineListView.ForeColor = System.Drawing.SystemColors.WindowText;
            this.MachineListView.FullRowSelect = true;
            this.MachineListView.GridLines = true;
            this.MachineListView.HideSelection = false;
            this.MachineListView.Location = new System.Drawing.Point(82, 25);
            this.MachineListView.MultiSelect = false;
            this.MachineListView.Name = "MachineListView";
            this.MachineListView.OwnerDraw = true;
            this.MachineListView.Size = new System.Drawing.Size(835, 486);
            this.MachineListView.SortColumn = 0;
            this.MachineListView.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.MachineListView.TabIndex = 0;
            this.MachineListView.UseCompatibleStateImageBehavior = false;
            this.MachineListView.View = System.Windows.Forms.View.Details;
            this.MachineListView.SelectedIndexChanged += new System.EventHandler(this.MachineListView_SelectedIndexChanged);
            // 
            // MachineIDHeader
            // 
            this.MachineIDHeader.Tag = "";
            this.MachineIDHeader.Text = "Machine ID";
            this.MachineIDHeader.Width = 89;
            // 
            // MachineClientIDHeader
            // 
            this.MachineClientIDHeader.Tag = "";
            this.MachineClientIDHeader.Text = "Machine ClientID";
            this.MachineClientIDHeader.Width = 153;
            // 
            // MachineLoginTimeHeader
            // 
            this.MachineLoginTimeHeader.Tag = "";
            this.MachineLoginTimeHeader.Text = "Machine LoginTime";
            this.MachineLoginTimeHeader.Width = 206;
            // 
            // StaffNameHeader
            // 
            this.StaffNameHeader.Tag = "";
            this.StaffNameHeader.Text = "Staff Name";
            this.StaffNameHeader.Width = 189;
            // 
            // OperatorNameHeader
            // 
            this.OperatorNameHeader.Tag = "";
            this.OperatorNameHeader.Text = "Operator Name";
            this.OperatorNameHeader.Width = 194;
            // 
            // contextMachineLogOut
            // 
            this.contextMachineLogOut.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.contextMenuLogout});
            this.contextMachineLogOut.Name = "contextMachineLogOut";
            this.contextMachineLogOut.Size = new System.Drawing.Size(163, 26);
            // 
            // contextMenuLogout
            // 
            this.contextMenuLogout.Font = new System.Drawing.Font("Trebuchet MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuLogout.Name = "contextMenuLogout";
            this.contextMenuLogout.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            this.contextMenuLogout.Size = new System.Drawing.Size(162, 22);
            this.contextMenuLogout.Text = "Logout...";
            this.contextMenuLogout.Click += new System.EventHandler(this.MachineLogoutButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.MachineListView);
            this.groupBox1.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(992, 534);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Machine";
            // 
            // MachineRefreshButton
            // 
            this.MachineRefreshButton.BackColor = System.Drawing.Color.Transparent;
            this.MachineRefreshButton.FocusColor = System.Drawing.Color.Black;
            this.MachineRefreshButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MachineRefreshButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("MachineRefreshButton.ImageNormal")));
            this.MachineRefreshButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("MachineRefreshButton.ImagePressed")));
            this.MachineRefreshButton.Location = new System.Drawing.Point(522, 563);
            this.MachineRefreshButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.MachineRefreshButton.Name = "MachineRefreshButton";
            this.MachineRefreshButton.RepeatRate = 150;
            this.MachineRefreshButton.RepeatWhenHeldFor = 750;
            this.MachineRefreshButton.Size = new System.Drawing.Size(132, 30);
            this.MachineRefreshButton.TabIndex = 2;
            this.MachineRefreshButton.Text = "&Refresh";
            this.MachineRefreshButton.UseVisualStyleBackColor = false;
            this.MachineRefreshButton.Click += new System.EventHandler(this.MachineRefreshButton_Click);
            // 
            // MachineLogoutButton
            // 
            this.MachineLogoutButton.BackColor = System.Drawing.Color.Transparent;
            this.MachineLogoutButton.FocusColor = System.Drawing.Color.Black;
            this.MachineLogoutButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MachineLogoutButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.MachineLogoutButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("MachineLogoutButton.ImageNormal")));
            this.MachineLogoutButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("MachineLogoutButton.ImagePressed")));
            this.MachineLogoutButton.Location = new System.Drawing.Point(318, 563);
            this.MachineLogoutButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.MachineLogoutButton.Name = "MachineLogoutButton";
            this.MachineLogoutButton.RepeatRate = 150;
            this.MachineLogoutButton.RepeatWhenHeldFor = 750;
            this.MachineLogoutButton.Size = new System.Drawing.Size(132, 30);
            this.MachineLogoutButton.TabIndex = 3;
            this.MachineLogoutButton.Text = "&Logout";
            this.MachineLogoutButton.UseVisualStyleBackColor = false;
            this.MachineLogoutButton.Click += new System.EventHandler(this.MachineLogoutButton_Click);
            // 
            // MachineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1016, 676);
            this.ControlBox = false;
            this.Controls.Add(this.MachineLogoutButton);
            this.Controls.Add(this.MachineRefreshButton);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MachineForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Machine";
            this.contextMachineLogOut.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.GTIListView MachineListView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader MachineClientIDHeader;
        private System.Windows.Forms.ColumnHeader MachineLoginTimeHeader;
        private System.Windows.Forms.ColumnHeader StaffNameHeader;
        private System.Windows.Forms.ColumnHeader OperatorNameHeader;
        private Controls.ImageButton MachineRefreshButton;
        private Controls.ImageButton MachineLogoutButton;
        private System.Windows.Forms.ColumnHeader MachineIDHeader;
        private System.Windows.Forms.ContextMenuStrip contextMachineLogOut;
        private System.Windows.Forms.ToolStripMenuItem contextMenuLogout;
    }
}