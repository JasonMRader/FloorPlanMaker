using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class HourlyWeatherForecast
    {
        private static List<HourlyWeatherData> hourlyWeatherDataList = new List<HourlyWeatherData>();
        public static List<HourlyWeatherData> HourlyWeatherDataList
        {
            get { return hourlyWeatherDataList; }
        }
        
        public static List<HourlyWeatherData> TodayLunchHourlyWeatherDataList
        {
            //get
            //{
            //    return hourlyWeatherDataList
            //        .Where(w => w.Date.Hour >= 11 && w.Date.Hour < 16
            //        && w.Date.Date == DateTime.Today)
            //        .ToList();
            //}
            get
            {
                List<HourlyWeatherData> blankList = new List<HourlyWeatherData>();
                List<HourlyWeatherData> dataList = hourlyWeatherDataList
                    .Where(w => w.Date.Hour >= 11 && w.Date.Hour < 16
                    && w.Date.Date == DateTime.Today)
                    .ToList();
                if (dataList.Count > 0)
                {
                    blankList.AddRange(dataList);
                }
                return blankList;

            }
        }

        public static List<HourlyWeatherData> TodayDinnerHourlyWeatherDataList
        {
           
            get
            {
                List<HourlyWeatherData> blankList = new List<HourlyWeatherData>();
                List<HourlyWeatherData> dataList = hourlyWeatherDataList
                    .Where(w => w.Date.Hour >= 16 && w.Date.Hour <= 22
                    && w.Date.Date == DateTime.Today)
                    .ToList();
                if (dataList.Count > 0)
                {
                    blankList.AddRange(dataList);
                }
                return blankList;

            }
        }
        public static List<HourlyWeatherData> TomorrowLunchHourlyWeatherDataList
        {
            get
            {
                List<HourlyWeatherData> blankList = new List<HourlyWeatherData>();
                List<HourlyWeatherData> dataList = hourlyWeatherDataList
                    .Where(w => w.Date.Hour >= 11 && w.Date.Hour < 16
                    && w.Date.Date == DateTime.Today.AddDays(1))
                    .ToList();
                if (dataList.Count > 0)
                {
                    blankList.AddRange(dataList);
                }
                return blankList;
                
            }
        }

        public static List<HourlyWeatherData> TomorrowDinnerHourlyWeatherDataList
        {
            get
            {
                return hourlyWeatherDataList
                    .Where(w => w.Date.Hour >= 16 && w.Date.Hour <= 22
                    && w.Date.Date == DateTime.Today.AddDays(1))
                    .ToList();
            }
        }
        public static async Task InitializeAsync()
        {            
            hourlyWeatherDataList = await WeatherApiDataAccess.GetWeatherForecast();
           
        }
    }
}
