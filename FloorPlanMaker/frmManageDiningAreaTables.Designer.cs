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
            if (disposing && (components != null)) {
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
            txtExcludedTable = new TextBox();
            btnAddTablesToCountedManual = new Button();
            nudLastTable = new NumericUpDown();
            nudFirstTable = new NumericUpDown();
            txtTableToAdd = new TextBox();
            btnAddExcluded = new Button();
            btnAddRange = new Button();
            ((System.ComponentModel.ISupportInitialize)nudLastTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudFirstTable).BeginInit();
            SuspendLayout();
            // 
            // lblDiningAreaName
            // 
            lblDiningAreaName.Dock = DockStyle.Top;
            lblDiningAreaName.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningAreaName.Location = new Point(0, 0);
            lblDiningAreaName.Name = "lblDiningAreaName";
            lblDiningAreaName.Size = new Size(785, 33);
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
            lbTablesInArea.SelectedIndexChanged += lbTablesInArea_SelectedIndexChanged;
            // 
            // lbTablesCountedInStats
            // 
            lbTablesCountedInStats.FormattingEnabled = true;
            lbTablesCountedInStats.ItemHeight = 15;
            lbTablesCountedInStats.Location = new Point(219, 143);
            lbTablesCountedInStats.Name = "lbTablesCountedInStats";
            lbTablesCountedInStats.Size = new Size(152, 559);
            lbTablesCountedInStats.TabIndex = 1;
            // 
            // lbTablesExcludedFromStats
            // 
            lbTablesExcludedFromStats.FormattingEnabled = true;
            lbTablesExcludedFromStats.ItemHeight = 15;
            lbTablesExcludedFromStats.Location = new Point(602, 143);
            lbTablesExcludedFromStats.Name = "lbTablesExcludedFromStats";
            lbTablesExcludedFromStats.Size = new Size(152, 559);
            lbTablesExcludedFromStats.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(29, 33);
            label2.Name = "label2";
            label2.Size = new Size(131, 15);
            label2.TabIndex = 2;
            label2.Text = "Tables Currently In Area";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(219, 33);
            label3.Name = "label3";
            label3.Size = new Size(129, 15);
            label3.TabIndex = 2;
            label3.Text = "Tables Counted in Stats";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(605, 33);
            label4.Name = "label4";
            label4.Size = new Size(149, 15);
            label4.TabIndex = 2;
            label4.Text = "Tables Excluded From Stats";
            // 
            // btnAddAllToCounted
            // 
            btnAddAllToCounted.Location = new Point(29, 73);
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
            lbCountedNotInCurrent.Location = new Point(401, 143);
            lbCountedNotInCurrent.Name = "lbCountedNotInCurrent";
            lbCountedNotInCurrent.Size = new Size(152, 559);
            lbCountedNotInCurrent.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(401, 33);
            label1.Name = "label1";
            label1.Size = new Size(167, 15);
            label1.TabIndex = 2;
            label1.Text = "Tables Counted Not In Current";
            // 
            // btnAddSelected
            // 
            btnAddSelected.Location = new Point(29, 108);
            btnAddSelected.Name = "btnAddSelected";
            btnAddSelected.Size = new Size(138, 29);
            btnAddSelected.TabIndex = 3;
            btnAddSelected.Text = "Add Selected";
            btnAddSelected.UseVisualStyleBackColor = true;
            btnAddSelected.Click += btnAddSelected_Click;
            // 
            // btnSaveChanges
            // 
            btnSaveChanges.Location = new Point(679, 0);
            btnSaveChanges.Name = "btnSaveChanges";
            btnSaveChanges.Size = new Size(75, 23);
            btnSaveChanges.TabIndex = 4;
            btnSaveChanges.Text = "Save Changes";
            btnSaveChanges.UseVisualStyleBackColor = true;
            btnSaveChanges.Click += btnSaveChanges_Click;
            // 
            // txtExcludedTable
            // 
            txtExcludedTable.Location = new Point(602, 85);
            txtExcludedTable.Name = "txtExcludedTable";
            txtExcludedTable.Size = new Size(152, 23);
            txtExcludedTable.TabIndex = 8;
            // 
            // btnAddTablesToCountedManual
            // 
            btnAddTablesToCountedManual.Location = new Point(219, 114);
            btnAddTablesToCountedManual.Name = "btnAddTablesToCountedManual";
            btnAddTablesToCountedManual.Size = new Size(152, 23);
            btnAddTablesToCountedManual.TabIndex = 5;
            btnAddTablesToCountedManual.Text = "Add to Included";
            btnAddTablesToCountedManual.UseVisualStyleBackColor = true;
            btnAddTablesToCountedManual.Click += btnAddTablesToCountedManual_Click;
            // 
            // nudLastTable
            // 
            nudLastTable.Location = new Point(481, 85);
            nudLastTable.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudLastTable.Name = "nudLastTable";
            nudLastTable.Size = new Size(72, 23);
            nudLastTable.TabIndex = 6;
            // 
            // nudFirstTable
            // 
            nudFirstTable.Location = new Point(401, 85);
            nudFirstTable.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            nudFirstTable.Name = "nudFirstTable";
            nudFirstTable.Size = new Size(75, 23);
            nudFirstTable.TabIndex = 6;
            // 
            // txtTableToAdd
            // 
            txtTableToAdd.Location = new Point(219, 85);
            txtTableToAdd.Name = "txtTableToAdd";
            txtTableToAdd.Size = new Size(152, 23);
            txtTableToAdd.TabIndex = 8;
            // 
            // btnAddExcluded
            // 
            btnAddExcluded.Location = new Point(602, 113);
            btnAddExcluded.Name = "btnAddExcluded";
            btnAddExcluded.Size = new Size(152, 23);
            btnAddExcluded.TabIndex = 5;
            btnAddExcluded.Text = "Add to Excluded";
            btnAddExcluded.UseVisualStyleBackColor = true;
            btnAddExcluded.Click += btnAddExcluded_Click;
            // 
            // btnAddRange
            // 
            btnAddRange.Location = new Point(401, 114);
            btnAddRange.Name = "btnAddRange";
            btnAddRange.Size = new Size(152, 23);
            btnAddRange.TabIndex = 5;
            btnAddRange.Text = "Add Range";
            btnAddRange.UseVisualStyleBackColor = true;
            btnAddRange.Click += btnAddRange_Click;
            // 
            // frmManageDiningAreaTables
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(785, 720);
            Controls.Add(txtTableToAdd);
            Controls.Add(txtExcludedTable);
            Controls.Add(nudFirstTable);
            Controls.Add(nudLastTable);
            Controls.Add(btnAddExcluded);
            Controls.Add(btnAddRange);
            Controls.Add(btnAddTablesToCountedManual);
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
            ((System.ComponentModel.ISupportInitialize)nudLastTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudFirstTable).EndInit();
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
        private TextBox txtExcludedTable;
        private Button btnAddTablesToCountedManual;
        private NumericUpDown nudLastTable;
        private NumericUpDown nudFirstTable;
        private TextBox txtTableToAdd;
        private Button btnAddExcluded;
        private Button btnAddRange;
    }
}