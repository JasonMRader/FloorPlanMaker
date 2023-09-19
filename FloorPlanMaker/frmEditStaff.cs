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

namespace FloorPlanMaker
{
    public partial class frmEditStaff : Form
    {
        //public List<Server> AllServers = new List<Server>();
        public StaffManager staffManager;
        public frmEditStaff(StaffManager staffManager)
        {
            InitializeComponent();
            this.staffManager = staffManager;
        }

        private void btnAddNewServer_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            server.Name = txtNewServerName.Text;
            SqliteDataAccess.SaveNewServer(server);
            txtNewServerName.Clear();
        }

        private void frmEditStaff_Load(object sender, EventArgs e)
        {
            clbAllServers.DataSource = staffManager.AllServers;
            clbAllServers.DisplayMember = "Name";
            //clbAllServers.ValueMember = Server;

            lbServersOnShift.DataSource = staffManager.ServersOnShift;
            lbServersOnShift.DisplayMember = "Name";
            //lbServersOnShift.ValueMember = "Value";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (staffManager.ServersOnShift == null)
            {
                staffManager.ServersOnShift = new List<Server>();
            }

            foreach (Server server in clbAllServers.CheckedItems)
            {
                staffManager.ServersOnShift.Add(server);
            }
            lbServersOnShift.DataSource = null;
            lbServersOnShift.DataSource = staffManager.ServersOnShift;
        }

        private void btnAssignTables_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
