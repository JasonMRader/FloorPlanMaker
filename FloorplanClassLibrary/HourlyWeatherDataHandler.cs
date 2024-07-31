﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class HourlyWeatherDataHandler
    {
        public async static Task<List<HourlyWeatherData>> GetWeatherForShift(DateOnly dateOnly, bool isLunch)
        {
            DateOnly today = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            DateOnly tomorrow = new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day);
            if(dateOnly == today) 
            {
                if (isLunch)
                {
                    return HourlyWeatherForecast.TodayLunchHourlyWeatherDataList;
                }
                else
                {
                    return HourlyWeatherForecast.TodayDinnerHourlyWeatherDataList;
                }

            }
            else if(dateOnly == tomorrow) 
            {
                if (isLunch)
                {
                    return HourlyWeatherForecast.TomorrowLunchHourlyWeatherDataList;
                }
                else
                {
                    return HourlyWeatherForecast.TomorrowDinnerHourlyWeatherDataList;
                }

            }
            else if (dateOnly < today)
            {
                return await GetPastWeatherData(dateOnly, isLunch);
            }
            else
            {
                return null;
            }
        }
        public async static Task <List<HourlyWeatherData>> GetPastWeatherData(DateOnly dateOnly, bool isLunch)
        {
            List<HourlyWeatherData> hourlyData = SqliteDataAccess.LoadHourlyWeatherData(dateOnly, isLunch);
            if(hourlyData == null)
            {
                hourlyData = await WeatherApiDataAccess.GetHourlyWeatherHistory(dateOnly);
                SqliteDataAccess.SaveOrUpdateHourlyWeatherData(hourlyData);
            }
            return hourlyData;
        }
        

    }
}
