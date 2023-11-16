using FloorplanClassLibrary;
using FloorPlanMaker;
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

namespace FloorPlanMakerUI
{
    public partial class frmNewShiftDatePicker : Form
    {
        private DateTime dateSelected = DateTime.Today;
        private bool isAM = true;
        public ShiftManager ShiftManagerCreated = new ShiftManager();
        private DiningAreaCreationManager DiningAreaManager = new DiningAreaCreationManager();
        private List<Floorplan> allFloorplans = new List<Floorplan>();
        private List<Server> allServers = new List<Server>();
        private frmEditStaff frmEditStaff { get; set; }
        public frmNewShiftDatePicker(DiningAreaCreationManager diningAreaManager, List<Floorplan> allFloorplans, List<Server> allServers, frmEditStaff frmEditStaff)
        {
            InitializeComponent();
            DiningAreaManager = diningAreaManager;
            this.allFloorplans = allFloorplans;
            this.allServers = allServers;
            this.frmEditStaff = frmEditStaff;
        }
        private void SetColors()
        {
            UITheme.FormatSecondColor(panel1);
            UITheme.FormatSecondColor(panel2);
            UITheme.FormatSecondColor(panel3);
            UITheme.FormatSecondColor(panel4);

            UITheme.FormatMainButton(btnBackDay);
            UITheme.FormatMainButton(btnForwardDay);
            UITheme.FormatMainButton(cbIsAm);

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
            PopulateServersNotOnShift(allServers);

        }
        private void UpdateAreasAndServersForDateAndShift()
        {
            Dictionary<DiningArea, int> thisDaysFloorplans = ServersAssignedPreviousDay(allFloorplans, cbIsAm.Checked, 0);
        }
        private void PopulateServersNotOnShift(List<Server> servers)
        {
            servers = servers.OrderBy(s => s.Name).ToList();
            flowAllServers.Controls.Clear();
            foreach (Server server in servers)
            {
                server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
                Button ServerButton = CreateServerButton(server);
                //ServerButton.Click += AddToShift_Click;

                ServerButton.Click += AddToShift_Click;
                flowAllServers.Controls.Add(ServerButton);
            }
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

            };

            //b.Click += ServerButton_Click;
            return b;
        }
        private void AddToShift_Click(object sender, EventArgs e)
        {
            Button serverButton = (Button)sender;
            Server server = (Server)serverButton.Tag;
            ShiftManagerCreated.UnassignedServers.Add(server);
            ShiftManagerCreated.ServersNotOnShift.Remove(server);
            serverButton.Click -= AddToShift_Click;
            serverButton.Click += RemoveFromShift_Click;
            serverButton.BackColor = Color.FromArgb(192, 255, 192);
            flowServersOnShift.Controls.Add(serverButton);
            flowAllServers.Controls.Remove(serverButton);
        }
        private void RemoveFromShift_Click(object sender, EventArgs e)
        {
            Button serverButton = (Button)sender;
            Server server = (Server)serverButton.Tag;
            ShiftManagerCreated.ServersNotOnShift.Add(server);
            ShiftManagerCreated.UnassignedServers.Remove(server);
            serverButton.Click += AddToShift_Click;
            serverButton.Click -= RemoveFromShift_Click;
            serverButton.BackColor = UITheme.ButtonColor;
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
                    ForeColor = Color.Black,
                    BackColor = UITheme.ButtonColor,
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
            // Obtain the counts for last week and yesterday
            Dictionary<DiningArea, int> lastWeekFloorplans = ServersAssignedPreviousDay(allFloorplans, cbIsAm.Checked, -7);
            Dictionary<DiningArea, int> yesterdayFloorplans = ServersAssignedPreviousDay(allFloorplans, cbIsAm.Checked, -1);

            // Update labels for last week
            UpdateLabels(flowLastWeekdayCounts, lastWeekFloorplans);

            // Update labels for yesterday
            UpdateLabels(flowYesterdayCounts, yesterdayFloorplans);
        }

        private void UpdateLabels(FlowLayoutPanel panel, Dictionary<DiningArea, int> floorplanCounts)
        {
            foreach (Label lbl in panel.Controls.OfType<Label>())
            {
                if (lbl.Tag is DiningArea area && floorplanCounts.TryGetValue(area, out int count))
                {
                    lbl.Text = $"{count}"; // Update the text of the label
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
           
            var relevantFloorplans = allFloorplans
                .Where(fp => fp.Date.Date == dateSelected && fp.IsLunch == cbIsAm.Checked)
                .ToList();
            List<DiningArea> diningAreasUsed = new List<DiningArea>();
            List<Server> serverUsed = new List<Server>();
          
            foreach (var fp in relevantFloorplans)
            {
                diningAreasUsed.Add(fp.DiningArea);
                serverUsed.AddRange(fp.Servers);
            }
            foreach(CheckBox cb in flowDiningAreas.Controls)
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
            foreach(Button btn in flowAllServers.Controls)
            {
                
                if (serverUsed.Contains(btn.Tag))
                {
                    EventArgs e = new EventArgs();
                    AddToShift_Click(btn, e); 
                   
                }
            }
            foreach (Button btn in flowServersOnShift.Controls)
            {
                if (!serverUsed.Contains(btn.Tag))
                {
                    EventArgs e = new EventArgs(); 
                    RemoveFromShift_Click(btn, e); 
                   
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


        private void cbDiningArea_CheckChanged(object sender, EventArgs e)
        {
            CheckBox cbArea = sender as CheckBox;
            DiningArea area = (DiningArea)cbArea.Tag;


            if (cbArea.Checked)
            {
                cbArea.BackColor = Color.FromArgb(192, 255, 192);
                if (!ShiftManagerCreated.DiningAreasUsed.Contains(area))
                {
                    ShiftManagerCreated.CreateFloorplanForDiningArea(area, DateTime.Now, false, 0, 0);
                }


            }
            else
            {
                cbArea.BackColor = UITheme.ButtonColor;
                var floorplanToRemove = ShiftManagerCreated.Floorplans.FirstOrDefault(fp => fp.DiningArea == area);
                ShiftManagerCreated.DiningAreasUsed.Remove(area);
                ShiftManagerCreated.RemoveFloorplan(floorplanToRemove);

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
            if (ShiftManagerCreated.Floorplans.Count == 0)
            {
                MessageBox.Show("You have not choosen any dining areas for this shift");
                return;
            }
            if (ShiftManagerCreated.ServersOnShift.Count == 0)
            {
                MessageBox.Show("You have not assigned any servers");
                return;
            }
            this.ShiftManagerCreated.DateOnly = new DateOnly(dateSelected.Year, dateSelected.Month, dateSelected.Day);
            this.ShiftManagerCreated.IsAM = isAM;
            frmEditStaff.UpdateNewShift(ShiftManagerCreated);
            this.Close();

        }

        private void cbIsAm_CheckedChanged(object sender, EventArgs e)
        {
            isAM = cbIsAm.Checked;
            if (isAM)
            {
                cbIsAm.Text = "AM";
            }
            else
            {
                cbIsAm.Text = "PM";
            }
            RefreshPreviousFloorplanCounts();
        }
    }
}
