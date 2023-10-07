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
            cbIsPM = new CheckBox();
            SuspendLayout();
            // 
            // btnAddNewServer
            // 
            btnAddNewServer.Location = new Point(12, 935);
            btnAddNewServer.Name = "btnAddNewServer";
            btnAddNewServer.Size = new Size(165, 23);
            btnAddNewServer.TabIndex = 1;
            btnAddNewServer.Text = "Add New Server";
            btnAddNewServer.UseVisualStyleBackColor = true;
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
            btnAssignTables.Location = new Point(183, 912);
            btnAssignTables.Name = "btnAssignTables";
            btnAssignTables.Size = new Size(1058, 46);
            btnAssignTables.TabIndex = 4;
            btnAssignTables.Text = "Assign Tables";
            btnAssignTables.UseVisualStyleBackColor = true;
            btnAssignTables.Click += btnAssignTables_Click;
            // 
            // flowDiningAreaAssignment
            // 
            flowDiningAreaAssignment.Location = new Point(195, 166);
            flowDiningAreaAssignment.Name = "flowDiningAreaAssignment";
            flowDiningAreaAssignment.Size = new Size(680, 740);
            flowDiningAreaAssignment.TabIndex = 10;
            // 
            // flowUnassignedServers
            // 
            flowUnassignedServers.AutoScroll = true;
            flowUnassignedServers.Location = new Point(881, 74);
            flowUnassignedServers.Name = "flowUnassignedServers";
            flowUnassignedServers.Size = new Size(360, 832);
            flowUnassignedServers.TabIndex = 11;
            // 
            // cbUnassignedServers
            // 
            cbUnassignedServers.Appearance = Appearance.Button;
            cbUnassignedServers.Location = new Point(881, 38);
            cbUnassignedServers.Name = "cbUnassignedServers";
            cbUnassignedServers.Size = new Size(360, 30);
            cbUnassignedServers.TabIndex = 12;
            cbUnassignedServers.Text = "Unassigned Servers";
            cbUnassignedServers.TextAlign = ContentAlignment.MiddleCenter;
            cbUnassignedServers.UseVisualStyleBackColor = true;
            cbUnassignedServers.CheckedChanged += cbUnassignedServers_CheckedChanged;
            // 
            // flowAllServers
            // 
            flowAllServers.AutoScroll = true;
            flowAllServers.Location = new Point(12, 230);
            flowAllServers.Name = "flowAllServers";
            flowAllServers.Size = new Size(165, 667);
            flowAllServers.TabIndex = 14;
            // 
            // flowDiningAreas
            // 
            flowDiningAreas.Location = new Point(12, 40);
            flowDiningAreas.Name = "flowDiningAreas";
            flowDiningAreas.Size = new Size(165, 184);
            flowDiningAreas.TabIndex = 15;
            // 
            // lblShiftDate
            // 
            lblShiftDate.Font = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point);
            lblShiftDate.Location = new Point(284, 13);
            lblShiftDate.Name = "lblShiftDate";
            lblShiftDate.Size = new Size(331, 32);
            lblShiftDate.TabIndex = 16;
            lblShiftDate.Text = "Date";
            lblShiftDate.TextAlign = ContentAlignment.MiddleRight;
            // 
            // btnDateUp
            // 
            btnDateUp.FlatStyle = FlatStyle.Flat;
            btnDateUp.Image = FloorPlanMakerUI.Resource1.forwardArrow;
            btnDateUp.Location = new Point(706, 11);
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
            btnDateDown.Location = new Point(251, 11);
            btnDateDown.Name = "btnDateDown";
            btnDateDown.Size = new Size(27, 42);
            btnDateDown.TabIndex = 18;
            btnDateDown.UseVisualStyleBackColor = true;
            btnDateDown.Click += btnDateDown_Click;
            // 
            // cbIsPM
            // 
            cbIsPM.Appearance = Appearance.Button;
            cbIsPM.BackColor = Color.FromArgb(255, 255, 192);
            cbIsPM.FlatAppearance.BorderSize = 0;
            cbIsPM.FlatStyle = FlatStyle.Flat;
            cbIsPM.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsPM.Location = new Point(621, 13);
            cbIsPM.Name = "cbIsPM";
            cbIsPM.Size = new Size(79, 32);
            cbIsPM.TabIndex = 19;
            cbIsPM.Text = "AM";
            cbIsPM.TextAlign = ContentAlignment.MiddleCenter;
            cbIsPM.UseVisualStyleBackColor = false;
            cbIsPM.CheckedChanged += cbIsPM_CheckedChanged;
            // 
            // frmEditStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 970);
            Controls.Add(cbIsPM);
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
            Name = "frmEditStaff";
            StartPosition = FormStartPosition.CenterParent;
            Text = "frmEditStaff";
            Load += frmEditStaff_Load;
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
        private CheckBox cbIsPM;
    }
}