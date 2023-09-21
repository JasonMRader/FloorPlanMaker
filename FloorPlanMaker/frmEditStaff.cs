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
        private DiningAreaCreationManager DiningAreaManager = new DiningAreaCreationManager();
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
            clbDiningAreaSelection.DataSource = DiningAreaManager.DiningAreas;

            clbAllServers.DataSource = staffManager.AllServers;
            clbAllServers.DisplayMember = "Name";
            //clbAllServers.ValueMember = Server;


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
                Button serverButton = CreateServerButton(server);
                flowUnassignedServers.Controls.Add(serverButton);
            }

        }
        private Button CreateServerButton(Server server)
        {

            Button b = new Button
            {
                Width = 150,
                Height = 30,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Margin = new Padding(5),
                Text = server.Name,
                Tag = server
            };


            return b;
        }
        private void btnAssignTables_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnAssignAreas_Click(object sender, EventArgs e)
        {
            // Clearing any previous controls (radio buttons)
            flowDiningAreaAssignment.Controls.Clear();

            int selectedCount = clbDiningAreaSelection.CheckedItems.Count;

            if (selectedCount == 0)
                return;

            int width = flowDiningAreaAssignment.Width / selectedCount;
            int height = 30;

            //switch (selectedCount)
            //{
            //    case 1:
            //        width = flowDiningAreaAssignment.Width;
            //        break;
            //    case 2:
            //    case 4:
            //    case 5:
            //    case 6:
            //        width = flowDiningAreaAssignment.Width / 2;
            //        break;
            //    case 3:
            //        width = flowDiningAreaAssignment.Width / 3;
            //        break;
            //}

            for (int i = 0; i < selectedCount; i++)
            {
                //if (selectedCount == 5 && i < 3)
                //{
                //    width = flowDiningAreaAssignment.Width / 3;
                //}
                //else if (selectedCount == 5 && i == 3)
                //{
                //    width = flowDiningAreaAssignment.Width / 2;
                //}
                RadioButton rb = new RadioButton
                {
                    Width = width,
                    Height = height,
                    Appearance = Appearance.Button,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(0),
                    Text = ((DiningArea)clbDiningAreaSelection.CheckedItems[i]).Name
                };

                flowDiningAreaAssignment.Controls.Add(rb);

                // For the scenario of 5 items: after adding 3 items on the top row, adjust width for the remaining 2 items

            }
        }

    }
}
