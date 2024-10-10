using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FloorplanUserControlLibrary
{
    public partial class ShiftReservationControl : UserControl
    {
        private ShiftReservations _shiftReservations { get; set; }
        public ShiftReservationControl()
        {
            InitializeComponent();

        }
       

        private void SetForResos()
        {
            lblParties.Text = _shiftReservations.TotalResoCount.ToString();
            lblCovers.Text = _shiftReservations.TotalResoCovers.ToString();
            List<ReservationRecord> reservationsBySize = 
                _shiftReservations.ReservationRecords.OrderByDescending(r => r.Covers).ToList();
            reservationsBySize = reservationsBySize.Take(5).ToList();
            lbl1Largest.Text = $"{reservationsBySize[0].TimeDisplay} | {reservationsBySize[0].Covers}";
            lbl2Largest.Text = $"{reservationsBySize[1].TimeDisplay} | {reservationsBySize[1].Covers}";
            lbl3Largest.Text = $"{reservationsBySize[2].TimeDisplay} | {reservationsBySize[2].Covers}";
            lbl4Largest.Text = $"{reservationsBySize[3].TimeDisplay} | {reservationsBySize[3].Covers}";
            lbl5Largest.Text = $"{reservationsBySize[4].TimeDisplay} | {reservationsBySize[4].Covers}";
        }

        public async void SetForNewShift(DateOnly dateOnlySelected, bool isAM)
        {
            flowLayoutPanel1.Controls.Clear();
            this._shiftReservations = await ShiftReservations.CreateAsync(dateOnlySelected, isAM);
            SetForResos();
            Dictionary<TimeOnly, List<ReservationRecord>> reservationDistribution = _shiftReservations.GetTimeDistribution();
            foreach (TimeOnly time in reservationDistribution.Keys) {
                ReservationTimeBlockControl reservationTimeBlockControl =
                    new ReservationTimeBlockControl(time, reservationDistribution[time]);
                flowLayoutPanel1.Controls.Add(reservationTimeBlockControl);
            }
        }
    }
}
