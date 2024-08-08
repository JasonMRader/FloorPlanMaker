namespace FloorPlanMakerUI
{
    partial class frmAddServer
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
            label1 = new Label();
            txtServerName = new TextBox();
            label2 = new Label();
            btnAddServer = new Button();
            btnDone = new Button();
            lbServersToAdd = new ListBox();
            btnCancel = new Button();
            label3 = new Label();
            panel1 = new Panel();
            panel3 = new Panel();
            panel2 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            label4 = new Label();
            lbMissingServerIDs = new ListBox();
            panel7 = new Panel();
            panel8 = new Panel();
            txtSearch = new TextBox();
            dgvHotSchedulesEmployees = new DataGridView();
            label5 = new Label();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHotSchedulesEmployees).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(86, 13);
            label1.Name = "label1";
            label1.Size = new Size(53, 21);
            label1.TabIndex = 0;
            label1.Text = "Name";
            // 
            // txtServerName
            // 
            txtServerName.Location = new Point(7, 50);
            txtServerName.Name = "txtServerName";
            txtServerName.Size = new Size(206, 23);
            txtServerName.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(226, 377);
            label2.Name = "label2";
            label2.Size = new Size(182, 30);
            label2.TabIndex = 0;
            label2.Text = "Add New Servers";
            // 
            // btnAddServer
            // 
            btnAddServer.BackColor = Color.FromArgb(100, 130, 180);
            btnAddServer.FlatAppearance.BorderSize = 0;
            btnAddServer.FlatStyle = FlatStyle.Flat;
            btnAddServer.Location = new Point(6, 95);
            btnAddServer.Name = "btnAddServer";
            btnAddServer.Size = new Size(207, 34);
            btnAddServer.TabIndex = 2;
            btnAddServer.Text = "Add More";
            btnAddServer.UseVisualStyleBackColor = false;
            btnAddServer.Click += btnAddServer_Click;
            // 
            // btnDone
            // 
            btnDone.BackColor = Color.FromArgb(120, 180, 120);
            btnDone.FlatAppearance.BorderSize = 0;
            btnDone.FlatStyle = FlatStyle.Flat;
            btnDone.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnDone.Location = new Point(25, 656);
            btnDone.Name = "btnDone";
            btnDone.Size = new Size(641, 36);
            btnDone.TabIndex = 2;
            btnDone.Text = "Save Servers";
            btnDone.UseVisualStyleBackColor = false;
            btnDone.Click += btnDone_Click;
            // 
            // lbServersToAdd
            // 
            lbServersToAdd.FormattingEnabled = true;
            lbServersToAdd.ItemHeight = 15;
            lbServersToAdd.Location = new Point(18, 24);
            lbServersToAdd.Name = "lbServersToAdd";
            lbServersToAdd.Size = new Size(286, 139);
            lbServersToAdd.TabIndex = 3;
            // 
            // btnCancel
            // 
            btnCancel.BackColor = Color.FromArgb(190, 80, 70);
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = FlatStyle.Flat;
            btnCancel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnCancel.Location = new Point(25, 696);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(641, 37);
            btnCancel.TabIndex = 4;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += btnCancel_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(117, 0);
            label3.Name = "label3";
            label3.Size = new Size(66, 21);
            label3.TabIndex = 0;
            label3.Text = "Servers";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(panel3);
            panel1.Location = new Point(25, 410);
            panel1.Name = "panel1";
            panel1.Size = new Size(266, 224);
            panel1.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Controls.Add(btnAddServer);
            panel3.Controls.Add(txtServerName);
            panel3.Controls.Add(label1);
            panel3.Location = new Point(13, 15);
            panel3.Name = "panel3";
            panel3.Size = new Size(226, 185);
            panel3.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(180, 190, 200);
            panel2.Controls.Add(panel4);
            panel2.Location = new Point(319, 410);
            panel2.Name = "panel2";
            panel2.Size = new Size(347, 224);
            panel2.TabIndex = 5;
            // 
            // panel4
            // 
            panel4.BackColor = Color.WhiteSmoke;
            panel4.Controls.Add(label3);
            panel4.Controls.Add(lbServersToAdd);
            panel4.Location = new Point(16, 15);
            panel4.Name = "panel4";
            panel4.Size = new Size(318, 185);
            panel4.TabIndex = 0;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(180, 190, 200);
            panel5.Controls.Add(panel6);
            panel5.Location = new Point(25, 49);
            panel5.Name = "panel5";
            panel5.Size = new Size(266, 315);
            panel5.TabIndex = 6;
            // 
            // panel6
            // 
            panel6.BackColor = Color.WhiteSmoke;
            panel6.Controls.Add(label4);
            panel6.Controls.Add(lbMissingServerIDs);
            panel6.Location = new Point(19, 15);
            panel6.Name = "panel6";
            panel6.Size = new Size(226, 285);
            panel6.TabIndex = 0;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(33, 0);
            label4.Name = "label4";
            label4.Size = new Size(150, 21);
            label4.TabIndex = 0;
            label4.Text = "Missing Server IDs";
            // 
            // lbMissingServerIDs
            // 
            lbMissingServerIDs.FormattingEnabled = true;
            lbMissingServerIDs.ItemHeight = 15;
            lbMissingServerIDs.Location = new Point(18, 24);
            lbMissingServerIDs.Name = "lbMissingServerIDs";
            lbMissingServerIDs.Size = new Size(189, 244);
            lbMissingServerIDs.TabIndex = 3;
            lbMissingServerIDs.SelectedIndexChanged += lbMissingServerIDs_SelectedIndexChanged;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(180, 190, 200);
            panel7.Controls.Add(panel8);
            panel7.Location = new Point(319, 49);
            panel7.Name = "panel7";
            panel7.Size = new Size(351, 315);
            panel7.TabIndex = 6;
            // 
            // panel8
            // 
            panel8.BackColor = Color.WhiteSmoke;
            panel8.Controls.Add(txtSearch);
            panel8.Controls.Add(dgvHotSchedulesEmployees);
            panel8.Controls.Add(label5);
            panel8.Location = new Point(19, 15);
            panel8.Name = "panel8";
            panel8.Size = new Size(315, 285);
            panel8.TabIndex = 0;
            // 
            // txtSearch
            // 
            txtSearch.Location = new Point(15, 33);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Search Name";
            txtSearch.Size = new Size(286, 23);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // dgvHotSchedulesEmployees
            // 
            dgvHotSchedulesEmployees.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHotSchedulesEmployees.Location = new Point(15, 62);
            dgvHotSchedulesEmployees.Name = "dgvHotSchedulesEmployees";
            dgvHotSchedulesEmployees.RowTemplate.Height = 25;
            dgvHotSchedulesEmployees.Size = new Size(286, 206);
            dgvHotSchedulesEmployees.TabIndex = 1;
            dgvHotSchedulesEmployees.CellContentClick += dgvHotSchedulesEmployees_CellContentClick;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(63, 0);
            label5.Name = "label5";
            label5.Size = new Size(202, 21);
            label5.TabIndex = 0;
            label5.Text = "HotSchedules Employees";
            // 
            // frmAddServer
            // 
            AcceptButton = btnAddServer;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(710, 755);
            Controls.Add(panel7);
            Controls.Add(btnDone);
            Controls.Add(panel5);
            Controls.Add(panel2);
            Controls.Add(btnCancel);
            Controls.Add(panel1);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAddServer";
            Text = "frmAddServer";
            Load += frmAddServer_Load;
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHotSchedulesEmployees).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtServerName;
        private Label label2;
        private Button btnAddServer;
        private Button btnDone;
        private ListBox lbServersToAdd;
        private Button btnCancel;
        private Label label3;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
        private Panel panel4;
        private Panel panel5;
        private Panel panel6;
        private Label label4;
        private ListBox lbMissingServerIDs;
        private Panel panel7;
        private Panel panel8;
        private TextBox txtSearch;
        private DataGridView dgvHotSchedulesEmployees;
        private Label label5;
    }
}