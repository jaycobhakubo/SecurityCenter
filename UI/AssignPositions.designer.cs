namespace GTI.Modules.SecurityCenter
{
    partial class AssignPositions
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "test1",
            "test2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("wanted");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
            "test1",
            "test2"}, -1);
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("wanted");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AssignPositions));
            this.currentPositionLabel = new System.Windows.Forms.Label();
            this.availablePositionlabel = new System.Windows.Forms.Label();
            this.availablePositionListBox = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.assignedPostionslistBox = new System.Windows.Forms.ListView();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.mCancelButton = new GTI.Controls.ImageButton();
            this.mOKButton = new GTI.Controls.ImageButton();
            this.removeButton = new GTI.Controls.ImageButton();
            this.assignButton = new GTI.Controls.ImageButton();
            this.SuspendLayout();
            // 
            // currentPositionLabel
            // 
            this.currentPositionLabel.AutoSize = true;
            this.currentPositionLabel.BackColor = System.Drawing.Color.Transparent;
            this.currentPositionLabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.currentPositionLabel.Location = new System.Drawing.Point(427, 39);
            this.currentPositionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.currentPositionLabel.Name = "currentPositionLabel";
            this.currentPositionLabel.Size = new System.Drawing.Size(149, 22);
            this.currentPositionLabel.TabIndex = 4;
            this.currentPositionLabel.Text = "Assigned Positions";
            // 
            // availablePositionlabel
            // 
            this.availablePositionlabel.AutoSize = true;
            this.availablePositionlabel.BackColor = System.Drawing.Color.Transparent;
            this.availablePositionlabel.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.availablePositionlabel.Location = new System.Drawing.Point(27, 39);
            this.availablePositionlabel.Name = "availablePositionlabel";
            this.availablePositionlabel.Size = new System.Drawing.Size(151, 22);
            this.availablePositionlabel.TabIndex = 0;
            this.availablePositionlabel.Text = "Available Positions";
            // 
            // availablePositionListBox
            // 
            this.availablePositionListBox.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.availablePositionListBox.AutoArrange = false;
            this.availablePositionListBox.BackColor = System.Drawing.Color.White;
            this.availablePositionListBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.availablePositionListBox.ForeColor = System.Drawing.Color.Black;
            this.availablePositionListBox.FullRowSelect = true;
            this.availablePositionListBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.availablePositionListBox.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.availablePositionListBox.Location = new System.Drawing.Point(29, 64);
            this.availablePositionListBox.Name = "availablePositionListBox";
            this.availablePositionListBox.Size = new System.Drawing.Size(280, 378);
            this.availablePositionListBox.TabIndex = 1;
            this.availablePositionListBox.UseCompatibleStateImageBehavior = false;
            this.availablePositionListBox.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 250;
            // 
            // assignedPostionslistBox
            // 
            this.assignedPostionslistBox.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.assignedPostionslistBox.AutoArrange = false;
            this.assignedPostionslistBox.BackColor = System.Drawing.Color.White;
            this.assignedPostionslistBox.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.assignedPostionslistBox.ForeColor = System.Drawing.Color.Black;
            this.assignedPostionslistBox.FullRowSelect = true;
            this.assignedPostionslistBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.assignedPostionslistBox.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem3,
            listViewItem4});
            this.assignedPostionslistBox.Location = new System.Drawing.Point(431, 64);
            this.assignedPostionslistBox.Name = "assignedPostionslistBox";
            this.assignedPostionslistBox.Size = new System.Drawing.Size(280, 378);
            this.assignedPostionslistBox.TabIndex = 5;
            this.assignedPostionslistBox.UseCompatibleStateImageBehavior = false;
            this.assignedPostionslistBox.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 250;
            // 
            // mCancelButton
            // 
            this.mCancelButton.BackColor = System.Drawing.Color.Transparent;
            this.mCancelButton.FocusColor = System.Drawing.Color.Black;
            this.mCancelButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mCancelButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("mCancelButton.ImageNormal")));
            this.mCancelButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("mCancelButton.ImagePressed")));
            this.mCancelButton.Location = new System.Drawing.Point(601, 460);
            this.mCancelButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.mCancelButton.Name = "mCancelButton";
            this.mCancelButton.Size = new System.Drawing.Size(110, 30);
            this.mCancelButton.TabIndex = 7;
            this.mCancelButton.Text = "&Cancel";
            this.mCancelButton.UseVisualStyleBackColor = false;
            // 
            // mOKButton
            // 
            this.mOKButton.BackColor = System.Drawing.Color.Transparent;
            this.mOKButton.FocusColor = System.Drawing.Color.Black;
            this.mOKButton.Font = new System.Drawing.Font("Trebuchet MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mOKButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("mOKButton.ImageNormal")));
            this.mOKButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("mOKButton.ImagePressed")));
            this.mOKButton.Location = new System.Drawing.Point(466, 460);
            this.mOKButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.mOKButton.Name = "mOKButton";
            this.mOKButton.Size = new System.Drawing.Size(110, 30);
            this.mOKButton.TabIndex = 6;
            this.mOKButton.Text = "&OK";
            this.mOKButton.UseVisualStyleBackColor = false;
            // 
            // removeButton
            // 
            this.removeButton.BackColor = System.Drawing.Color.Transparent;
            this.removeButton.FocusColor = System.Drawing.Color.Black;
            this.removeButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("removeButton.ImageNormal")));
            this.removeButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("removeButton.ImagePressed")));
            this.removeButton.Location = new System.Drawing.Point(315, 233);
            this.removeButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(110, 30);
            this.removeButton.TabIndex = 3;
            this.removeButton.Text = "<<";
            this.removeButton.UseVisualStyleBackColor = false;
            // 
            // assignButton
            // 
            this.assignButton.BackColor = System.Drawing.Color.Transparent;
            this.assignButton.FocusColor = System.Drawing.Color.Black;
            this.assignButton.Font = new System.Drawing.Font("Trebuchet MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assignButton.ImageNormal = ((System.Drawing.Image)(resources.GetObject("assignButton.ImageNormal")));
            this.assignButton.ImagePressed = ((System.Drawing.Image)(resources.GetObject("assignButton.ImagePressed")));
            this.assignButton.Location = new System.Drawing.Point(315, 197);
            this.assignButton.MinimumSize = new System.Drawing.Size(30, 30);
            this.assignButton.Name = "assignButton";
            this.assignButton.Size = new System.Drawing.Size(110, 30);
            this.assignButton.TabIndex = 2;
            this.assignButton.Text = ">>";
            this.assignButton.UseVisualStyleBackColor = false;
            // 
            // AssignPositions
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(178)))), ((int)(((byte)(213)))));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(739, 510);
            this.ControlBox = false;
            this.Controls.Add(this.assignedPostionslistBox);
            this.Controls.Add(this.availablePositionListBox);
            this.Controls.Add(this.mCancelButton);
            this.Controls.Add(this.mOKButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.assignButton);
            this.Controls.Add(this.availablePositionlabel);
            this.Controls.Add(this.currentPositionLabel);
            this.Font = new System.Drawing.Font("Trebuchet MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AssignPositions";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Assign Position";
            this.Load += new System.EventHandler(this.AssignPositions_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label currentPositionLabel;
        private System.Windows.Forms.Label availablePositionlabel;
        private GTI.Controls.ImageButton assignButton;
        private GTI.Controls.ImageButton removeButton;
        private GTI.Controls.ImageButton mCancelButton;
        private System.Windows.Forms.ListView availablePositionListBox;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView assignedPostionslistBox;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private GTI.Controls.ImageButton mOKButton;
    }
}