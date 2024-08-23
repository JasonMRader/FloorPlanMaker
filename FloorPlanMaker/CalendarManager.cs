using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class CalendarManager
    {
        public int month = 0;
        public int year = 2024;
        public enum CalendarDisplayType
        {
            FloorplanCounts,
            FloorplanSales
        }
        private CalendarDisplayType displayType {  get; set; } = CalendarDisplayType.FloorplanCounts;
        
        public DayOfWeek startDay = DayOfWeek.Monday;
        public CalendarManager(int month, WeekViewControl[] weekControls)         
        { 
            this.month = month;
            this.weekControls = weekControls;
            SetDateList();
            SetCalendarForFloorplanCounts();
        }
        public CalendarManager()
        {
           
        }
        public void SetNewMonth(int month)
        {
            this.month = month;
            
            SetDateList();
            SetCalendarForFloorplanCounts();
        }
        public void SetNewDisplayType(CalendarDisplayType displayType)
        {
            this.displayType = displayType;
        }
        private void RefreshCalendarForDisplayType()
        {
            if(displayType == CalendarDisplayType.FloorplanCounts)
            {
                SetCalendarForFloorplanCounts();
            }    
            else if(displayType == CalendarDisplayType.FloorplanSales)
            {

            }
        }
        private WeekViewControl[] weekControls = new WeekViewControl[5];
        
        public List<DateOnly> DateOnlyList = new List<DateOnly>();
        public void SetDateList()
        {
            DateOnly firstDay = new DateOnly(year, month, 1);
            int daysUntilStart = ( (int)startDay - (int)firstDay.DayOfWeek);
            DateOnly firstCalendarDay = firstDay.AddDays(daysUntilStart);
            DateOnlyList.Clear();
            for(DateOnly start = firstCalendarDay; start < firstCalendarDay.AddDays(35); start = start.AddDays(1))
            {
                DateOnlyList.Add(start);
            }

        }
        private void SetCalendarForFloorplanCounts()
        {
            int dateIndex = 0;
            for(int i = 0; i < weekControls.Length; i++)
            {
               
                for(int j = 0; j < weekControls[i].DateControls.Length; j++)
                {
                    weekControls[i].DateControls[j].SetDateOnly(DateOnlyList[dateIndex]);
                    weekControls[i].DateControls[j].ShowFloorplansForAmAndPM();
                    dateIndex++;
                }
            }
           
        }
    }
}
