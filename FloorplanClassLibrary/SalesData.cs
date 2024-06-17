using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SalesData
    {
        public SalesData() { }
        public SalesData(DateTime dateTime)
        {
            this.Date = dateTime;
            this.WeatherData = SqliteDataAccess.LoadWeatherDataByDate(DateOnly);
        }
        private TableSalesManager tableSalesManager { get; set; }
        public DateTime Date { get; set; }
        public DateOnly DateOnly => DateOnly.FromDateTime(Date);
        public Dictionary<string, float> SalesByDiningArea { get; set; } = new Dictionary<string, float>();
        public float TotalSales { get; set; }
        public string DateDisplay()
        {
            SpecialEventDate specialEventDate = SqliteDataAccess.GetEventByDate(DateOnly);
            if (specialEventDate != null)
            {
               return specialEventDate.Name;
            }
            return Date.ToString("ddd, M/d");
        }
        public WeatherData WeatherData { get; set; } = new WeatherData();

    }
}
