using FloorplanClassLibrary;
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
        }
        private FlowLayoutPanel flowLayoutPanel { get; set; }
        private List<HourlyWeatherData> hourlyWeatherDataList { get; set; } = new List<HourlyWeatherData>();
        private bool isLunch { get; set; }
        private DateOnly dateOnly { get; set; }
    }
}
