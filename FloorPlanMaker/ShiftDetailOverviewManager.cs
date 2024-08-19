using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class ShiftDetailOverviewManager : IShiftObserver
    {
        public ShiftDetailOverviewManager(FlowLayoutPanel flowLayoutPanel, Panel panel, bool isLunch, DateOnly dateOnly, 
            RadioButton rdoWeather, RadioButton rdoReservations, RadioButton rdoSalesStats)
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
        public ShiftDetailOverviewManager(FlowLayoutPanel flowWeatherPanel, FlowLayoutPanel flowResoPanel, Panel panel,
            RadioButton rdoWeather, RadioButton rdoReservations, RadioButton rdoSalesStats, Panel pnlSaleStats)
        {
            this.flowHourlyWeather = flowWeatherPanel;
            this.flowResos = flowResoPanel;
            this.shiftDetailsPanel = panel;
            this.pnlStats = pnlSaleStats;
           
            shiftDetailsPanel.Controls.Add(this.ShiftDetailsControl);
            this.rdoWeather = rdoWeather;
            this.rdoReservations = rdoReservations;
            this.rdoStats = rdoSalesStats;
            this.pnlStats.Visible = false;
            rdoWeather.CheckedChanged += rdoViewType_CheckChanged;
            rdoReservations.CheckedChanged += rdoViewType_CheckChanged;
            rdoSalesStats.CheckedChanged += rdoViewType_CheckChanged;
            PopulateFlowPanelForShiftData();


        }

        private void rdoViewType_CheckChanged(object? sender, EventArgs e)
        {
            //PopulateFlowPanelForShiftData();
            if(rdoWeather.Checked)
            {
                flowHourlyWeather.Visible = true; 
                flowResos.Visible = false;
                pnlStats.Visible = false;
            }
            if (rdoReservations.Checked)
            {
                flowHourlyWeather.Visible = false;
                flowResos.Visible = true;
                pnlStats.Visible = false;
            }
            if (rdoStats.Checked)
            {
                pnlStats.Visible = true;
                flowResos.Visible = false;
                flowHourlyWeather.Visible = false;
            }
        }
        private void PopulateFlowPanelForShiftData()
        {
            PopulateWeatherControlsForDateAndShift();
            PopulateReservationControlsForDateAndShift();
            //if (rdoWeather.Checked)
            //{

            //}
            //else if (rdoReservations.Checked)
            //{
                
            //}
        }

       
        private RadioButton rdoWeather {  get; set; }
        private RadioButton rdoReservations { get; set; }  
        private RadioButton rdoStats { get; set; }
        private Panel pnlStats { get; set; }    
        private Panel shiftDetailsPanel { get; set; }
        private ShiftDetailsControl ShiftDetailsControl { get; set; } = new ShiftDetailsControl();
        private FlowLayoutPanel flowHourlyWeather { get; set; }
        private FlowLayoutPanel flowResos { get; set; }
        private List<HourlyWeatherData> hourlyWeatherDataList { get; set; } = new List<HourlyWeatherData>();
        private List<HourlyWeatherDisplay> hourlyWeatherDisplays { get; set; } = new List<HourlyWeatherDisplay>();
        private bool isLunch { get; set; }
        private DateOnly dateOnly { get; set; }
        private Shift shift { get; set; }
        private async void PopulateWeatherControlsForDateAndShift()
        {
            // Clear any previous data and fetch new weather data
            hourlyWeatherDataList = await HourlyWeatherDataHandler.GetWeatherForShift(dateOnly, isLunch);
            CreateHourlyWeatherControls();
            UpdateWeatherControls();
        }

        private void CreateHourlyWeatherControls()
        {
            // Ensure there are enough controls for the hourly weather data
            while (this.hourlyWeatherDisplays.Count < hourlyWeatherDataList.Count)
            {
                HourlyWeatherDisplay weatherDisplay = new HourlyWeatherDisplay();
                this.hourlyWeatherDisplays.Add(weatherDisplay);
                this.flowHourlyWeather.Controls.Add(weatherDisplay);
            }
        }

        private void UpdateWeatherControls()
        {
            for (int i = 0; i < hourlyWeatherDisplays.Count; i++)
            {
                if (i < hourlyWeatherDataList.Count)
                {
                    hourlyWeatherDisplays[i].AssignNewWeatherData(hourlyWeatherDataList[i]);
                    hourlyWeatherDisplays[i].Visible = true;
                }
                else
                {
                    hourlyWeatherDisplays[i].Visible = false;
                }
            }
        }

        
        private async void PopulateReservationControlsForDateAndShift()
        {          
           
            flowResos.Controls.Clear();
            Label label = new Label
            {
                AutoSize = false,
                Size = new Size(flowHourlyWeather.Width, flowHourlyWeather.Height),
                TextAlign = ContentAlignment.MiddleCenter,
                Text = "Reservation Data Integration Coming Soon!",
                Font = UITheme.LargeFont,
            };
            flowResos.Controls.Add(label);
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
