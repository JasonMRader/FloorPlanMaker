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
        public int? DiningAreaID { get; set; }
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
        public override string ToString()
        {
            return "{" + TableStatNumber + "} " + ", " + DayOfWeek.ToString() + ", " + Date.ToString() +
                ", IsLunch: " + IsLunch.ToString() + ", " + Sales.ToString();
        }
    }
}

