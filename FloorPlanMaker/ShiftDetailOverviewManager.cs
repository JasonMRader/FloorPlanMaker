using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class ShiftDetailOverviewManager : IShiftObserver
    {
        public ShiftDetailOverviewManager(FlowLayoutPanel flowLayoutPanel, Panel panel, bool isLunch, DateOnly dateOnly, RadioButton rdoWeather, RadioButton rdoReservations)
        {
            this.flowHourlyWeather = flowLayoutPanel;
            this.shiftDetailsPanel = panel;
            this.isLunch = isLunch;
            this.dateOnly = dateOnly;
            this.rdoWeather = rdoWeather;
            this.rdoReservations = rdoReservations;
            rdoWeather.CheckedChanged += rdoViewType_CheckChanged;
            rdoReservations.CheckedChanged += rdoViewType_CheckChanged;
            PopulateFlowPanelForShiftData();
            shiftDetailsPanel.Controls.Add(this.ShiftDetailsControl);
        }
        public ShiftDetailOverviewManager(FlowLayoutPanel flowLayoutPanel, Panel panel, RadioButton rdoWeather, RadioButton rdoReservations)
        {
            this.flowHourlyWeather = flowLayoutPanel;
            this.shiftDetailsPanel = panel;
           
            shiftDetailsPanel.Controls.Add(this.ShiftDetailsControl);
            this.rdoWeather = rdoWeather;
            this.rdoReservations = rdoReservations;
            rdoWeather.CheckedChanged += rdoViewType_CheckChanged;
            rdoReservations.CheckedChanged += rdoViewType_CheckChanged;
            PopulateFlowPanelForShiftData();


        }

        private void rdoViewType_CheckChanged(object? sender, EventArgs e)
        {
            PopulateFlowPanelForShiftData();
        }
        private void PopulateFlowPanelForShiftData()
        {
            if (rdoWeather.Checked)
            {
                PopulateWeatherControlsForDateAndShift();
            }
            else if (rdoReservations.Checked)
            {
                PopulateReservationControlsForDateAndShift();
            }
        }

        public ShiftDetailOverviewManager()
        {
            
        }
        private RadioButton rdoWeather {  get; set; }
        private RadioButton rdoReservations { get; set; }   
        private Panel shiftDetailsPanel { get; set; }
        private ShiftDetailsControl ShiftDetailsControl { get; set; } = new ShiftDetailsControl();
        private FlowLayoutPanel flowHourlyWeather { get; set; }
        private List<HourlyWeatherData> hourlyWeatherDataList { get; set; } = new List<HourlyWeatherData>();
        private List<HourlyWeatherDisplay> hourlyWeatherDisplays { get; set; } = new List<HourlyWeatherDisplay>();
        private bool isLunch { get; set; }
        private DateOnly dateOnly { get; set; }
        private Shift shift { get; set; }
        private async void PopulateWeatherControlsForDateAndShift()
        {
            //shiftDetailsPanel.Controls.Clear();
            hourlyWeatherDisplays.Clear();
            flowHourlyWeather.Controls.Clear();
            hourlyWeatherDataList = await HourlyWeatherDataHandler.GetWeatherForShift(dateOnly, isLunch);
            foreach(HourlyWeatherData weatherData in hourlyWeatherDataList)
            {
                HourlyWeatherDisplay weatherDisplay = new HourlyWeatherDisplay(weatherData);
                hourlyWeatherDisplays.Add(weatherDisplay);
                flowHourlyWeather.Controls.Add(weatherDisplay);
            }
        }
        private async void PopulateReservationControlsForDateAndShift()
        {          
           
            flowHourlyWeather.Controls.Clear();
            Label label = new Label
            {
                AutoSize = false,
                Size = new Size(flowHourlyWeather.Width, flowHourlyWeather.Height),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Reservation Data Integration Coming Soon!",
                Font = UITheme.LargeFont,
            };
            flowHourlyWeather.Controls.Add(label);
        }
        public void SetNewShift(Shift shift)
        {
            if(shift == null) { return; }
            if(this.shift != null)
            {
                this.shift.RemoveObserver(this);
            }
            this.shift = shift;
            this.shift.SubscribeObserver(this);
            UpdateForNewDate(shift.DateOnly, shift.IsAM);
            this.ShiftDetailsControl.SetLabelsForShift(shift);
        }
        public void UpdateForNewDate(DateOnly dateOnly, bool isLunch)
        {
            if(this.dateOnly != dateOnly || this.isLunch != isLunch)
            {
                this.dateOnly = dateOnly;
                this.isLunch = isLunch;
                PopulateFlowPanelForShiftData();
            }
            
        }

        public void UpdateShift(Shift shift)
        {
            UpdateForNewDate(shift.DateOnly, shift.IsAM);
            this.ShiftDetailsControl.SetLabelsForShift(shift);
        }
    }
}
