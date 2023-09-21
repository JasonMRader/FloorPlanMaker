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
            btnAdd = new Button();
            btnAssignTables = new Button();
            clbAllServers = new CheckedListBox();
            clbDiningAreaSelection = new CheckedListBox();
            btnAssignAreas = new Button();
            flowDiningAreaAssignment = new FlowLayoutPanel();
            flowUnassignedServers = new FlowLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
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
            // btnAdd
            // 
            btnAdd.Location = new Point(12, 874);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(165, 23);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add To Shift";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
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
            // clbAllServers
            // 
            clbAllServers.CheckOnClick = true;
            clbAllServers.FormattingEnabled = true;
            clbAllServers.Location = new Point(12, 234);
            clbAllServers.Name = "clbAllServers";
            clbAllServers.Size = new Size(165, 634);
            clbAllServers.TabIndex = 7;
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
            flowDiningAreaAssignment.Size = new Size(504, 90);
            flowDiningAreaAssignment.TabIndex = 10;
            // 
            // flowUnassignedServers
            // 
            flowUnassignedServers.Location = new Point(715, 38);
            flowUnassignedServers.Name = "flowUnassignedServers";
            flowUnassignedServers.Size = new Size(526, 868);
            flowUnassignedServers.TabIndex = 11;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Location = new Point(195, 148);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(504, 758);
            flowLayoutPanel1.TabIndex = 12;
            // 
            // frmEditStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 970);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(flowUnassignedServers);
            Controls.Add(flowDiningAreaAssignment);
            Controls.Add(btnAssignAreas);
            Controls.Add(clbDiningAreaSelection);
            Controls.Add(clbAllServers);
            Controls.Add(btnAssignTables);
            Controls.Add(btnAdd);
            Controls.Add(txtNewServerName);
            Controls.Add(btnAddNewServer);
            Name = "frmEditStaff";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmEditStaff";
            Load += frmEditStaff_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnAddNewServer;
        private TextBox txtNewServerName;
        private Button btnAdd;
        private Button btnAssignTables;
        private CheckedListBox clbAllServers;
        private CheckedListBox clbDiningAreaSelection;
        private Button btnAssignAreas;
        private FlowLayoutPanel flowDiningAreaAssignment;
        private FlowLayoutPanel flowUnassignedServers;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}