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
        public SpecialEventDate SpecialEventDate {get;set;}
        public SalesData(DateTime dateTime)
        {
            this.Date = dateTime;
            var weatherData = SqliteDataAccess.LoadWeatherDataByDate(DateOnly);
            if (weatherData != null)
            {
                this.WeatherData = weatherData;
            }
            SpecialEventDate specialEventDate = SqliteDataAccess.GetEventByDate(DateOnly);
            if (specialEventDate != null)
            {
                this.SpecialEventDate = specialEventDate;
            }
        }

        private TableSalesManager tableSalesManager { get; set; }
        public DateTime Date { get; set; }
        public DateOnly DateOnly => DateOnly.FromDateTime(Date);
        public Dictionary<string, float> SalesByDiningArea { get; set; } = new Dictionary<string, float>();
        public float TotalSales { get; set; }
        public int ServersScheduled { get; set; }
        public string DateDisplay()
        {
            //SpecialEventDate specialEventDate = SqliteDataAccess.GetEventByDate(DateOnly);
            //if (specialEventDate != null)
            //{
            //   return specialEventDate.Name;
            //}
            return Date.ToString("ddd, M/d/yy");
        }
        public WeatherData WeatherData { get; set; } = new WeatherData();

    }
}
