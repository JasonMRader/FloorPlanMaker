using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary {
    public class ShiftWeather
    {
        private List<HourlyWeatherData> weatherData { get; set; }
        public List<HourlyWeatherData> HourlyWeather {
            get {
                return weatherData;
            }
        }

        public ShiftWeather(List<HourlyWeatherData> weatherData)
        {
            this.weatherData = weatherData ?? new List<HourlyWeatherData>();
        }

        public float RainAmount {
            get {
                return weatherData.Count > 0 ? weatherData.Sum(w => w.PrecipitationAmount) : 0f;
            }
        }

        public float SnowAmount {
            get {
                return weatherData.Count > 0 ? weatherData.Sum(w => w.SnowAmount_CM) : 0f;
            }
        }

        public float CloudCoverAverage {
            get {
                return weatherData.Count > 0 ? weatherData.Sum(w => w.CloudCover) / weatherData.Count : 0f;
            }
        }

        public int WindMax {
            get {
                return weatherData.Count > 0 ? weatherData.Max(w => w.WindSpeedMax) : 0;
            }
        }

        public int WindAvg {
            get {
                return weatherData.Count > 0 ? weatherData.Sum(w => w.WindSpeedAvg) / weatherData.Count : 0;
            }
        }

        public int TempHi {
            get {
                return weatherData.Count > 0 ? weatherData.Max(w => w.TempHi) : 0;
            }
        }

        public int FeelsLikeHi {
            get {
                return weatherData.Count > 0 ? weatherData.Max(w => w.FeelsLikeHi) : 0;
            }
        }

        public int TempLow {
            get {
                return weatherData.Count > 0 ? weatherData.Min(w => w.TempLow) : 0;
            }
        }

        public int FeelsLikeLow {
            get {
                return weatherData.Count > 0 ? weatherData.Min(w => w.FeelsLikeLow) : 0;
            }
        }

        public int FeelsLikeAvg {
            get {
                return weatherData.Count > 0 ? weatherData.Sum(w => w.FeelsLikeAvg) / weatherData.Count : 0;
            }
        }

        public int TempAvg {
            get {
                return weatherData.Count > 0 ? weatherData.Sum(w => w.TempAvg) / weatherData.Count : 0;
            }
        }
    }

}
