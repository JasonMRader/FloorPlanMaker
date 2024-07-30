using FloorplanClassLibrary;
using FloorPlanMakerUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorplanUserControlLibrary
{
    public partial class HourlyWeatherDisplay : UserControl
    {
        public HourlyWeatherData HourlyWeatherData { get; set; }
        public HourlyWeatherDisplay(HourlyWeatherData hourlyWeatherData)
        {
            InitializeComponent();
            this.HourlyWeatherData = hourlyWeatherData;
            SetLabelsForWeatherData();
            

        }
        private void SetLabelsForWeatherData()
        {
            
           
            UITheme.FormatTempLabelColor(this.lblFeelsLikeHi, this.HourlyWeatherData.FeelsLikeHi);
            UITheme.FormatTempLabelColor(this.lblFeelsLikeLow, this.HourlyWeatherData.FeelsLikeLow);
            UITheme.FormateWindLabel(this.lblWindAvg, this.HourlyWeatherData.WindSpeedAvg);
            UITheme.FormateWindLabel(this.lblWindMax, this.HourlyWeatherData.WindSpeedMax);
            UITheme.FormatePrecipAmountLabel(this.lblRainAmount, this.HourlyWeatherData.PrecipitationAmount);
            UITheme.FormatePrecipChanceLabel(this.lblChanceOfRain, this.HourlyWeatherData.PrecipitationChanceFormatted);
            this.lblWindAvg.Text += "MPH";
            this.lblWindMax.Text += "MPH";
        }
       
    }
}
