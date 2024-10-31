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
            if (lblParties.InvokeRequired) {
                lblParties.Invoke(new Action(SetForResos));

            }
            else {
                if (_shiftReservations == null) {
                    return;
                }
                if (_shiftReservations.ReservationRecords.Count > 0) {
                    lblParties.Text = _shiftReservations.TotalResoCount.ToString();
                    lblCovers.Text = _shiftReservations.TotalResoCovers.ToString();
                    List<ReservationRecord> reservationsBySize =
                        _shiftReservations.PreBookedRecords.OrderByDescending(r => r.Covers).ToList();
                    reservationsBySize = reservationsBySize.Take(5).ToList();
                    if(reservationsBySize.Count > 0) {
                        for(int i = 0; i < reservationsBySize.Count; i++) {
                            lbl1Largest.Text = $"{reservationsBySize[i].TimeDisplay} | {reservationsBySize[i].Covers}";
                            lbl2Largest.Text = $"{reservationsBySize[i].TimeDisplay} | {reservationsBySize[i].Covers}";
                            lbl3Largest.Text = $"{reservationsBySize[i].TimeDisplay} | {reservationsBySize[i].Covers}";
                            lbl4Largest.Text = $"{reservationsBySize[i].TimeDisplay} | {reservationsBySize[i].Covers}";
                            lbl5Largest.Text = $"{reservationsBySize[i].TimeDisplay} | {reservationsBySize[i].Covers}";
                        }
                    }
                   
                    lbl1to4Count.Text = _shiftReservations.PreBookedRecords.Where(r => r.Covers <= 4).ToList().Count().ToString();
                    lbl5to8Count.Text = _shiftReservations.PreBookedRecords.Where(
                        r => r.Covers <= 8 && r.Covers >= 5).ToList().Count().ToString();
                    lbl9to14Count.Text = _shiftReservations.PreBookedRecords.Where(
                        r => r.Covers <= 14 && r.Covers >= 9).ToList().Count().ToString();
                    lbl15to19Count.Text = _shiftReservations.PreBookedRecords.Where(
                        r => r.Covers <= 19 && r.Covers >= 15).ToList().Count().ToString();
                    lbl20PlusCount.Text = _shiftReservations.PreBookedRecords.Where(
                        r => r.Covers >= 20).ToList().Count().ToString();

                }




            }

        }

        public async void SetForNewShift(DateOnly dateOnlySelected, bool isAM)
        {
            flowLayoutPanel1.Controls.Clear();
            this._shiftReservations = ShiftReservationDataControler.GetReservations(dateOnlySelected, isAM);
            SetForResos();
            if (this._shiftReservations == null) {
                return;
            }
            Dictionary<TimeOnly, List<ReservationRecord>> reservationDistribution = _shiftReservations.GetTimeDistribution();
            foreach (TimeOnly time in reservationDistribution.Keys) {
                ReservationTimeBlockControl reservationTimeBlockControl =
                    new ReservationTimeBlockControl(time, reservationDistribution[time]);
                flowLayoutPanel1.Controls.Add(reservationTimeBlockControl);
            }
        }

        private void ShiftReservationControl_Load(object sender, EventArgs e)
        {

        }
    }
}
