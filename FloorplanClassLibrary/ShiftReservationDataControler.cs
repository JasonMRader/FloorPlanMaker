using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class ShiftReservationDataControler
    {
        private static ShiftReservations TodayAMResos {get; set;}
        private static ShiftReservations TodayPMResos { get; set; }
        private static ShiftReservations TomorrowAMResos { get; set; }
        private static ShiftReservations TomorrowPMResos { get; set; }
        public static async Task InitializeAsync()
        {
            DateOnly today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);
            TodayAMResos = await ShiftReservations.CreateAsync(today, true);
            TodayPMResos = await ShiftReservations.CreateAsync(today, false);

        }
        public static ShiftReservations GetReservations(DateOnly dateOnly, bool isAM)
        {
            DateOnly today = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day);

            if (dateOnly == today && isAM) {
                return TodayAMResos;
            }
            else if (dateOnly == today && !isAM) {
                return TodayPMResos;
            }
            else {
                return ShiftReservations.CreateShiftResosFromDB(dateOnly, isAM);
            }

        }
        
        //private static async Task InitializeReservationRecordsTodayAM()
        //{
        //    DateTime today = DateTime.Now;
        //    DateOnly DateOnly = new DateOnly(today.Year, today.Month, today.Day);
        //    DateTime scheduledTimeFrom = new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, 9, 0, 0);
        //    DateTime scheduledTimeTo = new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, 15, 59, 59);

        //    var reservations = await ReservationDataAccess.GetReservationsAsync(scheduledTimeFrom, scheduledTimeTo);
        //    SetReservationRecords(reservations, true);
        //}
        //private static async Task InitializeReservationRecordsTodayPM()
        //{
        //    DateTime today = DateTime.Now;
        //    DateOnly DateOnly = new DateOnly(today.Year, today.Month, today.Day);
        //    DateTime scheduledTimeFrom = new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, 16, 0, 0);
        //    DateTime scheduledTimeTo = new DateTime(DateOnly.Year, DateOnly.Month, DateOnly.Day, 23, 59, 59);

        //    var reservations = await ReservationDataAccess.GetReservationsAsync(scheduledTimeFrom, scheduledTimeTo);
        //    SetReservationRecords(reservations, false);
        //}

        //private static void SetReservationRecords(List<Reservation> reservations, bool isAM)
        //{
        //    reservations.RemoveAll(r => r.State == "Cancelled");
        //    List<ReservationRecord> reservationRecords = reservations
        //        .Select(reservation => new ReservationRecord(reservation))
        //        .OrderBy(r => r.DateTime)
        //        .ToList();

        //    if (isAM) {
        //        //TodayAMResos = reservationRecords;
        //    }
        //    //this.PreBookedRecords = reservationRecords.Where(r => r.Origin != ReservationRecord.ResoOrigin.WalkIn).ToList();
        //}
    }
}
