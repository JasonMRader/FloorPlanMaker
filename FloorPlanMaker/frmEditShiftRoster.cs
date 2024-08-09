using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmEditShiftRoster : Form
    {
        private Shift shift { get; set; }
        private Floorplan secondaryFloorplan { get; set; }
        public frmEditShiftRoster(Shift shift)
        {
            InitializeComponent();
            this.shift = shift;
        }

        private void frmEditShiftRoster_Load(object sender, EventArgs e)
        {
            lblSelectedDiningArea.Text = shift.SelectedFloorplan.DiningArea.Name;
            PopulateSelectedFloorplanServerButtons();
            PopulateCboAreas();
            UITheme.FormatCTAButton(btnDone);
            btnDone.BackColor = UITheme.YesColor;
            btnDone.ForeColor = UITheme.YesFontColor;

        }
        private void PopulateCboAreas()
        {
            foreach (Floorplan floorplan in shift.Floorplans)
            {
                if (floorplan != shift.SelectedFloorplan)
                {
                    cboFloorplans.Items.Add(floorplan);
                    cboFloorplans.DisplayMember = floorplan.DiningArea.Name;
                }
            }
            if (cboFloorplans.Items.Count > 0)
            {
                cboFloorplans.SelectedIndex = 0;
            }


        }
        private void PopulateSelectedFloorplanServerButtons()
        {
            flowThisFloorplan.Controls.Clear();
            foreach (Server server in shift.SelectedFloorplan.Servers)
            {
                Button button = CreateServerButton(server);                
                button.Click += MoveServerToSecondaryFloorplan;
                flowThisFloorplan.Controls.Add(button);
            }
        }

        private void MoveServerToSecondaryFloorplan(object? sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Server server = (Server)btn.Tag;
            if (shift.SelectedFloorplan.Servers.Contains(server))
            {
                if (cbServersNotOnShift.Checked)
                {
                    shift.RemoveServerFromShift(server);
                    PopulateServersNotOnShiftServerButtons(shift.ServersNotOnShift);
                }
                else
                {
                    shift.SelectedFloorplan.RemoveServerAndSection(server);
                    secondaryFloorplan.AddServerAndSection(server);
                    PopulateOtherFloorplanServers();
                }

            }
            PopulateSelectedFloorplanServerButtons();

            //PopulateCboAreas();
        }

        private void PopulateSecondaryFloorplanServerButtons()
        {
            flowOtherServers.Controls.Clear();
            foreach (Server server in secondaryFloorplan.Servers)
            {
                Button button = CreateServerButton(server);
               
                button.Click += TakeServerFromSecondarySection;
                flowOtherServers.Controls.Add(button);
            }
        }

        private void TakeServerFromSecondarySection(object? sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Server server = (Server)btn.Tag;
            if (secondaryFloorplan.Servers.Contains(server))
            {
                secondaryFloorplan.RemoveServerAndSection(server);
                shift.SelectedFloorplan.AddServerAndSection(server);
            }
            PopulateSelectedFloorplanServerButtons();
            PopulateSecondaryFloorplanServerButtons();
            //PopulateCboAreas();
        }

        private Button CreateServerButton(Server server)
        {
            Button button = new Button();
            button.Text = server.ToString();
            button.Tag = server;
            UITheme.FormatCTAButton(button);
            button.Font = UITheme.MainFont;
            
            button.AutoSize = false;
            button.Size = new System.Drawing.Size(flowThisFloorplan.Width - 10, 30);
            button.TextAlign = ContentAlignment.MiddleCenter;

            return button;
        }

        private void cbServersNotOnShift_CheckedChanged(object sender, EventArgs e)
        {
            cboFloorplans.Enabled = !cbServersNotOnShift.Checked;
            txtSearchServers.Visible = cbServersNotOnShift.Checked;
            if (cbServersNotOnShift.Checked)
            {
                PopulateServersNotOnShiftServerButtons(shift.ServersNotOnShift);
                txtSearchServers.Focus();
                secondaryFloorplan = null;
            }
            else
            {
                PopulateOtherFloorplanServers();

            }
        }

        private void PopulateServersNotOnShiftServerButtons(List<Server> servers)
        {
            flowOtherServers.Controls.Clear();
            foreach (Server server in servers)
            {
                Button button = CreateServerButton(server);
                
                button.Click += TakeServerFromServersNotOnShift;
                flowOtherServers.Controls.Add(button);
            }
        }

        private void TakeServerFromServersNotOnShift(object? sender, EventArgs e)
        {
            Button btn = (Button)sender;
            Server server = (Server)btn.Tag;
            if (shift.ServersNotOnShift.Contains(server))
            {
                shift.AddServerToSelectedFloorplan(server);
            }
            PopulateServersNotOnShiftServerButtons(shift.ServersNotOnShift);
            PopulateSelectedFloorplanServerButtons();
            //PopulateCboAreas();
        }

        private void PopulateOtherFloorplanServers()
        {
            Floorplan floorplanSelected = (Floorplan)cboFloorplans.SelectedItem;
            secondaryFloorplan = floorplanSelected;
            if (floorplanSelected != null)
            {
                PopulateSecondaryFloorplanServerButtons();
            }
        }

        private void cboFloorplans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //secondaryFloorplan = (Floorplan)cboFloorplans.SelectedItem;
            PopulateOtherFloorplanServers();

        }

        private void txtSearchServers_TextChanged(object sender, EventArgs e)
        {
            var searchText = txtSearchServers.Text;
            FilterServers(searchText);
        }
        private void FilterServers(string searchText)
        {
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filteredServers = shift.ServersNotOnShift
                    .Where(server => server.Name.StartsWith(searchText, StringComparison.OrdinalIgnoreCase) ||
                                     (server.DisplayName != null && server.DisplayName.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)))
                    .OrderByFirstLetter()
                    .ToList();

                PopulateServersNotOnShiftServerButtons(filteredServers);
                txtSearchServers.Focus();
            }
            else
            {
                PopulateServersNotOnShiftServerButtons(shift.ServersNotOnShift);
                txtSearchServers.Focus();
            }
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
