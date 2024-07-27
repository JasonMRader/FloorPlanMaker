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
        public static async Task<string> GetWeatherForSingleDate()
        {
            string apiKey = GetApiKey();
            string result = "";
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get,
                    $"https://weather.visualcrossing.com/VisualCrossingWebServices/rest/services/timeline/" +
                    $"Indianapolis%2CIN/2024-07-01?unitGroup=us&include=days&key={apiKey}&include=days&elements=tempmax,tempmin,temp");

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

                    result += weather_date;
                    //result += (" General conditions: " + weather_desc);
                    result += ("Hi: " + weather_tmax);
                    result += ("Low: " + weather_tmin);
                    result += "\n";
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
