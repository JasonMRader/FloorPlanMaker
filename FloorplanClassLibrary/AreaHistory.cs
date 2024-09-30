using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public record AreaHistory
    {
        public AreaHistory(DiningArea area, DateOnly dateOnly, bool isAm)
        {
            DiningArea = area;
            DateOnly = dateOnly;
            Sales = DiningArea.GetTotalSalesForDateAndIsAm(isAm, dateOnly);
            IsAm = isAm;
            ServerCount = GetServerCount();
        }
        public AreaHistory(AreaHistory areaHistory)
        {
            DiningArea = areaHistory.DiningArea;
            DateOnly = areaHistory.DateOnly;
            IsAm = areaHistory.IsAm;
        }

        public AreaHistory(DiningAreaRecord areaRecord)
        {
            DiningArea = SqliteDataAccess.LoadDiningAreaByID(areaRecord.DiningAreaID);
            DateOnly = areaRecord.DateOnly;
            Sales = areaRecord.Sales;
            IsAm = areaRecord.IsAm;
            ServerCount = areaRecord.ServerCount;
        }
        public static List<AreaHistory> GetAreaHistoriesFromAreaRecords(List<DiningAreaRecord> records)
        {
            List<AreaHistory> areaHistories = new List<AreaHistory>();
            foreach (DiningAreaRecord record in records) {
                if (record.DiningAreaID != 0) {
                    AreaHistory history = new AreaHistory(record);
                    areaHistories.Add(history);
                }

            }
            return areaHistories;
        }
        public static List<AreaHistory> GetAverageHistoriesFromRecords(List<DiningAreaRecord> records, List<DiningArea> areas)
        {
            List<AreaHistory> areaHistories = new List<AreaHistory>();
            foreach (DiningAreaRecord record in records) {
                if (record.DiningAreaID != 0) {
                    AreaHistory history = new AreaHistory(record);
                    areaHistories.Add(history);
                }

            }
            List<AreaHistory> groupedHistories = new List<AreaHistory>();
            foreach(DiningArea area in  areas) {
                var singleAreaHistory = areaHistories.Where(h => h.DiningArea.ID == area.ID).ToList();
                if(singleAreaHistory.Count > 0) {
                    groupedHistories.Add(GetAverageHistory(singleAreaHistory));
                }
                
            }

            return groupedHistories;
        }
        public static AreaHistory GetAverageHistory(List<AreaHistory> histories)
        {
            if(histories == null) { return null; }
            AreaHistory first = histories.First();
            AreaHistory history = new AreaHistory(first);
           
            history.Sales = histories.Sum(s => s.Sales) / histories.Count();
            history.ServerCount = (int)Math.Round((double)(histories.Sum(h => h.ServerCount) / histories.Count()));
            history.IsAverage = true;
            return history;
        }
        public DiningArea DiningArea { get; set; }
        public DateOnly DateOnly { get; set; }
        public List<WeatherData> WeatherData { get; set; } = new List<WeatherData>();
        public float Sales { get; set; }
        public bool IsAm { get; set; }
        bool IsAverage { get; set; }
        public int ServerCount { get; set; }
        public string GetAreaHistoryLabelToolTip(bool isDayBefore)
        {
            string isLunch = "PM";
            string areaName = this.DiningArea.Name;
            string dayOfWeek = this.DateOnly.DayOfWeek.ToString();

            if (this.IsAm)
            {
                isLunch = "AM";
            }            
            if (!IsAverage && !isDayBefore)
            {
                return $"|# of Servers| and Sales for {areaName} Last {dayOfWeek} {isLunch}";
            }
            if (!IsAverage && isDayBefore)
            {
                return $"|# of Servers| and Sales for {areaName} Yesterday {isLunch}";
            }
            if (IsAverage && !isDayBefore)
            {
                return $"Average of |# of Servers| and Sales for {areaName} Last 4 {dayOfWeek} {isLunch}s";
            }
            if (IsAverage && isDayBefore)
            {
                return $"Average of |# of Servers| and Sales for {areaName} Last 4 {dayOfWeek} {isLunch}s";
            }

            return "";

        }
        private int GetServerCount()
        {
            Floorplan matchedFP = SqliteDataAccess.LoadFloorplanByCriteria(DiningArea, DateOnly, IsAm);
            if(matchedFP != null)
            {
                return matchedFP.Servers.Count();
            }
            return 0;            
        }
        public void SetDatesToLastFourWeekdays()
        {
           
            var previousWeekdays = new List<DateOnly>();
            for (int i = 1; i <= 4; i++)
            {
                previousWeekdays.Add(DateOnly.AddDays(-7 * i));
            }
            int serversUsed = 0;
            foreach (DateOnly day in previousWeekdays)
            {
                Floorplan matchedFP = SqliteDataAccess.LoadFloorplanByCriteria(DiningArea, day, IsAm);
                if (matchedFP != null)
                {
                   serversUsed += matchedFP.Servers.Count();
                }               
            }
            ServerCount = (int)Math.Round((double)serversUsed / 4);
            List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateListAndLunch(IsAm, previousWeekdays);
            IsAverage = true;
            
            this.Sales = DiningArea.SetTableSales(stats);
        }
    }
}
