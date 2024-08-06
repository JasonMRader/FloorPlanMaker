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
        public void AssignNewWeatherData(HourlyWeatherData hourlyWeatherData)
        {
            this.HourlyWeatherData = hourlyWeatherData;
            SetLabelsForWeatherData();
        }
        public HourlyWeatherDisplay() { InitializeComponent(); }
        private void SetLabelsForWeatherData()
        {

            this.lblTime.Text = this.HourlyWeatherData.Date.ToString("t");
            //UITheme.FormatTempLabelColor(this.lblFeelsLikeHi, this.HourlyWeatherData.FeelsLikeHi);
            //UITheme.FormatTempLabelColor(this.lblFeelsLikeLow, this.HourlyWeatherData.FeelsLikeLow);
            UITheme.FormatTempLabelColor(this.pbTemp, this.HourlyWeatherData.FeelsLikeHi);
            UITheme.FormatePrecipChanceLabel(this.pbRain, this.HourlyWeatherData.PrecipitationChanceFormatted);
            UITheme.FormateWindLabel(this.pbWind, this.HourlyWeatherData.WindSpeedMax);
            //UITheme.FormateWindLabel(this.lblWindAvg, this.HourlyWeatherData.WindSpeedAvg);
            //UITheme.FormateWindLabel(this.lblWindMax, this.HourlyWeatherData.WindSpeedMax);
            //UITheme.FormatePrecipAmountLabel(this.lblRainAmount, this.HourlyWeatherData.PrecipitationAmount);
            //UITheme.FormatePrecipChanceLabel(this.lblChanceOfRain, this.HourlyWeatherData.PrecipitationChanceFormatted);
            //this.lblWindAvg.Text += "MPH";
            //this.lblWindMax.Text += "MPH";
            this.lblFeelsLikeHi.Text = this.HourlyWeatherData.FeelsLikeHi.ToString() + "°";
            this.lblFeelsLikeLow.Text = this.HourlyWeatherData.FeelsLikeLow.ToString() + "°";
            this.lblWindAvg.Text = this.HourlyWeatherData.WindSpeedAvg.ToString() + "MPH";
            this.lblWindMax.Text = this.HourlyWeatherData.WindSpeedMax.ToString() + "MPH";
            this.lblRainAmount.Text = this.HourlyWeatherData.PrecipitationAmount.ToString() + "\"";
            this.lblChanceOfRain.Text = this.HourlyWeatherData.PrecipitationChanceFormatted.ToString() + "%";
        }

    }
}
