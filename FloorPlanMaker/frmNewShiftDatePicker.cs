using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorPlanMakerUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace FloorPlanMakerUI
{
    public partial class frmNewShiftDatePicker : Form
    {
        private DateTime dateSelected = DateTime.Today;
        private DateOnly dateOnlySelected => new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);

        private bool isAM { get { return cbIsAm.Checked; } }

        //private Shift ShiftManagerCreated = new Shift();
        private ShiftManager shiftManager;
        private DiningAreaManager DiningAreaManager = new DiningAreaManager();
        private List<Floorplan> allFloorplans = new List<Floorplan>();

        private frmEditStaff frmEditStaff { get; set; }
        public frmNewShiftDatePicker(DiningAreaManager diningAreaManager, List<Floorplan> allFloorplans,
            List<Server> allServers, frmEditStaff frmEditStaff, Shift shiftManager)
        {
            InitializeComponent();
            DiningAreaManager = diningAreaManager;
            this.allFloorplans = allFloorplans;
            this.frmEditStaff = frmEditStaff;
            //this.ShiftManagerCreated = shiftManager;
            this.dateSelected = shiftManager.DateOnly.ToDateTime(TimeOnly.MinValue);
            //cbIsAm.Checked = shiftManager.IsAM;
        }
        public frmNewShiftDatePicker(DiningAreaManager diningAreaManager, List<Floorplan> allFloorplans,
           frmEditStaff frmEditStaff, DateTime date, bool isAm, ShiftManager shiftManager)
        {
            InitializeComponent();

            DiningAreaManager = diningAreaManager;
            this.allFloorplans = allFloorplans;
            this.shiftManager = shiftManager;
            this.frmEditStaff = frmEditStaff;
            this.dateSelected = date;
            this.shiftManager.SelectedShift.SetBartendersToShift(shiftManager.SelectedShift.BartenderCount);
            cbIsAm.Checked = isAm;

        }
        //TODO: when reopening after closing, the floorplans are reset, one that was just created is gone
        private void SetColors()
        {
            UITheme.FormatSecondColor(panel1);
            UITheme.FormatSecondColor(panel2);
            UITheme.FormatSecondColor(panel3);
            UITheme.FormatSecondColor(panel4);

            UITheme.FormatMainButton(btnBackDay);
            UITheme.FormatMainButton(btnForwardDay);

            UITheme.FormatCTAButton(btnOK);

            UITheme.FormatCanvasColor(flowAllServers);
            UITheme.FormatCanvasColor(flowDiningAreas);
            UITheme.FormatCanvasColor(flowLastWeekdayCounts);
            UITheme.FormatCanvasColor(flowServersOnShift);
            UITheme.FormatCanvasColor(flowYesterdayCounts);

        }
        private void frmNewShiftDatePicker_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.TimeOfDay > new TimeSpan(11, 0, 0))
            {
                cbIsAm.Checked = false;
            }
            SetColors();
            GetDateString();
            GetTodayLabel();
            LoadDiningAreas();
            RefreshPreviousFloorplanCounts();
            PopulateServers();
            RefreshForDateSelected();
            txtServerSearch.Focus();

        }
        private void GetDateString()
        {
            string shiftType = "";
            if (shiftManager.SelectedShift.IsAM)
            {
                shiftType = "Lunch";
            }
            else
            {
                shiftType = "Dinner";
            }
            string dateLabel = shiftType + ": " + dateSelected.ToString("dddd, MMMM dd");
            lblDate.Text = dateLabel;
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Shift)
            {
                txtServerSearch.Focus();
                return true;
            }
            if (keyData == Keys.Enter)
            {
                if (Control.ModifierKeys == Keys.Shift && this.Visible)
                {
                    if (shiftManager.SelectedShift.ServersOnShift.Count == 0)
                    {
                        btnImportServers.PerformClick();
                    }
                    else
                    {
                        btnOK.PerformClick();
                    }

                }
                else
                {

                    AddFirstServerToShift();
                }
                return true;
            }
            if (keyData >= Keys.D1 && keyData <= Keys.D9)
            {
                int numberPressed = keyData - Keys.D0;
                HandleNumericKeyPress(numberPressed);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void TxtServerSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                if (Control.ModifierKeys == Keys.Shift)
                {
                    if (shiftManager.SelectedShift.ServersOnShift.Count == 0)
                    {
                        btnImportServers.PerformClick();
                    }
                    else
                    {
                        btnOK.PerformClick();
                    }
                }
                else
                {
                    AddFirstServerToShift();
                }
            }
            else if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;

                int numberPressed = e.KeyCode - Keys.D0;
                HandleNumericKeyPress(numberPressed);
            }

        }

        private void HandleNumericKeyPress(int numberPressed)
        {
            if (numberPressed <= flowDiningAreas.Controls.Count)
            {
                Control c = flowDiningAreas.Controls[numberPressed - 1];
                CheckBox cbArea = (CheckBox)c;
                cbArea.Checked = !cbArea.Checked;
            }
        }

        private void PopulateServers()
        {
            PopulateNotOnServers(shiftManager.SelectedShift.ServersNotOnShift);
            PopulateServersOnShift(shiftManager.SelectedShift.ServersOnShift);
        }
        private void PopulateNotOnServers(List<Server> servers)
        {
            servers = servers.OrderByFirstLetter().ToList();
            flowAllServers.Controls.Clear();
            foreach (Server server in servers)
            {
                if (server.IsBartender) { continue; }
                Button ServerButton = CreateNotOnShiftServerButton(server);

                flowAllServers.Controls.Add(ServerButton);
            }
        }
        private void PopulateServersOnShift(List<Server> servers)
        {
            flowServersOnShift.Controls.Clear();
            foreach (Server server in servers)
            {
                Button ServerButton = CreateOnShiftServerButton(server);
                flowServersOnShift.Controls.Add(ServerButton);
            }
        }
        private Button CreateNotOnShiftServerButton(Server server)
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
                BackColor = UITheme.CTAColor,
                ForeColor = Color.White,
                Font = UITheme.MainFont,
                TabStop = false,
                Tag = server,
            };

            b.Click += AddToShift_Click;
            return b;
        }
        private Button CreateOnShiftServerButton(Server server)
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
                BackColor = UITheme.YesColor,
                ForeColor = Color.Black,
                Font = UITheme.MainFont,
                TabStop = false,
                Tag = server,

            };
            b.Click += RemoveFromShift_Click;

            return b;
        }
        private void AddToShift_Click(object sender, EventArgs e)
        {
            Button serverButton = (Button)sender;
            Server server = (Server)serverButton.Tag;
            shiftManager.SelectedShift.AddNewUnassignedServer(server);
            serverButton.Click -= AddToShift_Click;
            serverButton.Click += RemoveFromShift_Click;
            serverButton.BackColor = UITheme.YesColor;
            serverButton.ForeColor = Color.Black;
            flowServersOnShift.Controls.Add(serverButton);
            flowAllServers.Controls.Remove(serverButton);
            if (txtServerSearch.Text.Length > 0)
            {
                txtServerSearch.Clear();
                txtServerSearch.Focus();
            }
            txtServerSearch.Focus();
        }
        private void RemoveFromShift_Click(object sender, EventArgs e)
        {
            Button serverButton = (Button)sender;
            Server server = (Server)serverButton.Tag;
            shiftManager.SelectedShift.RemoveServerFromShift(server);
            serverButton.Click += AddToShift_Click;
            serverButton.Click -= RemoveFromShift_Click;
            serverButton.BackColor = UITheme.CTAColor;
            serverButton.ForeColor = Color.White;
            flowServersOnShift.Controls.Remove(serverButton);
            flowAllServers.Controls.Add(serverButton);
            txtServerSearch.Focus();
        }
        private void LoadDiningAreas()
        {
            int width = (flowDiningAreas.Width / (DiningAreaManager.DiningAreas.Count)) - 20;
            foreach (DiningArea area in DiningAreaManager.DiningAreas)
            {
                CheckBox btnDining = new CheckBox
                {
                    Text = area.Name,
                    Tag = area,
                    Size = new Size(width, 30),
                    Margin = new Padding(10, 10, 10, 10),
                    Appearance = Appearance.Button,
                    TextAlign = ContentAlignment.MiddleCenter,
                    FlatStyle = FlatStyle.Flat,
                    ForeColor = Color.White,
                    BackColor = UITheme.CTAColor,
                    Font = UITheme.SmallerFont,
                    TabStop = false

                };
                btnDining.CheckedChanged += cbDiningArea_CheckChanged;
                flowDiningAreas.Controls.Add(btnDining);
                CreateCountLabel(flowYesterdayCounts, area);
                CreateCountLabel(flowLastWeekdayCounts, area);
            }
        }
        private void RefreshPreviousFloorplanCounts()
        {
            List<AreaHistory> lastWeekAreaHistories = new List<AreaHistory>();
            if (cbStatsType.Checked)
            {
                lastWeekAreaHistories = GetHistoryForLastFour();
            }
            else
            {
                lastWeekAreaHistories = GetAreaHistories(cbIsAm.Checked, -7);
            }
            List<AreaHistory> yesterdayAreaHistories = GetAreaHistories(cbIsAm.Checked, -1);

            CreateAreaHistoryLabels(flowLastWeekdayCounts, lastWeekAreaHistories);
            CreateAreaHistoryLabels(flowYesterdayCounts, yesterdayAreaHistories);
        }
        private List<AreaHistory> GetHistoryForLastFour()
        {
            List<AreaHistory> areaHistories = new List<AreaHistory>();
            foreach (DiningArea area in this.DiningAreaManager.DiningAreas)
            {
                AreaHistory history = new AreaHistory(area, dateOnlySelected, cbIsAm.Checked);
                history.SetDatesToLastFourWeekdays();
                areaHistories.Add(history);
            }
            return areaHistories;
        }
        private List<AreaHistory> GetAreaHistories(bool isAm, int v)
        {
            List<AreaHistory> areaHistories = new List<AreaHistory>();
            foreach (DiningArea area in this.DiningAreaManager.DiningAreas)
            {
                areaHistories.Add(new AreaHistory(area, dateOnlySelected.AddDays(v), isAm));
            }
            return areaHistories;
        }
        private void CreateAreaHistoryLabels(FlowLayoutPanel panel, List<AreaHistory> areaHistories)
        {
            if (cbStatsType.Checked)
            {
                foreach (AreaHistory areaHistory in areaHistories)
                {
                    areaHistory.SetDatesToLastFourWeekdays();
                }
            }
            foreach (Label lbl in panel.Controls.OfType<Label>())
            {
                if (lbl.Tag is DiningArea area)
                {
                    AreaHistory history = areaHistories.FirstOrDefault(x => x.DiningArea == area);
                    if (history == null)
                    {
                        lbl.BackColor = Color.Gray;
                        lbl.ForeColor = Color.LightGray;
                        lbl.Text = "";
                    }
                    else if (history.Sales == 0f)
                    {
                        lbl.BackColor = Color.Gray;
                        lbl.ForeColor = Color.LightGray;
                        if (history.ServerCount > 0)
                        {
                            lbl.Text = "|" + history.ServerCount.ToString() + "| " + "  ?";
                        }
                        else
                        {
                            lbl.Text = "";
                        }

                    }
                    else if (history.Sales > 0f && history.Sales < 1000f)
                    {
                        lbl.BackColor = Color.Gray;
                        lbl.ForeColor = Color.LightGray;
                        lbl.Text = "|" + history.ServerCount.ToString() + "| " + history.Sales.ToString("C0");
                    }
                    else
                    {
                        lbl.BackColor = UITheme.YesColor;
                        lbl.ForeColor = Color.Black;
                        lbl.Text = "|" + history.ServerCount.ToString() + "| " + history.Sales.ToString("C0");
                    }

                }
            }
        }
        private void CreateCountLabel(FlowLayoutPanel panel, DiningArea diningArea)
        {
            int width = (flowDiningAreas.Width / (DiningAreaManager.DiningAreas.Count)) - 20;
            Label lbl = new Label
            {
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                Text = diningArea.Name,
                AutoSize = false,
                Tag = diningArea,
                Size = new Size(width, 35),
                Margin = new Padding(10, 10, 10, 10),
                BackColor = UITheme.ButtonColor,
                TextAlign = ContentAlignment.MiddleCenter

            };
            panel.Controls.Add(lbl);
        }
        private void RefreshForDateSelected()
        {
            SetToShiftFromDatabase();

        }
        private void SetToShiftFromDatabase()
        {


            shiftManager.SetSelectedShift(dateOnlySelected, cbIsAm.Checked);
            flowServersOnShift.Controls.Clear();
            PopulateServers();
            List<Button> serversNotOnShiftButtons = new List<Button>();
            List<DiningArea> diningAreasUsed = new List<DiningArea>();
            List<Server> serverUsed = new List<Server>();

            foreach (Floorplan fp in shiftManager.SelectedShift.Floorplans)
            {
                diningAreasUsed.Add(fp.DiningArea);
                serverUsed.AddRange(fp.Servers);
            }
            foreach (CheckBox cb in flowDiningAreas.Controls)
            {
                if (diningAreasUsed.Contains(cb.Tag))
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                }
            }
            foreach (Button btn in flowAllServers.Controls)
            {
                serversNotOnShiftButtons.Add(btn);

            }
            foreach (Button btn in serversNotOnShiftButtons)
            {
                if (serverUsed.Contains(btn.Tag))
                {
                    EventArgs e = new EventArgs();
                    AddToShift_Click(btn, e);
                }
            }
        }
        private void cbDiningArea_CheckChanged(object sender, EventArgs e)
        {
            CheckBox cbArea = sender as CheckBox;
            DiningArea area = (DiningArea)cbArea.Tag;


            if (cbArea.Checked)
            {
                cbArea.BackColor = UITheme.YesColor;
                cbArea.ForeColor = Color.Black;
                //Changed here
                if (!shiftManager.SelectedShift.DiningAreasUsed.Contains(area))
                {
                    shiftManager.SelectedShift.CreateFloorplanForDiningArea(area, 0, 0);
                }
            }
            else
            {
                cbArea.BackColor = UITheme.CTAColor;
                cbArea.ForeColor = Color.White;
                var floorplanToRemove = shiftManager.SelectedShift.Floorplans.FirstOrDefault(fp => fp.DiningArea.ID == area.ID);
                shiftManager.SelectedShift.DiningAreasUsed.Remove(area);
                shiftManager.SelectedShift.RemoveFloorplan(floorplanToRemove);
            }
            txtServerSearch.Focus();
        }
        private void btnBackDay_Click(object sender, EventArgs e)
        {
            dateSelected = dateSelected.AddDays(-1);
            GetDateString();
            GetTodayLabel();
            RefreshPreviousFloorplanCounts();
            RefreshForDateSelected();
            txtServerSearch.Focus();
        }

        private void btnForwardDay_Click(object sender, EventArgs e)
        {
            dateSelected = dateSelected.AddDays(1);
            GetDateString();
            GetTodayLabel();
            RefreshPreviousFloorplanCounts();
            RefreshForDateSelected();
            txtServerSearch.Focus();
        }
        private void GetTodayLabel()
        {
            if (dateSelected.Date == DateTime.Today)
            {
                lblIsToday.Text = "(Today)";
            }
            else if (dateSelected.Date == DateTime.Today.AddDays(-1))
            {
                lblIsToday.Text = "(Yesterday)";
            }
            else if (dateSelected.Date == DateTime.Today.AddDays(1))
            {
                lblIsToday.Text = "(Tomorrow)";
            }
            else
            {
                lblIsToday.Text = "";
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            frmEditStaff.NotifyNewShiftClosed();
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (shiftManager.SelectedShift.Floorplans.Count == 0)
            {
                MessageBox.Show("You have not choosen any dining areas for this shift");
                return;
            }
            if (shiftManager.SelectedShift.ServersOnShift.Count == 0)
            {
                MessageBox.Show("You have not assigned any servers");
                return;
            }
            int bartenderCount = Int32.Parse(lblBartenderCount.Text);
            shiftManager.SelectedShift.SetBartendersToShift(bartenderCount);
            shiftManager.SetNewShiftToSelectedShift();
            frmEditStaff.UpdateNewShift(shiftManager);
            this.Close();

        }

        private void cbIsAm_CheckedChanged(object sender, EventArgs e)
        {

            if (isAM)
            {
                cbIsAm.Image = Resources.smallSunrise;
                cbIsAm.BackColor = Color.FromArgb(251, 175, 0);
            }
            else
            {
                cbIsAm.Image = Resources.smallMoon;
                cbIsAm.BackColor = Color.FromArgb(117, 70, 104);

            }
            RefreshPreviousFloorplanCounts();
            RefreshForDateSelected();
            txtServerSearch.Focus();
            GetDateString();
        }

        private void pbAddPerson_Click(object sender, EventArgs e)
        {
            frmAddServer addServerForm = new frmAddServer();
            addServerForm.StartPosition = FormStartPosition.CenterParent;
            DialogResult = addServerForm.ShowDialog();
            if (DialogResult == DialogResult.OK)
            {
                foreach (Server server in addServerForm.newServers)
                {
                    SqliteDataAccess.SaveNewServer(server);
                }
                shiftManager.SelectedShift.ReloadAllServerList();
                PopulateServers();
            }

            txtServerSearch.Focus();
        }
        private void FilterServers(string searchText)
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filteredServers = shiftManager.SelectedShift.ServersNotOnShift
                    .Where(server => server.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase) ||
                                        server.LastName.StartsWith(searchText, StringComparison.OrdinalIgnoreCase) ||
                                        (server.DisplayName != null && server.DisplayName.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)))
                    .OrderByFirstLetter()
                    .ToList();

                PopulateNotOnServers(filteredServers);
            }
            else
            {
                PopulateServers();
            }
        }
        private void txtServerSearch_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtServerSearch.Text;
            FilterServers(searchText);
        }
        private void AddFirstServerToShift()
        {
            if (flowAllServers.Controls.Count > 0)
            {
                Control ctr = flowAllServers.Controls[0];
                if (ctr is Button btn)
                {
                    btn.PerformClick();
                }
            }
        }
        private void btnImportServers_Click(object sender, EventArgs e)
        {
            Shift matchedShift = SqliteDataAccess.LoadShift(shiftManager.DateOnly.AddDays(-7), shiftManager.IsAM);
            shiftManager.SelectedShift.CopyServersAndDiningAreas(matchedShift);
            PopulateServers();
            foreach (CheckBox cb in flowDiningAreas.Controls)
            {
                if (shiftManager.SelectedShift.DiningAreasUsed.Contains(cb.Tag))
                {
                    cb.Checked = true;
                }
                else
                {
                    cb.Checked = false;
                }
            }


            txtServerSearch.Focus();
        }

        private void flowServersOnShift_ControlsChanged(object sender, ControlEventArgs e)
        {
            lblServersOnShift.Text = $"{shiftManager.SelectedShift.ServersOnShift.Count} Servers On Shift";
            lblBartenderCount.Text = shiftManager.SelectedShift.BartenderCount.ToString();
        }

        private void btnAddBartender_Click(object sender, EventArgs e)
        {
            int bartenderCount = Int32.Parse(lblBartenderCount.Text);
            if (bartenderCount == 6)
            {
                return;
            }
            bartenderCount++;
            lblBartenderCount.Text = bartenderCount.ToString();
            shiftManager.SelectedShift.SetBartendersToShift(bartenderCount);
            txtServerSearch.Focus();
        }
        private void btnSubtractBartender_Click(object sender, EventArgs e)
        {
            int bartenderCount = Int32.Parse(lblBartenderCount.Text);
            if (bartenderCount == 0)
            {
                return;
            }
            bartenderCount--;
            lblBartenderCount.Text = bartenderCount.ToString();
            shiftManager.SelectedShift.SetBartendersToShift(bartenderCount);
            txtServerSearch.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string defaultDirectory = @"C:\Users\Jason\OneDrive\Working On Now\misc";
                string fallbackDirectory = @"C:\";

                if (Directory.Exists(defaultDirectory))
                {
                    openFileDialog.InitialDirectory = defaultDirectory;
                }
                else
                {
                    openFileDialog.InitialDirectory = fallbackDirectory;
                }
                openFileDialog.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {

                    string filePath = openFileDialog.FileName;
                    frmLoading loadingForm = new frmLoading("Loading");
                    loadingForm.Show();
                    this.Enabled = false;

                    Task.Run(() =>
                    {
                        List<ScheduledShift> records = CsvScheduleReader.GetScheduledShifts(filePath);

                        records.OrderBy(r => r.Date).ToList();
                        PopulateServersFromCsv(records);

                        this.Invoke(new Action(() =>
                        {

                            loadingForm.Close();
                            this.Enabled = true;
                            PopulateServers();

                        }));
                    });

                }

            }
            txtServerSearch.Focus();
        }

        private void PopulateServersFromCsv(List<ScheduledShift> records)
        {
            ScheduledShift scheduledShift = records.FirstOrDefault(s => s.Date == shiftManager.DateOnly && s.IsAm == shiftManager.IsAM);
            if (scheduledShift == null)
            {
                MessageBox.Show("No shift was found in that file for the date selected");
                return;
            }
            try
            {
                List<Server> scheduledServers = scheduledShift.GetServersFromRecord(shiftManager.SelectedShift.AllServers);
                List<string> serversNotMatched = scheduledShift.GetMissedServerNames(shiftManager.SelectedShift.AllServers);
                foreach (Server server in scheduledServers)
                {
                    shiftManager.SelectedShift.AddNewUnassignedServer(server);
                }
                if (serversNotMatched.Count > 0)
                {
                    string serversMissed = "\n";
                    int i = 1;
                    foreach (string server in serversNotMatched)
                    {
                        serversMissed += i.ToString() + ") " + server + "\n";
                        i++;
                    }
                    MessageBox.Show("Could not find: " + serversMissed);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbStatsType_CheckedChanged(object sender, EventArgs e)
        {
            RefreshPreviousFloorplanCounts();
            if (cbStatsType.Checked)
            {
                cbStatsType.Text = "Last 4";
            }
            else
            {
                cbStatsType.Text = "Last Week";
            }
        }

        private void frmNewShiftDatePicker_Shown(object sender, EventArgs e)
        {
            txtServerSearch.Focus();
        }
    }
}
