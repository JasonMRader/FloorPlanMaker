using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ShiftAnalysis
    {
        public ShiftAnalysis(List<ShiftRecord> shifts) {
            _shifts = shifts;
        }
        private List<ShiftRecord> _shifts = new List<ShiftRecord>();
        public List<ShiftRecord> Shifts { get { return _shifts; } }
        private List<ShiftRecord> _filteredShifts { get; set; } = new List<ShiftRecord>();
        public List<ShiftRecord> FilteredShifts { get { return _filteredShifts; } }
        public bool isAM { get; set; }
        public bool isAllDay { get; set; }
        public ShiftAnalysis() { }
        private DateOnly _startDate = new DateOnly();
        private DateOnly _endDate = new DateOnly();
        public int tempAnchor { get; set; }
        public int tempRange { get; set; }
        public int ReservationAnchor { get; set; }
        public int ReservationRange { get; set; }
        public float rainAnchor { get; set; }
        public float rainRange { get; set; }
        public float CloudAnchor { get; set; }
        public float CloudRange { get; set; }
        public int WindMaxAnchor { get; set; }
        public int WindMaxRange { get; set; }
        public int WindAvgAnchor { get; set; }
        public int WindAvgRange { get; set; }
        private bool _filterByTemperature { get; set; } = false;
        private bool _filterByRainAmount { get; set; } = false;
        private bool _filterByClouds { get; set; } = false;
        private bool _filterByWindMax { get; set; } = false;
        private bool _filterByWindAvg { get; set; } = false;
        private bool _filterByDayOfWeek { get; set; }= false;
        private bool _filterByDiningArea { get; set; } = false;
        private bool _filterByMonth { get; set; } = false;
        private bool _filterByReservations { get; set; } = false;
        private bool _filterBySpecialEvent { get; set; } = false;
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
        private List<DiningArea> FilteredDiningAreas { get; set; } = new List<DiningArea>();

        public DateOnly StartDate
        {
            get { return _startDate; }
        }
        public DateOnly EndDate { get { return _endDate; } }
        public void SetIsFilteredByTemp(bool isFilteredByTemp) {
            this._filterByTemperature = isFilteredByTemp;
        }
        public void SetIsFilteredByDayOfWeek(bool  isFilteredByDayOfWeek) {
            this._filterByDayOfWeek = isFilteredByDayOfWeek;
        }
        public void SetIsFilteredbyRainAmount(bool isFilteredByRainAmount) {
            this._filterByRainAmount = isFilteredByRainAmount;
        }
        public void SetIsFilteredByClouds(bool  isFilteredByClouds) {
            this._filterByClouds = isFilteredByClouds;
        }
        public void SetIsFilteredByWindMax(bool  isFilteredByWindMax) {
            this._filterByWindMax = isFilteredByWindMax;
        }
        public void SetIsFilteredByWindAvg(bool isFilteredByWindAvg) {
            this._filterByWindAvg = isFilteredByWindAvg;
        }
        public void SetIsFilteredByReservations(bool isFilteredByReservations) {
            this._filterByReservations = isFilteredByReservations;
        }
        public void SetIsFilteredByDiningArea(bool isFilteredByDiningArea) {
            this._filterByDiningArea = isFilteredByDiningArea;
        }
        public void SetIsFilteredByMonth(bool isFilteredByMonth) {
            this._filterByMonth = isFilteredByMonth;
        }
        public void SetIsFilteredBySpecialEvent(bool isFilteredBySpecialEvent) {
            this._filterBySpecialEvent = isFilteredBySpecialEvent;
        }
        public void SetDateOnly(DateOnly startDate, DateOnly endDate)
        {
            this._endDate = endDate;
            this._startDate = startDate;
        }
        public void SetTempRange(int tempAnchor, int tempRange) {
            this.tempAnchor = tempAnchor;
            this.tempRange = tempRange;
        }
        public void SetReservationsRange(int anchor, int range) {

        }
        public void SetRainFilter(int anchor, int range) {

        }

        public void RemoveDayOfWeek(DayOfWeek dayOfWeek) {
            if (this.FilteredDaysOfWeek.Contains(dayOfWeek)) {
                this.FilteredDaysOfWeek.Remove(dayOfWeek);
                _filteredShifts.RemoveAll(s => s.Date.DayOfWeek == dayOfWeek);
            }
        }
        public void AddDayOfWeek(DayOfWeek dayOfWeek) {
            if (!this.FilteredDaysOfWeek.Contains(dayOfWeek)) {
                this.FilteredDaysOfWeek.Add(dayOfWeek);
                _filteredShifts.AddRange(_shifts.Where(s => s.Date.DayOfWeek == dayOfWeek).ToList());
            }
        }
        public void InitializetShiftsForDateRange()
        {
            this._shifts.Clear();
            for (DateOnly iDay = _startDate; iDay <= _endDate; iDay = iDay.AddDays(1))
            {
                _shifts.Add(SqliteDataAccess.LoadShiftRecord(iDay, this.isAM));
            }
            List<DateOnly> missingDates = SqliteDataAccess.GetMissingSalesDates(_startDate, _endDate);
            _shifts.RemoveAll(s => missingDates.Contains(s.dateOnly));
            
            SetFilters();
            
            
        }
       
        private void SetFilters() {
            _filteredShifts = _shifts.ToList();
            if (_filterByDayOfWeek) {
                FilterByDaysOfWeek();
            }
            if (_filterByTemperature) {
                FilterByTempRange();
            }
            if (_filterByRainAmount) {
                FilterByRainAmount();
            }
            
        }

        private void FilterByRainAmount() {
            throw new NotImplementedException();
        }

        
       
       
        public void FilterByTempRange() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.FeelsLikeAvg >= (tempAnchor - tempRange) 
            && shift.ShiftWeather.FeelsLikeAvg <= (tempAnchor + tempRange)).ToList();
        }

        public List<ShiftRecord> FilterByReservationRange(int minReservations, int maxReservations)
        {
            return Shifts.Where(shift => shift.Reservations >= minReservations && shift.Reservations <= maxReservations).ToList();
        }

        public void FilterByDaysOfWeek()
        {
            _filteredShifts = _filteredShifts.Where(shift => FilteredDaysOfWeek.Contains(shift.Date.DayOfWeek)).ToList();
        }

        public void FilterBySpecialEvent(bool isSpecialEvent)
        {
            if(isSpecialEvent)
            {
                _filteredShifts = _filteredShifts.Where(shift => shift.SpecialEventDate != null).ToList();
            }
            else
            {
                _filteredShifts = _filteredShifts.Where(shift => shift.SpecialEventDate == null).ToList();
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
