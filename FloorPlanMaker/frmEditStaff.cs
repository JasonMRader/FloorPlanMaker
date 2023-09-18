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
        public List<Server> AllServers = new List<Server>();
        public frmEditStaff()
        {
            InitializeComponent();
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
            lvAllStaff.Items.Clear();
            AllServers.Clear();
            AllServers = SqliteDataAccess.LoadServers();
            foreach (Server server in AllServers)
            {
                lvAllStaff.Items.Add(server.Name);
                lbAllServers.Items.Add(server);
            }
        }
    }
}
