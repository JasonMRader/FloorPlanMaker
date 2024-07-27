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
        public static async Task<string> GetWeatherForSingleDate(DateTime date)
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
                    string weather_date = day.datetime;
                    string weather_desc = day.description;
                    string weather_tmax = day.tempmax;
                    string weather_tmin = day.tempmin;
                    string weather_temp = day.temp;
                    string weather_feelslike = day.feelslike;
                    string weather_feelslikemax = day.feelslikemax;
                    string weather_feelslikemin = day.feelslikemin;
                    string weather_cloudCover = day.cloudCover;
                    string weather_precip = day.precip;
                    string weather_precipcover = day.precipcover;
                    string weather_preciptype = day.preciptype;
                    string weather_windspeedmax = day.windspeedmax;
                    string weather_windspeedmean = day.windspeedmean;

                    result += weather_date;
                    //result += (" General conditions: " + weather_desc);
                    result += ("Hi: " + weather_tmax) + "\n";
                    result += ("Low: " + weather_tmin) + "\n";
                    result += $"AVG: {weather_temp}" + "\n";
                    result += $"Feels like: {weather_feelslike}" + "\n";
                    result += $"Feels Max: {weather_feelslikemax}" + "\n";
                    result += $"Feels Min: {weather_feelslikemin}" + "\n";
                    result += $"Clound Cover: {weather_cloudCover}" + "\n";
                    result += $"Total Precip: {weather_precip}" + "\n";
                    result += $"Precip Cover: {weather_precipcover}" + "\n";
                    result += $"Precip Type: {weather_preciptype}" + "\n";
                    result += $"WindMax: {weather_windspeedmax}" + "\n";
                    result += $"WindAvg: {weather_windspeedmean}" + "\n";

                    
                }
                return result;
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
