using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Configuration;

namespace FloorplanClassLibrary
{
    public static class WeatherApiDataAccess
    {
        
        public static async Task<WeatherData> GetWeatherForSingleDate(DateTime date)
        {
            string apiKey = GetDailyApiKey();
            string dateOnlyFormatted = date.ToString("yyyy-MM-dd");
            string result = "";
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/" +
                    $"Indianapolis%2CIN/{dateOnlyFormatted}?unitGroup=us&include=days&key={apiKey}&include=days&elements=tempmax,tempmin,temp," +
                    $"feelslike,feelslikemax,feelslikemin,cloudcover,precip,precipcover,preciptype,windspeedmax,windspeedmean");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode(); // Throw an exception if error

                var body = await response.Content.ReadAsStringAsync();

                // Deserialize JSON and process data as needed
                dynamic weather = JsonConvert.DeserializeObject(body);
                if (weather == null)
                {
                    return null;
                }
                foreach (var day in weather.days)
                {
                    string weatherDate = day.datetime;
                    string weatherDescription = day.description;
                    string weatherMaxTemp = day.tempmax;
                    string weatherMinTemp = day.tempmin;
                    string weatherAvgTemp = day.temp;
                    string weatherFeelsLikeAvg = day.feelslike;
                    string weatherFeelsLikeMax = day.feelslikemax;
                    string weatherFeelsLikeMin = day.feelslikemin;
                    string weatherCloudCover = day.cloudCover;
                    string weatherPrecip = day.precip;
                    string weatherPrecipCover = day.precipcover;
                    string[] weatherPrecipTypeArray = day.preciptype?.ToObject<string[]>();
                    string weatherWindSpeedMax = day.windspeedmax;
                    string weatherWindSpeedAvg = day.windspeedmean;
                    return new WeatherData(date, weatherMaxTemp, weatherMinTemp, weatherAvgTemp, weatherFeelsLikeMax, weatherFeelsLikeMin,
                        weatherFeelsLikeAvg, weatherCloudCover, weatherPrecip, weatherPrecipCover, weatherPrecipTypeArray, weatherWindSpeedMax, weatherWindSpeedAvg);
                   



                }
                return null;
                //return result;
            }

        }
        public static async Task<List<HourlyWeatherData>> GetWeatherForecast()
        {
            try
            {
                string apiKey = GetHourlyApiKey();
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get,
                        $"http://api.weatherapi.com/v1/forecast.json?key={apiKey}&q=46254&days=2&aqi=no&alerts=no");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode(); // Throw an exception if error

                    var body = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON and process data
                    dynamic weather = JsonConvert.DeserializeObject(body);
                    if (weather == null)
                    {
                        // Handle case where deserialization fails or data is null
                        return null;
                    }

                    List<HourlyWeatherData> hourlyWeatherDataList = new List<HourlyWeatherData>();
                    for (int i = 0; i < weather.forecast.forecastday.Count; i++)
                    {
                        foreach (var hourData in weather.forecast.forecastday[i].hour)
                        {
                            DateTime time = hourData.time;
                            int hour = time.Hour;
                            if (hour >= 11 && hour <= 23) // Filter for 11 AM to 11 PM
                            {
                                var hourlyData = new HourlyWeatherData
                                {
                                    Date = time,
                                    TempHi = (int)Math.Round((double)hourData.temp_f), // Temperature in Fahrenheit
                                    TempLow = (int)Math.Round((double)hourData.windchill_f), // Windchill in Fahrenheit
                                    TempAvg = (int)Math.Round((double)hourData.feelslike_f), // Feels like temperature in Fahrenheit
                                    FeelsLikeHi = (int)Math.Round((double)hourData.heatindex_f), // Heat index in Fahrenheit
                                    FeelsLikeLow = (int)Math.Round((double)hourData.windchill_f), // Windchill in Fahrenheit
                                    FeelsLikeAvg = (int)Math.Round((double)hourData.feelslike_f), // Feels like temperature in Fahrenheit
                                    CloudCover = (float)hourData.cloud, // Cloud cover as a percentage
                                    PrecipitationAmount = (float)hourData.precip_in, // Precipitation amount in inches
                                    PrecipitationChance = (float)hourData.chance_of_rain, // Chance of rain as a percentage
                                    PrecipitationType = hourData.condition.text, // Weather condition description
                                    WindSpeedMax = (int)Math.Round((double)hourData.gust_mph), // Wind gust in MPH
                                    WindSpeedAvg = (int)Math.Round((double)hourData.wind_mph) // Average wind speed in MPH
                                };

                                hourlyWeatherDataList.Add(hourlyData);
                            }
                        }
                    }
                    

                    return hourlyWeatherDataList;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors (e.g., network issues, invalid response)
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                // Optionally, log the error details or notify the user
            }
            catch (Exception ex)
            {
                // Handle other potential errors
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                // Optionally, log the error details or notify the user
            }

            return new List<HourlyWeatherData>(); // Return null or an empty list if an error occurred
        }
        public static async Task<List<HourlyWeatherData>> GetHourlyWeatherHistory(List<DateOnly> missingDates)
        {
            try
            {
                string apiKey = GetHourlyApiKey();
                List<string> missingDateStrings = new List<string>();
                foreach (DateOnly dateOnly in missingDates)
                {
                    missingDateStrings.Add(dateOnly.ToString("yyyy-MM-dd"));
                }
                using (var client = new HttpClient())
                {
                    List<HourlyWeatherData> hourlyWeatherDataList = new List<HourlyWeatherData>();
                    for(int i = 0; i < missingDateStrings.Count; i++)
                    {
                        var request = new HttpRequestMessage(HttpMethod.Get,
                       $"http://api.weatherapi.com/v1/history.json?key={apiKey}&q=46254&dt={missingDateStrings[i]}");

                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode(); 

                        var body = await response.Content.ReadAsStringAsync();

                       
                        dynamic weather = JsonConvert.DeserializeObject(body);
                        if (weather == null)
                        {                           
                            continue;
                        }

                        foreach (var hourData in weather.forecast.forecastday[0].hour)
                        {
                            DateTime time = hourData.time;
                            int hour = time.Hour;
                            if (hour >= 11 && hour <= 23)
                            {
                                var hourlyData = new HourlyWeatherData
                                {
                                    Date = time,
                                    TempHi = (int)Math.Round((double)hourData.temp_f), 
                                    TempLow = (int)Math.Round((double)hourData.windchill_f),
                                    TempAvg = (int)Math.Round((double)hourData.feelslike_f), 
                                    FeelsLikeHi = (int)Math.Round((double)hourData.heatindex_f), 
                                    FeelsLikeLow = (int)Math.Round((double)hourData.windchill_f),                                     FeelsLikeAvg = (int)Math.Round((double)hourData.feelslike_f),
                                    CloudCover = (float)hourData.cloud, 
                                    PrecipitationAmount = (float)hourData.precip_in, 
                                    PrecipitationChance = (float)hourData.chance_of_rain,
                                    PrecipitationType = hourData.condition.text,
                                    WindSpeedMax = (int)Math.Round((double)hourData.gust_mph), 
                                    WindSpeedAvg = (int)Math.Round((double)hourData.wind_mph),
                                    SnowAmount_CM = (float)hourData.snow_cm
                                };

                                hourlyWeatherDataList.Add(hourlyData);
                            }
                        }
                        
                    }
                   


                    return hourlyWeatherDataList;
                }
            }
            catch (HttpRequestException ex)
            {
                // Handle HTTP request errors (e.g., network issues, invalid response)
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                // Optionally, log the error details or notify the user
            }
            catch (Exception ex)
            {
                // Handle other potential errors
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                // Optionally, log the error details or notify the user
            }

            return new List<HourlyWeatherData>(); 
        }

        private static string GetDailyApiKey()
        {
            
            string apiKey = ConfigurationManager.AppSettings["WeatherApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return apiKey;
        }
        private static string GetHourlyApiKey()
        {

            string apiKey = ConfigurationManager.AppSettings["HourlyWeatherApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return apiKey;
        }

    }
}
