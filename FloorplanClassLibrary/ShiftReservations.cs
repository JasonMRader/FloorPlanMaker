using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorplanClassLibrary
{
    public class ShiftReservations
    {
        private ShiftReservations(DateOnly dateOnly, bool isAM)
        {
            this.DateOnly = dateOnly;
            this.IsAm = isAM;
        }
        public static ShiftReservations CreateShiftResosFromDB(DateOnly dateOnly, bool isAM)
        {
            var shiftReservations = new ShiftReservations(dateOnly, isAM);
            shiftReservations.LoadResoRecordsForShift();
            return shiftReservations;
        }

        private void LoadResoRecordsForShift()
        {             
            ReservationRecords = SqliteDataAccess.LoadReservations(ScheduledTimeStart, ScheduledTimeEnd);
            this.PreBookedRecords = ReservationRecords.Where(r => r.Origin != ReservationRecord.ResoOrigin.WalkIn).ToList();
        }

        public static async Task<ShiftReservations> CreateAsync(DateOnly dateOnly, bool isAM)
        {
            var shiftReservations = new ShiftReservations(dateOnly, isAM);
            await shiftReservations.InitializeReservationRecordsAsync();
            return shiftReservations;
        }



        public DateOnly DateOnly { get; set; }
        public bool IsAm { get; set; }
        public int StartHour {
            get {
                if (IsAm) {
                    return 10;
                }
                else {
                    return 16;
                }
            }
        }
        public int EndHour {
            get {
                if (IsAm) {
                    return 15;
                }
                else {
                    return 23;
                }
            }
        }
        public DateTime ScheduledTimeStart {
            get {
                return new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, StartHour, 0, 0);
            }
        }
        public DateTime ScheduledTimeEnd {
            get {
                return new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, EndHour, 59, 59);
            }
        }

        public List<ReservationRecord> ReservationRecords { get; set; } = new List<ReservationRecord>();
        public List<ReservationRecord> PreBookedRecords { get; set; } = new List<ReservationRecord> ();
        public int TotalCovers {
            get {
                return ReservationRecords.Sum(r => r.Covers);
            }
        }
        public int TotalResoCount {
            get {
                return ReservationRecords.Where(
                    r => r.Origin == ReservationRecord.ResoOrigin.Web
                    || r.Origin == ReservationRecord.ResoOrigin.Phone).ToList().Count();
            }
        }
        public int TotalResoCovers {
            get {
                var uncancelledResos = ReservationRecords.Where(
                    r => (r.Origin == ReservationRecord.ResoOrigin.Web && !r.isCanceled)
                    || (r.Origin == ReservationRecord.ResoOrigin.Phone && !r.isCanceled)
                    ).ToList();
                return uncancelledResos.Sum(r => r.Covers);
            }
        }
        public int TotalWalkInCovers {
            get {
                return ReservationRecords.Where(
                    r => r.Origin == ReservationRecord.ResoOrigin.WalkIn).ToList().Sum(r => r.Covers);
            }
        }
        private async Task InitializeReservationRecordsAsync()
        {
            DateTime scheduledTimeFrom = new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, StartHour, 0, 0);
            DateTime scheduledTimeTo = new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, EndHour, 59, 59);

            var reservations = await ReservationAPIDataAccess.GetReservationsAsync(scheduledTimeFrom, scheduledTimeTo);
            SetReservationRecords(reservations);
        }
        public int MinRange {
            get {
                int floor = TotalResoCovers - 25;
                if (floor < 0) {
                    floor = 0;
                }
                return floor;
            }
        }
        public int MaxRange {
            get {
                return TotalResoCovers + 25;
            }
        }
        private void SetReservationRecords(List<Reservation> reservations)
        {
            reservations.RemoveAll(r => r.State == "Cancelled");
            List<ReservationRecord> reservationRecords = reservations
                .Select(reservation => new ReservationRecord(reservation))
                .OrderBy(r => r.DateTime)
                .ToList();

            this.ReservationRecords = reservationRecords;
            this.PreBookedRecords = reservationRecords.Where(r => r.Origin != ReservationRecord.ResoOrigin.WalkIn).ToList();
        }
        public List<ReservationRecord> ResosOfPartSize(int min, int max)
        {
            return PreBookedRecords.Where(r => r.Covers >= min && r.Covers <= max).ToList();
        }
        public Dictionary<TimeOnly, List<ReservationRecord>> GetTimeDistribution()
        {
            Dictionary<TimeOnly, List<ReservationRecord>> timeDistribution = new Dictionary<TimeOnly, List<ReservationRecord>>();
            if(PreBookedRecords.Count == 0) {
                return timeDistribution;
            }
            //TimeOnly endTime = new TimeOnly(EndHour, 0,0);
            TimeOnly startTime = PreBookedRecords.Min(r => r.timeOnly);
            TimeOnly endTime = PreBookedRecords.Max(r => r.timeOnly);
            for(startTime = new TimeOnly(startTime.Hour, 0, 0); startTime <= endTime; startTime = startTime.AddMinutes(15)) {
                List<ReservationRecord> groupedRecords = PreBookedRecords.Where(
                    r => r.timeOnly >= startTime && r.timeOnly < startTime.AddMinutes(15)).ToList();
                timeDistribution.TryAdd(startTime, groupedRecords);

            }
            return timeDistribution;
        }
    }
    
}
