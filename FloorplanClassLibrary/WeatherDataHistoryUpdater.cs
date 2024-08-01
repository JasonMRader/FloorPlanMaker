using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class WeatherDataHistoryUpdater
    {
        public static async void SaveMissingDatesToDatabase()
        {       
            DateOnly endDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            DateOnly startDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-250));           
            List<DateOnly> missingDates = SqliteDataAccess.GetMissingWeatherDates(startDate, endDate);
            List<HourlyWeatherData> hourlyData = await WeatherApiDataAccess.GetHourlyWeatherHistory(missingDates);
            SqliteDataAccess.SaveOrUpdateHourlyWeatherData(hourlyData);
        }
        public static async void SaveMissingDatesToDatabase(DateTime start, DateTime end)
        {
            DateOnly maxEndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            DateOnly endDate = DateOnly.FromDateTime(end);
            DateOnly startDate = DateOnly.FromDateTime(start);
            if (endDate > maxEndDate)
            {
                endDate = maxEndDate;
            }
            List<DateOnly> missingDates = SqliteDataAccess.GetMissingWeatherDates(startDate, endDate);
            List<HourlyWeatherData> hourlyData = await WeatherApiDataAccess.GetHourlyWeatherHistory(missingDates);
            
           
            SqliteDataAccess.SaveOrUpdateHourlyWeatherData(hourlyData);
        }
    }
}
