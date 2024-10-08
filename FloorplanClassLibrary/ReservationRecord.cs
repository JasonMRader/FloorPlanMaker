using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public record ReservationRecord
    {
        public int Covers { get; set; } 
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public ReservationRecord(Reservation reservation)
        {
            Covers = reservation.PartySize;
            DateTime = reservation.ScheduledTime;
        }
        public override string ToString()
        {
            return this.DateTime.ToString("h:mm") + ":  " + this.Covers.ToString();
        }
    }
}
