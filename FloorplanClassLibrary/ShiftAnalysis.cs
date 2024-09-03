using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ShiftAnalysis
    {
        public event Action FilterUpdated;
        
        private List<ShiftRecord> _shifts = new List<ShiftRecord>();
        public List<ShiftRecord> Shifts { get { return _shifts; } }
        private List<ShiftRecord> _filteredShifts { get; set; } = new List<ShiftRecord>();
        public List<ShiftRecord> FilteredShifts { get { return _filteredShifts; } }
        private List<DiningAreaStats> _diningAreaStats { get; set; } = new List<DiningAreaStats>();
        public List<DiningAreaStats> DiningAreaStats { get { return _diningAreaStats; } }
        public List<TableStat> FilteredTableStats {
            get {
                return _filteredShifts.SelectMany(fs => fs.tableStats).ToList().OrderBy(ts => ts.TableStatNumber).ToList();
            }
        }
        public float FilteredShiftMaxSales {
            get {
                return _filteredShifts.Max(s => s.Sales);
            }
        }
        public float FilteredShiftMinSales {
            get {
                return _filteredShifts.Min(s => s.Sales);
            }
        }
        public float FilteredShiftAvgSales {
            get {
                return _filteredShifts.Average(s => s.Sales);
            }
        }
        private bool _isAM { get; set; }
        public bool IsAM { get { return _isAM; } }
        private bool _isAllDay { get; set; }
        public ShiftAnalysis() 
        { 

        }
        public void SetStandardFiltersForDateAndShiftType(bool isLunch, DateOnly dateOnly)
        {
            this._isAM = isLunch;
            this._endDate = dateOnly.AddDays(-1);
            this._startDate = dateOnly.AddDays(-91);
            this._filterBySpecialEvent = true;
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
        }
        public void SetDefaultWeatherFilters(ShiftWeather shiftWeather)
        {

        }
        private DateOnly _startDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-91);
        private DateOnly _endDate = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(-1);
        private int tempMin { get; set; }
        private int tempMax { get; set; }
        private int reservationMin { get; set; }
        private int reservationMax { get; set; }
        private float rainMin { get; set; }
        private float rainMax { get; set; }
        private float CloudMin { get; set; }
        private float CloudMax { get; set; }
        private int WindMaxMin { get; set; }
        private int WindMaxMax { get; set; }
        private int WindAvgMin { get; set; }
        private int WindAvgMax { get; set; }
        private bool _filterByTemperature { get; set; } = false;
        public bool IsFilterByTemperature { get {  return _filterByTemperature; } }
        private bool _filterByRainAmount { get; set; } = false;
        public bool IsFilteredByRainAmount { get { return _filterByRainAmount; } }
        private bool _filterByClouds { get; set; } = false;
        private bool _filterByWindMax { get; set; } = false;
        private bool _filterByWindAvg { get; set; } = false;
        private bool _filterByDayOfWeek { get; set; }= false;
        private bool _filterByDiningArea { get; set; } = false;
        private bool _filterByMonth { get; set; } = false;
        private bool _filterByReservations { get; set; } = false;
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
            this.tempMin = tempAnchor;
            this.tempMax = tempRange;
        }
        public void SetReservationsRange(int anchor, int range) {
            this.reservationMin = anchor;
            this.reservationMax = range;
        }
        public void SetRainRange(float anchor, float range) {
            this.rainMin = anchor;
            this.rainMax = range;
        }
        public void SetCloudRange(float anchor, float range) {
            this.CloudMin = anchor;
            this.CloudMax = range;
        }
        public void SetWindMaxRange(int anchor, int range) {
            this.WindMaxMin = anchor;
            this.WindMaxMax = range;
        }
        public void SetWindAvgRange(int anchor, int range) {
            this.WindAvgMin = anchor;
            this.WindAvgMax = range;
        }
        public void SetSpecialEvents(bool specialEventsAllowed) {
            this._specialEventsAllowed = specialEventsAllowed;
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
            _filteredShifts = _filteredShifts.Where(shift => FilteredMonths.Contains(shift.Date.Month)).ToList();
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
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.RainAmount >= (rainMin) 
            && shift.ShiftWeather.RainAmount <= (rainMax)).ToList();
        }       
       
       
        public void FilterByTempRange() {
            _filteredShifts = _filteredShifts.Where(shift => shift.ShiftWeather.FeelsLikeAvg >= (tempMin) 
            && shift.ShiftWeather.FeelsLikeAvg <= (tempMax)).ToList();
        }

        public void FilterByReservationRange()
        {
            _filteredShifts = _filteredShifts.Where(shift => shift.Reservations >= (reservationMin)
            && shift.Reservations <= (reservationMax)).ToList();
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
