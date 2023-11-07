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
            lblLastWeekDay = new Label();
            label2 = new Label();
            flowYesterdayCounts = new FlowLayoutPanel();
            flowLastWeekdayCounts = new FlowLayoutPanel();
            label1 = new Label();
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
            flowDiningAreaAssignment.Location = new Point(195, 230);
            flowDiningAreaAssignment.Name = "flowDiningAreaAssignment";
            flowDiningAreaAssignment.Size = new Size(666, 650);
            flowDiningAreaAssignment.TabIndex = 10;
            // 
            // flowUnassignedServers
            // 
            flowUnassignedServers.AutoScroll = true;
            flowUnassignedServers.BackColor = Color.FromArgb(178, 87, 46);
            flowUnassignedServers.Location = new Point(881, 230);
            flowUnassignedServers.Name = "flowUnassignedServers";
            flowUnassignedServers.Size = new Size(360, 650);
            flowUnassignedServers.TabIndex = 11;
            // 
            // cbUnassignedServers
            // 
            cbUnassignedServers.Appearance = Appearance.Button;
            cbUnassignedServers.BackColor = Color.FromArgb(158, 171, 222);
            cbUnassignedServers.FlatAppearance.BorderSize = 0;
            cbUnassignedServers.FlatStyle = FlatStyle.Flat;
            cbUnassignedServers.ForeColor = Color.Black;
            cbUnassignedServers.Location = new Point(881, 194);
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
            flowAllServers.Location = new Point(12, 230);
            flowAllServers.MaximumSize = new Size(165, 10000);
            flowAllServers.Name = "flowAllServers";
            flowAllServers.Size = new Size(165, 650);
            flowAllServers.TabIndex = 14;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.BackColor = Color.FromArgb(178, 87, 46);
            flowDiningAreas.Location = new Point(12, 40);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(165, 175);
            flowDiningAreas.TabIndex = 15;
            // 
            // lblShiftDate
            // 
            lblShiftDate.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblShiftDate.Location = new Point(224, 81);
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
            btnDateUp.Location = new Point(829, 79);
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
            btnDateDown.Location = new Point(195, 81);
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
            cbIsAM.Location = new Point(721, 79);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(79, 42);
            cbIsAM.TabIndex = 19;
            cbIsAM.Text = "PM";
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAM.UseVisualStyleBackColor = false;
            cbIsAM.CheckedChanged += cbIsPM_CheckedChanged;
            // 
            // lblLastWeekDay
            // 
            lblLastWeekDay.Anchor = AnchorStyles.Right;
            lblLastWeekDay.AutoSize = true;
            lblLastWeekDay.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblLastWeekDay.Location = new Point(881, 20);
            lblLastWeekDay.Name = "lblLastWeekDay";
            lblLastWeekDay.Size = new Size(89, 21);
            lblLastWeekDay.TabIndex = 20;
            lblLastWeekDay.Text = "Last Week:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(881, 104);
            label2.Name = "label2";
            label2.Size = new Size(85, 21);
            label2.TabIndex = 22;
            label2.Text = "Yesterday:";
            // 
            // flowYesterdayCounts
            // 
            flowYesterdayCounts.BackColor = Color.FromArgb(224, 224, 224);
            flowYesterdayCounts.ForeColor = Color.Black;
            flowYesterdayCounts.Location = new Point(881, 128);
            flowYesterdayCounts.Name = "flowYesterdayCounts";
            flowYesterdayCounts.Size = new Size(360, 60);
            flowYesterdayCounts.TabIndex = 23;
            // 
            // flowLastWeekdayCounts
            // 
            flowLastWeekdayCounts.BackColor = Color.FromArgb(224, 224, 224);
            flowLastWeekdayCounts.ForeColor = Color.Black;
            flowLastWeekdayCounts.Location = new Point(881, 44);
            flowLastWeekdayCounts.Name = "flowLastWeekdayCounts";
            flowLastWeekdayCounts.Size = new Size(360, 60);
            flowLastWeekdayCounts.TabIndex = 21;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(196, 9);
            label1.Name = "label1";
            label1.Size = new Size(679, 38);
            label1.TabIndex = 24;
            label1.Text = "New Shift For:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnCreateANewShift
            // 
            btnCreateANewShift.FlatAppearance.BorderSize = 0;
            btnCreateANewShift.FlatStyle = FlatStyle.Flat;
            btnCreateANewShift.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            btnCreateANewShift.ForeColor = Color.Black;
            btnCreateANewShift.Location = new Point(196, 12);
            btnCreateANewShift.Name = "btnCreateANewShift";
            btnCreateANewShift.Size = new Size(679, 36);
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
            Controls.Add(btnCreateANewShift);
            Controls.Add(label1);
            Controls.Add(flowYesterdayCounts);
            Controls.Add(label2);
            Controls.Add(flowLastWeekdayCounts);
            Controls.Add(lblLastWeekDay);
            Controls.Add(cbIsAM);
            Controls.Add(btnDateDown);
            Controls.Add(btnDateUp);
            Controls.Add(lblShiftDate);
            Controls.Add(flowDiningAreas);
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
        private Label lblLastWeekDay;
        private Label label2;
        private FlowLayoutPanel flowYesterdayCounts;
        private FlowLayoutPanel flowLastWeekdayCounts;
        private Label label1;
        private Button btnCreateANewShift;
    }
}