
using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorPlanMakerUI.Properties;
using System;
using System.Data;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FloorPlanMaker
{
    public partial class frmEditStaff : Form
    {
        //public List<Server> AllServers = new List<Server>();
        public EmployeeManager employeeManager;
        //private Shift newShiftManager = new Shift();
        //private Shift pastShiftsManager;
        private ShiftManager ShiftManager = new ShiftManager();
        private DiningAreaManager DiningAreaManager = new DiningAreaManager();
        private DateTime dateSelected = DateTime.MinValue;
        private List<Floorplan> allFloorplans = new List<Floorplan>();
        private int currentFocusedFloorplanIndex = 0;
        private int DaysAgoStats = -1;

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

            if (keyData == Keys.Tab)
            {
                MoveToNextFloorplan();
                return true;
            }

            if (keyData == Keys.Left)
            {
                MovedDateBack();
                return true;
            }


            if (keyData == Keys.Right)
            {
                MoveDateForward();
                return true;
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MoveToNextFloorplan()
        {
            //if (newShiftManager == null) { return; }
            if (ShiftManager.SelectedShift == null) { return; }
            currentFocusedFloorplanIndex++;
            if (currentFocusedFloorplanIndex == DiningAreaRBs.Count) { currentFocusedFloorplanIndex = 0; }

            var rbToFocus = DiningAreaRBs[currentFocusedFloorplanIndex];
            rbToFocus.Focus();

        }



        public frmEditStaff(EmployeeManager staffManager, Shift shiftManager, Form1 form1)
        {
            InitializeComponent();
            this.employeeManager = staffManager;
            //this.pastShiftsManager = shiftManager;
            this.ShiftManager.SetSelectedShift(shiftManager.DateOnly, shiftManager.IsAM);
            this.form1Reference = form1;
            allFloorplans = SqliteDataAccess.LoadFloorplanList();
        }

        private Dictionary<DiningArea, int> PreviousServerCountsForNewShift(int Days)
        {
            DateTime targetDate = dateSelected.AddDays(Days);
            DateOnly dateOnly = new DateOnly(targetDate.Year, targetDate.Month, targetDate.Day);
            List<Floorplan> floorplansResults = new List<Floorplan>();


            //foreach (Floorplan floorplan in newShiftManager.Floorplans)
            foreach (Floorplan floorplan in ShiftManager.SelectedShift.Floorplans)
            {
                //Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(floorplan.DiningArea, dateOnly, newShiftManager.IsAM);
                Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(floorplan.DiningArea, dateOnly, ShiftManager.IsAM);
                if (fp != null)
                {
                    floorplansResults.Add(fp);
                }
            }

            var result = new Dictionary<DiningArea, int>();

            //foreach (var floorplan in newShiftManager.Floorplans)
            foreach (var floorplan in ShiftManager.SelectedShift.Floorplans)
            {
                result[floorplan.DiningArea] = 0;
            }


            foreach (var fp in floorplansResults)
            {
                result[fp.DiningArea] += fp.Servers.Count;
            }

            return result;
        }
        //private Dictionary<DiningArea, int> PreviousServerCountsForOldShift(int Days)
        //{
        //    DateTime targetDate = dateSelected.AddDays(Days);
        //    DateOnly dateOnly = new DateOnly(targetDate.Year, targetDate.Month, targetDate.Day);
        //    List<Floorplan> floorplansResults = new List<Floorplan>();


        //    foreach (Floorplan floorplan in pastShiftsManager.Floorplans)
        //    {
        //        Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(floorplan.DiningArea, dateOnly, cbIsAM.Checked);
        //        if (fp != null)
        //        {
        //            floorplansResults.Add(fp);
        //        }
        //    }

        //    var result = new Dictionary<DiningArea, int>();

        //    foreach (var floorplan in floorplansResults)
        //    {
        //        result[floorplan.DiningArea] = 0;
        //    }


        //    foreach (var fp in floorplansResults)
        //    {
        //        result[fp.DiningArea] += fp.Servers.Count;
        //    }

        //    return result;
        //}

        private void UpdateCountLabels()
        {
            Dictionary<DiningArea, int> LastWeekFloorplans = new Dictionary<DiningArea, int>();
            Dictionary<DiningArea, int> yesterdayCounts = new Dictionary<DiningArea, int>();
            LastWeekFloorplans = PreviousServerCountsForNewShift(-7);
            yesterdayCounts = PreviousServerCountsForNewShift(-1);
            //if (isNewShift)
            //{
            //    LastWeekFloorplans = PreviousServerCountsForNewShift(-7);
            //    yesterdayCounts = PreviousServerCountsForNewShift(-1);
            //}
            //else
            //{
            //    LastWeekFloorplans = PreviousServerCountsForOldShift(-7);
            //    yesterdayCounts = PreviousServerCountsForOldShift(-1);
            //}
            foreach (FloorplanInfoControl infoPanel in infoPanelList)
            {
                if (infoPanel.Floorplan.DiningArea is DiningArea area)
                {
                    int yesterday = 0;
                    int lastWeek = 0;

                    if (yesterdayCounts.TryGetValue(area, out int yesterdayCount))
                    {
                        yesterday = yesterdayCount;
                    }

                    if (LastWeekFloorplans.TryGetValue(area, out int lastWeekCount))
                    {
                        lastWeek = lastWeekCount;
                    }

                    infoPanel.UpdatePastLabels(yesterday, lastWeek);
                    infoPanel.UpdateCurrentLabels(DaysAgoStats);
                }
            }
        }


        private void frmEditStaff_Load(object sender, EventArgs e)
        {
            dateSelected = DateTime.Now;
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            SetColorTheme();
            SetFloorplansForShiftManager();
            cboSalesMethod.Items.Clear();
            cboSalesMethod.Items.Add("Yesterday");
            cboSalesMethod.Items.Add("Last Weekday");
            //cboSalesMethod.Items.Add("Last 4 Weekday");

        }


        private void SetFloorplansForShiftManager()
        {
            setIsNewShiftBool();
            flowDiningAreaAssignment.Controls.Clear();
            DateOnly date = DateOnly.FromDateTime(dateSelected);
            ShiftManager.SetSelectedShift(date, cbIsAM.Checked);
            RefreshFloorplanFlowPanel(ShiftManager.SelectedShift.Floorplans);
            //UpdateCountLabels();

            //if (!isNewShift)
            //{
            //    pastShiftsManager.ClearFloorplans();
            //    foreach (DiningArea diningArea in DiningAreaManager.DiningAreas)
            //    {
            //        Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(diningArea, date, cbIsAM.Checked);
            //        if (fp != null)
            //        {
            //            pastShiftsManager.AddFloorplanAndServers(fp);

            //        }
            //    }
            //    RefreshFloorplanFlowPanel(pastShiftsManager.Floorplans);
            //    UpdateCountLabels();
            //}
            //else
            //{

            //    RefreshFloorplanFlowPanel(newShiftManager.Floorplans);

            //}


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

            foreach (Floorplan fp in floorplans)
            {
                FloorplanInfoControl infoPanel = new FloorplanInfoControl(fp, width)
                {

                    Padding = new Padding(4, 0, 0, 0),
                    BackColor = UITheme.CanvasColor,
                    Tag = fp
                };
                infoPanelList.Add(infoPanel);

                RadioButton rb = new RadioButton
                {
                    Width = width - 8,
                    Height = height,
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
                    //TabIndex = tabIndex++

                };
                rb.GotFocus += (sender, e) => { ((RadioButton)sender).Checked = true; };
                rb.CheckedChanged += FloorplanRadioButton_CheckedChanged;
                if (floorplanCount == 1)
                {
                    rb.Checked = true;
                }
                floorplanCount++;


                FlowLayoutPanel serversInFloorplanPanel = new FlowLayoutPanel
                {
                    Width = width - 8,
                    Height = flowDiningAreaAssignment.Height - height,
                    Margin = new Padding(4),
                    BackColor = UITheme.CanvasColor,
                    Tag = fp
                };
                DiningAreaRBs.Add(rb);
                flowList.Add(serversInFloorplanPanel);
                foreach (var server in fp.Servers)
                {
                    Button newServerButton = CreateServerButton(server);
                    newServerButton.Click += MoveFromFloorplanServerButton_Click;
                    newServerButton.TabStop = false;
                    newServerButton.Width = serversInFloorplanPanel.Width - 8;
                    serversInFloorplanPanel.Controls.Add(newServerButton);
                }

            }
            foreach (Control c in infoPanelList)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }

            foreach (Control c in DiningAreaRBs)
            {
                flowDiningAreaAssignment.Controls.Add(((Control)c));
            }
            foreach (Control c in flowList)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }

        }


        private List<Control> DiningAreaRBs = new List<Control>();
        List<FloorplanInfoControl> infoPanelList = new List<FloorplanInfoControl>();




        private Button CreateServerButton(Server server)
        {
            Button b = new Button
            {
                Width = 130,
                Height = 30,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(5),
                Text = server.ToString(),
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.ButtonColor,
                ForeColor = Color.Black,
                Font = UITheme.MainFont,
                TabStop = false,
                Tag = server,
                Dock = DockStyle.Top
            };
            return b;
        }
        private void MoveFromFloorplanServerButton_Click(object sender, EventArgs e)
        {
            Button serverButton = sender as Button;
            if (serverButton == null) return;

            Server server = (Server)serverButton.Tag;

            foreach (Control c in flowDiningAreaAssignment.Controls)
            {
                if (c is FlowLayoutPanel flowLayoutPanel)
                {
                    foreach (Control c2 in flowLayoutPanel.Controls)
                    {
                        if (c2.Tag == server)
                        {
                            flowLayoutPanel.Controls.Remove(c2);
                            if (flowLayoutPanel.Tag is Floorplan fp)
                            {
                                //newShiftManager.RemoveServerFromFloorplanByDiningArea(server, fp);
                                ShiftManager.SelectedShift.RemoveServerFromFloorplanByDiningArea(server, fp);
                            }
                        }
                    }
                }
            }
            //newShiftManager.SelectedFloorplan.AddServerAndSection(server);
            ShiftManager.SelectedShift.SelectedFloorplan.AddServerAndSection(server);

            FlowLayoutPanel SelectedTargetPanel = null;
            foreach (Control control in flowDiningAreaAssignment.Controls)
            {
                //if (control is FlowLayoutPanel panel && panel.Tag == newShiftManager.SelectedFloorplan)
                if (control is FlowLayoutPanel panel && panel.Tag == ShiftManager.SelectedShift.SelectedFloorplan)
                {
                    SelectedTargetPanel = panel;

                    break;
                }
            }
            if (SelectedTargetPanel != null)
            {
                Button newServerButton = CreateServerButton(server);
                newServerButton.Click += MoveFromFloorplanServerButton_Click;
                newServerButton.Width = SelectedTargetPanel.Width - 8;

                SelectedTargetPanel.Controls.Add(newServerButton);
            }
            RefreshFloorplanCountLabels();
        }
        private void ServerControl_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            ServerControl serverControl = sender as ServerControl;
            if (sender is ServerControl)
            {
                if (serverControl == null) return;
                server = serverControl.Server;
            }

            //if (newShiftManager.UnassignedServers.Contains(server))
            if (ShiftManager.SelectedShift.UnassignedServers.Contains(server))
            {
                //newShiftManager.AddServerToAFloorplan(server);
                ShiftManager.SelectedShift.AddServerToAFloorplan(server);
                flowUnassignedServers.Controls.Remove(serverControl);
            }
            else
            {
                foreach (Control c in flowDiningAreaAssignment.Controls)
                {
                    if (c is FlowLayoutPanel flowLayoutPanel)
                    {
                        foreach (Control c2 in flowLayoutPanel.Controls)
                        {
                            if (c2.Tag == server)
                            {
                                flowLayoutPanel.Controls.Remove(c2);
                                if (flowLayoutPanel.Tag is Floorplan fp)
                                {
                                    fp.RemoveServerAndSection(server);
                                }
                            }
                        }
                    }
                }
            }

            //newShiftManager.SelectedFloorplan.AddServerAndSection(server);
            //NewAddServerButtonToFloorplan(newShiftManager.SelectedFloorplan, server);
            ShiftManager.SelectedShift.SelectedFloorplan.AddServerAndSection(server);
            NewAddServerButtonToFloorplan(ShiftManager.SelectedShift.SelectedFloorplan, server);
            RefreshFloorplanCountLabels();
        }
        private void NewAddServerButtonToFloorplan(Floorplan floorplan, Server server)
        {
            FlowLayoutPanel SelectedTargetPanel = null;
            foreach (Control control in flowDiningAreaAssignment.Controls)
            {
                if (control is FlowLayoutPanel panel && panel.Tag == floorplan)
                {
                    SelectedTargetPanel = panel;
                    break;
                }
            }
            if (SelectedTargetPanel != null)
            {
                Button newServerButton = CreateServerButton(server);
                newServerButton.Width = SelectedTargetPanel.Width - 8;
                newServerButton.Click += MoveFromFloorplanServerButton_Click;

                SelectedTargetPanel.Controls.Add(newServerButton);
            }
            RefreshFloorplanCountLabels();
        }

        private void btnAssignTables_Click(object sender, EventArgs e)
        {
            bool emptyFloorplan = false;
            foreach (Floorplan fp in ShiftManager.SelectedShift.Floorplans)
            {
                fp.Date = dateSelected;
                if (fp.Servers.Count == 0)
                {
                    emptyFloorplan = true;
                }
            }
            if (emptyFloorplan)
            {
                MessageBox.Show("All floorplans must have a servers assigned.");
                return;
            }
            ShiftManager.SelectedShift.DateOnly = new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);
            form1Reference.UpdateForm1ShiftManager(this.ShiftManager.SelectedShift);

        }

        private void RefreshFloorplanCountLabels()
        {
            List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(
                ShiftManager.IsAM, ShiftManager.DateOnly.AddDays(DaysAgoStats));
            foreach (FloorplanInfoControl info in infoPanelList)
            {
                info.UpdateCurrentLabels(DaysAgoStats);
            }
            if(ShiftManager.SelectedShift.DiningAreasUsed.Count !=0)
            {
                foreach(DiningArea area in ShiftManager.SelectedShift.DiningAreasUsed)
                {
                    area.SetTableSales(stats);
                }
            }


        }
        private void FloorplanRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                //newShiftManager.SelectedFloorplan = (Floorplan)rb.Tag;
                ShiftManager.SelectedShift.SelectedFloorplan = (Floorplan)rb.Tag;

            }
        }

        private void cbIsPM_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsAM.Checked)
            {
                cbIsAM.Image = Resources.smallSunrise;
                cbIsAM.BackColor = Color.FromArgb(251, 175, 0);
                //newShiftManager.SetFloorplansToAM();
                ShiftManager.SelectedShift.SetFloorplansToAM();

            }
            else
            {
                cbIsAM.Image = Resources.smallMoon;
                cbIsAM.BackColor = Color.FromArgb(117, 70, 104);
                //newShiftManager.SetFloorplansToPM();
                ShiftManager.SelectedShift.SetFloorplansToPM();

            }
            SetFloorplansForShiftManager();

        }

        private void btnDateUp_Click(object sender, EventArgs e)
        {
            MoveDateForward();
        }
        public void MoveDateForward()
        {
            dateSelected = dateSelected.AddDays(1);
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");

            SetFloorplansForShiftManager();
        }
        private void btnDateDown_Click(object sender, EventArgs e)
        {
            MovedDateBack();
        }
        public void MovedDateBack()
        {
            dateSelected = dateSelected.AddDays(-1);
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");

            SetFloorplansForShiftManager();
        }
        private void setIsNewShiftBool()
        {
            DateOnly date = DateOnly.FromDateTime(dateSelected);
            foreach (DiningArea diningArea in DiningAreaManager.DiningAreas)
            {
                Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(diningArea, date, cbIsAM.Checked);
                if (fp != null)
                {
                    isNewShift = false;
                    return;

                }
            }
            isNewShift = true;

        }
        private void btnCreateANewShift_Click(object sender, EventArgs e)
        {
            frmNewShiftDatePicker form = new frmNewShiftDatePicker(DiningAreaManager, allFloorplans, this, dateSelected, cbIsAM.Checked, ShiftManager);
            form.TopLevel = false;
            this.Controls.Add(form);
            form.Show();
            form.BringToFront();


        }
        //TODO have this method load current floorplans
        public void UpdateNewShift(ShiftManager shiftManager)
        {
            cboSalesMethod.SelectedIndex = 1;
            //this.newShiftManager = null;
            //this.newShiftManager = shiftManagerToAdd;
            //dateSelected = newShiftManager.DateOnly.ToDateTime(new TimeOnly(0, 0));
            //lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            //cbIsAM.Checked = newShiftManager.IsAM;

            //RefreshFloorplanFlowPanel(newShiftManager.Floorplans);
            //this.ShiftManager.SelectedShift = null;
            this.ShiftManager = shiftManager;
            dateSelected = ShiftManager.DateOnly.ToDateTime(new TimeOnly(0, 0));
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            cbIsAM.Checked = ShiftManager.IsAM;

            RefreshFloorplanFlowPanel(ShiftManager.SelectedShift.Floorplans);
            PopulateUnassignedServers();
            UpdateCountLabels();

        }
        private Button AutoAssignButton()
        {
            Button btnAutoAssign = new Button()
            {
                Dock = DockStyle.Top,
                Text = "Auto-Assign",
                Size = new Size(305, 25)
            };
            btnAutoAssign.Click += btnAutoAssign_Click;
            
            return btnAutoAssign;
        }

        private void btnAutoAssign_Click(object? sender, EventArgs e)
        {
            string FloorplansString = "";
            Dictionary<DiningArea, int> distributions = 
                FloorplanGenerator.GetServerDistribution(ShiftManager.SelectedShift.DiningAreasUsed, 
                ShiftManager.SelectedShift.ServersOnShift.Count());

            //foreach (Floorplan fp in ShiftManager.SelectedShift.Floorplans)
            //{
            //    FloorplansString += fp.DiningArea.Name + ", ";
            //}
            foreach (DiningArea area in distributions.Keys)
            {
                FloorplansString += area + ": " + distributions[area].ToString() + "\n";
            }
            MessageBox.Show(FloorplansString);
        }

        private void PopulateUnassignedServers()
        {
            flowUnassignedServers.Controls.Clear();
            Button btnAutoAssign = this.AutoAssignButton();
           
            flowUnassignedServers.Controls.Add((Control)btnAutoAssign);

            foreach (var server in ShiftManager.SelectedShift.UnassignedServers)
            {
                //newShiftManager.ServersNotOnShift.Remove(server);
                ShiftManager.SelectedShift.ServersNotOnShift.Remove(server);
                ServerControl newServerControl = new ServerControl(server, 20);
                newServerControl.Margin = new Padding(5);
                newServerControl.Click += ServerControl_Click;
                ImageSetter.SetShiftControlImages(newServerControl);
                flowUnassignedServers.Controls.Add(newServerControl);

            }
        }

        private void frmEditStaff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                MovedDateBack();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                MoveDateForward();
                e.Handled = true;
            }
        }

        private void flowUnassignedServers_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboSalesMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboSalesMethod.SelectedItem == "Yesterday")
            {
                DaysAgoStats = -1;
            }
            if (cboSalesMethod.SelectedItem == "Last Weekday")
            {
                DaysAgoStats = -7;
            }
            RefreshFloorplanCountLabels();

        }
    }
}
