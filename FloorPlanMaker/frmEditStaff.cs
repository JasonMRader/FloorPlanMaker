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
        public ShiftManager shiftManager;
        private DiningAreaCreationManager DiningAreaManager = new DiningAreaCreationManager();
        public frmEditStaff(StaffManager staffManager, ShiftManager shiftManager)
        {
            InitializeComponent();
            this.staffManager = staffManager;
            this.shiftManager = shiftManager;

        }

        private void btnAddNewServer_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            server.Name = txtNewServerName.Text;
            SqliteDataAccess.SaveNewServer(server);
            txtNewServerName.Clear();
            staffManager.AllServers.Clear();
            staffManager.AllServers = SqliteDataAccess.LoadServers();
            clbAllServers.DataSource = null;
            clbAllServers.DataSource = staffManager.AllServers;
            clbAllServers.DisplayMember = "Name";

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
                shiftManager.UnassignedServers.Add(server);
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
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.LightGray,
                Tag = server
            };

            b.Click += ServerButton_Click;
            return b;
        }
        private void ServerButton_Click(object sender, EventArgs e)
        {

            Button serverButton = sender as Button;
            if (serverButton == null) return;

            Server server = (Server)serverButton.Tag;

            if (shiftManager.UnassignedServers.Contains(server))
            {
                shiftManager.UnassignedServers.Remove(server);
                flowUnassignedServers.Controls.Remove(serverButton);

                shiftManager.SelectedFloorplan.Servers.Add(server);
            }
            else
            {
                foreach (Control c in flowDiningAreaAssignment.Controls)
                {
                    if (c is FlowLayoutPanel flowLayoutPanel)
                    {
                        foreach (Control c2 in flowLayoutPanel.Controls)
                        {
                            if (c2.Tag == server)
                            {
                                flowLayoutPanel.Controls.Remove(c2);
                                if (flowLayoutPanel.Tag is Floorplan fp)
                                {
                                    fp.Servers.Remove(server);
                                }
                            }
                        }
                    }
                }


                if (cbUnassignedServers.Checked)
                {
                    shiftManager.UnassignedServers.Add(server);
                    Button newServerButton = CreateServerButton(server);
                    flowUnassignedServers.Controls.Add(newServerButton);
                }
                else
                {
                    shiftManager.SelectedFloorplan.Servers.Add(server);
                }

            }
            FlowLayoutPanel SelectedTargetPanel = null;
            foreach (Control control in flowDiningAreaAssignment.Controls)
            {
                if (control is FlowLayoutPanel panel && panel.Tag == shiftManager.SelectedFloorplan)
                {
                    SelectedTargetPanel = panel;
                    break;
                }
            }
            if (SelectedTargetPanel != null)
            {
                Button newServerButton = CreateServerButton(server);
                newServerButton.Width = SelectedTargetPanel.Width - 8;

                SelectedTargetPanel.Controls.Add(newServerButton);
            }
            RefreshFloorplanCountLabels();
        }
        //private void ServerButton_Click(object sender, EventArgs e)
        //{
        //    Button serverButton = sender as Button;
        //    if (serverButton == null || shiftManager.SelectedFloorplan == null) return;

        //    Server server = (Server)serverButton.Tag;

        //    shiftManager.UnassignedServers.Remove(server);
        //    flowUnassignedServers.Controls.Remove(serverButton);

        //    shiftManager.SelectedFloorplan.Servers.Add(server);


        //    FlowLayoutPanel matchingPanel = null;
        //    foreach (Control control in flowDiningAreaAssignment.Controls)
        //    {
        //        if (control is FlowLayoutPanel panel && panel.Tag == shiftManager.SelectedFloorplan)
        //        {
        //            matchingPanel = panel;
        //            break;
        //        }
        //    }

        //    if (matchingPanel != null)
        //    {
        //        Button newServerButton = CreateServerButton(server);
        //        newServerButton.Width = matchingPanel.Width - 8;

        //        matchingPanel.Controls.Add(newServerButton);
        //    }
        //}
        private void btnAssignTables_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void btnAssignAreas_Click(object sender, EventArgs e)
        {

            int selectedCount = clbDiningAreaSelection.CheckedItems.Count;

            if (selectedCount == 0)
                return;

            for (int i = 0; i < selectedCount; i++)
            {
                DiningArea area = (DiningArea)clbDiningAreaSelection.CheckedItems[i];

                shiftManager.DiningAreasUsed.Add(area);
                shiftManager.CreateFloorplanForDiningArea(area);
            }
            AddFloorplansToFlowPanel();

        }
        private void AddFloorplansToFlowPanel()
        {
            flowDiningAreaAssignment.Controls.Clear();

            int selectedCount = shiftManager.Floorplans.Count;

            if (selectedCount == 0)
                return;

            int width = flowDiningAreaAssignment.Width / selectedCount;
            int height = 30;
            int floorplanCount = 1;
            bool isChecked = true;
            List<Control> labelList = new List<Control>();
            List<Control> flowList = new List<Control>();
            foreach (Floorplan fp in shiftManager.Floorplans)
            {
                

                RadioButton rb = new RadioButton
                {
                    Width = width,
                    Height = height,
                    Appearance = Appearance.Button,
                    AutoSize = false,
                    TextAlign = ContentAlignment.MiddleCenter,
                    Margin = new Padding(0),
                    Text = fp.DiningArea.Name,
                    Tag = fp
                   
                };
                
                //shiftManager.DiningAreasUsed.Add((DiningArea)clbDiningAreaSelection.CheckedItems[i]);
                rb.CheckedChanged += FloorplanRadioButton_CheckedChanged;
                if (floorplanCount == 1)
                {
                    rb.Checked = true;
                }
                floorplanCount++;
                Label label = new Label
                {
                    AutoSize = false,
                    Width = width - 8,
                    Height = 30,
                    Margin = new Padding(4),
                    Text = "Servers: " + fp.Servers.Count.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12F),
                    Tag = fp
                };
                labelList.Add(label);
                FlowLayoutPanel floorplanPanel = new FlowLayoutPanel
                {
                    Width = width - 8,
                    Height = flowDiningAreaAssignment.Height - height, // Adjust as needed
                    Margin = new Padding(4),
                    BackColor = Color.SkyBlue,
                    Tag = fp
                };
                flowDiningAreaAssignment.Controls.Add(rb);
                //flowDiningAreaAssignment.Controls.Add(floorplanPanel);
                flowList.Add(floorplanPanel);

            }
            foreach (Control c in labelList)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }
            foreach (Control c in flowList)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }
        }
        private void RefreshFloorplanCountLabels()
        {
            foreach (Control c in flowDiningAreaAssignment.Controls)
            {
                if (c is Label fpCountLabel)
                {
                    if (fpCountLabel.Tag is Floorplan fp)
                    {
                        fpCountLabel.Text = "Servers: " + fp.Servers.Count.ToString();
                    }
                }
            }


        }
        private void FloorplanRadioButton_CheckedChanged(object? sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null && rb.Checked)
            {
                shiftManager.SelectedFloorplan = (Floorplan)rb.Tag;
                cbUnassignedServers.Checked = false;
            }
        }

        private void cbUnassignedServers_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUnassignedServers.Checked)
            {
                foreach (Control c in flowDiningAreaAssignment.Controls)
                {
                    if (c is RadioButton rb)
                    {
                        rb.Checked = false;
                    }
                }
                shiftManager.SelectedFloorplan = null;
            }


        }
        //private void btnAssignAreas_Click(object sender, EventArgs e)
        //{

        //    flowDiningAreaAssignment.Controls.Clear();

        //    int selectedCount = clbDiningAreaSelection.CheckedItems.Count;

        //    if (selectedCount == 0)
        //        return;

        //    int width = flowDiningAreaAssignment.Width / selectedCount;
        //    int height = 30;



        //    for (int i = 0; i < selectedCount; i++)
        //    {

        //        RadioButton rb = new RadioButton
        //        {
        //            Width = width,
        //            Height = height,
        //            Appearance = Appearance.Button,
        //            AutoSize = false,
        //            TextAlign = ContentAlignment.MiddleCenter,
        //            Margin = new Padding(0),
        //            Text = ((DiningArea)clbDiningAreaSelection.CheckedItems[i]).Name
        //        };
        //        shiftManager.DiningAreasUsed.Add((DiningArea)clbDiningAreaSelection.CheckedItems[i]);
        //        flowDiningAreaAssignment.Controls.Add(rb);



        //    }
        //}
    }
}
