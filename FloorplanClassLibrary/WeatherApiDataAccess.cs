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
        public static async Task<string> WeatherData()
        {
            string apiKey = GetApiKey();
            string result = "";
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/Paris?key={apiKey}");

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

                    result += ("Forecast for date: " + weather_date);
                    result += (" General conditions: " + weather_desc);
                    result += (" The high temperature will be " + weather_tmax);
                    result += (" The low temperature will be: " + weather_tmin);
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
