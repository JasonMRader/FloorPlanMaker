using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class TodayHourlyWeather
    {
        private static List<HourlyWeatherData> hourlyWeatherDataList = new List<HourlyWeatherData>();
        public static List<HourlyWeatherData> HourlyWeatherDataList
        {
            get { return hourlyWeatherDataList; }
        }
        public static List<HourlyWeatherData> LunchHourlyWeatherDataList
        {
            get
            {
                return hourlyWeatherDataList
                    .Where(w => w.Date.Hour >= 11 && w.Date.Hour < 16)
                    .ToList();
            }
        }

        public static List<HourlyWeatherData> DinnerHourlyWeatherDataList
        {
            get
            {
                return hourlyWeatherDataList
                    .Where(w => w.Date.Hour >= 16 && w.Date.Hour <= 23)
                    .ToList();
            }
        }
        public static async Task InitializeAsync()
        {            
            hourlyWeatherDataList = await WeatherApiDataAccess.GetWeatherForToday();
        }
    }
}
