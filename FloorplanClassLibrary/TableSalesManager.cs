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
            var tableStatsList = new List<TableStats>();

            foreach (var order in orders)
            {
                var date = DateOnly.FromDateTime(order.Opened);
                var dayOfWeek = order.Opened.DayOfWeek;
                var isLunch = order.Opened.Hour < 16;
                var tableKey = (order.Table, date, isLunch);

                var existingStat = tableStatsList.FirstOrDefault(ts => ts.Table == tableKey.Table &&
                                                                       ts.Date == tableKey.date &&
                                                                       ts.IsLunch == tableKey.isLunch);

                if (existingStat != null)
                {
                    existingStat.Sales += (float)order.Amount;
                    existingStat.Orders += 1; // Increment the order count
                }
                else
                {
                    var newStat = new TableStats(order.Table, dayOfWeek, date, isLunch, (float)order.Amount, 1);
                    tableStatsList.Add(newStat);
                }
            }

            return tableStatsList;
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
