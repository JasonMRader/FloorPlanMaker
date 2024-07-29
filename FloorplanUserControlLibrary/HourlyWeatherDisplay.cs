using FloorplanClassLibrary;
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
            SetLabelColors();
            
        }
        private void SetLabelsForWeatherData()
        {
            this.lblTime.Text = this.HourlyWeatherData.Date.ToString("t");
            this.lblFeelsLikeHi.Text = this.HourlyWeatherData.FeelsLikeHi.ToString() + "°";
            this.lblFeelsLikeLow.Text = this.HourlyWeatherData.FeelsLikeLow.ToString() + "°";
            this.lblChanceOfRain.Text = this.HourlyWeatherData.PrecipitationChance.ToString() + "%";
            this.lblRainAmount.Text = this.HourlyWeatherData.PrecipitationAmount.ToString() + "\"";
            this.lblWindAvg.Text = this.HourlyWeatherData.WindSpeedAvg.ToString() + "MPH";
            this.lblWindMax.Text = this.HourlyWeatherData.WindSpeedMax.ToString() + "MPH";
        }
        private void SetLabelColors()
        {
            if(this.HourlyWeatherData.FeelsLikeHi > 90)
            {
                this.lblFeelsLikeHi.BackColor = Color.Red;
            }
            if(this.HourlyWeatherData.FeelsLikeLow < 75)
            {
                this.lblFeelsLikeLow.BackColor = Color.Blue;
            }
            if(this.HourlyWeatherData.PrecipitationChance > 0 && this.HourlyWeatherData.PrecipitationChance < 50)
            {
                this.lblChanceOfRain.BackColor = Color.Pink;
            }
            if (this.HourlyWeatherData.PrecipitationChance >= 50 && this.HourlyWeatherData.PrecipitationChance < 75)
            {
                this.lblChanceOfRain.BackColor = Color.Red;
            }
            if (this.HourlyWeatherData.PrecipitationChance >= 75)
            {
                this.lblChanceOfRain.BackColor = Color.DarkRed;
            }
        }
    }
}
