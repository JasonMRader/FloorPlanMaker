using FloorplanClassLibrary;
using FloorPlanMaker;
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

namespace FloorplanUserControlLibrary
{
    public partial class ServerInFloorplanControl : UserControl
    {
        public ServerInFloorplanControl(Server server, Floorplan floorplan, FlowLayoutPanel flowPanel)
        {
            InitializeComponent();
        }
        private void subscribeToSectionEvents(List<Section> sections)
        {
            foreach (Section section in sections)
            {
                section.ServerAssigned += OnServerAssignedToSection;
                section.ServerRemoved += OnServerRemovedFromSection;
            }
        }
        private Floorplan _floorplan {  get; set; }
        private FlowLayoutPanel _flowLayoutPanel;
        public Floorplan Floorplan { get { return _floorplan; } }
        public FlowLayoutPanel FlowLayoutPanel { get { return _flowLayoutPanel; } }
        private Server _server;
        public Server Server
        {
            get => _server;
            set
            {
                if (_server != value)
                {
                    if (_server != null && _server.CurrentSection != null)
                    {

                        _server.CurrentSection.ServerAssigned -= OnServerAssignedToSection;
                        _server.CurrentSection.ServerRemoved -= OnServerRemovedFromSection;
                    }

                    _server = value;

                    if (_server != null && _server.CurrentSection != null)
                    {

                        _server.CurrentSection.ServerAssigned += OnServerAssignedToSection;
                        _server.CurrentSection.ServerRemoved += OnServerRemovedFromSection;
                    }


                }
            }
        }

        private void OnServerCurrentSectionChanged(Section section)
        {
            if (this.Server.CurrentSection == null)
            {
                //this.Section = null;
                //this.Label.BackColor = UITheme.ButtonColor;
            }
            else
            {
                // this.Section = section;
                //this.Label.BackColor = section.Color;
            }
        }
        private void OnServerAssignedToSection(Server server, Section section)
        {

            if (server == this.Server)
            {
                this.Section = section;
                this.UpdateSection(section);
            }
        }

        private void OnServerRemovedFromSection(Server server, Section section)
        {

            if (server == this.Server)
            {
                this.Section = null;
                //this.Label.BackColor = UITheme.ButtonColor;
                //this.Label.ForeColor = Color.Black;
            }
        }

        public FlowLayoutPanel ShiftsDisplay { get; set; }

        public Section? Section { get; set; }
        public Button RemoveButton { get; set; }
        public void UpdateSection(Section section)
        {
            this.btnServer.BackColor = section.Color;
            this.btnServer.ForeColor = section.FontColor;

        }


        public List<ShiftControl> ShiftControls = new List<ShiftControl>();

        public void DisplayShifts(int maxShiftsToShow = 5)
        {
            ShiftsDisplay = new FlowLayoutPanel
            {
                Height = this.Height,
                Width = this.Width,
                AutoSize = true,
                Margin = new Padding(0)
            };
            this.Controls.Add(ShiftsDisplay);

            float OutsidePercentage = 0f;

            if (this.Server.Shifts != null)
            {
                var lastShifts = this.Server.Shifts.Take(maxShiftsToShow);
                lastShifts = lastShifts.OrderBy(s => s.Date).ToList();

                foreach (var shift in lastShifts)
                {
                    ShiftControl shiftControl = new ShiftControl(shift, this.Width / 8, 80);
                    this.ShiftControls.Add(shiftControl);
                    this.ShiftsDisplay.Controls.Add(shiftControl);
                }
                var lastShiftsForPercentage = this.Server.Shifts.Take(10);
                int OutsideShifts = 0;
                foreach (var shift in lastShiftsForPercentage)
                {
                    if (!shift.IsInside)
                    {
                        OutsideShifts += 1;
                    }
                }
                //OutsidePercentage = (float)OutsideShifts / (float)lastShiftsForPercentage.Count();
                //string formattedPercentage = $"{(int)(OutsidePercentage * 100)}%";
                //this.lblOutsidePercentage.Text = $"Last {lastShiftsForPercentage.Count()}: {formattedPercentage}";
                string serverRatingDisplay =
                    $"Section:       {this.Server.PreferedSectionWeight}\n" +
                    $"TeamWait:  {this.Server.TeamWaitFrequency}\n" +
                    $"Close:           {this.Server.CloseFrequency}";
                //this.lblOutsidePercentage.Text = serverRatingDisplay;
                //this.lblOutsidePercentage.Font = UITheme.SmallerFont;
                //this.lblOutsidePercentage.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
                //this.lblOutsidePercentage.Margin = new Padding(10, 4, 0, 0);
                //this.lblOutsidePercentage.AutoSize = false;
                // this.lblOutsidePercentage.Size = new Size(90, 50);
                //ShiftsDisplay.Controls.Add(this.lblOutsidePercentage);
            }
        }
        public void HideShifts()
        {
            this.ShiftsDisplay.AutoSize = false;
            this.ShiftsDisplay.MaximumSize = new Size(this.Width, 0);
        }
        public void ShowShifts()
        {
            this.ShiftsDisplay.AutoSize = true;
        }
    }
}
