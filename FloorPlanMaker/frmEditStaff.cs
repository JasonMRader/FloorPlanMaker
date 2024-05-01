
using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorPlanMakerUI.Properties;
using System;
using System.Data;
using System.Windows.Forms;

namespace FloorPlanMaker
{
    public partial class frmEditStaff : Form
    {
        //public List<Server> AllServers = new List<Server>();
        public EmployeeManager employeeManager;
        private ShiftManager newShiftManager = new ShiftManager();
        private ShiftManager pastShiftsManager;
        private DiningAreaCreationManager DiningAreaManager = new DiningAreaCreationManager();
        private DateTime dateSelected = DateTime.MinValue;
        private List<Floorplan> allFloorplans = new List<Floorplan>();
        private int currentFocusedFloorplanIndex = 0;

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
        // TODO: refactor the backend logic out of this form
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Check if the Tab key is pressed
            if (keyData == Keys.Tab)
            {
                MoveToNextFloorplan();
                return true;
            }

            if (keyData == Keys.Left)
            {
                MovedDateBack();
                return true; // Indicate that you've handled this key press
            }

            // Check if the Right Arrow key is pressed
            if (keyData == Keys.Right)
            {
                MoveDateForward();
                return true; // Indicate that you've handled this key press
            }


            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MoveToNextFloorplan()
        {
            if (newShiftManager == null) { return; }
            currentFocusedFloorplanIndex++;
            if (currentFocusedFloorplanIndex == DiningAreaRBs.Count) { currentFocusedFloorplanIndex = 0; }

            var rbToFocus = DiningAreaRBs[currentFocusedFloorplanIndex];
            rbToFocus.Focus();

        }



        public frmEditStaff(EmployeeManager staffManager, ShiftManager shiftManager, Form1 form1)
        {
            InitializeComponent();
            this.employeeManager = staffManager;
            this.pastShiftsManager = shiftManager;
            this.form1Reference = form1;
            allFloorplans = SqliteDataAccess.LoadFloorplanList();
        }

        private Dictionary<DiningArea, int> PreviousServerCountsForNewShift(int Days)
        {
            DateTime targetDate = dateSelected.AddDays(Days);
            DateOnly dateOnly = new DateOnly(targetDate.Year, targetDate.Month, targetDate.Day);
            List<Floorplan> floorplansResults = new List<Floorplan>();


            foreach (Floorplan floorplan in newShiftManager.Floorplans)
            {
                Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(floorplan.DiningArea, dateOnly, newShiftManager.IsAM);
                if (fp != null)
                {
                    floorplansResults.Add(fp);
                }
            }

            var result = new Dictionary<DiningArea, int>();

            foreach (var floorplan in newShiftManager.Floorplans)
            {
                result[floorplan.DiningArea] = 0;
            }


            foreach (var fp in floorplansResults)
            {
                result[fp.DiningArea] += fp.Servers.Count;
            }

            return result;
        }
        private Dictionary<DiningArea, int> PreviousServerCountsForOldShift(int Days)
        {
            DateTime targetDate = dateSelected.AddDays(Days);
            DateOnly dateOnly = new DateOnly(targetDate.Year, targetDate.Month, targetDate.Day);
            List<Floorplan> floorplansResults = new List<Floorplan>();


            foreach (Floorplan floorplan in pastShiftsManager.Floorplans)
            {
                Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(floorplan.DiningArea, dateOnly, cbIsAM.Checked);
                if (fp != null)
                {
                    floorplansResults.Add(fp);
                }
            }

            var result = new Dictionary<DiningArea, int>();

            foreach (var floorplan in floorplansResults)
            {
                result[floorplan.DiningArea] = 0;
            }


            foreach (var fp in floorplansResults)
            {
                result[fp.DiningArea] += fp.Servers.Count;
            }

            return result;
        }

