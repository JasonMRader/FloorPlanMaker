using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SpecialEventDate
    {
        public int ID { get; set; }
        public DateTime Date { get; private set; }
        public DateOnly DateOnly { get; private set; }
        public string Name { get; set; }
        public SpecialEventDate() { }
        public OutlierType Type { get; set; }
        public bool ShouldIgnoreSales { get; set; }

        public enum OutlierType
        {
            SpecialEvent,
            Holiday,
            Closed,
            Convention,
            Other
        }

        public SpecialEventDate(DateTime date, OutlierType type, string name)
        {
            SetDate(date);
            Type = type;
            Name = name;
        }
        public SpecialEventDate(DateOnly dateOnly, OutlierType type, string name)
        {
            SetDate(dateOnly);
            Type = type;
            Name = name;
        }
        public SpecialEventDate(int id, DateOnly dateOnly, OutlierType type, bool shouldIgnoreSales, string name)
        {
            ID = id;
            DateOnly = dateOnly;
            Type = type;
            ShouldIgnoreSales = shouldIgnoreSales;
            Name = name;
        }
        public void SetDate(DateTime date)
        {
            this.Date = date.Date;
            this.DateOnly = new DateOnly(date.Year, date.Month, date.Day);
        }
        public void SetDate(DateOnly dateOnly)
        {
            this.DateOnly = dateOnly;
            this.Date = dateOnly.ToDateTime(TimeOnly.MinValue);
        }
       
        public override string ToString()
        {
            return Name + " " + DateOnly.ToString();    
        }
        public string GetUpcomingEventString()
        {
            DateTime eventDate = this.DateOnly.ToDateTime(TimeOnly.MinValue);
            int daysAway = (eventDate - DateTime.Now.Date).Days;
            return $"{this.Name} ({daysAway} days)";
        }


    }

}
