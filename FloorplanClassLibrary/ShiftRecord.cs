﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public record ShiftRecord
    {
        public List<FloorplanRecord> FloorplanRecords { get; set; } = new List<FloorplanRecord>();
        public DateOnly dateOnly { get; set; }
        public bool IsAm { get; set; }
        public List<HourlyWeatherData> HourlyWeatherData { get; set; } = new List<HourlyWeatherData>();
        public int Reservations { get; set; } 
        public SpecialEventDate? SpecialEventDate { get; set; }

    }
}