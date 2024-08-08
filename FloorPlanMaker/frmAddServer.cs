using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmAddServer : Form
    {
        public BindingList<Server> newServers = new BindingList<Server>();
        private List<int> missingIDs = new List<int>();
        private List<HotSchedulesEmployee> hotSchedulesEmployees { get; set; }
        private List<HotSchedulesEmployee> filteredHotScheduleEmployees { get; set; }
        public frmAddServer(List<int> missingServerIDs)
        {
            InitializeComponent();
            lbServersToAdd.DataSource = newServers;
            lbServersToAdd.DisplayMember = "Name";
            this.missingIDs = missingServerIDs;
            //GetHotSchedulesEmployees();
            lbMissingServerIDs.DataSource = missingIDs;

        }
        private async void GetHotSchedulesEmployees()
        {
            this.hotSchedulesEmployees = await HotSchedulesApiAccess.GetAllEmployees();
            filteredHotScheduleEmployees = this.hotSchedulesEmployees;
            dgvHotSchedulesEmployees.DataSource = new BindingSource { DataSource = hotSchedulesEmployees };
        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {
            Server server = new Server
            {
                Name = txtServerName.Text
            };
            newServers.Add(server);
            txtServerName.Clear();
            txtServerName.Focus();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddServer_Load(object sender, EventArgs e)
        {

            dgvHotSchedulesEmployees.AutoGenerateColumns = false;
            dgvHotSchedulesEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FName",
                HeaderText = "First Name"
            });
            dgvHotSchedulesEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "LName",
                HeaderText = "Last Name"
            });
            dgvHotSchedulesEmployees.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "HsId",
                HeaderText = "HsId"
            });
            GetHotSchedulesEmployees();
        }

        private void lbMissingServerIDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbMissingServerIDs.SelectedIndex != -1)
            {
                if (lbMissingServerIDs.SelectedItem != null)
                {
                    int id = (int)lbMissingServerIDs.SelectedItem;
                    FilterByID(id);
                    txtSearch.Text = "";
                }

            }
        }
        private void FilterServers(string searchText)
        {
            if (hotSchedulesEmployees == null)
            {
                return;
            }
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                var filteredServers = hotSchedulesEmployees
                    .Where(server => server.FName.StartsWith(searchText, StringComparison.OrdinalIgnoreCase) ||
                                        server.LName.StartsWith(searchText, StringComparison.OrdinalIgnoreCase))
                    .OrderByFirstLetter()
                    .ToList();

                dgvHotSchedulesEmployees.DataSource = new BindingSource { DataSource = filteredServers };
            }
            else
            {
                dgvHotSchedulesEmployees.DataSource = new BindingSource { DataSource = hotSchedulesEmployees };
            }
        }
        private void FilterByID(int id)
        {
            if (hotSchedulesEmployees == null)
            {
                return;
            }
            var filteredServers = hotSchedulesEmployees
                   .Where(server => server.HsId == id)
                   .OrderByFirstLetter()
                   .ToList();

            dgvHotSchedulesEmployees.DataSource = new BindingSource { DataSource = filteredServers };
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text.Length > 0)
            {
                FilterServers(txtSearch.Text);
            }
        }

        private void dgvHotSchedulesEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvHotSchedulesEmployees.Rows[e.RowIndex];
                HotSchedulesEmployee selectedEmployee = (HotSchedulesEmployee)row.DataBoundItem;
                txtServerName.Text = $"{selectedEmployee.FName} {selectedEmployee.LName}";
            }
        }
    }
}
