namespace FloorPlanMakerUI
{
    partial class frmGetHotSchedulesIDs
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
            rbMissingID = new RadioButton();
            rbHasID = new RadioButton();
            lblServerName = new Label();
            txtHSID = new TextBox();
            label1 = new Label();
            btnSaveEmployeeID = new Button();
            dgvHotSchedulesEmployees = new DataGridView();
            txtSearch = new TextBox();
            btnAutoAssignIDs = new Button();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgvHotSchedulesEmployees).BeginInit();
            SuspendLayout();
            // 
            // lbServers
            // 
            lbServers.FormattingEnabled = true;
            lbServers.ItemHeight = 15;
            lbServers.Location = new Point(12, 60);
            lbServers.Name = "lbServers";
            lbServers.Size = new Size(231, 589);
            lbServers.TabIndex = 0;
            lbServers.SelectedIndexChanged += lbServers_SelectedIndexChanged;
            // 
            // rbMissingID
            // 
            rbMissingID.AutoSize = true;
            rbMissingID.Checked = true;
            rbMissingID.Location = new Point(12, 35);
            rbMissingID.Name = "rbMissingID";
            rbMissingID.Size = new Size(80, 19);
            rbMissingID.TabIndex = 1;
            rbMissingID.TabStop = true;
            rbMissingID.Text = "Missing ID";
            rbMissingID.UseVisualStyleBackColor = true;
            rbMissingID.CheckedChanged += rbMissingID_CheckedChanged;
            // 
            // rbHasID
            // 
            rbHasID.AutoSize = true;
            rbHasID.Location = new Point(98, 35);
            rbHasID.Name = "rbHasID";
            rbHasID.Size = new Size(59, 19);
            rbHasID.TabIndex = 1;
            rbHasID.Text = "Has ID";
            rbHasID.UseVisualStyleBackColor = true;
            // 
            // lblServerName
            // 
            lblServerName.AutoSize = true;
            lblServerName.Font = new Font("Segoe UI", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerName.Location = new Point(338, 17);
            lblServerName.Name = "lblServerName";
            lblServerName.Size = new Size(100, 37);
            lblServerName.TabIndex = 2;
            lblServerName.Text = "Server";
            // 
            // txtHSID
            // 
            txtHSID.Location = new Point(333, 117);
            txtHSID.Name = "txtHSID";
            txtHSID.Size = new Size(125, 23);
            txtHSID.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(338, 92);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 4;
            label1.Text = "HotSchedules ID";
            // 
            // btnSaveEmployeeID
            // 
            btnSaveEmployeeID.Location = new Point(333, 159);
            btnSaveEmployeeID.Name = "btnSaveEmployeeID";
            btnSaveEmployeeID.Size = new Size(125, 23);
            btnSaveEmployeeID.TabIndex = 5;
            btnSaveEmployeeID.Text = "Save";
            btnSaveEmployeeID.UseVisualStyleBackColor = true;
            btnSaveEmployeeID.Click += btnSaveEmployeeID_Click;
            // 
            // dgvHotSchedulesEmployees
            // 
            dgvHotSchedulesEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHotSchedulesEmployees.Location = new Point(607, 92);
            dgvHotSchedulesEmployees.Name = "dgvHotSchedulesEmployees";
            dgvHotSchedulesEmployees.RowTemplate.Height = 25;
            dgvHotSchedulesEmployees.Size = new Size(343, 555);
            dgvHotSchedulesEmployees.TabIndex = 6;
            dgvHotSchedulesEmployees.CellContentClick += dgvHotSchedulesEmployees_CellContentClick;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(607, 58);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search";
            txtSearch.Size = new Size(343, 23);
            txtSearch.TabIndex = 7;
            // 
            // btnAutoAssignIDs
            // 
            btnAutoAssignIDs.Location = new Point(12, 6);
            btnAutoAssignIDs.Name = "btnAutoAssignIDs";
            btnAutoAssignIDs.Size = new Size(231, 23);
            btnAutoAssignIDs.TabIndex = 5;
            btnAutoAssignIDs.Text = "Auto Retrieve Server IDs";
            btnAutoAssignIDs.UseVisualStyleBackColor = true;
            btnAutoAssignIDs.Click += btnAutoAssignIDs_Click;
            // 
            // panel1
            // 
            panel1.Location = new Point(607, 10);
            panel1.Name = "panel1";
            panel1.Size = new Size(343, 44);
            panel1.TabIndex = 8;
            // 
            // frmGetHotSchedulesIDs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1021, 681);
            Controls.Add(panel1);
            Controls.Add(txtSearch);
            Controls.Add(dgvHotSchedulesEmployees);
            Controls.Add(btnAutoAssignIDs);
            Controls.Add(btnSaveEmployeeID);
            Controls.Add(label1);
            Controls.Add(txtHSID);
            Controls.Add(lblServerName);
            Controls.Add(rbHasID);
            Controls.Add(rbMissingID);
            Controls.Add(lbServers);
            Name = "frmGetHotSchedulesIDs";
            Text = "frmGetHotSchedulesIDs";
            Load += frmGetHotSchedulesIDs_Load;
            ((System.ComponentModel.ISupportInitialize)dgvHotSchedulesEmployees).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lbServers;
        private RadioButton rbMissingID;
        private RadioButton rbHasID;
        private Label lblServerName;
        private TextBox txtHSID;
        private Label label1;
        private Button btnSaveEmployeeID;
        private DataGridView dgvHotSchedulesEmployees;
        private TextBox txtSearch;
        private Button btnAutoAssignIDs;
        private Panel panel1;
    }
}