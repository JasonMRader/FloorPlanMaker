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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            flowServersInFloorplan = new FlowLayoutPanel();
            lblServerMaxCovers = new Label();
            lblServerAverageCovers = new Label();
            btnSaveFloorplanTemplate = new Button();
            flowSectionSelect = new FlowLayoutPanel();
            cbTableDisplayMode = new CheckBox();
            btnChooseTemplate = new Button();
            btnPrint = new Button();
            pnlFloorPlan = new Panel();
            cboDiningAreas = new ComboBox();
            rdoSections = new RadioButton();
            rdoDiningAreas = new RadioButton();
            panel1 = new Panel();
            rdoSettings = new RadioButton();
            rdoShifts = new RadioButton();
            pnlNavHighlight = new Panel();
            cbIsAM = new CheckBox();
            btnDayBefore = new Button();
            btnNextDay = new Button();
            btnCloseApp = new Button();
            pnlFloorplanContainer = new Panel();
            pnlNotifications = new Panel();
            sectionHeaderDisplay = new FloorplanUserControlLibrary.SectionHeaderDisplay();
            lblDateSelected = new Label();
            pnlNavigationWindow = new Panel();
            pnlMainContainer = new Panel();
            label4 = new Label();
            pnlLocationIndicator2 = new Panel();
            pnlIndicator2 = new Panel();
            pnlAreaIndicatorContainer = new Panel();
            pnlAreaIndicator = new Panel();
            flowSideDiningAreas = new FlowLayoutPanel();
            flowLayoutPanel3 = new FlowLayoutPanel();
            btnEditRoster = new Button();
            cbDrawToggle = new CheckBox();
            btnEraseAllSections = new Button();
            flowLayoutPanel4 = new FlowLayoutPanel();
            btnSaveColorPDF = new Button();
            flowLayoutPanel1 = new FlowLayoutPanel();
            flowLayoutPanel2 = new FlowLayoutPanel();
            btnAutomatic = new Button();
            pnlTemplateContainer = new Panel();
            pnlSideContainer = new Panel();
            pnlSectionsAndServers = new Panel();
            pnlStatMode = new Panel();
            lblTotalSales = new Label();
            label3 = new Label();
            btnClearDates = new Button();
            btnApplyDates = new Button();
            btnAddCustomDate = new Button();
            dtpCustomStatDateSelect = new DateTimePicker();
            lbFilteredStatDates = new ListBox();
            label2 = new Label();
            label1 = new Label();
            dtpStatRangeEnd = new DateTimePicker();
            dtpStatRangeStart = new DateTimePicker();
            rdoLastWeekdayStats = new RadioButton();
            rdoSelectedDatesStats = new RadioButton();
            rdoRangeStats = new RadioButton();
            rdoLastFourWeekdayStats = new RadioButton();
            rdoYearlyAverageStats = new RadioButton();
            rdoDayOfStats = new RadioButton();
            rdoYesterdayStats = new RadioButton();
            rdoSales = new RadioButton();
            rdoViewServerFlow = new RadioButton();
            rdoViewSectionFlow = new RadioButton();
            btnDeleteSelectedFloorplan = new Button();
            btnHelp = new Button();
            toolTip1 = new ToolTip(components);
            btnReportBug = new Button();
            rdoReservations = new RadioButton();
            rdoWeather = new RadioButton();
            btnUploadSalesData = new Button();
            helpProvider1 = new HelpProvider();
            panel2 = new Panel();
            pnlSideDetails = new Panel();
            flowResoDisplay = new FlowLayoutPanel();
            pnlShiftDetails = new Panel();
            flowWeatherDisplay = new FlowLayoutPanel();
            pnlSalesDataUpdated = new Panel();
            lblMissingSalesData = new Label();
            btnTest = new Button();
            btnTestLabels = new Button();
            flowSectionSelect.SuspendLayout();
            panel1.SuspendLayout();
            pnlFloorplanContainer.SuspendLayout();
            pnlNotifications.SuspendLayout();
            pnlNavigationWindow.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            pnlLocationIndicator2.SuspendLayout();
            pnlAreaIndicatorContainer.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            flowLayoutPanel4.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            pnlSideContainer.SuspendLayout();
            pnlSectionsAndServers.SuspendLayout();
            pnlStatMode.SuspendLayout();
            panel2.SuspendLayout();
            pnlSideDetails.SuspendLayout();
            pnlSalesDataUpdated.SuspendLayout();
            SuspendLayout();
            // 
            // flowServersInFloorplan
            // 
            flowServersInFloorplan.AutoScroll = true;
            flowServersInFloorplan.BackColor = Color.WhiteSmoke;
            flowServersInFloorplan.Location = new Point(0, 28);
            flowServersInFloorplan.Name = "flowServersInFloorplan";
            flowServersInFloorplan.Padding = new Padding(10, 20, 0, 0);
            flowServersInFloorplan.Size = new Size(335, 865);
            flowServersInFloorplan.TabIndex = 2;
            flowServersInFloorplan.Visible = false;
            flowServersInFloorplan.MouseClick += flowServersInFloorplan_MouseClick;
            // 
            // lblServerMaxCovers
            // 
            lblServerMaxCovers.AutoSize = true;
            lblServerMaxCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerMaxCovers.ForeColor = Color.Black;
            lblServerMaxCovers.Location = new Point(0, 20);
            lblServerMaxCovers.Margin = new Padding(0, 0, 3, 0);
            lblServerMaxCovers.Name = "lblServerMaxCovers";
            lblServerMaxCovers.Size = new Size(19, 21);
            lblServerMaxCovers.TabIndex = 5;
            lblServerMaxCovers.Text = "0";
            lblServerMaxCovers.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblServerAverageCovers
            // 
            lblServerAverageCovers.AutoSize = true;
            lblServerAverageCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerAverageCovers.ForeColor = Color.Black;
            lblServerAverageCovers.Location = new Point(22, 20);
            lblServerAverageCovers.Margin = new Padding(0, 0, 3, 15);
            lblServerAverageCovers.Name = "lblServerAverageCovers";
            lblServerAverageCovers.Size = new Size(19, 21);
            lblServerAverageCovers.TabIndex = 5;
            lblServerAverageCovers.Text = "0";
            lblServerAverageCovers.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // btnSaveFloorplanTemplate
            // 
            btnSaveFloorplanTemplate.BackColor = Color.FromArgb(100, 130, 180);
            btnSaveFloorplanTemplate.FlatAppearance.BorderSize = 0;
            btnSaveFloorplanTemplate.FlatStyle = FlatStyle.Flat;
            btnSaveFloorplanTemplate.ForeColor = Color.Black;
            btnSaveFloorplanTemplate.Image = FloorPlanMakerUI.Properties.Resources.ExtraSmallSave;
            btnSaveFloorplanTemplate.Location = new Point(8, 66);
            btnSaveFloorplanTemplate.Margin = new Padding(3, 3, 3, 10);
            btnSaveFloorplanTemplate.Name = "btnSaveFloorplanTemplate";
            btnSaveFloorplanTemplate.Size = new Size(45, 45);
            btnSaveFloorplanTemplate.TabIndex = 12;
            toolTip1.SetToolTip(btnSaveFloorplanTemplate, "Save the Current Sections as a Template");
            btnSaveFloorplanTemplate.UseVisualStyleBackColor = false;
            btnSaveFloorplanTemplate.Click += btnSaveFloorplanTemplate_Click;
            // 
            // flowSectionSelect
            // 
            flowSectionSelect.AutoScroll = true;
            flowSectionSelect.BackColor = Color.Silver;
            flowSectionSelect.Controls.Add(lblServerMaxCovers);
            flowSectionSelect.Controls.Add(lblServerAverageCovers);
            flowSectionSelect.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            flowSectionSelect.Location = new Point(0, 28);
            flowSectionSelect.Name = "flowSectionSelect";
            flowSectionSelect.Padding = new Padding(0, 20, 0, 0);
            flowSectionSelect.Size = new Size(335, 865);
            flowSectionSelect.TabIndex = 9;
            // 
            // cbTableDisplayMode
            // 
            cbTableDisplayMode.Appearance = Appearance.Button;
            cbTableDisplayMode.BackColor = Color.FromArgb(100, 130, 180);
            cbTableDisplayMode.FlatAppearance.BorderSize = 0;
            cbTableDisplayMode.FlatStyle = FlatStyle.Flat;
            cbTableDisplayMode.ForeColor = Color.Black;
            cbTableDisplayMode.Image = FloorPlanMakerUI.Properties.Resources.noun_view_Smalll;
            cbTableDisplayMode.Location = new Point(8, 121);
            cbTableDisplayMode.Margin = new Padding(3, 3, 3, 10);
            cbTableDisplayMode.Name = "cbTableDisplayMode";
            cbTableDisplayMode.Size = new Size(45, 45);
            cbTableDisplayMode.TabIndex = 16;
            cbTableDisplayMode.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(cbTableDisplayMode, "Toggle Table View Mode");
            cbTableDisplayMode.UseVisualStyleBackColor = false;
            cbTableDisplayMode.CheckedChanged += cbTableDisplayMode_CheckedChanged;
            // 
            // btnChooseTemplate
            // 
            btnChooseTemplate.BackColor = Color.FromArgb(100, 130, 180);
            btnChooseTemplate.FlatAppearance.BorderSize = 0;
            btnChooseTemplate.FlatStyle = FlatStyle.Flat;
            btnChooseTemplate.ForeColor = Color.Black;
            btnChooseTemplate.Image = FloorPlanMakerUI.Properties.Resources.blueSMall;
            btnChooseTemplate.Location = new Point(8, 8);
            btnChooseTemplate.Margin = new Padding(3, 3, 3, 10);
            btnChooseTemplate.Name = "btnChooseTemplate";
            btnChooseTemplate.Size = new Size(45, 45);
            btnChooseTemplate.TabIndex = 14;
            toolTip1.SetToolTip(btnChooseTemplate, "Choose a Template");
            btnChooseTemplate.UseVisualStyleBackColor = false;
            btnChooseTemplate.Click += btnChooseTemplate_Click;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.FromArgb(100, 130, 180);
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnPrint.Image = FloorPlanMakerUI.Properties.Resources.lilPrinter;
            btnPrint.Location = new Point(8, 66);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(45, 45);
            btnPrint.TabIndex = 13;
            toolTip1.SetToolTip(btnPrint, "Print and Save Floorplan");
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // pnlFloorPlan
            // 
            pnlFloorPlan.BackColor = Color.WhiteSmoke;
            pnlFloorPlan.Location = new Point(5, 124);
            pnlFloorPlan.Name = "pnlFloorPlan";
            pnlFloorPlan.Size = new Size(672, 815);
            pnlFloorPlan.TabIndex = 2;
            pnlFloorPlan.MouseClick += pnlFloorPlan_MouseClick;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDiningAreas.FlatStyle = FlatStyle.Flat;
            cboDiningAreas.Font = new Font("Segoe UI Semilight", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(203, 5);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(105, 21);
            cboDiningAreas.TabIndex = 7;
            toolTip1.SetToolTip(cboDiningAreas, "Up / Down Arrows to Cycle");
            cboDiningAreas.Visible = false;
            // 
            // rdoSections
            // 
            rdoSections.Appearance = Appearance.Button;
            rdoSections.BackColor = Color.FromArgb(100, 130, 180);
            rdoSections.FlatAppearance.BorderSize = 0;
            rdoSections.FlatAppearance.CheckedBackColor = Color.FromArgb(49, 56, 82);
            rdoSections.FlatStyle = FlatStyle.Flat;
            rdoSections.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            rdoSections.ForeColor = Color.White;
            rdoSections.Location = new Point(0, 3);
            rdoSections.Name = "rdoSections";
            rdoSections.Size = new Size(160, 37);
            rdoSections.TabIndex = 10;
            rdoSections.Text = "Floorplans";
            rdoSections.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(rdoSections, "Make and Assign Sections");
            rdoSections.UseVisualStyleBackColor = false;
            rdoSections.CheckedChanged += rdoSections_CheckedChanged;
            // 
            // rdoDiningAreas
            // 
            rdoDiningAreas.Appearance = Appearance.Button;
            rdoDiningAreas.BackColor = Color.FromArgb(100, 130, 180);
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
            panel1.Controls.Add(rdoSettings);
            panel1.Controls.Add(rdoShifts);
            panel1.Controls.Add(rdoDiningAreas);
            panel1.Controls.Add(pnlNavHighlight);
            panel1.Controls.Add(rdoSections);
            panel1.Location = new Point(70, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(554, 40);
            panel1.TabIndex = 11;
            // 
            // rdoSettings
            // 
            rdoSettings.Appearance = Appearance.Button;
            rdoSettings.BackColor = Color.FromArgb(100, 130, 180);
            rdoSettings.FlatAppearance.BorderSize = 0;
            rdoSettings.FlatAppearance.CheckedBackColor = Color.FromArgb(49, 56, 82);
            rdoSettings.FlatStyle = FlatStyle.Flat;
            rdoSettings.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            rdoSettings.ForeColor = Color.White;
            rdoSettings.Image = FloorPlanMakerUI.Properties.Resources.settings34X;
            rdoSettings.Location = new Point(490, 3);
            rdoSettings.Name = "rdoSettings";
            rdoSettings.Size = new Size(47, 37);
            rdoSettings.TabIndex = 11;
            rdoSettings.TextAlign = ContentAlignment.MiddleCenter;
            rdoSettings.UseVisualStyleBackColor = false;
            rdoSettings.CheckedChanged += rdoSettings_CheckedChanged;
            // 
            // rdoShifts
            // 
            rdoShifts.Appearance = Appearance.Button;
            rdoShifts.BackColor = Color.FromArgb(100, 130, 180);
            rdoShifts.Checked = true;
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
            toolTip1.SetToolTip(rdoShifts, "Assign the Servers to Floorplans");
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
            // cbIsAM
            // 
            cbIsAM.Appearance = Appearance.Button;
            cbIsAM.BackColor = Color.FromArgb(251, 175, 0);
            cbIsAM.Checked = true;
            cbIsAM.CheckState = CheckState.Checked;
            cbIsAM.FlatStyle = FlatStyle.Flat;
            cbIsAM.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAM.Image = FloorPlanMakerUI.Properties.Resources.smallSunrise;
            cbIsAM.Location = new Point(618, 5);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(56, 30);
            cbIsAM.TabIndex = 15;
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(cbIsAM, "AM or PM Shift");
            cbIsAM.UseVisualStyleBackColor = false;
            cbIsAM.CheckedChanged += cbIsAM_CheckedChanged;
            // 
            // btnDayBefore
            // 
            btnDayBefore.BackColor = Color.FromArgb(100, 130, 180);
            btnDayBefore.FlatAppearance.BorderSize = 0;
            btnDayBefore.FlatStyle = FlatStyle.Flat;
            btnDayBefore.Image = FloorPlanMakerUI.Properties.Resources.smallBackArrow;
            btnDayBefore.Location = new Point(8, 5);
            btnDayBefore.Name = "btnDayBefore";
            btnDayBefore.Size = new Size(21, 30);
            btnDayBefore.TabIndex = 16;
            toolTip1.SetToolTip(btnDayBefore, "Left / Right Arrow Keys to Cycle");
            btnDayBefore.UseVisualStyleBackColor = false;
            btnDayBefore.Click += btnDayBefore_Click;
            // 
            // btnNextDay
            // 
            btnNextDay.BackColor = Color.FromArgb(100, 130, 180);
            btnNextDay.FlatAppearance.BorderSize = 0;
            btnNextDay.FlatStyle = FlatStyle.Flat;
            btnNextDay.Image = FloorPlanMakerUI.Properties.Resources.smallForwardArrow;
            btnNextDay.Location = new Point(591, 5);
            btnNextDay.Name = "btnNextDay";
            btnNextDay.Size = new Size(21, 30);
            btnNextDay.TabIndex = 17;
            toolTip1.SetToolTip(btnNextDay, "Left / Right Arrow Keys to Cycle");
            btnNextDay.UseVisualStyleBackColor = false;
            btnNextDay.Click += btnNextDay_Click;
            // 
            // btnCloseApp
            // 
            btnCloseApp.FlatAppearance.BorderSize = 0;
            btnCloseApp.FlatStyle = FlatStyle.Flat;
            btnCloseApp.Image = FloorPlanMakerUI.Properties.Resources.X15x;
            btnCloseApp.Location = new Point(1465, 12);
            btnCloseApp.Name = "btnCloseApp";
            btnCloseApp.Size = new Size(24, 23);
            btnCloseApp.TabIndex = 18;
            btnCloseApp.Text = "X";
            btnCloseApp.UseVisualStyleBackColor = true;
            btnCloseApp.Click += btnCloseApp_Click;
            // 
            // pnlFloorplanContainer
            // 
            pnlFloorplanContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlFloorplanContainer.BackColor = Color.WhiteSmoke;
            pnlFloorplanContainer.Controls.Add(pnlNotifications);
            pnlFloorplanContainer.Controls.Add(pnlFloorPlan);
            pnlFloorplanContainer.Controls.Add(lblDateSelected);
            pnlFloorplanContainer.Controls.Add(cbIsAM);
            pnlFloorplanContainer.Controls.Add(btnDayBefore);
            pnlFloorplanContainer.Controls.Add(btnNextDay);
            pnlFloorplanContainer.Location = new Point(80, 10);
            pnlFloorplanContainer.Name = "pnlFloorplanContainer";
            pnlFloorplanContainer.Size = new Size(684, 944);
            pnlFloorplanContainer.TabIndex = 19;
            // 
            // pnlNotifications
            // 
            pnlNotifications.BackColor = Color.WhiteSmoke;
            pnlNotifications.Controls.Add(sectionHeaderDisplay);
            pnlNotifications.Location = new Point(0, 40);
            pnlNotifications.Name = "pnlNotifications";
            pnlNotifications.Size = new Size(684, 78);
            pnlNotifications.TabIndex = 23;
            // 
            // sectionHeaderDisplay
            // 
            sectionHeaderDisplay.BorderStyle = BorderStyle.FixedSingle;
            sectionHeaderDisplay.Location = new Point(8, 5);
            sectionHeaderDisplay.Name = "sectionHeaderDisplay";
            sectionHeaderDisplay.Size = new Size(666, 68);
            sectionHeaderDisplay.TabIndex = 0;
            sectionHeaderDisplay.Load += sectionHeaderDisplay_Load;
            // 
            // lblDateSelected
            // 
            lblDateSelected.BackColor = Color.FromArgb(100, 130, 180);
            lblDateSelected.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblDateSelected.ForeColor = Color.White;
            lblDateSelected.Location = new Point(35, 5);
            lblDateSelected.Name = "lblDateSelected";
            lblDateSelected.Size = new Size(550, 30);
            lblDateSelected.TabIndex = 22;
            lblDateSelected.Text = "Fri, 11/11";
            lblDateSelected.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(lblDateSelected, "Left / Right Arrow Keys to Cycle");
            lblDateSelected.Click += lblDateSelected_Click;
            // 
            // pnlNavigationWindow
            // 
            pnlNavigationWindow.BackColor = Color.FromArgb(225, 225, 225);
            pnlNavigationWindow.Controls.Add(pnlMainContainer);
            pnlNavigationWindow.Controls.Add(pnlSideContainer);
            pnlNavigationWindow.Location = new Point(0, 43);
            pnlNavigationWindow.Name = "pnlNavigationWindow";
            pnlNavigationWindow.Size = new Size(1264, 999);
            pnlNavigationWindow.TabIndex = 20;
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlMainContainer.BackColor = Color.FromArgb(180, 190, 200);
            pnlMainContainer.Controls.Add(label4);
            pnlMainContainer.Controls.Add(pnlLocationIndicator2);
            pnlMainContainer.Controls.Add(pnlAreaIndicatorContainer);
            pnlMainContainer.Controls.Add(flowSideDiningAreas);
            pnlMainContainer.Controls.Add(flowLayoutPanel3);
            pnlMainContainer.Controls.Add(flowLayoutPanel4);
            pnlMainContainer.Controls.Add(flowLayoutPanel1);
            pnlMainContainer.Controls.Add(flowLayoutPanel2);
            pnlMainContainer.Controls.Add(pnlFloorplanContainer);
            pnlMainContainer.Controls.Add(pnlTemplateContainer);
            pnlMainContainer.Location = new Point(410, 10);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Size = new Size(837, 977);
            pnlMainContainer.TabIndex = 20;
            // 
            // label4
            // 
            label4.BackColor = Color.WhiteSmoke;
            label4.Font = new Font("Segoe UI Semibold", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label4.ForeColor = Color.Black;
            label4.Location = new Point(772, 10);
            label4.Name = "label4";
            label4.Size = new Size(55, 42);
            label4.TabIndex = 25;
            label4.Text = "Dining Areas";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlLocationIndicator2
            // 
            pnlLocationIndicator2.Controls.Add(pnlIndicator2);
            pnlLocationIndicator2.Location = new Point(827, 50);
            pnlLocationIndicator2.Name = "pnlLocationIndicator2";
            pnlLocationIndicator2.Size = new Size(3, 904);
            pnlLocationIndicator2.TabIndex = 27;
            // 
            // pnlIndicator2
            // 
            pnlIndicator2.BackColor = Color.Red;
            pnlIndicator2.Location = new Point(0, 0);
            pnlIndicator2.Name = "pnlIndicator2";
            pnlIndicator2.Size = new Size(3, 163);
            pnlIndicator2.TabIndex = 26;
            // 
            // pnlAreaIndicatorContainer
            // 
            pnlAreaIndicatorContainer.BackColor = Color.FromArgb(225, 225, 225);
            pnlAreaIndicatorContainer.Controls.Add(pnlAreaIndicator);
            pnlAreaIndicatorContainer.Location = new Point(764, 10);
            pnlAreaIndicatorContainer.Name = "pnlAreaIndicatorContainer";
            pnlAreaIndicatorContainer.Size = new Size(8, 944);
            pnlAreaIndicatorContainer.TabIndex = 26;
            // 
            // pnlAreaIndicator
            // 
            pnlAreaIndicator.BackColor = Color.White;
            pnlAreaIndicator.Location = new Point(0, 0);
            pnlAreaIndicator.Name = "pnlAreaIndicator";
            pnlAreaIndicator.Size = new Size(8, 163);
            pnlAreaIndicator.TabIndex = 26;
            // 
            // flowSideDiningAreas
            // 
            flowSideDiningAreas.BackColor = Color.WhiteSmoke;
            flowSideDiningAreas.FlowDirection = FlowDirection.TopDown;
            flowSideDiningAreas.Location = new Point(772, 50);
            flowSideDiningAreas.Name = "flowSideDiningAreas";
            flowSideDiningAreas.Size = new Size(55, 904);
            flowSideDiningAreas.TabIndex = 25;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.BackColor = Color.WhiteSmoke;
            flowLayoutPanel3.Controls.Add(btnEditRoster);
            flowLayoutPanel3.Controls.Add(cbDrawToggle);
            flowLayoutPanel3.Controls.Add(cbTableDisplayMode);
            flowLayoutPanel3.Controls.Add(btnEraseAllSections);
            flowLayoutPanel3.Location = new Point(10, 228);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Padding = new Padding(5, 5, 0, 0);
            flowLayoutPanel3.Size = new Size(60, 233);
            flowLayoutPanel3.TabIndex = 25;
            // 
            // btnEditRoster
            // 
            btnEditRoster.BackColor = Color.FromArgb(100, 130, 180);
            btnEditRoster.FlatAppearance.BorderSize = 0;
            btnEditRoster.FlatStyle = FlatStyle.Flat;
            btnEditRoster.Image = FloorPlanMakerUI.Properties.Resources.EditGroup_60px;
            btnEditRoster.Location = new Point(8, 8);
            btnEditRoster.Margin = new Padding(3, 3, 3, 7);
            btnEditRoster.Name = "btnEditRoster";
            btnEditRoster.Size = new Size(45, 45);
            btnEditRoster.TabIndex = 17;
            toolTip1.SetToolTip(btnEditRoster, "Add / Remove Servers From Floorplan");
            btnEditRoster.UseVisualStyleBackColor = false;
            btnEditRoster.Click += btnEditRoster_Click;
            // 
            // cbDrawToggle
            // 
            cbDrawToggle.Appearance = Appearance.Button;
            cbDrawToggle.BackColor = Color.FromArgb(100, 130, 180);
            cbDrawToggle.FlatAppearance.BorderSize = 0;
            cbDrawToggle.FlatStyle = FlatStyle.Flat;
            cbDrawToggle.ForeColor = Color.Black;
            cbDrawToggle.Image = FloorPlanMakerUI.Properties.Resources.bluePrintSmall;
            cbDrawToggle.Location = new Point(8, 63);
            cbDrawToggle.Margin = new Padding(3, 3, 3, 10);
            cbDrawToggle.Name = "cbDrawToggle";
            cbDrawToggle.Size = new Size(45, 45);
            cbDrawToggle.TabIndex = 16;
            cbDrawToggle.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(cbDrawToggle, "Toggle Draw Section Lines");
            cbDrawToggle.UseVisualStyleBackColor = false;
            cbDrawToggle.CheckedChanged += cbDrawToggle_CheckedChanged;
            // 
            // btnEraseAllSections
            // 
            btnEraseAllSections.BackColor = Color.FromArgb(190, 80, 70);
            btnEraseAllSections.FlatAppearance.BorderSize = 0;
            btnEraseAllSections.FlatStyle = FlatStyle.Flat;
            btnEraseAllSections.Image = FloorPlanMakerUI.Properties.Resources.erase_Small;
            btnEraseAllSections.Location = new Point(8, 179);
            btnEraseAllSections.Margin = new Padding(3, 3, 3, 7);
            btnEraseAllSections.Name = "btnEraseAllSections";
            btnEraseAllSections.Size = new Size(45, 45);
            btnEraseAllSections.TabIndex = 17;
            toolTip1.SetToolTip(btnEraseAllSections, "Clear All Sections");
            btnEraseAllSections.UseVisualStyleBackColor = false;
            btnEraseAllSections.Click += btnEraseAllSections_Click;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.BackColor = Color.WhiteSmoke;
            flowLayoutPanel4.Controls.Add(btnSaveColorPDF);
            flowLayoutPanel4.Controls.Add(btnPrint);
            flowLayoutPanel4.Location = new Point(10, 479);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Padding = new Padding(5, 5, 0, 0);
            flowLayoutPanel4.Size = new Size(60, 120);
            flowLayoutPanel4.TabIndex = 25;
            // 
            // btnSaveColorPDF
            // 
            btnSaveColorPDF.BackColor = Color.FromArgb(100, 130, 180);
            btnSaveColorPDF.FlatAppearance.BorderSize = 0;
            btnSaveColorPDF.FlatStyle = FlatStyle.Flat;
            btnSaveColorPDF.ForeColor = Color.Black;
            btnSaveColorPDF.Image = FloorPlanMakerUI.Properties.Resources.download_pdf;
            btnSaveColorPDF.Location = new Point(8, 8);
            btnSaveColorPDF.Margin = new Padding(3, 3, 3, 10);
            btnSaveColorPDF.Name = "btnSaveColorPDF";
            btnSaveColorPDF.Size = new Size(45, 45);
            btnSaveColorPDF.TabIndex = 12;
            toolTip1.SetToolTip(btnSaveColorPDF, "Download a Colorized PDF");
            btnSaveColorPDF.UseVisualStyleBackColor = false;
            btnSaveColorPDF.Click += btnSaveColorPDF_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.BackColor = Color.WhiteSmoke;
            flowLayoutPanel1.Controls.Add(btnChooseTemplate);
            flowLayoutPanel1.Controls.Add(btnSaveFloorplanTemplate);
            flowLayoutPanel1.Location = new Point(10, 88);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Padding = new Padding(5, 5, 0, 0);
            flowLayoutPanel1.Size = new Size(60, 122);
            flowLayoutPanel1.TabIndex = 25;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.BackColor = Color.WhiteSmoke;
            flowLayoutPanel2.Controls.Add(btnAutomatic);
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.Location = new Point(10, 10);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Padding = new Padding(5, 0, 0, 0);
            flowLayoutPanel2.Size = new Size(60, 60);
            flowLayoutPanel2.TabIndex = 24;
            // 
            // btnAutomatic
            // 
            btnAutomatic.BackColor = Color.GreenYellow;
            btnAutomatic.FlatAppearance.BorderSize = 0;
            btnAutomatic.FlatStyle = FlatStyle.Flat;
            btnAutomatic.Image = FloorPlanMakerUI.Properties.Resources.LightlingSmall;
            btnAutomatic.Location = new Point(8, 7);
            btnAutomatic.Margin = new Padding(3, 7, 3, 7);
            btnAutomatic.Name = "btnAutomatic";
            btnAutomatic.Size = new Size(45, 45);
            btnAutomatic.TabIndex = 17;
            toolTip1.SetToolTip(btnAutomatic, "Automate The Next Step [ENTER]");
            btnAutomatic.UseVisualStyleBackColor = false;
            btnAutomatic.Click += btnAutomatic_Click;
            // 
            // pnlTemplateContainer
            // 
            pnlTemplateContainer.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pnlTemplateContainer.Location = new Point(80, 17);
            pnlTemplateContainer.Name = "pnlTemplateContainer";
            pnlTemplateContainer.Size = new Size(684, 940);
            pnlTemplateContainer.TabIndex = 22;
            // 
            // pnlSideContainer
            // 
            pnlSideContainer.BackColor = Color.FromArgb(180, 190, 200);
            pnlSideContainer.Controls.Add(pnlSectionsAndServers);
            pnlSideContainer.Location = new Point(46, 10);
            pnlSideContainer.Name = "pnlSideContainer";
            pnlSideContainer.Size = new Size(364, 977);
            pnlSideContainer.TabIndex = 21;
            // 
            // pnlSectionsAndServers
            // 
            pnlSectionsAndServers.BackColor = Color.WhiteSmoke;
            pnlSectionsAndServers.Controls.Add(rdoViewServerFlow);
            pnlSectionsAndServers.Controls.Add(cboDiningAreas);
            pnlSectionsAndServers.Controls.Add(rdoViewSectionFlow);
            pnlSectionsAndServers.Controls.Add(flowServersInFloorplan);
            pnlSectionsAndServers.Controls.Add(flowSectionSelect);
            pnlSectionsAndServers.ForeColor = Color.White;
            pnlSectionsAndServers.Location = new Point(13, 10);
            pnlSectionsAndServers.Name = "pnlSectionsAndServers";
            pnlSectionsAndServers.Size = new Size(335, 947);
            pnlSectionsAndServers.TabIndex = 1;
            // 
            // pnlStatMode
            // 
            pnlStatMode.BackColor = Color.WhiteSmoke;
            pnlStatMode.Controls.Add(lblTotalSales);
            pnlStatMode.Controls.Add(label3);
            pnlStatMode.Controls.Add(btnClearDates);
            pnlStatMode.Controls.Add(btnApplyDates);
            pnlStatMode.Controls.Add(btnAddCustomDate);
            pnlStatMode.Controls.Add(dtpCustomStatDateSelect);
            pnlStatMode.Controls.Add(lbFilteredStatDates);
            pnlStatMode.Controls.Add(label2);
            pnlStatMode.Controls.Add(label1);
            pnlStatMode.Controls.Add(dtpStatRangeEnd);
            pnlStatMode.Controls.Add(dtpStatRangeStart);
            pnlStatMode.Controls.Add(rdoLastWeekdayStats);
            pnlStatMode.Controls.Add(rdoSelectedDatesStats);
            pnlStatMode.Controls.Add(rdoRangeStats);
            pnlStatMode.Controls.Add(rdoLastFourWeekdayStats);
            pnlStatMode.Controls.Add(rdoYearlyAverageStats);
            pnlStatMode.Controls.Add(rdoDayOfStats);
            pnlStatMode.Controls.Add(rdoYesterdayStats);
            pnlStatMode.Location = new Point(5, 201);
            pnlStatMode.Name = "pnlStatMode";
            pnlStatMode.Size = new Size(200, 737);
            pnlStatMode.TabIndex = 13;
            // 
            // lblTotalSales
            // 
            lblTotalSales.AutoSize = true;
            lblTotalSales.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            lblTotalSales.ForeColor = Color.Black;
            lblTotalSales.Location = new Point(126, 17);
            lblTotalSales.Name = "lblTotalSales";
            lblTotalSales.Size = new Size(34, 25);
            lblTotalSales.TabIndex = 6;
            lblTotalSales.Text = "$0";
            lblTotalSales.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.ForeColor = Color.Black;
            label3.Location = new Point(63, 17);
            label3.Name = "label3";
            label3.Size = new Size(60, 25);
            label3.TabIndex = 6;
            label3.Text = "Sales:";
            // 
            // btnClearDates
            // 
            btnClearDates.BackColor = Color.FromArgb(190, 80, 70);
            btnClearDates.FlatAppearance.BorderSize = 0;
            btnClearDates.FlatStyle = FlatStyle.Flat;
            btnClearDates.Location = new Point(22, 429);
            btnClearDates.Name = "btnClearDates";
            btnClearDates.Size = new Size(161, 23);
            btnClearDates.TabIndex = 5;
            btnClearDates.Text = "Clear Dates";
            btnClearDates.UseVisualStyleBackColor = false;
            btnClearDates.Click += btnClearDates_Click;
            // 
            // btnApplyDates
            // 
            btnApplyDates.BackColor = Color.FromArgb(120, 180, 120);
            btnApplyDates.FlatAppearance.BorderSize = 0;
            btnApplyDates.FlatStyle = FlatStyle.Flat;
            btnApplyDates.Location = new Point(22, 400);
            btnApplyDates.Name = "btnApplyDates";
            btnApplyDates.Size = new Size(161, 23);
            btnApplyDates.TabIndex = 5;
            btnApplyDates.Text = "Apply";
            btnApplyDates.UseVisualStyleBackColor = false;
            btnApplyDates.Click += btnApplyDates_Click;
            // 
            // btnAddCustomDate
            // 
            btnAddCustomDate.BackColor = Color.FromArgb(100, 130, 180);
            btnAddCustomDate.FlatAppearance.BorderSize = 0;
            btnAddCustomDate.FlatStyle = FlatStyle.Flat;
            btnAddCustomDate.Location = new Point(15, 287);
            btnAddCustomDate.Name = "btnAddCustomDate";
            btnAddCustomDate.Size = new Size(170, 23);
            btnAddCustomDate.TabIndex = 5;
            btnAddCustomDate.Text = "Add Date";
            btnAddCustomDate.UseVisualStyleBackColor = false;
            btnAddCustomDate.Click += btnAddCustomDate_Click;
            // 
            // dtpCustomStatDateSelect
            // 
            dtpCustomStatDateSelect.Location = new Point(22, 253);
            dtpCustomStatDateSelect.Name = "dtpCustomStatDateSelect";
            dtpCustomStatDateSelect.Size = new Size(170, 23);
            dtpCustomStatDateSelect.TabIndex = 4;
            // 
            // lbFilteredStatDates
            // 
            lbFilteredStatDates.FormattingEnabled = true;
            lbFilteredStatDates.ItemHeight = 15;
            lbFilteredStatDates.Location = new Point(22, 316);
            lbFilteredStatDates.Name = "lbFilteredStatDates";
            lbFilteredStatDates.Size = new Size(161, 64);
            lbFilteredStatDates.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Enabled = false;
            label2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(23, 526);
            label2.Name = "label2";
            label2.Size = new Size(31, 21);
            label2.TabIndex = 2;
            label2.Text = "To:";
            label2.Visible = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Enabled = false;
            label1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Black;
            label1.Location = new Point(22, 497);
            label1.Name = "label1";
            label1.Size = new Size(52, 21);
            label1.TabIndex = 2;
            label1.Text = "From:";
            label1.Visible = false;
            // 
            // dtpStatRangeEnd
            // 
            dtpStatRangeEnd.CalendarFont = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dtpStatRangeEnd.Enabled = false;
            dtpStatRangeEnd.Location = new Point(23, 653);
            dtpStatRangeEnd.Name = "dtpStatRangeEnd";
            dtpStatRangeEnd.Size = new Size(148, 23);
            dtpStatRangeEnd.TabIndex = 1;
            dtpStatRangeEnd.Visible = false;
            // 
            // dtpStatRangeStart
            // 
            dtpStatRangeStart.CalendarFont = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            dtpStatRangeStart.Enabled = false;
            dtpStatRangeStart.Location = new Point(23, 521);
            dtpStatRangeStart.Name = "dtpStatRangeStart";
            dtpStatRangeStart.Size = new Size(148, 23);
            dtpStatRangeStart.TabIndex = 1;
            dtpStatRangeStart.Visible = false;
            // 
            // rdoLastWeekdayStats
            // 
            rdoLastWeekdayStats.Appearance = Appearance.Button;
            rdoLastWeekdayStats.BackColor = Color.FromArgb(100, 130, 180);
            rdoLastWeekdayStats.FlatAppearance.BorderSize = 0;
            rdoLastWeekdayStats.FlatStyle = FlatStyle.Flat;
            rdoLastWeekdayStats.Location = new Point(15, 127);
            rdoLastWeekdayStats.Name = "rdoLastWeekdayStats";
            rdoLastWeekdayStats.Size = new Size(170, 25);
            rdoLastWeekdayStats.TabIndex = 0;
            rdoLastWeekdayStats.Text = "Last Week";
            rdoLastWeekdayStats.TextAlign = ContentAlignment.MiddleCenter;
            rdoLastWeekdayStats.UseVisualStyleBackColor = false;
            rdoLastWeekdayStats.CheckedChanged += rdoLastWeekdayStats_CheckedChanged;
            // 
            // rdoSelectedDatesStats
            // 
            rdoSelectedDatesStats.Appearance = Appearance.Button;
            rdoSelectedDatesStats.BackColor = Color.FromArgb(100, 130, 180);
            rdoSelectedDatesStats.FlatAppearance.BorderSize = 0;
            rdoSelectedDatesStats.FlatStyle = FlatStyle.Flat;
            rdoSelectedDatesStats.Location = new Point(15, 209);
            rdoSelectedDatesStats.Name = "rdoSelectedDatesStats";
            rdoSelectedDatesStats.Size = new Size(170, 23);
            rdoSelectedDatesStats.TabIndex = 0;
            rdoSelectedDatesStats.Text = "Selected Dates";
            rdoSelectedDatesStats.TextAlign = ContentAlignment.MiddleCenter;
            rdoSelectedDatesStats.UseVisualStyleBackColor = false;
            rdoSelectedDatesStats.CheckedChanged += rdoSelectedDatesStats_CheckedChanged;
            // 
            // rdoRangeStats
            // 
            rdoRangeStats.Appearance = Appearance.Button;
            rdoRangeStats.BackColor = Color.FromArgb(100, 130, 180);
            rdoRangeStats.Enabled = false;
            rdoRangeStats.FlatAppearance.BorderSize = 0;
            rdoRangeStats.FlatStyle = FlatStyle.Flat;
            rdoRangeStats.Location = new Point(22, 597);
            rdoRangeStats.Name = "rdoRangeStats";
            rdoRangeStats.Size = new Size(149, 35);
            rdoRangeStats.TabIndex = 0;
            rdoRangeStats.Text = "Range";
            rdoRangeStats.TextAlign = ContentAlignment.MiddleCenter;
            rdoRangeStats.UseVisualStyleBackColor = false;
            rdoRangeStats.Visible = false;
            rdoRangeStats.CheckedChanged += rdoRangeStats_CheckedChanged;
            // 
            // rdoLastFourWeekdayStats
            // 
            rdoLastFourWeekdayStats.Appearance = Appearance.Button;
            rdoLastFourWeekdayStats.BackColor = Color.FromArgb(100, 130, 180);
            rdoLastFourWeekdayStats.Checked = true;
            rdoLastFourWeekdayStats.FlatAppearance.BorderSize = 0;
            rdoLastFourWeekdayStats.FlatStyle = FlatStyle.Flat;
            rdoLastFourWeekdayStats.Location = new Point(15, 168);
            rdoLastFourWeekdayStats.Name = "rdoLastFourWeekdayStats";
            rdoLastFourWeekdayStats.Size = new Size(170, 25);
            rdoLastFourWeekdayStats.TabIndex = 0;
            rdoLastFourWeekdayStats.TabStop = true;
            rdoLastFourWeekdayStats.Text = "Last Four Weekday";
            rdoLastFourWeekdayStats.TextAlign = ContentAlignment.MiddleCenter;
            rdoLastFourWeekdayStats.UseVisualStyleBackColor = false;
            rdoLastFourWeekdayStats.CheckedChanged += rdoLastFourWeekdayStats_CheckedChanged;
            // 
            // rdoYearlyAverageStats
            // 
            rdoYearlyAverageStats.Appearance = Appearance.Button;
            rdoYearlyAverageStats.BackColor = Color.FromArgb(100, 130, 180);
            rdoYearlyAverageStats.Enabled = false;
            rdoYearlyAverageStats.FlatAppearance.BorderSize = 0;
            rdoYearlyAverageStats.FlatStyle = FlatStyle.Flat;
            rdoYearlyAverageStats.Location = new Point(22, 556);
            rdoYearlyAverageStats.Name = "rdoYearlyAverageStats";
            rdoYearlyAverageStats.Size = new Size(149, 35);
            rdoYearlyAverageStats.TabIndex = 0;
            rdoYearlyAverageStats.Text = "Yearly Average";
            rdoYearlyAverageStats.TextAlign = ContentAlignment.MiddleCenter;
            rdoYearlyAverageStats.UseVisualStyleBackColor = false;
            rdoYearlyAverageStats.Visible = false;
            rdoYearlyAverageStats.CheckedChanged += rdoYearlyAverageStats_CheckedChanged;
            // 
            // rdoDayOfStats
            // 
            rdoDayOfStats.Appearance = Appearance.Button;
            rdoDayOfStats.BackColor = Color.FromArgb(100, 130, 180);
            rdoDayOfStats.FlatAppearance.BorderSize = 0;
            rdoDayOfStats.FlatStyle = FlatStyle.Flat;
            rdoDayOfStats.Location = new Point(15, 45);
            rdoDayOfStats.Name = "rdoDayOfStats";
            rdoDayOfStats.Size = new Size(170, 25);
            rdoDayOfStats.TabIndex = 0;
            rdoDayOfStats.Text = "Day Of";
            rdoDayOfStats.TextAlign = ContentAlignment.MiddleCenter;
            rdoDayOfStats.UseVisualStyleBackColor = false;
            rdoDayOfStats.CheckedChanged += rdoDayOfStats_CheckedChanged;
            // 
            // rdoYesterdayStats
            // 
            rdoYesterdayStats.Appearance = Appearance.Button;
            rdoYesterdayStats.BackColor = Color.FromArgb(100, 130, 180);
            rdoYesterdayStats.FlatAppearance.BorderSize = 0;
            rdoYesterdayStats.FlatStyle = FlatStyle.Flat;
            rdoYesterdayStats.Location = new Point(15, 86);
            rdoYesterdayStats.Name = "rdoYesterdayStats";
            rdoYesterdayStats.Size = new Size(170, 25);
            rdoYesterdayStats.TabIndex = 0;
            rdoYesterdayStats.Text = "Yesterday";
            rdoYesterdayStats.TextAlign = ContentAlignment.MiddleCenter;
            rdoYesterdayStats.UseVisualStyleBackColor = false;
            rdoYesterdayStats.CheckedChanged += rdoYesterdayStats_CheckedChanged;
            // 
            // rdoSales
            // 
            rdoSales.Appearance = Appearance.Button;
            rdoSales.BackColor = Color.FromArgb(100, 130, 180);
            rdoSales.FlatAppearance.BorderColor = Color.FromArgb(100, 130, 180);
            rdoSales.FlatAppearance.CheckedBackColor = Color.WhiteSmoke;
            rdoSales.FlatStyle = FlatStyle.Flat;
            rdoSales.ForeColor = Color.Black;
            rdoSales.Image = FloorPlanMakerUI.Properties.Resources.salesSMall;
            rdoSales.Location = new Point(141, 174);
            rdoSales.Name = "rdoSales";
            rdoSales.Size = new Size(63, 28);
            rdoSales.TabIndex = 10;
            rdoSales.TextAlign = ContentAlignment.MiddleCenter;
            rdoSales.UseVisualStyleBackColor = false;
            rdoSales.CheckedChanged += rdoSales_CheckedChanged;
            // 
            // rdoViewServerFlow
            // 
            rdoViewServerFlow.Appearance = Appearance.Button;
            rdoViewServerFlow.BackColor = Color.FromArgb(100, 130, 180);
            rdoViewServerFlow.FlatAppearance.BorderColor = Color.FromArgb(100, 130, 180);
            rdoViewServerFlow.FlatAppearance.CheckedBackColor = Color.WhiteSmoke;
            rdoViewServerFlow.FlatStyle = FlatStyle.Flat;
            rdoViewServerFlow.ForeColor = Color.Black;
            rdoViewServerFlow.Image = FloorPlanMakerUI.Properties.Resources.trayReeversedLeessSmall;
            rdoViewServerFlow.Location = new Point(63, 0);
            rdoViewServerFlow.Name = "rdoViewServerFlow";
            rdoViewServerFlow.Size = new Size(63, 28);
            rdoViewServerFlow.TabIndex = 10;
            rdoViewServerFlow.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(rdoViewServerFlow, "Servers");
            rdoViewServerFlow.UseVisualStyleBackColor = false;
            rdoViewServerFlow.CheckedChanged += rdoViewServerFlow_CheckedChanged;
            // 
            // rdoViewSectionFlow
            // 
            rdoViewSectionFlow.Appearance = Appearance.Button;
            rdoViewSectionFlow.BackColor = Color.FromArgb(100, 130, 180);
            rdoViewSectionFlow.FlatAppearance.BorderColor = Color.FromArgb(100, 130, 180);
            rdoViewSectionFlow.FlatAppearance.CheckedBackColor = Color.WhiteSmoke;
            rdoViewSectionFlow.FlatStyle = FlatStyle.Flat;
            rdoViewSectionFlow.ForeColor = Color.Black;
            rdoViewSectionFlow.Image = FloorPlanMakerUI.Properties.Resources.lilCanvasBook;
            rdoViewSectionFlow.Location = new Point(0, 0);
            rdoViewSectionFlow.Name = "rdoViewSectionFlow";
            rdoViewSectionFlow.Size = new Size(63, 28);
            rdoViewSectionFlow.TabIndex = 10;
            rdoViewSectionFlow.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(rdoViewSectionFlow, "Sections");
            rdoViewSectionFlow.UseVisualStyleBackColor = false;
            rdoViewSectionFlow.CheckedChanged += rdoViewSectionFlow_CheckedChanged;
            // 
            // btnDeleteSelectedFloorplan
            // 
            btnDeleteSelectedFloorplan.BackColor = Color.IndianRed;
            btnDeleteSelectedFloorplan.Image = FloorPlanMakerUI.Properties.Resources.Trash_28px;
            btnDeleteSelectedFloorplan.Location = new Point(632, 6);
            btnDeleteSelectedFloorplan.Name = "btnDeleteSelectedFloorplan";
            btnDeleteSelectedFloorplan.Size = new Size(25, 32);
            btnDeleteSelectedFloorplan.TabIndex = 22;
            btnDeleteSelectedFloorplan.UseVisualStyleBackColor = false;
            btnDeleteSelectedFloorplan.Visible = false;
            btnDeleteSelectedFloorplan.Click += btnDeleteSelectedFloorplan_Click;
            // 
            // btnHelp
            // 
            btnHelp.BackColor = Color.Orange;
            btnHelp.FlatAppearance.BorderSize = 0;
            btnHelp.FlatStyle = FlatStyle.Flat;
            btnHelp.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnHelp.ForeColor = Color.Black;
            btnHelp.Image = FloorPlanMakerUI.Properties.Resources.HelpOpenSmall;
            btnHelp.Location = new Point(1367, 8);
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(31, 29);
            btnHelp.TabIndex = 13;
            toolTip1.SetToolTip(btnHelp, "Tutorials and Help");
            btnHelp.UseVisualStyleBackColor = false;
            btnHelp.Click += btnHelp_Click;
            // 
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 2500;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 100;
            toolTip1.Popup += toolTip1_Popup;
            // 
            // btnReportBug
            // 
            btnReportBug.BackColor = Color.FromArgb(100, 130, 180);
            btnReportBug.FlatAppearance.BorderSize = 0;
            btnReportBug.FlatStyle = FlatStyle.Flat;
            btnReportBug.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnReportBug.Image = FloorPlanMakerUI.Properties.Resources.report35x;
            btnReportBug.Location = new Point(1411, 8);
            btnReportBug.Name = "btnReportBug";
            btnReportBug.Size = new Size(31, 29);
            btnReportBug.TabIndex = 13;
            toolTip1.SetToolTip(btnReportBug, "Report Bug / Request Feature");
            btnReportBug.UseVisualStyleBackColor = false;
            btnReportBug.Click += btnReportBug_Click;
            // 
            // rdoReservations
            // 
            rdoReservations.Appearance = Appearance.Button;
            rdoReservations.BackColor = Color.FromArgb(100, 130, 180);
            rdoReservations.FlatAppearance.BorderColor = Color.FromArgb(100, 130, 180);
            rdoReservations.FlatAppearance.CheckedBackColor = Color.WhiteSmoke;
            rdoReservations.FlatStyle = FlatStyle.Flat;
            rdoReservations.ForeColor = Color.Black;
            rdoReservations.Image = FloorPlanMakerUI.Properties.Resources.PeopleBook_28px;
            rdoReservations.Location = new Point(73, 174);
            rdoReservations.Name = "rdoReservations";
            rdoReservations.Size = new Size(63, 28);
            rdoReservations.TabIndex = 10;
            rdoReservations.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(rdoReservations, "View Reservations");
            rdoReservations.UseVisualStyleBackColor = false;
            rdoReservations.CheckedChanged += rdoReservations_CheckedChanged;
            // 
            // rdoWeather
            // 
            rdoWeather.Appearance = Appearance.Button;
            rdoWeather.BackColor = Color.FromArgb(100, 130, 180);
            rdoWeather.Checked = true;
            rdoWeather.FlatAppearance.BorderColor = Color.FromArgb(100, 130, 180);
            rdoWeather.FlatAppearance.CheckedBackColor = Color.WhiteSmoke;
            rdoWeather.FlatStyle = FlatStyle.Flat;
            rdoWeather.ForeColor = Color.Black;
            rdoWeather.Image = FloorPlanMakerUI.Properties.Resources.WeatherIcon_28px;
            rdoWeather.Location = new Point(5, 174);
            rdoWeather.Name = "rdoWeather";
            rdoWeather.Size = new Size(63, 28);
            rdoWeather.TabIndex = 10;
            rdoWeather.TabStop = true;
            rdoWeather.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(rdoWeather, "View Weather");
            rdoWeather.UseVisualStyleBackColor = false;
            // 
            // btnUploadSalesData
            // 
            btnUploadSalesData.BackColor = Color.FromArgb(100, 130, 180);
            btnUploadSalesData.FlatAppearance.BorderSize = 0;
            btnUploadSalesData.FlatStyle = FlatStyle.Flat;
            btnUploadSalesData.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnUploadSalesData.Image = FloorPlanMakerUI.Properties.Resources.download_20px;
            btnUploadSalesData.Location = new Point(157, 4);
            btnUploadSalesData.Name = "btnUploadSalesData";
            btnUploadSalesData.Size = new Size(23, 24);
            btnUploadSalesData.TabIndex = 13;
            toolTip1.SetToolTip(btnUploadSalesData, "Choose Toast File to Download");
            btnUploadSalesData.UseVisualStyleBackColor = false;
            btnUploadSalesData.Visible = false;
            btnUploadSalesData.Click += btnUploadSalesData_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(225, 225, 225);
            panel2.Controls.Add(pnlSideDetails);
            panel2.Location = new Point(1270, 43);
            panel2.Name = "panel2";
            panel2.Size = new Size(228, 997);
            panel2.TabIndex = 23;
            // 
            // pnlSideDetails
            // 
            pnlSideDetails.BackColor = Color.FromArgb(180, 190, 200);
            pnlSideDetails.Controls.Add(pnlStatMode);
            pnlSideDetails.Controls.Add(flowResoDisplay);
            pnlSideDetails.Controls.Add(rdoSales);
            pnlSideDetails.Controls.Add(pnlShiftDetails);
            pnlSideDetails.Controls.Add(rdoReservations);
            pnlSideDetails.Controls.Add(rdoWeather);
            pnlSideDetails.Controls.Add(flowWeatherDisplay);
            pnlSideDetails.Location = new Point(11, 10);
            pnlSideDetails.Name = "pnlSideDetails";
            pnlSideDetails.Size = new Size(208, 950);
            pnlSideDetails.TabIndex = 1;
            // 
            // flowResoDisplay
            // 
            flowResoDisplay.BackColor = Color.WhiteSmoke;
            flowResoDisplay.Location = new Point(5, 201);
            flowResoDisplay.Name = "flowResoDisplay";
            flowResoDisplay.Size = new Size(200, 737);
            flowResoDisplay.TabIndex = 11;
            flowResoDisplay.Visible = false;
            // 
            // pnlShiftDetails
            // 
            pnlShiftDetails.Location = new Point(5, 5);
            pnlShiftDetails.Name = "pnlShiftDetails";
            pnlShiftDetails.Size = new Size(200, 163);
            pnlShiftDetails.TabIndex = 1;
            // 
            // flowWeatherDisplay
            // 
            flowWeatherDisplay.BackColor = Color.WhiteSmoke;
            flowWeatherDisplay.Location = new Point(5, 201);
            flowWeatherDisplay.Name = "flowWeatherDisplay";
            flowWeatherDisplay.Size = new Size(200, 737);
            flowWeatherDisplay.TabIndex = 0;
            // 
            // pnlSalesDataUpdated
            // 
            pnlSalesDataUpdated.BackColor = Color.FromArgb(180, 190, 200);
            pnlSalesDataUpdated.Controls.Add(lblMissingSalesData);
            pnlSalesDataUpdated.Controls.Add(btnUploadSalesData);
            pnlSalesDataUpdated.Location = new Point(1174, 6);
            pnlSalesDataUpdated.Name = "pnlSalesDataUpdated";
            pnlSalesDataUpdated.Size = new Size(183, 35);
            pnlSalesDataUpdated.TabIndex = 24;
            // 
            // lblMissingSalesData
            // 
            lblMissingSalesData.BackColor = Color.Gold;
            lblMissingSalesData.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblMissingSalesData.ForeColor = SystemColors.ControlLightLight;
            lblMissingSalesData.Location = new Point(3, 4);
            lblMissingSalesData.Name = "lblMissingSalesData";
            lblMissingSalesData.Size = new Size(148, 24);
            lblMissingSalesData.TabIndex = 6;
            lblMissingSalesData.Text = " Please Update!";
            lblMissingSalesData.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnTest
            // 
            btnTest.Location = new Point(689, 8);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(44, 23);
            btnTest.TabIndex = 25;
            btnTest.Text = "Test";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // btnTestLabels
            // 
            btnTestLabels.Location = new Point(749, 8);
            btnTestLabels.Name = "btnTestLabels";
            btnTestLabels.Size = new Size(75, 23);
            btnTestLabels.TabIndex = 26;
            btnTestLabels.Text = "Test Labels";
            btnTestLabels.UseVisualStyleBackColor = true;
            btnTestLabels.Click += btnTestLabels_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1501, 1042);
            Controls.Add(btnTestLabels);
            Controls.Add(btnTest);
            Controls.Add(pnlSalesDataUpdated);
            Controls.Add(btnDeleteSelectedFloorplan);
            Controls.Add(panel2);
            Controls.Add(btnCloseApp);
            Controls.Add(btnReportBug);
            Controls.Add(btnHelp);
            Controls.Add(panel1);
            Controls.Add(pnlNavigationWindow);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            flowSectionSelect.ResumeLayout(false);
            flowSectionSelect.PerformLayout();
            panel1.ResumeLayout(false);
            pnlFloorplanContainer.ResumeLayout(false);
            pnlNotifications.ResumeLayout(false);
            pnlNavigationWindow.ResumeLayout(false);
            pnlMainContainer.ResumeLayout(false);
            pnlLocationIndicator2.ResumeLayout(false);
            pnlAreaIndicatorContainer.ResumeLayout(false);
            flowLayoutPanel3.ResumeLayout(false);
            flowLayoutPanel4.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            pnlSideContainer.ResumeLayout(false);
            pnlSectionsAndServers.ResumeLayout(false);
            pnlStatMode.ResumeLayout(false);
            pnlStatMode.PerformLayout();
            panel2.ResumeLayout(false);
            pnlSideDetails.ResumeLayout(false);
            pnlSalesDataUpdated.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlFloorPlan;
        private ComboBox cboDiningAreas;
        private Label lblServerAverageCovers;
        private Label lblServerMaxCovers;
        private FlowLayoutPanel flowSectionSelect;
        private RadioButton rdoSections;
        private RadioButton rdoDiningAreas;
        private Panel panel1;
        private FlowLayoutPanel flowServersInFloorplan;
        private Button btnSaveFloorplanTemplate;
        private Button btnPrint;
        private Button btnChooseTemplate;
        private CheckBox cbIsAM;
        private Button btnDayBefore;
        private Button btnNextDay;
        private Button btnCloseApp;
        private Panel pnlFloorplanContainer;
        private Panel pnlNavigationWindow;
        private Panel pnlNavHighlight;
        private RadioButton rdoShifts;
        private Panel pnlSectionsAndServers;
        private CheckBox cbTableDisplayMode;
        private RadioButton rdoViewSectionFlow;
        private RadioButton rdoViewServerFlow;
        private ToolTip toolTip1;
        private Panel pnlMainContainer;
        private Panel pnlSideContainer;
        private Label lblDateSelected;
        private Panel pnlTemplateContainer;
        private Button btnReportBug;
        private RadioButton rdoSettings;
        private RadioButton rdoSales;
        private Panel pnlStatMode;
        private RadioButton rdoYesterdayStats;
        private RadioButton rdoLastWeekdayStats;
        private RadioButton rdoYearlyAverageStats;
        private RadioButton rdoLastFourWeekdayStats;
        private RadioButton rdoSelectedDatesStats;
        private RadioButton rdoRangeStats;
        private Button btnClearDates;
        private Button btnApplyDates;
        private Button btnAddCustomDate;
        private DateTimePicker dtpCustomStatDateSelect;
        private ListBox lbFilteredStatDates;
        private Label label2;
        private Label label1;
        private DateTimePicker dtpStatRangeEnd;
        private DateTimePicker dtpStatRangeStart;
        private Button btnDeleteSelectedFloorplan;
        private Label lblTotalSales;
        private Label label3;
        private RadioButton rdoDayOfStats;
        private CheckBox cbDrawToggle;
        private HelpProvider helpProvider1;
        private Button btnHelp;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button btnAutomatic;
        private Button btnEraseAllSections;
        private Button btnEditRoster;
        private Panel panel2;
        private FlowLayoutPanel flowWeatherDisplay;
        private Panel pnlSideDetails;
        private Panel pnlShiftDetails;
        private RadioButton rdoReservations;
        private RadioButton rdoWeather;
        private Panel pnlSalesDataUpdated;
        private Label lblMissingSalesData;
        private Button btnUploadSalesData;
        private FlowLayoutPanel flowResoDisplay;
        private Button btnSaveColorPDF;
        private Panel pnlNotifications;
        private FloorplanUserControlLibrary.SectionHeaderDisplay sectionHeaderDisplay;
        private FlowLayoutPanel flowLayoutPanel1;
        private FlowLayoutPanel flowSideDiningAreas;
        private FlowLayoutPanel flowLayoutPanel3;
        private FlowLayoutPanel flowLayoutPanel4;
        private Panel pnlAreaIndicatorContainer;
        private Panel pnlAreaIndicator;
        private Panel pnlLocationIndicator2;
        private Panel pnlIndicator2;
        private Label label4;
        private Button btnTest;
        private Button btnTestLabels;
    }
}