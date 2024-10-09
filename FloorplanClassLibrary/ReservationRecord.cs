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
        public TimeOnly timeOnly {
            get {
                return new TimeOnly(DateTime.Hour, DateTime.Minute, DateTime.Second);
            }
        }
        public string Name { get; set; }
        public ResoOrigin Origin { get; set; }
        public enum ResoOrigin {
            Web, 
            Phone,
            WalkIn
        }
        public ReservationRecord(Reservation reservation)
        {
            Covers = reservation.PartySize;
            DateTime = reservation.ScheduledTime;
            if(reservation.Origin == "Web") {
                this.Origin = ResoOrigin.Web;
            }
            else if(reservation.Origin == "Phone") {
                this.Origin = ResoOrigin.Phone;
            }
            else {
                this.Origin = ResoOrigin.WalkIn;
            }
        }
        public override string ToString()
        {
            return this.DateTime.ToString("h:mm") + ":  " + this.Covers.ToString();
        }
    }
}
