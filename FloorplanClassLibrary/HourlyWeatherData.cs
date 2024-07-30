using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class HourlyWeatherData
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public int TempHi { get; set; }
        public int TempLow { get; set; }
        public int TempAvg { get; set; }
        public int FeelsLikeHi { get; set; }
        public int FeelsLikeLow { get; set; }
        public int FeelsLikeAvg { get; set; }
        public float CloudCover { get; set; }
        public float PrecipitationAmount { get; set; }
        public float PrecipitationChance { get; set; }
        public int PrecipitationChanceFormatted
        {
            get
            {
                return (int)PrecipitationChance;
            }
        }
        public string PrecipitationType { get; set; } = "";
        public int WindSpeedMax { get; set; }
        public int WindSpeedAvg { get; set; }
    }
}
