using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ShiftAnalysis
    {
        private List<ShiftRecord> _shifts = new List<ShiftRecord>();
        public List<ShiftRecord> Shifts { get { return _shifts; } }
        public ShiftAnalysis() { }
        private DateOnly _startDate = new DateOnly();
        private DateOnly _endDate = new DateOnly();
        public DateOnly StartDate
        {
            get { return _startDate; }
        }
        public DateOnly EndDate { get { return _endDate; } }
        public void SetDateOnly(DateOnly startDate, DateOnly endDate)
        {
            this._endDate = endDate;
            this._startDate = startDate;
        }
        public void SetShiftsForDateRange()
        {
            this.Shifts.Clear();
            for (DateOnly iDay = _startDate; iDay <= _endDate; iDay = iDay.AddDays(1))
            {
                Shifts.Add(SqliteDataAccess.LoadShiftRecord(iDay, false));
            }
        }
        public ShiftAnalysis(List<ShiftRecord> shifts)
        {
            _shifts = shifts;
        }
        private List<DayOfWeek> FilteredDaysOfWeek = new List<DayOfWeek>
        {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday
        };
        private List<int> FilteredMonths = new List<int>
        {
           1,2,3,4,5,6,7,8,9,10,11,12
        };

        public List<ShiftRecord> FilterByReservationRange(int minReservations, int maxReservations)
        {
            return Shifts.Where(shift => shift.Reservations >= minReservations && shift.Reservations <= maxReservations).ToList();
        }

        //public List<ShiftRecord> FilterByTemperatureRange(double minTemperature, double maxTemperature)
        //{
        //    return Shifts.Where(shift => shift.Weather.Temperature >= minTemperature && shift.Weather.Temperature <= maxTemperature).ToList();
        //}

        public List<ShiftRecord> FilterByDaysOfWeek(List<DayOfWeek> daysOfWeek)
        {
            return Shifts.Where(shift => daysOfWeek.Contains(shift.Date.DayOfWeek)).ToList();
        }

        public List<ShiftRecord> FilterBySpecialEvent(bool isSpecialEvent)
        {
            if(isSpecialEvent)
            {
                return Shifts.Where(shift => shift.SpecialEventDate != null).ToList();
            }
            else
            {
                return Shifts.Where(shift => shift.SpecialEventDate == null).ToList();
            }
            
        }

        //public double CalculateTotalSales()
        //{
        //    return Shifts.Sum(shift => shift.Sales);
        //}

        //public double CalculateAreaSales(string areaName)
        //{
        //    return Shifts.Sum(shift => shift.Areas.Where(area => area.Area == areaName).Sum(area => area.Sales));
        //}

        //public Dictionary<string, double> CalculateAreaSales()
        //{
        //    var areaSales = new Dictionary<string, double>();

        //    foreach (var shift in Shifts)
        //    {
        //        foreach (var area in shift.Areas)
        //        {
        //            if (!areaSales.ContainsKey(area.Area))
        //            {
        //                areaSales[area.Area] = 0;
        //            }
        //            areaSales[area.Area] += area.Sales;
        //        }
        //    }

        //    return areaSales;
        //}
    }

}
