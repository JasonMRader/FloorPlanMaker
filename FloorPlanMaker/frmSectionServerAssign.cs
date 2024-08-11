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
        public frmSectionServerAssign(Section section, Shift shift)
        {
            InitializeComponent();
            this.section = section;
            this.shift = shift;
            this.floorplan = shift.SelectedFloorplan;
            this.BackColor = section.Color;
            flowServerSelect.BackColor = Color.WhiteSmoke;
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
                    Button button = CreateServerButton(server);


                    flowServerSelect.Controls.Add(button);
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

            button.AutoSize = false;
            button.Size = new System.Drawing.Size(flowServerSelect.Width - 10, 30);
            button.TextAlign = ContentAlignment.MiddleCenter;

            return button;
        }
        private void PopulateCboAreas()
        {
            foreach (Floorplan floorplan in shift.Floorplans)
            {

                cboDiningArea.Items.Add(floorplan);
                cboDiningArea.DisplayMember = floorplan.DiningArea.Name;

            }
            cboDiningArea.SelectedItem = floorplan;


        }

        private void flowServerSelect_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboDiningArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFloorplanServers();
        }
    }
}
