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
        }

        private void frmAddRemoveStaff_Load(object sender, EventArgs e)
        {
            foreach (Server server in employeeManager.AllServers)
            {
                lbServers.Items.Add(server);
            }
        }

        private void lbServers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbServers.SelectedIndex != -1)
            {
                SelectedServer = lbServers.SelectedItem as Server;
                txtServerName.Text = SelectedServer.Name;
                txtServerDisplayName.Text = SelectedServer.DisplayName;
            }
        }

        private void btnAddNewServer_Click(object sender, EventArgs e)
        {

        }

        private void btnSaveServer_Click(object sender, EventArgs e)
        {

        }

        private void btnArchiveServer_Click(object sender, EventArgs e)
        {

        }

        private void rdoShowActive_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdoShowArchived_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
