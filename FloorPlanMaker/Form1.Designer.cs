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
            flowServersInFloorplan = new FlowLayoutPanel();
            lblDiningRoomName = new Label();
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
            pnlSections = new Panel();
            btnGenerateSectionLines = new Button();
            btnChooseTemplate = new Button();
            btnPrint = new Button();
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
            lblPanel2Text = new Label();
            pnlAddTables = new Panel();
            pnlServers.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudServerCount).BeginInit();
            pnlSections.SuspendLayout();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            pnlAddTables.SuspendLayout();
            SuspendLayout();
            // 
            // pnlServers
            // 
            pnlServers.BackColor = Color.Silver;
            pnlServers.Controls.Add(flowServersInFloorplan);
            pnlServers.Controls.Add(lblDiningRoomName);
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
            // flowServersInFloorplan
            // 
            flowServersInFloorplan.Location = new Point(6, 255);
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
            txtTemplateName.Location = new Point(18, 713);
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
            btnSaveFloorplanTemplate.Location = new Point(18, 745);
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
            btnAddSectionLabels.Location = new Point(16, 5);
            btnAddSectionLabels.Name = "btnAddSectionLabels";
            btnAddSectionLabels.Size = new Size(192, 28);
            btnAddSectionLabels.TabIndex = 13;
            btnAddSectionLabels.Text = "Add Section Labels";
            btnAddSectionLabels.UseVisualStyleBackColor = true;
            btnAddSectionLabels.Click += btnAddSectionLabels_Click;
            // 
            // flowSectionSelect
            // 
            flowSectionSelect.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            flowSectionSelect.Location = new Point(21, 69);
            flowSectionSelect.Name = "flowSectionSelect";
            flowSectionSelect.Size = new Size(212, 601);
            flowSectionSelect.TabIndex = 9;
            // 
            // pnlSections
            // 
            pnlSections.Controls.Add(flowSectionSelect);
            pnlSections.Controls.Add(label9);
            pnlSections.Controls.Add(cboFloorplanTemplates);
            pnlSections.Controls.Add(nudServerCount);
            pnlSections.Controls.Add(txtTemplateName);
            pnlSections.Controls.Add(btnSaveFloorplanTemplate);
            pnlSections.Controls.Add(btnRemoveSection);
            pnlSections.Controls.Add(btnGenerateSectionLines);
            pnlSections.Controls.Add(btnChooseTemplate);
            pnlSections.Controls.Add(btnAddSection);
            pnlSections.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            pnlSections.Location = new Point(8, 32);
            pnlSections.Name = "pnlSections";
            pnlSections.Size = new Size(239, 869);
            pnlSections.TabIndex = 8;
            // 
            // btnGenerateSectionLines
            // 
            btnGenerateSectionLines.FlatStyle = FlatStyle.Flat;
            btnGenerateSectionLines.ForeColor = Color.Black;
            btnGenerateSectionLines.Location = new Point(15, 676);
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
            // btnPrint
            // 
            btnPrint.BackColor = Color.FromArgb(255, 103, 0);
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnPrint.Location = new Point(214, 5);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(184, 28);
            btnPrint.TabIndex = 13;
            btnPrint.Text = "Print";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
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
            cboDiningAreas.Location = new Point(404, 5);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(284, 28);
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
            panel1.Location = new Point(40, 3);
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
            dtpFloorplan.Location = new Point(730, 5);
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
            cbIsAM.Location = new Point(892, 5);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(56, 28);
            cbIsAM.TabIndex = 15;
            cbIsAM.Text = "PM";
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            cbIsAM.UseVisualStyleBackColor = false;
            // 
            // btnDayBefore
            // 
            btnDayBefore.FlatStyle = FlatStyle.Flat;
            btnDayBefore.Image = Resource1.BackArrow;
            btnDayBefore.Location = new Point(703, 5);
            btnDayBefore.Name = "btnDayBefore";
            btnDayBefore.Size = new Size(21, 28);
            btnDayBefore.TabIndex = 16;
            btnDayBefore.UseVisualStyleBackColor = true;
            btnDayBefore.Click += btnDayBefore_Click;
            // 
            // btnNextDay
            // 
            btnNextDay.FlatStyle = FlatStyle.Flat;
            btnNextDay.Image = Resource1.forwardArrow;
            btnNextDay.Location = new Point(860, 5);
            btnNextDay.Name = "btnNextDay";
            btnNextDay.Size = new Size(21, 28);
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
            panel3.Controls.Add(btnAddSectionLabels);
            panel3.Controls.Add(btnPrint);
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
            // pnlAddTables
            // 
            pnlAddTables.BackColor = Color.FromArgb(178, 87, 46);
            pnlAddTables.Controls.Add(pnlSections);
            pnlAddTables.Controls.Add(lblPanel2Text);
            pnlAddTables.ForeColor = Color.White;
            pnlAddTables.Location = new Point(12, 73);
            pnlAddTables.Name = "pnlAddTables";
            pnlAddTables.Size = new Size(250, 911);
            pnlAddTables.TabIndex = 1;
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
            ((System.ComponentModel.ISupportInitialize)nudServerCount).EndInit();
            pnlSections.ResumeLayout(false);
            pnlSections.PerformLayout();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            pnlAddTables.ResumeLayout(false);
            pnlAddTables.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlServers;
        private Label label1;
        private Panel pnlFloorPlan;
        private ComboBox cboDiningAreas;
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
        private ComboBox cboFloorplanTemplates;
        private TextBox txtTemplateName;
        private Button btnPrint;
        private DateTimePicker dtpFloorplan;
        private Button btnChooseTemplate;
        private Button btnGenerateSectionLines;
        private CheckBox cbIsAM;
        private Button btnRemoveSection;
        private Button btnAddSection;
        private Button btnTest2;
        private Button btnDoAThing;
        private Button btnDayBefore;
        private Button btnNextDay;
        private Button btnCloseApp;
        private Panel panel3;
        private Label lblDiningRoomName;
        private Panel pnlNavigationWindow;
        private Panel pnlNavHighlight;
        private RadioButton rdoShifts;
        private Label lblPanel2Text;
        private Panel pnlAddTables;
    }
}