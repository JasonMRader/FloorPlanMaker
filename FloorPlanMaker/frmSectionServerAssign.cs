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
    public partial class frmSectionServerAssign : Form , IShiftObserver
    {
        private Section section { get; set; }
        private Shift shift { get; set; }
        private Floorplan floorplan { get; set; }
        public event EventHandler CloseClicked;
        public event EventHandler ServerAssignedClicked;
        public event EventHandler SignalForInvisible;
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

            if (server.CurrentSection != null)
            {
                button.BackColor = server.CurrentSection.Color;
                button.ForeColor = server.CurrentSection.FontColor;
                if(server.CurrentSection == this.section)
                {
                    button.Text = "Remove " + server.ToString();
                }
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
        private void RefreshServerButtonProperties()
        {
            foreach(Button button in flowServerSelect.Controls)
            {
                Server server = button.Tag as Server;
                if(server.CurrentSection == null)
                {
                    button.BackColor = Color.Gray;
                    button.ForeColor = Color.Black;
                    button.Text = server.ToString();
                }
                else
                {
                    button.BackColor = server.CurrentSection.Color;
                    button.ForeColor= server.CurrentSection.FontColor;
                    if(server.CurrentSection == this.section)
                    {
                        button.Text= "Remove " + server.ToString();
                    }
                    else
                    {
                        button.Text = server.ToString();
                    }
                }
            }
        }

        private void ServerButtonClicked(object? sender, EventArgs e)
        {
            var clickedButton = (Button)sender;
            if(!section.IsTeamWait)
            {
                SoloSectionServerButtonClicked(clickedButton);
            }
            else if(section.IsTeamWait)
            {
                TeamSectionServerButtonClicked(clickedButton);
            }
           
            
            RefreshServerButtonProperties();          

           
        }

        private void TeamSectionServerButtonClicked(Button? clickedButton)
        {
            var assignedServer = (Server)clickedButton.Tag;
            if (assignedServer.CurrentSection == null)
            {   
                section.AddServer(assignedServer);
                if(section.ServerCount == section.ServerTeam.Count)
                {
                    SignalForInvisible?.Invoke(this, EventArgs.Empty);
                }    
            }
            //Server clicked DOES have a Section AND this Section has a Server AND ServerTeam does not contain the Server Clicked
            else if (assignedServer.CurrentSection != null
                && section.ServerCount == section.ServerTeam.Count
                && !section.ServerTeam.Contains(assignedServer))
            {
                floorplan.SwapServers(section, assignedServer.CurrentSection);
                SignalForInvisible?.Invoke(this, EventArgs.Empty);
            }
            //CLicked server Does have a Section AND this section does not have a server
            else if (assignedServer.CurrentSection != null && section.Server == null)
            {
                assignedServer.CurrentSection.RemoveServer(assignedServer);
                section.AddServer(assignedServer);
                if (section.ServerCount == section.ServerTeam.Count)
                {
                    SignalForInvisible?.Invoke(this, EventArgs.Empty);
                }
            }
            else if (assignedServer.CurrentSection != null)
            {
                //THis server is in this section
                if (assignedServer.CurrentSection == this.section)
                {
                    assignedServer.CurrentSection.RemoveServer(assignedServer);
                }
            }
        }

        private void SoloSectionServerButtonClicked(Button clickedButton)
        {
            var assignedServer = (Server)clickedButton.Tag;
            if (assignedServer.CurrentSection == null)
            {
                //This Section Already Has a server
                if (section.Server != null)
                {
                    section.RemoveServer(section.Server);
                }
                section.AddServer(assignedServer);
                SignalForInvisible?.Invoke(this, EventArgs.Empty);

            }
            //Server clicked DOES have a Section AND this Section has a Server AND ServerTeam does not contain the Server Clicked
            else if (assignedServer.CurrentSection != null
                && section.Server != null
                && !section.ServerTeam.Contains(assignedServer))
            {
                floorplan.SwapServers(section, assignedServer.CurrentSection);
                SignalForInvisible?.Invoke(this, EventArgs.Empty);
            }
            //CLicked server Does have a Section AND this section does not have a server
            else if (assignedServer.CurrentSection != null && section.Server == null)
            {
                assignedServer.CurrentSection.RemoveServer(assignedServer);
                section.AddServer(assignedServer);
                SignalForInvisible?.Invoke(this, EventArgs.Empty);
            }
            else if (assignedServer.CurrentSection != null)
            {
                //THis server is in this section
                if (assignedServer.CurrentSection == this.section)
                {
                    assignedServer.CurrentSection.RemoveServer(assignedServer);
                }
            }
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
            this.Size = new System.Drawing.Size(this.Width, 57 + flowServerSelect.Height);
        }

        private void cboDiningArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFloorplanServers();
            GetFormSize();
        }

        public void UpdateSection(Section section)
        {
            RefreshServerButtonProperties();
        }

        public void UpdateShift(Shift shift)
        {
            throw new NotImplementedException();
        }

        //private void pbClose_Click(object sender, EventArgs e)
        //{


        //    CloseClicked?.Invoke(this, EventArgs.Empty);
        //}
    }
}
