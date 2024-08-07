using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
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
    public partial class frmGetHotSchedulesIDs : Form
    {
        private EmployeeManager employeeManager { get; set; }
        private List<Server> serversWithoutIDs { get; set; }
        private List<Server> serversWithIDs { get; set; }
        private List<HotSchedulesEmployee> hotSchedulesEmployees { get; set; }
        private List<HotSchedulesEmployee> filteredHotScheduleEmployees { get; set; }
        private Server serverSelected { get; set; }
        public frmGetHotSchedulesIDs(EmployeeManager employeeManager)
        {
            InitializeComponent();
            this.employeeManager = employeeManager;
            RefreshServerListBox();

        }
        private async void GetHotSchedulesEmployees()
        {
            this.hotSchedulesEmployees = await HotSchedulesApiAccess.GetAllEmployees();
            filteredHotScheduleEmployees = this.hotSchedulesEmployees;
            dgvHotSchedulesEmployees.DataSource = new BindingSource { DataSource = hotSchedulesEmployees };
        }

        private void frmGetHotSchedulesIDs_Load(object sender, EventArgs e)
        {
            lbServers.SelectedIndex = 0;
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
        private void RefreshServerListBox()
        {
            employeeManager = new EmployeeManager();
            lbServers.Items.Clear();
            serversWithoutIDs = employeeManager.ActiveServers.Where(s => s.HSID == null).ToList();
            serversWithIDs = employeeManager.ActiveServers.Where(s => s.HSID != null).ToList();
            if (rbMissingID.Checked)
            {
                foreach (Server server in serversWithoutIDs)
                {
                    lbServers.Items.Add(server);
                }
            }
            if (rbHasID.Checked)
            {

                foreach (Server server in serversWithIDs)
                {
                    lbServers.Items.Add(server);
                }
            }
        }

        private void rbMissingID_CheckedChanged(object sender, EventArgs e)
        {
            RefreshServerListBox();
        }

        private void btnSaveEmployeeID_Click(object sender, EventArgs e)
        {
            if (Int32.TryParse(txtHSID.Text, out int HSID))
            {
                serverSelected.HSID = HSID;
                SqliteDataAccess.UpdateServer(serverSelected);
            }
            else
            {
                MessageBox.Show("The HotSchedules ID must be an integet");
            }
        }

        private void lbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbServers.SelectedIndex != -1)
            {
                serverSelected = lbServers.SelectedItem as Server;
                lblServerName.Text = serverSelected.Name;
                txtHSID.Text = serverSelected.HSID.ToString();
            }
            else
            {
                lblServerName.Text = "Server";
            }
        }

        private void dgvHotSchedulesEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure that the row index is valid
            {
                DataGridViewRow row = dgvHotSchedulesEmployees.Rows[e.RowIndex];
                HotSchedulesEmployee selectedEmployee = (HotSchedulesEmployee)row.DataBoundItem;
                txtHSID.Text = selectedEmployee.HsId.ToString();
            }
        }

        private void btnAutoAssignIDs_Click(object sender, EventArgs e)
        {
            if (serversWithoutIDs == null)
            {
                MessageBox.Show("The serversWithoutIDs list is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check if hotSchedulesEmployees is not null
            if (hotSchedulesEmployees == null)
            {
                MessageBox.Show("The hotSchedulesEmployees list is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            List<Server> matchedServers = new List<Server>();
            lbServers.Items.Clear();
            foreach (Server server in serversWithoutIDs)
            {
                HotSchedulesEmployee matchedEmployee = hotSchedulesEmployees.FirstOrDefault(
                    e => e.FName == server.FirstName && e.LName == server.LastName);

                if (matchedEmployee != null)
                {
                    server.HSID = matchedEmployee.HsId;
                    //matchedServers.Add(server);
                    SqliteDataAccess.UpdateServer(server);
                }

            }
            RefreshServerListBox();
            
        }
    }
}
