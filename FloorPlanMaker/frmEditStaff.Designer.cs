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
            clbDiningAreaSelection = new CheckedListBox();
            btnAssignAreas = new Button();
            flowDiningAreaAssignment = new FlowLayoutPanel();
            flowUnassignedServers = new FlowLayoutPanel();
            cbUnassignedServers = new CheckBox();
            label1 = new Label();
            flowAllServers = new FlowLayoutPanel();
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
            // clbDiningAreaSelection
            // 
            clbDiningAreaSelection.CheckOnClick = true;
            clbDiningAreaSelection.FormattingEnabled = true;
            clbDiningAreaSelection.Location = new Point(12, 38);
            clbDiningAreaSelection.Name = "clbDiningAreaSelection";
            clbDiningAreaSelection.Size = new Size(165, 148);
            clbDiningAreaSelection.TabIndex = 8;
            // 
            // btnAssignAreas
            // 
            btnAssignAreas.Location = new Point(12, 201);
            btnAssignAreas.Name = "btnAssignAreas";
            btnAssignAreas.Size = new Size(165, 23);
            btnAssignAreas.TabIndex = 9;
            btnAssignAreas.Text = "Assign Areas";
            btnAssignAreas.UseVisualStyleBackColor = true;
            btnAssignAreas.Click += btnAssignAreas_Click;
            // 
            // flowDiningAreaAssignment
            // 
            flowDiningAreaAssignment.Location = new Point(195, 38);
            flowDiningAreaAssignment.Name = "flowDiningAreaAssignment";
            flowDiningAreaAssignment.Size = new Size(680, 868);
            flowDiningAreaAssignment.TabIndex = 10;
            // 
            // flowUnassignedServers
            // 
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
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.FromArgb(255, 192, 192);
            label1.Location = new Point(195, 9);
            label1.Name = "label1";
            label1.Size = new Size(245, 15);
            label1.TabIndex = 13;
            label1.Text = "*****Add Covers per Server for each floorplan";
            // 
            // flowAllServers
            // 
            flowAllServers.AutoScroll = true;
            flowAllServers.Location = new Point(12, 230);
            flowAllServers.Name = "flowAllServers";
            flowAllServers.Size = new Size(165, 667);
            flowAllServers.TabIndex = 14;
            // 
            // frmEditStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 970);
            Controls.Add(flowAllServers);
            Controls.Add(label1);
            Controls.Add(cbUnassignedServers);
            Controls.Add(flowUnassignedServers);
            Controls.Add(flowDiningAreaAssignment);
            Controls.Add(btnAssignAreas);
            Controls.Add(clbDiningAreaSelection);
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
        private CheckedListBox clbDiningAreaSelection;
        private Button btnAssignAreas;
        private FlowLayoutPanel flowDiningAreaAssignment;
        private FlowLayoutPanel flowUnassignedServers;
        private CheckBox cbUnassignedServers;
        private Label label1;
        private FlowLayoutPanel flowAllServers;
    }
}