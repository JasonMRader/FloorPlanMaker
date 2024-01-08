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
        public List<TableStats> ProcessCsvFile(string filePath)
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
                .Select(group => new TableStats
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
        public List<TableStats> GetStatsByShiftAndDayOfWeek(List<TableStats> allStats, bool isLunch, DayOfWeek dayOfWeek)
        {
            return allStats
                .Where(ts => ts.IsLunch == isLunch && ts.DayOfWeek == dayOfWeek)
                .ToList();
        }
        public List<TableStats> GetStatsByDateRange(List<TableStats> allStats, DateOnly startDate, DateOnly endDate)
        {
            return allStats
                .Where(ts => ts.Date >= startDate && ts.Date <= endDate)
                .ToList();
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
