using FloorplanClassLibrary;
using FloorPlanMakerUI;
using PdfSharp.Charting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FloorplanUserControlLibrary
{
    public partial class ServerSelectionPanel : UserControl, IFloorplanObserver
    {
        private Button serverButtonSender { get; set; }
        private Section _section { get; set; }
        public Section Section { get { return _section; } }
        private Floorplan _floorplan { get; set; }
        public Floorplan Floorplan { get { return _floorplan; } }
        public event Action<Button, Server> NotifyOfButtonToAssign;
        public ServerSelectionPanel(Section section, Floorplan floorplan)
        {
            InitializeComponent();
            _section = section;
            _floorplan = floorplan;
            this.BackColor = section.Color;
            CreateServerButtons();
            SubscribeToAllSectionChanges();
        }
        private void SubscribeToAllSectionChanges()
        {
            foreach(Section section in _floorplan.Sections)
            {
                section.ServerAssigned += RemoveServerAdded;
                section.ServerRemoved += AddNewServerAvailable;
            }
        }

        private void AddNewServerAvailable(Server server, Section arg2)
        {
            Button button = CreateServerButton(server);
            //AddButtonLog("Button Created", button);

            if (server.CurrentSection == null)
            {
                pnlMain.Controls.Add(button);
            }
        }

        private void RemoveServerAdded(Server arg1, Section arg2)
        {
            foreach(Control control in pnlMain.Controls)
            {
                if(control.Tag is  Server serverTag)
                {
                    if(serverTag.CurrentSection != null)
                    {
                        pnlMain.Controls.Remove(control);
                    }
                }
            }
        }

        private void CreateServerButtons()
        {
            if (_floorplan != null)
            {

                pnlMain.Controls.Clear();

                foreach (Server server in _floorplan.Servers)
                {
                    if (server.IsBartender) { continue; }
                    Button button = CreateServerButton(server);
                    //AddButtonLog("Button Created", button);

                    if (server.CurrentSection == null)
                    {
                        pnlMain.Controls.Add(button);
                    }
                    else if (server.CurrentSection != _section)
                    {
                        //flowServerList.Controls.Add(button);
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
            button.Size = new System.Drawing.Size(100, 30);
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.Click += ServerButtonClicked;
            button.Dock = DockStyle.Top;

            return button;
        }

        private void ServerButtonClicked(object? sender, EventArgs e)
        {
            Button button = (Button)sender;
            Server server = button.Tag as Server;
            this._section.AddServer(server);
            if (_section.IsFull)
            {
                this.Visible = false;
            }
            else
            {
                this.Controls.Remove(button);
            }
            NotifyOfButtonToAssign?.Invoke(serverButtonSender, server);

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        internal void SetButton(Button button)
        {
            this.serverButtonSender = button;
        }

        public void UpdateFloorplan(Floorplan floorplan)
        {
            throw new NotImplementedException();
        }
    }
}
