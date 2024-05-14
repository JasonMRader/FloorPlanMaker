using CsvHelper;
using System;
using System.Collections.Generic;
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
        public List<DateOnly> DatesAveraged { get; set; }
        public bool IsLunch { get; set; }
        public string DiningAreaTotalSalesDisplay { get; private set; }
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
        public void SetDateToToday(DateOnly dateOnly)
        {
            this.Stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(IsLunch, dateOnly);
        }
        public void SetDateToYesterday(DateOnly dateOnly)
        {
            this.Stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(IsLunch, dateOnly.AddDays(-1));
        }
        public void SetDateToLastWeek(DateOnly dateOnly)
        {
            this.Stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(IsLunch, dateOnly.AddDays(-7));
        }
        public void SetDatesToLastFourWeekdays(DateOnly dateOnly)
        {
            var day = dateOnly;


            var previousWeekdays = new List<DateOnly>();


            for (int i = 1; i <= 4; i++)
            {

                previousWeekdays.Add(day.AddDays(-7 * i));
            }


            this.Stats = SqliteDataAccess.LoadTableStatsByDateListAndLunch(IsLunch, previousWeekdays);
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
