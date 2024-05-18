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
        public frmAddRemoveStaff()
        {
            InitializeComponent();
        }

        private void frmAddRemoveStaff_Load(object sender, EventArgs e)
        {
            foreach (Server server in employeeManager.AllServers)
            {
                lbServers.Items.Add(server.Name);
            }
        }
    }
}
