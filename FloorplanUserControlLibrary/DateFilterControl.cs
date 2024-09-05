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

        private DateTimePicker dtpStart = new DateTimePicker();
        private DateTimePicker dtpEnd = new DateTimePicker();
        private ShiftAnalysis shiftAnalysis { get; set; }
        private RadioButton previouslyClickedRDO { get; set; }

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
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            else if (rdoLast90.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-90), endDate);
                button1.Text = $"Last 90 Days";
                rdoLast90.BackColor = UITheme.CTAColor;
                rdoLast90.ForeColor = UITheme.CTAFontColor; 
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            else if (rdoLast365.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-365), endDate);
                button1.Text = $"Last 365 Days";
                rdoLast365.BackColor = UITheme.CTAColor;
                rdoLast365.ForeColor = UITheme.CTAFontColor;
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            else if (rdoAllRecords.Checked) {
                shiftAnalysis.SetDateOnly(new DateOnly(2023, 1, 1), endDate);
                button1.Text = $"All Records";
                rdoAllRecords.BackColor = UITheme.CTAColor;
                rdoAllRecords.ForeColor = UITheme.CTAFontColor;
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
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
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            else if (rdoLast90.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-90), endDate);
                button1.Text = $"Last 90 Days";
                rdoLast90.BackColor = UITheme.CTAColor;
                rdoLast90.ForeColor = UITheme.CTAFontColor;
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            else if (rdoLast365.Checked) {
                shiftAnalysis.SetDateOnly(endDate.AddDays(-365), endDate);
                button1.Text = $"Last 365 Days";
                rdoLast365.BackColor = UITheme.CTAColor;
                rdoLast365.ForeColor = UITheme.CTAFontColor;
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            else if (rdoAllRecords.Checked) {
                shiftAnalysis.SetDateOnly(new DateOnly(2023, 1, 1), endDate);
                button1.Text = $"All Records";
                rdoAllRecords.BackColor = UITheme.CTAColor;
                rdoAllRecords.ForeColor = UITheme.CTAFontColor;
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            flowRangeSelection.Visible = false;
        }


        private void button1_Click(object sender, EventArgs e)
        {
           
            if(cbCustom.Checked && flowRangeSelection.Visible) {
                DateOnly startDate = DateOnly.FromDateTime(dtpStart.Value);
                DateOnly endDate = DateOnly.FromDateTime(dtpEnd.Value);
                shiftAnalysis.SetDateOnly(startDate, endDate);
                button1.Text = $"{startDate} - {endDate}";
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
            }
            else if(cbCustom.Checked && !flowRangeSelection.Visible) {
                button1.Text = $"OK";
                button1.BackColor = UITheme.YesColor;
                button1.ForeColor = Color.White;
            }
            else if(!cbCustom.Checked && !rdoAllRecords.Checked && !rdoLast365.Checked 
                && !rdoLast90.Checked && !rdoLast30.Checked) {
                return;
            }
            flowRangeSelection.Visible = !flowRangeSelection.Visible;

        }

        private void cbCustom_CheckedChanged(object sender, EventArgs e)
        {
            if(cbCustom.Checked) {
                if(rdoAllRecords.Checked) {
                    previouslyClickedRDO = rdoAllRecords;
                }
                else if(rdoLast365.Checked) {
                    previouslyClickedRDO = rdoLast365;
                }
                else if(rdoLast90.Checked) {
                    previouslyClickedRDO=rdoLast90;
                }
                else {
                    previouslyClickedRDO = rdoLast30;
                }
                rdoLast30.BackColor = UITheme.ButtonColor;
                rdoLast30.ForeColor = Color.Black;
                rdoLast30.Checked = false;
                rdoLast30.Visible = false;
                rdoLast90.BackColor = UITheme.ButtonColor;
                rdoLast90.ForeColor = Color.Black;
                rdoLast90.Checked = false;
                rdoLast90.Visible = false;
                rdoLast365.BackColor = UITheme.ButtonColor;
                rdoLast365.ForeColor = Color.Black;
                rdoLast365.Checked = false;
                rdoLast365.Visible = false;
                rdoAllRecords.BackColor = UITheme.ButtonColor;
                rdoAllRecords.ForeColor = Color.Black;
                rdoAllRecords.Checked = false;
                rdoAllRecords.Visible = false;
                Label lblStart = new Label() {
                    Text = "Start",
                    Margin = new Padding(0, 7, 0, 0)
                };
                dtpStart = new DateTimePicker() {
                    Size = new Size(194, 30),
                    Margin = new Padding(0)


                };
                Label lblEnd = new Label() { 
                    Text = "end",
                    Margin = new Padding(0, 7, 0, 0)
                };
                dtpEnd = new DateTimePicker() {
                    Size = new Size(194, 30),
                    Margin = new Padding(0,0,0,0), 
                };
                flowRangeSelection.Controls.Add(lblStart);
                flowRangeSelection.Controls.Add(dtpStart);
                flowRangeSelection.Controls.Add(lblEnd);
                flowRangeSelection.Controls.Add(dtpEnd);
                button1.Text = $"OK";
                button1.BackColor = UITheme.YesColor;
                button1.ForeColor = Color.White;

            }
            else {
                rdoLast30.BackColor = UITheme.ButtonColor;
                rdoLast30.ForeColor = Color.Black;
                rdoLast30.Checked = false;
                rdoLast30.Visible = true;
                rdoLast90.BackColor = UITheme.ButtonColor;
                rdoLast90.ForeColor = Color.Black;
                rdoLast90.Checked = false;
                rdoLast90.Visible = true;
                rdoLast365.BackColor = UITheme.ButtonColor;
                rdoLast365.ForeColor = Color.Black;
                rdoLast365.Checked = false;
                rdoLast365.Visible = true;
                rdoAllRecords.BackColor = UITheme.ButtonColor;
                rdoAllRecords.ForeColor = Color.Black;
                rdoAllRecords.Checked = false;
                rdoAllRecords.Visible = true;
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = Color.White;
                if ( previouslyClickedRDO != null){
                    previouslyClickedRDO.PerformClick();
                }
            }
            
        }
    }
}
