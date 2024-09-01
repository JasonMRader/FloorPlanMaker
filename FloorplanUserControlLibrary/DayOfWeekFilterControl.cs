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
    public partial class DayOfWeekFilterControl : UserControl
    {
        private ShiftAnalysis shiftAnalysis { get; set; }
        private bool allChecked { get; set; } = true;
        List<CheckBox> allCbs { get; set; } = new List<CheckBox>();
        public DayOfWeekFilterControl(ShiftAnalysis shiftAnalysis)
        {
            InitializeComponent();
            this.shiftAnalysis = shiftAnalysis;
            pnlDaySelect.Visible = false;
            allCbs.Clear();
            allCbs.Add(cbMon);
            allCbs.Add(cbTues);
            allCbs.Add(cbWed);
            allCbs.Add(cbThurs);
            allCbs.Add(cbFri);
            allCbs.Add(cbSat);
            allCbs.Add(cbSun);
        }

        private void cbDayOfWeek_Clicked(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (allChecked) {
                cbAll.Checked = false;
                cb.Checked = true;
                //foreach (CheckBox c in allCbs) {
                //    c.Checked = false;
                //}
               
                allChecked = false;
            }
            else {

                allChecked = cbMon.Checked && cbTues.Checked && cbWed.Checked && cbThurs.Checked &&
                         cbFri.Checked && cbSat.Checked && cbSun.Checked;

            }
            bool noneChecked = !cbMon.Checked && !cbTues.Checked && !cbWed.Checked && !cbThurs.Checked &&
                        !cbFri.Checked && !cbSat.Checked && !cbSun.Checked;
            if (allChecked || noneChecked) {
                cbAll.Checked = true;
            }
           
            


        }

        private void cbDayOfWeek_CheckedChanged(object sender, EventArgs e)
        {
            //CheckBox cb = sender as CheckBox;

            //// Determine the corresponding day of the week for the checkbox
            //DayOfWeek day = DayOfWeek.Monday; // Default value, will be overwritten
            //if (cb == cbMon) day = DayOfWeek.Monday;
            //else if (cb == cbTues) day = DayOfWeek.Tuesday;
            //else if (cb == cbWed) day = DayOfWeek.Wednesday;
            //else if (cb == cbThurs) day = DayOfWeek.Thursday;
            //else if (cb == cbFri) day = DayOfWeek.Friday;
            //else if (cb == cbSat) day = DayOfWeek.Saturday;
            //else if (cb == cbSun) day = DayOfWeek.Sunday;

            //// Add or remove the day from the filter based on the checkbox status
            //if (cb.Checked) {
            //    shiftAnalysis.AddDayOfWeek(day);
            //}
            //else {
            //    shiftAnalysis.RemoveDayOfWeek(day);
            //}

            //UpdateDayOfWeekFilter();
        }
        private void UpdateShiftAnalysisFilter()
        {
            allChecked = true;
            foreach(CheckBox cb in allCbs) {
                DayOfWeek day = DayOfWeek.Monday;
                if (cb == cbMon) day = DayOfWeek.Monday;
                else if (cb == cbTues) day = DayOfWeek.Tuesday;
                else if (cb == cbWed) day = DayOfWeek.Wednesday;
                else if (cb == cbThurs) day = DayOfWeek.Thursday;
                else if (cb == cbFri) day = DayOfWeek.Friday;
                else if (cb == cbSat) day = DayOfWeek.Saturday;
                else if (cb == cbSun) day = DayOfWeek.Sunday;
                if (cb.Checked) {
                    shiftAnalysis.AddDayOfWeek(day);
                }
                else {
                    allChecked = false;
                    shiftAnalysis.RemoveDayOfWeek(day);
                }
            }
           
            shiftAnalysis.SetIsFilteredByDayOfWeek(!allChecked);
        }
        private void UpdateDayOfWeekFilter()
        {
            // Check if all checkboxes are checked
            allChecked = cbMon.Checked && cbTues.Checked && cbWed.Checked && cbThurs.Checked &&
                         cbFri.Checked && cbSat.Checked && cbSun.Checked;

            // Set the filtering status based on the checkboxes
            shiftAnalysis.SetIsFilteredByDayOfWeek(!allChecked);
        }


        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAll.Checked) {
                cbMon.Checked = true;
                cbTues.Checked = true;
                cbWed.Checked = true;
                cbThurs.Checked = true;
                cbFri.Checked = true;
                cbSat.Checked = true;
                cbSun.Checked = true;
                allChecked = true;

            }
            else {
                cbMon.Checked = false;
                cbTues.Checked = false;
                cbWed.Checked = false;
                cbThurs.Checked = false;
                cbFri.Checked = false;
                cbSat.Checked = false;
                cbSun.Checked = false;
                allChecked = false;

            }
        }
        private string GetButtonFilteredString()
        {
            string display = "";
            if (cbMon.Checked) {
                display += "|Mon|";
            }
            if (cbTues.Checked) {
                display += "|Tue|";
            }
            if (cbWed.Checked) {
                display += "|Wed|";
            }
            if (cbThurs.Checked) {
                display += "|Thu|";
            }
            if (cbFri.Checked) {
                display += "|Fri|";
            }
            if (cbSat.Checked) {
                display += "|Sat|";
            }
            if (cbSun.Checked) {
                display += "|Sun|";
            }
            return display;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pnlDaySelect.Visible = !pnlDaySelect.Visible;
            if (pnlDaySelect.Visible) {
                button1.BackColor = UITheme.YesColor;
                button1.Text = "OK";
            }
            else if (allChecked) {
                button1.BackColor = UITheme.ButtonColor;
                button1.ForeColor = UITheme.ButtonFontColor;
                button1.Text = "Filter By Day Of Week";
                UpdateShiftAnalysisFilter();
            }
            else {
                button1.BackColor = UITheme.CTAColor;
                button1.ForeColor = UITheme.CTAFontColor;
                button1.Text = GetButtonFilteredString();
                UpdateShiftAnalysisFilter();
            }
        }
    }
}
