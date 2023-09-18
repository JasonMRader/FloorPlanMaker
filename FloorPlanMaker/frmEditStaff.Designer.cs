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
            lvAllStaff = new ListView();
            lvThisShift = new ListView();
            btnAddNewServer = new Button();
            txtNewServerName = new TextBox();
            btnRemove = new Button();
            btnAdd = new Button();
            btnAssignTables = new Button();
            SuspendLayout();
            // 
            // lvAllStaff
            // 
            lvAllStaff.Location = new Point(42, 38);
            lvAllStaff.Name = "lvAllStaff";
            lvAllStaff.Size = new Size(224, 377);
            lvAllStaff.TabIndex = 0;
            lvAllStaff.UseCompatibleStateImageBehavior = false;
            // 
            // lvThisShift
            // 
            lvThisShift.Location = new Point(365, 38);
            lvThisShift.Name = "lvThisShift";
            lvThisShift.Size = new Size(224, 377);
            lvThisShift.TabIndex = 0;
            lvThisShift.UseCompatibleStateImageBehavior = false;
            // 
            // btnAddNewServer
            // 
            btnAddNewServer.Location = new Point(323, 9);
            btnAddNewServer.Name = "btnAddNewServer";
            btnAddNewServer.Size = new Size(286, 23);
            btnAddNewServer.TabIndex = 1;
            btnAddNewServer.Text = "Add New Server";
            btnAddNewServer.UseVisualStyleBackColor = true;
            btnAddNewServer.Click += btnAddNewServer_Click;
            // 
            // txtNewServerName
            // 
            txtNewServerName.Location = new Point(12, 9);
            txtNewServerName.Name = "txtNewServerName";
            txtNewServerName.Size = new Size(290, 23);
            txtNewServerName.TabIndex = 2;
            // 
            // btnRemove
            // 
            btnRemove.Location = new Point(272, 145);
            btnRemove.Name = "btnRemove";
            btnRemove.Size = new Size(87, 23);
            btnRemove.TabIndex = 3;
            btnRemove.Text = "Remove From Shift";
            btnRemove.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(272, 38);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(87, 23);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "Add To Shift";
            btnAdd.UseVisualStyleBackColor = true;
            // 
            // btnAssignTables
            // 
            btnAssignTables.Location = new Point(365, 431);
            btnAssignTables.Name = "btnAssignTables";
            btnAssignTables.Size = new Size(224, 23);
            btnAssignTables.TabIndex = 4;
            btnAssignTables.Text = "Assign Tables";
            btnAssignTables.UseVisualStyleBackColor = true;
            // 
            // frmEditStaff
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(621, 467);
            Controls.Add(btnAssignTables);
            Controls.Add(btnAdd);
            Controls.Add(btnRemove);
            Controls.Add(txtNewServerName);
            Controls.Add(btnAddNewServer);
            Controls.Add(lvThisShift);
            Controls.Add(lvAllStaff);
            Name = "frmEditStaff";
            Text = "frmEditStaff";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvAllStaff;
        private ListView lvThisShift;
        private Button btnAddNewServer;
        private TextBox txtNewServerName;
        private Button btnRemove;
        private Button btnAdd;
        private Button btnAssignTables;
    }
}