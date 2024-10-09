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

namespace FloorplanUserControlLibrary
{
    public partial class ReservationTimeBlockControl : UserControl
    {
        ToolTip toolTip = new ToolTip();
        public ReservationTimeBlockControl(TimeOnly timeOnly, List<ReservationRecord> reservationRecords)
        {
            InitializeComponent();
            lblTime.Text = timeOnly.ToString("h:mm");
            AddResoPanels(reservationRecords);
        }

        private void AddResoPanels(List<ReservationRecord> reservationRecords)
        {
            foreach (ReservationRecord rec in reservationRecords) {
                Panel panel = new Panel() {
                    Height = this.Height,
                    Width = (int)(rec.Covers * 2),
                    BackColor = GetBackColor(rec.Covers),
                    Margin = new Padding(1, 0, 0, 0),

                };
                toolTip.SetToolTip(panel, "Covers: " + rec.Covers.ToString() + rec.request);
                flowPanel.Controls.Add(panel);
            }

            lblCoverCount.Text = reservationRecords.Sum(r => r.Covers).ToString();

        }
        private Color GetBackColor(int covers)
        {
            if (covers < 3) {
                return Color.LightGreen;
            }
            if (covers < 5) {
                return Color.DarkGreen;
            }
            if (covers < 8) {
                return Color.DarkBlue;
            }
            if (covers < 12) {
                return Color.Purple;
            }
            if (covers < 18) {
                return Color.Orange;
            }
            return Color.Red;
        }
        private void ReservationTimeBlockControl_Load(object sender, EventArgs e)
        {

        }
    }
}
