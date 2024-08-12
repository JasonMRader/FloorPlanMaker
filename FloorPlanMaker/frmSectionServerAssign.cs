using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmSectionServerAssign : Form
    {
        private Section section { get; set; }
        private Shift shift { get; set; }
        private Floorplan floorplan { get; set; }
        public event EventHandler CloseClicked;
        public event EventHandler ServerAssignedClicked;
        public frmSectionServerAssign()
        {
            InitializeComponent();
        }
        public frmSectionServerAssign(Section section, Shift shift)
        {
            InitializeComponent();
            this.section = section;
            this.shift = shift;
            this.floorplan = shift.SelectedFloorplan;
            this.BackColor = section.Color;
            flowServerSelect.BackColor = Color.WhiteSmoke;
        }
        public void SetNewSectionAndShift(Section section, Shift shift)
        {
            this.section = section;
            this.shift = shift;
            this.floorplan = shift.SelectedFloorplan;
            this.BackColor = section.Color;
            flowServerSelect.BackColor = Color.WhiteSmoke;
            PopulateCboAreas();
        }

        private void frmSectionServerAssign_Load(object sender, EventArgs e)
        {
            PopulateCboAreas();

        }
        private void PopulateFloorplanServers()
        {
            Floorplan floorplanSelected = (Floorplan)cboDiningArea.SelectedItem;

            if (floorplanSelected != null)
            {
                flowServerSelect.Controls.Clear();
                foreach (Server server in floorplanSelected.Servers)
                {
                    if(server.CurrentSection != this.section)
                    {
                        Button button = CreateServerButton(server);
                        flowServerSelect.Controls.Add(button);
                    }
                   
                }
            }
        }
        private Button CreateServerButton(Server server)
        {
            Button button = new Button();
            button.Text = server.ToString();
            button.Tag = server;
            UITheme.FormatCTAButton(button);
            button.Font = UITheme.MainFont;

            if (server.CurrentSection != null)
            {
                button.BackColor = server.CurrentSection.Color;
                button.ForeColor = server.CurrentSection.FontColor;
            }
            else
            {
                button.BackColor = Color.Gray;
                button.ForeColor = Color.Black;
            }

            button.AutoSize = false;
            button.Size = new System.Drawing.Size(flowServerSelect.Width - 10, 30);
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Click += ServerButtonClicked;

            return button;
        }

        private void ServerButtonClicked(object? sender, EventArgs e)
        {
            var clickedButton = (Button)sender;
            var assignedServer = (Server)clickedButton.Tag;

            if(assignedServer.CurrentSection == null)
            {
                section.AddServer(assignedServer);
                
            }
            
            //assignedServer.SalesFromPickupSection = this.Section.AdditionalPickupSales;
            //if (section.ServerTeam != null)
            //{
            //    for (int i = 0; i < Section.ServerTeam.Count; i++)
            //    {
            //        this.sectionLabel.Height += 30;
            //        this.headerPanel.Height += 30;
            //    }
            //}
            //if (this.Section.ServerCount == this.Section.ServerTeam.Count())
            //{
            //    serversPanel.Height = 0;
            //    serverPanelOpen = false;
            //    RefreshUnassignedServerPanel();
            //}
            //else
            //{
            //    RefreshUnassignedServerPanel();
            //}


            //UpdateLabel();
        }

        private void PopulateCboAreas()
        {
            cboDiningArea.Items.Clear();
            foreach (Floorplan floorplan in shift.Floorplans)
            {

                cboDiningArea.Items.Add(floorplan);
                cboDiningArea.DisplayMember = floorplan.DiningArea.Name;

            }
            cboDiningArea.SelectedItem = floorplan;


        }

        private void GetFormSize()
        {
            this.Size = new System.Drawing.Size(this.Width, 76 + flowServerSelect.Height);
        }

        private void cboDiningArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFloorplanServers();
            GetFormSize();
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            
            
           CloseClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
