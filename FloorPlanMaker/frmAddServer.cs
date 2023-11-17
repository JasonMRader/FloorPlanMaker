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
    public partial class frmAddServer : Form
    {
        public BindingList<Server> newServers = new BindingList<Server>();
        public frmAddServer()
        {
            InitializeComponent();
            lbServersToAdd.DataSource = newServers;
            lbServersToAdd.DisplayMember = "Name";
        }

        private void btnAddServer_Click(object sender, EventArgs e)
        {
            Server server = new Server
            {
                Name = txtServerName.Text
            };
            newServers.Add(server);
            txtServerName.Clear();
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
            
        }
        
    }
}
