﻿
using FloorplanClassLibrary;
using FloorPlanMakerUI;
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
        public EmployeeManager employeeManager;
        public ShiftManager shiftManager;
        private DiningAreaCreationManager DiningAreaManager = new DiningAreaCreationManager();
        public frmEditStaff(EmployeeManager staffManager, ShiftManager shiftManager)
        {
            InitializeComponent();
            this.employeeManager = staffManager;
            this.shiftManager = shiftManager;

        }

        private void btnAddNewServer_Click(object sender, EventArgs e)
        {
            Server server = new Server();
            server.Name = txtNewServerName.Text;
            SqliteDataAccess.SaveNewServer(server);
            txtNewServerName.Clear();
            employeeManager.AllServers.Clear();
            employeeManager.AllServers = SqliteDataAccess.LoadServers();


        }

        private void frmEditStaff_Load(object sender, EventArgs e)
        {
            clbDiningAreaSelection.DataSource = DiningAreaManager.DiningAreas;




            foreach (Server server in shiftManager.ServersNotOnShift)
            {
                server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
                ServerControl serverControl = new ServerControl(server, 155, 20);
                serverControl.Click += serverControl_Click_AddToShift;
                serverControl.HideShifts();
                flowAllServers.Controls.Add(serverControl);
            }


            //lbServersOnShift.ValueMember = "Value";
        }
        private void AdjustServerLists(Server server, List<Server> listToRemoveFrom, List<Server> listToAddTo, FlowLayoutPanel flowToRemoveFrom, FlowLayoutPanel flowToAddTo)
        {

        }

        private void serverControl_Click_AddToShift(object sender, EventArgs e)
        {
            ServerControl oldServerControl = (ServerControl)sender;
            Server server = oldServerControl.Server;
            //if (employeeManager.ServersOnShift == null)
            //{
            //    employeeManager.ServersOnShift = new List<Server>();
            //}
            //employeeManager.ServersOnShift.Add(server);
            shiftManager.UnassignedServers.Add(server);
            shiftManager.ServersNotOnShift.Remove(server);
            //Button serverButton = CreateServerButton(server);
            ServerControl newServerControl = new ServerControl(server, 350, 30);

            newServerControl.Click += ServerControl_Click;

            newServerControl.AddRemoveButton(flowUnassignedServers, flowAllServers, shiftManager.UnassignedServers, shiftManager.ServersNotOnShift, 155, 20);
            newServerControl.RemoveButton.Click += serverControl_Click_RemoveFromShift;
            ImageSetter.SetShiftControlImages(newServerControl);
            flowUnassignedServers.Controls.Add(newServerControl);
            flowAllServers.Controls.Remove(oldServerControl);
        }
        private void serverControl_Click_RemoveFromShift(object sender, EventArgs e)
        {
            Button removeButton = (Button)sender;

            // Get the ServerControl instance.
            ServerControl oldServerControl = (ServerControl)removeButton.Parent.Parent;

            Server server = oldServerControl.Server;

            shiftManager.ServersNotOnShift.Add(server);
            shiftManager.UnassignedServers.Remove(server);
            ServerControl serverControl = new ServerControl(server, 155, 20);
            serverControl.Click += serverControl_Click_AddToShift;
            serverControl.HideShifts();
            flowAllServers.Controls.Add(serverControl);
            flowUnassignedServers.Controls.Remove(oldServerControl);
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
                    ServerControl serverControl = new ServerControl(server, 350, 30);
                    serverControl.Click += ServerControl_Click;
                    ImageSetter.SetShiftControlImages(serverControl);
                    flowUnassignedServers.Controls.Add(serverControl);

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
        private void ServerControl_Click(object sender, EventArgs e)
        {

            ServerControl serverControl = sender as ServerControl;
            if (serverControl == null) return;

            Server server = serverControl.Server;

            if (shiftManager.UnassignedServers.Contains(server))
            {
                shiftManager.UnassignedServers.Remove(server);
                flowUnassignedServers.Controls.Remove(serverControl);

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
                shiftManager.CreateFloorplanForDiningArea(area, DateTime.Now, false, 0, 0);
            }
            AddFloorplansToFlowPanel();

        }
        private List<Control> ServerCountLabels = new List<Control>();
        private List<Control> ServerMaxLabels = new List<Control>();
        private List<Control> ServerAvgLabels = new List<Control>();
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
            ServerCountLabels = new List<Control>();
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
                ServerCountLabels.Add(label);
                Label maxLabel = new Label
                {
                    AutoSize = false,
                    Width = (width - 8),
                    Height = 30,
                    Margin = new Padding(4),
                    Text = "Max: " + fp.DiningArea.GetMaxCovers().ToString() + "  Avg: " + fp.DiningArea.GetAverageCovers().ToString(),
                    TextAlign = ContentAlignment.MiddleCenter,
                    Font = new Font("Segoe UI", 12F),
                    Tag = fp
                };
                ServerMaxLabels.Add(maxLabel);
               
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
            foreach (Control c in ServerCountLabels)
            {
                flowDiningAreaAssignment.Controls.Add((Control)c);
            }
            foreach (Control c in ServerMaxLabels)
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
            //foreach (Control c in flowDiningAreaAssignment.Controls)
            foreach (Control c in ServerCountLabels)
            {
                if (c is Label fpCountLabel)
                {
                    if (fpCountLabel.Tag is Floorplan fp)
                    {
                        fpCountLabel.Text = "Servers: " + fp.Servers.Count.ToString();
                    }
                }
            }
            foreach (Control c in ServerMaxLabels)
            {
                if (c is Label fpMaxLabel)
                {
                    if (fpMaxLabel.Tag is Floorplan fp)
                    {
                        if (fp.Servers.Count > 0)
                        {
                            fpMaxLabel.Text = "Max: " + (fp.DiningArea.GetMaxCovers() / fp.Servers.Count).ToString("F1")
                                + "  Avg: " + (fp.DiningArea.GetAverageCovers() / fp.Servers.Count).ToString("F1");
                        }
                        else
                        {
                            fpMaxLabel.Text = "Max: " + fp.DiningArea.GetMaxCovers().ToString() + "  Avg: " + fp.DiningArea.GetAverageCovers().ToString();
                        }
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
