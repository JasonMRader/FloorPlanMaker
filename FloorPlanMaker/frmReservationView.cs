using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorPlanMakerUI
{
    public partial class frmReservationView : Form
    {
        int startHour = 0;
        int endHour = 0;
        public frmReservationView()
        {
            InitializeComponent();
        }


        private async void btnGetReservations_Click(object sender, EventArgs e)
        {
            try {

                DateOnly dtpDay = DateOnly.FromDateTime(dateTimePicker1.Value);
                ShiftReservations shiftReservations = await ShiftReservations.CreateAsync(dtpDay, rdoAM.Checked);
                lblCoverCount.Text = shiftReservations.TotalCovers.ToString();
                lblResoCovers.Text = shiftReservations.TotalResoCovers.ToString();
                lblReservationCount.Text = shiftReservations.TotalResoCount.ToString();
                populateLB(shiftReservations.ReservationRecords);
                SetTimeLabels(shiftReservations.ReservationRecords);
                SetPartySizeLabels(shiftReservations);

            }
            catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void SetPartySizeLabels(ShiftReservations shiftReservations)
        {
            lbl1to2.Text = shiftReservations.ResosOfPartSize(1, 2).Count.ToString();
            lbl3to4.Text = shiftReservations.ResosOfPartSize(3, 4).Count.ToString();
            lbl5to6.Text = shiftReservations.ResosOfPartSize(5, 6).Count.ToString();
            lbl7to8.Text = shiftReservations.ResosOfPartSize(7, 8).Count.ToString();
            lbl9to12.Text = shiftReservations.ResosOfPartSize(9, 12).Count.ToString();
            lbl13to16.Text = shiftReservations.ResosOfPartSize(13, 16).Count.ToString();
            lbl17to20.Text = shiftReservations.ResosOfPartSize(17, 20).Count.ToString();
            lbl20Plus.Text = shiftReservations.ResosOfPartSize(20, 200000).Count.ToString();
        }

        private void populateLB(List<ReservationRecord> reservations)
        {
            listBox1.Items.Clear();
            foreach (ReservationRecord reservation in reservations) {
                listBox1.Items.Add(reservation);
            }
        }
        private void SetTimeLabels(List<ReservationRecord> reservationRecords)
        {
            if (rdoPM.Checked) {
                startHour = 16;
                endHour = 23;
            }
            else {
                startHour = 9;
                endHour = 15;
            }
            List<int> covers = new List<int>();
            for (int i = startHour; i <= endHour; i++) {
                List<ReservationRecord> groupedResos = reservationRecords.Where(r => r.DateTime.Hour == i).ToList();
                int coversThisHour = groupedResos.Sum(r => r.Covers);
                if (i == 16) {
                    lbl4pm.Text = coversThisHour.ToString();
                }
                if (i == 17) {
                    lbl5pm.Text = coversThisHour.ToString();
                }
                if (i == 18) {
                    lbl6pm.Text = coversThisHour.ToString();
                }
                if (i == 19) {
                    lbl7pm.Text = coversThisHour.ToString();
                }
                if (i == 20) {
                    lbl8pm.Text = coversThisHour.ToString();
                }
                if (i == 21) {
                    lbl9pm.Text = coversThisHour.ToString();
                }
                if (i == 22) {
                    lbl10pm.Text = coversThisHour.ToString();
                }
            }
        }
        private List<ReservationRecord> recordsToSave = new List<ReservationRecord>();
        private async void btnGetTimeSpanResos_Click(object sender, EventArgs e)
        {
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            DateTime scheduledTimeFrom = new DateTime(start.Year, start.Month, start.Day, 1, 0, 0);
            DateTime scheduledTimeTo = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);

            var reservations = await LoadReservationsAsync(scheduledTimeFrom, scheduledTimeTo);

            recordsToSave.Clear();
            recordsToSave = reservations
               .Select(reservation => new ReservationRecord(reservation))
               .OrderBy(r => r.DateTime)
               .ToList();
            lblTimeSpanResosCount.Text = recordsToSave.Count.ToString();
        }
        public async Task<List<Reservation>> LoadReservationsAsync(DateTime startDateTime, DateTime endDateTime)
        {
            var allReservations = new List<Reservation>();

            // Ensure the start time is at the beginning of the day and end time is at the end of the day
            DateTime currentStart = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, 0, 0, 0);
            DateTime endOfPeriod = new DateTime(endDateTime.Year, endDateTime.Month, endDateTime.Day, 23, 59, 59);

            while (currentStart <= endOfPeriod) {
                DateTime currentEnd = currentStart.AddDays(1).AddSeconds(-1); // End of the current day
                if (currentEnd > endOfPeriod) {
                    currentEnd = endOfPeriod;
                }

                // Load reservations for the current day (or interval)
                var dailyReservations = await LoadReservationsForIntervalAsync(currentStart, currentEnd);
                allReservations.AddRange(dailyReservations);

                // Move to the next day
                currentStart = currentStart.AddDays(1);
            }

            return allReservations;
        }

        private async Task<List<Reservation>> LoadReservationsForIntervalAsync(DateTime intervalStart, DateTime intervalEnd)
        {
            var reservations = new List<Reservation>();
            bool needsSplitting = true;
            var intervalsToProcess = new Queue<Tuple<DateTime, DateTime>>();
            intervalsToProcess.Enqueue(Tuple.Create(intervalStart, intervalEnd));

            while (intervalsToProcess.Count > 0) {
                var currentInterval = intervalsToProcess.Dequeue();
                DateTime start = currentInterval.Item1;
                DateTime end = currentInterval.Item2;

                // Load reservations for the current interval
                var intervalReservations = await ReservationDataAccess.GetReservationsAsync(start, end);

                if (intervalReservations.Count >= 1000) {
                    // Split the interval further
                    TimeSpan intervalDuration = end - start;
                    if (intervalDuration.TotalMinutes <= 1) {
                        // Cannot split further; add what we have
                        reservations.AddRange(intervalReservations);
                    }
                    else {
                        // Split the interval into two halves
                        DateTime midPoint = start.AddSeconds(intervalDuration.TotalSeconds / 2);

                        intervalsToProcess.Enqueue(Tuple.Create(start, midPoint));
                        intervalsToProcess.Enqueue(Tuple.Create(midPoint.AddSeconds(1), end));
                    }
                }
                else {
                    // Add the loaded reservations to the list
                    reservations.AddRange(intervalReservations);
                }
            }

            return reservations;
        }

        private void btnSaveResos_Click(object sender, EventArgs e)
        {
            SqliteDataAccess.SaveReservations(recordsToSave);
            MessageBox.Show("Records Saved!");
            lblTimeSpanResosCount.Text = "0";
        }

        private void btnLoadRange_Click(object sender, EventArgs e)
        {
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            DateTime scheduledTimeFrom = new DateTime(start.Year, start.Month, start.Day, 1, 0, 0);
            DateTime scheduledTimeTo = new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
           
            List<ReservationRecord> records = SqliteDataAccess.LoadReservations(scheduledTimeFrom, scheduledTimeTo);
        }

        private void btnLoadFirstDatePM_Click(object sender, EventArgs e)
        {
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            DateTime firstDatePMStart = new DateTime(start.Year, start.Month, start.Day, 16, 0, 0);
            DateTime firstDatePMEnd = new DateTime(start.Year, start.Month, start.Day, 23, 0, 0);
            List<ReservationRecord> records = SqliteDataAccess.LoadReservations(firstDatePMStart, firstDatePMEnd);
        }
    }
}
