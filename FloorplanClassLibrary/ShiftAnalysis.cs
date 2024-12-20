﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ShiftAnalysis
    {
        private static ShiftAnalysis _instance;

        
        private static readonly object _lock = new object();

       
        public static ShiftAnalysis Instance {
            get {
                lock (_lock) {
                    if (_instance == null) {
                        _instance = new ShiftAnalysis();
                    }
                    return _instance;
                }
            }
        }

        
        private ShiftAnalysis()
        {
            
        }
        public event Action FilterUpdated;
        
        private List<ShiftRecord> _shifts = new List<ShiftRecord>();
        public List<ShiftRecord> Shifts { get { return _shifts; } }
        private List<ShiftRecord> _filteredShifts { get; set; } = new List<ShiftRecord>();
        public List<ShiftRecord> FilteredShifts { get { return _filteredShifts; } }
        private List<DiningAreaStats> _diningAreaStats { get; set; } = new List<DiningAreaStats>();
        public List<DiningAreaStats> DiningAreaStats { get { return _diningAreaStats; } }
        public ShiftRecord currentShiftRecord { get; private set; }
        public List<TableStat> FilteredTableStats {
            get {
                
                return _filteredShifts.SelectMany(fs => fs.tableStats).ToList().OrderBy(ts => ts.TableStatNumber).ToList();
            }
        }
        public float FilteredShiftMaxSales {
            get {
                if (_filteredShifts.Count == 0) {
                    return 0;
                }
                return _filteredShifts.Max(s => s.Sales);
            }
        }
        public float FilteredShiftMinSales {
            get {
                if (_filteredShifts.Count == 0) {
                    return 0;
                }
                return _filteredShifts.Min(s => s.Sales);
            }
        }
        public float FilteredShiftAvgSales {
            get {
                if(_filteredShifts.Count == 0) {
                    return 0;
                }
                return _filteredShifts.Average(s => s.Sales);
            }
        }
        private bool _isAM { get; set; }
        public bool IsAM { get { return _isAM; } }
        private bool _isAllDay { get; set; }
       
        public void SetStandardFiltersForDateAndShiftType(bool isLunch, DateOnly dateOnly)
        {
            this._isAM = isLunch;
            this._endDate = dateOnly.AddDays(-1);
            this._startDate = dateOnly.AddDays(-91);
            this._filterBySpecialEvent = true;
            _filterByDayOfWeek = true;
            _filteredDaysOfWeek.Clear();
            _filteredDaysOfWeek.Add(dateOnly.DayOfWeek);
            //int currentMonth = dateOnly.Month;
            //int previousMonth = dateOnly.AddMonths(-1).Month;
            //int nextMonth = dateOnly.AddMonths(1).Month;
        }
        public void SetStandardFiltersForShift(Shift shift)
        {
            this._isAM = shift.IsAM;
            this._endDate = shift.DateOnly.AddDays(-1);
            this._startDate = shift.DateOnly.AddDays(-91);
            this._filterBySpecialEvent = true;
            _filterByDayOfWeek = true;
            _filteredDaysOfWeek.Clear();
            if(shift.DateOnly.DayOfWeek == DayOfWeek.Monday || shift.DateOnly.DayOfWeek == DayOfWeek.Tuesday
                || shift.DateOnly.DayOfWeek == DayOfWeek.Wednesday || shift.DateOnly.DayOfWeek == DayOfWeek.Thursday) {
                _filteredDaysOfWeek.Add(DayOfWeek.Monday);
                _filteredDaysOfWeek.Add(DayOfWeek.Tuesday);
                _filteredDaysOfWeek.Add(DayOfWeek.Wednesday);
                _filteredDaysOfWeek.Add(DayOfWeek.Thursday);
            }
            else {
                _filteredDaysOfWeek.Add(DayOfWeek.Friday);
                _filteredDaysOfWeek.Add(DayOfWeek.Saturday);
                _filteredDaysOfWeek.Add(DayOfWeek.Sunday);
            }
            
            if(shift.ShiftReservations != null) {
                SetReservationsRange(shift.ShiftReservations.MinRange, shift.ShiftReservations.MaxRange);
                _filterByReservations = true;
            }
            if(shift.WeatherData != null) {

            }
        }
        public void SetDefaultWeatherFilters(ShiftWeather shiftWeather)
        {

        }
        private DateOnly _startDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-91);
        private DateOnly _endDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-1);
        public bool isDayOf =false;
        
        private int _tempMin { get; set; }
        private int _tempMax { get; set; }
        private int _reservationMin { get; set; }
        private int _reservationMax { get; set; }
        private float _rainMin { get; set; }
        private float _rainMax { get; set; }
        private float _cloudMin { get; set; }
        private float _cloudMax { get; set; }
        private int _windMaxMin { get; set; }
        private int _windMaxMax { get; set; }
        private int _windAvgMin { get; set; }
        private int _windAvgMax { get; set; }

        
        public int TempMin => _tempMin;
        public int TempMax => _tempMax;
        public int ReservationMin => _reservationMin;
        public int ReservationMax => _reservationMax;
        public float RainMin => _rainMin;
        public float RainMax => _rainMax;
        public float CloudMin => _cloudMin;
        public float CloudMax => _cloudMax;
        public int WindMaxMin => _windMaxMin;
        public int WindMaxMax => _windMaxMax;
        public int WindAvgMin => _windAvgMin;
        public int WindAvgMax => _windAvgMax;

        private bool _filterByTemperature { get; set; } = false;
        public bool IsFilterByTemperature { get { return _filterByTemperature; } }

        private bool _filterByRainAmount { get; set; } = false;
        public bool IsFilteredByRainAmount { get { return _filterByRainAmount; } }

        private bool _filterByClouds { get; set; } = false;
        public bool IsFilteredByClouds { get { return _filterByClouds; } }

        private bool _filterByWindMax { get; set; } = false;
        public bool IsFilteredByWindMax { get { return _filterByWindMax; } }

        private bool _filterByWindAvg { get; set; } = false;
        public bool IsFilteredByWindAvg { get { return _filterByWindAvg; } }

        private bool _filterByDayOfWeek { get; set; } = false;
        public bool IsFilteredByDayOfWeek { get { return _filterByDayOfWeek; } }

        private bool _filterByDiningArea { get; set; } = false;
        public bool IsFilteredByDiningArea { get { return _filterByDiningArea; } }

        private bool _filterByMonth { get; set; } = false;
        public bool IsFilteredByMonth { get { return _filterByMonth; } }

        private bool _filterByReservations { get; set; } = false;
        public bool IsFilteredByReservations { get { return _filterByReservations; } }

        private bool _filterBySpecialEvent { get; set; } = false;
        private bool _specialEventsAllowed { get; set; } = false;
        public bool SpecialEventsAllowed { get { return _specialEventsAllowed; } }
        private List<DayOfWeek> _filteredDaysOfWeek = new List<DayOfWeek>
       {
            DayOfWeek.Monday,
            DayOfWeek.Tuesday,
            DayOfWeek.Wednesday,
            DayOfWeek.Thursday,
            DayOfWeek.Friday,
            DayOfWeek.Saturday,
            DayOfWeek.Sunday
        };
        public List<DayOfWeek> FilteredDaysOfWeek {  get { return _filteredDaysOfWeek; } }
        private List<int> _filteredMonths = new List<int>
        {
           1,2,3,4,5,6,7,8,9,10,11,12
        };
        public List<int> FilteredMonths { get { return _filteredMonths; } }
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
            this._tempMin = tempAnchor;
            this._tempMax = tempRange;
        }
        public void SetReservationsRange(int anchor, int range) {
            this._reservationMin = anchor;
            this._reservationMax = range;
        }
        public void SetRainRange(float anchor, float range) {
            this._rainMin = anchor;
            this._rainMax = range;
        }
        public void SetCloudRange(float anchor, float range) {
            this._cloudMin = anchor;
            this._cloudMax = range;
        }
        public void SetWindMaxRange(int anchor, int range) {
            this._windMaxMin = anchor;
            this._windMaxMax = range;
        }
        public void SetWindAvgRange(int anchor, int range) {
            this._windAvgMin = anchor;
            this._windAvgMax = range;
        }
        public void SetSpecialEvents(bool specialEventsAllowed) {
            this._specialEventsAllowed = specialEventsAllowed;
        }
        public void RemoveMonth(int month) {
            if (this._filteredMonths.Contains(month)) {
                this._filteredMonths.Remove(month);
            }
        }
        public  void AddMonth(int month) {
            if(!this._filteredMonths.Contains(month)) {
                this._filteredMonths.Add(month);
            }
        }
        public void RemoveDayOfWeek(DayOfWeek dayOfWeek) {
            if (this._filteredDaysOfWeek.Contains(dayOfWeek)) {
                this._filteredDaysOfWeek.Remove(dayOfWeek);
                _filteredShifts.RemoveAll(s => s.Date.DayOfWeek == dayOfWeek);
            }
        }
        public void AddDayOfWeek(DayOfWeek dayOfWeek) {
            if (!this._filteredDaysOfWeek.Contains(dayOfWeek)) {
                this._filteredDaysOfWeek.Add(dayOfWeek);
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
            CalculateDiningAreaStats();
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
            FilterUpdated?.Invoke();
            
        }

        private void FilterByMonth() {
            _filteredShifts = _filteredShifts.Where(shift => _filteredMonths.Contains(shift.Date.Month)).ToList();
        }

        private void FilterByDiningArea() {
            
        }

        private void FilterByWindAvg() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.WindAvg >= (WindAvgMin)
            && shift.ShiftWeather.WindAvg <= (WindAvgMax)).ToList();
        }

        private void FilterByWindMax() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.WindMax >= (WindMaxMin)
            && shift.ShiftWeather.WindMax <= (WindMaxMax)).ToList();
        }

        private void FilterByClouds() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.CloudCoverAverage >= (CloudMin) 
            && shift.ShiftWeather.CloudCoverAverage <= (CloudMax)).ToList();
        }

        private void FilterByRainAmount() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.RainAmount >= (_rainMin) 
            && shift.ShiftWeather.RainAmount <= (_rainMax)).ToList();
        }       
       
       
        public void FilterByTempRange() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.FeelsLikeAvg >= (_tempMin) 
            && shift.ShiftWeather.FeelsLikeAvg <= (_tempMax)).ToList();
        }

        public void FilterByReservationRange()
        {
            _filteredShifts = _filteredShifts.Where(shift => shift.Reservations >= (_reservationMin)
            && shift.Reservations <= (_reservationMax)).ToList();
        }

        public void FilterByDaysOfWeek()
        {
            _filteredShifts = _filteredShifts.Where(shift => _filteredDaysOfWeek.Contains(shift.Date.DayOfWeek)).ToList();
        }

        public void FilterBySpecialEvent()
        {
            if(_specialEventsAllowed)
            {
                _filteredShifts = _filteredShifts.Where(shift => shift.SpecialEventDate != null).ToList();
            }
            else
            {
                _filteredShifts = _filteredShifts.Where(shift => shift.SpecialEventDate == null).ToList();
            }
            
        }
        public void CalculateDiningAreaStats()
        {
            var areaStats = new Dictionary<int, List<float>>();
            var areaPercentage = new Dictionary<int, List<float>>();
            // Gather sales data for each area
            foreach (var shift in _filteredShifts) {
                foreach (var area in shift.DiningAreaRecords) {
                    if (!areaStats.ContainsKey(area.DiningAreaID)) {
                        areaStats[area.DiningAreaID] = new List<float>();
                    }
                    if (!areaPercentage.ContainsKey(area.DiningAreaID)) {
                        areaPercentage[area.DiningAreaID] = new List<float>();
                    }
                    areaStats[area.DiningAreaID].Add(area.Sales);
                    areaPercentage[area.DiningAreaID].Add(area.PercentageOfSales);
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
                    MaxPercentage = areaPercentage[entry.Key].Max(),  
                    MinPercentage = areaPercentage[entry.Key].Min(),
                    TotalSales = entry.Value.Sum()
                };
                if(stats.DiningAreaID > 0) {
                    results.Add(stats);
                }
                
            }

            // Calculate percentage of total sales for each area
            float totalSalesAcrossAllAreas = results.Sum(r => r.TotalSales);
            foreach (var result in results) {
                result.PercentageOfTotalSales = (result.TotalSales / totalSalesAcrossAllAreas) * 100;
            }
            
            _diningAreaStats = results.OrderBy(r => r.DiningAreaID).ToList();
            foreach(var areaStat in _diningAreaStats) {
                SalesRegistry.SetDiningAreaExpectedSales(areaStat.DiningAreaID, areaStat.AvgSales);
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
