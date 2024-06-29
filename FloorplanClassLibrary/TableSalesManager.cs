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
       
        public List<TableStat> ProcessCsvFile(string filePath)
        {
            var orders = ReadOrderDetails(filePath);

            var groupedOrders = orders
                .Where(order => order.Table != "") 
                .GroupBy(order => new
                {
                    Table = order.Table,  
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


    }
}
