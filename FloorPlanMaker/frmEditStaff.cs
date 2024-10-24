﻿
using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorPlanMakerUI.Properties;
using FloorplanUserControlLibrary;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FloorPlanMaker
{
    public partial class frmEditStaff : Form
    {

        public EmployeeManager employeeManager;
        private ShiftManager ShiftManager = new ShiftManager();
        private DiningAreaManager DiningAreaManager = new DiningAreaManager();
        private DateTime dateSelected = DateTime.MinValue;
        private DateOnly dateOnlySelected => new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);
        private List<Floorplan> allFloorplans = new List<Floorplan>();
        private int currentFocusedFloorplanIndex = 0;
        private int DaysAgoStats = -1;
        frmLoading loadingForm = new frmLoading("Loading");
        public ShiftDetailOverviewManager shiftDetailOverviewManager { get; set; }
        private Form1 form1Reference;
        private bool isNewShift = false;
        private void SetColorTheme()
        {
            UITheme.FormatCTAButton(btnCreateANewShift);
            UITheme.FormatCTAButton(btnAssignTables);
            UITheme.FormatCTAButton(pbBack);
            UITheme.FormatCTAButton(pbForward);
            UITheme.FormatSecondColor(flowDiningAreaAssignment);

            UITheme.FormatCanvasColor(flowUnassignedServers);

            UITheme.FormatAccentColor(this);

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Tab) {
                MoveToNextFloorplan();
                return true;
            }
            if (keyData == Keys.Left) {
                MovedDateBack();
                return true;
            }
            if (keyData == Keys.Right) {
                MoveDateForward();
                return true;
            }
            if (keyData == Keys.Enter) {
                AutomateNextStep();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MoveToNextFloorplan()
        {
            if (ShiftManager.SelectedShift == null) { return; }
            currentFocusedFloorplanIndex++;
            if (currentFocusedFloorplanIndex == DiningAreaRBs.Count) { currentFocusedFloorplanIndex = 0; }
            if (currentFocusedFloorplanIndex < DiningAreaRBs.Count) {
                var rbToFocus = DiningAreaRBs[currentFocusedFloorplanIndex];
                rbToFocus.Focus();
            }

        }

        public frmEditStaff(EmployeeManager staffManager, Shift shiftManager, Form1 form1)
        {
            //loadingForm.Show();
            InitializeComponent();
            this.employeeManager = staffManager;
            this.ShiftManager.SetSelectedShift(shiftManager.DateOnly, shiftManager.IsAM);
            this.shiftDetailOverviewManager = form1.shiftDetailManager;
            this.form1Reference = form1;
            allFloorplans = SqliteDataAccess.LoadFloorplanList();
            //LoadFloorplansAsync();
        }
        private async void LoadFloorplansAsync()
        {

            this.Enabled = false;
            try {
                allFloorplans = await Task.Run(() => SqliteDataAccess.LoadFloorplanList());
            }
            finally {
                //loadingForm.Close();
                this.Enabled = true;
            }
        }

        private void frmEditStaff_Load(object sender, EventArgs e)
        {

        }

        public void UpdateUI()
        {
            dateSelected = DateTime.Now;
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            SetColorTheme();
            SetFloorplansForShiftManager();
            cboSalesMethod.Items.Clear();
            cboSalesMethod.Items.Add("Yesterday");
            cboSalesMethod.Items.Add("Last Weekday");
            cboSalesMethod.Items.Add("Last 4 Weekday");
            cboSalesMethod.Items.Add("Day Of");
            cboSalesMethod.SelectedIndex = 2;
        }

        private void SetFloorplansForShiftManager()
        {
            setIsNewShiftBool();
            flowDiningAreaAssignment.Controls.Clear();
            DateOnly date = DateOnly.FromDateTime(dateSelected);
            ShiftManager.SetSelectedShift(date, cbIsAM.Checked);
            RefreshFloorplanFlowPanel(ShiftManager.SelectedShift.Floorplans);
            RefreshFloorplanCountLabels();
        }

        private void RefreshFloorplanFlowPanel(IReadOnlyList<Floorplan> floorplans)
        {
            flowDiningAreaAssignment.Controls.Clear();
            infoPanelList.Clear();
            DiningAreaRBs.Clear();

            int selectedCount = floorplans.Count;
            if (selectedCount == 0)
                return;

            int width = flowDiningAreaAssignment.Width / selectedCount;
            int height = 30;
            int floorplanCount = 1;
            bool isChecked = true;

            List<Control> flowList = new List<Control>();

            foreach (Floorplan fp in floorplans) {
                //FloorplanInfoControl infoPanel = new FloorplanInfoControl(fp, width)
                //{

                //    Padding = new Padding(4, 0, 0, 0),
                //    BackColor = UITheme.CanvasColor,
                //    Tag = fp
                //};
                FloorplanInfoDisplay infoPanel = new FloorplanInfoDisplay(fp, width) {

                    Padding = new Padding(4, 0, 0, 0),
                    BackColor = UITheme.CanvasColor,
                    Tag = fp
                };
                infoPanelList.Add(infoPanel);

                RadioButton rb = new RadioButton {
                    Width = width - 8,
                    Height = height +5,
                    Appearance = Appearance.Button,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(4, 0, 4, 0),
                    Font = UITheme.MainFont,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = UITheme.CTAColor,
                    ForeColor = Color.Black,
                    Text = fp.DiningArea.Name,
                    Tag = fp,
                    Image = UITheme.GetSmallDiningAreaImage(fp.DiningArea),
                    TextImageRelation = TextImageRelation.ImageBeforeText
                    //TabIndex = tabIndex++

                };
                rb.GotFocus += (sender, e) => { ((RadioButton)sender).Checked = true; };
                rb.CheckedChanged += FloorplanRadioButton_CheckedChanged;
                if (floorplanCount == 1) {
                    rb.Checked = true;
                }
                floorplanCount++;

                FlowLayoutPanel serversInFloorplanPanel = new FlowLayoutPanel {
                    Width = width - 8,
                    Height = flowDiningAreaAssignment.Height - height,
                    Margin = new Padding(4),
                    BackColor = UITheme.CanvasColor,
                    Tag = fp
                };
                DiningAreaRBs.Add(rb);
                flowList.Add(serversInFloorplanPanel);
                foreach (var server in fp.Servers) {
                    server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
                    ServerHistoryControl newServerButton = new ServerHistoryControl(server, dateOnlySelected.AddDays(-30),
                    dateOnlySelected, ShiftManager.IsAM, true, serversInFloorplanPanel.Width - 8);
                    newServerButton.Click += MoveFromFloorplanServerButton_Click;
                    newServerButton.TabStop = false;

                    serversInFloorplanPanel.Controls.Add(newServerButton);
                }

            }
            foreach (Control c in infoPanelList) {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }

            foreach (Control c in DiningAreaRBs) {
                flowDiningAreaAssignment.Controls.Add(((Control)c));
            }
            foreach (Control c in flowList) {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }
            UpdateCountLabels();

        }

        private List<Control> DiningAreaRBs = new List<Control>();
        //List<FloorplanInfoControl> infoPanelList = new List<FloorplanInfoControl>();
        List<FloorplanInfoDisplay> infoPanelList = new List<FloorplanInfoDisplay>();
        private void MoveFromFloorplanServerButton_Click(object sender, EventArgs e)
        {
            ServerHistoryControl serverButton = sender as ServerHistoryControl;
            if (serverButton == null) return;

            Server server = (Server)serverButton.Server;

            foreach (Control c in flowDiningAreaAssignment.Controls) {
                if (c is FlowLayoutPanel flowLayoutPanel) {
                    foreach (Control c2 in flowLayoutPanel.Controls) {
                        if (c2 == serverButton) {
                            flowLayoutPanel.Controls.Remove(c2);
                            if (flowLayoutPanel.Tag is Floorplan fp) {

                                ShiftManager.SelectedShift.RemoveServerFromFloorplanByDiningArea(server, fp);
                                break;
                            }
                        }
                    }
                }
            }

            ShiftManager.SelectedShift.SelectedFloorplan.AddServerAndSection(server);

            FlowLayoutPanel SelectedTargetPanel = null;
            foreach (Control control in flowDiningAreaAssignment.Controls) {
                if (control is FlowLayoutPanel panel && panel.Tag == ShiftManager.SelectedShift.SelectedFloorplan) {
                    SelectedTargetPanel = panel;

                    break;
                }
            }
            if (SelectedTargetPanel != null) {
                ServerHistoryControl newServerButton = new ServerHistoryControl(server, dateOnlySelected.AddDays(-30),
                   dateOnlySelected, ShiftManager.IsAM, true, SelectedTargetPanel.Width - 8);
                newServerButton.Click += MoveFromFloorplanServerButton_Click;
                newServerButton.TabStop = false;

                SelectedTargetPanel.Controls.Add(newServerButton);
            }
            RefreshFloorplanCountLabels();
        }
        private void ServerControl_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            ServerHistoryControl serverControl = sender as ServerHistoryControl;
            if (sender is ServerHistoryControl) {
                if (serverControl == null) return;
                server = serverControl.Server;
            }


            if (ShiftManager.SelectedShift.UnassignedServers.Contains(server)) {
                ShiftManager.SelectedShift.AddServerToAFloorplan(server);
                flowUnassignedServers.Controls.Remove(serverControl);
            }
            else {
                foreach (Control c in flowDiningAreaAssignment.Controls) {
                    if (c is FlowLayoutPanel flowLayoutPanel) {
                        foreach (Control c2 in flowLayoutPanel.Controls) {
                            if (c2.Tag == server) {
                                flowLayoutPanel.Controls.Remove(c2);
                                if (flowLayoutPanel.Tag is Floorplan fp) {
                                    fp.RemoveServerAndSection(server);
                                }
                            }
                        }
                    }
                }
            }

            ShiftManager.SelectedShift.SelectedFloorplan.AddServerAndSection(server);
            NewAddServerButtonToFloorplan(ShiftManager.SelectedShift.SelectedFloorplan, server, serverControl);
            RefreshFloorplanCountLabels();
        }
        private void NewAddServerButtonToFloorplan(Floorplan floorplan, Server server, ServerHistoryControl serverHistory)
        {
            FlowLayoutPanel SelectedTargetPanel = null;
            serverHistory.Click -= ServerControl_Click;
            foreach (Control control in flowDiningAreaAssignment.Controls) {
                if (control is FlowLayoutPanel panel && panel.Tag == floorplan) {
                    SelectedTargetPanel = panel;
                    break;
                }
            }
            if (SelectedTargetPanel != null) {

                serverHistory.SetWidth(SelectedTargetPanel.Width - 8);
                serverHistory.Click += MoveFromFloorplanServerButton_Click;
                serverHistory.SetIsCollapsible(true);
                SelectedTargetPanel.Controls.Add(serverHistory);

            }
            RefreshFloorplanCountLabels();
        }

        private void btnAssignTables_Click(object sender, EventArgs e)
        {
            bool emptyFloorplan = false;
            foreach (Floorplan fp in ShiftManager.SelectedShift.Floorplans) {
                fp.Date = dateSelected;
                if (fp.Servers.Count == 0) {
                    emptyFloorplan = true;
                }
            }
            if (emptyFloorplan) {
                MessageBox.Show("All floorplans must have a servers assigned.");
                return;
            }
            ShiftManager.SelectedShift.DateOnly = new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);

            form1Reference.SelectDiningAreaWithFirstFloorplan(this.ShiftManager.SelectedShift.SelectedFloorplan);
            ShiftManager.SelectedShift.PairBothBarSections();
            form1Reference.UpdateForm1ShiftManager(this.ShiftManager.SelectedShift);
        }

        private void RefreshFloorplanCountLabels()
        {
            List<TableStat> stats = new List<TableStat>();
            if (cboSalesMethod.SelectedIndex == 2) {
                var previousWeekdays = new List<DateOnly>();
                for (int i = 1; i <= 4; i++) {
                    previousWeekdays.Add(ShiftManager.SelectedShift.DateOnly.AddDays(-7 * i));
                }

                stats = SqliteDataAccess.LoadTableStatsByDateListAndLunch(ShiftManager.SelectedShift.IsAM, previousWeekdays);
            }
            else {
                stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(
               ShiftManager.IsAM, ShiftManager.DateOnly.AddDays(DaysAgoStats));
            }
            if (ShiftManager.SelectedShift.DiningAreasUsed.Count != 0) {
                foreach (DiningArea area in ShiftManager.SelectedShift.DiningAreasUsed) {
                    area.SetTableSales(stats);
                }
            }
            foreach (FloorplanInfoDisplay info in infoPanelList) {
                info.UpdateCurrentLabelsForLastFour();
            }


        }
        private void FloorplanRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked) {
                ShiftManager.SelectedShift.SelectedFloorplan = (Floorplan)rb.Tag;
            }
        }

        private void cbIsPM_CheckedChanged(object sender, EventArgs e)
        {
            SetFloorplansForShiftManager();
            if (cbIsAM.Checked) {
                cbIsAM.Image = Resources.smallSunrise;
                cbIsAM.BackColor = Color.FromArgb(251, 175, 0);

                ShiftManager.SelectedShift.SetFloorplansToAM();

            }
            else {
                cbIsAM.Image = Resources.smallMoon;
                cbIsAM.BackColor = Color.FromArgb(117, 70, 104);

                ShiftManager.SelectedShift.SetFloorplansToPM();

            }


        }

        private void btnDateUp_Click(object sender, EventArgs e)
        {
            MoveDateForward();
        }
        public void MoveDateForward()
        {
            dateSelected = dateSelected.AddDays(1);
            SetToDateSelected();
        }
        private void btnDateDown_Click(object sender, EventArgs e)
        {
            MovedDateBack();
        }
        public void MovedDateBack()
        {
            dateSelected = dateSelected.AddDays(-1);
            SetToDateSelected();

        }
        private void SetToDateSelected()
        {
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            SpecialEventDate specialEventDate = SqliteDataAccess.GetEventByDate(dateOnlySelected);
            if (specialEventDate != null) {
                lblShiftDate.Text = specialEventDate.Name + " (" + dateSelected.ToString("ddd, M/dd") + ")";
            }

            SetFloorplansForShiftManager();
        }
        private void setIsNewShiftBool()
        {
            DateOnly date = DateOnly.FromDateTime(dateSelected);
            foreach (DiningArea diningArea in DiningAreaManager.DiningAreas) {
                Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(diningArea, date, cbIsAM.Checked);
                if (fp != null) {
                    isNewShift = false;
                    return;

                }
            }
            isNewShift = true;

        }
        public void OpenNewShiftForm()
        {
            form1Reference.tutorialType = TutorialImages.TutorialType.CreateShift;

            frmNewShiftDatePicker form = new frmNewShiftDatePicker(DiningAreaManager, allFloorplans, this, dateSelected, cbIsAM.Checked, ShiftManager);
            form.TopLevel = false;
            this.Controls.Add(form);
            form.Show();
            form.BringToFront();
        }
        private void btnCreateANewShift_Click(object sender, EventArgs e)
        {
            form1Reference.tutorialType = TutorialImages.TutorialType.CreateShift;
            frmNewShiftDatePicker form = new frmNewShiftDatePicker(DiningAreaManager, allFloorplans, this, dateSelected, cbIsAM.Checked, ShiftManager);
            form.TopLevel = false;
            this.Controls.Add(form);
            form.Show();
            form.BringToFront();


        }
        //TODO have this method load current floorplans
        public void UpdateNewShift(ShiftManager shiftManager)
        {
            cboSalesMethod.SelectedIndex = 2;

            this.ShiftManager = shiftManager;
            dateSelected = ShiftManager.DateOnly.ToDateTime(new TimeOnly(0, 0));
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            cbIsAM.Checked = ShiftManager.IsAM;

            RefreshFloorplanFlowPanel(ShiftManager.SelectedShift.Floorplans);
            PopulateUnassignedServers();
            RefreshFloorplanCountLabels();
            form1Reference.tutorialType = TutorialImages.TutorialType.EditDistribution;

        }
        private Button AutoAssignButton()
        {
            Button btnAutoAssign = new Button() {
                Dock = DockStyle.Top,
                Text = "Auto-Assign",
                Size = new Size(305, 25)
            };
            btnAutoAssign.Click += btnAutoAssign_Click;

            return btnAutoAssign;
        }

        private void btnAutoAssign_Click(object? sender, EventArgs e)
        {

            FloorplanGenerator floorplanGenerator = new FloorplanGenerator(ShiftManager.SelectedShift);
            floorplanGenerator.GetServerDistribution();
            floorplanGenerator.AutoAssignDiningAreas();
            PopulateUnassignedServers();
            RefreshFloorplanFlowPanel(ShiftManager.SelectedShift.Floorplans);
            RefreshFloorplanCountLabels();
        }

        private void PopulateUnassignedServers()
        {
            flowUnassignedServers.Controls.Clear();
            Button btnAutoAssign = this.AutoAssignButton();

            flowUnassignedServers.Controls.Add((Control)btnAutoAssign);

            foreach (var server in ShiftManager.SelectedShift.UnassignedServers) {
                server.Shifts = SqliteDataAccess.GetShiftsForServer(server);

                ShiftManager.SelectedShift.ServersNotOnShift.Remove(server);

                ServerHistoryControl newServerControl = new ServerHistoryControl(server, dateOnlySelected.AddDays(-30),
                    dateOnlySelected, ShiftManager.IsAM, false, 300);
                newServerControl.Margin = new Padding(5);
                newServerControl.Click += ServerControl_Click;

                flowUnassignedServers.Controls.Add(newServerControl);

            }
        }
        private void PopulateUnassignedServersOLD()
        {
            flowUnassignedServers.Controls.Clear();
            Button btnAutoAssign = this.AutoAssignButton();
            flowUnassignedServers.Controls.Add((Control)btnAutoAssign);

            foreach (var server in ShiftManager.SelectedShift.UnassignedServers) {
                ShiftManager.SelectedShift.ServersNotOnShift.Remove(server);
                ServerControl newServerControl = new ServerControl(server, 20);
                newServerControl.Margin = new Padding(5);
                newServerControl.Click += ServerControl_Click;
                //ImageSetter.SetShiftControlImages(newServerControl);
                flowUnassignedServers.Controls.Add(newServerControl);
            }
        }

        private void frmEditStaff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left) {
                MovedDateBack();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right) {
                MoveDateForward();
                e.Handled = true;
            }
        }

        private void flowUnassignedServers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboSalesMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSalesMethod.SelectedItem == "Yesterday") {
                DaysAgoStats = -1;
            }
            if (cboSalesMethod.SelectedItem == "Last Weekday") {
                DaysAgoStats = -7;
            }
            if (cboSalesMethod.SelectedItem == "Last 4 Weekday") {
                DaysAgoStats = -13;
            }
            if (cboSalesMethod.SelectedItem == "Day Of") {
                DaysAgoStats = 0;
            }
            RefreshFloorplanCountLabels();

        }

        internal void NotifyNewShiftClosed()
        {
            form1Reference.tutorialType = TutorialImages.TutorialType.EditDistribution;
        }
        private Dictionary<DiningArea, int> PreviousServerCountsForNewShift(int Days)
        {
            DateTime targetDate = dateSelected.AddDays(Days);
            DateOnly dateOnly = new DateOnly(targetDate.Year, targetDate.Month, targetDate.Day);
            List<Floorplan> floorplansResults = new List<Floorplan>();



            foreach (Floorplan floorplan in ShiftManager.SelectedShift.Floorplans) {

                Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(floorplan.DiningArea, dateOnly, ShiftManager.IsAM);
                if (fp != null) {
                    floorplansResults.Add(fp);
                }
            }

            var result = new Dictionary<DiningArea, int>();


            foreach (var floorplan in ShiftManager.SelectedShift.Floorplans) {
                result[floorplan.DiningArea] = 0;
            }


            foreach (var fp in floorplansResults) {
                result[fp.DiningArea] += fp.Servers.Count;
            }

            return result;
        }


        private void UpdateCountLabels()
        {
            Dictionary<DiningArea, int> LastWeekFloorplans = new Dictionary<DiningArea, int>();
            Dictionary<DiningArea, int> yesterdayCounts = new Dictionary<DiningArea, int>();
            LastWeekFloorplans = PreviousServerCountsForNewShift(-7);
            yesterdayCounts = PreviousServerCountsForNewShift(-1);
            foreach (FloorplanInfoDisplay infoPanel in infoPanelList) {
                if (infoPanel.Floorplan.DiningArea is DiningArea area) {
                    int yesterday = 0;
                    int lastWeek = 0;

                    if (yesterdayCounts.TryGetValue(area, out int yesterdayCount)) {
                        yesterday = yesterdayCount;
                    }

                    if (LastWeekFloorplans.TryGetValue(area, out int lastWeekCount)) {
                        lastWeek = lastWeekCount;
                    }


                    infoPanel.UpdatePastCountLabels(yesterday, lastWeek);
                }
            }
        }

        private void btnAutomatic_Click(object sender, EventArgs e)
        {
            AutomateNextStep();
        }
        private async void AutomateNextStep()
        {
            if (ShiftManager.SelectedShift.Floorplans == null) {
                btnCreateANewShift.PerformClick();
            }
            if (ShiftManager.SelectedShift.Floorplans.Count == 0) {
                btnCreateANewShift.PerformClick();
            }
            else if (ShiftManager.SelectedShift.UnassignedServers.Count > 0) {
                frmLoading loadingForm = null;
                Thread loadingThread = new Thread(() => {
                    loadingForm = new frmLoading(frmLoading.GifType.staffAllocation);
                    Application.Run(loadingForm);  // Start the loading form on a separate UI thread
                });

                loadingThread.SetApartmentState(ApartmentState.STA);  // Set thread to STA mode
                loadingThread.Start();  // Start the new UI thread

                this.Enabled = false;

                await Task.Delay(100);

                await Task.Run(() => {
                    FloorplanGenerator floorplanGenerator = new FloorplanGenerator(ShiftManager.SelectedShift);
                    floorplanGenerator.GetServerDistribution();
                    floorplanGenerator.AutoAssignDiningAreas();

                });

                // Close the loading form and re-enable the main form once the work is done
                this.Invoke(new Action(() => {
                    PopulateUnassignedServers();
                    RefreshFloorplanFlowPanel(ShiftManager.SelectedShift.Floorplans);
                    RefreshFloorplanCountLabels();

                    if (loadingForm != null && loadingForm.InvokeRequired) {
                        loadingForm.Invoke(new Action(() => loadingForm.Close()));  // Close the form on its own thread
                    }
                    else {
                        loadingForm?.Close();
                    }


                    //this.Close();
                    this.Enabled = true;
                    this.BringToFront();
                }));
                //FloorplanGenerator floorplanGenerator = new FloorplanGenerator(ShiftManager.SelectedShift);
                //floorplanGenerator.GetServerDistribution();
                //floorplanGenerator.AutoAssignDiningAreas();
                //PopulateUnassignedServers();
                //RefreshFloorplanFlowPanel(ShiftManager.SelectedShift.Floorplans);
                //RefreshFloorplanCountLabels();
                //frmLoading loadingForm = new frmLoading(frmLoading.GifType.staffAllocation);
                //loadingForm.Show();
                //this.Enabled = false;
                //await Task.Delay(100);
                //await Task.Run(() => {
                //    FloorplanGenerator floorplanGenerator = new FloorplanGenerator(ShiftManager.SelectedShift);
                //    floorplanGenerator.GetServerDistribution();
                //    floorplanGenerator.AutoAssignDiningAreas();
                //    this.Invoke(new Action(() => {
                //        PopulateUnassignedServers();
                //        RefreshFloorplanFlowPanel(ShiftManager.SelectedShift.Floorplans);
                //        RefreshFloorplanCountLabels();

                //        loadingForm.Close();
                //        this.Enabled = true;
                //        this.BringToFront();

                //    }));
                //});


            }
            else if (ShiftManager.SelectedShift.UnassignedServers.Count == 0) {
                btnAssignTables.PerformClick();
            }
        }

        private void lblShiftDate_Click(object sender, EventArgs e)
        {
            using (frmDateSelect selectDateForm = new frmDateSelect(dateSelected)) {
                selectDateForm.StartPosition = FormStartPosition.Manual;
                selectDateForm.Location = Cursor.Position;
                DialogResult = selectDateForm.ShowDialog();
                if (DialogResult == DialogResult.OK) {
                    this.dateSelected = selectDateForm.dateSelected;
                    SetToDateSelected();
                }
            }
        }
    }
}
