using FloorPlanMakerUI;

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
            flowServersInFloorplan = new FlowLayoutPanel();
            lblDiningRoomName = new Label();
            panel2 = new Panel();
            txtDiningAreaName = new TextBox();
            btnCreateNewDiningArea = new Button();
            btnSaveDiningArea = new Button();
            rbInside = new RadioButton();
            rbOutside = new RadioButton();
            btnSaveTables = new Button();
            label1 = new Label();
            label10 = new Label();
            label2 = new Label();
            label8 = new Label();
            lblDiningAreaAverageCovers = new Label();
            lblServerAverageCovers = new Label();
            label11 = new Label();
            lblServerMaxCovers = new Label();
            lblDiningAreaMaxCovers = new Label();
            label9 = new Label();
            nudServerCount = new NumericUpDown();
            btnRemoveSection = new Button();
            btnAddSection = new Button();
            txtTemplateName = new TextBox();
            cboFloorplanTemplates = new ComboBox();
            btnSaveFloorplanTemplate = new Button();
            btnAddSectionLabels = new Button();
            flowSectionSelect = new FlowLayoutPanel();
            pnlAddTables = new Panel();
            pnlSections = new Panel();
            btnPrint = new Button();
            btnGenerateSectionLines = new Button();
            btnChooseTemplate = new Button();
            btnCopyTable = new Button();
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
            btnDeleteTable = new Button();
            btnDoAThing = new Button();
            btnTest2 = new Button();
            btnTest = new Button();
            pnlFloorPlan = new Panel();
            cboDiningAreas = new ComboBox();
            rdoSections = new RadioButton();
            rdoDiningAreas = new RadioButton();
            panel1 = new Panel();
            rdoShifts = new RadioButton();
            pnlNavHighlight = new Panel();
            dtpFloorplan = new DateTimePicker();
            cbIsAM = new CheckBox();
            btnDayBefore = new Button();
            btnNextDay = new Button();
            btnCloseApp = new Button();
            panel3 = new Panel();
            pnlNavigationWindow = new Panel();
            pnlServers.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudServerCount).BeginInit();
            pnlAddTables.SuspendLayout();
            pnlSections.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // pnlServers
            // 
            pnlServers.BackColor = Color.Silver;
            pnlServers.Controls.Add(flowServersInFloorplan);
            pnlServers.Controls.Add(lblDiningRoomName);
            pnlServers.Controls.Add(panel2);
            pnlServers.Controls.Add(label1);
            pnlServers.Controls.Add(label10);
            pnlServers.Controls.Add(label2);
            pnlServers.Controls.Add(label8);
            pnlServers.Controls.Add(lblDiningAreaAverageCovers);
            pnlServers.Controls.Add(lblServerAverageCovers);
            pnlServers.Controls.Add(label11);
            pnlServers.Controls.Add(lblServerMaxCovers);
            pnlServers.Controls.Add(lblDiningAreaMaxCovers);
            pnlServers.ForeColor = Color.Black;
            pnlServers.Location = new Point(703, 40);
            pnlServers.Name = "pnlServers";
            pnlServers.Size = new Size(241, 877);
            pnlServers.TabIndex = 0;
            // 
            // btnAddServers
            // 
            btnAddServers.FlatAppearance.BorderSize = 0;
            btnAddServers.FlatStyle = FlatStyle.Flat;
            btnAddServers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddServers.Location = new Point(111, 329);
            btnAddServers.Name = "btnAddServers";
            btnAddServers.Size = new Size(95, 74);
            btnAddServers.TabIndex = 1;
            btnAddServers.Text = "Shifts";
            btnAddServers.UseVisualStyleBackColor = true;
            btnAddServers.Click += btnAddServers_Click;
            // 
            // flowServersInFloorplan
            // 
            flowServersInFloorplan.Location = new Point(3, 225);
            flowServersInFloorplan.Name = "flowServersInFloorplan";
            flowServersInFloorplan.Size = new Size(235, 605);
            flowServersInFloorplan.TabIndex = 2;
            // 
            // lblDiningRoomName
            // 
            lblDiningRoomName.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningRoomName.Location = new Point(0, 10);
            lblDiningRoomName.Name = "lblDiningRoomName";
            lblDiningRoomName.Size = new Size(238, 37);
            lblDiningRoomName.TabIndex = 6;
            lblDiningRoomName.Text = "Outside Cocktail";
            lblDiningRoomName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            panel2.Controls.Add(btnAddServers);
            panel2.Controls.Add(txtDiningAreaName);
            panel2.Controls.Add(btnCreateNewDiningArea);
            panel2.Controls.Add(btnSaveDiningArea);
            panel2.Controls.Add(rbInside);
            panel2.Controls.Add(rbOutside);
            panel2.Controls.Add(btnSaveTables);
            panel2.Location = new Point(15, 245);
            panel2.Name = "panel2";
            panel2.Size = new Size(219, 409);
            panel2.TabIndex = 0;
            // 
            // txtDiningAreaName
            // 
            txtDiningAreaName.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            txtDiningAreaName.Location = new Point(3, 113);
            txtDiningAreaName.Name = "txtDiningAreaName";
            txtDiningAreaName.Size = new Size(213, 35);
            txtDiningAreaName.TabIndex = 3;
            txtDiningAreaName.Visible = false;
            // 
            // btnCreateNewDiningArea
            // 
            btnCreateNewDiningArea.Location = new Point(3, 35);
            btnCreateNewDiningArea.Name = "btnCreateNewDiningArea";
            btnCreateNewDiningArea.Size = new Size(213, 38);
            btnCreateNewDiningArea.TabIndex = 5;
            btnCreateNewDiningArea.Text = "Create New Dining Area";
            btnCreateNewDiningArea.UseVisualStyleBackColor = true;
            btnCreateNewDiningArea.Visible = false;
            btnCreateNewDiningArea.Click += btnCreateNewDiningArea_Click;
            // 
            // btnSaveDiningArea
            // 
            btnSaveDiningArea.Location = new Point(3, 228);
            btnSaveDiningArea.Name = "btnSaveDiningArea";
            btnSaveDiningArea.Size = new Size(213, 34);
            btnSaveDiningArea.TabIndex = 6;
            btnSaveDiningArea.Text = "Save";
            btnSaveDiningArea.UseVisualStyleBackColor = true;
            btnSaveDiningArea.Visible = false;
            btnSaveDiningArea.Click += btnSaveDiningArea_Click;
            // 
            // rbInside
            // 
            rbInside.AutoSize = true;
            rbInside.Location = new Point(18, 168);
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
            rbOutside.Location = new Point(140, 168);
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
            btnSaveTables.Location = new Point(3, 268);
            btnSaveTables.Name = "btnSaveTables";
            btnSaveTables.Size = new Size(213, 36);
            btnSaveTables.TabIndex = 9;
            btnSaveTables.Text = "Save Tables";
            btnSaveTables.UseVisualStyleBackColor = true;
            btnSaveTables.Visible = false;
            btnSaveTables.Click += btnSaveTables_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(81, 192);
            label1.Name = "label1";
            label1.Size = new Size(84, 30);
            label1.TabIndex = 0;
            label1.Text = "Servers";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(28, 134);
            label10.Name = "label10";
            label10.Size = new Size(125, 21);
            label10.TabIndex = 4;
            label10.Text = "Max Per Server:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(2, 88);
            label2.Name = "label2";
            label2.Size = new Size(151, 25);
            label2.TabIndex = 0;
            label2.Text = "Average Covers:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label8.ForeColor = Color.Black;
            label8.Location = new Point(35, 63);
            label8.Name = "label8";
            label8.Size = new Size(118, 25);
            label8.TabIndex = 0;
            label8.Text = "Max Covers:";
            // 
            // lblDiningAreaAverageCovers
            // 
            lblDiningAreaAverageCovers.AutoSize = true;
            lblDiningAreaAverageCovers.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningAreaAverageCovers.ForeColor = Color.Black;
            lblDiningAreaAverageCovers.Location = new Point(159, 88);
            lblDiningAreaAverageCovers.Name = "lblDiningAreaAverageCovers";
            lblDiningAreaAverageCovers.Size = new Size(23, 25);
            lblDiningAreaAverageCovers.TabIndex = 0;
            lblDiningAreaAverageCovers.Text = "0";
            // 
            // lblServerAverageCovers
            // 
            lblServerAverageCovers.AutoSize = true;
            lblServerAverageCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerAverageCovers.Location = new Point(159, 155);
            lblServerAverageCovers.Name = "lblServerAverageCovers";
            lblServerAverageCovers.Size = new Size(19, 21);
            lblServerAverageCovers.TabIndex = 5;
            lblServerAverageCovers.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(30, 155);
            label11.Name = "label11";
            label11.Size = new Size(123, 21);
            label11.TabIndex = 4;
            label11.Text = "Avg Per Server:";
            // 
            // lblServerMaxCovers
            // 
            lblServerMaxCovers.AutoSize = true;
            lblServerMaxCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerMaxCovers.Location = new Point(159, 134);
            lblServerMaxCovers.Name = "lblServerMaxCovers";
            lblServerMaxCovers.Size = new Size(19, 21);
            lblServerMaxCovers.TabIndex = 5;
            lblServerMaxCovers.Text = "0";
            // 
            // lblDiningAreaMaxCovers
            // 
            lblDiningAreaMaxCovers.AutoSize = true;
            lblDiningAreaMaxCovers.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDiningAreaMaxCovers.ForeColor = Color.Black;
            lblDiningAreaMaxCovers.Location = new Point(159, 63);
            lblDiningAreaMaxCovers.Name = "lblDiningAreaMaxCovers";
            lblDiningAreaMaxCovers.Size = new Size(23, 25);
            lblDiningAreaMaxCovers.TabIndex = 1;
            lblDiningAreaMaxCovers.Text = "0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(80, 813);
            label9.Name = "label9";
            label9.Size = new Size(69, 21);
            label9.TabIndex = 2;
            label9.Text = "Servers:";
            label9.Visible = false;
            // 
            // nudServerCount
            // 
            nudServerCount.Location = new Point(165, 813);
            nudServerCount.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudServerCount.Name = "nudServerCount";
            nudServerCount.Size = new Size(46, 29);
            nudServerCount.TabIndex = 3;
            nudServerCount.Value = new decimal(new int[] { 1, 0, 0, 0 });
            nudServerCount.Visible = false;
            nudServerCount.ValueChanged += nudServerCount_ValueChanged;
            // 
            // btnRemoveSection
            // 
            btnRemoveSection.FlatStyle = FlatStyle.Flat;
            btnRemoveSection.ForeColor = Color.Black;
            btnRemoveSection.Location = new Point(112, 837);
            btnRemoveSection.Name = "btnRemoveSection";
            btnRemoveSection.Size = new Size(115, 23);
            btnRemoveSection.TabIndex = 9;
            btnRemoveSection.Text = "Remove Section";
            btnRemoveSection.UseVisualStyleBackColor = true;
            btnRemoveSection.Visible = false;
            btnRemoveSection.Click += btnRemoveSection_Click;
            // 
            // btnAddSection
            // 
            btnAddSection.FlatStyle = FlatStyle.Flat;
            btnAddSection.ForeColor = Color.Black;
            btnAddSection.Location = new Point(8, 837);
            btnAddSection.Name = "btnAddSection";
            btnAddSection.Size = new Size(98, 23);
            btnAddSection.TabIndex = 9;
            btnAddSection.Text = "Add Section";
            btnAddSection.UseVisualStyleBackColor = true;
            btnAddSection.Visible = false;
            btnAddSection.Click += btnAddSection_Click;
            // 
            // txtTemplateName
            // 
            txtTemplateName.Location = new Point(18, 692);
            txtTemplateName.Name = "txtTemplateName";
            txtTemplateName.PlaceholderText = "Enter Template Name Here";
            txtTemplateName.Size = new Size(212, 29);
            txtTemplateName.TabIndex = 15;
            txtTemplateName.Visible = false;
            // 
            // cboFloorplanTemplates
            // 
            cboFloorplanTemplates.FormattingEnabled = true;
            cboFloorplanTemplates.Location = new Point(16, 781);
            cboFloorplanTemplates.Name = "cboFloorplanTemplates";
            cboFloorplanTemplates.Size = new Size(218, 29);
            cboFloorplanTemplates.TabIndex = 14;
            cboFloorplanTemplates.Visible = false;
            cboFloorplanTemplates.SelectedIndexChanged += cboFloorplanTemplates_SelectedIndexChanged;
            // 
            // btnSaveFloorplanTemplate
            // 
            btnSaveFloorplanTemplate.FlatStyle = FlatStyle.Flat;
            btnSaveFloorplanTemplate.ForeColor = Color.Black;
            btnSaveFloorplanTemplate.Location = new Point(18, 735);
            btnSaveFloorplanTemplate.Name = "btnSaveFloorplanTemplate";
            btnSaveFloorplanTemplate.Size = new Size(215, 30);
            btnSaveFloorplanTemplate.TabIndex = 12;
            btnSaveFloorplanTemplate.Text = "Save Floorplan Template";
            btnSaveFloorplanTemplate.UseVisualStyleBackColor = true;
            btnSaveFloorplanTemplate.Visible = false;
            btnSaveFloorplanTemplate.Click += btnSaveFloorplanTemplate_Click;
            // 
            // btnAddSectionLabels
            // 
            btnAddSectionLabels.BackColor = Color.FromArgb(255, 103, 0);
            btnAddSectionLabels.FlatAppearance.BorderSize = 0;
            btnAddSectionLabels.FlatStyle = FlatStyle.Flat;
            btnAddSectionLabels.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddSectionLabels.Location = new Point(12, 583);
            btnAddSectionLabels.Name = "btnAddSectionLabels";
            btnAddSectionLabels.Size = new Size(212, 25);
            btnAddSectionLabels.TabIndex = 13;
            btnAddSectionLabels.Text = "Add Section Labels";
            btnAddSectionLabels.UseVisualStyleBackColor = true;
            btnAddSectionLabels.Click += btnAddSectionLabels_Click;
            // 
            // flowSectionSelect
            // 
            flowSectionSelect.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            flowSectionSelect.Location = new Point(12, 75);
            flowSectionSelect.Name = "flowSectionSelect";
            flowSectionSelect.Size = new Size(212, 494);
            flowSectionSelect.TabIndex = 9;
            // 
            // pnlAddTables
            // 
            pnlAddTables.BackColor = Color.FromArgb(178, 87, 46);
            pnlAddTables.Controls.Add(pnlSections);
            pnlAddTables.Controls.Add(btnCopyTable);
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
            pnlAddTables.Controls.Add(btnDeleteTable);
            pnlAddTables.ForeColor = Color.White;
            pnlAddTables.Location = new Point(12, 73);
            pnlAddTables.Name = "pnlAddTables";
            pnlAddTables.Size = new Size(250, 911);
            pnlAddTables.TabIndex = 1;
            // 
            // pnlSections
            // 
            pnlSections.Controls.Add(btnAddSectionLabels);
            pnlSections.Controls.Add(label9);
            pnlSections.Controls.Add(btnPrint);
            pnlSections.Controls.Add(cboFloorplanTemplates);
            pnlSections.Controls.Add(nudServerCount);
            pnlSections.Controls.Add(txtTemplateName);
            pnlSections.Controls.Add(btnSaveFloorplanTemplate);
            pnlSections.Controls.Add(btnRemoveSection);
            pnlSections.Controls.Add(btnGenerateSectionLines);
            pnlSections.Controls.Add(btnChooseTemplate);
            pnlSections.Controls.Add(flowSectionSelect);
            pnlSections.Controls.Add(btnAddSection);
            pnlSections.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            pnlSections.Location = new Point(8, 32);
            pnlSections.Name = "pnlSections";
            pnlSections.Size = new Size(239, 869);
            pnlSections.TabIndex = 8;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.FromArgb(255, 103, 0);
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnPrint.Location = new Point(12, 610);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(212, 25);
            btnPrint.TabIndex = 13;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnGenerateSectionLines
            // 
            btnGenerateSectionLines.FlatStyle = FlatStyle.Flat;
            btnGenerateSectionLines.ForeColor = Color.Black;
            btnGenerateSectionLines.Location = new Point(12, 651);
            btnGenerateSectionLines.Name = "btnGenerateSectionLines";
            btnGenerateSectionLines.Size = new Size(212, 35);
            btnGenerateSectionLines.TabIndex = 15;
            btnGenerateSectionLines.Text = "Auto Section Lines";
            btnGenerateSectionLines.UseVisualStyleBackColor = true;
            btnGenerateSectionLines.Visible = false;
            // 
            // btnChooseTemplate
            // 
            btnChooseTemplate.FlatStyle = FlatStyle.Flat;
            btnChooseTemplate.ForeColor = Color.Black;
            btnChooseTemplate.Location = new Point(28, 16);
            btnChooseTemplate.Name = "btnChooseTemplate";
            btnChooseTemplate.Size = new Size(184, 37);
            btnChooseTemplate.TabIndex = 14;
            btnChooseTemplate.Text = "Choose Template";
            btnChooseTemplate.UseVisualStyleBackColor = true;
            btnChooseTemplate.Click += btnChooseTemplate_Click;
            // 
            // btnCopyTable
            // 
            btnCopyTable.FlatAppearance.BorderSize = 0;
            btnCopyTable.FlatStyle = FlatStyle.Flat;
            btnCopyTable.Location = new Point(18, 510);
            btnCopyTable.Name = "btnCopyTable";
            btnCopyTable.Size = new Size(166, 23);
            btnCopyTable.TabIndex = 7;
            btnCopyTable.Text = "Copy Table";
            btnCopyTable.UseVisualStyleBackColor = true;
            btnCopyTable.Click += btnCopyTable_Click;
            // 
            // btnSaveTable
            // 
            btnSaveTable.FlatAppearance.BorderSize = 0;
            btnSaveTable.FlatStyle = FlatStyle.Flat;
            btnSaveTable.Location = new Point(31, 812);
            btnSaveTable.Name = "btnSaveTable";
            btnSaveTable.Size = new Size(166, 37);
            btnSaveTable.TabIndex = 5;
            btnSaveTable.Text = "Save Table";
            btnSaveTable.UseVisualStyleBackColor = true;
            btnSaveTable.Click += btnSaveTable_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(54, 757);
            label7.Name = "label7";
            label7.Size = new Size(39, 15);
            label7.TabIndex = 4;
            label7.Text = "Width";
            // 
            // txtWidth
            // 
            txtWidth.Location = new Point(54, 775);
            txtWidth.Name = "txtWidth";
            txtWidth.Size = new Size(107, 23);
            txtWidth.TabIndex = 3;
            txtWidth.Validated += RefreshTableControl;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(54, 699);
            label6.Name = "label6";
            label6.Size = new Size(43, 15);
            label6.TabIndex = 4;
            label6.Text = "Height";
            // 
            // txtHeight
            // 
            txtHeight.Location = new Point(54, 717);
            txtHeight.Name = "txtHeight";
            txtHeight.Size = new Size(107, 23);
            txtHeight.TabIndex = 3;
            txtHeight.Validated += RefreshTableControl;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(54, 645);
            label5.Name = "label5";
            label5.Size = new Size(89, 15);
            label5.TabIndex = 4;
            label5.Text = "Average Covers";
            // 
            // txtAverageCovers
            // 
            txtAverageCovers.Location = new Point(54, 663);
            txtAverageCovers.Name = "txtAverageCovers";
            txtAverageCovers.Size = new Size(107, 23);
            txtAverageCovers.TabIndex = 3;
            txtAverageCovers.Validated += RefreshTableControl;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(54, 588);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 4;
            label4.Text = "Max Covers";
            // 
            // txtMaxCovers
            // 
            txtMaxCovers.Location = new Point(54, 606);
            txtMaxCovers.Name = "txtMaxCovers";
            txtMaxCovers.Size = new Size(107, 23);
            txtMaxCovers.TabIndex = 3;
            txtMaxCovers.Validated += RefreshTableControl;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(54, 534);
            label3.Name = "label3";
            label3.Size = new Size(81, 15);
            label3.TabIndex = 4;
            label3.Text = "Table Number";
            // 
            // txtTableNumber
            // 
            txtTableNumber.Location = new Point(54, 552);
            txtTableNumber.Name = "txtTableNumber";
            txtTableNumber.Size = new Size(107, 23);
            txtTableNumber.TabIndex = 3;
            txtTableNumber.TextChanged += RefreshTableControl;
            // 
            // cbLockTables
            // 
            cbLockTables.Appearance = Appearance.Button;
            cbLockTables.FlatAppearance.BorderSize = 0;
            cbLockTables.FlatStyle = FlatStyle.Flat;
            cbLockTables.Location = new Point(18, 459);
            cbLockTables.Name = "cbLockTables";
            cbLockTables.Size = new Size(166, 45);
            cbLockTables.TabIndex = 2;
            cbLockTables.Text = "Lock Tables";
            cbLockTables.TextAlign = ContentAlignment.MiddleCenter;
            cbLockTables.UseVisualStyleBackColor = true;
            cbLockTables.CheckedChanged += cbLockTables_CheckedChanged;
            // 
            // lblPanel2Text
            // 
            lblPanel2Text.AutoSize = true;
            lblPanel2Text.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            lblPanel2Text.Location = new Point(67, 1);
            lblPanel2Text.Name = "lblPanel2Text";
            lblPanel2Text.Size = new Size(120, 30);
            lblPanel2Text.TabIndex = 0;
            lblPanel2Text.Text = "Add Tables";
            // 
            // btnMoreHeight
            // 
            btnMoreHeight.FlatAppearance.BorderSize = 0;
            btnMoreHeight.FlatStyle = FlatStyle.Flat;
            btnMoreHeight.Location = new Point(167, 717);
            btnMoreHeight.Name = "btnMoreHeight";
            btnMoreHeight.Size = new Size(33, 23);
            btnMoreHeight.TabIndex = 9;
            btnMoreHeight.Text = "+";
            btnMoreHeight.UseVisualStyleBackColor = true;
            btnMoreHeight.Click += btnMoreHeight_Click;
            // 
            // btnMoreWidth
            // 
            btnMoreWidth.FlatAppearance.BorderSize = 0;
            btnMoreWidth.FlatStyle = FlatStyle.Flat;
            btnMoreWidth.Location = new Point(167, 774);
            btnMoreWidth.Name = "btnMoreWidth";
            btnMoreWidth.Size = new Size(33, 23);
            btnMoreWidth.TabIndex = 9;
            btnMoreWidth.Text = "+";
            btnMoreWidth.UseVisualStyleBackColor = true;
            btnMoreWidth.Click += btnMoreWidth_Click;
            // 
            // btnLessWidth
            // 
            btnLessWidth.FlatAppearance.BorderSize = 0;
            btnLessWidth.FlatStyle = FlatStyle.Flat;
            btnLessWidth.Location = new Point(15, 775);
            btnLessWidth.Name = "btnLessWidth";
            btnLessWidth.Size = new Size(33, 23);
            btnLessWidth.TabIndex = 9;
            btnLessWidth.Text = "-";
            btnLessWidth.UseVisualStyleBackColor = true;
            btnLessWidth.Click += btnLessWidth_Click;
            // 
            // btnLessHeight
            // 
            btnLessHeight.FlatAppearance.BorderSize = 0;
            btnLessHeight.FlatStyle = FlatStyle.Flat;
            btnLessHeight.Location = new Point(15, 716);
            btnLessHeight.Name = "btnLessHeight";
            btnLessHeight.Size = new Size(33, 23);
            btnLessHeight.TabIndex = 9;
            btnLessHeight.Text = "-";
            btnLessHeight.UseVisualStyleBackColor = true;
            btnLessHeight.Click += btnLessHeight_Click;
            // 
            // txtXco
            // 
            txtXco.Location = new Point(180, 556);
            txtXco.Name = "txtXco";
            txtXco.PlaceholderText = "X";
            txtXco.Size = new Size(34, 23);
            txtXco.TabIndex = 10;
            // 
            // txtYco
            // 
            txtYco.Location = new Point(180, 606);
            txtYco.Name = "txtYco";
            txtYco.PlaceholderText = "Y";
            txtYco.Size = new Size(34, 23);
            txtYco.TabIndex = 10;
            // 
            // btnDeleteTable
            // 
            btnDeleteTable.FlatAppearance.BorderSize = 0;
            btnDeleteTable.FlatStyle = FlatStyle.Flat;
            btnDeleteTable.Location = new Point(31, 850);
            btnDeleteTable.Name = "btnDeleteTable";
            btnDeleteTable.Size = new Size(166, 37);
            btnDeleteTable.TabIndex = 6;
            btnDeleteTable.Text = "Delete Table";
            btnDeleteTable.UseVisualStyleBackColor = true;
            btnDeleteTable.Click += btnDeleteTable_Click;
            // 
            // btnDoAThing
            // 
            btnDoAThing.Location = new Point(125, 983);
            btnDoAThing.Name = "btnDoAThing";
            btnDoAThing.Size = new Size(22, 23);
            btnDoAThing.TabIndex = 17;
            btnDoAThing.Text = "3";
            btnDoAThing.UseVisualStyleBackColor = true;
            btnDoAThing.Click += btnDoAThing_Click;
            // 
            // btnTest2
            // 
            btnTest2.Location = new Point(86, 984);
            btnTest2.Name = "btnTest2";
            btnTest2.Size = new Size(23, 23);
            btnTest2.TabIndex = 16;
            btnTest2.Text = "2";
            btnTest2.UseVisualStyleBackColor = true;
            btnTest2.Click += btnTest2_Click;
            // 
            // btnTest
            // 
            btnTest.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnTest.Location = new Point(43, 982);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(23, 25);
            btnTest.TabIndex = 12;
            btnTest.Text = "1";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // pnlFloorPlan
            // 
            pnlFloorPlan.BackColor = Color.FromArgb(192, 192, 192);
            pnlFloorPlan.Location = new Point(16, 40);
            pnlFloorPlan.Name = "pnlFloorPlan";
            pnlFloorPlan.Size = new Size(672, 877);
            pnlFloorPlan.TabIndex = 2;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDiningAreas.FlatStyle = FlatStyle.Flat;
            cboDiningAreas.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(16, 6);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(355, 28);
            cboDiningAreas.TabIndex = 7;
            cboDiningAreas.SelectedIndexChanged += cboDiningAreas_SelectedIndexChanged;
            // 
            // rdoSections
            // 
            rdoSections.Appearance = Appearance.Button;
            rdoSections.BackColor = Color.FromArgb(158, 171, 222);
            rdoSections.Checked = true;
            rdoSections.FlatAppearance.BorderSize = 0;
            rdoSections.FlatAppearance.CheckedBackColor = Color.FromArgb(49, 56, 82);
            rdoSections.FlatStyle = FlatStyle.Flat;
            rdoSections.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            rdoSections.ForeColor = Color.White;
            rdoSections.Location = new Point(0, 3);
            rdoSections.Name = "rdoSections";
            rdoSections.Size = new Size(160, 37);
            rdoSections.TabIndex = 10;
            rdoSections.TabStop = true;
            rdoSections.Text = "Floorplans";
            rdoSections.TextAlign = ContentAlignment.MiddleCenter;
            rdoSections.UseVisualStyleBackColor = false;
            rdoSections.CheckedChanged += rdoSections_CheckedChanged;
            // 
            // rdoDiningAreas
            // 
            rdoDiningAreas.Appearance = Appearance.Button;
            rdoDiningAreas.BackColor = Color.FromArgb(158, 171, 222);
            rdoDiningAreas.FlatAppearance.BorderSize = 0;
            rdoDiningAreas.FlatAppearance.CheckedBackColor = Color.FromArgb(49, 56, 82);
            rdoDiningAreas.FlatStyle = FlatStyle.Flat;
            rdoDiningAreas.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            rdoDiningAreas.ForeColor = Color.White;
            rdoDiningAreas.Location = new Point(326, 3);
            rdoDiningAreas.Name = "rdoDiningAreas";
            rdoDiningAreas.Size = new Size(160, 37);
            rdoDiningAreas.TabIndex = 10;
            rdoDiningAreas.Text = "Edit Dining Areas";
            rdoDiningAreas.TextAlign = ContentAlignment.MiddleCenter;
            rdoDiningAreas.UseVisualStyleBackColor = false;
            rdoDiningAreas.CheckedChanged += rdoDiningAreas_CheckedChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(rdoShifts);
            panel1.Controls.Add(rdoDiningAreas);
            panel1.Controls.Add(pnlNavHighlight);
            panel1.Controls.Add(rdoSections);
            panel1.Location = new Point(86, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(510, 40);
            panel1.TabIndex = 11;
            // 
            // rdoShifts
            // 
            rdoShifts.Appearance = Appearance.Button;
            rdoShifts.BackColor = Color.FromArgb(158, 171, 222);
            rdoShifts.FlatAppearance.BorderSize = 0;
            rdoShifts.FlatAppearance.CheckedBackColor = Color.FromArgb(49, 56, 82);
            rdoShifts.FlatStyle = FlatStyle.Flat;
            rdoShifts.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            rdoShifts.ForeColor = Color.White;
            rdoShifts.Location = new Point(163, 3);
            rdoShifts.Name = "rdoShifts";
            rdoShifts.Size = new Size(160, 37);
            rdoShifts.TabIndex = 0;
            rdoShifts.TabStop = true;
            rdoShifts.Text = "Shifts";
            rdoShifts.TextAlign = ContentAlignment.MiddleCenter;
            rdoShifts.UseVisualStyleBackColor = false;
            rdoShifts.CheckedChanged += rdoShifts_CheckedChanged;
            // 
            // pnlNavHighlight
            // 
            pnlNavHighlight.BackColor = Color.FromArgb(255, 103, 0);
            pnlNavHighlight.Location = new Point(0, 0);
            pnlNavHighlight.Name = "pnlNavHighlight";
            pnlNavHighlight.Size = new Size(160, 3);
            pnlNavHighlight.TabIndex = 0;
            // 
            // dtpFloorplan
            // 
            dtpFloorplan.CalendarFont = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point);
            dtpFloorplan.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            dtpFloorplan.Format = DateTimePickerFormat.Short;
            dtpFloorplan.Location = new Point(426, 7);
            dtpFloorplan.Name = "dtpFloorplan";
            dtpFloorplan.Size = new Size(124, 27);
            dtpFloorplan.TabIndex = 14;
            dtpFloorplan.ValueChanged += dtpFloorplan_ValueChanged;
            // 
            // cbIsAM
            // 
            cbIsAM.Appearance = Appearance.Button;
            cbIsAM.BackColor = Color.FromArgb(255, 255, 192);
            cbIsAM.FlatStyle = FlatStyle.Flat;
            cbIsAM.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAM.Location = new Point(597, 8);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(56, 26);
            cbIsAM.TabIndex = 15;
            cbIsAM.Text = "PM";
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAM.UseVisualStyleBackColor = false;
            // 
            // btnDayBefore
            // 
            btnDayBefore.FlatStyle = FlatStyle.Flat;
            btnDayBefore.Image = Resource1.BackArrow;
            btnDayBefore.Location = new Point(399, 7);
            btnDayBefore.Name = "btnDayBefore";
            btnDayBefore.Size = new Size(21, 27);
            btnDayBefore.TabIndex = 16;
            btnDayBefore.UseVisualStyleBackColor = true;
            btnDayBefore.Click += btnDayBefore_Click;
            // 
            // btnNextDay
            // 
            btnNextDay.FlatStyle = FlatStyle.Flat;
            btnNextDay.Image = Resource1.forwardArrow;
            btnNextDay.Location = new Point(556, 7);
            btnNextDay.Name = "btnNextDay";
            btnNextDay.Size = new Size(21, 29);
            btnNextDay.TabIndex = 17;
            btnNextDay.UseVisualStyleBackColor = true;
            btnNextDay.Click += btnNextDay_Click;
            // 
            // btnCloseApp
            // 
            btnCloseApp.FlatAppearance.BorderSize = 0;
            btnCloseApp.FlatStyle = FlatStyle.Flat;
            btnCloseApp.Location = new Point(1232, 3);
            btnCloseApp.Name = "btnCloseApp";
            btnCloseApp.Size = new Size(32, 23);
            btnCloseApp.TabIndex = 18;
            btnCloseApp.Text = "X";
            btnCloseApp.UseVisualStyleBackColor = true;
            btnCloseApp.Click += btnCloseApp_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(178, 87, 46);
            panel3.Controls.Add(dtpFloorplan);
            panel3.Controls.Add(cbIsAM);
            panel3.Controls.Add(pnlServers);
            panel3.Controls.Add(btnNextDay);
            panel3.Controls.Add(pnlFloorPlan);
            panel3.Controls.Add(cboDiningAreas);
            panel3.Controls.Add(btnDayBefore);
            panel3.Location = new Point(281, 65);
            panel3.Name = "panel3";
            panel3.Size = new Size(960, 933);
            panel3.TabIndex = 19;
            // 
            // pnlNavigationWindow
            // 
            pnlNavigationWindow.BackColor = Color.FromArgb(49, 56, 82);
            pnlNavigationWindow.Dock = DockStyle.Bottom;
            pnlNavigationWindow.Location = new Point(0, 43);
            pnlNavigationWindow.Name = "pnlNavigationWindow";
            pnlNavigationWindow.Size = new Size(1264, 979);
            pnlNavigationWindow.TabIndex = 20;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(9, 35, 39);
            ClientSize = new Size(1264, 1022);
            Controls.Add(btnDoAThing);
            Controls.Add(btnCloseApp);
            Controls.Add(btnTest2);
            Controls.Add(btnTest);
            Controls.Add(panel1);
            Controls.Add(pnlAddTables);
            Controls.Add(panel3);
            Controls.Add(pnlNavigationWindow);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            pnlServers.ResumeLayout(false);
            pnlServers.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudServerCount).EndInit();
            pnlAddTables.ResumeLayout(false);
            pnlAddTables.PerformLayout();
            pnlSections.ResumeLayout(false);
            pnlSections.PerformLayout();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
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
        private FlowLayoutPanel flowSectionSelect;
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
        private CheckBox cbIsAM;
        private Button btnRemoveSection;
        private Button btnAddSection;
        private Button btnTest2;
        private Button btnDoAThing;
        private Button btnDayBefore;
        private Button btnNextDay;
        private Button btnCloseApp;
        private Panel panel2;
        private Panel panel3;
        private Label lblDiningRoomName;
        private Panel pnlNavigationWindow;
        private Panel pnlNavHighlight;
        private RadioButton rdoShifts;
    }
}