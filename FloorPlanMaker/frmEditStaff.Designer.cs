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
            cbUnassignedServers = new CheckBox();
            flowAllServers = new FlowLayoutPanel();
            flowDiningAreas = new FlowLayoutPanel();
            lblShiftDate = new Label();
            btnDateUp = new Button();
            btnDateDown = new Button();
            cbIsAM = new CheckBox();
            flowYesterdayCounts = new FlowLayoutPanel();
            flowLastWeekdayCounts = new FlowLayoutPanel();
            btnCreateANewShift = new Button();
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
            btnAssignTables.BackColor = Color.FromArgb(158, 171, 222);
            btnAssignTables.FlatAppearance.BorderSize = 0;
            btnAssignTables.FlatStyle = FlatStyle.Flat;
            btnAssignTables.ForeColor = Color.Black;
            btnAssignTables.Location = new Point(195, 912);
            btnAssignTables.Name = "btnAssignTables";
            btnAssignTables.Size = new Size(1046, 46);
            btnAssignTables.TabIndex = 4;
            btnAssignTables.Text = "Assign Tables";
            btnAssignTables.UseVisualStyleBackColor = false;
            btnAssignTables.Click += btnAssignTables_Click;
            // 
            // flowDiningAreaAssignment
            // 
            flowDiningAreaAssignment.BackColor = Color.FromArgb(178, 87, 46);
            flowDiningAreaAssignment.Location = new Point(378, 123);
            flowDiningAreaAssignment.Name = "flowDiningAreaAssignment";
            flowDiningAreaAssignment.Size = new Size(863, 774);
            flowDiningAreaAssignment.TabIndex = 10;
            // 
            // flowUnassignedServers
            // 
            flowUnassignedServers.AutoScroll = true;
            flowUnassignedServers.BackColor = Color.FromArgb(178, 87, 46);
            flowUnassignedServers.Location = new Point(12, 123);
            flowUnassignedServers.Name = "flowUnassignedServers";
            flowUnassignedServers.Size = new Size(360, 774);
            flowUnassignedServers.TabIndex = 11;
            // 
            // cbUnassignedServers
            // 
            cbUnassignedServers.Appearance = Appearance.Button;
            cbUnassignedServers.BackColor = Color.FromArgb(158, 171, 222);
            cbUnassignedServers.FlatAppearance.BorderSize = 0;
            cbUnassignedServers.FlatStyle = FlatStyle.Flat;
            cbUnassignedServers.ForeColor = Color.Black;
            cbUnassignedServers.Location = new Point(12, 87);
            cbUnassignedServers.Name = "cbUnassignedServers";
            cbUnassignedServers.Size = new Size(360, 30);
            cbUnassignedServers.TabIndex = 12;
            cbUnassignedServers.Text = "Unassigned Servers";
            cbUnassignedServers.TextAlign = ContentAlignment.MiddleCenter;
            cbUnassignedServers.UseVisualStyleBackColor = false;
            cbUnassignedServers.CheckedChanged += cbUnassignedServers_CheckedChanged;
            // 
            // flowAllServers
            // 
            flowAllServers.AutoScroll = true;
            flowAllServers.BackColor = Color.FromArgb(178, 87, 46);
            flowAllServers.Location = new Point(12, 44);
            flowAllServers.MaximumSize = new Size(165, 10000);
            flowAllServers.Name = "flowAllServers";
            flowAllServers.Size = new Size(47, 23);
            flowAllServers.TabIndex = 14;
            flowAllServers.Visible = false;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.BackColor = Color.FromArgb(178, 87, 46);
            flowDiningAreas.Location = new Point(12, 12);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(17, 12);
            flowDiningAreas.TabIndex = 15;
            flowDiningAreas.Visible = false;
            // 
            // lblShiftDate
            // 
            lblShiftDate.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblShiftDate.Location = new Point(516, 81);
            lblShiftDate.Name = "lblShiftDate";
            lblShiftDate.Size = new Size(433, 32);
            lblShiftDate.TabIndex = 16;
            lblShiftDate.Text = "Date";
            lblShiftDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnDateUp
            // 
            btnDateUp.FlatStyle = FlatStyle.Flat;
            btnDateUp.Image = FloorPlanMakerUI.Resource1.forwardArrow;
            btnDateUp.Location = new Point(1040, 81);
            btnDateUp.Name = "btnDateUp";
            btnDateUp.Size = new Size(27, 42);
            btnDateUp.TabIndex = 17;
            btnDateUp.UseVisualStyleBackColor = true;
            btnDateUp.Click += btnDateUp_Click;
            // 
            // btnDateDown
            // 
            btnDateDown.FlatStyle = FlatStyle.Flat;
            btnDateDown.Image = FloorPlanMakerUI.Resource1.BackArrow;
            btnDateDown.Location = new Point(468, 76);
            btnDateDown.Name = "btnDateDown";
            btnDateDown.Size = new Size(27, 42);
            btnDateDown.TabIndex = 18;
            btnDateDown.UseVisualStyleBackColor = true;
            btnDateDown.Click += btnDateDown_Click;
            // 
            // cbIsAM
            // 
            cbIsAM.Appearance = Appearance.Button;
            cbIsAM.BackColor = Color.FromArgb(255, 255, 192);
            cbIsAM.FlatAppearance.BorderSize = 0;
            cbIsAM.FlatStyle = FlatStyle.Flat;
            cbIsAM.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAM.ForeColor = Color.Black;
            cbIsAM.Location = new Point(955, 76);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(79, 42);
            cbIsAM.TabIndex = 19;
            cbIsAM.Text = "PM";
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAM.UseVisualStyleBackColor = false;
            cbIsAM.CheckedChanged += cbIsPM_CheckedChanged;
            // 
            // flowYesterdayCounts
            // 
            flowYesterdayCounts.BackColor = Color.FromArgb(224, 224, 224);
            flowYesterdayCounts.ForeColor = Color.Black;
            flowYesterdayCounts.Location = new Point(59, 12);
            flowYesterdayCounts.Name = "flowYesterdayCounts";
            flowYesterdayCounts.Size = new Size(17, 12);
            flowYesterdayCounts.TabIndex = 23;
            flowYesterdayCounts.Visible = false;
            // 
            // flowLastWeekdayCounts
            // 
            flowLastWeekdayCounts.BackColor = Color.FromArgb(224, 224, 224);
            flowLastWeekdayCounts.ForeColor = Color.Black;
            flowLastWeekdayCounts.Location = new Point(35, 12);
            flowLastWeekdayCounts.Name = "flowLastWeekdayCounts";
            flowLastWeekdayCounts.Size = new Size(18, 10);
            flowLastWeekdayCounts.TabIndex = 21;
            flowLastWeekdayCounts.Visible = false;
            // 
            // btnCreateANewShift
            // 
            btnCreateANewShift.FlatAppearance.BorderSize = 0;
            btnCreateANewShift.FlatStyle = FlatStyle.Flat;
            btnCreateANewShift.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreateANewShift.ForeColor = Color.Black;
            btnCreateANewShift.Location = new Point(12, 0);
            btnCreateANewShift.Name = "btnCreateANewShift";
            btnCreateANewShift.Size = new Size(1240, 33);
            btnCreateANewShift.TabIndex = 25;
            btnCreateANewShift.Text = "Create a New Shift";
            btnCreateANewShift.UseVisualStyleBackColor = true;
            btnCreateANewShift.Click += btnCreateANewShift_Click;
            // 
            // frmEditStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 56, 82);
            ClientSize = new Size(1264, 967);
            Controls.Add(flowDiningAreas);
            Controls.Add(btnCreateANewShift);
            Controls.Add(flowYesterdayCounts);
            Controls.Add(flowLastWeekdayCounts);
            Controls.Add(cbIsAM);
            Controls.Add(btnDateDown);
            Controls.Add(btnDateUp);
            Controls.Add(lblShiftDate);
            Controls.Add(flowAllServers);
            Controls.Add(cbUnassignedServers);
            Controls.Add(flowUnassignedServers);
            Controls.Add(flowDiningAreaAssignment);
            Controls.Add(btnAssignTables);
            Controls.Add(txtNewServerName);
            Controls.Add(btnAddNewServer);
            ForeColor = Color.White;
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmEditStaff";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmEditStaff";
            Load += frmEditStaff_Load;
            MouseDown += frmEditStaff_MouseDown;
            MouseMove += frmEditStaff_MouseMove;
            MouseUp += frmEditStaff_MouseUp;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnAddNewServer;
        private TextBox txtNewServerName;
        private Button btnAssignTables;
        private FlowLayoutPanel flowDiningAreaAssignment;
        private FlowLayoutPanel flowUnassignedServers;
        private CheckBox cbUnassignedServers;
        private FlowLayoutPanel flowAllServers;
        private FlowLayoutPanel flowDiningAreas;
        private Label lblShiftDate;
        private Button btnDateUp;
        private Button btnDateDown;
        private CheckBox cbIsAM;
        private FlowLayoutPanel flowYesterdayCounts;
        private FlowLayoutPanel flowLastWeekdayCounts;
        private Button btnCreateANewShift;
    }
}