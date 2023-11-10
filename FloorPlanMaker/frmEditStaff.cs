
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
        private System.Drawing.Point lastLocation;
        private Form1 form1Reference;
        private List<Floorplan> FloorplansForDateAndShift = new List<Floorplan>();
        private bool isNewShift = false;
        private void SetColorTheme()
        {
            btnCreateANewShift.BackColor = AppColors.CTAColor;
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
        private void SetEnableStatusOfDiningAreaButtons()
        {
            foreach (Control c in flowDiningAreas.Controls)
            {
                c.Enabled = isNewShift;
            }
            foreach (Control c in flowAllServers.Controls)
            {
                c.Enabled = isNewShift;
            }
        }
        public frmEditStaff(EmployeeManager staffManager, ShiftManager shiftManager, Form1 form1)
        {
            InitializeComponent();
            this.employeeManager = staffManager;
            this.pastShiftsManager = shiftManager;
            this.form1Reference = form1;
            allFloorplans = SqliteDataAccess.LoadFloorplanList();

        }
        //private Dictionary<DiningArea, int> ServersAssignedPreviousDay(List<Floorplan> floorplans, bool isLunch, int Days)
        //{
        //    DateTime oneWeekAgo = dateSelected.AddDays(Days);

        //    var result = floorplans
        //        .Where(fp => fp.Date.Date == oneWeekAgo.Date)// && fp.IsLunch == isLunch)
        //        .GroupBy(fp => fp.DiningArea)
        //        .ToDictionary(group => group.Key, group => group.Sum(fp => fp.Servers.Count));

        //    return result;
        //}
        private Dictionary<DiningArea, int> ServersAssignedPreviousDay(List<Floorplan> floorplans, bool isLunch, int Days)
        {
            DateTime targetDate = dateSelected.AddDays(Days);
            var relevantFloorplans = floorplans
                .Where(fp => fp.Date.Date == targetDate.Date && fp.IsLunch == isLunch)
                .ToList();

            var result = new Dictionary<DiningArea, int>();

            // Initialize all dining areas with zero count
            foreach (var area in DiningAreaManager.DiningAreas)
            {
                result[area] = 0;
            }

            // Count servers for the relevant floorplans
            foreach (var fp in relevantFloorplans)
            {
                // Now this should update the entry instead of creating a new one
                result[fp.DiningArea] += fp.Servers.Count;
            }

            return result;
        }
        private Dictionary<DiningArea, int> PreviousServerCountsForDiningArea(int Days)
        {
            DateTime targetDate = dateSelected.AddDays(Days);
            DateOnly dateOnly = new DateOnly(targetDate.Year,targetDate.Month, targetDate.Day);
            List<Floorplan> floorplans = new List<Floorplan>();
            foreach(Floorplan floorplan in newShiftManager.Floorplans)
            {
               Floorplan fp = SqliteDataAccess.LoadFloorplanByCriteria(floorplan.DiningArea, dateOnly, newShiftManager.IsAM);
                if(fp != null)
                {
                    floorplans.Add(fp);
                }
            }
                    
           
            var result = new Dictionary<DiningArea, int>();

            
            foreach (var floorplan in newShiftManager.Floorplans)
            {
                result[floorplan.DiningArea] = 0;
            }

            // Count servers for the relevant floorplans
            foreach (var fp in floorplans)
            {
                // Now this should update the entry instead of creating a new one
                result[fp.DiningArea] += fp.Servers.Count;
            }

            return result;
        }
        private void UpdateCountLabels()
        {
            Dictionary<DiningArea, int> LastWeekFloorplans = PreviousServerCountsForDiningArea(-1);
            Dictionary<DiningArea, int> yesterdayCounts = PreviousServerCountsForDiningArea(-7);
            foreach (Label lbl in PastStaffLevelLabels)
            {
                if (lbl.Tag is DiningArea area)
                {
                    string yesterdayText = "Yesterday: N/A";
                    string lastWeekText = "Last Week: N/A";

                    // Check if yesterday's counts are available for this area and update the text.
                    if (yesterdayCounts.TryGetValue(area, out int yesterdayCount))
                    {
                        yesterdayText = $"Yesterday: {yesterdayCount}";
                    }

                    // Check if last week's counts are available for this area and update the text.
                    if (LastWeekFloorplans.TryGetValue(area, out int lastWeekCount))
                    {
                        lastWeekText = $"Last Week: {lastWeekCount}";
                    }

                    // Set the label's text.
                    lbl.Text = $" {yesterdayText} \n {lastWeekText}";
                }

            }
        }
        private void RefreshPreviousFloorplanCounts()
        {

            Dictionary<DiningArea, int> LastWeekFloorplans = ServersAssignedPreviousDay(allFloorplans, cbIsAM.Checked, -7);
            Dictionary<DiningArea, int> YesterdayFloorplans = ServersAssignedPreviousDay(allFloorplans, cbIsAM.Checked, -1);
            flowLastWeekdayCounts.Controls.Clear(); // Clear any existing controls
            flowYesterdayCounts.Controls.Clear();

            foreach (var entry in LastWeekFloorplans)
            {
                Label lbl = new Label();
                lbl.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
                lbl.Text = $"{entry.Key}: {entry.Value}";
                lbl.AutoSize = true;
                flowLastWeekdayCounts.Controls.Add(lbl);
            }
            foreach (var entry in YesterdayFloorplans)
            {
                Label lbl = new Label();
                lbl.Font = new Font("Segoe UI", 12f, FontStyle.Bold);
                lbl.AutoSize = true;
                lbl.Text = $"{entry.Key}: {entry.Value}";
                flowYesterdayCounts.Controls.Add(lbl);
            }
            //UpdateCountLabels(flowDiningAreaAssignment, YesterdayFloorplans, LastWeekFloorplans);

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
            //clbDiningAreaSelection.DataSource = DiningAreaManager.DiningAreas;

            //LoadDiningAreas();
            dateSelected = DateTime.Now;
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            //lblLastWeekDay.Text = "Last " + dateSelected.ToString("dddd") + ":";
            SetColorTheme();

            RefreshPreviousFloorplanCounts();
            SetFloorplansForShiftManager();


            //lbServersOnShift.ValueMember = "Value";
        }

        //private void LoadDiningAreas()
        //{
        //    foreach (DiningArea area in DiningAreaManager.DiningAreas)
        //    {
        //        CheckBox btnDining = new CheckBox
        //        {
        //            Text = area.Name,
        //            Tag = area,
        //            Size = new Size(155, 23),
        //            Appearance = Appearance.Button,
        //            TextAlign = ContentAlignment.MiddleCenter,
        //            FlatStyle = FlatStyle.Flat,
        //            ForeColor = Color.Black,
        //            BackColor = AppColors.ButtonColor,
        //            Enabled = false,

        //            TabStop = false

        //        };
        //        btnDining.CheckedChanged += cbDiningArea_CheckChanged;
        //        flowDiningAreas.Controls.Add(btnDining);

        //    }
        //}

        private void SetFloorplansForShiftManager()
        {

            UncheckDiningAreas();
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
                        //newShiftManager.Floorplans.Add(fp);
                        pastShiftsManager.AddFloorplanAndServers(fp);

                        foreach (Control c in flowDiningAreas.Controls)
                        {
                            if (c is CheckBox cb && c.Tag == diningArea)
                            {
                                cb.Checked = true;
                            }

                        }


                    }

                }

                RefreshFloorplanFlowPanel(pastShiftsManager.Floorplans);
                //pastShiftsManager.UpdateUnassignedServers();
                PopulateServersNotOnShift(pastShiftsManager.ServersNotOnShift);
            }
            else
            {

                foreach (DiningArea diningArea in DiningAreaManager.DiningAreas)
                {
                    Floorplan fp = newShiftManager.Floorplans.FirstOrDefault(fp => fp.DiningArea == diningArea);
                    if (fp != null)
                    {
                        //newShiftManager.Floorplans.Add(fp);
                        //pastShiftsManager.Floorplans.Add(fp);
                        foreach (Control c in flowDiningAreas.Controls)
                        {
                            if (c is CheckBox cb && c.Tag == diningArea)
                            {
                                cb.Checked = true;
                            }

                        }


                    }

                }
                RefreshFloorplanFlowPanel(newShiftManager.Floorplans);
                //newShiftManager.UpdateUnassignedServers();
                PopulateServersNotOnShift(newShiftManager.ServersNotOnShift);
            }





        }
       
        private void AddServerToUnassignedServersInShift(Server server)
        {
            newShiftManager.UnassignedServers.Add(server);

            ServerControl newServerControl = new ServerControl(server, 350, 30);
            newServerControl.Click += ServerControl_Click;
            newServerControl.AddRemoveButton(flowUnassignedServers, flowAllServers, newShiftManager.UnassignedServers, newShiftManager.ServersNotOnShift, 155, 20);
            newServerControl.RemoveButton.Click += serverControl_Click_RemoveFromShift;
            ImageSetter.SetShiftControlImages(newServerControl);
            flowUnassignedServers.Controls.Add(newServerControl);
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


            int selectedCount = floorplans.Count;

            if (selectedCount == 0)
                return;

            int width = flowDiningAreaAssignment.Width / selectedCount;
            int height = 30;
            int floorplanCount = 1;
            bool isChecked = true;
            ServerCountLabels = new List<Control>();
            List<Control> flowList = new List<Control>();
            //int tabIndex = 0;
            foreach (Floorplan fp in floorplans)
            {


                RadioButton rb = new RadioButton
                {
                    Width = width,
                    Height = height,
                    Appearance = Appearance.Button,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(0),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = AppColors.ButtonColor,
                    ForeColor = Color.Black,
                    Text = fp.DiningArea.Name,
                    Tag = fp,
                    //TabIndex = tabIndex++

                };
                rb.GotFocus += (sender, e) => { ((RadioButton)sender).Checked = true; };
                //shiftManager.DiningAreasUsed.Add((DiningArea)clbDiningAreaSelection.CheckedItems[i]);
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
                    Margin = new Padding(4),
                    Text = "Servers: " + fp.Servers.Count.ToString(),
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
                    Margin = new Padding(4),
                    Text = "Max: " + fp.DiningArea.GetMaxCovers().ToString() + "  Avg: " + fp.DiningArea.GetAverageCovers().ToString(),
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
                    Margin = new Padding(4),
                    Text = "Yesterday: " + fp.DiningArea.Name + "\n" + "Last Week: " + fp.DiningArea.Name,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 10f, FontStyle.Bold),
                    Tag = fp.DiningArea
                };
                PastStaffLevelLabels.Add(pastLabel);


                FlowLayoutPanel serversInFloorplanPanel = new FlowLayoutPanel
                {
                    Width = width - 8,
                    Height = flowDiningAreaAssignment.Height - height, // Adjust as needed
                    Margin = new Padding(4),
                    BackColor = AppColors.CanvasColor,
                    Tag = fp
                };
                //flowDiningAreaAssignment.Controls.Add(rb);
                DiningAreaRBs.Add(rb);
                //flowDiningAreaAssignment.Controls.Add(floorplanPanel);
                flowList.Add(serversInFloorplanPanel);
                foreach (var server in fp.Servers)
                {
                    Button newServerButton = CreateServerButton(server);
                    newServerButton.Click += MoveFromFloorplanServerButton_Click;
                    newServerButton.TabStop = false;
                    newServerButton.Width = serversInFloorplanPanel.Width - 8;

                    serversInFloorplanPanel.Controls.Add(newServerButton);
                    //newShiftManager.Re
                }

            }
            foreach (Control c in PastStaffLevelLabels)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }
            foreach (Control c in DiningAreaRBs)
            {
                flowDiningAreaAssignment.Controls.Add(((Control)c));
            }
            foreach (Control c in ServerCountLabels)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }
            foreach (Control c in ServerMaxLabels)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
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
        private void AdjustServerLists(Server server, List<Server> listToRemoveFrom, List<Server> listToAddTo, FlowLayoutPanel flowToRemoveFrom, FlowLayoutPanel flowToAddTo)
        {

        }
        private void PopulateServersNotOnShift(List<Server> servers)
        {
            servers = servers.OrderBy(s => s.Name).ToList();
            flowAllServers.Controls.Clear();
            foreach (Server server in servers)
            {
                server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
                //ServerControl serverControl = new ServerControl(server, 155, 20);
                //serverControl.Label.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                //serverControl.Click += serverControl_Click_AddToShift;
                Button ServerButton = CreateServerButton(server);
                // ServerButton.Click -= MoveFromFloorplanServerButton_Click;
                ServerButton.Click += AddToShift_Click;
                ServerButton.Enabled = false;
                //serverControl.HideShifts();
                flowAllServers.Controls.Add(ServerButton);
            }
        }
        private void RemoveServersOnShift(List<Server> servers)
        {


        }
        private void AddToShift_Click(object sender, EventArgs e)
        {
            //ServerControl oldServerControl = (ServerControl)sender;
            Button serverButton = (Button)sender;
            //Server server = oldServerControl.Server;
            Server server = (Server)serverButton.Tag;

            newShiftManager.UnassignedServers.Add(server);
            newShiftManager.ServersNotOnShift.Remove(server);
            //Button serverButton = CreateServerButton(server);
            ServerControl newServerControl = new ServerControl(server, 350, 30);

            newServerControl.Click += ServerControl_Click;

            newServerControl.AddRemoveButton(flowUnassignedServers, flowAllServers, newShiftManager.UnassignedServers, newShiftManager.ServersNotOnShift, 155, 20);
            newServerControl.RemoveButton.Click += serverControl_Click_RemoveFromShift;
            ImageSetter.SetShiftControlImages(newServerControl);
            flowUnassignedServers.Controls.Add(newServerControl);
            //flowAllServers.Controls.Remove(oldServerControl);
            flowAllServers.Controls.Remove(serverButton);

        }

        private void serverControl_Click_RemoveFromShift(object sender, EventArgs e)
        {
            Button removeButton = (Button)sender;

            // Get the ServerControl instance.
            ServerControl oldServerControl = (ServerControl)removeButton.Parent.Parent;

            Server server = oldServerControl.Server;

            newShiftManager.ServersNotOnShift.Add(server);
            newShiftManager.UnassignedServers.Remove(server);
            Button serverControl = CreateServerButton(server);
            serverControl.Click += AddToShift_Click;
            //serverControl.HideShifts();
            flowAllServers.Controls.Add(serverControl);
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
                BackColor = AppColors.ButtonColor,
                ForeColor = Color.Black,
                TabStop = false,
                Tag = server,
                Dock = DockStyle.Top
            };

            //b.Click += ServerButton_Click;
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



            if (cbUnassignedServers.Checked)
            {
                newShiftManager.UnassignedServers.Add(server);
                ServerControl serverControl = new ServerControl(server, 350, 30);
                serverControl.Click += ServerControl_Click;
                ImageSetter.SetShiftControlImages(serverControl);
                flowUnassignedServers.Controls.Add(serverControl);

            }
            else
            {
                newShiftManager.SelectedFloorplan.AddServerAndSection(server);
            }
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

                //newShiftManager.SelectedFloorplan.AddServerAndSection(server);
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
            if (cbUnassignedServers.Checked)
            {
                newShiftManager.UnassignedServers.Add(server);

                //Button serverButton = CreateServerButton(server);
                ServerControl newServerControl = new ServerControl(server, 350, 30);

                newServerControl.Click += ServerControl_Click;

                newServerControl.AddRemoveButton(flowUnassignedServers, flowAllServers, newShiftManager.UnassignedServers, newShiftManager.ServersNotOnShift, 155, 20);
                newServerControl.RemoveButton.Click += serverControl_Click_RemoveFromShift;
                ImageSetter.SetShiftControlImages(newServerControl);
                flowUnassignedServers.Controls.Add(newServerControl);
            }
            else
            {
                newShiftManager.SelectedFloorplan.AddServerAndSection(server);
            }
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
            //this.DialogResult = DialogResult.OK;
            //this.Close();

        }




        private void RefreshFloorplanCountLabels()
        {
            //foreach (Control c in flowDiningAreaAssignment.Controls)
            foreach (Control c in ServerCountLabels)
            {
                if (c is Label fpCountLabel)
                {
                    if (fpCountLabel.Tag is Floorplan fp)
                    {
                        fpCountLabel.Text = "Servers: " + fp.Servers.Count.ToString();
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
                            fpMaxLabel.Text = "Max: " + (fp.DiningArea.GetMaxCovers() / fp.Servers.Count).ToString("F1")
                                + "  Avg: " + (fp.DiningArea.GetAverageCovers() / fp.Servers.Count).ToString("F1");
                        }
                        else
                        {
                            fpMaxLabel.Text = "Max: " + fp.DiningArea.GetMaxCovers().ToString() + "  Avg: " + fp.DiningArea.GetAverageCovers().ToString();
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
                cbUnassignedServers.Checked = false;
            }
        }

        private void cbUnassignedServers_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUnassignedServers.Checked)
            {
                foreach (Control c in flowDiningAreaAssignment.Controls)
                {
                    if (c is RadioButton rb)
                    {
                        rb.Checked = false;
                    }
                }
                newShiftManager.SelectedFloorplan = null;
            }


        }

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
            //lblLastWeekDay.Text = "Last " + dateSelected.ToString("dddd") + ":";
            setIsNewShiftBool();
            RefreshPreviousFloorplanCounts();
            SetFloorplansForShiftManager();
            SetEnableStatusOfDiningAreaButtons();
        }

        private void btnDateDown_Click(object sender, EventArgs e)
        {
            dateSelected = dateSelected.AddDays(-1);
            lblShiftDate.Text = dateSelected.ToString("dddd, MMMM dd");
            //lblLastWeekDay.Text = "Last " + dateSelected.ToString("dddd") + ":";
            setIsNewShiftBool();
            RefreshPreviousFloorplanCounts();
            SetFloorplansForShiftManager();
            SetEnableStatusOfDiningAreaButtons();
        }
        private void UncheckDiningAreas()
        {
            foreach (Control c in flowDiningAreas.Controls)
            {
                if (c is CheckBox checkBox)
                {
                    checkBox.Checked = false;
                }
            }
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
            //lblLastWeekDay.Text = "Last " + dateSelected.ToString("dddd") + ":";
            cbIsAM.Checked = newShiftManager.IsAM;
            setIsNewShiftBool();
            RefreshPreviousFloorplanCounts();
            SetFloorplansForShiftManager();
            //PopulateServersNotOnShift(newShiftManager.ServersNotOnShift);
            PopulateUnassignedServers();
            SetEnableStatusOfDiningAreaButtons();
            btnCreateANewShift.Visible = false;
            UpdateCountLabels();

        }
        private void PopulateUnassignedServers()
        {
            foreach (var server in newShiftManager.UnassignedServers)
            {
                newShiftManager.ServersNotOnShift.Remove(server);
                ServerControl newServerControl = new ServerControl(server, 350, 30);
                newServerControl.Click += ServerControl_Click;
                newServerControl.AddRemoveButton(flowUnassignedServers, flowAllServers, newShiftManager.UnassignedServers, newShiftManager.ServersNotOnShift, 155, 20);
                newServerControl.RemoveButton.Click += serverControl_Click_RemoveFromShift;
                ImageSetter.SetShiftControlImages(newServerControl);
                flowUnassignedServers.Controls.Add(newServerControl);
                //flowAllServers.Controls.Remove(oldServerControl);

            }
        }
    }
}
