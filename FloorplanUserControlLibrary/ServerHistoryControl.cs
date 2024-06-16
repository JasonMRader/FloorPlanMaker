using FloorplanClassLibrary;
using FloorPlanMaker;
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
    public partial class ServerHistoryControl : UserControl
    {
        public ServerHistoryControl(Server server, DateOnly start, DateOnly end, bool isLunch)
        {
            InitializeComponent();
            this.server = server;
            this.shiftHistory = new ServerShiftHistory(server, start, end, isLunch);
            this.isLunch = isLunch;
            InitializeControls();
            SetShiftControls();
        }
        private bool isLunch;
        private string GetIsLunchDisplay()
        {
            if (this.isLunch) { return "AM"; }
            else { return "PM"; }
        }
        public Server server { get; private set; }
        public ServerShiftHistory shiftHistory { get; private set; }
        private void InitializeControls()
        {
            btnServer.Text = this.server.ToString();

            lblDescription.Text = $"Last {shiftHistory.filteredShifts.Count} {GetIsLunchDisplay()} Shifts";
            lblOutsidePercentage.Text = $"{FormattedPercentage(shiftHistory.OutsidePercentage)} Outside";
        }
        private string FormattedPercentage(float num)
        {
            return $"{(int)(num * 100)}%";
        }
        private void SetShiftControls()
        {
            var lastFiveShifts = shiftHistory.filteredShifts.OrderByDescending(s => s.Date).ToList();
            lastFiveShifts = lastFiveShifts.Take(5).ToList();
            List<ShiftControl> shiftControls = new List<ShiftControl>();
            foreach (var shift in lastFiveShifts)
            {
                ShiftControl shiftControl = new ShiftControl(shift, pnlShift1.Width, pnlShift1.Height);
                shiftControls.Add(shiftControl);
            }
            pnlShift5.Controls.Add(shiftControls[0]);
            pnlShift4.Controls.Add(shiftControls[1]);
            pnlShift3.Controls.Add(shiftControls[2]);
            pnlShift2.Controls.Add(shiftControls[3]);
            pnlShift1.Controls.Add(shiftControls[4]);
        }
        private void ServerHistoryControl_Load(object sender, EventArgs e)
        {

        }
    }
}