        private void UpdateCountLabels()
        {
            Dictionary<DiningArea, int> LastWeekFloorplans = new Dictionary<DiningArea, int>();
            Dictionary<DiningArea, int> yesterdayCounts = new Dictionary<DiningArea, int>();
            if (isNewShift)
            {
                LastWeekFloorplans = PreviousServerCountsForNewShift(-7);
                yesterdayCounts = PreviousServerCountsForNewShift(-1);
            }
            else
            {
                LastWeekFloorplans = PreviousServerCountsForOldShift(-7);
                yesterdayCounts = PreviousServerCountsForOldShift(-1);
            }
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
                    infoPanel.UpdateCurrentLabels();
                }
            }
        }


        private void frmEditStaff_Load(object sender, EventArgs e)
        {
            dateSelected = DateTime.Now;
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            SetColorTheme();
            SetFloorplansForShiftManager();


        }


        private void SetFloorplansForShiftManager()
        {
            flowDiningAreaAssignment.Controls.Clear();
            DateOnly date = DateOnly.FromDateTime(dateSelected);
            if (!isNewShift)
            {
                pastShiftsManager.ClearFloorplans();
                foreach (DiningArea diningArea in DiningAreaManager.DiningAreas)
                {
                    Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(diningArea, date, cbIsAM.Checked);
                    if (fp != null)
                    {
                        pastShiftsManager.AddFloorplanAndServers(fp);

                    }
                }
                RefreshFloorplanFlowPanel(pastShiftsManager.Floorplans);
                UpdateCountLabels();
            }
            else
            {
                foreach (DiningArea diningArea in DiningAreaManager.DiningAreas)
                {
                    Floorplan fp = newShiftManager.Floorplans.FirstOrDefault(fp => fp.DiningArea == diningArea);
                    //Floorplan existingFP = SqliteDataAccess.LoadFloorplanByCriteria(diningArea, date, cbIsAM.Checked);
                    if (fp != null)
                    {

                    }
                }
                RefreshFloorplanFlowPanel(newShiftManager.Floorplans);

            }
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
                Text = server.AbbreviatedName,
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
                                newShiftManager.RemoveServerFromFloorplanByDiningArea(server, fp);
                            }
                        }
                    }
                }
            }
            newShiftManager.SelectedFloorplan.AddServerAndSection(server);

            FlowLayoutPanel SelectedTargetPanel = null;
            foreach (Control control in flowDiningAreaAssignment.Controls)
            {
                if (control is FlowLayoutPanel panel && panel.Tag == newShiftManager.SelectedFloorplan)
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

            if (newShiftManager.UnassignedServers.Contains(server))
            {
                newShiftManager.UnassignedServers.Remove(server);
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

            newShiftManager.SelectedFloorplan.AddServerAndSection(server);
            NewAddServerButtonToFloorplan(newShiftManager.SelectedFloorplan, server);
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
            foreach (Floorplan fp in newShiftManager.Floorplans)
            {
                fp.Date = dateSelected;
            }
            newShiftManager.DateOnly = new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);
            form1Reference.UpdateForm1ShiftManager(this.newShiftManager);
        }

        private void RefreshFloorplanCountLabels()
        {
            foreach (FloorplanInfoControl info in infoPanelList)
            {
                info.UpdateCurrentLabels();
            }


        }
        private void FloorplanRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                newShiftManager.SelectedFloorplan = (Floorplan)rb.Tag;
                //cbUnassignedServers.Checked = false;
            }
        }

        private void cbIsPM_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsAM.Checked)
            {
                cbIsAM.Image = Resources.smallSunrise;
                cbIsAM.BackColor = Color.FromArgb(251, 175, 0);
                newShiftManager.SetFloorplansToAM();

            }
            else
            {
                cbIsAM.Image = Resources.smallMoon;
                cbIsAM.BackColor = Color.FromArgb(117, 70, 104);
                newShiftManager.SetFloorplansToPM();

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
            setIsNewShiftBool();
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
            setIsNewShiftBool();
            SetFloorplansForShiftManager();
        }
        private void setIsNewShiftBool()
        {
            DateOnly dateOnlySelected = DateOnly.FromDateTime(dateSelected);
            if (dateOnlySelected == newShiftManager.DateOnly)
            {
                isNewShift = true;
            }
            else
            {
                isNewShift = false;
            }
        }
        private void btnCreateANewShift_Click(object sender, EventArgs e)
        {
            frmNewShiftDatePicker form = new frmNewShiftDatePicker(DiningAreaManager, allFloorplans, employeeManager.AllServers, this);
            form.TopLevel = false;
            this.Controls.Add(form);
            form.Show();
            form.BringToFront();

        }
        //TODO have this method load current floorplans
        public void UpdateNewShift(ShiftManager shiftManagerToAdd)
        {
            this.newShiftManager = null;
            this.newShiftManager = shiftManagerToAdd;
            dateSelected = newShiftManager.DateOnly.ToDateTime(new TimeOnly(0, 0));
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            cbIsAM.Checked = newShiftManager.IsAM;
            setIsNewShiftBool();
            SetFloorplansForShiftManager();
            PopulateUnassignedServers();
            //btnCreateANewShift.Visible = false;
            UpdateCountLabels();

        }
        private void PopulateUnassignedServers()
        {
            foreach (var server in newShiftManager.UnassignedServers)
            {
                newShiftManager.ServersNotOnShift.Remove(server);
                ServerControl newServerControl = new ServerControl(server, 20);
                newServerControl.Click += ServerControl_Click;
                //newServerControl.AddRemoveButton(flowUnassignedServers, flowAllServers, newShiftManager.UnassignedServers, newShiftManager.ServersNotOnShift, 155, 20);
                //newServerControl.RemoveButton.Click += serverControl_Click_RemoveFromShift;
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
    }
}
