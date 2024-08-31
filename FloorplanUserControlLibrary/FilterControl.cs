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
        

        public FilterControl(string filterName, decimal defaultMin,decimal defaultMax, 
            FilterType filter, ShiftAnalysis shiftAnalysis)
        {
            InitializeComponent();
            this.shiftAnalysis = shiftAnalysis;
            numericUpDown1.Value = defaultMin;
            numericUpDown2.Value = defaultMax;
            numericUpDown1.Visible = false;
            numericUpDown2.Visible = false;
            flowModifiers.Visible = false;
            this.filterName = filterName;
            button1.Text = $"Filter by {filterName}";
            this.filterType = filter;
            if(filterType == FilterType.Temperature) {
                isInt = true;
            }
            button1.BackColor = UITheme.ButtonColor;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!_filtered && !_choosing) {
                numericUpDown1.Visible = true;
                numericUpDown2.Visible = true;
                flowModifiers.Visible = true;
                button1.Text = $"OK";
                button1.BackColor = UITheme.YesColor;
                _filtered = true;
                _choosing = true;
                return;
            }
            else if(_filtered && _choosing) {
                SetTemperatureFilter();
                numericUpDown1.Visible = false;
                numericUpDown2.Visible = false;
                flowModifiers.Visible = false;
                _choosing = false;
                button1.BackColor = UITheme.CTAColor;
                return;
            }
            else if(_filtered &&  !_choosing) {
                button1.Text = $"Filter by {filterName}";
                _filtered = false;
                shiftAnalysis.SetIsFilteredByTemp(false);
                button1.BackColor = UITheme.ButtonColor;
            }
        }
        private void SetTemperatureFilter()
        {
            this.minInt = (int)numericUpDown1.Value;
            this.maxInt = (int)numericUpDown2.Value;
            button1.Text = $"{minInt}° to {maxInt}°";
            shiftAnalysis.SetIsFilteredByTemp(true);
            shiftAnalysis.SetTempRange(minInt, maxInt);
        }
    }
}
