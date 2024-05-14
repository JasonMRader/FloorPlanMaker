using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TableSalesManager
    {
        public TableSalesManager() { }
        public List<TableStat> Stats { get; set; } = new List<TableStat>();
        public List<DateOnly> DatesAveraged { get; set; }
        public bool IsAm { get; set; }
        
        public float? DiningAreaExpectedSales
        {
            get
            {
                return Stats.Sum(stat => stat.Sales);
            }
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

        }
        public void SetDateToYesterday(DateOnly dateOnly)
        {

        }
        public void SetDateToLastWeek(DateOnly dateOnly)
        {

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
