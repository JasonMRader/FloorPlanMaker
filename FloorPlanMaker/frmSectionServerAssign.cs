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
    public partial class frmSectionServerAssign : Form, IShiftObserver, ISectionObserver
    {
        private Section section { get; set; }
        private Shift shift { get; set; }
        private Floorplan floorplan { get; set; }
        public event EventHandler CloseClicked;
        public event EventHandler ServerAssignedClicked;
        public event EventHandler SignalForInvisible;
        private List<Button> serverButtons = new List<Button>();
        private List<string> log = new List<string>();
        private List<string> serverLogs = new List<string>();
        private int logCount = 0;
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
            flowServersInFloorplan.BackColor = Color.WhiteSmoke;
            flowServersInSection.BackColor = Color.WhiteSmoke;

        }

        private void AddServerLogFromButton(Button button, string description)
        {
            logCount ++;
            if(button.Tag != null)
            {
                if(button.Tag is Server server)
                {
                    string sectionString = "";
                    if (server.CurrentSection != null)
                    { sectionString = server.CurrentSection.Number.ToString(); }
                    else
                    {
                        sectionString = "NO SECTION";
                    }
                    serverLogs.Add($"\n{logCount}) btn: {server}: {sectionString}| {description}");
                }
                else
                {
                    serverLogs.Add($"\n{logCount}) btn: WTF | {description}");
                }
                
            }
            else
            {
                serverLogs.Add($"\n{logCount}) btn: NULL TAG | {description}");
            }

           
            
        }
        private void AddServerLogFromServer(Server server, string description)
        {
            logCount ++;
            if (server != null)
            {
                string sectionString = "";
                if (server.CurrentSection != null)
                { sectionString = server.CurrentSection.Number.ToString(); }
                else
                {
                    sectionString = "NO SECTION";
                }
                serverLogs.Add($"\n{logCount}) {server}: {sectionString} | {description}");
                
               
            }
            else
            {
                serverLogs.Add($"\n {logCount}) NULL SERVER | {description}");
            }
           

        }
        private void AddServerLogWithSection(Server server, string description, Section originalSection = null)
        {
           
            originalSection ??= server.CurrentSection;

            string originalSectionString = originalSection != null ? originalSection.Number.ToString() : "NO SECTION";
            string currentSectionString = server.CurrentSection != null ? server.CurrentSection.Number.ToString() : "NO SECTION";

            serverLogs.Add($"\n{logCount}) {server}: {originalSectionString} -> {currentSectionString} | {description}");
        }


        public void SetNewSectionAndShift(Section section, Shift shift)
        {
            //if(this.section != null)
            //{
            //    this.section.RemoveObserver(this);
            //}
            this.section = section;
            //this.section.SubscribeObserver(this);
            this.shift = shift;
            this.floorplan = shift.SelectedFloorplan;
            this.pnlSectionColor.BackColor = section.Color;
            flowServersInFloorplan.BackColor = Color.WhiteSmoke;
            flowServersInSection.BackColor = Color.WhiteSmoke;
            PopulateCboAreas();
        }

        private void frmSectionServerAssign_Load(object sender, EventArgs e)
        {
            PopulateCboAreas();

        }
        private void AddServerLogs()
        {
            foreach (Button button in serverButtons)
            {
                if (button.Tag is Server server)
                {
                    string logString = server.DisplayName + ": " + server.serversCurrentSectionDisplay;

                    log.Add(logString);
                }
                else
                {
                    log.Add("A button is Missing a Server");
                }
            }
        }
        private void AddButtonLog(string description, Button button)
        {
            log.Add("\n");

            if (button.Tag is Server)
            {
                Server taggedServer = button.Tag as Server;
                
                if (taggedServer != null)
                {
                    string sectionString = "";
                    if(taggedServer.CurrentSection != null)
                    { sectionString = taggedServer.CurrentSection.Number.ToString(); }
                    log.Add($"{description} \n" +
                    $"[{button.Text}]: {taggedServer}: {sectionString}");
                }
                else
                {
                    log.Add($"{description} \n" +
                    $"[{button.Text}]: {taggedServer}: WTF");
                }
            }
            else
            {
                log.Add($"{description}  \n" +
                   $"[{button.Text}]: TAG IS NOT SERVER");
            }

        }
        private void PopulateFloorplanServers()
        {
            Floorplan floorplanSelected = (Floorplan)cboDiningArea.SelectedItem;
            //log.Add("Servers Added: ");
            if (floorplanSelected != null)
            {
                flowServersInFloorplan.Controls.Clear();
                flowServersInSection.Controls.Clear();
                serverButtons.Clear();
                foreach (Server server in floorplanSelected.Servers)
                {
                    if (server.IsBartender) { continue; }
                    Button button = CreateServerButton(server);
                    //AddButtonLog("Button Created", button);

                    serverButtons.Add(button);
                    if (server.CurrentSection != section)
                    {
                        flowServersInFloorplan.Controls.Add(button);
                    }
                    else
                    {
                        flowServersInSection.Controls.Add(button);
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
                if (server.CurrentSection == this.section)
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
            button.Size = new System.Drawing.Size(flowServersInFloorplan.Width - 10, 30);
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Click += ServerButtonClicked;

            return button;
        }

        private void RefreshAllServerButtonProperties()
        {
            foreach (Button button in serverButtons)
            {
                RefreshSingleButtonsProperties(button);
            }
        }
        private void RefreshSingleButtonsProperties(Button button)
        {
            Server server = button.Tag as Server;
            //button.Click -= SwapServerButtonClicked;
            //button.Click -= CancelSwap;
            //button.Click -= ServerButtonClicked;
            button.Visible = true;
            button.Enabled = true;
            if (server.CurrentSection == null)
            {
                button.BackColor = Color.Gray;
                button.ForeColor = Color.Black;
                button.Text = server.ToString();
                MoveButtonToFloorplanServers(button);
            }
            else
            {
                button.BackColor = server.CurrentSection.Color;
                button.ForeColor = server.CurrentSection.FontColor;
                if (server.CurrentSection == this.section)
                {
                    MoveButtonToSectionServers(button);
                    button.Text = "Remove " + server.ToString();

                }
                else
                {
                    MoveButtonToFloorplanServers(button);
                    button.Text = server.ToString();
                }
            }
            //AddButtonLog("Button Refreshed", button);
            //button.Click += ServerButtonClicked;
        }
        private void MoveButtonToSectionServers(Button button)
        {
            if (flowServersInFloorplan.Contains(button))
            {
                flowServersInFloorplan.Controls.Remove(button);
            }
            if (!flowServersInSection.Contains(button))
            {
                flowServersInSection.Controls.Add(button);
            }
        }
        private void MoveButtonToFloorplanServers(Button button)
        {
            if (flowServersInSection.Contains(button))
            {
                flowServersInSection.Controls.Remove(button);
            }
            if (!flowServersInFloorplan.Contains(button))
            {
                flowServersInFloorplan.Controls.Add(button);
            }

        }

        private void ServerButtonClicked(object? sender, EventArgs e)
        {

            
            var clickedButton = (Button)sender;
            AddServerLogFromButton(clickedButton, "DEFAULT");
            AddButtonLog("__Default CLICK", clickedButton);
            AddServerLogs();
            if (!section.IsTeamWait)
            {
                SoloSectionServerButtonClicked(clickedButton);
                RefreshAllServerButtonProperties();
            }
            else if (section.IsTeamWait)
            {
                bool isSwaping = TeamSectionServerButtonClicked(clickedButton);
                if (!isSwaping)
                {
                    RefreshAllServerButtonProperties();
                }
            }
            AddButtonLog("__POST Default CLICK", clickedButton);

            AddServerLogs();
            // RefreshServerButtonProperties();
            AddServerLogFromButton(clickedButton, "POST DEFAULT");


        }

        private bool TeamSectionServerButtonClicked(Button? clickedButton)
        {
            AddButtonLog("____TEAM SectionClicked METHOD", clickedButton);
            AddServerLogFromButton(clickedButton, "TEAM METHOD");
            bool isSwappingTeamSections = false;
            var clickedServer = (Server)clickedButton.Tag;

            if (clickedServer.CurrentSection != null)
            {
                if (clickedServer.CurrentSection == this.section)
                {
                    clickedServer.CurrentSection.RemoveServer(clickedServer);
                    AddServerLogFromServer(clickedServer, "Removed FROM section because was CLicked ");
                    return false;
                }
            }
            if (!section.IsFull)
            {
                if (clickedServer.CurrentSection != null)
                {
                    clickedServer.CurrentSection.RemoveServer(clickedServer);
                    AddServerLogFromServer(clickedServer, "");
                }
                AddServerLogFromServer(clickedServer, "Server added to NONfull TeamSection");
                section.AddServer(clickedServer);
                if (section.ServerCount == section.ServerTeam.Count)
                {
                    SignalForInvisible?.Invoke(this, EventArgs.Empty);
                    return false;
                }
            }
            else if (section.IsFull)
            {

                if (!section.ServerTeam.Contains(clickedServer))
                {
                    ChooseThisSectionsTeamServerToSwap(clickedServer);
                    return true;

                }
            }
            AddButtonLog("____POST TEAM clicked METHOD", clickedButton);
            AddServerLogFromButton(clickedButton, " POST TEAM METHOD");
            return isSwappingTeamSections;
        }
        private Server serverToSwap { get; set; }
        private void ChooseThisSectionsTeamServerToSwap(Server serverClicked)
        {
            AddServerLogFromServer(serverClicked, "Server Swap Initiated");
            log.Add("___CHOOSING SERVERS");
            serverToSwap = serverClicked;
            foreach (Button button in serverButtons)
            {
                Server server = button.Tag as Server;

                if (server != serverClicked && !section.ServerTeam.Contains(server))
                {
                    button.Enabled = false;
                    continue;
                }
                if (section.ServerTeam.Contains(server))
                {
                    button.Text = "Swap Out " + server.ToString();
                    button.Click -= ServerButtonClicked;
                    button.Click += SwapServerButtonClicked;
                    AddServerLogFromButton(button, "SERVER CHANGED TO SWAP OUT");
                }
                if (server == serverClicked)
                {

                    button.Text = server.ToString() + " (Cancel Swap)";
                    button.Click -= ServerButtonClicked;
                    button.Click += CancelSwap;
                    AddServerLogFromButton(button, "SERVER CHANGED CANCEL");
                }

            }
            log.Add("___POST CHOOSING SERVERS");
        }


        private void CancelSwap(object? sender, EventArgs e)
        {
            Button button = sender as Button;
            AddButtonLog("___CANCEL ServerButtonClicked METHOD", button);
            serverToSwap = null;
            RefreshAllServerButtonProperties();
            button.Click += ServerButtonClicked;
            button.Click -= CancelSwap;
            AddServerLogFromButton(button, "SERVER CHANGED TO DEFAULT FROM CANCEL");
            AddButtonLog("___POST CANCEL METHOD", button);
        }

        private void SwapServerButtonClicked(object? sender, EventArgs e)
        {

            var button = (Button)sender;
            AddButtonLog("___SWAP ServerButtonClicked METHOD", button);
            Server serverChoosen = (Server)button.Tag;
            floorplan.SwapTwoServers(serverToSwap, serverChoosen);
            RefreshAllServerButtonProperties();
            foreach (Button btn in flowServersInSection.Controls)
            {
                button.Click += ServerButtonClicked;
                button.Click -= SwapServerButtonClicked;
                AddServerLogFromButton(button, "SERVER CHANGED TO DEFAULT FROM SWAP OUT");
            }

            AddButtonLog("___POST SWAP ServerButtonClicked METHOD", button);


        }
        private void SoloSectionServerButtonClicked(Button clickedButton)
        {
            AddButtonLog("___SOLO SectionClicked METHOD", clickedButton);
            AddServerLogFromButton(clickedButton, "SOLO METHOD START");
            var clickedServer = (Server)clickedButton.Tag;
            if (clickedServer.CurrentSection == null)
            {
                //This Section Already Has a server
                if (section.Server != null)
                {

                    section.RemoveServer(section.Server);
                    AddServerLogFromServer(section.Server, "REMOVED");
                }
                section.AddServer(clickedServer);
                AddServerLogFromServer(clickedServer, "ADDED");
                SignalForInvisible?.Invoke(this, EventArgs.Empty);

            }
            //Server clicked DOES have a Section AND this Section has a Server AND ServerTeam does not contain the Server Clicked
            else if (clickedServer.CurrentSection != null
                && section.Server != null
                && !section.ServerTeam.Contains(clickedServer))
            {
                AddServerLogFromServer(section.Server, "SWAP server in section");
                AddServerLogFromServer(clickedServer, "SWAP server Clicked");
                floorplan.SwapTwoServers(section.Server, clickedServer);
                SignalForInvisible?.Invoke(this, EventArgs.Empty);

            }
            //CLicked server Does have a Section AND this section does not have a server
            else if (clickedServer.CurrentSection != null && section.Server == null)
            {

                clickedServer.CurrentSection.RemoveServer(clickedServer);
                section.AddServer(clickedServer);
                AddServerLogFromServer(clickedServer, "Removed from current section and added to new empty one");
                SignalForInvisible?.Invoke(this, EventArgs.Empty);
            }
            else if (clickedServer.CurrentSection != null)
            {
                //THis server is in this section
                if (clickedServer.CurrentSection == this.section)
                {
                    clickedServer.CurrentSection.RemoveServer(clickedServer);
                    AddServerLogFromServer(clickedServer, "Server Removed From Current Section");
                    //RefreshSingleButtonsProperties(clickedButton);
                }
            }
            AddButtonLog("___POST SOLO METHOD", clickedButton);
            AddServerLogFromButton(clickedButton, "POST SOLO METHOD ");
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
            this.Size = new System.Drawing.Size(this.Width, 115 + flowServersInFloorplan.Height + flowServersInSection.Height);
        }

        private void cboDiningArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateFloorplanServers();
            //GetFormSize();
        }

        public void UpdateSection(Section section)
        {
            //RefreshAllServerButtonProperties();
        }

        public void UpdateShift(Shift shift)
        {
            throw new NotImplementedException();
        }

        private void frmSectionServerAssign_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                string logRecords = "";
                foreach (string s in serverLogs)
                {
                    logRecords += s;
                }
                MessageBox.Show(logRecords);
                serverLogs.Clear();
            }

        }

        //private void pbClose_Click(object sender, EventArgs e)
        //{


        //    CloseClicked?.Invoke(this, EventArgs.Empty);
        //}
    }
}
