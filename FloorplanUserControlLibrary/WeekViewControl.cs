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
    public partial class WeekViewControl : UserControl
    {
        public WeekViewControl()
        {
            InitializeComponent();
            DateControls[0] = dateControl1;
            DateControls[1] = dateControl2;
            DateControls[2] = dateControl3;
            DateControls[3] = dateControl4;
            DateControls[4] = dateControl5;
            DateControls[5] = dateControl6;
            DateControls[6] = dateControl7;
        }
        public DateOnly startDate = new DateOnly();
        public int WeekNumber = 0;
        public int MonthNumber = 0;
        private ToolTip toolTip = new ToolTip();
        public WeekViewControl parent { get; set; }
        public WeekViewControl child { get; set; }
        public void SetWeekAndMonth(int weekNumber, int monthNumber)
        {
            WeekNumber = weekNumber;
            MonthNumber = monthNumber;
        }

        public void SetFirstDayOfWeek(DayOfWeek dayOfWeek)
        {

            for (int i = 0; i < DateControls.Length; i++)
            {
                DateControls[i].SetDayOfWeek(dayOfWeek);
            }
        }
        public void PopulateDateControls(DayOfWeek dayOfWeek)
        {

        }
        public void FindFirstDate()
        {
            DateOnly firstDate = new DateOnly();
        }


        public DateControl[] DateControls = new DateControl[7];
    }
}
