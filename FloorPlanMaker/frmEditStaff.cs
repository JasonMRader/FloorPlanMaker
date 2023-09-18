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
    }
}
