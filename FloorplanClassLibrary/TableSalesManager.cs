using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FloorplanClassLibrary
{
    public class TableSalesManager
    {
        public TableSalesManager() { }
        public List<TableStat> Stats { get; set; } = new List<TableStat>();
        public StatsPeriod CurrentStatsPeriod { get; set; } = StatsPeriod.Today;
        public List<DateOnly> DatesAveraged { get; set; }
        public bool IsLunch { get; set; }
        public string DiningAreaTotalSalesDisplay { get; private set; }
        public enum StatsPeriod
        {
            Today,
            Yesterday,
            LastWeekday,
            LastFourWeekDays
        }
        public float? ShiftExpectedSales
        {
            get
            {
                return Stats.Sum(stat => stat.Sales);
            }
        }
        public float DiningAreaExpectedSales(DiningArea diningArea)
        {
            string test = "";
            float totalAreaSales = 0f;

            var insideBarSales = this.Stats
                .Where(t => t.TableStatNumber.CompareTo("120") >= 0 && t.TableStatNumber.CompareTo("155") <= 0)
                .Sum(t => t.Sales);
            foreach (Table table in diningArea.Tables)
            {
                if (table.TableNumber == "INSIDE BAR")
                {
                    table.AverageSales = (float)insideBarSales;
                    totalAreaSales += (float)insideBarSales;
                    test += $"\n{table.TableNumber} : {insideBarSales} : {totalAreaSales}";
                }
                else
                {
                    var matchedStat =this.Stats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                    if (matchedStat != null)
                    {
                        table.AverageSales = (float)matchedStat.Sales;
                        totalAreaSales += (float)matchedStat.Sales;
                        test += $"\n{table.TableNumber} : {matchedStat.Sales} : {totalAreaSales}";
                    }
                    else
                    {
                        table.AverageSales = 0;
                    }
                }
            }
            DiningAreaTotalSalesDisplay = Section.FormatAsCurrencyWithoutParentheses(totalAreaSales);
            return totalAreaSales;
            
        }
        public List<TableStat> ProcessCsvFile(string filePath)
        {
            var orders = ReadOrderDetails(filePath);

            var groupedOrders = orders
                .Where(order => order.Table != "")  // Exclude orders without a table number
                .GroupBy(order => new
                {
                    Table = order.Table,  // Safe to use .Value here due to the filter above
                    Date = DateOnly.FromDateTime(order.Opened),
                    IsLunch = order.Opened.Hour < 16
                })
                .Select(group => new TableStat
                {
                    TableStatNumber = group.Key.Table,
                    Date = group.Key.Date,
                    DayOfWeek = group.Key.Date.DayOfWeek,
                    IsLunch = group.Key.IsLunch,
                    Sales = group.Sum(order => (float)order.Amount),
                    Orders = group.Count()
                })
                .ToList();


            return groupedOrders;
        }
        public List<OrderDetail> ReadOrderDetails(string filePath)
        {
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<OrderDetail>().ToList();
                return records;
            }
        }
        public void SetStatsList(bool isAm, DateOnly dateOnly)
        {
            switch (this.CurrentStatsPeriod)
            {
                case StatsPeriod.Today:
                    SetDateToToday(isAm, dateOnly); 
                    break;
                case StatsPeriod.Yesterday:
                    SetDateToYesterday(isAm, dateOnly);
                    break;
                case StatsPeriod.LastWeekday:
                    SetDateToLastWeek(isAm, dateOnly);
                    break;
                case StatsPeriod.LastFourWeekDays:
                    SetDatesToLastFourWeekdays(isAm, dateOnly);
                    break;
            }
        }
        public void SetDateToToday(bool isAM, DateOnly dateOnly)
        {
            this.Stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(isAM, dateOnly);
        }
        public void SetDateToYesterday(bool isAM, DateOnly dateOnly)
        {
            this.Stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(isAM, dateOnly.AddDays(-1));
        }
        public void SetDateToLastWeek(bool isAM, DateOnly dateOnly)
        {
            this.Stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(isAM, dateOnly.AddDays(-7));
        }
        public void SetDatesToLastFourWeekdays(bool isAM, DateOnly dateOnly)
        {
            var day = dateOnly;


            var previousWeekdays = new List<DateOnly>();


            for (int i = 1; i <= 4; i++)
            {

                previousWeekdays.Add(day.AddDays(-7 * i));
            }


            this.Stats = SqliteDataAccess.LoadTableStatsByDateListAndLunch(isAM, previousWeekdays);
        }
        private static List<TableStat> CalculateAverageSales(List<TableStat> tableStats, int numberOfDays)
        {
            // Group the table stats by TableStatNumber
            var groupedStats = tableStats
                .GroupBy(ts => ts.TableStatNumber)
                .Select(group => new
                {
                    TableStatNumber = group.Key,
                    TotalSales = group.Sum(g => g.Sales ?? 0)
                });

            // Calculate average sales and create new TableStat objects
            var averageStats = groupedStats
                .Select(g => new TableStat
                {
                    TableStatNumber = g.TableStatNumber,
                    Sales = g.TotalSales / numberOfDays
                }).ToList();

            return averageStats;
        }
        //public List<TableStat> GetStatsByShiftAndDayOfWeek(List<TableStat> allStats, bool isLunch, DayOfWeek dayOfWeek)
        //{
        //    return allStats
        //        .Where(ts => ts.IsLunch == isLunch && ts.DayOfWeek == dayOfWeek)
        //        .ToList();
        //}
        //public List<TableStat> GetStatsByDateRange(List<TableStat> allStats, DateOnly startDate, DateOnly endDate)
        //{
        //    return allStats
        //        .Where(ts => ts.Date >= startDate && ts.Date <= endDate)
        //        .ToList();
        //}
        //public void SetTableStats(List<Table> tables, bool isLunch, DateOnly dateOnly)
        //{
        //    List<TableStat> tableStats = SqliteDataAccess.LoadTableStats();
        //    TableSalesManager tableSalesManager = new TableSalesManager();
        //    List<TableStat> filteredTableStats = tableSalesManager.GetStatsByShiftAndDayOfWeek(tableStats, isLunch, dateOnly.DayOfWeek);
        //    foreach (Table table in tables)
        //    {
        //        var matchedStat = filteredTableStats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
        //        if (matchedStat != null)
        //        {
        //            table.AverageSales = (float)matchedStat.Sales;
        //        }
        //        else { table.AverageSales = -1; }
        //    }
        //}


    }
}
