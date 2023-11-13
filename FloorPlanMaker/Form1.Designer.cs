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
            flowServersInFloorplan = new FlowLayoutPanel();
            label10 = new Label();
            lblServerAverageCovers = new Label();
            label11 = new Label();
            lblServerMaxCovers = new Label();
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
            pnlSideBar = new Panel();
            pnlSectionsAndServers = new Panel();
            rdoViewServerFlow = new RadioButton();
            rdoViewSectionFlow = new RadioButton();
            panel1.SuspendLayout();
            pnlFloorplanContainer.SuspendLayout();
            pnlNavigationWindow.SuspendLayout();
            pnlSideBar.SuspendLayout();
            pnlSectionsAndServers.SuspendLayout();
            SuspendLayout();
            // 
            // flowServersInFloorplan
            // 
            flowServersInFloorplan.BackColor = Color.WhiteSmoke;
            flowServersInFloorplan.Location = new Point(25, 47);
            flowServersInFloorplan.Name = "flowServersInFloorplan";
            flowServersInFloorplan.Size = new Size(235, 865);
            flowServersInFloorplan.TabIndex = 2;
            flowServersInFloorplan.Paint += flowServersInFloorplan_Paint;
            // 
            // label10
            // 
            label10.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label10.ForeColor = Color.Black;
            label10.Location = new Point(0, 84);
            label10.Name = "label10";
            label10.Size = new Size(131, 21);
            label10.TabIndex = 4;
            label10.Text = "Covers / Server";
            label10.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblServerAverageCovers
            // 
            lblServerAverageCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerAverageCovers.ForeColor = Color.Black;
            lblServerAverageCovers.Location = new Point(0, 147);
            lblServerAverageCovers.Name = "lblServerAverageCovers";
            lblServerAverageCovers.Size = new Size(131, 21);
            lblServerAverageCovers.TabIndex = 5;
            lblServerAverageCovers.Text = "0";
            lblServerAverageCovers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            label11.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label11.ForeColor = Color.Black;
            label11.Location = new Point(0, 126);
            label11.Name = "label11";
            label11.Size = new Size(128, 21);
            label11.TabIndex = 4;
            label11.Text = "Sales / Server";
            label11.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblServerMaxCovers
            // 
            lblServerMaxCovers.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            lblServerMaxCovers.ForeColor = Color.Black;
            lblServerMaxCovers.Location = new Point(0, 105);
            lblServerMaxCovers.Name = "lblServerMaxCovers";
            lblServerMaxCovers.Size = new Size(131, 21);
            lblServerMaxCovers.TabIndex = 5;
            lblServerMaxCovers.Text = "0";
            lblServerMaxCovers.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnSaveFloorplanTemplate
            // 
            btnSaveFloorplanTemplate.FlatAppearance.BorderSize = 0;
            btnSaveFloorplanTemplate.FlatStyle = FlatStyle.Flat;
            btnSaveFloorplanTemplate.ForeColor = Color.Black;
            btnSaveFloorplanTemplate.Location = new Point(3, 430);
            btnSaveFloorplanTemplate.Name = "btnSaveFloorplanTemplate";
            btnSaveFloorplanTemplate.Size = new Size(122, 65);
            btnSaveFloorplanTemplate.TabIndex = 12;
            btnSaveFloorplanTemplate.Text = "Save Floorplan Template";
            btnSaveFloorplanTemplate.UseVisualStyleBackColor = true;
            btnSaveFloorplanTemplate.Visible = false;
            btnSaveFloorplanTemplate.Click += btnSaveFloorplanTemplate_Click;
            // 
            // btnAddSectionLabels
            // 
            btnAddSectionLabels.BackColor = Color.FromArgb(100, 130, 180);
            btnAddSectionLabels.FlatAppearance.BorderSize = 0;
            btnAddSectionLabels.FlatStyle = FlatStyle.Flat;
            btnAddSectionLabels.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnAddSectionLabels.Image = FloorPlanMakerUI.Properties.Resources.lilLabels;
            btnAddSectionLabels.Location = new Point(19, 13);
            btnAddSectionLabels.Name = "btnAddSectionLabels";
            btnAddSectionLabels.Size = new Size(63, 28);
            btnAddSectionLabels.TabIndex = 13;
            btnAddSectionLabels.UseVisualStyleBackColor = false;
            btnAddSectionLabels.Click += btnAddSectionLabels_Click;
            // 
            // flowSectionSelect
            // 
            flowSectionSelect.BackColor = Color.Silver;
            flowSectionSelect.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            flowSectionSelect.Location = new Point(25, 47);
            flowSectionSelect.Name = "flowSectionSelect";
            flowSectionSelect.Size = new Size(235, 865);
            flowSectionSelect.TabIndex = 9;
            // 
            // cbTableDisplayMode
            // 
            cbTableDisplayMode.Appearance = Appearance.Button;
            cbTableDisplayMode.FlatAppearance.BorderSize = 0;
            cbTableDisplayMode.FlatStyle = FlatStyle.Flat;
            cbTableDisplayMode.ForeColor = Color.Black;
            cbTableDisplayMode.Location = new Point(0, 323);
            cbTableDisplayMode.Name = "cbTableDisplayMode";
            cbTableDisplayMode.Size = new Size(123, 51);
            cbTableDisplayMode.TabIndex = 16;
            cbTableDisplayMode.Text = "Table Display Mode";
            cbTableDisplayMode.TextAlign = ContentAlignment.MiddleCenter;
            cbTableDisplayMode.UseVisualStyleBackColor = true;
            cbTableDisplayMode.CheckedChanged += cbTableDisplayMode_CheckedChanged;
            // 
            // btnGenerateSectionLines
            // 
            btnGenerateSectionLines.FlatAppearance.BorderSize = 0;
            btnGenerateSectionLines.FlatStyle = FlatStyle.Flat;
            btnGenerateSectionLines.ForeColor = Color.Black;
            btnGenerateSectionLines.Location = new Point(9, 556);
            btnGenerateSectionLines.Name = "btnGenerateSectionLines";
            btnGenerateSectionLines.Size = new Size(114, 52);
            btnGenerateSectionLines.TabIndex = 15;
            btnGenerateSectionLines.Text = "Auto Section Lines";
            btnGenerateSectionLines.UseVisualStyleBackColor = true;
            btnGenerateSectionLines.Visible = false;
            // 
            // btnChooseTemplate
            // 
            btnChooseTemplate.FlatAppearance.BorderSize = 0;
            btnChooseTemplate.FlatStyle = FlatStyle.Flat;
            btnChooseTemplate.ForeColor = Color.Black;
            btnChooseTemplate.Location = new Point(3, 218);
            btnChooseTemplate.Name = "btnChooseTemplate";
            btnChooseTemplate.Size = new Size(125, 51);
            btnChooseTemplate.TabIndex = 14;
            btnChooseTemplate.Text = "Choose Template";
            btnChooseTemplate.UseVisualStyleBackColor = true;
            btnChooseTemplate.Click += btnChooseTemplate_Click;
            // 
            // btnPrint
            // 
            btnPrint.BackColor = Color.FromArgb(100, 130, 180);
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.FlatStyle = FlatStyle.Flat;
            btnPrint.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            btnPrint.Image = FloorPlanMakerUI.Properties.Resources.lilPrinter;
            btnPrint.Location = new Point(88, 13);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(63, 28);
            btnPrint.TabIndex = 13;
            btnPrint.UseVisualStyleBackColor = false;
            btnPrint.Click += btnPrint_Click;
            // 
            // btnDoAThing
            // 
            btnDoAThing.Location = new Point(41, 863);
            btnDoAThing.Name = "btnDoAThing";
            btnDoAThing.Size = new Size(49, 23);
            btnDoAThing.TabIndex = 17;
            btnDoAThing.Text = "3";
            btnDoAThing.UseVisualStyleBackColor = true;
            btnDoAThing.Visible = false;
            btnDoAThing.Click += btnDoAThing_Click;
            // 
            // btnTest2
            // 
            btnTest2.Location = new Point(41, 828);
            btnTest2.Name = "btnTest2";
            btnTest2.Size = new Size(50, 23);
            btnTest2.TabIndex = 16;
            btnTest2.Text = "2";
            btnTest2.UseVisualStyleBackColor = true;
            btnTest2.Visible = false;
            btnTest2.Click += btnTest2_Click;
            // 
            // btnTest
            // 
            btnTest.Font = new Font("Segoe UI", 8.25F, FontStyle.Regular, GraphicsUnit.Point);
            btnTest.Location = new Point(41, 788);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(50, 25);
            btnTest.TabIndex = 12;
            btnTest.Text = "1";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Visible = false;
            btnTest.Click += btnTest_Click;
            // 
            // pnlFloorPlan
            // 
            pnlFloorPlan.BackColor = Color.WhiteSmoke;
            pnlFloorPlan.Location = new Point(19, 47);
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
            cboDiningAreas.Location = new Point(157, 14);
            cboDiningAreas.Name = "cboDiningAreas";
            cboDiningAreas.Size = new Size(288, 28);
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
            dtpFloorplan.Location = new Point(478, 14);
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
            cbIsAM.Location = new Point(635, 14);
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
            btnDayBefore.Location = new Point(451, 14);
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
            btnNextDay.Location = new Point(608, 15);
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
            // pnlFloorplanContainer
            // 
            pnlFloorplanContainer.BackColor = Color.FromArgb(180, 190, 200);
            pnlFloorplanContainer.Controls.Add(btnAddSectionLabels);
            pnlFloorplanContainer.Controls.Add(btnPrint);
            pnlFloorplanContainer.Controls.Add(dtpFloorplan);
            pnlFloorplanContainer.Controls.Add(cbIsAM);
            pnlFloorplanContainer.Controls.Add(btnNextDay);
            pnlFloorplanContainer.Controls.Add(pnlFloorPlan);
            pnlFloorplanContainer.Controls.Add(cboDiningAreas);
            pnlFloorplanContainer.Controls.Add(btnDayBefore);
            pnlFloorplanContainer.Location = new Point(516, 23);
            pnlFloorplanContainer.Name = "pnlFloorplanContainer";
            pnlFloorplanContainer.Size = new Size(716, 933);
            pnlFloorplanContainer.TabIndex = 19;
            pnlFloorplanContainer.Paint += panel3_Paint;
            // 
            // pnlNavigationWindow
            // 
            pnlNavigationWindow.BackColor = Color.FromArgb(225, 225, 225);
            pnlNavigationWindow.Controls.Add(pnlSideBar);
            pnlNavigationWindow.Controls.Add(pnlFloorplanContainer);
            pnlNavigationWindow.Controls.Add(pnlSectionsAndServers);
            pnlNavigationWindow.Dock = DockStyle.Bottom;
            pnlNavigationWindow.Location = new Point(0, 43);
            pnlNavigationWindow.Name = "pnlNavigationWindow";
            pnlNavigationWindow.Size = new Size(1264, 979);
            pnlNavigationWindow.TabIndex = 20;
            // 
            // pnlSideBar
            // 
            pnlSideBar.BackColor = Color.FromArgb(180, 190, 200);
            pnlSideBar.Controls.Add(btnGenerateSectionLines);
            pnlSideBar.Controls.Add(btnChooseTemplate);
            pnlSideBar.Controls.Add(label10);
            pnlSideBar.Controls.Add(btnDoAThing);
            pnlSideBar.Controls.Add(label11);
            pnlSideBar.Controls.Add(cbTableDisplayMode);
            pnlSideBar.Controls.Add(lblServerAverageCovers);
            pnlSideBar.Controls.Add(btnTest2);
            pnlSideBar.Controls.Add(lblServerMaxCovers);
            pnlSideBar.Controls.Add(btnTest);
            pnlSideBar.Controls.Add(btnSaveFloorplanTemplate);
            pnlSideBar.Location = new Point(31, 23);
            pnlSideBar.Name = "pnlSideBar";
            pnlSideBar.Size = new Size(131, 933);
            pnlSideBar.TabIndex = 20;
            // 
            // pnlSectionsAndServers
            // 
            pnlSectionsAndServers.BackColor = Color.FromArgb(180, 190, 200);
            pnlSectionsAndServers.Controls.Add(rdoViewServerFlow);
            pnlSectionsAndServers.Controls.Add(rdoViewSectionFlow);
            pnlSectionsAndServers.Controls.Add(flowServersInFloorplan);
            pnlSectionsAndServers.Controls.Add(flowSectionSelect);
            pnlSectionsAndServers.ForeColor = Color.White;
            pnlSectionsAndServers.Location = new Point(194, 23);
            pnlSectionsAndServers.Name = "pnlSectionsAndServers";
            pnlSectionsAndServers.Size = new Size(285, 933);
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
            rdoViewServerFlow.Location = new Point(91, 20);
            rdoViewServerFlow.Name = "rdoViewServerFlow";
            rdoViewServerFlow.Size = new Size(63, 28);
            rdoViewServerFlow.TabIndex = 10;
            rdoViewServerFlow.TextAlign = ContentAlignment.MiddleCenter;
            rdoViewServerFlow.UseVisualStyleBackColor = false;
            // 
            // rdoViewSectionFlow
            // 
            rdoViewSectionFlow.Appearance = Appearance.Button;
            rdoViewSectionFlow.BackColor = Color.FromArgb(100, 130, 180);
            rdoViewSectionFlow.Checked = true;
            rdoViewSectionFlow.FlatAppearance.BorderColor = Color.FromArgb(100, 130, 180);
            rdoViewSectionFlow.FlatAppearance.CheckedBackColor = Color.WhiteSmoke;
            rdoViewSectionFlow.FlatStyle = FlatStyle.Flat;
            rdoViewSectionFlow.ForeColor = Color.Black;
            rdoViewSectionFlow.Image = FloorPlanMakerUI.Properties.Resources.lilCanvasBook;
            rdoViewSectionFlow.Location = new Point(28, 20);
            rdoViewSectionFlow.Name = "rdoViewSectionFlow";
            rdoViewSectionFlow.Size = new Size(63, 28);
            rdoViewSectionFlow.TabIndex = 10;
            rdoViewSectionFlow.TabStop = true;
            rdoViewSectionFlow.TextAlign = ContentAlignment.MiddleCenter;
            rdoViewSectionFlow.UseVisualStyleBackColor = false;
            rdoViewSectionFlow.CheckedChanged += rdoViewSectionFlow_CheckedChanged;
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
            panel1.ResumeLayout(false);
            pnlFloorplanContainer.ResumeLayout(false);
            pnlNavigationWindow.ResumeLayout(false);
            pnlSideBar.ResumeLayout(false);
            pnlSectionsAndServers.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel pnlFloorPlan;
        private ComboBox cboDiningAreas;
        private Label lblServerAverageCovers;
        private Label lblServerMaxCovers;
        private Label label11;
        private Label label10;
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
        private Panel pnlSideBar;
        private RadioButton rdoViewSectionFlow;
        private RadioButton rdoViewServerFlow;
    }
}