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
    public partial class frmAddRemoveStaff : Form
    {
        private EmployeeManager employeeManager = new EmployeeManager();
        private Server SelectedServer;
        public frmAddRemoveStaff()
        {
            InitializeComponent();
            BindDataGridView();
            dgvServers.CellValueChanged += DgvServers_CellValueChanged;
        }

        private void frmAddRemoveStaff_Load(object sender, EventArgs e)
        {
            RefreshServerListBox();
        }

        private void lbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbServers.SelectedIndex != -1)
            {
                SelectedServer = lbServers.SelectedItem as Server;
                txtServerName.Text = SelectedServer.Name;
                txtServerDisplayName.Text = SelectedServer.DisplayName;
                lblServerName.Text = SelectedServer.Name;
                tbarClosing.Value = SelectedServer.CloseFrequency;
                tbarCocktail.Value = SelectedServer.CocktailPreference;
                tbarOutside.Value = SelectedServer.OutsideFrequency;
                tbarTeamWait.Value = SelectedServer.TeamWaitFrequency;
                tbarSection.Value = SelectedServer.PreferedSectionWeight;
                lblClosing.Text = SelectedServer.CloseFrequency.ToString();
                lblCocktail.Text = SelectedServer.CocktailPreference.ToString();
                lblOutside.Text = SelectedServer.OutsideFrequency.ToString();
                lblTeamWait.Text = SelectedServer.TeamWaitFrequency.ToString();
                lblPerferedSections.Text = SelectedServer.PreferedSectionWeight.ToString();
            }
            else
            {
                lblServerName.Text = "Server";
            }
        }

        private void btnAddNewServer_Click(object sender, EventArgs e)
        {
            Server newServer = new Server();
            newServer.Name = txtNewServerName.Text;
            newServer.Archived = false;
            SqliteDataAccess.SaveNewServer(newServer);
            RefreshServerListBox();
            txtNewServerName.Clear();
        }

        private void btnSaveServer_Click(object sender, EventArgs e)
        {
            SelectedServer.Name = txtServerName.Text;
            SelectedServer.DisplayName = txtServerDisplayName.Text;
            SqliteDataAccess.UpdateServer(SelectedServer);
            RefreshServerListBox();
        }

        private void btnArchiveServer_Click(object sender, EventArgs e)
        {
            if (SelectedServer != null)
            {
                SelectedServer.Archived = !SelectedServer.Archived;
                SqliteDataAccess.UpdateServer(SelectedServer);

                RefreshServerListBox();


                lbServers.ClearSelected();
                SelectedServer = null;
                txtServerName.Text = string.Empty;
                txtServerDisplayName.Text = string.Empty;
            }
        }


        private void rdoShowActive_CheckedChanged(object sender, EventArgs e)
        {
            RefreshServerListBox();
        }

        private void rdoShowArchived_CheckedChanged(object sender, EventArgs e)
        {
            RefreshServerListBox();
        }
        private void RefreshServerListBox()
        {
            employeeManager = new EmployeeManager();
            lbServers.Items.Clear();
            if (rdoShowActive.Checked)
            {
                btnArchiveServer.Text = "Archive Server";
                foreach (Server server in employeeManager.ActiveServers)
                {
                    lbServers.Items.Add(server);
                }
            }
            if (rdoShowArchived.Checked)
            {
                btnArchiveServer.Text = "Activate Server";
                foreach (Server server in employeeManager.InactiveServers)
                {
                    lbServers.Items.Add(server);
                }
            }
        }

        private void btnSetDisplayToFirstName_Click(object sender, EventArgs e)
        {
            SelectedServer.DisplayName = SelectedServer.FirstName;
            txtServerDisplayName.Text = SelectedServer.DisplayName;
            SqliteDataAccess.UpdateServer(SelectedServer);
            RefreshServerListBox();
        }

        private void tbarCocktail_Scroll(object sender, EventArgs e)
        {
            lblCocktail.Text = tbarCocktail.Value.ToString();
            SelectedServer.CocktailPreference = tbarCocktail.Value;
            SqliteDataAccess.UpdateServer(SelectedServer);
        }

        private void tbarClosing_Scroll(object sender, EventArgs e)
        {
            lblClosing.Text = tbarClosing.Value.ToString();
            SelectedServer.CloseFrequency = tbarClosing.Value;
            SqliteDataAccess.UpdateServer(SelectedServer);
        }

        private void tbarOutside_Scroll(object sender, EventArgs e)
        {
            lblOutside.Text = tbarOutside.Value.ToString();
            SelectedServer.OutsideFrequency = tbarOutside.Value;
            SqliteDataAccess.UpdateServer(SelectedServer);
        }

        private void tbarTeamWait_Scroll(object sender, EventArgs e)
        {
            lblTeamWait.Text = tbarTeamWait.Value.ToString();
            SelectedServer.TeamWaitFrequency = tbarTeamWait.Value;
            SqliteDataAccess.UpdateServer(SelectedServer);
        }

        private void tbarSection_Scroll(object sender, EventArgs e)
        {
            lblPerferedSections.Text = tbarSection.Value.ToString();
            SelectedServer.PreferedSectionWeight = tbarSection.Value;
            SqliteDataAccess.UpdateServer(SelectedServer);
        }
        private void BindDataGridView()
        {
            // Transform the list of servers into a list of ServerDisplay objects
            var serverDisplayList = employeeManager.ActiveServers
                .Select(s => new ServerDisplay
                {
                    Server = s.ToString(),
                    Cocktail = s.CocktailPreference,
                    Close = s.CloseFrequency,
                    TeamWait = s.TeamWaitFrequency,
                    Outside = s.OutsideFrequency,
                    Section = s.PreferedSectionWeight
                }).ToList();

            var bindingList = new SortableBindingList<ServerDisplay>(serverDisplayList);
            var source = new BindingSource(bindingList, null);
            dgvServers.DataSource = source;

            // Set columns to be sortable
            foreach (DataGridViewColumn column in dgvServers.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            dgvServers.Columns["Server"].Width = 90;
            dgvServers.Columns["Cocktail"].Width = 70;
            dgvServers.Columns["Close"].Width = 70;
            dgvServers.Columns["TeamWait"].HeaderText = "TeamWait";
            dgvServers.Columns["TeamWait"].Width = 70;
            dgvServers.Columns["Outside"].HeaderText = "Outside";
            dgvServers.Columns["Outside"].Width = 70;
            dgvServers.Columns["Section"].HeaderText = "Section";
            dgvServers.Columns["Section"].Width = 70;
        }
        private void DgvServers_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvServers.Rows[e.RowIndex];
                var serverDisplay = row.DataBoundItem as ServerDisplay;

                if (serverDisplay != null)
                {
                    // Find the corresponding server object
                    var server = employeeManager.ActiveServers.FirstOrDefault(s => s.ToString() == serverDisplay.Server);

                    if (server != null)
                    {
                        // Update the server's properties with the new values
                        server.CocktailPreference = serverDisplay.Cocktail;
                        server.CloseFrequency = serverDisplay.Close;
                        server.TeamWaitFrequency = serverDisplay.TeamWait;
                        server.OutsideFrequency = serverDisplay.Outside;
                        server.PreferedSectionWeight = serverDisplay.Section;

                        // Update the server in the database
                        SqliteDataAccess.UpdateServer(server);
                    }
                }
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            frmTutorialVideos tutorialForm = new frmTutorialVideos(TutorialImages.TutorialType.Servers);
            tutorialForm.Show();
        }
    }

}
