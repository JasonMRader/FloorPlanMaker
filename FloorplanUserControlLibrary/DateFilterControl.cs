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


        private ShiftAnalysis shiftAnalysis { get; set; }

        public DateFilterControl(ShiftAnalysis shiftAnalysis)
        {
            InitializeComponent();
            this.shiftAnalysis = shiftAnalysis;

            flowRangeSelection.Visible = false;

            button1.Text = $"Last 90 Days";

            //if (filterType == FilterType.Temperature) {
            //    isInt = true;
            //}
            //button1.BackColor = UITheme.ButtonColor;
        }

        private void DateFilterControl_Load(object sender, EventArgs e)
        {

        }
        private void rdoTimeFrame_Clicked(object sender, EventArgs e)
        {
            DateOnly endDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);
            rdoLast30.BackColor = UITheme.ButtonColor;
            rdoLast30.ForeColor = Color.Black;
            rdoLast90.BackColor = UITheme.ButtonColor;
            rdoLast90.ForeColor = Color.Black;
            rdoLast365.BackColor = UITheme.ButtonColor;
            rdoLast365.ForeColor = Color.Black;
            rdoAllRecords.BackColor = UITheme.ButtonColor;
            rdoAllRecords.ForeColor = Color.Black;

            if (rdoLast30.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-30), endDate);
                button1.Text = $"Last 30 Days";
                rdoLast30.BackColor = UITheme.CTAColor;
                rdoLast30.ForeColor = UITheme.CTAFontColor;
            }
            else if (rdoLast90.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-90), endDate);
                button1.Text = $"Last 90 Days";
                rdoLast90.BackColor = UITheme.CTAColor;
                rdoLast90.ForeColor = UITheme.CTAFontColor;
            }
            else if (rdoLast365.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-365), endDate);
                button1.Text = $"Last 365 Days";
                rdoLast365.BackColor = UITheme.CTAColor;
                rdoLast365.ForeColor = UITheme.CTAFontColor;
            }
            else if (rdoAllRecords.Checked) {
                shiftAnalysis.SetDateOnly(new DateOnly(2023, 1, 1), endDate);
                button1.Text = $"All Records";
                rdoAllRecords.BackColor = UITheme.CTAColor;
                rdoAllRecords.ForeColor = UITheme.CTAFontColor;
            }
            flowRangeSelection.Visible = false;
        }
        private void rdoTimeFrame_CheckChanged(object sender, EventArgs e)
        {
            DateOnly endDate = DateOnly.FromDateTime(DateTime.Today).AddDays(-1);
            rdoLast30.BackColor = UITheme.ButtonColor;
            rdoLast30.ForeColor = Color.Black;
            rdoLast90.BackColor = UITheme.ButtonColor;
            rdoLast90.ForeColor = Color.Black;
            rdoLast365.BackColor = UITheme.ButtonColor;
            rdoLast365.ForeColor = Color.Black;
            rdoAllRecords.BackColor = UITheme.ButtonColor;
            rdoAllRecords.ForeColor = Color.Black;

            if (rdoLast30.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-30), endDate);
                button1.Text = $"Last 30 Days";
                rdoLast30.BackColor = UITheme.CTAColor;
                rdoLast30.ForeColor = UITheme.CTAFontColor;
            }
            else if (rdoLast90.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-90), endDate);
                button1.Text = $"Last 90 Days";
                rdoLast90.BackColor = UITheme.CTAColor;
                rdoLast90.ForeColor = UITheme.CTAFontColor;
            }
            else if (rdoLast365.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-365), endDate);
                button1.Text = $"Last 365 Days";
                rdoLast365.BackColor = UITheme.CTAColor;
                rdoLast365.ForeColor = UITheme.CTAFontColor;
            }
            else if (rdoAllRecords.Checked) {
                shiftAnalysis.SetDateOnly(new DateOnly(2023, 1, 1), endDate);
                button1.Text = $"All Records";
                rdoAllRecords.BackColor = UITheme.CTAColor;
                rdoAllRecords.ForeColor = UITheme.CTAFontColor;
            }
            flowRangeSelection.Visible = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            flowRangeSelection.Visible = !flowRangeSelection.Visible;

        }

    }
}
