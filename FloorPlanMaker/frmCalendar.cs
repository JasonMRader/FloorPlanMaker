using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI {
    public partial class frmCalendar : Form {
        CalendarManager calendarManager = new CalendarManager();
        public frmCalendar() {
            InitializeComponent();
            this.monthSelected = DateTime.Today.Month;
            PopulateMonthSelectComboBox();
            InitializeWeekControls();
        }

        private void PopulateMonthSelectComboBox() {
            var months = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames;
            for (int i = 0; i < 12; i++) {
                cboMonthSelect.Items.Add(new KeyValuePair<int, string>(i + 1, months[i]));
            }
            cboMonthSelect.DisplayMember = "Value";
            cboMonthSelect.ValueMember = "Key";

        }

        int monthSelected = 0;
        private void rdoCalendarTypeChanged(object sender, EventArgs e) {
            if (rdoFloorplanCounts.Checked) {
                rdoAm.Visible = false;
                rdoPm.Visible = false;
                rdoAllDay.Visible = true;
                rdoAllDay.Checked = true;
            }
            else if (rdoSalesByArea.Checked) {
                rdoAm.Visible = true;
                rdoPm.Visible = true;
                rdoAllDay.Visible = false;
                rdoAllDay.Checked = false;
            }
        }
        private List<DiningArea> diningAreas { get; set; } = new List<DiningArea>();
        private void frmCalendar_Load(object sender, EventArgs e) {
            diningAreas = SqliteDataAccess.LoadDiningAreas();
            calendarManager = new CalendarManager(monthSelected, weekControls, diningAreas);
            SetInitialMonthSelection();


        }

        private WeekViewControl[] weekControls = new WeekViewControl[5];
        private void InitializeWeekControls() {
            weekControls[0] = weekViewControl1;
            weekControls[1] = weekViewControl2;
            weekControls[2] = weekViewControl3;
            weekControls[3] = weekViewControl4;
            weekControls[4] = weekViewControl5;
        }
        private void SetInitialMonthSelection() {
            for (int i = 0; i < cboMonthSelect.Items.Count; i++) {
                var item = (KeyValuePair<int, string>)cboMonthSelect.Items[i];
                if (item.Key == monthSelected) {
                    cboMonthSelect.SelectedIndex = i;
                    break;
                }
            }
        }



        private void cboMonthSelect_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboMonthSelect.SelectedItem != null) {
                var selectedMonth = (KeyValuePair<int, string>)cboMonthSelect.SelectedItem;
                monthSelected = selectedMonth.Key;
            }
            calendarManager.SetNewMonth(monthSelected);

        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            if (rdoFloorplanCounts.Checked) {
                calendarManager.SetNewDisplayType(CalendarManager.CalendarDisplayType.FloorplanCounts);

            }
            else if (rdoSalesByArea.Checked) {
                calendarManager.SetNewDisplayType(CalendarManager.CalendarDisplayType.FloorplanSales);
            }
        }

        private void rdoShiftType_CheckedChanged(object sender, EventArgs e) {
            if (rdoAm.Checked) {
                calendarManager.SetIsAM(true, false);
            }
            else if (rdoPm.Checked) {
                calendarManager.SetIsAM(false, true);
            }
            else if (rdoAllDay.Checked) {
                calendarManager.SetIsAM(true, true);
            }
        }

        private void cbIgnoredDiningAreas_CheckedChanged(object sender, EventArgs e) {
            List<DiningArea> list = new List<DiningArea>();
            list.AddRange(diningAreas);
            if (cbBanquet.Checked) {
                DiningArea areaToRemove = list.FirstOrDefault(a => a.ID == 6);
                if (areaToRemove != null) {
                    list.Remove(areaToRemove);
                }
            }
            if (cbInsideDining.Checked) {
                DiningArea areaToRemove = list.FirstOrDefault(a => a.ID == 1);
                if (areaToRemove != null) {
                    list.Remove(areaToRemove);
                }
            }
            if (cbOutsideDining.Checked) {
                DiningArea areaToRemove = list.FirstOrDefault(a => a.ID == 2);
                if (areaToRemove != null) {
                    list.Remove(areaToRemove);
                }
            }
            if (cbOutsideCocktail.Checked) {
                DiningArea areaToRemove = list.FirstOrDefault(a => a.ID == 3);
                if (areaToRemove != null) {
                    list.Remove(areaToRemove);
                }
            }
            if (cbInsideCocktail.Checked) {
                DiningArea areaToRemove = list.FirstOrDefault(a => a.ID == 4);
                if (areaToRemove != null) {
                    list.Remove(areaToRemove);
                }
            }
            if (cbUpperLevel.Checked) {
                DiningArea areaToRemove = list.FirstOrDefault(a => a.ID == 5);
                if (areaToRemove != null) {
                    list.Remove(areaToRemove);
                }
            }
            calendarManager.UpdateIgnoredAreas(list);

        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
            this.Dispose();
        }

        private void rdoYear_CheckedChanged(object sender, EventArgs e) {
            if(rdo2024.Checked) {
                calendarManager.SetNewYear(2024);
            }
            else if (rdo2023.Checked) {
                calendarManager.SetNewYear(2023);
            }
            else if (rdo2022.Checked) {
                calendarManager.SetNewYear(2022);
            }
        }
    }
}
