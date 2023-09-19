namespace FloorPlanMaker
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            btnAddServers = new Button();
            label1 = new Label();
            pnlAddTables = new Panel();
            btnCopyTable = new Button();
            btnDeleteTable = new Button();
            btnSaveTable = new Button();
            label7 = new Label();
            txtWidth = new TextBox();
            label6 = new Label();
            txtHeight = new TextBox();
            label5 = new Label();
            txtAverageCovers = new TextBox();
            label4 = new Label();
            txtMaxCovers = new TextBox();
            label3 = new Label();
            txtTableNumber = new TextBox();
            cbLockTables = new CheckBox();
            label2 = new Label();
            pnlFloorPlan = new Panel();
            txtDiningAreaName = new TextBox();
            cbDesignMode = new CheckBox();
            btnCreateNewDiningArea = new Button();
            btnSaveDiningArea = new Button();
            cboDiningAreas = new ComboBox();
            rbInside = new RadioButton();
            rbOutside = new RadioButton();
            btnSaveTables = new Button();
            panel1.SuspendLayout();
            pnlAddTables.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientActiveCaption;
            panel1.Controls.Add(btnAddServers);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(250, 970);
            panel1.TabIndex = 0;
            // 
            // btnAddServers
            // 
            btnAddServers.Location = new Point(3, 47);
            btnAddServers.Name = "btnAddServers";
            btnAddServers.Size = new Size(232, 23);
            btnAddServers.TabIndex = 1;
            btnAddServers.Text = "Add Servers To Shift";
            btnAddServers.UseVisualStyleBackColor = true;
            btnAddServers.Click += btnAddServers_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(54, 9);
            label1.Name = "label1";
            label1.Size = new Size(128, 21);
            label1.TabIndex = 0;
            label1.Text = "Assign Sections";
            // 
            // pnlAddTables
            // 
            pnlAddTables.BackColor = SystemColors.ActiveCaption;
            pnlAddTables.Controls.Add(btnCopyTable);
            pnlAddTables.Controls.Add(btnDeleteTable);
            pnlAddTables.Controls.Add(btnSaveTable);
            pnlAddTables.Controls.Add(label7);
            pnlAddTables.Controls.Add(txtWidth);
            pnlAddTables.Controls.Add(label6);
            pnlAddTables.Controls.Add(txtHeight);
            pnlAddTables.Controls.Add(label5);
            pnlAddTables.Controls.Add(txtAverageCovers);
            pnlAddTables.Controls.Add(label4);
            pnlAddTables.Controls.Add(txtMaxCovers);
            pnlAddTables.Controls.Add(label3);
            pnlAddTables.Controls.Add(txtTableNumber);
            pnlAddTables.Controls.Add(cbLockTables);
            pnlAddTables.Controls.Add(label2);
            pnlAddTables.Dock = DockStyle.Left;
            pnlAddTables.Location = new Point(250, 0);
            pnlAddTables.Name = "pnlAddTables";
            pnlAddTables.Size = new Size(250, 970);
            pnlAddTables.TabIndex = 1;
            // 
            // btnCopyTable
            // 
            btnCopyTable.Location = new Point(28, 508);
            btnCopyTable.Name = "btnCopyTable";
            btnCopyTable.Size = new Size(206, 23);
            btnCopyTable.TabIndex = 7;
            btnCopyTable.Text = "Copy Table";
            btnCopyTable.UseVisualStyleBackColor = true;
            btnCopyTable.Click += btnCopyTable_Click;
            // 
            // btnDeleteTable
            // 
            btnDeleteTable.Location = new Point(28, 924);
            btnDeleteTable.Name = "btnDeleteTable";
            btnDeleteTable.Size = new Size(206, 23);
            btnDeleteTable.TabIndex = 6;
            btnDeleteTable.Text = "Delete Table";
            btnDeleteTable.UseVisualStyleBackColor = true;
            btnDeleteTable.Click += btnDeleteTable_Click;
            // 
            // btnSaveTable
            // 
            btnSaveTable.Location = new Point(28, 859);
            btnSaveTable.Name = "btnSaveTable";
            btnSaveTable.Size = new Size(206, 37);
            btnSaveTable.TabIndex = 5;
            btnSaveTable.Text = "Save Table";
            btnSaveTable.UseVisualStyleBackColor = true;
            btnSaveTable.Click += btnSaveTable_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(77, 761);
            label7.Name = "label7";
            label7.Size = new Size(39, 15);
            label7.TabIndex = 4;
            label7.Text = "Width";
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(77, 779);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(107, 23);
            txtWidth.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(77, 703);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 4;
            label6.Text = "Height";
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(77, 721);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(107, 23);
            txtHeight.TabIndex = 3;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(77, 649);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 4;
            label5.Text = "Average Covers";
            // 
            // txtAverageCovers
            // 
            txtAverageCovers.Location = new Point(77, 667);
            txtAverageCovers.Name = "txtAverageCovers";
            txtAverageCovers.Size = new Size(107, 23);
            txtAverageCovers.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(77, 592);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 4;
            label4.Text = "Max Covers";
            // 
            // txtMaxCovers
            // 
            txtMaxCovers.Location = new Point(77, 610);
            txtMaxCovers.Name = "txtMaxCovers";
            txtMaxCovers.Size = new Size(107, 23);
            txtMaxCovers.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(77, 538);
            label3.Name = "label3";
            label3.Size = new Size(81, 15);
            label3.TabIndex = 4;
            label3.Text = "Table Number";
            // 
            // txtTableNumber
            // 
            txtTableNumber.Location = new Point(77, 556);
            txtTableNumber.Name = "txtTableNumber";
            txtTableNumber.Size = new Size(107, 23);
            txtTableNumber.TabIndex = 3;
            // 
            // cbLockTables
            // 
            cbLockTables.Appearance = Appearance.Button;
            cbLockTables.Location = new Point(28, 457);
            cbLockTables.Name = "cbLockTables";
            cbLockTables.Size = new Size(206, 45);
            cbLockTables.TabIndex = 2;
            cbLockTables.Text = "Lock Tables";
            cbLockTables.TextAlign = ContentAlignment.MiddleCenter;
            cbLockTables.UseVisualStyleBackColor = true;
            cbLockTables.CheckedChanged += cbLockTables_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(77, 9);
            label2.Name = "label2";
            label2.Size = new Size(93, 21);
            label2.TabIndex = 0;
            label2.Text = "Add Tables";
            // 
            // pnlFloorPlan
            // 
            pnlFloorPlan.BackColor = Color.FromArgb(255, 244, 232);
            pnlFloorPlan.Location = new Point(540, 77);
            pnlFloorPlan.Name = "pnlFloorPlan";
            pnlFloorPlan.Size = new Size(670, 870);
            pnlFloorPlan.TabIndex = 2;
            // 
            // txtDiningAreaName
            // 
            txtDiningAreaName.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtDiningAreaName.Location = new Point(719, 37);
            txtDiningAreaName.Name = "txtDiningAreaName";
            txtDiningAreaName.Size = new Size(303, 35);
            txtDiningAreaName.TabIndex = 3;
            // 
            // cbDesignMode
            // 
            cbDesignMode.Appearance = Appearance.Button;
            cbDesignMode.FlatStyle = FlatStyle.Flat;
            cbDesignMode.Location = new Point(540, 47);
            cbDesignMode.Name = "cbDesignMode";
            cbDesignMode.Size = new Size(105, 24);
            cbDesignMode.TabIndex = 4;
            cbDesignMode.Text = "Edit Dining Area";
            cbDesignMode.TextAlign = ContentAlignment.MiddleCenter;
            cbDesignMode.UseVisualStyleBackColor = true;
            cbDesignMode.CheckedChanged += cbDesignMode_CheckedChanged;
            // 
            // btnCreateNewDiningArea
            // 
            btnCreateNewDiningArea.Location = new Point(540, 14);
            btnCreateNewDiningArea.Name = "btnCreateNewDiningArea";
            btnCreateNewDiningArea.Size = new Size(154, 23);
            btnCreateNewDiningArea.TabIndex = 5;
            btnCreateNewDiningArea.Text = "Create New Dining Area";
            btnCreateNewDiningArea.UseVisualStyleBackColor = true;
            // 
            // btnSaveDiningArea
            // 
            btnSaveDiningArea.Location = new Point(1130, 12);
            btnSaveDiningArea.Name = "btnSaveDiningArea";
            btnSaveDiningArea.Size = new Size(111, 32);
            btnSaveDiningArea.TabIndex = 6;
            btnSaveDiningArea.Text = "Save";
            btnSaveDiningArea.UseVisualStyleBackColor = true;
            btnSaveDiningArea.Click += btnSaveDiningArea_Click;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(719, 10);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(303, 23);
            cboDiningAreas.TabIndex = 7;
            cboDiningAreas.SelectedIndexChanged += cboDiningAreas_SelectedIndexChanged;
            // 
            // rbInside
            // 
            rbInside.AutoSize = true;
            rbInside.Location = new Point(1030, 28);
            rbInside.Name = "rbInside";
            rbInside.Size = new Size(56, 19);
            rbInside.TabIndex = 8;
            rbInside.TabStop = true;
            rbInside.Text = "Inside";
            rbInside.UseVisualStyleBackColor = true;
            // 
            // rbOutside
            // 
            rbOutside.AutoSize = true;
            rbOutside.Location = new Point(1030, 53);
            rbOutside.Name = "rbOutside";
            rbOutside.Size = new Size(66, 19);
            rbOutside.TabIndex = 8;
            rbOutside.TabStop = true;
            rbOutside.Text = "Outside";
            rbOutside.UseVisualStyleBackColor = true;
            // 
            // btnSaveTables
            // 
            btnSaveTables.Location = new Point(1130, 47);
            btnSaveTables.Name = "btnSaveTables";
            btnSaveTables.Size = new Size(111, 23);
            btnSaveTables.TabIndex = 9;
            btnSaveTables.Text = "Save Tables";
            btnSaveTables.UseVisualStyleBackColor = true;
            btnSaveTables.Click += btnSaveTables_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 970);
            Controls.Add(btnSaveTables);
            Controls.Add(rbOutside);
            Controls.Add(rbInside);
            Controls.Add(cboDiningAreas);
            Controls.Add(btnSaveDiningArea);
            Controls.Add(btnCreateNewDiningArea);
            Controls.Add(cbDesignMode);
            Controls.Add(txtDiningAreaName);
            Controls.Add(pnlFloorPlan);
            Controls.Add(pnlAddTables);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            pnlAddTables.ResumeLayout(false);
            pnlAddTables.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label label1;
        private Panel pnlAddTables;
        private Label label2;
        private Panel pnlFloorPlan;
        private TextBox txtDiningAreaName;
        private CheckBox cbDesignMode;
        private Button btnCreateNewDiningArea;
        private Button btnSaveDiningArea;
        private ComboBox cboDiningAreas;
        private RadioButton rbInside;
        private RadioButton rbOutside;
        private Button btnSaveTables;
        private CheckBox cbLockTables;
        private Label label7;
        private TextBox txtWidth;
        private Label label6;
        private TextBox txtHeight;
        private Label label5;
        private TextBox txtAverageCovers;
        private Label label4;
        private TextBox txtMaxCovers;
        private Label label3;
        private TextBox txtTableNumber;
        private Button btnSaveTable;
        private Button btnDeleteTable;
        private Button btnCopyTable;
        private Button btnAddServers;
    }
}