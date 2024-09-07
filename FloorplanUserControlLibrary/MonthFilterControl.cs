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
    public partial class MonthFilterControl : UserControl
    {
        private bool allChecked { get; set; } = true;
        private List<CheckBox> allCbs = new List<CheckBox>();
        private ShiftAnalysis shiftAnalysis { get; set; }
        public MonthFilterControl(ShiftAnalysis shiftAnalysis)
        {
            InitializeComponent();
            this.shiftAnalysis = shiftAnalysis;
            this.pnlMonthSelect.Visible = false;
            allCbs.Add(cbJan);
            allCbs.Add(cbFeb);
            allCbs.Add(cbMar);
            allCbs.Add(cbApr);
            allCbs.Add(cbMay);
            allCbs.Add(cbJun);
            allCbs.Add(cbJul);
            allCbs.Add(cbAug);
            allCbs.Add(cbSep);
            allCbs.Add(cbOct);
            allCbs.Add(cbNov);
            allCbs.Add(cbDec);
            if (shiftAnalysis.IsFilteredByMonth) {
                UpdateCheckedStatusForCBs();
            }
        }

        private void UpdateCheckedStatusForCBs()
        {
            foreach (CheckBox checkBox in allCbs) {
                if (shiftAnalysis.FilteredMonths.Contains((int)checkBox.Tag)) {
                    checkBox.Checked = true;
                }
                else {
                    checkBox.Checked = false;
                    button1.BackColor = UITheme.CTAColor;
                    button1.ForeColor = UITheme.CTAFontColor;
                    button1.Text = GetButtonFilteredString();

                }
            }
            UpdateShiftAnalysisFilter();
        }

        private void cbMonth_Clicked(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if (allChecked) {
                UncheckAllMonths();
                cb.Checked = true;
            }
            else {

                allChecked = cbJan.Checked && cbFeb.Checked && cbMar.Checked && cbApr.Checked &&
                         cbMay.Checked && cbJun.Checked && cbJul.Checked && cbAug.Checked &&
                         cbSep.Checked && cbOct.Checked && cbNov.Checked && cbDec.Checked;

            }
            bool noneChecked = !cbJan.Checked && !cbFeb.Checked && !cbMar.Checked && !cbApr.Checked &&
                         !cbMay.Checked && !cbJun.Checked && !cbJul.Checked && !cbAug.Checked &&
                         !cbSep.Checked && !cbOct.Checked && !cbNov.Checked && !cbDec.Checked;
            if (allChecked || noneChecked) {
                CheckAllMonths();
            }
        }

        private void CheckAllMonths()
        {
            cbJan.Checked = true;
            cbFeb.Checked = true;
            cbMar.Checked = true;
            cbApr.Checked = true;
            cbMay.Checked = true;
            cbJun.Checked = true;
            cbJul.Checked = true;
            cbAug.Checked = true;
            cbSep.Checked = true;
            cbOct.Checked = true;
            cbNov.Checked = true;
            cbDec.Checked = true;
            allChecked = true;
        }

        private void UncheckAllMonths()
        {
            cbJan.Checked = false;
            cbFeb.Checked = false;
            cbMar.Checked = false;
            cbApr.Checked = false;
            cbMay.Checked = false;
            cbJun.Checked = false;
            cbJul.Checked = false;
            cbAug.Checked = false;
            cbSep.Checked = false;
            cbOct.Checked = false;
            cbNov.Checked = false;
            cbDec.Checked = false;
            allChecked = false;
        }
        private void UpdateShiftAnalysisFilter()
        {
            allChecked = true;
            foreach (CheckBox cb in allCbs) {
                int month = allCbs.IndexOf(cb) + 1;
                if (cb.Checked) {
                    shiftAnalysis.AddMonth(month);
                }
                else {
                    allChecked = false;
                    shiftAnalysis.RemoveMonth(month);
                }
            }

            shiftAnalysis.SetIsFilteredByMonth(!allChecked);
        }
        private string GetButtonFilteredString()
        {
            string display = "";
            List<CheckBox> checkedCbs = allCbs.Where(allCbs => allCbs.Checked).ToList();
            if (checkedCbs.Count > 6) {
                return $"{checkedCbs.Count} Months Included";
            }
            foreach (CheckBox checkBox in checkedCbs) {
                display += checkBox.Text;

                if (checkBox != checkedCbs.Last()) {
                    if (checkedCbs.Count >= 6) {
                        display += "|";
                    }
                    else if (checkedCbs.Count == 5) {
                        display += " | ";
                    }
                    else {
                        display += "  |  ";
                    }

                }
            }

            return display;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            pnlMonthSelect.Visible = !pnlMonthSelect.Visible;
            if (pnlMonthSelect.Visible) {
                button1.BackColor = UITheme.YesColor;
                button1.Text = "OK";
            }
            else if (allChecked) {
                button1.BackColor = UITheme.ButtonColor;
                button1.ForeColor = Color.Black;
                button1.Text = "Filter By Month";
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
