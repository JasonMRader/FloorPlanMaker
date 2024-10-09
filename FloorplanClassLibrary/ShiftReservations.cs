using System;
using System.Collections.Generic;
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

        // Static asynchronous factory method
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
                    return 9;
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
                    return 22;
                }
            }
        }

        public List<ReservationRecord> ReservationRecords { get; set; } = new List<ReservationRecord>();
        public int TotalCovers {
            get {
                return ReservationRecords.Sum(r => r.Covers);
            }
        }
        public int TotalResoCount {
            get {
                return ReservationRecords.Count();
            }
        }
        public int TotalResoCovers {
            get {
                return ReservationRecords.Where(
                    r => r.Origin == ReservationRecord.ResoOrigin.Web
                    || r.Origin == ReservationRecord.ResoOrigin.Phone).ToList().Sum(r => r.Covers);
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

        var reservations = await ReservationDataAccess.GetReservationsAsync(scheduledTimeFrom, scheduledTimeTo);
        SetReservationRecords(reservations);
    }

    private void SetReservationRecords(List<Reservation> reservations)
    {
            reservations.RemoveAll(r => r.State == "Cancelled");
        List<ReservationRecord> reservationRecords = reservations
            .Select(reservation => new ReservationRecord(reservation))
            .OrderBy(r => r.DateTime)
            .ToList();

        this.ReservationRecords = reservationRecords;
    }
    }
}
