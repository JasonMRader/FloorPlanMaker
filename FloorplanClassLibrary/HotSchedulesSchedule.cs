using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class HotSchedulesSchedule
    {
        public DateDetails OutDate { get; set; }
        public int JobHsId { get; set; }
        public decimal PayRate { get; set; }
        public DateDetails InDate { get; set; }
        public decimal SpecialPay { get; set; }
        public int EmpHSId { get; set; }
        public int OvtMinutes { get; set; }
        public TimeDetails InTime { get; set; }
        public int JobPosId { get; set; }
        public DateDetails WeekStart { get; set; }
        public decimal OvtRate { get; set; }
        public int RegMinutes { get; set; }
        public int LocationId { get; set; }
        public DateDetails WeekEnd { get; set; }
        public TimeDetails OutTime { get; set; }
        public int ScheduleId { get; set; }
        public int EmpPosId { get; set; }
        public bool IsAM { get { return this.InTime.Hours < 14; } }
        public override string ToString()
        {
            return $"{InTime.Hours}, IsAM: {IsAM},jobPos: {JobPosId}, jobHsID: {JobHsId}, empPosId: {EmpPosId}, EmpHSId: {EmpHSId}";
        }
    }
    public class DateDetails
    {
        public int Month { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
    }

    public class TimeDetails
    {
        public int Hours { get; set; }
        public int Seconds { get; set; }
        public bool MilitaryTime { get; set; }
        public int Minutes { get; set; }
    }

}
