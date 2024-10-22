using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public record ReservationRecord
    {
        public int Id { get; set; } 
        public int Covers { get; set; } 
        public DateTime DateTime { get; set; }
        public DateTime TimeCreated { get; set; }
        public DateTime TimeUpdated { get; set; }
        public List<string> TableNumbers { get; set; } = new List<string>();
        public List<string> VisitTags { get; set; } = new List<string>();
        public float? CheckTotal { get; set; } = 0;
        public string Server { get; set; } = "";
        public string State { get; set; } = "";
        
        public string Request { get; set; } = string.Empty;
        public string Name { get; set; }
        public bool isCanceled {
            get {
                if(State == "Cancelled") {
                    return true;
                }
                return false;
            }
        }
       
        public ResoOrigin Origin { get; set; }
        public enum ResoOrigin {
            Web, 
            Phone,
            WalkIn
        }
        public ReservationRecord() { }
        public ReservationRecord(Reservation reservation)
        {
            Covers = reservation.PartySize;
            DateTime = reservation.ScheduledTime;
            TimeCreated = reservation.CreatedDate;
            TimeUpdated = reservation.UpdatedAt;
            if(reservation.State != null) {
                State = reservation.State;
            }
            if(reservation.TableNumber != null) {
                TableNumbers.AddRange(reservation.TableNumber);
            }
            if(reservation.VisitTags != null) {
                VisitTags.AddRange(reservation.VisitTags);
            }
            if(reservation.PosData != null) {
                if(reservation.PosData.PosSubTotal != null) {
                    CheckTotal = (float)(reservation.PosData.PosSubTotal * .1);
                }
            }
            if(reservation.Server != null) {
                Server = reservation.Server;
            }
            
            if(reservation.Origin == "Web") {
                this.Origin = ResoOrigin.Web;
            }
            else if(reservation.Origin == "Phone") {
                this.Origin = ResoOrigin.Phone;
            }
            else {
                this.Origin = ResoOrigin.WalkIn;
            }
            
            if(reservation.VenueNotes != null) {
                Request += "\n" + reservation.VenueNotes;
            }
            if(reservation.GuestRequest != null) {
                Request += "\n" + reservation.GuestRequest;
            }
            if(reservation.TableCategory  != null) {
                Request += "\n" + reservation.TableCategory;
            }
        }
        public TimeOnly timeOnly {
            get {
                return new TimeOnly(DateTime.Hour, DateTime.Minute, DateTime.Second);
            }
        }
        public string TimeDisplay {
            get {
                return timeOnly.ToString("h:mm");
            }
        }
        public override string ToString()
        {
            return this.DateTime.ToString("h:mm") + ":  " + this.Covers.ToString();
        }
    }
}
