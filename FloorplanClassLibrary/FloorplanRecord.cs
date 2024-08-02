using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public record FloorplanRecord
    {
        public int DiningAreaID { get; set; }
        public DateOnly DateOnly { get; set; }
        public List<WeatherData> WeatherData { get; set; } = new List<WeatherData>();
        public float Sales { get; set; }
        public bool IsAm { get; set; }
        bool IsAverage { get; set; }
        public int ServerCount { get; set; }      
        
        
    }
}
