namespace FloorPlanMakerUI
{
    partial class frmAddRemoveStaff
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
            lbServers = new ListBox();
            btnAddNewServer = new Button();
            txtNewServerName = new TextBox();
            rdoShowActive = new RadioButton();
            rdoShowArchived = new RadioButton();
            txtServerName = new TextBox();
            txtServerDisplayName = new TextBox();
            label1 = new Label();
            label2 = new Label();
            btnSaveServer = new Button();
            btnArchiveServer = new Button();
            btnSetDisplayToFirstName = new Button();
            SuspendLayout();
            // 
            // lbServers
            // 
            lbServers.FormattingEnabled = true;
            lbServers.ItemHeight = 15;
            lbServers.Location = new Point(12, 42);
            lbServers.Name = "lbServers";
            lbServers.Size = new Size(247, 349);
            lbServers.TabIndex = 0;
            lbServers.SelectedIndexChanged += lbServers_SelectedIndexChanged;
            // 
            // btnAddNewServer
            // 
            btnAddNewServer.Location = new Point(12, 426);
            btnAddNewServer.Name = "btnAddNewServer";
            btnAddNewServer.Size = new Size(247, 23);
            btnAddNewServer.TabIndex = 1;
            btnAddNewServer.Text = "Add New Server";
            btnAddNewServer.UseVisualStyleBackColor = true;
            btnAddNewServer.Click += btnAddNewServer_Click;
            // 
            // txtNewServerName
            // 
            txtNewServerName.Location = new Point(12, 397);
            txtNewServerName.Name = "txtNewServerName";
            txtNewServerName.Size = new Size(247, 23);
            txtNewServerName.TabIndex = 2;
            // 
            // rdoShowActive
            // 
            rdoShowActive.AutoSize = true;
            rdoShowActive.Checked = true;
            rdoShowActive.Location = new Point(12, 12);
            rdoShowActive.Name = "rdoShowActive";
            rdoShowActive.Size = new Size(90, 19);
            rdoShowActive.TabIndex = 3;
            rdoShowActive.TabStop = true;
            rdoShowActive.Text = "Show Active";
            rdoShowActive.UseVisualStyleBackColor = true;
            rdoShowActive.CheckedChanged += rdoShowActive_CheckedChanged;
            // 
            // rdoShowArchived
            // 
            rdoShowArchived.AutoSize = true;
            rdoShowArchived.Location = new Point(127, 12);
            rdoShowArchived.Name = "rdoShowArchived";
            rdoShowArchived.Size = new Size(104, 19);
            rdoShowArchived.TabIndex = 3;
            rdoShowArchived.Text = "Show Archived";
            rdoShowArchived.UseVisualStyleBackColor = true;
            rdoShowArchived.CheckedChanged += rdoShowArchived_CheckedChanged;
            // 
            // txtServerName
            // 
            txtServerName.Location = new Point(358, 42);
            txtServerName.Name = "txtServerName";
            txtServerName.Size = new Size(247, 23);
            txtServerName.TabIndex = 2;
            // 
            // txtServerDisplayName
            // 
            txtServerDisplayName.Location = new Point(358, 71);
            txtServerDisplayName.Name = "txtServerDisplayName";
            txtServerDisplayName.Size = new Size(247, 23);
            txtServerDisplayName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(306, 45);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 4;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(265, 74);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 4;
            label2.Text = "Display Name:";
            // 
            // btnSaveServer
            // 
            btnSaveServer.Location = new Point(358, 131);
            btnSaveServer.Name = "btnSaveServer";
            btnSaveServer.Size = new Size(247, 23);
            btnSaveServer.TabIndex = 5;
            btnSaveServer.Text = "Save Changes";
            btnSaveServer.UseVisualStyleBackColor = true;
            btnSaveServer.Click += btnSaveServer_Click;
            // 
            // btnArchiveServer
            // 
            btnArchiveServer.Location = new Point(358, 160);
            btnArchiveServer.Name = "btnArchiveServer";
            btnArchiveServer.Size = new Size(247, 23);
            btnArchiveServer.TabIndex = 5;
            btnArchiveServer.Text = "Archive This Server";
            btnArchiveServer.UseVisualStyleBackColor = true;
            btnArchiveServer.Click += btnArchiveServer_Click;
            // 
            // btnSetDisplayToFirstName
            // 
            btnSetDisplayToFirstName.Location = new Point(358, 102);
            btnSetDisplayToFirstName.Name = "btnSetDisplayToFirstName";
            btnSetDisplayToFirstName.Size = new Size(247, 23);
            btnSetDisplayToFirstName.TabIndex = 5;
            btnSetDisplayToFirstName.Text = "Set Display To First Name";
            btnSetDisplayToFirstName.UseVisualStyleBackColor = true;
            btnSetDisplayToFirstName.Click += btnSetDisplayToFirstName_Click;
            // 
            // frmAddRemoveStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(659, 497);
            Controls.Add(btnArchiveServer);
            Controls.Add(btnSetDisplayToFirstName);
            Controls.Add(btnSaveServer);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(rdoShowArchived);
            Controls.Add(rdoShowActive);
            Controls.Add(txtServerDisplayName);
            Controls.Add(txtServerName);
            Controls.Add(txtNewServerName);
            Controls.Add(btnAddNewServer);
            Controls.Add(lbServers);
            Name = "frmAddRemoveStaff";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmAddRemoveStaff";
            Load += frmAddRemoveStaff_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbServers;
        private Button btnAddNewServer;
        private TextBox txtNewServerName;
        private RadioButton rdoShowActive;
        private RadioButton rdoShowArchived;
        private TextBox txtServerName;
        private TextBox txtServerDisplayName;
        private Label label1;
        private Label label2;
        private Button btnSaveServer;
        private Button btnArchiveServer;
        private Button btnSetDisplayToFirstName;
    }
}