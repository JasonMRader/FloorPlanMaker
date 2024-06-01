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
            tbarCocktail = new TrackBar();
            label3 = new Label();
            tbarClosing = new TrackBar();
            label4 = new Label();
            tbarOutside = new TrackBar();
            label5 = new Label();
            tbarTeamWait = new TrackBar();
            label6 = new Label();
            tbarSection = new TrackBar();
            label7 = new Label();
            lblCocktail = new Label();
            lblClosing = new Label();
            lblOutside = new Label();
            lblTeamWait = new Label();
            lblPerferedSections = new Label();
            lblServerName = new Label();
            ((System.ComponentModel.ISupportInitialize)tbarCocktail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbarClosing).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbarOutside).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbarTeamWait).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tbarSection).BeginInit();
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
            txtServerName.Location = new Point(12, 470);
            txtServerName.Name = "txtServerName";
            txtServerName.Size = new Size(247, 23);
            txtServerName.TabIndex = 2;
            // 
            // txtServerDisplayName
            // 
            txtServerDisplayName.Location = new Point(12, 530);
            txtServerDisplayName.Name = "txtServerDisplayName";
            txtServerDisplayName.Size = new Size(247, 23);
            txtServerDisplayName.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 452);
            label1.Name = "label1";
            label1.Size = new Size(42, 15);
            label1.TabIndex = 4;
            label1.Text = "Name:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 509);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 4;
            label2.Text = "Display Name:";
            // 
            // btnSaveServer
            // 
            btnSaveServer.Location = new Point(12, 590);
            btnSaveServer.Name = "btnSaveServer";
            btnSaveServer.Size = new Size(247, 23);
            btnSaveServer.TabIndex = 5;
            btnSaveServer.Text = "Save Changes";
            btnSaveServer.UseVisualStyleBackColor = true;
            btnSaveServer.Click += btnSaveServer_Click;
            // 
            // btnArchiveServer
            // 
            btnArchiveServer.Location = new Point(12, 619);
            btnArchiveServer.Name = "btnArchiveServer";
            btnArchiveServer.Size = new Size(247, 23);
            btnArchiveServer.TabIndex = 5;
            btnArchiveServer.Text = "Archive This Server";
            btnArchiveServer.UseVisualStyleBackColor = true;
            btnArchiveServer.Click += btnArchiveServer_Click;
            // 
            // btnSetDisplayToFirstName
            // 
            btnSetDisplayToFirstName.Location = new Point(12, 561);
            btnSetDisplayToFirstName.Name = "btnSetDisplayToFirstName";
            btnSetDisplayToFirstName.Size = new Size(247, 23);
            btnSetDisplayToFirstName.TabIndex = 5;
            btnSetDisplayToFirstName.Text = "Set Display To First Name";
            btnSetDisplayToFirstName.UseVisualStyleBackColor = true;
            btnSetDisplayToFirstName.Click += btnSetDisplayToFirstName_Click;
            // 
            // tbarCocktail
            // 
            tbarCocktail.Location = new Point(310, 116);
            tbarCocktail.Name = "tbarCocktail";
            tbarCocktail.Size = new Size(246, 45);
            tbarCocktail.TabIndex = 6;
            tbarCocktail.Scroll += tbarCocktail_Scroll;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(310, 80);
            label3.Name = "label3";
            label3.Size = new Size(109, 15);
            label3.TabIndex = 7;
            label3.Text = "Cocktail Preference";
            // 
            // tbarClosing
            // 
            tbarClosing.Location = new Point(310, 215);
            tbarClosing.Name = "tbarClosing";
            tbarClosing.Size = new Size(246, 45);
            tbarClosing.TabIndex = 6;
            tbarClosing.Scroll += tbarClosing_Scroll;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(310, 179);
            label4.Name = "label4";
            label4.Size = new Size(105, 15);
            label4.TabIndex = 7;
            label4.Text = "Closing Frequency";
            // 
            // tbarOutside
            // 
            tbarOutside.Location = new Point(310, 315);
            tbarOutside.Name = "tbarOutside";
            tbarOutside.Size = new Size(246, 45);
            tbarOutside.TabIndex = 6;
            tbarOutside.Scroll += tbarOutside_Scroll;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(310, 279);
            label5.Name = "label5";
            label5.Size = new Size(106, 15);
            label5.TabIndex = 7;
            label5.Text = "Outside Frequency";
            // 
            // tbarTeamWait
            // 
            tbarTeamWait.Location = new Point(310, 419);
            tbarTeamWait.Name = "tbarTeamWait";
            tbarTeamWait.Size = new Size(246, 45);
            tbarTeamWait.TabIndex = 6;
            tbarTeamWait.Scroll += tbarTeamWait_Scroll;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(310, 383);
            label6.Name = "label6";
            label6.Size = new Size(121, 15);
            label6.TabIndex = 7;
            label6.Text = "Team Wait Preference";
            // 
            // tbarSection
            // 
            tbarSection.Location = new Point(310, 526);
            tbarSection.Name = "tbarSection";
            tbarSection.Size = new Size(246, 45);
            tbarSection.TabIndex = 6;
            tbarSection.Scroll += tbarSection_Scroll;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(310, 490);
            label7.Name = "label7";
            label7.Size = new Size(134, 15);
            label7.TabIndex = 7;
            label7.Text = "Prefered Section Weight";
            // 
            // lblCocktail
            // 
            lblCocktail.AutoSize = true;
            lblCocktail.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblCocktail.Location = new Point(562, 116);
            lblCocktail.Name = "lblCocktail";
            lblCocktail.Size = new Size(23, 25);
            lblCocktail.TabIndex = 8;
            lblCocktail.Text = "0";
            // 
            // lblClosing
            // 
            lblClosing.AutoSize = true;
            lblClosing.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblClosing.Location = new Point(562, 215);
            lblClosing.Name = "lblClosing";
            lblClosing.Size = new Size(23, 25);
            lblClosing.TabIndex = 8;
            lblClosing.Text = "0";
            // 
            // lblOutside
            // 
            lblOutside.AutoSize = true;
            lblOutside.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblOutside.Location = new Point(562, 315);
            lblOutside.Name = "lblOutside";
            lblOutside.Size = new Size(23, 25);
            lblOutside.TabIndex = 8;
            lblOutside.Text = "0";
            // 
            // lblTeamWait
            // 
            lblTeamWait.AutoSize = true;
            lblTeamWait.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblTeamWait.Location = new Point(562, 419);
            lblTeamWait.Name = "lblTeamWait";
            lblTeamWait.Size = new Size(23, 25);
            lblTeamWait.TabIndex = 8;
            lblTeamWait.Text = "0";
            // 
            // lblPerferedSections
            // 
            lblPerferedSections.AutoSize = true;
            lblPerferedSections.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblPerferedSections.Location = new Point(562, 526);
            lblPerferedSections.Name = "lblPerferedSections";
            lblPerferedSections.Size = new Size(23, 25);
            lblPerferedSections.TabIndex = 8;
            lblPerferedSections.Text = "0";
            // 
            // lblServerName
            // 
            lblServerName.AutoSize = true;
            lblServerName.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerName.Location = new Point(399, 12);
            lblServerName.Name = "lblServerName";
            lblServerName.Size = new Size(75, 30);
            lblServerName.TabIndex = 9;
            lblServerName.Text = "Server";
            // 
            // frmAddRemoveStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(753, 663);
            Controls.Add(lblServerName);
            Controls.Add(lblPerferedSections);
            Controls.Add(lblTeamWait);
            Controls.Add(lblOutside);
            Controls.Add(lblClosing);
            Controls.Add(lblCocktail);
            Controls.Add(label7);
            Controls.Add(tbarSection);
            Controls.Add(label6);
            Controls.Add(tbarTeamWait);
            Controls.Add(label5);
            Controls.Add(tbarOutside);
            Controls.Add(label4);
            Controls.Add(tbarClosing);
            Controls.Add(label3);
            Controls.Add(tbarCocktail);
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
            ((System.ComponentModel.ISupportInitialize)tbarCocktail).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbarClosing).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbarOutside).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbarTeamWait).EndInit();
            ((System.ComponentModel.ISupportInitialize)tbarSection).EndInit();
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
        private TrackBar tbarCocktail;
        private Label label3;
        private TrackBar tbarClosing;
        private Label label4;
        private TrackBar tbarOutside;
        private Label label5;
        private TrackBar tbarTeamWait;
        private Label label6;
        private TrackBar tbarSection;
        private Label label7;
        private Label lblCocktail;
        private Label lblClosing;
        private Label lblOutside;
        private Label lblTeamWait;
        private Label lblPerferedSections;
        private Label lblServerName;
    }
}