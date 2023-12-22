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
            btnSaveTable = new Button();
            panel2 = new Panel();
            label6 = new Label();
            cbViewMode = new CheckBox();
            txtYco = new TextBox();
            btnQuickEdit = new Button();
            cbLockTables = new CheckBox();
            txtXco = new TextBox();
            label9 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            txtTableNumber = new TextBox();
            txtMaxCovers = new TextBox();
            txtAverageCovers = new TextBox();
            panel3 = new Panel();
            panel6 = new Panel();
            cbTemporaryFloorplan = new CheckBox();
            txtDiningAreaName = new TextBox();
            panel4 = new Panel();
            rdoDefaultView = new RadioButton();
            rdoEditPositions = new RadioButton();
            rdoSalesView = new RadioButton();
            rdoCoverView = new RadioButton();
            cboDiningAreas = new ComboBox();
            panel5 = new Panel();
            lblEndPoint = new Label();
            txtEnd = new TextBox();
            lblStartPoint = new Label();
            txtStart = new TextBox();
            lblMidPoint = new Label();
            txtMidPoint = new TextBox();
            cbOnlyShowThisTableLines = new CheckBox();
            btnAddNewNeighbor = new Button();
            btnRemoveNeighbor = new Button();
            lblPairData = new Label();
            checkBox1 = new CheckBox();
            lblSelectedTableNumber = new Label();
            lblSelectTable = new Label();
            lbTableNeighbors = new ListBox();
            picAddCircle = new PictureBox();
            picAddDiamond = new PictureBox();
            picAddSquare = new PictureBox();
            label7 = new Label();
            panel1 = new Panel();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel6.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAddCircle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAddDiamond).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAddSquare).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFloorPlan
            // 
            pnlFloorPlan.BackColor = Color.WhiteSmoke;
            pnlFloorPlan.Location = new Point(16, 56);
            pnlFloorPlan.Name = "pnlFloorPlan";
            pnlFloorPlan.Size = new Size(672, 877);
            pnlFloorPlan.TabIndex = 0;
            pnlFloorPlan.Click += pnlFloorPlan_Click;
            // 
            // btnCreateNewDiningArea
            // 
            btnCreateNewDiningArea.BackColor = Color.FromArgb(158, 171, 222);
            btnCreateNewDiningArea.FlatAppearance.BorderSize = 0;
            btnCreateNewDiningArea.FlatStyle = FlatStyle.Flat;
            btnCreateNewDiningArea.Image = Properties.Resources.addSmall;
            btnCreateNewDiningArea.Location = new Point(649, 18);
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
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txtTableNumber);
            panel2.Controls.Add(txtMaxCovers);
            panel2.Controls.Add(txtAverageCovers);
            panel2.Controls.Add(btnSaveTable);
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
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(180, 190, 200);
            panel3.Controls.Add(panel2);
            panel3.Location = new Point(1238, 16);
            panel3.Margin = new Padding(20);
            panel3.Name = "panel3";
            panel3.Size = new Size(11, 934);
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
            panel4.Controls.Add(rdoSalesView);
            panel4.Controls.Add(rdoCoverView);
            panel4.Controls.Add(cboDiningAreas);
            panel4.Controls.Add(pnlFloorPlan);
            panel4.Location = new Point(498, 29);
            panel4.Margin = new Padding(20);
            panel4.Name = "panel4";
            panel4.Size = new Size(711, 950);
            panel4.TabIndex = 5;
            // 
            // rdoDefaultView
            // 
            rdoDefaultView.Appearance = Appearance.Button;
            rdoDefaultView.BackColor = Color.FromArgb(130, 180, 130);
            rdoDefaultView.FlatAppearance.BorderSize = 0;
            rdoDefaultView.FlatStyle = FlatStyle.Flat;
            rdoDefaultView.Image = Properties.Resources.reverseSmall;
            rdoDefaultView.Location = new Point(16, 21);
            rdoDefaultView.Name = "rdoDefaultView";
            rdoDefaultView.Size = new Size(43, 33);
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
            rdoEditPositions.Image = Properties.Resources.repositionSmall;
            rdoEditPositions.Location = new Point(163, 21);
            rdoEditPositions.Name = "rdoEditPositions";
            rdoEditPositions.Size = new Size(46, 33);
            rdoEditPositions.TabIndex = 6;
            rdoEditPositions.TextAlign = ContentAlignment.MiddleCenter;
            rdoEditPositions.UseVisualStyleBackColor = false;
            rdoEditPositions.CheckedChanged += rdoEditPositions_CheckedChanged;
            // 
            // rdoSalesView
            // 
            rdoSalesView.Appearance = Appearance.Button;
            rdoSalesView.BackColor = Color.FromArgb(130, 180, 130);
            rdoSalesView.FlatAppearance.BorderSize = 0;
            rdoSalesView.FlatStyle = FlatStyle.Flat;
            rdoSalesView.Image = Properties.Resources.salesSMall;
            rdoSalesView.Location = new Point(114, 22);
            rdoSalesView.Name = "rdoSalesView";
            rdoSalesView.Size = new Size(43, 33);
            rdoSalesView.TabIndex = 6;
            rdoSalesView.TextAlign = ContentAlignment.MiddleCenter;
            rdoSalesView.UseVisualStyleBackColor = false;
            rdoSalesView.CheckedChanged += rdoSalesView_CheckedChanged;
            // 
            // rdoCoverView
            // 
            rdoCoverView.Appearance = Appearance.Button;
            rdoCoverView.BackColor = Color.FromArgb(130, 180, 130);
            rdoCoverView.FlatAppearance.BorderSize = 0;
            rdoCoverView.FlatStyle = FlatStyle.Flat;
            rdoCoverView.Image = Properties.Resources.ChairSmall;
            rdoCoverView.Location = new Point(65, 22);
            rdoCoverView.Name = "rdoCoverView";
            rdoCoverView.Size = new Size(43, 33);
            rdoCoverView.TabIndex = 6;
            rdoCoverView.TextAlign = ContentAlignment.MiddleCenter;
            rdoCoverView.UseVisualStyleBackColor = false;
            rdoCoverView.CheckedChanged += rdoEditData_CheckedChanged;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(229, 22);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(396, 29);
            cboDiningAreas.TabIndex = 1;
            cboDiningAreas.SelectedIndexChanged += cboDiningAreas_SelectedIndexChanged;
            // 
            // panel5
            // 
            panel5.BackColor = Color.WhiteSmoke;
            panel5.Controls.Add(lblEndPoint);
            panel5.Controls.Add(txtEnd);
            panel5.Controls.Add(lblStartPoint);
            panel5.Controls.Add(txtStart);
            panel5.Controls.Add(lblMidPoint);
            panel5.Controls.Add(txtMidPoint);
            panel5.Controls.Add(cbOnlyShowThisTableLines);
            panel5.Controls.Add(btnAddNewNeighbor);
            panel5.Controls.Add(btnRemoveNeighbor);
            panel5.Controls.Add(lblPairData);
            panel5.Controls.Add(checkBox1);
            panel5.Controls.Add(lblSelectedTableNumber);
            panel5.Controls.Add(lblSelectTable);
            panel5.Controls.Add(lbTableNeighbors);
            panel5.Controls.Add(picAddCircle);
            panel5.Controls.Add(picAddDiamond);
            panel5.Controls.Add(picAddSquare);
            panel5.Controls.Add(label7);
            panel5.Location = new Point(17, 17);
            panel5.Name = "panel5";
            panel5.Size = new Size(335, 921);
            panel5.TabIndex = 4;
            // 
            // lblEndPoint
            // 
            lblEndPoint.AutoSize = true;
            lblEndPoint.Location = new Point(237, 804);
            lblEndPoint.Name = "lblEndPoint";
            lblEndPoint.Size = new Size(27, 15);
            lblEndPoint.TabIndex = 13;
            lblEndPoint.Text = "End";
            lblEndPoint.Visible = false;
            // 
            // txtEnd
            // 
            txtEnd.Location = new Point(237, 822);
            txtEnd.Name = "txtEnd";
            txtEnd.Size = new Size(52, 23);
            txtEnd.TabIndex = 12;
            txtEnd.Visible = false;
            // 
            // lblStartPoint
            // 
            lblStartPoint.AutoSize = true;
            lblStartPoint.Location = new Point(181, 804);
            lblStartPoint.Name = "lblStartPoint";
            lblStartPoint.Size = new Size(31, 15);
            lblStartPoint.TabIndex = 13;
            lblStartPoint.Text = "Start";
            lblStartPoint.Visible = false;
            // 
            // txtStart
            // 
            txtStart.Location = new Point(179, 822);
            txtStart.Name = "txtStart";
            txtStart.Size = new Size(52, 23);
            txtStart.TabIndex = 12;
            txtStart.Visible = false;
            // 
            // lblMidPoint
            // 
            lblMidPoint.AutoSize = true;
            lblMidPoint.Location = new Point(125, 804);
            lblMidPoint.Name = "lblMidPoint";
            lblMidPoint.Size = new Size(56, 15);
            lblMidPoint.TabIndex = 13;
            lblMidPoint.Text = "MidPoint";
            lblMidPoint.Visible = false;
            // 
            // txtMidPoint
            // 
            txtMidPoint.Location = new Point(123, 822);
            txtMidPoint.Name = "txtMidPoint";
            txtMidPoint.Size = new Size(52, 23);
            txtMidPoint.TabIndex = 12;
            txtMidPoint.Visible = false;
            // 
            // cbOnlyShowThisTableLines
            // 
            cbOnlyShowThisTableLines.AutoSize = true;
            cbOnlyShowThisTableLines.Location = new Point(56, 682);
            cbOnlyShowThisTableLines.Name = "cbOnlyShowThisTableLines";
            cbOnlyShowThisTableLines.Size = new Size(198, 19);
            cbOnlyShowThisTableLines.TabIndex = 11;
            cbOnlyShowThisTableLines.Text = "Only Show Selected Table's Lines";
            cbOnlyShowThisTableLines.UseVisualStyleBackColor = true;
            cbOnlyShowThisTableLines.Visible = false;
            cbOnlyShowThisTableLines.CheckedChanged += ckBxOnlyShowThisTableLines_CheckedChanged;
            // 
            // btnAddNewNeighbor
            // 
            btnAddNewNeighbor.Location = new Point(121, 744);
            btnAddNewNeighbor.Name = "btnAddNewNeighbor";
            btnAddNewNeighbor.Size = new Size(150, 23);
            btnAddNewNeighbor.TabIndex = 10;
            btnAddNewNeighbor.Text = "Add New Neighbor";
            btnAddNewNeighbor.UseVisualStyleBackColor = true;
            btnAddNewNeighbor.Visible = false;
            btnAddNewNeighbor.Click += btnAddNewNeighbor_Click;
            // 
            // btnRemoveNeighbor
            // 
            btnRemoveNeighbor.Location = new Point(121, 707);
            btnRemoveNeighbor.Name = "btnRemoveNeighbor";
            btnRemoveNeighbor.Size = new Size(150, 23);
            btnRemoveNeighbor.TabIndex = 9;
            btnRemoveNeighbor.Text = "Remove Neighbor";
            btnRemoveNeighbor.UseVisualStyleBackColor = true;
            btnRemoveNeighbor.Visible = false;
            btnRemoveNeighbor.Click += btnRemoveNeighbor_Click;
            // 
            // lblPairData
            // 
            lblPairData.AutoSize = true;
            lblPairData.Location = new Point(125, 782);
            lblPairData.Name = "lblPairData";
            lblPairData.Size = new Size(91, 15);
            lblPairData.TabIndex = 8;
            lblPairData.Text = "Border Location";
            lblPairData.Visible = false;
            // 
            // checkBox1
            // 
            checkBox1.Appearance = Appearance.Button;
            checkBox1.BackColor = Color.FromArgb(100, 130, 180);
            checkBox1.FlatAppearance.BorderSize = 0;
            checkBox1.FlatStyle = FlatStyle.Flat;
            checkBox1.ForeColor = Color.White;
            checkBox1.Location = new Point(55, 619);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(215, 29);
            checkBox1.TabIndex = 7;
            checkBox1.Text = "Edit Table Neighbors";
            checkBox1.TextAlign = ContentAlignment.MiddleCenter;
            checkBox1.UseVisualStyleBackColor = false;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            // 
            // lblSelectedTableNumber
            // 
            lblSelectedTableNumber.AutoSize = true;
            lblSelectedTableNumber.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblSelectedTableNumber.Location = new Point(172, 658);
            lblSelectedTableNumber.Name = "lblSelectedTableNumber";
            lblSelectedTableNumber.Size = new Size(28, 21);
            lblSelectedTableNumber.TabIndex = 6;
            lblSelectedTableNumber.Text = "##";
            lblSelectedTableNumber.Visible = false;
            // 
            // lblSelectTable
            // 
            lblSelectTable.AutoSize = true;
            lblSelectTable.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            lblSelectTable.Location = new Point(55, 658);
            lblSelectTable.Name = "lblSelectTable";
            lblSelectTable.Size = new Size(110, 21);
            lblSelectTable.TabIndex = 5;
            lblSelectTable.Text = "Selected Table:";
            lblSelectTable.Visible = false;
            // 
            // lbTableNeighbors
            // 
            lbTableNeighbors.FormattingEnabled = true;
            lbTableNeighbors.ItemHeight = 15;
            lbTableNeighbors.Location = new Point(55, 706);
            lbTableNeighbors.Name = "lbTableNeighbors";
            lbTableNeighbors.Size = new Size(60, 139);
            lbTableNeighbors.TabIndex = 4;
            lbTableNeighbors.Visible = false;
            lbTableNeighbors.SelectedIndexChanged += lbTableNeighbors_SelectedIndexChanged;
            // 
            // picAddCircle
            // 
            picAddCircle.Image = Properties.Resources.SmallFilledCircle;
            picAddCircle.Location = new Point(55, 451);
            picAddCircle.Name = "picAddCircle";
            picAddCircle.Size = new Size(215, 142);
            picAddCircle.SizeMode = PictureBoxSizeMode.Zoom;
            picAddCircle.TabIndex = 2;
            picAddCircle.TabStop = false;
            picAddCircle.Click += picAddCircle_Click;
            // 
            // picAddDiamond
            // 
            picAddDiamond.Image = Properties.Resources.DiamondFilled;
            picAddDiamond.Location = new Point(56, 244);
            picAddDiamond.Name = "picAddDiamond";
            picAddDiamond.Size = new Size(215, 160);
            picAddDiamond.SizeMode = PictureBoxSizeMode.Zoom;
            picAddDiamond.TabIndex = 2;
            picAddDiamond.TabStop = false;
            picAddDiamond.Click += picAddDiamond_Click;
            // 
            // picAddSquare
            // 
            picAddSquare.Image = Properties.Resources.SquareTableFilled;
            picAddSquare.Location = new Point(55, 79);
            picAddSquare.Name = "picAddSquare";
            picAddSquare.Size = new Size(215, 126);
            picAddSquare.SizeMode = PictureBoxSizeMode.Zoom;
            picAddSquare.TabIndex = 2;
            picAddSquare.TabStop = false;
            picAddSquare.Click += picAddSquare_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(117, 39);
            label7.Name = "label7";
            label7.Size = new Size(93, 21);
            label7.TabIndex = 1;
            label7.Text = "Add Tables";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel6);
            panel1.Location = new Point(55, 29);
            panel1.Name = "panel1";
            panel1.Size = new Size(364, 950);
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
            ((System.ComponentModel.ISupportInitialize)picAddCircle).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAddDiamond).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAddSquare).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFloorPlan;
        private Button btnCreateNewDiningArea;
        private Button btnSaveDiningArea;
        private RadioButton rbInside;
        private RadioButton rbOutside;
        private Button btnSaveTable;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private ComboBox cboDiningAreas;
        private TextBox txtDiningAreaName;
        private TextBox txtTableNumber;
        private TextBox txtMaxCovers;
        private TextBox txtAverageCovers;
        private Label label3;
        private Label label2;
        private Label label1;
        private Panel panel5;
        private Panel panel6;
        private CheckBox cbTemporaryFloorplan;
        private Label label7;
        private CheckBox cbLockTables;
        private TextBox txtXco;
        private TextBox txtYco;
        private Label label9;
        private Button btnQuickEdit;
        private CheckBox cbViewMode;
        private Label label6;
        private RadioButton rdoCoverView;
        private RadioButton rdoEditPositions;
        private RadioButton rdoDefaultView;
        private Panel panel1;
        private PictureBox picAddCircle;
        private PictureBox picAddDiamond;
        private PictureBox picAddSquare;
        private RadioButton rdoSalesView;
        private CheckBox checkBox1;
        private Label lblSelectedTableNumber;
        private Label lblSelectTable;
        private ListBox lbTableNeighbors;
        private Button btnAddNewNeighbor;
        private Button btnRemoveNeighbor;
        private Label lblPairData;
        private Label lblEndPoint;
        private TextBox txtEnd;
        private Label lblStartPoint;
        private TextBox txtStart;
        private Label lblMidPoint;
        private TextBox txtMidPoint;
        private CheckBox cbOnlyShowThisTableLines;
    }
}