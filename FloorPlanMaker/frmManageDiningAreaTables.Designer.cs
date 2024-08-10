namespace FloorPlanMakerUI
{
    partial class frmManageDiningAreaTables
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
            lblDiningAreaName = new Label();
            lbTablesInArea = new ListBox();
            lbTablesCountedInStats = new ListBox();
            lbTablesExcludedFromStats = new ListBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            btnAddAllToCounted = new Button();
            lbCountedNotInCurrent = new ListBox();
            label1 = new Label();
            btnAddSelected = new Button();
            btnSaveChanges = new Button();
            SuspendLayout();
            // 
            // lblDiningAreaName
            // 
            lblDiningAreaName.Dock = DockStyle.Top;
            lblDiningAreaName.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningAreaName.Location = new Point(0, 0);
            lblDiningAreaName.Name = "lblDiningAreaName";
            lblDiningAreaName.Size = new Size(905, 33);
            lblDiningAreaName.TabIndex = 0;
            lblDiningAreaName.Text = "DiningAreaNameLabel";
            lblDiningAreaName.TextAlign = ContentAlignment.MiddleCenter;
            lblDiningAreaName.Click += lblDiningAreaName_Click;
            // 
            // lbTablesInArea
            // 
            lbTablesInArea.AllowDrop = true;
            lbTablesInArea.FormattingEnabled = true;
            lbTablesInArea.ItemHeight = 15;
            lbTablesInArea.Location = new Point(29, 143);
            lbTablesInArea.Name = "lbTablesInArea";
            lbTablesInArea.Size = new Size(138, 559);
            lbTablesInArea.TabIndex = 1;
            // 
            // lbTablesCountedInStats
            // 
            lbTablesCountedInStats.FormattingEnabled = true;
            lbTablesCountedInStats.ItemHeight = 15;
            lbTablesCountedInStats.Location = new Point(219, 113);
            lbTablesCountedInStats.Name = "lbTablesCountedInStats";
            lbTablesCountedInStats.Size = new Size(152, 589);
            lbTablesCountedInStats.TabIndex = 1;
            // 
            // lbTablesExcludedFromStats
            // 
            lbTablesExcludedFromStats.FormattingEnabled = true;
            lbTablesExcludedFromStats.ItemHeight = 15;
            lbTablesExcludedFromStats.Location = new Point(602, 113);
            lbTablesExcludedFromStats.Name = "lbTablesExcludedFromStats";
            lbTablesExcludedFromStats.Size = new Size(152, 589);
            lbTablesExcludedFromStats.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 104);
            label2.Name = "label2";
            label2.Size = new Size(131, 15);
            label2.TabIndex = 2;
            label2.Text = "Tables Currently In Area";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(219, 79);
            label3.Name = "label3";
            label3.Size = new Size(129, 15);
            label3.TabIndex = 2;
            label3.Text = "Tables Counted in Stats";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(602, 79);
            label4.Name = "label4";
            label4.Size = new Size(149, 15);
            label4.TabIndex = 2;
            label4.Text = "Tables Excluded From Stats";
            // 
            // btnAddAllToCounted
            // 
            btnAddAllToCounted.Location = new Point(29, 36);
            btnAddAllToCounted.Name = "btnAddAllToCounted";
            btnAddAllToCounted.Size = new Size(138, 29);
            btnAddAllToCounted.TabIndex = 3;
            btnAddAllToCounted.Text = "Add All To Counted";
            btnAddAllToCounted.UseVisualStyleBackColor = true;
            btnAddAllToCounted.Click += btnAddAllToCounted_Click;
            // 
            // lbCountedNotInCurrent
            // 
            lbCountedNotInCurrent.FormattingEnabled = true;
            lbCountedNotInCurrent.ItemHeight = 15;
            lbCountedNotInCurrent.Location = new Point(401, 113);
            lbCountedNotInCurrent.Name = "lbCountedNotInCurrent";
            lbCountedNotInCurrent.Size = new Size(152, 589);
            lbCountedNotInCurrent.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(401, 79);
            label1.Name = "label1";
            label1.Size = new Size(167, 15);
            label1.TabIndex = 2;
            label1.Text = "Tables Counted Not In Current";
            // 
            // btnAddSelected
            // 
            btnAddSelected.Location = new Point(29, 71);
            btnAddSelected.Name = "btnAddSelected";
            btnAddSelected.Size = new Size(138, 29);
            btnAddSelected.TabIndex = 3;
            btnAddSelected.Text = "Add Selected";
            btnAddSelected.UseVisualStyleBackColor = true;
            btnAddSelected.Click += btnAddSelected_Click;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Location = new Point(796, 100);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(75, 23);
            btnSaveChanges.TabIndex = 4;
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // frmManageDiningAreaTables
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(905, 720);
            Controls.Add(btnSaveChanges);
            Controls.Add(btnAddSelected);
            Controls.Add(btnAddAllToCounted);
            Controls.Add(label1);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(lbCountedNotInCurrent);
            Controls.Add(label2);
            Controls.Add(lbTablesExcludedFromStats);
            Controls.Add(lbTablesCountedInStats);
            Controls.Add(lbTablesInArea);
            Controls.Add(lblDiningAreaName);
            Name = "frmManageDiningAreaTables";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmManageDiningAreaTables";
            Load += frmManageDiningAreaTables_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblDiningAreaName;
        private ListBox lbTablesInArea;
        private ListBox lbTablesCountedInStats;
        private ListBox lbTablesExcludedFromStats;
        private Label label2;
        private Label label3;
        private Label label4;
        private Button btnAddAllToCounted;
        private ListBox lbCountedNotInCurrent;
        private Label label1;
        private Button btnAddSelected;
        private Button btnSaveChanges;
    }
}