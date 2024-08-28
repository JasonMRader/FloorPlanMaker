﻿using System;
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
        private List<DiningAreaStats> _diningAreaStats { get; set; } = new List<DiningAreaStats>();
        public List<DiningAreaStats> DiningAreaStats { get { return _diningAreaStats; } }
        private bool _isAM { get; set; }
        private bool _isAllDay { get; set; }
        public ShiftAnalysis() { }
        private DateOnly _startDate = new DateOnly();
        private DateOnly _endDate = new DateOnly();
        private int tempAnchor { get; set; }
        private int tempRange { get; set; }
        private int reservationAnchor { get; set; }
        private int reservationRange { get; set; }
        private float rainAnchor { get; set; }
        private float rainRange { get; set; }
        private float CloudAnchor { get; set; }
        private float CloudRange { get; set; }
        private int WindMaxAnchor { get; set; }
        private int WindMaxRange { get; set; }
        private int WindAvgAnchor { get; set; }
        private int WindAvgRange { get; set; }
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
        private bool specialEventsAllowed { get; set; } = false;
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
        public void SetIsAM(bool isAM)
        {
            this._isAM = isAM;
        }
        public void SetIsAllDay(bool isAllDay)
        {
            this._isAllDay = isAllDay;
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
            this.reservationAnchor = anchor;
            this.reservationRange = range;
        }
        public void SetRainRange(float anchor, float range) {
            this.rainAnchor = anchor;
            this.rainRange = range;
        }
        public void SetCloudRange(float anchor, float range) {
            this.CloudAnchor = anchor;
            this.CloudRange = range;
        }
        public void SetWindMaxRange(int anchor, int range) {
            this.WindMaxAnchor = anchor;
            this.WindMaxRange = range;
        }
        public void SetWindAvgRange(int anchor, int range) {
            this.WindAvgAnchor = anchor;
            this.WindAvgRange = range;
        }
        public void SetSpecialEvents(bool specialEventsAllowed) {
            this.specialEventsAllowed = specialEventsAllowed;
        }
        public void RemoveMonth(int month) {
            if (this.FilteredMonths.Contains(month)) {
                this.FilteredMonths.Remove(month);
            }
        }
        public  void AddMonth(int month) {
            if(!this.FilteredMonths.Contains(month)) {
                this.FilteredMonths.Add(month);
            }
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
                _shifts.Add(SqliteDataAccess.LoadShiftRecord(iDay, this._isAM));
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
            if(_filterByClouds) {
                FilterByClouds();
            }
            if(_filterByWindMax) {
                FilterByWindMax();
            }
            if(_filterByWindAvg) {
                FilterByWindAvg();
            }
            if(_filterByDiningArea) {
                FilterByDiningArea();
            }
            if(_filterByMonth) {
                FilterByMonth();
            }
            if(_filterByReservations) {
                FilterByReservationRange();
            }
            if(_filterBySpecialEvent) {
                FilterBySpecialEvent();
            }
            
        }

        private void FilterByMonth() {
            _filteredShifts = _filteredShifts.Where(shift => FilteredMonths.Contains(shift.Date.Month)).ToList();
        }

        private void FilterByDiningArea() {
            
        }

        private void FilterByWindAvg() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.WindAvg >= (WindAvgAnchor - WindAvgRange)
            && shift.ShiftWeather.RainAmount <= (WindAvgAnchor + WindAvgRange)).ToList();
        }

        private void FilterByWindMax() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.WindMax >= (WindMaxAnchor - WindMaxRange)
            && shift.ShiftWeather.RainAmount <= (WindMaxAnchor + WindMaxRange)).ToList();
        }

        private void FilterByClouds() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.CloudCoverAverage >= (CloudAnchor - CloudRange)
            && shift.ShiftWeather.RainAmount <= (CloudAnchor + CloudRange)).ToList();
        }

        private void FilterByRainAmount() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.RainAmount >= (rainAnchor - rainRange)
            && shift.ShiftWeather.RainAmount <= (rainAnchor + rainRange)).ToList();
        }       
       
       
        public void FilterByTempRange() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.FeelsLikeAvg >= (tempAnchor - tempRange) 
            && shift.ShiftWeather.FeelsLikeAvg <= (tempAnchor + tempRange)).ToList();
        }

        public void FilterByReservationRange()
        {
            _filteredShifts = _filteredShifts.Where(shift => shift.Reservations >= (reservationAnchor - reservationRange)
            && shift.Reservations <= (reservationAnchor + reservationRange)).ToList();
        }

        public void FilterByDaysOfWeek()
        {
            _filteredShifts = _filteredShifts.Where(shift => FilteredDaysOfWeek.Contains(shift.Date.DayOfWeek)).ToList();
        }

        public void FilterBySpecialEvent()
        {
            if(specialEventsAllowed)
            {
                _filteredShifts = _filteredShifts.Where(shift => shift.SpecialEventDate != null).ToList();
            }
            else
            {
                _filteredShifts = _filteredShifts.Where(shift => shift.SpecialEventDate == null).ToList();
            }
            
        }
        public List<DiningAreaStats> CalculateDiningAreaStats()
        {
            var areaStats = new Dictionary<int, List<float>>();

            // Gather sales data for each area
            foreach (var shift in _filteredShifts) {
                foreach (var area in shift.DiningAreaRecords) {
                    if (!areaStats.ContainsKey(area.DiningAreaID)) {
                        areaStats[area.DiningAreaID] = new List<float>();
                    }
                    areaStats[area.DiningAreaID].Add(area.Sales);
                }
            }

            // Calculate stats for each area
            var results = new List<DiningAreaStats>();
            foreach (var entry in areaStats) {
                var stats = new DiningAreaStats {
                    DiningAreaID = entry.Key,
                    MaxSales = entry.Value.Max(),
                    MinSales = entry.Value.Min(),
                    AvgSales = entry.Value.Average(),
                    TotalSales = entry.Value.Sum()
                };

                results.Add(stats);
            }

            // Calculate percentage of total sales for each area
            float totalSalesAcrossAllAreas = results.Sum(r => r.TotalSales);
            foreach (var result in results) {
                result.PercentageOfTotalSales = (result.TotalSales / totalSalesAcrossAllAreas) * 100;
            }

            return results;
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
