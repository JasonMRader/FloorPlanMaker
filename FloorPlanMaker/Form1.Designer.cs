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
            flowServersInFloorplan = new FlowLayoutPanel();
            btnAddServers = new Button();
            lblTeamWaitLabel = new Label();
            label1 = new Label();
            nudNumberOfTeamWaits = new NumericUpDown();
            label8 = new Label();
            cbTeamWait = new CheckBox();
            label2 = new Label();
            lblServerAverageCovers = new Label();
            lblDiningAreaAverageCovers = new Label();
            lblServerMaxCovers = new Label();
            lblDiningAreaMaxCovers = new Label();
            label11 = new Label();
            label9 = new Label();
            label10 = new Label();
            nudServerCount = new NumericUpDown();
            txtTemplateName = new TextBox();
            cboFloorplanTemplates = new ComboBox();
            btnSaveFloorplanTemplate = new Button();
            cbLockNodes = new CheckBox();
            btnAddSectionLabels = new Button();
            flowSectionSelect = new FlowLayoutPanel();
            pnlAddTables = new Panel();
            pnlSections = new Panel();
            btnAddPickupSection = new Button();
            btnGenerateSectionLines = new Button();
            btnChooseTemplate = new Button();
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
            btnMoreHeight = new Button();
            btnMoreWidth = new Button();
            btnLessWidth = new Button();
            btnLessHeight = new Button();
            txtXco = new TextBox();
            txtYco = new TextBox();
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
            btnTest = new Button();
            btnPrint = new Button();
            dtpFloorplan = new DateTimePicker();
            cbIsAM = new CheckBox();
            pnlServers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfTeamWaits).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudServerCount).BeginInit();
            pnlAddTables.SuspendLayout();
            pnlSections.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlServers
            // 
            pnlServers.BackColor = SystemColors.GradientActiveCaption;
            pnlServers.Controls.Add(flowServersInFloorplan);
            pnlServers.Controls.Add(btnAddServers);
            pnlServers.Controls.Add(lblTeamWaitLabel);
            pnlServers.Controls.Add(label1);
            pnlServers.Controls.Add(nudNumberOfTeamWaits);
            pnlServers.Controls.Add(label8);
            pnlServers.Controls.Add(cbTeamWait);
            pnlServers.Controls.Add(label2);
            pnlServers.Controls.Add(lblServerAverageCovers);
            pnlServers.Controls.Add(lblDiningAreaAverageCovers);
            pnlServers.Controls.Add(lblServerMaxCovers);
            pnlServers.Controls.Add(lblDiningAreaMaxCovers);
            pnlServers.Controls.Add(label11);
            pnlServers.Controls.Add(label9);
            pnlServers.Controls.Add(label10);
            pnlServers.Controls.Add(nudServerCount);
            pnlServers.Dock = DockStyle.Left;
            pnlServers.Location = new Point(0, 0);
            pnlServers.Name = "pnlServers";
            pnlServers.Size = new Size(250, 970);
            pnlServers.TabIndex = 0;
            // 
            // flowServersInFloorplan
            // 
            flowServersInFloorplan.Location = new Point(12, 329);
            flowServersInFloorplan.Name = "flowServersInFloorplan";
            flowServersInFloorplan.Size = new Size(228, 635);
            flowServersInFloorplan.TabIndex = 2;
            // 
            // btnAddServers
            // 
            btnAddServers.Location = new Point(3, 47);
            btnAddServers.Name = "btnAddServers";
            btnAddServers.Size = new Size(232, 40);
            btnAddServers.TabIndex = 1;
            btnAddServers.Text = "Add Servers To Shift";
            btnAddServers.UseVisualStyleBackColor = true;
            btnAddServers.Click += btnAddServers_Click;
            // 
            // lblTeamWaitLabel
            // 
            lblTeamWaitLabel.AutoSize = true;
            lblTeamWaitLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblTeamWaitLabel.Location = new Point(58, 264);
            lblTeamWaitLabel.Name = "lblTeamWaitLabel";
            lblTeamWaitLabel.Size = new Size(95, 21);
            lblTeamWaitLabel.TabIndex = 8;
            lblTeamWaitLabel.Text = "How Many?";
            lblTeamWaitLabel.Visible = false;
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
            // nudNumberOfTeamWaits
            // 
            nudNumberOfTeamWaits.Location = new Point(159, 262);
            nudNumberOfTeamWaits.Name = "nudNumberOfTeamWaits";
            nudNumberOfTeamWaits.Size = new Size(46, 23);
            nudNumberOfTeamWaits.TabIndex = 7;
            nudNumberOfTeamWaits.Visible = false;
            nudNumberOfTeamWaits.ValueChanged += nudNumberOfTeamWaits_ValueChanged;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(54, 90);
            label8.Name = "label8";
            label8.Size = new Size(99, 21);
            label8.TabIndex = 0;
            label8.Text = "Max Covers:";
            // 
            // cbTeamWait
            // 
            cbTeamWait.AutoSize = true;
            cbTeamWait.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cbTeamWait.Location = new Point(34, 236);
            cbTeamWait.Name = "cbTeamWait";
            cbTeamWait.Size = new Size(207, 25);
            cbTeamWait.TabIndex = 6;
            cbTeamWait.Text = "Add TeamWait Sections?";
            cbTeamWait.UseVisualStyleBackColor = true;
            cbTeamWait.CheckedChanged += cbTeamWait_CheckedChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(24, 111);
            label2.Name = "label2";
            label2.Size = new Size(129, 21);
            label2.TabIndex = 0;
            label2.Text = "Average Covers:";
            // 
            // lblServerAverageCovers
            // 
            lblServerAverageCovers.AutoSize = true;
            lblServerAverageCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerAverageCovers.Location = new Point(169, 200);
            lblServerAverageCovers.Name = "lblServerAverageCovers";
            lblServerAverageCovers.Size = new Size(19, 21);
            lblServerAverageCovers.TabIndex = 5;
            lblServerAverageCovers.Text = "0";
            // 
            // lblDiningAreaAverageCovers
            // 
            lblDiningAreaAverageCovers.AutoSize = true;
            lblDiningAreaAverageCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningAreaAverageCovers.Location = new Point(169, 111);
            lblDiningAreaAverageCovers.Name = "lblDiningAreaAverageCovers";
            lblDiningAreaAverageCovers.Size = new Size(19, 21);
            lblDiningAreaAverageCovers.TabIndex = 0;
            lblDiningAreaAverageCovers.Text = "0";
            // 
            // lblServerMaxCovers
            // 
            lblServerMaxCovers.AutoSize = true;
            lblServerMaxCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerMaxCovers.Location = new Point(169, 179);
            lblServerMaxCovers.Name = "lblServerMaxCovers";
            lblServerMaxCovers.Size = new Size(19, 21);
            lblServerMaxCovers.TabIndex = 5;
            lblServerMaxCovers.Text = "0";
            // 
            // lblDiningAreaMaxCovers
            // 
            lblDiningAreaMaxCovers.AutoSize = true;
            lblDiningAreaMaxCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningAreaMaxCovers.Location = new Point(169, 90);
            lblDiningAreaMaxCovers.Name = "lblDiningAreaMaxCovers";
            lblDiningAreaMaxCovers.Size = new Size(19, 21);
            lblDiningAreaMaxCovers.TabIndex = 1;
            lblDiningAreaMaxCovers.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(30, 200);
            label11.Name = "label11";
            label11.Size = new Size(123, 21);
            label11.TabIndex = 4;
            label11.Text = "Avg Per Server:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(84, 132);
            label9.Name = "label9";
            label9.Size = new Size(69, 21);
            label9.TabIndex = 2;
            label9.Text = "Servers:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(28, 179);
            label10.Name = "label10";
            label10.Size = new Size(125, 21);
            label10.TabIndex = 4;
            label10.Text = "Max Per Server:";
            // 
            // nudServerCount
            // 
            nudServerCount.Location = new Point(169, 132);
            nudServerCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudServerCount.Name = "nudServerCount";
            nudServerCount.Size = new Size(46, 23);
            nudServerCount.TabIndex = 3;
            nudServerCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudServerCount.ValueChanged += nudServerCount_ValueChanged;
            // 
            // txtTemplateName
            // 
            txtTemplateName.Location = new Point(19, 725);
            txtTemplateName.Name = "txtTemplateName";
            txtTemplateName.PlaceholderText = "Enter Template Name Here";
            txtTemplateName.Size = new Size(237, 29);
            txtTemplateName.TabIndex = 15;
            // 
            // cboFloorplanTemplates
            // 
            cboFloorplanTemplates.FormattingEnabled = true;
            cboFloorplanTemplates.Location = new Point(28, 823);
            cboFloorplanTemplates.Name = "cboFloorplanTemplates";
            cboFloorplanTemplates.Size = new Size(214, 29);
            cboFloorplanTemplates.TabIndex = 14;
            cboFloorplanTemplates.SelectedIndexChanged += cboFloorplanTemplates_SelectedIndexChanged;
            // 
            // btnSaveFloorplanTemplate
            // 
            btnSaveFloorplanTemplate.Location = new Point(19, 772);
            btnSaveFloorplanTemplate.Name = "btnSaveFloorplanTemplate";
            btnSaveFloorplanTemplate.Size = new Size(237, 30);
            btnSaveFloorplanTemplate.TabIndex = 12;
            btnSaveFloorplanTemplate.Text = "Save Floorplan Template";
            btnSaveFloorplanTemplate.UseVisualStyleBackColor = true;
            btnSaveFloorplanTemplate.Click += btnSaveFloorplanTemplate_Click;
            // 
            // cbLockNodes
            // 
            cbLockNodes.Appearance = Appearance.Button;
            cbLockNodes.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cbLockNodes.Location = new Point(6, 621);
            cbLockNodes.Name = "cbLockNodes";
            cbLockNodes.Size = new Size(259, 34);
            cbLockNodes.TabIndex = 11;
            cbLockNodes.Text = "Draw Section Lines";
            cbLockNodes.TextAlign = ContentAlignment.MiddleCenter;
            cbLockNodes.UseVisualStyleBackColor = true;
            cbLockNodes.CheckedChanged += cbLockNodes_CheckedChanged;
            // 
            // btnAddSectionLabels
            // 
            btnAddSectionLabels.Location = new Point(0, 0);
            btnAddSectionLabels.Name = "btnAddSectionLabels";
            btnAddSectionLabels.Size = new Size(271, 37);
            btnAddSectionLabels.TabIndex = 13;
            btnAddSectionLabels.Text = "Add Section Labels";
            btnAddSectionLabels.UseVisualStyleBackColor = true;
            btnAddSectionLabels.Click += btnAddSectionLabels_Click;
            // 
            // flowSectionSelect
            // 
            flowSectionSelect.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            flowSectionSelect.Location = new Point(0, 78);
            flowSectionSelect.Name = "flowSectionSelect";
            flowSectionSelect.Size = new Size(268, 493);
            flowSectionSelect.TabIndex = 9;
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
            pnlAddTables.Controls.Add(btnMoreHeight);
            pnlAddTables.Controls.Add(btnMoreWidth);
            pnlAddTables.Controls.Add(btnLessWidth);
            pnlAddTables.Controls.Add(btnLessHeight);
            pnlAddTables.Controls.Add(txtXco);
            pnlAddTables.Controls.Add(txtYco);
            pnlAddTables.Dock = DockStyle.Left;
            pnlAddTables.Location = new Point(250, 0);
            pnlAddTables.Name = "pnlAddTables";
            pnlAddTables.Size = new Size(271, 970);
            pnlAddTables.TabIndex = 1;
            // 
            // pnlSections
            // 
            pnlSections.Controls.Add(cboFloorplanTemplates);
            pnlSections.Controls.Add(txtTemplateName);
            pnlSections.Controls.Add(btnSaveFloorplanTemplate);
            pnlSections.Controls.Add(btnAddPickupSection);
            pnlSections.Controls.Add(btnGenerateSectionLines);
            pnlSections.Controls.Add(btnChooseTemplate);
            pnlSections.Controls.Add(btnAddSectionLabels);
            pnlSections.Controls.Add(flowSectionSelect);
            pnlSections.Controls.Add(cbLockNodes);
            pnlSections.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            pnlSections.Location = new Point(0, 36);
            pnlSections.Name = "pnlSections";
            pnlSections.Size = new Size(268, 937);
            pnlSections.TabIndex = 8;
            // 
            // btnAddPickupSection
            // 
            btnAddPickupSection.Location = new Point(6, 578);
            btnAddPickupSection.Name = "btnAddPickupSection";
            btnAddPickupSection.Size = new Size(259, 37);
            btnAddPickupSection.TabIndex = 16;
            btnAddPickupSection.Text = "Add Pickup Section";
            btnAddPickupSection.UseVisualStyleBackColor = true;
            btnAddPickupSection.Click += btnAddPickupSection_Click;
            // 
            // btnGenerateSectionLines
            // 
            btnGenerateSectionLines.Location = new Point(6, 662);
            btnGenerateSectionLines.Name = "btnGenerateSectionLines";
            btnGenerateSectionLines.Size = new Size(259, 46);
            btnGenerateSectionLines.TabIndex = 15;
            btnGenerateSectionLines.Text = "Auto Section Lines";
            btnGenerateSectionLines.UseVisualStyleBackColor = true;
            btnGenerateSectionLines.Click += btnGenerateSectionLines_Click;
            // 
            // btnChooseTemplate
            // 
            btnChooseTemplate.Location = new Point(0, 34);
            btnChooseTemplate.Name = "btnChooseTemplate";
            btnChooseTemplate.Size = new Size(271, 37);
            btnChooseTemplate.TabIndex = 14;
            btnChooseTemplate.Text = "Choose Template";
            btnChooseTemplate.UseVisualStyleBackColor = true;
            btnChooseTemplate.Click += btnChooseTemplate_Click;
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
            txtWidth.Validated += RefreshTableControl;
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
            txtHeight.Validated += RefreshTableControl;
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
            txtAverageCovers.Validated += RefreshTableControl;
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
            txtMaxCovers.Validated += RefreshTableControl;
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
            txtTableNumber.TextChanged += RefreshTableControl;
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
            // btnMoreHeight
            // 
            btnMoreHeight.Location = new Point(200, 721);
            btnMoreHeight.Name = "btnMoreHeight";
            btnMoreHeight.Size = new Size(33, 23);
            btnMoreHeight.TabIndex = 9;
            btnMoreHeight.Text = "+";
            btnMoreHeight.UseVisualStyleBackColor = true;
            btnMoreHeight.Click += btnMoreHeight_Click;
            // 
            // btnMoreWidth
            // 
            btnMoreWidth.Location = new Point(200, 779);
            btnMoreWidth.Name = "btnMoreWidth";
            btnMoreWidth.Size = new Size(33, 23);
            btnMoreWidth.TabIndex = 9;
            btnMoreWidth.Text = "+";
            btnMoreWidth.UseVisualStyleBackColor = true;
            btnMoreWidth.Click += btnMoreWidth_Click;
            // 
            // btnLessWidth
            // 
            btnLessWidth.Location = new Point(28, 779);
            btnLessWidth.Name = "btnLessWidth";
            btnLessWidth.Size = new Size(33, 23);
            btnLessWidth.TabIndex = 9;
            btnLessWidth.Text = "-";
            btnLessWidth.UseVisualStyleBackColor = true;
            btnLessWidth.Click += btnLessWidth_Click;
            // 
            // btnLessHeight
            // 
            btnLessHeight.Location = new Point(28, 720);
            btnLessHeight.Name = "btnLessHeight";
            btnLessHeight.Size = new Size(33, 23);
            btnLessHeight.TabIndex = 9;
            btnLessHeight.Text = "-";
            btnLessHeight.UseVisualStyleBackColor = true;
            btnLessHeight.Click += btnLessHeight_Click;
            // 
            // txtXco
            // 
            txtXco.Location = new Point(203, 560);
            txtXco.Name = "txtXco";
            txtXco.PlaceholderText = "X";
            txtXco.Size = new Size(53, 23);
            txtXco.TabIndex = 10;
            // 
            // txtYco
            // 
            txtYco.Location = new Point(203, 610);
            txtYco.Name = "txtYco";
            txtYco.PlaceholderText = "Y";
            txtYco.Size = new Size(53, 23);
            txtYco.TabIndex = 10;
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
            txtDiningAreaName.Location = new Point(1128, 34);
            txtDiningAreaName.Name = "txtDiningAreaName";
            txtDiningAreaName.Size = new Size(26, 35);
            txtDiningAreaName.TabIndex = 3;
            txtDiningAreaName.Visible = false;
            // 
            // btnCreateNewDiningArea
            // 
            btnCreateNewDiningArea.Location = new Point(1000, 7);
            btnCreateNewDiningArea.Name = "btnCreateNewDiningArea";
            btnCreateNewDiningArea.Size = new Size(154, 23);
            btnCreateNewDiningArea.TabIndex = 5;
            btnCreateNewDiningArea.Text = "Create New Dining Area";
            btnCreateNewDiningArea.UseVisualStyleBackColor = true;
            btnCreateNewDiningArea.Visible = false;
            btnCreateNewDiningArea.Click += btnCreateNewDiningArea_Click;
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
            cboDiningAreas.Location = new Point(766, 7);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(228, 23);
            cboDiningAreas.TabIndex = 7;
            cboDiningAreas.SelectedIndexChanged += cboDiningAreas_SelectedIndexChanged;
            // 
            // rbInside
            // 
            rbInside.AutoSize = true;
            rbInside.Location = new Point(1018, 36);
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
            rbOutside.Location = new Point(1018, 52);
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
            panel1.Location = new Point(535, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(225, 36);
            panel1.TabIndex = 11;
            // 
            // btnTest
            // 
            btnTest.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnTest.Location = new Point(540, 951);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(75, 19);
            btnTest.TabIndex = 12;
            btnTest.Text = "TEST";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(540, 47);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(75, 23);
            btnPrint.TabIndex = 13;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // dtpFloorplan
            // 
            dtpFloorplan.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            dtpFloorplan.Location = new Point(645, 43);
            dtpFloorplan.Name = "dtpFloorplan";
            dtpFloorplan.Size = new Size(203, 27);
            dtpFloorplan.TabIndex = 14;
            dtpFloorplan.ValueChanged += dtpFloorplan_ValueChanged;
            // 
            // cbIsAM
            // 
            cbIsAM.Appearance = Appearance.Button;
            cbIsAM.BackColor = Color.FromArgb(255, 255, 192);
            cbIsAM.FlatStyle = FlatStyle.Flat;
            cbIsAM.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAM.Location = new Point(871, 42);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(56, 26);
            cbIsAM.TabIndex = 15;
            cbIsAM.Text = "PM";
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAM.UseVisualStyleBackColor = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1248, 970);
            Controls.Add(cbIsAM);
            Controls.Add(dtpFloorplan);
            Controls.Add(btnPrint);
            Controls.Add(btnTest);
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
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            pnlServers.ResumeLayout(false);
            pnlServers.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudNumberOfTeamWaits).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudServerCount).EndInit();
            pnlAddTables.ResumeLayout(false);
            pnlAddTables.PerformLayout();
            pnlSections.ResumeLayout(false);
            pnlSections.PerformLayout();
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
        private FlowLayoutPanel flowServersInFloorplan;
        private Button btnSaveFloorplanTemplate;
        private Button btnAddSectionLabels;
        private Button btnTest;
        private Button btnLessHeight;
        private Button btnLessWidth;
        private Button btnMoreWidth;
        private Button btnMoreHeight;
        private ComboBox cboFloorplanTemplates;
        private TextBox txtTemplateName;
        private Button btnPrint;
        private DateTimePicker dtpFloorplan;
        private Button btnChooseTemplate;
        private Button btnGenerateSectionLines;
        private TextBox txtXco;
        private TextBox txtYco;
        private Button btnAddPickupSection;
        private CheckBox cbIsAM;
    }
}