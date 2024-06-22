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
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            SetColors();
            lblDate.Text = dateSelected.ToString("dddd, MMMM dd");
            LoadDiningAreas();
            RefreshPreviousFloorplanCounts();
            PopulateServers();
            //SetShiftManagerDateAndIsAM();
            RefreshForDateSelected();

        }
        //////private void UpdateAreasAndServersForDateAndShift()
        //////{
        //////    Dictionary<DiningArea, int> thisDaysFloorplans = ServersAssignedPreviousDay(allFloorplans, cbIsAm.Checked, 0);
        //////}
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {

            if (keyData == Keys.Enter)
            {
                AddFirstServerToShift();

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
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

                Button ServerButton = CreateNotOnShiftServerButton(server);
                //////ServerButton.Click += AddToShift_Click;


                flowAllServers.Controls.Add(ServerButton);
            }
        }
        private void PopulateServersOnShift(List<Server> servers)
        {
            //servers = servers.OrderByFirstLetter().ToList();
            flowServersOnShift.Controls.Clear();
            foreach (Server server in servers)
            {

                Button ServerButton = CreateOnShiftServerButton(server);
                //////ServerButton.Click += AddToShift_Click;

                //////ServerButton.Click += RemoveFromShift_Click;
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
            //ShiftManagerCreated.AddNewUnassignedServer(server);
            shiftManager.SelectedShift.AddNewUnassignedServer(server);
            //////ShiftManagerCreated.ServersNotOnShift.Remove(server);
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
        }
        private void RemoveFromShift_Click(object sender, EventArgs e)
        {
            Button serverButton = (Button)sender;
            Server server = (Server)serverButton.Tag;
            //ShiftManagerCreated.RemoveServerFromShift(server);
            shiftManager.SelectedShift.RemoveServerFromShift(server);
            //////ShiftManagerCreated.UnassignedServers.Remove(server);
            serverButton.Click += AddToShift_Click;
            serverButton.Click -= RemoveFromShift_Click;
            serverButton.BackColor = UITheme.CTAColor;
            serverButton.ForeColor = Color.White;
            flowServersOnShift.Controls.Remove(serverButton);
            flowAllServers.Controls.Add(serverButton);
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

            Dictionary<DiningArea, int> lastWeekFloorplans = ServersAssignedPreviousDay(allFloorplans, cbIsAm.Checked, -7);
            Dictionary<DiningArea, int> yesterdayFloorplans = ServersAssignedPreviousDay(allFloorplans, cbIsAm.Checked, -1);


            UpdateLabels(flowLastWeekdayCounts, lastWeekFloorplans);


            UpdateLabels(flowYesterdayCounts, yesterdayFloorplans);
        }

        private void UpdateLabels(FlowLayoutPanel panel, Dictionary<DiningArea, int> floorplanCounts)
        {
            foreach (Label lbl in panel.Controls.OfType<Label>())
            {
                if (lbl.Tag is DiningArea area && floorplanCounts.TryGetValue(area, out int count))
                {
                    lbl.Text = $"{count}";
                    if (count > 0)
                    {
                        lbl.BackColor = UITheme.YesColor;
                        lbl.ForeColor = Color.Black;
                    }
                    else
                    {
                        lbl.BackColor = Color.Gray;
                        lbl.ForeColor = Color.LightGray;
                    }

                }
            }
        }


        private void CreateLabel(FlowLayoutPanel panel, string areaName, int count)
        {
            int width = (flowDiningAreas.Width / (DiningAreaManager.DiningAreas.Count)) - 20;
            Label lbl = new Label
            {
                Font = new Font("Segoe UI", 12f, FontStyle.Bold),
                Text = $"{count} Servers",
                AutoSize = false,

                Size = new Size(width, 35),
                Margin = new Padding(10, 10, 10, 10),
                BackColor = UITheme.ButtonColor,
                TextAlign = ContentAlignment.MiddleCenter

            };
            panel.Controls.Add(lbl);
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

            //////if (ShiftManagerCreated.DateOnly != dateOnlySelected || ShiftManagerCreated.IsAM != cbIsAm.Checked)
            //////{
            //////    //SetToShiftFromDatabase();
            //////}
            //////else
            //////{
            //////    SetToNewShift();
            //////}


        }
        //private void SetToNewShift()
        //{


        //    flowServersOnShift.Controls.Clear();
        //    PopulateServers();
        //    //////List<Button> serversOnShiftButtons = new List<Button>();
        //    List<Button> serversNotOnShiftButtons = new List<Button>();
        //    List<Floorplan> shiftFloorplans = new List<Floorplan>();
        //    shiftFloorplans.AddRange(ShiftManagerCreated.Floorplans);
        //    List<DiningArea> diningAreasUsed = new List<DiningArea>();
        //    diningAreasUsed.AddRange(ShiftManagerCreated.DiningAreasUsed);
        //    List<Server> serverUsed = new List<Server>();
        //    serverUsed.AddRange(ShiftManagerCreated.ServersOnShift);

        //    foreach (CheckBox cb in flowDiningAreas.Controls)
        //    {
        //        if (diningAreasUsed.Contains(cb.Tag))
        //        {
        //            cb.Checked = true;
        //        }
        //        else
        //        {
        //            cb.Checked = false;
        //        }
        //    }
        //    foreach (Button btn in flowAllServers.Controls)
        //    {
        //        serversNotOnShiftButtons.Add(btn);

        //    }
        //    foreach (Button btn in serversNotOnShiftButtons)
        //    {
        //        if (serverUsed.Contains(btn.Tag))
        //        {
        //            EventArgs e = new EventArgs();
        //            AddToShift_Click(btn, e);
        //        }
        //    }
        //}
        private void SetToShiftFromDatabase()
        {

            //ShiftManagerCreated.ClearFloorplans();
            //ShiftManagerCreated.UnassignedServers.Clear();
            shiftManager.SetSelectedShift(dateOnlySelected, cbIsAm.Checked);
            flowServersOnShift.Controls.Clear();
            PopulateServers();
            //////List<Button> serversOnShiftButtons = new List<Button>();
            List<Button> serversNotOnShiftButtons = new List<Button>();
            //var relevantFloorplans = allFloorplans
            //    .Where(fp => fp.Date.Date == dateSelected && fp.IsLunch == cbIsAm.Checked)
            //    .ToList();

            List<DiningArea> diningAreasUsed = new List<DiningArea>();
            List<Server> serverUsed = new List<Server>();

            foreach (Floorplan fp in shiftManager.SelectedShift.Floorplans)
            {
                diningAreasUsed.Add(fp.DiningArea);
                serverUsed.AddRange(fp.Servers);
                //ShiftManagerCreated.AddFloorplanToShift(fp);
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
        private Dictionary<DiningArea, int> ServersAssignedPreviousDay(List<Floorplan> floorplans, bool isLunch, int Days)
        {
            DateTime targetDate = dateSelected.AddDays(Days);
            var relevantFloorplans = floorplans
                .Where(fp => fp.Date.Date == targetDate.Date && fp.IsLunch == isLunch)
                .ToList();

            var result = new Dictionary<DiningArea, int>();


            foreach (var area in DiningAreaManager.DiningAreas)
            {
                result[area] = 0;
            }


            foreach (var fp in relevantFloorplans)
            {
                result[fp.DiningArea] += fp.Servers.Count;
            }

            return result;

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
                var floorplanToRemove = shiftManager.SelectedShift.Floorplans.FirstOrDefault(fp => fp.DiningArea == area);
                shiftManager.SelectedShift.DiningAreasUsed.Remove(area);
                shiftManager.SelectedShift.RemoveFloorplan(floorplanToRemove);

            }

        }
        private void btnBackDay_Click(object sender, EventArgs e)
        {
            dateSelected = dateSelected.AddDays(-1);
            lblDate.Text = dateSelected.ToString("dddd, MMMM dd");
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
            RefreshPreviousFloorplanCounts();
            RefreshForDateSelected();
        }

        private void btnForwardDay_Click(object sender, EventArgs e)
        {
            dateSelected = dateSelected.AddDays(1);
            lblDate.Text = dateSelected.ToString("dddd, MMMM dd");
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
            RefreshPreviousFloorplanCounts();
            RefreshForDateSelected();
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
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
            //ShiftManagerCreated.RemoveAssignedServers();
            //SetShiftManagerDateAndIsAM();
            //****frmEditStaff.UpdateNewShift(ShiftManagerCreated);
            shiftManager.SetNewShiftToSelectedShift();
            frmEditStaff.UpdateNewShift(shiftManager);
            this.Close();

        }
        //private void SetShiftManagerDateAndIsAM()
        //{
        //    this.ShiftManagerCreated.DateOnly = new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);
        //    this.ShiftManagerCreated.IsAM = isAM;
        //}

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


        }
        private void FilterServers(string searchText)
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filteredServers = shiftManager.SelectedShift.ServersNotOnShift
                    .Where(server => server.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase) ||
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
            List<int> controlIndex = new List<int>();
            Random random = new Random();

            for (int i = 0; i < 3; i++)
            {
                int index;
                do
                {
                    index = random.Next(0, flowAllServers.Controls.Count);
                } while (controlIndex.Contains(index));

                Button btn = (Button)flowAllServers.Controls[index];
                btn.PerformClick();
            }


        }

        private void flowServersOnShift_ControlsChanged(object sender, ControlEventArgs e)
        {
            lblServersOnShift.Text = $"{shiftManager.SelectedShift.ServersOnShift.Count} Servers On Shift";
        }

        private void btnAddBartender_Click(object sender, EventArgs e)
        {
            int bartenderCount = Int32.Parse(lblBartenderCount.Text);
            bartenderCount++;
            lblBartenderCount.Text = bartenderCount.ToString();
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
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                string defaultDirectory = @"C:\Users\Jason\OneDrive\Working On Now\misc";
                string fallbackDirectory = @"C:\";

                // Check if the default directory exists
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
                        //string s = GetTestRecordData(records);

                        this.Invoke(new Action(() =>
                        {
                            
                            loadingForm.Close();
                            this.Enabled = true;
                            PopulateServers();
                            //textBox1.Text = s;

                        }));
                    });

                }

            }
        }

        private void PopulateServersFromCsv(List<ScheduledShift> records)
        {
            ScheduledShift scheduledShift = records.FirstOrDefault(s => s.Date == shiftManager.DateOnly && s.IsAm == shiftManager.IsAM);
            List<Server> scheduledServers = scheduledShift.GetServersFromRecord(shiftManager.SelectedShift.AllServers);
            foreach (Server server in scheduledServers)
            {
                shiftManager.SelectedShift.AddNewUnassignedServer(server);
            }
           
        }
    }
}
