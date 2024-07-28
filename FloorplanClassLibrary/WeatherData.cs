using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FloorplanClassLibrary
{
    public class WeatherData
    {
        public WeatherData() { }
        public WeatherData(string DateString, string TempHi, string TempLow) { }

        public WeatherData(DateTime date, string weatherMaxTemp, string weatherMinTemp, string weatherAvgTemp, 
            string weatherFeelsLikeMax, string weatherFeelsLikeMin, string weatherFeelsLikeAvg, string weatherCloudCover,
            string weatherPrecip, string weatherPrecipCover, string[] weatherPrecipTypeArray, string weatherWindSpeedMax, string weatherWindSpeedAvg)
        {
            Date1 = date;
            WeatherMaxTemp = weatherMaxTemp;
            WeatherMinTemp = weatherMinTemp;
            WeatherAvgTemp = weatherAvgTemp;
            WeatherFeelsLikeMax = weatherFeelsLikeMax;
            WeatherFeelsLikeMin = weatherFeelsLikeMin;
            WeatherFeelsLikeAvg = weatherFeelsLikeAvg;
            WeatherCloudCover = weatherCloudCover;
            WeatherPrecip = weatherPrecip;
            WeatherPrecipCover = weatherPrecipCover;
            WeatherPrecipTypeArray = weatherPrecipTypeArray;
            WeatherWindSpeedMax = weatherWindSpeedMax;
            WeatherWindSpeedAvg = weatherWindSpeedAvg;
            ConvertStringFieldsToProperties();
           
            
        }

        public int ID { get; set; }
        public string Date { get; set; }
        public int TempHi { get; set; }
        public int TempLow { get; set; }
        public int TempAvg { get; set; }
        public int FeelsLikeHi { get; set; }
        public int FeelsLikeLow { get; set; }
        public int FeelsLikeAvg { get; set; }        
        public float CloudCover { get; set; }   
        public float Precipitation { get; set; }
        public float PrecipitationCover { get; set; }
        public string PrecipitationType { get; set; } = "";
        public int WindSpeedMax { get; set; }   
        public int WindSpeedAvg { get; set; }
        public string FeelsLikeHiFormatted { get { return $"{this.FeelsLikeHi}°F"; } }


        [NotMapped]
        public DateOnly DateOnly => DateOnly.Parse(Date);
        private void ConvertStringFieldsToProperties()
        {
            // Assuming Date1 is a DateTime or similar object
            this.Date = Date1.ToString("MM/dd/yyyy");

            // Try parsing and rounding the temperature and wind speed values to integers
            if (float.TryParse(WeatherMaxTemp, out float tempHiFloat))
            {
                this.TempHi = (int)Math.Round(tempHiFloat);
            }
            if (float.TryParse(WeatherMinTemp, out float tempLowFloat))
            {
                this.TempLow = (int)Math.Round(tempLowFloat);
            }
            if (float.TryParse(WeatherAvgTemp, out float tempAvgFloat))
            {
                this.TempAvg = (int)Math.Round(tempAvgFloat);
            }
            if (float.TryParse(WeatherFeelsLikeMax, out float feelsLikeHiFloat))
            {
                this.FeelsLikeHi = (int)Math.Round(feelsLikeHiFloat);
            }
            if (float.TryParse(WeatherFeelsLikeMin, out float feelsLikeLowFloat))
            {
                this.FeelsLikeLow = (int)Math.Round(feelsLikeLowFloat);
            }
            if (float.TryParse(WeatherFeelsLikeAvg, out float feelsLikeAvgFloat))
            {
                this.FeelsLikeAvg = (int)Math.Round(feelsLikeAvgFloat);
            }
            if (float.TryParse(WeatherWindSpeedMax, out float windSpeedMaxFloat))
            {
                this.WindSpeedMax = (int)Math.Round(windSpeedMaxFloat);
            }
            if (float.TryParse(WeatherWindSpeedAvg, out float windSpeedAvgFloat))
            {
                this.WindSpeedAvg = (int)Math.Round(windSpeedAvgFloat);
            }

            // Try parsing the cloud cover and precipitation values as floats
            if (float.TryParse(WeatherCloudCover, out float cloudCover))
            {
                this.CloudCover = cloudCover;
            }
            if (float.TryParse(WeatherPrecip, out float precipitation))
            {
                this.Precipitation = precipitation;
            }
            if (float.TryParse(WeatherPrecipCover, out float precipitationCover))
            {
                this.PrecipitationCover = precipitationCover;
            }
            this.WeatherPrecipType = "";
            if(WeatherPrecipTypeArray  != null)
            {
                foreach (string s in WeatherPrecipTypeArray)
                {
                    WeatherPrecipType += s + ",";
                }
            }
           
            
            this.PrecipitationType = WeatherPrecipType ?? string.Empty; 
        }


        public DateTime Date1 { get; }
        public string WeatherMaxTemp { get; }
        public string WeatherMinTemp { get; }
        public string WeatherAvgTemp { get; }
        public string WeatherFeelsLikeMax { get; }
        public string WeatherFeelsLikeMin { get; }
        public string WeatherFeelsLikeAvg { get; }
        public string WeatherCloudCover { get; }
        public string WeatherPrecip { get; }
        public string WeatherPrecipCover { get; }
        public string[] WeatherPrecipTypeArray { get; }
        public string WeatherPrecipType { get; private set; }
        public string WeatherWindSpeedMax { get; }
        public string WeatherWindSpeedAvg { get; }
    }



}
