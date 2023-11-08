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
            btnBackDay.BackColor = AppColors.ButtonColor;
            btnForwardDay.BackColor = AppColors.ButtonColor;
            cbIsAm.BackColor = AppColors.ButtonColor;

            btnOK.BackColor = AppColors.CTAColor;

        }
        private void frmNewShiftDatePicker_Load(object sender, EventArgs e)
        {
            SetColors();
            lblDate.Text = dateSelected.ToString("dddd, MMMM dd");
            LoadDiningAreas();
            RefreshPreviousFloorplanCounts();
            PopulateServersNotOnShift(allServers);
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
                BackColor = AppColors.ButtonColor,
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
                    BackColor = AppColors.ButtonColor,
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
                    lbl.Text = $"{count} Servers"; // Update the text of the label
                    if (count > 0)
                    {
                        lbl.BackColor = AppColors.ButtonColor;
                    }
                    else
                    {
                        lbl.BackColor = Color.Gray;
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
                BackColor = AppColors.ButtonColor,
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
                BackColor = AppColors.ButtonColor,
                TextAlign = ContentAlignment.MiddleCenter

            };
            panel.Controls.Add(lbl);
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
                if (!ShiftManagerCreated.DiningAreasUsed.Contains(area))
                {
                    ShiftManagerCreated.CreateFloorplanForDiningArea(area, DateTime.Now, false, 0, 0);
                }


            }
            else
            {
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
        }



        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

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
