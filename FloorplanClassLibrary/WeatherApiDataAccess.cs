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
        //https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Indianapolis%2CIN/today?unitGroup=us&key=PHS9PNFLLHZRKPCK59EKP9BZ2&contentType=json
        public static async Task<string> GetWeatherForSingleDateString(DateTime date)
        {
            string apiKey = GetApiKey();
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
                    string weatherPrecipType = day.preciptype;
                    string weatherWindSpeedMax = day.windspeedmax;
                    string weatherWindSpeedAvg = day.windspeedmean;
                    WeatherData weatherData = new WeatherData(date, weatherMaxTemp, weatherMinTemp, weatherAvgTemp, weatherFeelsLikeMax, weatherFeelsLikeMin,
                        weatherFeelsLikeAvg, weatherCloudCover, weatherPrecip, weatherPrecipCover, weatherPrecipType, weatherWindSpeedMax, weatherWindSpeedAvg);
                    result += weatherDate;
                    //result += (" General conditions: " + weather_desc);
                    result += ("Hi: " + weatherMaxTemp) + "\n";
                    result += ("Low: " + weatherMinTemp) + "\n";
                    result += $"AVG: {weatherAvgTemp}" + "\n";
                    result += $"Feels like: {weatherFeelsLikeAvg}" + "\n";
                    result += $"Feels Max: {weatherFeelsLikeMax}" + "\n";
                    result += $"Feels Min: {weatherFeelsLikeMin}" + "\n";
                    result += $"Clound Cover: {weatherCloudCover}" + "\n";
                    result += $"Total Precip: {weatherPrecip}" + "\n";
                    result += $"Precip Cover: {weatherPrecipCover}" + "\n";
                    result += $"Precip Type: {weatherPrecipType}" + "\n";
                    result += $"WindMax: {weatherWindSpeedMax}" + "\n";
                    result += $"WindAvg: {weatherWindSpeedAvg}" + "\n";
                  

                    
                }
                return result;
            }
           
        }
        public static async Task<WeatherData> GetWeatherForSingleDate(DateTime date)
        {
            string apiKey = GetApiKey();
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
                    string weatherPrecipType = day.preciptype;
                    string weatherWindSpeedMax = day.windspeedmax;
                    string weatherWindSpeedAvg = day.windspeedmean;
                    return new WeatherData(date, weatherMaxTemp, weatherMinTemp, weatherAvgTemp, weatherFeelsLikeMax, weatherFeelsLikeMin,
                        weatherFeelsLikeAvg, weatherCloudCover, weatherPrecip, weatherPrecipCover, weatherPrecipType, weatherWindSpeedMax, weatherWindSpeedAvg);
                   



                }
                return null;
                //return result;
            }

        }
        private static string GetApiKey()
        {
            
            string apiKey = ConfigurationManager.AppSettings["WeatherApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return apiKey;
        }
       
    }
}
