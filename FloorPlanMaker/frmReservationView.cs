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

            }
            catch (Exception ex) {
                MessageBox.Show($"Error: {ex.Message}");
            }
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
