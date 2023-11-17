
using FloorplanClassLibrary;
using FloorPlanMakerUI;
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
        private bool isDraggingForm = false;
        private Point lastLocation;
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


            UITheme.FormatMainButton(cbIsAM);
            UITheme.FormatMainButton(btnAddNewServer);

            //lblShiftDate.Font = UITheme.LargeFont;


            //AppColors.FormatSecondColor(pnlAddTables);


            //AppColors.FormatCanvasColor(flowServersInFloorplan);
        }
        private void frmEditStaff_MouseDown(object sender, MouseEventArgs e)
        {
            isDraggingForm = true;
            lastLocation = e.Location;
        }
        private void frmEditStaff_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingForm)
            {
                this.Location = new System.Drawing.Point(
                    (this.Location.X - lastLocation.X) + e.X,
                    (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }
        private void frmEditStaff_MouseUp(object sender, MouseEventArgs e)
        {
            isDraggingForm = false;
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
            foreach (Label lbl in PastStaffLevelLabels)
            {
                if (lbl.Tag is DiningArea area)
                {
                    string yesterdayText = "0";
                    string lastWeekText = "0";

                    if (yesterdayCounts.TryGetValue(area, out int yesterdayCount))
                    {
                        yesterdayText = $"{yesterdayCount}";
                    }

                    if (LastWeekFloorplans.TryGetValue(area, out int lastWeekCount))
                    {
                        lastWeekText = $"{lastWeekCount}";
                    }

                    lbl.Text = $" {yesterdayText}      |     {lastWeekText}";
                }
            }
        }

        private void btnAddNewServer_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            server.Name = txtNewServerName.Text;
            SqliteDataAccess.SaveNewServer(server);
            txtNewServerName.Clear();
            employeeManager.AllServers.Clear();
            employeeManager.AllServers = SqliteDataAccess.LoadServers();
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
                        //foreach (Control c in flowDiningAreas.Controls)
                        //{
                        //    if (c is CheckBox cb && c.Tag == diningArea)
                        //    {
                        //        cb.Checked = true;
                        //    }
                        //}
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
                    if (fp != null)
                    {
                        //foreach (Control c in flowDiningAreas.Controls)
                        //{
                        //    if (c is CheckBox cb && c.Tag == diningArea)
                        //    {
                        //        cb.Checked = true;
                        //    }
                        //}
                    }
                }
                RefreshFloorplanFlowPanel(newShiftManager.Floorplans);

            }
        }

        private void refreshTabOrder()
        {
            int tabIndexNum = 0;
            foreach (Control c in flowDiningAreaAssignment.Controls)
            {
                if (c is RadioButton rb)
                {
                    rb.TabStop = true;
                    rb.TabIndex = tabIndexNum++;
                }
            }
        }
        private void DisableTabStopForControls(Control parentControl)
        {
            foreach (Control c in parentControl.Controls)
            {
                if (!(c is RadioButton))
                {
                    c.TabStop = false;
                }
                // Recursively apply for child controls
                DisableTabStopForControls(c);
            }
        }
        private void RefreshFloorplanFlowPanel(IReadOnlyList<Floorplan> floorplans)
        {
            DisableTabStopForControls(this);
            flowDiningAreaAssignment.Controls.Clear();
            ServerMaxLabels.Clear();
            ServerAvgLabels.Clear();
            ServerCountLabels.Clear();
            PastStaffLevelLabels.Clear();
            DiningAreaRBs.Clear();
            List<Panel> infoPanelList = new List<Panel>();

            int selectedCount = floorplans.Count;
            if (selectedCount == 0)
                return;

            int width = flowDiningAreaAssignment.Width / selectedCount;
            int height = 30;
            int floorplanCount = 1;
            bool isChecked = true;
            ServerCountLabels = new List<Control>();
            List<Control> flowList = new List<Control>();

            foreach (Floorplan fp in floorplans)
            {
                Panel panel = new Panel
                {
                    Width = width,
                    Height = 60,
                    Padding = new Padding(4,0,0,0),
                    BackColor = UITheme.CanvasColor,
                    Tag = fp
                };
                infoPanelList.Add(panel);

                RadioButton rb = new RadioButton
                {
                    Width = width,
                    Height = height,
                    Appearance = Appearance.Button,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(0),
                    Font = UITheme.MainFont,
                    FlatStyle = FlatStyle.Flat,
                    BackColor = UITheme.ButtonColor,
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
                Label label = new Label
                {
                    AutoSize = false,
                    Width = width - 8,
                    Height = 20,
                    Margin = new Padding(4, 0, 4, 0),
                    BackColor = UITheme.CTAColor,
                    Text = fp.Servers.Count.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                    Tag = fp
                };
                ServerCountLabels.Add(label);
                Label maxLabel = new Label
                {
                    AutoSize = false,
                    Width = (width - 8),
                    Height = 20,
                    BackColor = UITheme.ButtonColor,
                    Margin = new Padding(4, 0, 4, 0),
                    Text =  fp.DiningArea.GetMaxCovers().ToString() + "  |  " + Section.FormatAsCurrencyWithoutParentheses(fp.DiningArea.GetAverageCovers()),//.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                    Tag = fp
                };
                ServerMaxLabels.Add(maxLabel);
                Label pastLabel = new Label
                {
                    AutoSize = false,
                    Width = (width - 8),
                    Height = 35,
                    Dock = DockStyle.Top,
                    BackColor = UITheme.SecondColor,
                    Margin = new Padding(4, 0, 4, 0),
                    //Text = "Yesterday: " + fp.DiningArea.Name + "Last Week: " + fp.DiningArea.Name,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                    Tag = fp.DiningArea
                };
                PastStaffLevelLabels.Add(pastLabel);


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
            foreach (Control c in PastStaffLevelLabels)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }

            foreach (Control c in ServerCountLabels)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }
            foreach (Control c in ServerMaxLabels)
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
            refreshTabOrder();

        }
        private List<Control> ServerCountLabels = new List<Control>();
        private List<Control> ServerMaxLabels = new List<Control>();
        private List<Control> ServerAvgLabels = new List<Control>();
        private List<Control> PastStaffLevelLabels = new List<Control>();
        private List<Control> DiningAreaRBs = new List<Control>();


        private void serverControl_Click_RemoveFromShift(object sender, EventArgs e)
        {
            Button removeButton = (Button)sender;
            ServerControl oldServerControl = (ServerControl)removeButton.Parent.Parent;
            Server server = oldServerControl.Server;
            newShiftManager.ServersNotOnShift.Add(server);
            newShiftManager.UnassignedServers.Remove(server);
            Button serverControl = CreateServerButton(server);
            //flowAllServers.Controls.Add(serverControl);
            flowUnassignedServers.Controls.Remove(oldServerControl);
        }

        private Button CreateServerButton(Server server)
        {
            Button b = new Button
            {
                Width = 130,
                Height = 30,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(5),
                Text = server.Name,
                FlatStyle = FlatStyle.Flat,
                BackColor = UITheme.ButtonColor,
                ForeColor = Color.Black,
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
            //if (cbUnassignedServers.Checked)
            //{
            //    newShiftManager.UnassignedServers.Add(server);
            //    ServerControl serverControl = new ServerControl(server, 350, 20);
            //    serverControl.Click += ServerControl_Click;
            //    ImageSetter.SetShiftControlImages(serverControl);
            //    flowUnassignedServers.Controls.Add(serverControl);
            //}
            //else
            //{
            //    newShiftManager.SelectedFloorplan.AddServerAndSection(server);
            //}
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
            //if (cbUnassignedServers.Checked)
            //{
            //    newShiftManager.UnassignedServers.Add(server);
            //    ServerControl newServerControl = new ServerControl(server, 350, 30);
            //    newServerControl.Click += ServerControl_Click;
            //    //newServerControl.AddRemoveButton(flowUnassignedServers, flowAllServers, newShiftManager.UnassignedServers, newShiftManager.ServersNotOnShift, 155, 20);
            //    // newServerControl.RemoveButton.Click += serverControl_Click_RemoveFromShift;
            //    ImageSetter.SetShiftControlImages(newServerControl);
            //    flowUnassignedServers.Controls.Add(newServerControl);
            //}
            //else
            //{
            //    newShiftManager.SelectedFloorplan.AddServerAndSection(server);
            //}
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
            foreach (Control c in ServerCountLabels)
            {
                if (c is Label fpCountLabel)
                {
                    if (fpCountLabel.Tag is Floorplan fp)
                    {
                        fpCountLabel.Text = fp.Servers.Count.ToString();
                    }
                }
            }
            foreach (Control c in ServerMaxLabels)
            {
                if (c is Label fpMaxLabel)
                {
                    if (fpMaxLabel.Tag is Floorplan fp)
                    {
                        if (fp.Servers.Count > 0)
                        {
                            fpMaxLabel.Text = (fp.DiningArea.GetMaxCovers() / fp.Servers.Count).ToString("F0")
                                + Section.FormatAsCurrencyWithoutParentheses((fp.DiningArea.GetAverageCovers() / fp.Servers.Count));
                        }
                        else
                        {
                            fpMaxLabel.Text = fp.DiningArea.GetMaxCovers().ToString() + Section.FormatAsCurrencyWithoutParentheses(fp.DiningArea.GetAverageCovers());
                        }
                    }
                }
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
        //private void cbUnassignedServers_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbUnassignedServers.Checked)
        //    {
        //        foreach (Control c in flowDiningAreaAssignment.Controls)
        //        {
        //            if (c is RadioButton rb)
        //            {
        //                rb.Checked = false;
        //            }
        //        }
        //        newShiftManager.SelectedFloorplan = null;
        //    }
        //}
        private void cbIsPM_CheckedChanged(object sender, EventArgs e)
        {
            if (cbIsAM.Checked)
            {
                cbIsAM.Text = "AM";
                newShiftManager.SetFloorplansToPM();
            }
            else
            {
                cbIsAM.Text = "PM";
                newShiftManager.SetFloorplansToAM();
            }
        }

        private void btnDateUp_Click(object sender, EventArgs e)
        {
            dateSelected = dateSelected.AddDays(1);
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            setIsNewShiftBool();
            SetFloorplansForShiftManager();
        }
        private void btnDateDown_Click(object sender, EventArgs e)
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
                ServerControl newServerControl = new ServerControl(server, flowUnassignedServers.Width - 10, 20);
                newServerControl.Click += ServerControl_Click;
                //newServerControl.AddRemoveButton(flowUnassignedServers, flowAllServers, newShiftManager.UnassignedServers, newShiftManager.ServersNotOnShift, 155, 20);
                //newServerControl.RemoveButton.Click += serverControl_Click_RemoveFromShift;
                ImageSetter.SetShiftControlImages(newServerControl);
                flowUnassignedServers.Controls.Add(newServerControl);

            }
        }
    }
}
