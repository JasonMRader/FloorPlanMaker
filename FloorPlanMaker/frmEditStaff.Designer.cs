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
            btnRemove = new Button();
            btnAdd = new Button();
            btnAssignTables = new Button();
            lbServersOnShift = new ListBox();
            clbAllServers = new CheckedListBox();
            clbDiningAreaSelection = new CheckedListBox();
            btnAssignAreas = new Button();
            flowDiningAreaAssignment = new FlowLayoutPanel();
            SuspendLayout();
            // 
            // btnAddNewServer
            // 
            btnAddNewServer.Location = new Point(232, 12);
            btnAddNewServer.Name = "btnAddNewServer";
            btnAddNewServer.Size = new Size(255, 23);
            btnAddNewServer.TabIndex = 1;
            btnAddNewServer.Text = "Add New Server";
            btnAddNewServer.UseVisualStyleBackColor = true;
            btnAddNewServer.Click += btnAddNewServer_Click;
            // 
            // txtNewServerName
            // 
            txtNewServerName.Location = new Point(12, 9);
            txtNewServerName.Name = "txtNewServerName";
            txtNewServerName.Size = new Size(202, 23);
            txtNewServerName.TabIndex = 2;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(220, 105);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(87, 23);
            btnRemove.TabIndex = 3;
            btnRemove.Text = "Remove From Shift";
            btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(220, 76);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(87, 23);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add To Shift";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnAssignTables
            // 
            btnAssignTables.Location = new Point(12, 912);
            btnAssignTables.Name = "btnAssignTables";
            btnAssignTables.Size = new Size(1229, 46);
            btnAssignTables.TabIndex = 4;
            btnAssignTables.Text = "Assign Tables";
            btnAssignTables.UseVisualStyleBackColor = true;
            btnAssignTables.Click += btnAssignTables_Click;
            // 
            // lbServersOnShift
            // 
            lbServersOnShift.FormattingEnabled = true;
            lbServersOnShift.ItemHeight = 15;
            lbServersOnShift.Location = new Point(313, 38);
            lbServersOnShift.Name = "lbServersOnShift";
            lbServersOnShift.Size = new Size(174, 379);
            lbServersOnShift.TabIndex = 6;
            // 
            // clbAllServers
            // 
            clbAllServers.CheckOnClick = true;
            clbAllServers.FormattingEnabled = true;
            clbAllServers.Location = new Point(12, 38);
            clbAllServers.Name = "clbAllServers";
            clbAllServers.Size = new Size(202, 382);
            clbAllServers.TabIndex = 7;
            // 
            // clbDiningAreaSelection
            // 
            clbDiningAreaSelection.FormattingEnabled = true;
            clbDiningAreaSelection.Location = new Point(521, 38);
            clbDiningAreaSelection.Name = "clbDiningAreaSelection";
            clbDiningAreaSelection.Size = new Size(156, 148);
            clbDiningAreaSelection.TabIndex = 8;
            // 
            // btnAssignAreas
            // 
            btnAssignAreas.Location = new Point(521, 192);
            btnAssignAreas.Name = "btnAssignAreas";
            btnAssignAreas.Size = new Size(156, 23);
            btnAssignAreas.TabIndex = 9;
            btnAssignAreas.Text = "Assign Areas";
            btnAssignAreas.UseVisualStyleBackColor = true;
            btnAssignAreas.Click += btnAssignAreas_Click;
            // 
            // flowDiningAreaAssignment
            // 
            flowDiningAreaAssignment.Location = new Point(704, 38);
            flowDiningAreaAssignment.Name = "flowDiningAreaAssignment";
            flowDiningAreaAssignment.Size = new Size(504, 90);
            flowDiningAreaAssignment.TabIndex = 10;
            // 
            // frmEditStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 970);
            Controls.Add(flowDiningAreaAssignment);
            Controls.Add(btnAssignAreas);
            Controls.Add(clbDiningAreaSelection);
            Controls.Add(clbAllServers);
            Controls.Add(lbServersOnShift);
            Controls.Add(btnAssignTables);
            Controls.Add(btnAdd);
            Controls.Add(btnRemove);
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
        private Button btnRemove;
        private Button btnAdd;
        private Button btnAssignTables;
        private ListBox lbServersOnShift;
        private CheckedListBox clbAllServers;
        private CheckedListBox clbDiningAreaSelection;
        private Button btnAssignAreas;
        private FlowLayoutPanel flowDiningAreaAssignment;
    }
}