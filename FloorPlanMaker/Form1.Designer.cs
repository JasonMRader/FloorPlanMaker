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
            pnlServers = new Panel();
            btnAddServers = new Button();
            label1 = new Label();
            pnlAddTables = new Panel();
            pnlSections = new Panel();
            cbLockNodes = new CheckBox();
            flowSectionSelect = new FlowLayoutPanel();
            lblTeamWaitLabel = new Label();
            nudNumberOfTeamWaits = new NumericUpDown();
            cbTeamWait = new CheckBox();
            lblServerAverageCovers = new Label();
            lblServerMaxCovers = new Label();
            label11 = new Label();
            label10 = new Label();
            nudServerCount = new NumericUpDown();
            label9 = new Label();
            lblDiningAreaMaxCovers = new Label();
            lblDiningAreaAverageCovers = new Label();
            label2 = new Label();
            label8 = new Label();
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
            lblPanel2Text = new Label();
            pnlFloorPlan = new Panel();
            txtDiningAreaName = new TextBox();
            btnCreateNewDiningArea = new Button();
            btnSaveDiningArea = new Button();
            cboDiningAreas = new ComboBox();
            rbInside = new RadioButton();
            rbOutside = new RadioButton();
            btnSaveTables = new Button();
            rdoSections = new RadioButton();
            rdoDiningAreas = new RadioButton();
            panel1 = new Panel();
            pnlServers.SuspendLayout();
            pnlAddTables.SuspendLayout();
            pnlSections.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfTeamWaits).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudServerCount).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlServers
            // 
            pnlServers.BackColor = SystemColors.GradientActiveCaption;
            pnlServers.Controls.Add(btnAddServers);
            pnlServers.Controls.Add(label1);
            pnlServers.Dock = DockStyle.Left;
            pnlServers.Location = new Point(0, 0);
            pnlServers.Name = "pnlServers";
            pnlServers.Size = new Size(250, 970);
            pnlServers.TabIndex = 0;
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
            pnlAddTables.Controls.Add(pnlSections);
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
            pnlAddTables.Controls.Add(lblPanel2Text);
            pnlAddTables.Dock = DockStyle.Left;
            pnlAddTables.Location = new Point(250, 0);
            pnlAddTables.Name = "pnlAddTables";
            pnlAddTables.Size = new Size(250, 970);
            pnlAddTables.TabIndex = 1;
            // 
            // pnlSections
            // 
            pnlSections.Controls.Add(cbLockNodes);
            pnlSections.Controls.Add(flowSectionSelect);
            pnlSections.Controls.Add(lblTeamWaitLabel);
            pnlSections.Controls.Add(nudNumberOfTeamWaits);
            pnlSections.Controls.Add(cbTeamWait);
            pnlSections.Controls.Add(lblServerAverageCovers);
            pnlSections.Controls.Add(lblServerMaxCovers);
            pnlSections.Controls.Add(label11);
            pnlSections.Controls.Add(label10);
            pnlSections.Controls.Add(nudServerCount);
            pnlSections.Controls.Add(label9);
            pnlSections.Controls.Add(lblDiningAreaMaxCovers);
            pnlSections.Controls.Add(lblDiningAreaAverageCovers);
            pnlSections.Controls.Add(label2);
            pnlSections.Controls.Add(label8);
            pnlSections.Location = new Point(0, 37);
            pnlSections.Name = "pnlSections";
            pnlSections.Size = new Size(247, 930);
            pnlSections.TabIndex = 8;
            // 
            // cbLockNodes
            // 
            cbLockNodes.Appearance = Appearance.Button;
            cbLockNodes.Location = new Point(3, 284);
            cbLockNodes.Name = "cbLockNodes";
            cbLockNodes.Size = new Size(241, 25);
            cbLockNodes.TabIndex = 11;
            cbLockNodes.Text = "Draw Section Lines";
            cbLockNodes.TextAlign = ContentAlignment.MiddleCenter;
            cbLockNodes.UseVisualStyleBackColor = true;
            cbLockNodes.CheckedChanged += cbLockNodes_CheckedChanged;
            // 
            // flowSectionSelect
            // 
            flowSectionSelect.Location = new Point(0, 315);
            flowSectionSelect.Name = "flowSectionSelect";
            flowSectionSelect.Size = new Size(247, 612);
            flowSectionSelect.TabIndex = 9;
            // 
            // lblTeamWaitLabel
            // 
            lblTeamWaitLabel.AutoSize = true;
            lblTeamWaitLabel.Location = new Point(46, 257);
            lblTeamWaitLabel.Name = "lblTeamWaitLabel";
            lblTeamWaitLabel.Size = new Size(70, 15);
            lblTeamWaitLabel.TabIndex = 8;
            lblTeamWaitLabel.Text = "How Many?";
            lblTeamWaitLabel.Visible = false;
            // 
            // nudNumberOfTeamWaits
            // 
            nudNumberOfTeamWaits.Location = new Point(132, 255);
            nudNumberOfTeamWaits.Name = "nudNumberOfTeamWaits";
            nudNumberOfTeamWaits.Size = new Size(46, 23);
            nudNumberOfTeamWaits.TabIndex = 7;
            nudNumberOfTeamWaits.Visible = false;
            nudNumberOfTeamWaits.ValueChanged += nudNumberOfTeamWaits_ValueChanged;
            // 
            // cbTeamWait
            // 
            cbTeamWait.AutoSize = true;
            cbTeamWait.Location = new Point(29, 219);
            cbTeamWait.Name = "cbTeamWait";
            cbTeamWait.Size = new Size(155, 19);
            cbTeamWait.TabIndex = 6;
            cbTeamWait.Text = "Add TeamWait Sections?";
            cbTeamWait.UseVisualStyleBackColor = true;
            cbTeamWait.CheckedChanged += cbTeamWait_CheckedChanged;
            // 
            // lblServerAverageCovers
            // 
            lblServerAverageCovers.AutoSize = true;
            lblServerAverageCovers.Location = new Point(132, 171);
            lblServerAverageCovers.Name = "lblServerAverageCovers";
            lblServerAverageCovers.Size = new Size(13, 15);
            lblServerAverageCovers.TabIndex = 5;
            lblServerAverageCovers.Text = "0";
            // 
            // lblServerMaxCovers
            // 
            lblServerMaxCovers.AutoSize = true;
            lblServerMaxCovers.Location = new Point(132, 136);
            lblServerMaxCovers.Name = "lblServerMaxCovers";
            lblServerMaxCovers.Size = new Size(13, 15);
            lblServerMaxCovers.TabIndex = 5;
            lblServerMaxCovers.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(28, 171);
            label11.Name = "label11";
            label11.Size = new Size(86, 15);
            label11.TabIndex = 4;
            label11.Text = "Avg Per Server:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(28, 136);
            label10.Name = "label10";
            label10.Size = new Size(88, 15);
            label10.TabIndex = 4;
            label10.Text = "Max Per Server:";
            // 
            // nudServerCount
            // 
            nudServerCount.Location = new Point(132, 96);
            nudServerCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudServerCount.Name = "nudServerCount";
            nudServerCount.Size = new Size(46, 23);
            nudServerCount.TabIndex = 3;
            nudServerCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudServerCount.ValueChanged += nudServerCount_ValueChanged;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(69, 98);
            label9.Name = "label9";
            label9.Size = new Size(47, 15);
            label9.TabIndex = 2;
            label9.Text = "Servers:";
            // 
            // lblDiningAreaMaxCovers
            // 
            lblDiningAreaMaxCovers.AutoSize = true;
            lblDiningAreaMaxCovers.Location = new Point(132, 29);
            lblDiningAreaMaxCovers.Name = "lblDiningAreaMaxCovers";
            lblDiningAreaMaxCovers.Size = new Size(13, 15);
            lblDiningAreaMaxCovers.TabIndex = 1;
            lblDiningAreaMaxCovers.Text = "0";
            // 
            // lblDiningAreaAverageCovers
            // 
            lblDiningAreaAverageCovers.AutoSize = true;
            lblDiningAreaAverageCovers.Location = new Point(132, 64);
            lblDiningAreaAverageCovers.Name = "lblDiningAreaAverageCovers";
            lblDiningAreaAverageCovers.Size = new Size(13, 15);
            lblDiningAreaAverageCovers.TabIndex = 0;
            lblDiningAreaAverageCovers.Text = "0";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 64);
            label2.Name = "label2";
            label2.Size = new Size(92, 15);
            label2.TabIndex = 0;
            label2.Text = "Average Covers:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(44, 29);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 0;
            label8.Text = "Max Covers:";
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
            // lblPanel2Text
            // 
            lblPanel2Text.AutoSize = true;
            lblPanel2Text.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblPanel2Text.Location = new Point(77, 9);
            lblPanel2Text.Name = "lblPanel2Text";
            lblPanel2Text.Size = new Size(93, 21);
            lblPanel2Text.TabIndex = 0;
            lblPanel2Text.Text = "Add Tables";
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
            txtDiningAreaName.Location = new Point(734, 37);
            txtDiningAreaName.Name = "txtDiningAreaName";
            txtDiningAreaName.Size = new Size(228, 35);
            txtDiningAreaName.TabIndex = 3;
            txtDiningAreaName.Visible = false;
            // 
            // btnCreateNewDiningArea
            // 
            btnCreateNewDiningArea.Location = new Point(968, 8);
            btnCreateNewDiningArea.Name = "btnCreateNewDiningArea";
            btnCreateNewDiningArea.Size = new Size(154, 23);
            btnCreateNewDiningArea.TabIndex = 5;
            btnCreateNewDiningArea.Text = "Create New Dining Area";
            btnCreateNewDiningArea.UseVisualStyleBackColor = true;
            btnCreateNewDiningArea.Visible = false;
            // 
            // btnSaveDiningArea
            // 
            btnSaveDiningArea.Location = new Point(1160, 12);
            btnSaveDiningArea.Name = "btnSaveDiningArea";
            btnSaveDiningArea.Size = new Size(81, 32);
            btnSaveDiningArea.TabIndex = 6;
            btnSaveDiningArea.Text = "Save";
            btnSaveDiningArea.UseVisualStyleBackColor = true;
            btnSaveDiningArea.Visible = false;
            btnSaveDiningArea.Click += btnSaveDiningArea_Click;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(734, 4);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(228, 23);
            cboDiningAreas.TabIndex = 7;
            cboDiningAreas.SelectedIndexChanged += cboDiningAreas_SelectedIndexChanged;
            // 
            // rbInside
            // 
            rbInside.AutoSize = true;
            rbInside.Location = new Point(968, 37);
            rbInside.Name = "rbInside";
            rbInside.Size = new Size(56, 19);
            rbInside.TabIndex = 8;
            rbInside.TabStop = true;
            rbInside.Text = "Inside";
            rbInside.UseVisualStyleBackColor = true;
            rbInside.Visible = false;
            // 
            // rbOutside
            // 
            rbOutside.AutoSize = true;
            rbOutside.Location = new Point(968, 53);
            rbOutside.Name = "rbOutside";
            rbOutside.Size = new Size(66, 19);
            rbOutside.TabIndex = 8;
            rbOutside.TabStop = true;
            rbOutside.Text = "Outside";
            rbOutside.UseVisualStyleBackColor = true;
            rbOutside.Visible = false;
            // 
            // btnSaveTables
            // 
            btnSaveTables.Location = new Point(1160, 47);
            btnSaveTables.Name = "btnSaveTables";
            btnSaveTables.Size = new Size(81, 23);
            btnSaveTables.TabIndex = 9;
            btnSaveTables.Text = "Save Tables";
            btnSaveTables.UseVisualStyleBackColor = true;
            btnSaveTables.Visible = false;
            btnSaveTables.Click += btnSaveTables_Click;
            // 
            // rdoSections
            // 
            rdoSections.Appearance = Appearance.Button;
            rdoSections.Checked = true;
            rdoSections.FlatStyle = FlatStyle.Flat;
            rdoSections.Location = new Point(0, 3);
            rdoSections.Name = "rdoSections";
            rdoSections.Size = new Size(104, 24);
            rdoSections.TabIndex = 10;
            rdoSections.TabStop = true;
            rdoSections.Text = "Create Sections";
            rdoSections.TextAlign = ContentAlignment.MiddleCenter;
            rdoSections.UseVisualStyleBackColor = true;
            rdoSections.CheckedChanged += rdoSections_CheckedChanged;
            // 
            // rdoDiningAreas
            // 
            rdoDiningAreas.Appearance = Appearance.Button;
            rdoDiningAreas.FlatStyle = FlatStyle.Flat;
            rdoDiningAreas.Location = new Point(110, 3);
            rdoDiningAreas.Name = "rdoDiningAreas";
            rdoDiningAreas.Size = new Size(112, 24);
            rdoDiningAreas.TabIndex = 10;
            rdoDiningAreas.Text = "Edit Dining Areas";
            rdoDiningAreas.TextAlign = ContentAlignment.MiddleCenter;
            rdoDiningAreas.UseVisualStyleBackColor = true;
            rdoDiningAreas.CheckedChanged += rdoDiningAreas_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoSections);
            panel1.Controls.Add(rdoDiningAreas);
            panel1.Location = new Point(503, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(225, 36);
            panel1.TabIndex = 11;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1253, 970);
            Controls.Add(panel1);
            Controls.Add(btnSaveTables);
            Controls.Add(rbOutside);
            Controls.Add(rbInside);
            Controls.Add(cboDiningAreas);
            Controls.Add(btnSaveDiningArea);
            Controls.Add(btnCreateNewDiningArea);
            Controls.Add(txtDiningAreaName);
            Controls.Add(pnlFloorPlan);
            Controls.Add(pnlAddTables);
            Controls.Add(pnlServers);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            pnlServers.ResumeLayout(false);
            pnlServers.PerformLayout();
            pnlAddTables.ResumeLayout(false);
            pnlAddTables.PerformLayout();
            pnlSections.ResumeLayout(false);
            pnlSections.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfTeamWaits).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudServerCount).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlServers;
        private Label label1;
        private Panel pnlAddTables;
        private Label lblPanel2Text;
        private Panel pnlFloorPlan;
        private TextBox txtDiningAreaName;
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
        private Panel pnlSections;
        private Label label8;
        private Label lblDiningAreaMaxCovers;
        private Label lblServerAverageCovers;
        private Label lblServerMaxCovers;
        private Label label11;
        private Label label10;
        private NumericUpDown nudServerCount;
        private Label label9;
        private Label lblDiningAreaAverageCovers;
        private Label label2;
        private Label lblTeamWaitLabel;
        private NumericUpDown nudNumberOfTeamWaits;
        private CheckBox cbTeamWait;
        private FlowLayoutPanel flowSectionSelect;
        private CheckBox cbLockNodes;
        private RadioButton rdoSections;
        private RadioButton rdoDiningAreas;
        private Panel panel1;
    }
}