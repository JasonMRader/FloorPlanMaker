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
        public void SetShiftReservations(ShiftReservations shiftReservations)
        {
            this._shiftReservations = shiftReservations;
            SetForResos();
        }

        private void SetForResos()
        {
            lblParties.Text = _shiftReservations.TotalResoCount.ToString();
            lblCovers.Text = _shiftReservations.TotalResoCovers.ToString();
        }

        public async void SetForNewShift(DateOnly dateOnlySelected, bool isAM)
        {
            flowLayoutPanel1.Controls.Clear();
            this._shiftReservations = await ShiftReservations.CreateAsync(dateOnlySelected, isAM);
            SetForResos();
            Dictionary<TimeOnly, List<ReservationRecord>> reservationDistribution = _shiftReservations.GetTimeDistribution();
            foreach(TimeOnly time in reservationDistribution.Keys) {                
                ReservationTimeBlockControl reservationTimeBlockControl = 
                    new ReservationTimeBlockControl(time, reservationDistribution[time]);
                flowLayoutPanel1.Controls.Add(reservationTimeBlockControl);
            }
        }
    }
}
