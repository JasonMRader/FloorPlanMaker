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
            SetShiftsForServer();

            //subscribeToSectionEvents();
            DisplayShifts();
        }
        private void SetShiftsForServer()
        {
            if (_server.Shifts.Count == 0)
            {
                _server.Shifts = SqliteDataAccess.GetShiftsForServer(_server);
                DateOnly start = DateOnly.FromDateTime(DateTime.Now.AddDays(-90));
                DateOnly end = _floorplan.DateOnly;
                GetShiftsForDateRangeAndIsLunch(start, end, _floorplan.IsLunch);
            }

        }

        private void SetControlProperties()
        {
            btnServer.Text = _server.ToString();
            btnServer.Tag = _server;
            SetButtonBackColor();
            ilcSectionRating.SetProperties(Resources.star, $"Section Rating", _server.PreferedSectionWeight.ToString());
            ilcCloseRating.SetProperties(Resources.CloseBlack, "Close Rating", _server.CloseFrequency.ToString());
            ilcTeamWaitRating.SetProperties(Resources.waiters_28, "TeamWait Rating", _server.TeamWaitFrequency.ToString());
            this.Margin = new Padding(10, 3, 0, 0);
        }


        private Floorplan _floorplan { get; set; }
        private FlowLayoutPanel _flowLayoutPanel;
        public Floorplan Floorplan { get { return _floorplan; } }
        public FlowLayoutPanel FlowLayoutPanel { get { return _flowLayoutPanel; } }
        private Server _server;
        private List<EmployeeShift> filteredShifts = new List<EmployeeShift>();
        public Server Server
        {
            get => _server;
            set
            {
                if (_server != value)
                {


                    _server = value;
                }
            }
        }

        public Section? Section { get; set; }

        public void UpdateSection(Section section)
        {
            this.btnServer.BackColor = section.Color;
            this.btnServer.ForeColor = section.FontColor;

        }


        public List<ShiftControl> ShiftControls = new List<ShiftControl>();
        public void GetShiftsForDateRangeAndIsLunch(DateOnly start, DateOnly end, bool isLunch)
        {
            List<EmployeeShift> employeeShifts = new List<EmployeeShift>();

            foreach (EmployeeShift shift in this.Server.Shifts)
            {
                DateOnly shiftDate = DateOnly.FromDateTime(shift.Date);
                if (shiftDate >= start && shiftDate <= end && shift.isLunch == isLunch)
                {
                    employeeShifts.Add(shift);
                }
            }

            filteredShifts = employeeShifts;
        }
        public void DisplayShifts(int maxShiftsToShow = 5)
        {

            if (filteredShifts != null)
            {
                var lastShifts = filteredShifts.Take(maxShiftsToShow);
                lastShifts = lastShifts.OrderBy(s => s.Date).ToList();

                foreach (var shift in lastShifts)
                {
                    ShiftControl shiftControl = new ShiftControl(shift, this.flowShiftDisplay.Width / 5, flowShiftDisplay.Height);
                    this.ShiftControls.Add(shiftControl);
                    this.flowShiftDisplay.Controls.Add(shiftControl);
                }


            }
        }

        public void OnServerSectionChange(Server server, Section section)
        {
            SetButtonBackColor();
        }
        private void SetButtonBackColor()
        {
            if (Server.CurrentSection != null)
            {
                btnServer.BackColor = Server.CurrentSection.Color;
                btnServer.ForeColor = Server.CurrentSection.FontColor;
            }
            else
            {
                btnServer.BackColor = UITheme.ButtonColor;
                btnServer.ForeColor = Color.Black;
            }
        }

        private void btnServer_Click(object sender, EventArgs e)
        {
            if(_floorplan.SectionSelected != null)
            {
                if(_floorplan.SectionSelected.Server == null || _floorplan.SectionSelected.IsTeamWait)
                {
                    _floorplan.SectionSelected.AddServer(_server);
                }
                else if(_floorplan.SectionSelected.Server != _server)
                {
                    _floorplan.SwapTwoServers(_floorplan.SectionSelected.Server, _server);
                }
               
            }
           
        }
    }
}
