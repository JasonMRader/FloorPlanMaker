using FloorplanClassLibrary;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
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
    public partial class SectionLabel : UserControl, ISectionObserver, IFloorplanObserver
    {
        private Section _section { get; set; }
        private Floorplan _floorplan { get; set; }
        public Section Section { get { return _section; } }
        public Floorplan Floorplan { get { return _floorplan; } }
        private Point MouseDownLocation;
        private bool isDragging = false;
        public event Action<Section> SectionSelected;
        public event EventHandler ServerRemoved;
        public event EventHandler ServerAdded;
        public SectionLabel(Section section, Floorplan floorplan)
        {
            InitializeComponent();
            _section = section;
            _floorplan = floorplan;
            _section.SubscribeObserver(this);
            _floorplan.SubscribeObserver(this);
            AssignClickEvents();
            UpdateControlsForSection();

        }

        private void UpdateControlsForSection()
        {
            SetSectionLabel();
            SetCutOrderImage();
            SetServerButtons();
            pnlAccent.BackColor = Color.White;
            pnlMainContainer.BackColor = Section.Color;
            this.BackColor = Section.Color;
        }

        private void SetServerButtons()
        {
            flowServers.Controls.Clear();
            for(int i = 0; i < _section.ServerCount; i++)
            {
                Server server = null;
                if (Section.ServerTeam.Count > i)
                {
                    server = this._section.ServerTeam[i];
                }
                Button button = CreateServerButton(server);
                flowServers.Controls.Add(button);
            }
        
        }

        private Button CreateServerButton(Server? server)
        {
            Button button = new Button();
            button.MinimumSize = new Size(150, 27);
            button.AutoSize = true;
            
            if (server == null)
            {
                SetButtonToUnassigned(button);
            }
            else
            {
                SetButtonToServer(button, server);
            }
            return button;
        }

        private void SetButtonToUnassigned(Button button)
        {
            UITheme.FormatMainButton(button);
            button.Text = "Empty";
            button.Tag = null;
            button.Click += AssignServer;
        }
        private void SetButtonToServer(Button button, Server server)
        {
            UITheme.FormatCTAButton(button);
            if (!server.isDouble)
            {
                button.Text = server.ToString();
            }
            else
            {
                button.Text = server.ToString() + "*";
            }

            button.Tag = server;
            button.Click += RemoveServer;
        }
        private void RemoveServer(object? sender, EventArgs e)
        {
            ServerRemoved?.Invoke(sender, e);
        }

        private void AssignServer(object? sender, EventArgs e)
        {
            ServerAdded?.Invoke(sender, e);
        }
        private void SetSectionLabel()
        {
            if (_section.IsPickUp)
            {
                lblSectionNumber.Text = "PU";
            }
            else if (_section.IsBarSection)
            {
                lblSectionNumber.Text = "B";
            }
            else
            {
                lblSectionNumber.Text = "#" + _section.Number.ToString();
            }
        }
        private void AssignClickEvents()
        {
            this.MouseDown += SectionLabel_MouseDown;
            this.MouseMove += SectionLabel_MouseMove;
            this.MouseUp += SectionLabel_MouseUp;
            pnlAccent.MouseDown += SectionLabel_MouseDown;
            pnlAccent.MouseUp += SectionLabel_MouseUp;
            pnlAccent.MouseMove += SectionLabel_MouseMove;
            pnlMainContainer.MouseDown += SectionLabel_MouseDown;
            pnlMainContainer.MouseMove += SectionLabel_MouseMove;
            pnlMainContainer.MouseUp += SectionLabel_MouseUp;
            lblSectionNumber.MouseUp += SectionLabel_MouseUp;
            lblSectionNumber.MouseMove += SectionLabel_MouseMove;
            lblSectionNumber.MouseDown += SectionLabel_MouseDown;
            picCutOrder.Click += CycleCutOrder;

        }

        private void CycleCutOrder(object? sender, EventArgs e)
        {
            if (this._section.IsCloser)
            {
                this._section.SetToPre();
            }
            else if (this._section.IsPre)
            {
                this._section.SetToCut();
            }
            else
            {
                this._section.SetToCut();
            }
            SetCutOrderImage();
        }

        private void SetCutOrderImage()
        {
            if (this._section.IsCloser)
            {
                this.picCutOrder.Image = Resources.Close;
            }
            else if (this._section.IsPre)
            {
                this.picCutOrder.Image = Resources.Pre;
            }
            else
            {
                this.picCutOrder.Image = Resources.Scissors__Copy;
            }
        }

        private void SectionLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true; // Start the drag action
                MouseDownLocation = e.Location;
            }
        }

        private void SectionLabel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging && e.Button == MouseButtons.Left)
            {
                // Determine the new position of the control
                this.Left += e.X - MouseDownLocation.X;
                this.Top += e.Y - MouseDownLocation.Y;

                // Optional: Update the parent form or control to reflect the new position
                this.Update();
            }
        }

        private void SectionLabel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false; // End the drag action
            }
        }
        private void btnServerButton_Click(object sender, EventArgs e)
        {

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Section.RemoveObserver(this);
            }
            base.Dispose(disposing);
        }

        public void UpdateSection(Section section)
        {
            throw new NotImplementedException();
        }

       

        public void UpdateFloorplan(Floorplan floorplan)
        {
            throw new NotImplementedException();
        }
    }
}
