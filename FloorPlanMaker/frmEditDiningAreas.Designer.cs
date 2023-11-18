namespace FloorPlanMakerUI
{
    partial class frmEditDiningAreas
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
            pnlFloorPlan = new Panel();
            btnCreateNewDiningArea = new Button();
            btnSaveDiningArea = new Button();
            rbInside = new RadioButton();
            rbOutside = new RadioButton();
            btnCopyTable = new Button();
            btnSaveTable = new Button();
            btnDeleteTable = new Button();
            btnMoreWidth = new Button();
            btnMoreHeight = new Button();
            btnLessHeight = new Button();
            btnLessWidth = new Button();
            panel2 = new Panel();
            label6 = new Label();
            cbViewMode = new CheckBox();
            txtYco = new TextBox();
            btnQuickEdit = new Button();
            cbLockTables = new CheckBox();
            txtXco = new TextBox();
            label9 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtWidth = new TextBox();
            txtTableNumber = new TextBox();
            txtMaxCovers = new TextBox();
            txtAverageCovers = new TextBox();
            txtHeight = new TextBox();
            panel3 = new Panel();
            panel6 = new Panel();
            cbTemporaryFloorplan = new CheckBox();
            txtDiningAreaName = new TextBox();
            panel4 = new Panel();
            rdoDefaultView = new RadioButton();
            rdoEditPositions = new RadioButton();
            rdoEditData = new RadioButton();
            cboDiningAreas = new ComboBox();
            panel5 = new Panel();
            label7 = new Label();
            btnAddCircle = new Button();
            btnAddDiamond = new Button();
            btnAddSquare = new Button();
            panel1 = new Panel();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFloorPlan
            // 
            pnlFloorPlan.BackColor = Color.WhiteSmoke;
            pnlFloorPlan.Location = new Point(13, 54);
            pnlFloorPlan.Name = "pnlFloorPlan";
            pnlFloorPlan.Size = new Size(672, 877);
            pnlFloorPlan.TabIndex = 0;
            // 
            // btnCreateNewDiningArea
            // 
            btnCreateNewDiningArea.BackColor = Color.FromArgb(158, 171, 222);
            btnCreateNewDiningArea.FlatAppearance.BorderSize = 0;
            btnCreateNewDiningArea.FlatStyle = FlatStyle.Flat;
            btnCreateNewDiningArea.Image = Properties.Resources.addSmall;
            btnCreateNewDiningArea.Location = new Point(646, 16);
            btnCreateNewDiningArea.Name = "btnCreateNewDiningArea";
            btnCreateNewDiningArea.Size = new Size(39, 32);
            btnCreateNewDiningArea.TabIndex = 1;
            btnCreateNewDiningArea.UseVisualStyleBackColor = false;
            btnCreateNewDiningArea.Click += btnCreateNewDiningArea_Click;
            // 
            // btnSaveDiningArea
            // 
            btnSaveDiningArea.BackColor = Color.FromArgb(158, 171, 222);
            btnSaveDiningArea.Enabled = false;
            btnSaveDiningArea.FlatAppearance.BorderSize = 0;
            btnSaveDiningArea.FlatStyle = FlatStyle.Flat;
            btnSaveDiningArea.Location = new Point(7, 278);
            btnSaveDiningArea.Name = "btnSaveDiningArea";
            btnSaveDiningArea.Size = new Size(152, 33);
            btnSaveDiningArea.TabIndex = 1;
            btnSaveDiningArea.Text = "Save";
            btnSaveDiningArea.UseVisualStyleBackColor = false;
            btnSaveDiningArea.Click += btnSaveDiningArea_Click;
            // 
            // rbInside
            // 
            rbInside.AutoSize = true;
            rbInside.Checked = true;
            rbInside.Enabled = false;
            rbInside.ForeColor = Color.White;
            rbInside.Location = new Point(12, 159);
            rbInside.Name = "rbInside";
            rbInside.Size = new Size(56, 19);
            rbInside.TabIndex = 2;
            rbInside.TabStop = true;
            rbInside.Text = "Inside";
            rbInside.UseVisualStyleBackColor = true;
            // 
            // rbOutside
            // 
            rbOutside.AutoSize = true;
            rbOutside.Enabled = false;
            rbOutside.ForeColor = Color.White;
            rbOutside.Location = new Point(88, 159);
            rbOutside.Name = "rbOutside";
            rbOutside.Size = new Size(66, 19);
            rbOutside.TabIndex = 2;
            rbOutside.Text = "Outside";
            rbOutside.UseVisualStyleBackColor = true;
            // 
            // btnCopyTable
            // 
            btnCopyTable.BackColor = Color.FromArgb(158, 171, 222);
            btnCopyTable.FlatAppearance.BorderSize = 0;
            btnCopyTable.FlatStyle = FlatStyle.Flat;
            btnCopyTable.Location = new Point(17, 806);
            btnCopyTable.Name = "btnCopyTable";
            btnCopyTable.Size = new Size(141, 23);
            btnCopyTable.TabIndex = 1;
            btnCopyTable.Text = "Copy Table";
            btnCopyTable.UseVisualStyleBackColor = false;
            btnCopyTable.Click += btnCopyTable_Click;
            // 
            // btnSaveTable
            // 
            btnSaveTable.BackColor = Color.FromArgb(158, 171, 222);
            btnSaveTable.FlatAppearance.BorderSize = 0;
            btnSaveTable.FlatStyle = FlatStyle.Flat;
            btnSaveTable.Location = new Point(18, 750);
            btnSaveTable.Name = "btnSaveTable";
            btnSaveTable.Size = new Size(141, 23);
            btnSaveTable.TabIndex = 1;
            btnSaveTable.Text = "Save Table";
            btnSaveTable.UseVisualStyleBackColor = false;
            btnSaveTable.Click += btnSaveTable_Click;
            // 
            // btnDeleteTable
            // 
            btnDeleteTable.BackColor = Color.FromArgb(158, 171, 222);
            btnDeleteTable.FlatAppearance.BorderSize = 0;
            btnDeleteTable.FlatStyle = FlatStyle.Flat;
            btnDeleteTable.Location = new Point(18, 857);
            btnDeleteTable.Name = "btnDeleteTable";
            btnDeleteTable.Size = new Size(141, 23);
            btnDeleteTable.TabIndex = 1;
            btnDeleteTable.Text = "Delete Table";
            btnDeleteTable.UseVisualStyleBackColor = false;
            btnDeleteTable.Click += btnDeleteTable_Click;
            // 
            // btnMoreWidth
            // 
            btnMoreWidth.BackColor = Color.FromArgb(158, 171, 222);
            btnMoreWidth.FlatAppearance.BorderSize = 0;
            btnMoreWidth.FlatStyle = FlatStyle.Flat;
            btnMoreWidth.Location = new Point(134, 601);
            btnMoreWidth.Name = "btnMoreWidth";
            btnMoreWidth.Size = new Size(24, 23);
            btnMoreWidth.TabIndex = 1;
            btnMoreWidth.Text = "+";
            btnMoreWidth.UseVisualStyleBackColor = false;
            btnMoreWidth.Click += btnMoreWidth_Click;
            // 
            // btnMoreHeight
            // 
            btnMoreHeight.BackColor = Color.FromArgb(158, 171, 222);
            btnMoreHeight.FlatAppearance.BorderSize = 0;
            btnMoreHeight.FlatStyle = FlatStyle.Flat;
            btnMoreHeight.Location = new Point(134, 527);
            btnMoreHeight.Name = "btnMoreHeight";
            btnMoreHeight.Size = new Size(24, 23);
            btnMoreHeight.TabIndex = 1;
            btnMoreHeight.Text = "+";
            btnMoreHeight.UseVisualStyleBackColor = false;
            btnMoreHeight.Click += btnMoreHeight_Click;
            // 
            // btnLessHeight
            // 
            btnLessHeight.BackColor = Color.FromArgb(158, 171, 222);
            btnLessHeight.FlatAppearance.BorderSize = 0;
            btnLessHeight.FlatStyle = FlatStyle.Flat;
            btnLessHeight.Location = new Point(17, 527);
            btnLessHeight.Name = "btnLessHeight";
            btnLessHeight.Size = new Size(24, 23);
            btnLessHeight.TabIndex = 1;
            btnLessHeight.Text = "-";
            btnLessHeight.UseVisualStyleBackColor = false;
            btnLessHeight.Click += btnLessHeight_Click;
            // 
            // btnLessWidth
            // 
            btnLessWidth.BackColor = Color.FromArgb(158, 171, 222);
            btnLessWidth.FlatAppearance.BorderSize = 0;
            btnLessWidth.FlatStyle = FlatStyle.Flat;
            btnLessWidth.Location = new Point(17, 601);
            btnLessWidth.Name = "btnLessWidth";
            btnLessWidth.Size = new Size(24, 23);
            btnLessWidth.TabIndex = 1;
            btnLessWidth.Text = "-";
            btnLessWidth.UseVisualStyleBackColor = false;
            btnLessWidth.Click += btnLessWidth_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.WhiteSmoke;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(cbViewMode);
            panel2.Controls.Add(txtYco);
            panel2.Controls.Add(btnQuickEdit);
            panel2.Controls.Add(cbLockTables);
            panel2.Controls.Add(txtXco);
            panel2.Controls.Add(label9);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txtWidth);
            panel2.Controls.Add(txtTableNumber);
            panel2.Controls.Add(txtMaxCovers);
            panel2.Controls.Add(txtAverageCovers);
            panel2.Controls.Add(txtHeight);
            panel2.Controls.Add(btnCopyTable);
            panel2.Controls.Add(btnSaveTable);
            panel2.Controls.Add(btnDeleteTable);
            panel2.Controls.Add(btnMoreWidth);
            panel2.Controls.Add(btnLessWidth);
            panel2.Controls.Add(btnMoreHeight);
            panel2.Controls.Add(btnLessHeight);
            panel2.Location = new Point(31, 19);
            panel2.Name = "panel2";
            panel2.Size = new Size(167, 901);
            panel2.TabIndex = 3;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(10, 229);
            label6.Name = "label6";
            label6.Size = new Size(149, 25);
            label6.TabIndex = 6;
            label6.Text = "Table Properties";
            // 
            // cbViewMode
            // 
            cbViewMode.Appearance = Appearance.Button;
            cbViewMode.BackColor = Color.FromArgb(158, 171, 222);
            cbViewMode.FlatAppearance.BorderSize = 0;
            cbViewMode.FlatStyle = FlatStyle.Flat;
            cbViewMode.Location = new Point(32, 69);
            cbViewMode.Name = "cbViewMode";
            cbViewMode.Size = new Size(104, 23);
            cbViewMode.TabIndex = 1;
            cbViewMode.Text = "Toggle View";
            cbViewMode.TextAlign = ContentAlignment.MiddleCenter;
            cbViewMode.UseVisualStyleBackColor = false;
            cbViewMode.CheckedChanged += cbViewMode_CheckedChanged;
            // 
            // txtYco
            // 
            txtYco.Location = new Point(93, 707);
            txtYco.Name = "txtYco";
            txtYco.PlaceholderText = "Y";
            txtYco.Size = new Size(35, 23);
            txtYco.TabIndex = 5;
            // 
            // btnQuickEdit
            // 
            btnQuickEdit.BackColor = Color.FromArgb(158, 171, 222);
            btnQuickEdit.FlatAppearance.BorderSize = 0;
            btnQuickEdit.FlatStyle = FlatStyle.Flat;
            btnQuickEdit.Image = Properties.Resources.SmallChair;
            btnQuickEdit.ImageAlign = ContentAlignment.TopCenter;
            btnQuickEdit.Location = new Point(18, 110);
            btnQuickEdit.Name = "btnQuickEdit";
            btnQuickEdit.Size = new Size(122, 29);
            btnQuickEdit.TabIndex = 5;
            btnQuickEdit.Text = "Default View";
            btnQuickEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnQuickEdit.UseVisualStyleBackColor = false;
            btnQuickEdit.Click += btnQuickEdit_Click;
            // 
            // cbLockTables
            // 
            cbLockTables.Appearance = Appearance.Button;
            cbLockTables.BackColor = Color.FromArgb(158, 171, 222);
            cbLockTables.Checked = true;
            cbLockTables.CheckState = CheckState.Checked;
            cbLockTables.FlatAppearance.BorderSize = 0;
            cbLockTables.FlatStyle = FlatStyle.Flat;
            cbLockTables.Location = new Point(18, 24);
            cbLockTables.Name = "cbLockTables";
            cbLockTables.Size = new Size(122, 24);
            cbLockTables.TabIndex = 4;
            cbLockTables.Text = "Unlock Tables";
            cbLockTables.TextAlign = ContentAlignment.MiddleCenter;
            cbLockTables.UseVisualStyleBackColor = false;
            cbLockTables.CheckedChanged += cbLockTables_CheckedChanged;
            // 
            // txtXco
            // 
            txtXco.Location = new Point(47, 707);
            txtXco.Name = "txtXco";
            txtXco.PlaceholderText = "X";
            txtXco.Size = new Size(35, 23);
            txtXco.TabIndex = 5;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(47, 689);
            label9.Name = "label9";
            label9.Size = new Size(53, 15);
            label9.TabIndex = 3;
            label9.Text = "Location";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(47, 584);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 3;
            label5.Text = "Width";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(47, 508);
            label4.Name = "label4";
            label4.Size = new Size(43, 15);
            label4.TabIndex = 3;
            label4.Text = "Height";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(47, 434);
            label3.Name = "label3";
            label3.Size = new Size(89, 15);
            label3.TabIndex = 3;
            label3.Text = "Average Covers";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(47, 362);
            label2.Name = "label2";
            label2.Size = new Size(69, 15);
            label2.TabIndex = 3;
            label2.Text = "Max Covers";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(47, 289);
            label1.Name = "label1";
            label1.Size = new Size(81, 15);
            label1.TabIndex = 3;
            label1.Text = "Table Number";
            // 
            // txtWidth
            // 
            txtWidth.BorderStyle = BorderStyle.None;
            txtWidth.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtWidth.Location = new Point(47, 602);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(81, 22);
            txtWidth.TabIndex = 2;
            txtWidth.Validated += RefreshTableControl;
            // 
            // txtTableNumber
            // 
            txtTableNumber.BorderStyle = BorderStyle.None;
            txtTableNumber.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtTableNumber.Location = new Point(47, 307);
            txtTableNumber.Name = "txtTableNumber";
            txtTableNumber.Size = new Size(81, 22);
            txtTableNumber.TabIndex = 2;
            txtTableNumber.TextChanged += RefreshTableControl;
            // 
            // txtMaxCovers
            // 
            txtMaxCovers.BorderStyle = BorderStyle.None;
            txtMaxCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtMaxCovers.Location = new Point(47, 380);
            txtMaxCovers.Name = "txtMaxCovers";
            txtMaxCovers.Size = new Size(81, 22);
            txtMaxCovers.TabIndex = 2;
            txtMaxCovers.Validated += RefreshTableControl;
            // 
            // txtAverageCovers
            // 
            txtAverageCovers.BorderStyle = BorderStyle.None;
            txtAverageCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtAverageCovers.Location = new Point(47, 452);
            txtAverageCovers.Name = "txtAverageCovers";
            txtAverageCovers.Size = new Size(81, 22);
            txtAverageCovers.TabIndex = 2;
            txtAverageCovers.Validated += RefreshTableControl;
            // 
            // txtHeight
            // 
            txtHeight.BorderStyle = BorderStyle.None;
            txtHeight.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            txtHeight.Location = new Point(47, 528);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(81, 22);
            txtHeight.TabIndex = 2;
            txtHeight.Validated += RefreshTableControl;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(180, 190, 200);
            panel3.Controls.Add(panel2);
            panel3.Location = new Point(1225, 16);
            panel3.Margin = new Padding(20);
            panel3.Name = "panel3";
            panel3.Size = new Size(24, 934);
            panel3.TabIndex = 4;
            panel3.Visible = false;
            // 
            // panel6
            // 
            panel6.BackColor = Color.WhiteSmoke;
            panel6.Controls.Add(cbTemporaryFloorplan);
            panel6.Controls.Add(txtDiningAreaName);
            panel6.Controls.Add(rbInside);
            panel6.Controls.Add(rbOutside);
            panel6.Controls.Add(btnSaveDiningArea);
            panel6.Location = new Point(15, 512);
            panel6.Name = "panel6";
            panel6.Size = new Size(168, 408);
            panel6.TabIndex = 4;
            panel6.Visible = false;
            // 
            // cbTemporaryFloorplan
            // 
            cbTemporaryFloorplan.AutoSize = true;
            cbTemporaryFloorplan.Enabled = false;
            cbTemporaryFloorplan.Location = new Point(14, 196);
            cbTemporaryFloorplan.Name = "cbTemporaryFloorplan";
            cbTemporaryFloorplan.Size = new Size(140, 19);
            cbTemporaryFloorplan.TabIndex = 4;
            cbTemporaryFloorplan.Text = "Temporary Floorplan?";
            cbTemporaryFloorplan.UseVisualStyleBackColor = true;
            // 
            // txtDiningAreaName
            // 
            txtDiningAreaName.Enabled = false;
            txtDiningAreaName.Location = new Point(10, 114);
            txtDiningAreaName.Name = "txtDiningAreaName";
            txtDiningAreaName.Size = new Size(146, 23);
            txtDiningAreaName.TabIndex = 3;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(180, 190, 200);
            panel4.Controls.Add(rdoDefaultView);
            panel4.Controls.Add(btnCreateNewDiningArea);
            panel4.Controls.Add(rdoEditPositions);
            panel4.Controls.Add(rdoEditData);
            panel4.Controls.Add(cboDiningAreas);
            panel4.Controls.Add(pnlFloorPlan);
            panel4.Location = new Point(362, 16);
            panel4.Margin = new Padding(20);
            panel4.Name = "panel4";
            panel4.Size = new Size(700, 934);
            panel4.TabIndex = 5;
            // 
            // rdoDefaultView
            // 
            rdoDefaultView.Appearance = Appearance.Button;
            rdoDefaultView.BackColor = Color.FromArgb(130, 180, 130);
            rdoDefaultView.FlatAppearance.BorderSize = 0;
            rdoDefaultView.FlatStyle = FlatStyle.Flat;
            rdoDefaultView.Image = Properties.Resources.resetViewSmallest;
            rdoDefaultView.Location = new Point(13, 19);
            rdoDefaultView.Name = "rdoDefaultView";
            rdoDefaultView.Size = new Size(77, 33);
            rdoDefaultView.TabIndex = 6;
            rdoDefaultView.TextAlign = ContentAlignment.MiddleCenter;
            rdoDefaultView.UseVisualStyleBackColor = false;
            rdoDefaultView.CheckedChanged += rdoDefaultView_CheckedChanged;
            // 
            // rdoEditPositions
            // 
            rdoEditPositions.Appearance = Appearance.Button;
            rdoEditPositions.BackColor = Color.FromArgb(130, 180, 130);
            rdoEditPositions.FlatAppearance.BorderSize = 0;
            rdoEditPositions.FlatStyle = FlatStyle.Flat;
            rdoEditPositions.Image = Properties.Resources.NumReposAdjust;
            rdoEditPositions.Location = new Point(179, 19);
            rdoEditPositions.Name = "rdoEditPositions";
            rdoEditPositions.Size = new Size(77, 33);
            rdoEditPositions.TabIndex = 6;
            rdoEditPositions.TextAlign = ContentAlignment.MiddleCenter;
            rdoEditPositions.UseVisualStyleBackColor = false;
            rdoEditPositions.CheckedChanged += rdoEditPositions_CheckedChanged;
            // 
            // rdoEditData
            // 
            rdoEditData.Appearance = Appearance.Button;
            rdoEditData.BackColor = Color.FromArgb(130, 180, 130);
            rdoEditData.FlatAppearance.BorderSize = 0;
            rdoEditData.FlatStyle = FlatStyle.Flat;
            rdoEditData.Image = Properties.Resources.coversSalesresizedSmallest;
            rdoEditData.Location = new Point(96, 19);
            rdoEditData.Name = "rdoEditData";
            rdoEditData.Size = new Size(77, 33);
            rdoEditData.TabIndex = 6;
            rdoEditData.TextAlign = ContentAlignment.MiddleCenter;
            rdoEditData.UseVisualStyleBackColor = false;
            rdoEditData.CheckedChanged += rdoEditData_CheckedChanged;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(262, 20);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(378, 29);
            cboDiningAreas.TabIndex = 1;
            cboDiningAreas.SelectedIndexChanged += cboDiningAreas_SelectedIndexChanged;
            // 
            // panel5
            // 
            panel5.BackColor = Color.WhiteSmoke;
            panel5.Controls.Add(label7);
            panel5.Controls.Add(btnAddCircle);
            panel5.Controls.Add(btnAddDiamond);
            panel5.Controls.Add(btnAddSquare);
            panel5.Location = new Point(15, 43);
            panel5.Name = "panel5";
            panel5.Size = new Size(168, 445);
            panel5.TabIndex = 4;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(28, 11);
            label7.Name = "label7";
            label7.Size = new Size(93, 21);
            label7.TabIndex = 1;
            label7.Text = "Add Tables";
            // 
            // btnAddCircle
            // 
            btnAddCircle.FlatAppearance.BorderSize = 0;
            btnAddCircle.FlatStyle = FlatStyle.Flat;
            btnAddCircle.Image = Properties.Resources.LargerCircleFilled;
            btnAddCircle.Location = new Point(10, 305);
            btnAddCircle.Name = "btnAddCircle";
            btnAddCircle.Size = new Size(149, 131);
            btnAddCircle.TabIndex = 0;
            btnAddCircle.UseVisualStyleBackColor = true;
            btnAddCircle.Click += btnAddCircle_Click;
            // 
            // btnAddDiamond
            // 
            btnAddDiamond.FlatAppearance.BorderSize = 0;
            btnAddDiamond.FlatStyle = FlatStyle.Flat;
            btnAddDiamond.Image = Properties.Resources.DiamondFilled;
            btnAddDiamond.Location = new Point(10, 170);
            btnAddDiamond.Name = "btnAddDiamond";
            btnAddDiamond.Size = new Size(149, 131);
            btnAddDiamond.TabIndex = 0;
            btnAddDiamond.UseVisualStyleBackColor = true;
            btnAddDiamond.Click += btnAddDiamond_Click;
            // 
            // btnAddSquare
            // 
            btnAddSquare.FlatAppearance.BorderSize = 0;
            btnAddSquare.FlatStyle = FlatStyle.Flat;
            btnAddSquare.Image = Properties.Resources.SquareTableFilled;
            btnAddSquare.Location = new Point(10, 35);
            btnAddSquare.Name = "btnAddSquare";
            btnAddSquare.Size = new Size(149, 131);
            btnAddSquare.TabIndex = 0;
            btnAddSquare.UseVisualStyleBackColor = true;
            btnAddSquare.Click += btnAddSquare_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel6);
            panel1.Location = new Point(43, 16);
            panel1.Name = "panel1";
            panel1.Size = new Size(200, 934);
            panel1.TabIndex = 6;
            // 
            // frmEditDiningAreas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 56, 82);
            ClientSize = new Size(1253, 970);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(panel4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmEditDiningAreas";
            Text = "frmEditDiningAreas";
            Load += frmEditDiningAreas_Load;
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFloorPlan;
        private Button btnCreateNewDiningArea;
        private Button btnSaveDiningArea;
        private RadioButton rbInside;
        private RadioButton rbOutside;
        private Button btnCopyTable;
        private Button btnSaveTable;
        private Button btnDeleteTable;
        private Button btnMoreWidth;
        private Button btnMoreHeight;
        private Button btnLessHeight;
        private Button btnLessWidth;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private ComboBox cboDiningAreas;
        private TextBox txtDiningAreaName;
        private TextBox txtHeight;
        private TextBox txtWidth;
        private TextBox txtTableNumber;
        private TextBox txtMaxCovers;
        private TextBox txtAverageCovers;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Panel panel5;
        private Panel panel6;
        private CheckBox cbTemporaryFloorplan;
        private Label label7;
        private Button btnAddCircle;
        private Button btnAddDiamond;
        private Button btnAddSquare;
        private CheckBox cbLockTables;
        private TextBox txtXco;
        private TextBox txtYco;
        private Label label9;
        private Button btnQuickEdit;
        private CheckBox cbViewMode;
        private Label label6;
        private RadioButton rdoEditData;
        private RadioButton rdoEditPositions;
        private RadioButton rdoDefaultView;
        private Panel panel1;
    }
}