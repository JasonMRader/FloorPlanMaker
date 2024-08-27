using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary {
    public class ShiftWeather {
        private List<HourlyWeatherData> weatherData { get; set; } 
        public ShiftWeather(List<HourlyWeatherData> weatherData) {
            this.weatherData = weatherData;
        }
        public float RainAmount {
            get {
                return weatherData.Sum(w => w.PrecipitationAmount);
            }
        }
        public float SnowAmount {
            get {
                return weatherData.Sum(w => w.SnowAmount_CM);
            }
        }

        public float CloudCoverAverage {
            get {
                return weatherData.Sum(w => w.CloudCover) / weatherData.Count;
            }
        }
        public int WindMax {
            get {
                return weatherData.Max(w => w.WindSpeedMax);
            }
        }
        public int WindAvg {
            get {
                return weatherData.Sum((w) => w.WindSpeedAvg) / weatherData.Count;
            }
        }
        public int TempHi {
            get {
                return weatherData.Max((w) => w.TempHi);
            }
        }
        public int FeelsLikeHi {
            get {
                return weatherData.Max(w => w.FeelsLikeHi);
            }
        }
        public int TempLow {
            get {
                return weatherData.Min(w => w.TempLow);
            }
        }
        public int FeelsLikeLow {
            get {
                return weatherData.Min(x => x.FeelsLikeLow);
            }
        }
        public int FeelsLikeAvg {
            get {
                return weatherData.Sum(w => w.FeelsLikeAvg) / weatherData.Count;
            }
        }
        public int TempAvg {
            get {
                return weatherData.Sum(w => w.TempAvg) / weatherData.Count;
            }
        }
    }
}
