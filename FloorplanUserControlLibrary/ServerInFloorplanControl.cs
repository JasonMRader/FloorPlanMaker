using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorPlanMakerUI;
using FloorplanUserControlLibrary.Properties;
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
    public partial class ServerInFloorplanControl : UserControl, IServerObserver
    {
        public ServerInFloorplanControl(Server server, Floorplan floorplan, FlowLayoutPanel flowPanel)
        {
            InitializeComponent();
            _server = server;
            _floorplan = floorplan;
            _flowLayoutPanel = flowPanel;
            server.Subscribe(this);
            SetControlProperties();
            //subscribeToSectionEvents();
            DisplayShifts();
        }

        private void SetControlProperties()
        {
            btnServer.Text = _server.ToString();
            btnServer.Tag = _server;
            SetButtonBackColor();
            ilcSectionRating.SetProperties(Resources.star, $"Section Rating", _server.PreferedSectionWeight.ToString());
            ilcCloseRating.SetProperties(Resources.CloseBlack, "Close Rating", _server.CloseFrequency.ToString());
            ilcTeamWaitRating.SetProperties(Resources.waiters_28, "TeamWait Rating", _server.TeamWaitFrequency.ToString());
        }
        
        //private void subscribeToSectionEvents()
        //{
        //    //foreach (Section section in _floorplan.Sections)
        //    //{
        //    //    section.ServerAssigned += OnServerAssignedToSection;
        //    //    section.ServerRemoved += OnServerRemovedFromSection;
        //    //}
        //}
        private Floorplan _floorplan { get; set; }
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
                    //if (_server != null && _server.CurrentSection != null)
                    //{

                    //    _server.CurrentSection.ServerAssigned -= OnServerAssignedToSection;
                    //    _server.CurrentSection.ServerRemoved -= OnServerRemovedFromSection;
                    //}

                    _server = value;

                    //if (_server != null && _server.CurrentSection != null)
                    //{

                    //    _server.CurrentSection.ServerAssigned += OnServerAssignedToSection;
                    //    _server.CurrentSection.ServerRemoved += OnServerRemovedFromSection;
                    //}


                }
            }
        }

        //private void OnServerCurrentSectionChanged(Section section)
        //{
        //    if (this.Server.CurrentSection == null)
        //    {
        //        //this.Section = null;
        //        //this.Label.BackColor = UITheme.ButtonColor;
        //    }
        //    else
        //    {
        //        // this.Section = section;
        //        //this.Label.BackColor = section.Color;
        //    }
        //}
        //private void OnServerAssignedToSection(Server server, Section section)
        //{

        //    if (server == this.Server)
        //    {
        //        this.Section = section;
        //        this.UpdateSection(section);
        //    }
        //}

        //private void OnServerRemovedFromSection(Server server, Section section)
        //{

        //    if (server == this.Server)
        //    {
        //        this.Section = null;
        //        //this.Label.BackColor = UITheme.ButtonColor;
        //        //this.Label.ForeColor = Color.Black;
        //    }
        //}

        

        public Section? Section { get; set; }
        
        public void UpdateSection(Section section)
        {
            this.btnServer.BackColor = section.Color;
            this.btnServer.ForeColor = section.FontColor;

        }


        public List<ShiftControl> ShiftControls = new List<ShiftControl>();

        public void DisplayShifts(int maxShiftsToShow = 5)
        {
            

            float OutsidePercentage = 0f;

            if (this.Server.Shifts != null)
            {
                var lastShifts = this.Server.Shifts.Take(maxShiftsToShow);
                lastShifts = lastShifts.OrderBy(s => s.Date).ToList();

                foreach (var shift in lastShifts)
                {
                    ShiftControl shiftControl = new ShiftControl(shift, this.Width / 8, 80);
                    this.ShiftControls.Add(shiftControl);
                    this.flowShiftDisplay.Controls.Add(shiftControl);
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
               
            }
        }
        
        public void OnServerSectionChange(Server server, Section section)
        {
            SetButtonBackColor();
        }
        private void SetButtonBackColor()
        {
            if (Server.CurrentSection!= null)
            {
                btnServer.BackColor = Server.CurrentSection.Color;
            }
            else
            {
                btnServer.BackColor = UITheme.ButtonColor;
            }
        }
    }
}
