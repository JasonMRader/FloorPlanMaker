using CsvHelper;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TableStat
    {
        private string table;
        private float amount;

        public string TableStatNumber { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public DateOnly Date { get; set; }
        public bool IsLunch { get; set; }
        public float? Sales { get; set; }
        public int Orders { get; set; } = 0;

        public TableStat(string table, DayOfWeek dayOfWeek, DateOnly date, bool isLunch, float sales)
        {
            TableStatNumber = table;
            DayOfWeek = dayOfWeek;
            Date = date;
            IsLunch = isLunch;
            Sales = sales;
        }
        public TableStat() { }
        public TableStat(string table, DayOfWeek dayOfWeek, DateOnly date, bool isLunch, float amount, int orders)
        {
            this.table = table;
            DayOfWeek = dayOfWeek;
            Date = date;
            IsLunch = isLunch;
            this.amount = amount;
            Orders = orders;
        }
    }
}
//    public class TableStats
//    {
//        public TableStats() { }
//        public Table Table { get; set; }
//        public DayOfWeek DayOfWeek { get; set; }
//        public DateOnly DateOnly { get; set; }
//        public bool IsLunch { get; set; }
//        public float Sales { get; set; } = 0f;
//        public List<OrderDetail> ReadOrderDetails(string filePath)
//        {
//            using (var reader = new StreamReader(filePath))
//            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
//            {
//                var records = csv.GetRecords<OrderDetail>().ToList();
//                return records;
//            }
//        }
//        public void AnalyzeSales(List<OrderDetail> orders)
//        {
//            var before4pm = orders.Where(x => x.Opened.Hour < 16);
//            var after4pm = orders.Where(x => x.Opened.Hour >= 16);

//            var avgSalesBefore4pm = before4pm
//                .GroupBy(x => x.Table)
//                .Select(g => new { Table = g.Key, AverageSales = g.Average(x => x.Amount) });

//            var avgSalesAfter4pm = after4pm
//                .GroupBy(x => x.Table)
//                .Select(g => new { Table = g.Key, AverageSales = g.Average(x => x.Amount) });

//            // Output or further processing of avgSalesBefore4pm and avgSalesAfter4pm
//        }

//    }
//}
