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

namespace FloorPlanMakerUI
{
    public partial class frmCalendar : Form
    {
        CalendarManager calendarManager = new CalendarManager();
        public frmCalendar()
        {
            InitializeComponent();
            this.monthSelected = DateTime.Today.Month;
            PopulateMonthSelectComboBox();
            InitializeWeekControls();
        }

        private void PopulateMonthSelectComboBox()
        {
            var months = System.Globalization.DateTimeFormatInfo.CurrentInfo.MonthNames;
            for (int i = 0; i < 12; i++)
            {
                cboMonthSelect.Items.Add(new KeyValuePair<int, string>(i + 1, months[i]));
            }
            cboMonthSelect.DisplayMember = "Value";
            cboMonthSelect.ValueMember = "Key";
            
        }

        int monthSelected = 0;
        private void frmCalendar_Load(object sender, EventArgs e)
        {
            SetWeekControls();
            calendarManager = new CalendarManager(monthSelected, weekControls);
            SetInitialMonthSelection();


        }
        private WeekViewControl[] weekControls = new WeekViewControl[5];
        private void InitializeWeekControls()
        {
            weekControls[0] = weekViewControl1;
            weekControls[1] = weekViewControl2;
            weekControls[2] = weekViewControl3;
            weekControls[3] = weekViewControl4;
            weekControls[4] = weekViewControl5;
        }
        private void SetInitialMonthSelection()
        {
            for (int i = 0; i < cboMonthSelect.Items.Count; i++)
            {
                var item = (KeyValuePair<int, string>)cboMonthSelect.Items[i];
                if (item.Key == monthSelected)
                {
                    cboMonthSelect.SelectedIndex = i;
                    break;
                }
            }
        }
        
        private void SetWeekControls()
        {
            
            DateOnly date = new DateOnly(2024, monthSelected, 1);
            for(int i = 0;i < weekControls.Length; i++)
            {
                 
            }
        }

        private void cboMonthSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMonthSelect.SelectedItem != null)
            {
                var selectedMonth = (KeyValuePair<int, string>)cboMonthSelect.SelectedItem;
                monthSelected = selectedMonth.Key;
            }
            calendarManager.SetNewMonth(monthSelected);
            
        }
    }
}
