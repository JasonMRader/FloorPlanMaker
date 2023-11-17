namespace FloorPlanMaker
{
    partial class frmEditStaff
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
            btnAddNewServer = new Button();
            txtNewServerName = new TextBox();
            btnAssignTables = new Button();
            flowDiningAreaAssignment = new FlowLayoutPanel();
            flowUnassignedServers = new FlowLayoutPanel();
            lblShiftDate = new Label();
            cbIsAM = new CheckBox();
            btnCreateANewShift = new Button();
            panel1 = new Panel();
            panel2 = new Panel();
            pbBack = new PictureBox();
            pbForward = new PictureBox();
            label1 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pbBack).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbForward).BeginInit();
            SuspendLayout();
            // 
            // btnAddNewServer
            // 
            btnAddNewServer.BackColor = Color.FromArgb(158, 171, 222);
            btnAddNewServer.FlatAppearance.BorderSize = 0;
            btnAddNewServer.FlatStyle = FlatStyle.Flat;
            btnAddNewServer.ForeColor = Color.Black;
            btnAddNewServer.Location = new Point(12, 935);
            btnAddNewServer.Name = "btnAddNewServer";
            btnAddNewServer.Size = new Size(165, 23);
            btnAddNewServer.TabIndex = 1;
            btnAddNewServer.Text = "Add New Server";
            btnAddNewServer.UseVisualStyleBackColor = false;
            btnAddNewServer.Click += btnAddNewServer_Click;
            // 
            // txtNewServerName
            // 
            txtNewServerName.Location = new Point(12, 903);
            txtNewServerName.Name = "txtNewServerName";
            txtNewServerName.Size = new Size(165, 23);
            txtNewServerName.TabIndex = 2;
            // 
            // btnAssignTables
            // 
            btnAssignTables.BackColor = Color.FromArgb(100, 130, 180);
            btnAssignTables.FlatAppearance.BorderSize = 0;
            btnAssignTables.FlatStyle = FlatStyle.Flat;
            btnAssignTables.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            btnAssignTables.ForeColor = Color.Black;
            btnAssignTables.Location = new Point(195, 912);
            btnAssignTables.Name = "btnAssignTables";
            btnAssignTables.Size = new Size(1057, 46);
            btnAssignTables.TabIndex = 4;
            btnAssignTables.Text = "Assign Tables";
            btnAssignTables.UseVisualStyleBackColor = false;
            btnAssignTables.Click += btnAssignTables_Click;
            // 
            // flowDiningAreaAssignment
            // 
            flowDiningAreaAssignment.BackColor = Color.FromArgb(180, 190, 200);
            flowDiningAreaAssignment.Location = new Point(429, 113);
            flowDiningAreaAssignment.Name = "flowDiningAreaAssignment";
            flowDiningAreaAssignment.Size = new Size(794, 768);
            flowDiningAreaAssignment.TabIndex = 10;
            // 
            // flowUnassignedServers
            // 
            flowUnassignedServers.AutoScroll = true;
            flowUnassignedServers.BackColor = Color.WhiteSmoke;
            flowUnassignedServers.Location = new Point(17, 15);
            flowUnassignedServers.Name = "flowUnassignedServers";
            flowUnassignedServers.Size = new Size(313, 768);
            flowUnassignedServers.TabIndex = 11;
            // 
            // lblShiftDate
            // 
            lblShiftDate.Font = new Font("Segoe UI", 21.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblShiftDate.ForeColor = Color.Black;
            lblShiftDate.Location = new Point(650, 37);
            lblShiftDate.Name = "lblShiftDate";
            lblShiftDate.Size = new Size(406, 39);
            lblShiftDate.TabIndex = 16;
            lblShiftDate.Text = "Date";
            lblShiftDate.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cbIsAM
            // 
            cbIsAM.Appearance = Appearance.Button;
            cbIsAM.BackColor = Color.FromArgb(255, 255, 192);
            cbIsAM.FlatAppearance.BorderSize = 0;
            cbIsAM.FlatStyle = FlatStyle.Flat;
            cbIsAM.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAM.ForeColor = Color.Black;
            cbIsAM.Location = new Point(1062, 37);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(79, 39);
            cbIsAM.TabIndex = 19;
            cbIsAM.Text = "PM";
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAM.UseVisualStyleBackColor = false;
            cbIsAM.CheckedChanged += cbIsPM_CheckedChanged;
            // 
            // btnCreateANewShift
            // 
            btnCreateANewShift.BackColor = Color.FromArgb(100, 130, 180);
            btnCreateANewShift.FlatAppearance.BorderSize = 0;
            btnCreateANewShift.FlatStyle = FlatStyle.Flat;
            btnCreateANewShift.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreateANewShift.ForeColor = Color.Black;
            btnCreateANewShift.Location = new Point(25, 44);
            btnCreateANewShift.Name = "btnCreateANewShift";
            btnCreateANewShift.Size = new Size(352, 33);
            btnCreateANewShift.TabIndex = 25;
            btnCreateANewShift.Text = "Edit / Create Shifts";
            btnCreateANewShift.UseVisualStyleBackColor = false;
            btnCreateANewShift.Click += btnCreateANewShift_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(flowUnassignedServers);
            panel1.Location = new Point(25, 98);
            panel1.Name = "panel1";
            panel1.Size = new Size(352, 799);
            panel1.TabIndex = 26;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(180, 190, 200);
            panel2.Location = new Point(409, 98);
            panel2.Name = "panel2";
            panel2.Size = new Size(833, 799);
            panel2.TabIndex = 27;
            // 
            // pbBack
            // 
            pbBack.Image = FloorPlanMakerUI.Properties.Resources.back;
            pbBack.Location = new Point(576, 17);
            pbBack.Name = "pbBack";
            pbBack.Size = new Size(55, 78);
            pbBack.SizeMode = PictureBoxSizeMode.Zoom;
            pbBack.TabIndex = 28;
            pbBack.TabStop = false;
            pbBack.Click += btnDateDown_Click;
            // 
            // pbForward
            // 
            pbForward.Image = FloorPlanMakerUI.Properties.Resources.forward;
            pbForward.Location = new Point(1168, 17);
            pbForward.Name = "pbForward";
            pbForward.Size = new Size(55, 75);
            pbForward.SizeMode = PictureBoxSizeMode.Zoom;
            pbForward.TabIndex = 28;
            pbForward.TabStop = false;
            pbForward.Click += btnDateUp_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(180, 190, 200);
            label1.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(409, 17);
            label1.Name = "label1";
            label1.Size = new Size(154, 20);
            label1.TabIndex = 29;
            label1.Text = "Yesterday  |  Last Week";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.BackColor = Color.FromArgb(100, 130, 180);
            label3.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.White;
            label3.Location = new Point(409, 40);
            label3.Name = "label3";
            label3.Size = new Size(154, 20);
            label3.TabIndex = 29;
            label3.Text = "Current Server Count";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.BackColor = SystemColors.ButtonShadow;
            label5.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(409, 64);
            label5.Name = "label5";
            label5.Size = new Size(77, 31);
            label5.TabIndex = 29;
            label5.Text = "Sales per Server";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            label6.BackColor = SystemColors.ButtonShadow;
            label6.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(486, 64);
            label6.Name = "label6";
            label6.Size = new Size(77, 31);
            label6.TabIndex = 30;
            label6.Text = "Covers per Server";
            label6.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // frmEditStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1264, 967);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label1);
            Controls.Add(pbForward);
            Controls.Add(pbBack);
            Controls.Add(btnCreateANewShift);
            Controls.Add(cbIsAM);
            Controls.Add(lblShiftDate);
            Controls.Add(flowDiningAreaAssignment);
            Controls.Add(btnAssignTables);
            Controls.Add(txtNewServerName);
            Controls.Add(btnAddNewServer);
            Controls.Add(panel1);
            Controls.Add(panel2);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmEditStaff";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmEditStaff";
            Load += frmEditStaff_Load;
            MouseDown += frmEditStaff_MouseDown;
            MouseMove += frmEditStaff_MouseMove;
            MouseUp += frmEditStaff_MouseUp;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pbBack).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbForward).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnAddNewServer;
        private TextBox txtNewServerName;
        private Button btnAssignTables;
        private FlowLayoutPanel flowDiningAreaAssignment;
        private FlowLayoutPanel flowUnassignedServers;
        private Label lblShiftDate;
        private CheckBox cbIsAM;
        private Button btnCreateANewShift;
        private Panel panel1;
        private Panel panel2;
        private PictureBox pbBack;
        private PictureBox pbForward;
        private Label label1;
        private Label label3;
        private Label label5;
        private Label label6;
    }
}