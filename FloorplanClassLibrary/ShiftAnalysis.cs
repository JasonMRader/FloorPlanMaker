using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ShiftAnalysis
    {
        public List<ShiftRecord> Shifts { get; set; }

        public ShiftAnalysis(List<ShiftRecord> shifts)
        {
            Shifts = shifts;
        }

        public List<ShiftRecord> FilterByReservationRange(int minReservations, int maxReservations)
        {
            return Shifts.Where(shift => shift.Reservations >= minReservations && shift.Reservations <= maxReservations).ToList();
        }

        //public List<ShiftRecord> FilterByTemperatureRange(double minTemperature, double maxTemperature)
        //{
        //    return Shifts.Where(shift => shift.Weather.Temperature >= minTemperature && shift.Weather.Temperature <= maxTemperature).ToList();
        //}

        //public List<ShiftRecord> FilterByDaysOfWeek(List<DayOfWeek> daysOfWeek)
        //{
        //    return Shifts.Where(shift => daysOfWeek.Contains(shift.Date.DayOfWeek)).ToList();
        //}

        //public List<ShiftRecord> FilterBySpecialEvent(bool isSpecialEvent)
        //{
        //    return Shifts.Where(shift => shift.SpecialEvent == isSpecialEvent).ToList();
        //}

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
