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
            components = new System.ComponentModel.Container();
            pnlFloorPlan = new Panel();
            btnCreateNewDiningArea = new Button();
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
            panel4 = new Panel();
            rdoDefaultView = new RadioButton();
            rdoEditPositions = new RadioButton();
            rdoSalesView = new RadioButton();
            rdoCoverView = new RadioButton();
            cboDiningAreas = new ComboBox();
            pnlAddTables = new Panel();
            txtAddNewNeighbor = new TextBox();
            btnChangeNeighborEdge = new Button();
            lblEndPoint = new Label();
            txtEnd = new TextBox();
            lblStartPoint = new Label();
            txtStart = new TextBox();
            lblMidPoint = new Label();
            txtMidPoint = new TextBox();
            cbOnlyShowThisTableLines = new CheckBox();
            btnAddTopBottomNeighbor = new Button();
            btnAddRightLeftNeighbor = new Button();
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
            pnlTableStats = new Panel();
            rdoTableStats = new RadioButton();
            rdoAddTables = new RadioButton();
            toolTip1 = new ToolTip(components);
            txtTotalSales = new TextBox();
            lbTableSales = new ListBox();
            btnGetTableSales = new Button();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            pnlAddTables.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)picAddCircle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAddDiamond).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picAddSquare).BeginInit();
            panel1.SuspendLayout();
            pnlTableStats.SuspendLayout();
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
            pnlFloorPlan.Paint += pnlFloorPlan_Paint;
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
            toolTip1.SetToolTip(btnCreateNewDiningArea, "Create New Dining Area");
            btnCreateNewDiningArea.UseVisualStyleBackColor = false;
            btnCreateNewDiningArea.Click += btnCreateNewDiningArea_Click;
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
            toolTip1.SetToolTip(rdoDefaultView, "Reset View");
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
            toolTip1.SetToolTip(rdoEditPositions, "Move Tables");
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
            toolTip1.SetToolTip(rdoSalesView, "Edit Table Sales");
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
            toolTip1.SetToolTip(rdoCoverView, "Edit Table Covers");
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
            // pnlAddTables
            // 
            pnlAddTables.BackColor = Color.WhiteSmoke;
            pnlAddTables.Controls.Add(txtAddNewNeighbor);
            pnlAddTables.Controls.Add(btnChangeNeighborEdge);
            pnlAddTables.Controls.Add(lblEndPoint);
            pnlAddTables.Controls.Add(txtEnd);
            pnlAddTables.Controls.Add(lblStartPoint);
            pnlAddTables.Controls.Add(txtStart);
            pnlAddTables.Controls.Add(lblMidPoint);
            pnlAddTables.Controls.Add(txtMidPoint);
            pnlAddTables.Controls.Add(cbOnlyShowThisTableLines);
            pnlAddTables.Controls.Add(btnAddTopBottomNeighbor);
            pnlAddTables.Controls.Add(btnAddRightLeftNeighbor);
            pnlAddTables.Controls.Add(btnRemoveNeighbor);
            pnlAddTables.Controls.Add(lblPairData);
            pnlAddTables.Controls.Add(checkBox1);
            pnlAddTables.Controls.Add(lblSelectedTableNumber);
            pnlAddTables.Controls.Add(lblSelectTable);
            pnlAddTables.Controls.Add(lbTableNeighbors);
            pnlAddTables.Controls.Add(picAddCircle);
            pnlAddTables.Controls.Add(picAddDiamond);
            pnlAddTables.Controls.Add(picAddSquare);
            pnlAddTables.Controls.Add(label7);
            pnlAddTables.Location = new Point(17, 56);
            pnlAddTables.Name = "pnlAddTables";
            pnlAddTables.Size = new Size(335, 874);
            pnlAddTables.TabIndex = 4;
            // 
            // txtAddNewNeighbor
            // 
            txtAddNewNeighbor.Location = new Point(56, 757);
            txtAddNewNeighbor.Name = "txtAddNewNeighbor";
            txtAddNewNeighbor.Size = new Size(35, 23);
            txtAddNewNeighbor.TabIndex = 15;
            txtAddNewNeighbor.Visible = false;
            // 
            // btnChangeNeighborEdge
            // 
            btnChangeNeighborEdge.Location = new Point(121, 718);
            btnChangeNeighborEdge.Name = "btnChangeNeighborEdge";
            btnChangeNeighborEdge.Size = new Size(166, 23);
            btnChangeNeighborEdge.TabIndex = 14;
            btnChangeNeighborEdge.Text = "Change Neighbor Border";
            btnChangeNeighborEdge.UseVisualStyleBackColor = true;
            btnChangeNeighborEdge.Visible = false;
            btnChangeNeighborEdge.Click += btnChangeNeighborEdge_Click;
            // 
            // lblEndPoint
            // 
            lblEndPoint.AutoSize = true;
            lblEndPoint.Location = new Point(235, 671);
            lblEndPoint.Name = "lblEndPoint";
            lblEndPoint.Size = new Size(27, 15);
            lblEndPoint.TabIndex = 13;
            lblEndPoint.Text = "End";
            lblEndPoint.Visible = false;
            // 
            // txtEnd
            // 
            txtEnd.Location = new Point(235, 689);
            txtEnd.Name = "txtEnd";
            txtEnd.Size = new Size(52, 23);
            txtEnd.TabIndex = 12;
            txtEnd.Visible = false;
            // 
            // lblStartPoint
            // 
            lblStartPoint.AutoSize = true;
            lblStartPoint.Location = new Point(179, 671);
            lblStartPoint.Name = "lblStartPoint";
            lblStartPoint.Size = new Size(31, 15);
            lblStartPoint.TabIndex = 13;
            lblStartPoint.Text = "Start";
            lblStartPoint.Visible = false;
            // 
            // txtStart
            // 
            txtStart.Location = new Point(177, 689);
            txtStart.Name = "txtStart";
            txtStart.Size = new Size(52, 23);
            txtStart.TabIndex = 12;
            txtStart.Visible = false;
            // 
            // lblMidPoint
            // 
            lblMidPoint.AutoSize = true;
            lblMidPoint.Location = new Point(123, 671);
            lblMidPoint.Name = "lblMidPoint";
            lblMidPoint.Size = new Size(56, 15);
            lblMidPoint.TabIndex = 13;
            lblMidPoint.Text = "MidPoint";
            lblMidPoint.Visible = false;
            // 
            // txtMidPoint
            // 
            txtMidPoint.Location = new Point(121, 689);
            txtMidPoint.Name = "txtMidPoint";
            txtMidPoint.Size = new Size(52, 23);
            txtMidPoint.TabIndex = 12;
            txtMidPoint.Visible = false;
            // 
            // cbOnlyShowThisTableLines
            // 
            cbOnlyShowThisTableLines.AutoSize = true;
            cbOnlyShowThisTableLines.Location = new Point(56, 588);
            cbOnlyShowThisTableLines.Name = "cbOnlyShowThisTableLines";
            cbOnlyShowThisTableLines.Size = new Size(198, 19);
            cbOnlyShowThisTableLines.TabIndex = 11;
            cbOnlyShowThisTableLines.Text = "Only Show Selected Table's Lines";
            cbOnlyShowThisTableLines.UseVisualStyleBackColor = true;
            cbOnlyShowThisTableLines.Visible = false;
            cbOnlyShowThisTableLines.CheckedChanged += ckBxOnlyShowThisTableLines_CheckedChanged;
            // 
            // btnAddTopBottomNeighbor
            // 
            btnAddTopBottomNeighbor.Location = new Point(97, 783);
            btnAddTopBottomNeighbor.Name = "btnAddTopBottomNeighbor";
            btnAddTopBottomNeighbor.Size = new Size(150, 23);
            btnAddTopBottomNeighbor.TabIndex = 10;
            btnAddTopBottomNeighbor.Text = "Add T/B Neighbor";
            btnAddTopBottomNeighbor.UseVisualStyleBackColor = true;
            btnAddTopBottomNeighbor.Visible = false;
            btnAddTopBottomNeighbor.Click += btnAddTopBottomNeighbor_Click;
            // 
            // btnAddRightLeftNeighbor
            // 
            btnAddRightLeftNeighbor.Location = new Point(97, 757);
            btnAddRightLeftNeighbor.Name = "btnAddRightLeftNeighbor";
            btnAddRightLeftNeighbor.Size = new Size(150, 23);
            btnAddRightLeftNeighbor.TabIndex = 10;
            btnAddRightLeftNeighbor.Text = "Add R/L Neighbor";
            btnAddRightLeftNeighbor.UseVisualStyleBackColor = true;
            btnAddRightLeftNeighbor.Visible = false;
            btnAddRightLeftNeighbor.Click += btnAddNewRightLeftNeighbor_Click;
            // 
            // btnRemoveNeighbor
            // 
            btnRemoveNeighbor.Location = new Point(121, 613);
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
            lblPairData.Location = new Point(123, 649);
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
            checkBox1.Location = new Point(55, 525);
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
            lblSelectedTableNumber.Location = new Point(172, 564);
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
            lblSelectTable.Location = new Point(55, 564);
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
            lbTableNeighbors.Location = new Point(55, 612);
            lbTableNeighbors.Name = "lbTableNeighbors";
            lbTableNeighbors.Size = new Size(60, 139);
            lbTableNeighbors.TabIndex = 4;
            lbTableNeighbors.Visible = false;
            lbTableNeighbors.SelectedIndexChanged += lbTableNeighbors_SelectedIndexChanged;
            // 
            // picAddCircle
            // 
            picAddCircle.Image = Properties.Resources.SmallFilledCircle;
            picAddCircle.Location = new Point(55, 347);
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
            picAddDiamond.Location = new Point(55, 176);
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
            picAddSquare.Location = new Point(55, 33);
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
            label7.Location = new Point(117, 9);
            label7.Name = "label7";
            label7.Size = new Size(93, 21);
            label7.TabIndex = 1;
            label7.Text = "Add Tables";
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(180, 190, 200);
            panel1.Controls.Add(pnlTableStats);
            panel1.Controls.Add(pnlAddTables);
            panel1.Controls.Add(rdoTableStats);
            panel1.Controls.Add(rdoAddTables);
            panel1.Location = new Point(55, 29);
            panel1.Name = "panel1";
            panel1.Size = new Size(364, 950);
            panel1.TabIndex = 6;
            // 
            // pnlTableStats
            // 
            pnlTableStats.BackColor = Color.WhiteSmoke;
            pnlTableStats.Controls.Add(btnGetTableSales);
            pnlTableStats.Controls.Add(lbTableSales);
            pnlTableStats.Controls.Add(txtTotalSales);
            pnlTableStats.Location = new Point(17, 56);
            pnlTableStats.Name = "pnlTableStats";
            pnlTableStats.Size = new Size(335, 874);
            pnlTableStats.TabIndex = 0;
            pnlTableStats.Visible = false;
            // 
            // rdoTableStats
            // 
            rdoTableStats.Appearance = Appearance.Button;
            rdoTableStats.BackColor = Color.FromArgb(130, 180, 130);
            rdoTableStats.FlatAppearance.BorderSize = 0;
            rdoTableStats.FlatStyle = FlatStyle.Flat;
            rdoTableStats.Location = new Point(114, 21);
            rdoTableStats.Name = "rdoTableStats";
            rdoTableStats.Size = new Size(91, 33);
            rdoTableStats.TabIndex = 6;
            rdoTableStats.Text = "Table Stats";
            rdoTableStats.TextAlign = ContentAlignment.MiddleCenter;
            rdoTableStats.UseVisualStyleBackColor = false;
            // 
            // rdoAddTables
            // 
            rdoAddTables.Appearance = Appearance.Button;
            rdoAddTables.BackColor = Color.FromArgb(130, 180, 130);
            rdoAddTables.Checked = true;
            rdoAddTables.FlatAppearance.BorderSize = 0;
            rdoAddTables.FlatStyle = FlatStyle.Flat;
            rdoAddTables.Location = new Point(17, 21);
            rdoAddTables.Name = "rdoAddTables";
            rdoAddTables.Size = new Size(91, 33);
            rdoAddTables.TabIndex = 6;
            rdoAddTables.TabStop = true;
            rdoAddTables.Text = "Add Tables";
            rdoAddTables.TextAlign = ContentAlignment.MiddleCenter;
            rdoAddTables.UseVisualStyleBackColor = false;
            rdoAddTables.CheckedChanged += rdoAddTables_CheckChanged;
            // 
            // txtTotalSales
            // 
            txtTotalSales.Location = new Point(15, 60);
            txtTotalSales.Name = "txtTotalSales";
            txtTotalSales.Size = new Size(158, 23);
            txtTotalSales.TabIndex = 0;
            // 
            // lbTableSales
            // 
            lbTableSales.FormattingEnabled = true;
            lbTableSales.ItemHeight = 15;
            lbTableSales.Location = new Point(15, 146);
            lbTableSales.MultiColumn = true;
            lbTableSales.Name = "lbTableSales";
            lbTableSales.Size = new Size(153, 709);
            lbTableSales.TabIndex = 1;
            // 
            // btnGetTableSales
            // 
            btnGetTableSales.Location = new Point(16, 99);
            btnGetTableSales.Name = "btnGetTableSales";
            btnGetTableSales.Size = new Size(157, 23);
            btnGetTableSales.TabIndex = 2;
            btnGetTableSales.Text = "button1";
            btnGetTableSales.UseVisualStyleBackColor = true;
            btnGetTableSales.Click += btnGetTableSales_Click;
            // 
            // frmEditDiningAreas
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(49, 56, 82);
            ClientSize = new Size(1253, 999);
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
            panel4.ResumeLayout(false);
            pnlAddTables.ResumeLayout(false);
            pnlAddTables.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)picAddCircle).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAddDiamond).EndInit();
            ((System.ComponentModel.ISupportInitialize)picAddSquare).EndInit();
            panel1.ResumeLayout(false);
            pnlTableStats.ResumeLayout(false);
            pnlTableStats.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFloorPlan;
        private Button btnCreateNewDiningArea;
        private Button btnSaveTable;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private ComboBox cboDiningAreas;
        private TextBox txtTableNumber;
        private TextBox txtMaxCovers;
        private TextBox txtAverageCovers;
        private Label label3;
        private Label label2;
        private Label label1;
        private Panel pnlAddTables;
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
        private Button btnAddRightLeftNeighbor;
        private Button btnRemoveNeighbor;
        private Label lblPairData;
        private Label lblEndPoint;
        private TextBox txtEnd;
        private Label lblStartPoint;
        private TextBox txtStart;
        private Label lblMidPoint;
        private TextBox txtMidPoint;
        private CheckBox cbOnlyShowThisTableLines;
        private Button btnChangeNeighborEdge;
        private TextBox txtAddNewNeighbor;
        private Button btnAddTopBottomNeighbor;
        private ToolTip toolTip1;
        private Panel pnlTableStats;
        private RadioButton rdoTableStats;
        private RadioButton rdoAddTables;
        private Button btnGetTableSales;
        private ListBox lbTableSales;
        private TextBox txtTotalSales;
    }
}