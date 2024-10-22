using FloorplanClassLibrary;
using FloorPlanMakerUI;
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
    public partial class FilterControl : UserControl
    {
        private bool _filtered = false;
        private bool _choosing = false;
        private int minInt = 0;
        private int maxInt = 0;
        private double minDouble = 0;
        private double maxDouble = 0;
        private bool isInt { get; set; } = false;
        private string filterName { get; set; }
        private FilterType filterType { get; set; }
        private ShiftAnalysis shiftAnalysis { get; set; }
        public enum FilterType
        {
            Temperature,
            Rain,
            Clouds,
            WindMax,
            WindAvg,
            Reservations
        }


        public FilterControl(string filterName, decimal defaultMin, decimal defaultMax,
            FilterType filter, ShiftAnalysis shiftAnalysis)
        {
            InitializeComponent();
            this.shiftAnalysis = shiftAnalysis;
            this.filterType = filter;
            if (filterType == FilterType.Temperature) {

                numericUpDown1.Minimum = -50;
                numericUpDown2.Minimum = -50;
                numericUpDown1.Maximum = 130;
                numericUpDown2.Maximum = 130;
            }
            if (filterType == FilterType.Rain) {
                numericUpDown1.Increment = 0.01m;
                numericUpDown2.Increment = 0.01m;
                numericUpDown1.DecimalPlaces = 2;
                numericUpDown2.DecimalPlaces = 2;
            }
            if (filterType == FilterType.Reservations) {
                //this.Enabled = false;
                numericUpDown1.Minimum = 0;
                numericUpDown2.Minimum = 0;
                numericUpDown1.Maximum = 2000;
                numericUpDown2.Maximum = 2000;
                numericUpDown1.Increment = 50;
                numericUpDown2.Increment = 50;
            }
            if (filterType == FilterType.Clouds) {

            }
            if (filterType == FilterType.WindAvg) {

            }
            if (filterType == FilterType.WindMax) {

            }
            numericUpDown1.Value = defaultMin;
            numericUpDown2.Value = defaultMax;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            flowModifiers.Visible = false;
            this.filterName = filterName;
            button1.Text = $"Filter by {filterName}";
            button1.BackColor = UITheme.ButtonColor;
            button1.ForeColor = Color.Black;
            UpdateControlsForCurrentFilter();
        }
        public void SetForResos(int min,  int max)
        {
            if (filterType == FilterType.Reservations) {
                numericUpDown1.Value = (decimal)min;
                numericUpDown2.Value = (decimal)max;
            }
        }
        private void UpdateControlsForCurrentFilter()
        {
            if (filterType == FilterType.Temperature && shiftAnalysis.IsFilterByTemperature) {
                numericUpDown1.Value = shiftAnalysis.TempMin;
                numericUpDown2.Value = shiftAnalysis.TempMax;
                button1.Text = $"{shiftAnalysis.TempMin}° to {shiftAnalysis.TempMax}°";
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            if (filterType == FilterType.Rain && shiftAnalysis.IsFilteredByRainAmount) {
                numericUpDown1.Value = (decimal)shiftAnalysis.RainMin;
                numericUpDown2.Value = (decimal)shiftAnalysis.RainMax;
                button1.Text = $"{numericUpDown1.Value:F2}\" to {numericUpDown2.Value:F2}\"";
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            if (filterType == FilterType.Reservations) {
               
            }
            if (filterType == FilterType.Clouds && shiftAnalysis.IsFilteredByClouds) {
                numericUpDown1.Value = (decimal)shiftAnalysis.CloudMin;
                numericUpDown2.Value = (decimal)shiftAnalysis.CloudMax;
                button1.Text = $"{numericUpDown1.Value:F0}% to {numericUpDown2.Value:F0}%";
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;

            }
            if (filterType == FilterType.WindAvg && shiftAnalysis.IsFilteredByWindAvg) {
                numericUpDown1.Value = shiftAnalysis.WindAvgMin;
                numericUpDown2.Value = shiftAnalysis.WindAvgMax;
                button1.Text = $"Avg wind {numericUpDown1.Value:F0} MPH to {numericUpDown2.Value:F0} MPH";
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            if (filterType == FilterType.WindMax && shiftAnalysis.IsFilteredByWindMax) {
                numericUpDown1.Value = shiftAnalysis.WindMaxMin;
                numericUpDown2.Value = shiftAnalysis.WindMaxMax;
                button1.Text = $"Max wind {numericUpDown1.Value:F0} MPH to {numericUpDown2.Value:F0} MPH";
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
        }

        public void UpdateDefaultsForCurrentWeather(ShiftWeather shiftWeather)
        {
            if (filterType == FilterType.Temperature) {
                numericUpDown1.Value = shiftWeather.FeelsLikeAvg - 5;
                numericUpDown2.Value = shiftWeather.FeelsLikeAvg + 5;
            }
            else if (filterType == FilterType.Rain) {
                if(shiftWeather.RainAmount == 0) {
                    numericUpDown1.Value = 0;
                    numericUpDown2.Value = 0;
                }
                else if (shiftWeather.RainAmount > 0 && shiftWeather.RainAmount <= .1) {
                    numericUpDown1.Value = .01m;
                    numericUpDown2.Value = (decimal)shiftWeather.RainAmount + .15m;
                }
                else if (shiftWeather.RainAmount > .1 && shiftWeather.RainAmount <= .25) {
                    numericUpDown1.Value = .05m;
                    numericUpDown2.Value = (decimal)shiftWeather.RainAmount + .3m;
                }
                else if (shiftWeather.RainAmount > .25) {
                    numericUpDown1.Value = .2m;
                    numericUpDown2.Value = (decimal)shiftWeather.RainAmount + 1m;
                }


            }
            else if (filterType == FilterType.Clouds) {
                if (shiftWeather.CloudCoverAverage < 40) {
                    numericUpDown1.Value = 0;
                    numericUpDown2.Value = 60;
                }
                else {
                    numericUpDown1.Value = (decimal)shiftWeather.CloudCoverAverage - 30;
                    if (shiftWeather.CloudCoverAverage < 70) {
                        numericUpDown2.Value = (decimal)shiftWeather.CloudCoverAverage + 30;
                    }
                    else {
                        numericUpDown2.Value = 100;
                    }
                    
                }
            }
            else if (filterType == FilterType.WindMax) {
                
            }
            else if (filterType == FilterType.WindAvg) {
               
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (!_filtered && !_choosing) {
                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                //flowModifiers.Visible = true;
                button1.Text = $"OK";
                button1.BackColor = UITheme.YesColor;
                button1.ForeColor = Color.White;
                _filtered = true;
                _choosing = true;
                return;
            }
            else if (_filtered && _choosing) {
                SetIsFilteredBy(true);
                numericUpDown1.Visible = false;
                numericUpDown2.Visible = false;
                flowModifiers.Visible = false;
                _choosing = false;
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
                return;
            }
            else if (_filtered && !_choosing) {
                button1.Text = $"Filter by {filterName}";
                _filtered = false;
                SetIsFilteredBy(false);
                button1.BackColor = UITheme.ButtonColor;
                button1.ForeColor = Color.Black;
            }
        }
        private void SetIsFilteredBy(bool isFiltered)
        {
            if (isFiltered) {
                if (filterType == FilterType.Temperature) {
                    shiftAnalysis.SetIsFilteredByTemp(isFiltered);
                    SetTemperatureFilter();
                }
                else if (filterType == FilterType.Rain) {
                    shiftAnalysis.SetIsFilteredbyRainAmount(isFiltered);
                    SetRainFilter();
                }
                else if (filterType == FilterType.Clouds) {
                    shiftAnalysis.SetIsFilteredByClouds(isFiltered);
                    SetCloudFilter();
                }
                else if (filterType == FilterType.WindMax) {
                    shiftAnalysis.SetIsFilteredByWindMax(isFiltered);
                    SetWindMaxFilter();
                }
                else if(filterType == FilterType.WindAvg) {
                    shiftAnalysis.SetIsFilteredByWindAvg(isFiltered);
                    SetWindAvgFilter();
                }
            }
            else {
                if (filterType == FilterType.Temperature) {
                    shiftAnalysis.SetIsFilteredByTemp(isFiltered);

                }
                else if (filterType == FilterType.Rain) {
                    shiftAnalysis.SetIsFilteredbyRainAmount(isFiltered);                    
                }
                else if (filterType == FilterType.Clouds) {
                    shiftAnalysis.SetIsFilteredByClouds(isFiltered);
                }
                else if (filterType == FilterType.WindMax) {
                    shiftAnalysis.SetIsFilteredByWindMax(isFiltered);
                }
                else if (filterType == FilterType.WindAvg) {
                    shiftAnalysis.SetIsFilteredByWindAvg(isFiltered);
                }
            }

        }

        private void SetWindAvgFilter()
        {
            button1.Text = $"Avg wind {numericUpDown1.Value:F0} MPH to {numericUpDown2.Value:F0} MPH";
            shiftAnalysis.SetWindAvgRange((int)numericUpDown1.Value, (int)numericUpDown2.Value);
        }

        private void SetWindMaxFilter()
        {
            button1.Text = $"Max wind {numericUpDown1.Value:F0} MPH to {numericUpDown2.Value:F0} MPH";
            shiftAnalysis.SetWindMaxRange((int)numericUpDown1.Value, (int)numericUpDown2.Value);
        }
        
        private void SetCloudFilter()
        {
            button1.Text = $"{numericUpDown1.Value:F0}% to {numericUpDown2.Value:F0}%";
            shiftAnalysis.SetCloudRange((float)numericUpDown1.Value, (float)numericUpDown2.Value);
        }

        private void SetTemperatureFilter()
        {
            this.minInt = (int)numericUpDown1.Value;
            this.maxInt = (int)numericUpDown2.Value;
            button1.Text = $"{minInt}° to {maxInt}°";
            shiftAnalysis.SetTempRange(minInt, maxInt);
        }
        private void SetRainFilter()
        {
           
            button1.Text = $"{numericUpDown1.Value:F2}\" to {numericUpDown2.Value:F2}\"";
            shiftAnalysis.SetRainRange((float)numericUpDown1.Value, (float)numericUpDown2.Value);
        }
    }
}
