using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SpecialEventDate
    {
        public DateTime Date { get; private set; }
        public DateOnly DateOnly { get; private set; }
        public string Name { get; set; }

        public OutlierType Type { get; set; }

        public enum OutlierType
        {
            BusyDay,
            SlowDay,
            Holiday,
            Closed,
            Convention,
            Other
        }

        public SpecialEventDate(DateTime date, OutlierType type)
        {
            SetDate(date);
            Type = type;
        }
        public SpecialEventDate(DateOnly dateOnly, OutlierType type)
        {
            SetDate(dateOnly);
            Type = type;
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
        public bool ShouldIgnoreSales()
        {
            return Type == OutlierType.Closed;
        }

       
    }

}
