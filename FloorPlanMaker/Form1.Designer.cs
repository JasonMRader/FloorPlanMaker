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
            flowServersInFloorplan = new FlowLayoutPanel();
            lblServerMaxCovers = new Label();
            lblServerAverageCovers = new Label();
            btnSaveFloorplanTemplate = new Button();
            btnAddSectionLabels = new Button();
            flowSectionSelect = new FlowLayoutPanel();
            cbTableDisplayMode = new CheckBox();
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
            pnlFloorplanContainer = new Panel();
            pnlNavigationWindow = new Panel();
            pnlMainContainer = new Panel();
            pnlSideContainer = new Panel();
            pnlSectionsAndServers = new Panel();
            rdoViewServerFlow = new RadioButton();
            rdoViewSectionFlow = new RadioButton();
            toolTip1 = new ToolTip(components);
            flowSectionSelect.SuspendLayout();
            panel1.SuspendLayout();
            pnlFloorplanContainer.SuspendLayout();
            pnlNavigationWindow.SuspendLayout();
            pnlMainContainer.SuspendLayout();
            pnlSideContainer.SuspendLayout();
            pnlSectionsAndServers.SuspendLayout();
            SuspendLayout();
            // 
            // flowServersInFloorplan
            // 
            flowServersInFloorplan.BackColor = Color.WhiteSmoke;
            flowServersInFloorplan.Location = new Point(0, 28);
            flowServersInFloorplan.Name = "flowServersInFloorplan";
            flowServersInFloorplan.Padding = new Padding(10, 20, 0, 0);
            flowServersInFloorplan.Size = new Size(335, 865);
            flowServersInFloorplan.TabIndex = 2;
            flowServersInFloorplan.Visible = false;
            flowServersInFloorplan.Paint += flowServersInFloorplan_Paint;
            // 
            // lblServerMaxCovers
            // 
            lblServerMaxCovers.Dock = DockStyle.Top;
            lblServerMaxCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerMaxCovers.ForeColor = Color.Black;
            lblServerMaxCovers.Location = new Point(3, 20);
            lblServerMaxCovers.Name = "lblServerMaxCovers";
            lblServerMaxCovers.Size = new Size(232, 35);
            lblServerMaxCovers.TabIndex = 5;
            lblServerMaxCovers.Text = "0";
            lblServerMaxCovers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblServerAverageCovers
            // 
            lblServerAverageCovers.Dock = DockStyle.Top;
            lblServerAverageCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerAverageCovers.ForeColor = Color.Black;
            lblServerAverageCovers.Location = new Point(3, 55);
            lblServerAverageCovers.Margin = new Padding(3, 0, 3, 15);
            lblServerAverageCovers.Name = "lblServerAverageCovers";
            lblServerAverageCovers.Size = new Size(232, 31);
            lblServerAverageCovers.TabIndex = 5;
            lblServerAverageCovers.Text = "0";
            lblServerAverageCovers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSaveFloorplanTemplate
            // 
            btnSaveFloorplanTemplate.FlatAppearance.BorderSize = 0;
            btnSaveFloorplanTemplate.FlatStyle = FlatStyle.Flat;
            btnSaveFloorplanTemplate.ForeColor = Color.Black;
            btnSaveFloorplanTemplate.Image = FloorPlanMakerUI.Properties.Resources.ExtraSmallSave;
            btnSaveFloorplanTemplate.Location = new Point(291, 0);
            btnSaveFloorplanTemplate.Name = "btnSaveFloorplanTemplate";
            btnSaveFloorplanTemplate.Size = new Size(41, 28);
            btnSaveFloorplanTemplate.TabIndex = 12;
            toolTip1.SetToolTip(btnSaveFloorplanTemplate, "Save the Current Sections as a Template");
            btnSaveFloorplanTemplate.UseVisualStyleBackColor = true;
            btnSaveFloorplanTemplate.Click += btnSaveFloorplanTemplate_Click;
            // 
            // btnAddSectionLabels
            // 
            btnAddSectionLabels.BackColor = Color.FromArgb(100, 130, 180);
            btnAddSectionLabels.FlatAppearance.BorderSize = 0;
            btnAddSectionLabels.FlatStyle = FlatStyle.Flat;
            btnAddSectionLabels.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddSectionLabels.Image = FloorPlanMakerUI.Properties.Resources.lilLabels;
            btnAddSectionLabels.Location = new Point(3, 3);
            btnAddSectionLabels.Name = "btnAddSectionLabels";
            btnAddSectionLabels.Size = new Size(45, 33);
            btnAddSectionLabels.TabIndex = 13;
            toolTip1.SetToolTip(btnAddSectionLabels, "Add Section Labels");
            btnAddSectionLabels.UseVisualStyleBackColor = false;
            btnAddSectionLabels.Click += btnAddSectionLabels_Click;
            // 
            // flowSectionSelect
            // 
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
            cbTableDisplayMode.Location = new Point(159, 3);
            cbTableDisplayMode.Name = "cbTableDisplayMode";
            cbTableDisplayMode.Size = new Size(45, 33);
            cbTableDisplayMode.TabIndex = 16;
            cbTableDisplayMode.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(cbTableDisplayMode, "Toggle Table View Mode");
            cbTableDisplayMode.UseVisualStyleBackColor = false;
            cbTableDisplayMode.CheckedChanged += cbTableDisplayMode_CheckedChanged;
            // 
            // btnGenerateSectionLines
            // 
            btnGenerateSectionLines.FlatAppearance.BorderSize = 0;
            btnGenerateSectionLines.FlatStyle = FlatStyle.Flat;
            btnGenerateSectionLines.ForeColor = Color.Black;
            btnGenerateSectionLines.Location = new Point(3, 375);
            btnGenerateSectionLines.Name = "btnGenerateSectionLines";
            btnGenerateSectionLines.Size = new Size(36, 65);
            btnGenerateSectionLines.TabIndex = 15;
            btnGenerateSectionLines.Text = "Auto Section Lines";
            btnGenerateSectionLines.UseVisualStyleBackColor = true;
            btnGenerateSectionLines.Visible = false;
            // 
            // btnChooseTemplate
            // 
            btnChooseTemplate.BackColor = Color.FromArgb(100, 130, 180);
            btnChooseTemplate.FlatAppearance.BorderSize = 0;
            btnChooseTemplate.FlatStyle = FlatStyle.Flat;
            btnChooseTemplate.ForeColor = Color.Black;
            btnChooseTemplate.Image = FloorPlanMakerUI.Properties.Resources.blueSMall;
            btnChooseTemplate.Location = new Point(107, 3);
            btnChooseTemplate.Name = "btnChooseTemplate";
            btnChooseTemplate.Size = new Size(45, 33);
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
            btnPrint.Location = new Point(55, 3);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(45, 33);
            btnPrint.TabIndex = 13;
            toolTip1.SetToolTip(btnPrint, "Print and Save Floorplan");
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnDoAThing
            // 
            btnDoAThing.Location = new Point(3, 551);
            btnDoAThing.Name = "btnDoAThing";
            btnDoAThing.Size = new Size(26, 23);
            btnDoAThing.TabIndex = 17;
            btnDoAThing.Text = "3";
            btnDoAThing.UseVisualStyleBackColor = true;
            btnDoAThing.Visible = false;
            btnDoAThing.Click += btnDoAThing_Click;
            // 
            // btnTest2
            // 
            btnTest2.Location = new Point(3, 522);
            btnTest2.Name = "btnTest2";
            btnTest2.Size = new Size(26, 23);
            btnTest2.TabIndex = 16;
            btnTest2.Text = "2";
            btnTest2.UseVisualStyleBackColor = true;
            btnTest2.Visible = false;
            btnTest2.Click += btnTest2_Click;
            // 
            // btnTest
            // 
            btnTest.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnTest.Location = new Point(3, 491);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(26, 25);
            btnTest.TabIndex = 12;
            btnTest.Text = "1";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnTest_Click;
            // 
            // pnlFloorPlan
            // 
            pnlFloorPlan.BackColor = Color.WhiteSmoke;
            pnlFloorPlan.Location = new Point(5, 36);
            pnlFloorPlan.Name = "pnlFloorPlan";
            pnlFloorPlan.Size = new Size(672, 877);
            pnlFloorPlan.TabIndex = 2;
            // 
            // cboDiningAreas
            // 
            cboDiningAreas.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDiningAreas.FlatStyle = FlatStyle.Flat;
            cboDiningAreas.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            cboDiningAreas.FormattingEnabled = true;
            cboDiningAreas.Location = new Point(211, 3);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(222, 33);
            cboDiningAreas.TabIndex = 7;
            cboDiningAreas.SelectedIndexChanged += cboDiningAreas_SelectedIndexChanged;
            // 
            // rdoSections
            // 
            rdoSections.Appearance = Appearance.Button;
            rdoSections.BackColor = Color.FromArgb(158, 171, 222);
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
            dtpFloorplan.Location = new Point(466, 6);
            dtpFloorplan.Name = "dtpFloorplan";
            dtpFloorplan.Size = new Size(124, 27);
            dtpFloorplan.TabIndex = 14;
            dtpFloorplan.ValueChanged += dtpFloorplan_ValueChanged;
            // 
            // cbIsAM
            // 
            cbIsAM.Appearance = Appearance.Button;
            cbIsAM.BackColor = Color.FromArgb(117, 70, 104);
            cbIsAM.FlatStyle = FlatStyle.Flat;
            cbIsAM.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point);
            cbIsAM.Image = FloorPlanMakerUI.Properties.Resources.smallMoon;
            cbIsAM.Location = new Point(623, 3);
            cbIsAM.Name = "cbIsAM";
            cbIsAM.Size = new Size(56, 33);
            cbIsAM.TabIndex = 15;
            cbIsAM.TextAlign = ContentAlignment.MiddleCenter;
            toolTip1.SetToolTip(cbIsAM, "AM or PM Shift");
            cbIsAM.UseVisualStyleBackColor = false;
            cbIsAM.CheckedChanged += cbIsAM_CheckedChanged;
            // 
            // btnDayBefore
            // 
            btnDayBefore.FlatAppearance.BorderSize = 0;
            btnDayBefore.FlatStyle = FlatStyle.Flat;
            btnDayBefore.Image = FloorPlanMakerUI.Properties.Resources.smallBackArrow;
            btnDayBefore.Location = new Point(439, 3);
            btnDayBefore.Name = "btnDayBefore";
            btnDayBefore.Size = new Size(21, 33);
            btnDayBefore.TabIndex = 16;
            btnDayBefore.UseVisualStyleBackColor = true;
            btnDayBefore.Click += btnDayBefore_Click;
            // 
            // btnNextDay
            // 
            btnNextDay.FlatAppearance.BorderSize = 0;
            btnNextDay.FlatStyle = FlatStyle.Flat;
            btnNextDay.Image = FloorPlanMakerUI.Properties.Resources.smallForwardArrow;
            btnNextDay.Location = new Point(596, 3);
            btnNextDay.Name = "btnNextDay";
            btnNextDay.Size = new Size(21, 33);
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
            // pnlFloorplanContainer
            // 
            pnlFloorplanContainer.BackColor = Color.WhiteSmoke;
            pnlFloorplanContainer.Controls.Add(btnAddSectionLabels);
            pnlFloorplanContainer.Controls.Add(btnChooseTemplate);
            pnlFloorplanContainer.Controls.Add(btnPrint);
            pnlFloorplanContainer.Controls.Add(dtpFloorplan);
            pnlFloorplanContainer.Controls.Add(cbTableDisplayMode);
            pnlFloorplanContainer.Controls.Add(cbIsAM);
            pnlFloorplanContainer.Controls.Add(btnNextDay);
            pnlFloorplanContainer.Controls.Add(pnlFloorPlan);
            pnlFloorplanContainer.Controls.Add(cboDiningAreas);
            pnlFloorplanContainer.Controls.Add(btnDayBefore);
            pnlFloorplanContainer.Location = new Point(13, 17);
            pnlFloorplanContainer.Name = "pnlFloorplanContainer";
            pnlFloorplanContainer.Size = new Size(684, 921);
            pnlFloorplanContainer.TabIndex = 19;
            pnlFloorplanContainer.Paint += panel3_Paint;
            // 
            // pnlNavigationWindow
            // 
            pnlNavigationWindow.BackColor = Color.FromArgb(225, 225, 225);
            pnlNavigationWindow.Controls.Add(btnTest2);
            pnlNavigationWindow.Controls.Add(btnDoAThing);
            pnlNavigationWindow.Controls.Add(btnTest);
            pnlNavigationWindow.Controls.Add(btnGenerateSectionLines);
            pnlNavigationWindow.Controls.Add(pnlMainContainer);
            pnlNavigationWindow.Controls.Add(pnlSideContainer);
            pnlNavigationWindow.Dock = DockStyle.Bottom;
            pnlNavigationWindow.Location = new Point(0, 43);
            pnlNavigationWindow.Name = "pnlNavigationWindow";
            pnlNavigationWindow.Size = new Size(1264, 979);
            pnlNavigationWindow.TabIndex = 20;
            // 
            // pnlMainContainer
            // 
            pnlMainContainer.BackColor = Color.FromArgb(180, 190, 200);
            pnlMainContainer.Controls.Add(pnlFloorplanContainer);
            pnlMainContainer.Location = new Point(494, 29);
            pnlMainContainer.Name = "pnlMainContainer";
            pnlMainContainer.Size = new Size(711, 950);
            pnlMainContainer.TabIndex = 20;
            // 
            // pnlSideContainer
            // 
            pnlSideContainer.BackColor = Color.FromArgb(180, 190, 200);
            pnlSideContainer.Controls.Add(pnlSectionsAndServers);
            pnlSideContainer.Location = new Point(55, 29);
            pnlSideContainer.Name = "pnlSideContainer";
            pnlSideContainer.Size = new Size(364, 950);
            pnlSideContainer.TabIndex = 21;
            // 
            // pnlSectionsAndServers
            // 
            pnlSectionsAndServers.BackColor = Color.WhiteSmoke;
            pnlSectionsAndServers.Controls.Add(rdoViewServerFlow);
            pnlSectionsAndServers.Controls.Add(rdoViewSectionFlow);
            pnlSectionsAndServers.Controls.Add(flowServersInFloorplan);
            pnlSectionsAndServers.Controls.Add(flowSectionSelect);
            pnlSectionsAndServers.Controls.Add(btnSaveFloorplanTemplate);
            pnlSectionsAndServers.ForeColor = Color.White;
            pnlSectionsAndServers.Location = new Point(13, 17);
            pnlSectionsAndServers.Name = "pnlSectionsAndServers";
            pnlSectionsAndServers.Size = new Size(335, 921);
            pnlSectionsAndServers.TabIndex = 1;
            // 
            // rdoViewServerFlow
            // 
            rdoViewServerFlow.Appearance = Appearance.Button;
            rdoViewServerFlow.BackColor = Color.FromArgb(100, 130, 180);
            rdoViewServerFlow.FlatAppearance.BorderColor = Color.FromArgb(100, 130, 180);
            rdoViewServerFlow.FlatAppearance.CheckedBackColor = Color.WhiteSmoke;
            rdoViewServerFlow.FlatStyle = FlatStyle.Flat;
            rdoViewServerFlow.ForeColor = Color.Black;
            rdoViewServerFlow.Image = FloorPlanMakerUI.Properties.Resources.lilPeople;
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
            // toolTip1
            // 
            toolTip1.AutoPopDelay = 2500;
            toolTip1.InitialDelay = 500;
            toolTip1.ReshowDelay = 100;
            toolTip1.Popup += btnSaveTemplate_Popup;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            ClientSize = new Size(1264, 1022);
            Controls.Add(btnCloseApp);
            Controls.Add(panel1);
            Controls.Add(pnlNavigationWindow);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            MouseDown += Form1_MouseDown;
            MouseMove += Form1_MouseMove;
            MouseUp += Form1_MouseUp;
            flowSectionSelect.ResumeLayout(false);
            panel1.ResumeLayout(false);
            pnlFloorplanContainer.ResumeLayout(false);
            pnlNavigationWindow.ResumeLayout(false);
            pnlMainContainer.ResumeLayout(false);
            pnlSideContainer.ResumeLayout(false);
            pnlSectionsAndServers.ResumeLayout(false);
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
        private Button btnAddSectionLabels;
        private Button btnTest;
        private Button btnPrint;
        private DateTimePicker dtpFloorplan;
        private Button btnChooseTemplate;
        private Button btnGenerateSectionLines;
        private CheckBox cbIsAM;
        private Button btnTest2;
        private Button btnDoAThing;
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
    }
}