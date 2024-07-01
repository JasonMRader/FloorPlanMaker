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
        public frmEditShiftRoster(Shift shift)
        {
            InitializeComponent();
            this.shift = shift;
        }

        private void frmEditShiftRoster_Load(object sender, EventArgs e)
        {
            lblSelectedDiningArea.Text = shift.SelectedFloorplan.DiningArea.Name;
            PopulateFloorplanServerButtons(shift.SelectedFloorplan.Servers, flowThisFloorplan);
            PopulateCboAreas();

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
            cboFloorplans.SelectedIndex = 0;

        }
        private void PopulateFloorplanServerButtons(List<Server> servers, FlowLayoutPanel flowPanel)
        {
            flowPanel.Controls.Clear();
            foreach (Server server in servers)
            {
                Button button = CreateServerButton(server);
                flowPanel.Controls.Add(button);
            }
        }
        private Button CreateServerButton(Server server)
        {
            Button button = new Button();
            button.Text = server.ToString();
            button.Tag = server;

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
                PopulateFloorplanServerButtons(shift.ServersNotOnShift, flowOtherServers);
                txtSearchServers.Focus();
            }
            else
            {
                PopulateOtherFloorplanServers();

            }
        }

        private void PopulateOtherFloorplanServers()
        {
            Floorplan floorplanSelected = (Floorplan)cboFloorplans.SelectedItem;
            if (floorplanSelected != null)
            {
                PopulateFloorplanServerButtons(floorplanSelected.Servers, flowOtherServers);
            }
        }

        private void cboFloorplans_SelectedIndexChanged(object sender, EventArgs e)
        {
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

                PopulateFloorplanServerButtons(filteredServers, flowOtherServers);
                txtSearchServers.Focus();
            }
            else
            {
                PopulateFloorplanServerButtons(shift.ServersNotOnShift, flowOtherServers);
                txtSearchServers.Focus();
            }
        }
    }
}
