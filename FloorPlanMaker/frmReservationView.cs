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
                if(rdoPM.Checked) {
                    startHour = 16;
                    endHour = 23;
                }
                else {
                    startHour = 9;
                    endHour = 15;
                }
                DateTime dtpDay = dateTimePicker1.Value;
                DateTime scheduledTimeFrom = new DateTime(dtpDay.Year, dtpDay.Month, dtpDay.Day, startHour, 0, 0);


                DateTime scheduledTimeTo = new DateTime(dtpDay.Year, dtpDay.Month, dtpDay.Day, endHour, 59, 59);

                var reservations = await ReservationDataAccess.GetReservationsAsync(scheduledTimeFrom, scheduledTimeTo);
                List<ReservationRecord> reservationsRecords = GetReservationRecords(reservations);
                int Covers = reservationsRecords.Sum(r => r.Covers);
                // Bind reservations to a data grid or process as needed
                lblCoverCount.Text = Covers.ToString();
                lblReservationCount.Text = reservationsRecords.Count.ToString();
                //MessageBox.Show($"{reservationsRecords.Count} resos, {Covers} Covers");
            }
            catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
        private List<ReservationRecord> GetReservationRecords(List<Reservation> reservations)
        {
            List<ReservationRecord> reservationRecords = new List<ReservationRecord>();
            foreach (Reservation reservation in reservations) {
                reservationRecords.Add(new ReservationRecord(reservation));
            }
            return reservationRecords;
        }
    }
}
