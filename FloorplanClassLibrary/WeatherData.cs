using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class WeatherData
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public int TempHi { get; set; }
        public int TempLow { get; set; }
        public int TempAvg { get; set; }
        public int FeelsLikeHi { get; set; }
        public int FeelsLikeLow { get; set; }
        public int FeelsLikeAvg { get; set; }

        [NotMapped]
        public DateOnly DateOnly => DateOnly.Parse(Date);
    }



}
