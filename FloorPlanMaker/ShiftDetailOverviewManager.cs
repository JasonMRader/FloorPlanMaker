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
        public ShiftDetailOverviewManager(FlowLayoutPanel flowLayoutPanel, bool isLunch, DateOnly dateOnly)
        {
            this.flowLayoutPanel = flowLayoutPanel;
            this.isLunch = isLunch;
            this.dateOnly = dateOnly;
            InitializeControlsForDateAndShift();
        }
        private FlowLayoutPanel flowLayoutPanel { get; set; }
        private List<HourlyWeatherData> hourlyWeatherDataList { get; set; } = new List<HourlyWeatherData>();
        private List<HourlyWeatherDisplay> hourlyWeatherDisplays { get; set; } = new List<HourlyWeatherDisplay>();
        private bool isLunch { get; set; }
        private DateOnly dateOnly { get; set; }
        private async void InitializeControlsForDateAndShift()
        {
            hourlyWeatherDisplays.Clear();
            flowLayoutPanel.Controls.Clear();
            hourlyWeatherDataList = await HourlyWeatherDataHandler.GetWeatherForShift(dateOnly, isLunch);
            foreach(HourlyWeatherData weatherData in hourlyWeatherDataList)
            {
                HourlyWeatherDisplay weatherDisplay = new HourlyWeatherDisplay(weatherData);
                hourlyWeatherDisplays.Add(weatherDisplay);
                flowLayoutPanel.Controls.Add(weatherDisplay);
            }
        }
    }
}
