using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class ShiftDetailOverviewManager
    {
        public ShiftDetailOverviewManager(FlowLayoutPanel flowLayoutPanel, Panel panel, bool isLunch, DateOnly dateOnly)
        {
            this.flowHourlyWeather = flowLayoutPanel;
            this.shiftDetailsPanel = panel;
            this.isLunch = isLunch;
            this.dateOnly = dateOnly;
            InitializeControlsForDateAndShift();
            shiftDetailsPanel.Controls.Add(this.ShiftDetailsControl);
        }
        public ShiftDetailOverviewManager(FlowLayoutPanel flowLayoutPanel, Panel panel)
        {
            this.flowHourlyWeather = flowLayoutPanel;
            this.shiftDetailsPanel = panel;            
            InitializeControlsForDateAndShift();
            shiftDetailsPanel.Controls.Add(this.ShiftDetailsControl);
        }
        
        public ShiftDetailOverviewManager()
        {
            
        }
        private Panel shiftDetailsPanel { get; set; }
        private ShiftDetailsControl ShiftDetailsControl { get; set; } = new ShiftDetailsControl();
        private FlowLayoutPanel flowHourlyWeather { get; set; }
        private List<HourlyWeatherData> hourlyWeatherDataList { get; set; } = new List<HourlyWeatherData>();
        private List<HourlyWeatherDisplay> hourlyWeatherDisplays { get; set; } = new List<HourlyWeatherDisplay>();
        private bool isLunch { get; set; }
        private DateOnly dateOnly { get; set; }
        private async void InitializeControlsForDateAndShift()
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
        public void UpdateForShift(Shift shift)
        {
            if(shift == null) { return; }
          
            UpdateForNewDate(shift.DateOnly, shift.IsAM);
            this.ShiftDetailsControl.SetLabelsForShift(shift);
        }
        public void UpdateForNewDate(DateOnly dateOnly, bool isLunch)
        {
            this.dateOnly = dateOnly;
            this.isLunch = isLunch;
            InitializeControlsForDateAndShift();
        }
    }
}
