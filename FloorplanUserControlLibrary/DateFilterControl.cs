using FloorplanClassLibrary;
using FloorPlanMakerUI;
using PdfSharp.Pdf.Filters;
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
    public partial class DateFilterControl : UserControl
    {
        private bool _choosing = false;
       
        private ShiftAnalysis shiftAnalysis { get; set; }
        
        public DateFilterControl(ShiftAnalysis shiftAnalysis)
        {
            InitializeComponent();
            this.shiftAnalysis = shiftAnalysis;
            
            flowRangeSelection.Visible = false;
           
            button1.Text = $"";
            
            //if (filterType == FilterType.Temperature) {
            //    isInt = true;
            //}
            button1.BackColor = UITheme.ButtonColor;
        }

        private void DateFilterControl_Load(object sender, EventArgs e)
        {

        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            if (!_choosing) {
                
                flowRangeSelection.Visible = true;
                button1.Text = $"OK";
                button1.BackColor = UITheme.YesColor;
                
                _choosing = true;
                return;
            }
            else if (_choosing) {
                SetTemperatureFilter();
               
                flowRangeSelection.Visible = false;
                _choosing = false;
                button1.BackColor = UITheme.CTAColor;
                return;
            }
           
        }
        private void SetTemperatureFilter()
        {
            
            //button1.Text = $"{minInt}° to {maxInt}°";
            shiftAnalysis.SetIsFilteredByTemp(true);
            //shiftAnalysis.SetTempRange(minInt, maxInt);
        }
    }
}
