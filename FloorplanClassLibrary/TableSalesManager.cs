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
        public float? FloorplanSalesTotal
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
        public List<TableStat> GetStatsByShiftAndDayOfWeek(List<TableStat> allStats, bool isLunch, DayOfWeek dayOfWeek)
        {
            return allStats
                .Where(ts => ts.IsLunch == isLunch && ts.DayOfWeek == dayOfWeek)
                .ToList();
        }
        public List<TableStat> GetStatsByDateRange(List<TableStat> allStats, DateOnly startDate, DateOnly endDate)
        {
            return allStats
                .Where(ts => ts.Date >= startDate && ts.Date <= endDate)
                .ToList();
        }
        public void SetTableStats(List<Table> tables, bool isLunch, DateOnly dateOnly)
        {
            List<TableStat> tableStats = SqliteDataAccess.LoadTableStats();
            TableSalesManager tableSalesManager = new TableSalesManager();
            List<TableStat> filteredTableStats = tableSalesManager.GetStatsByShiftAndDayOfWeek(tableStats, isLunch, dateOnly.DayOfWeek);
            foreach (Table table in tables)
            {
                var matchedStat = filteredTableStats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                if (matchedStat != null)
                {
                    table.AverageSales = (float)matchedStat.Sales;
                }
                else { table.AverageSales = -1; }
            }
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
        public void AnalyzeSales(List<OrderDetail> orders)
        {
            var before4pm = orders.Where(x => x.Opened.Hour < 16);
            var after4pm = orders.Where(x => x.Opened.Hour >= 16);

            var avgSalesBefore4pm = before4pm
                .GroupBy(x => x.Table)
                .Select(g => new { Table = g.Key, AverageSales = g.Average(x => x.Amount) });

            var avgSalesAfter4pm = after4pm
                .GroupBy(x => x.Table)
                .Select(g => new { Table = g.Key, AverageSales = g.Average(x => x.Amount) });

            // Output or further processing of avgSalesBefore4pm and avgSalesAfter4pm
        }
    }
}
