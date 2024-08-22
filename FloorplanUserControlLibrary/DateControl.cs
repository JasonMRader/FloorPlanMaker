using FloorplanClassLibrary;
using FloorPlanMakerUI;
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
    public partial class DateControl : UserControl
    {

        public DateControl()
        {
            InitializeComponent();

        }
        private DateOnly dateOnly { get; set; }
        private DayOfWeek dayOfWeek { get; set; }
        public void SetDayOfWeek(DayOfWeek dayOfWeek)
        {
            this.dayOfWeek = dayOfWeek;
        }
        public void SetDateOnly(DateOnly dateOnly)
        {
            this.dateOnly = dateOnly;
            lblDate.Text = dateOnly.Day.ToString();
            if (dateOnly == DateOnly.FromDateTime(DateTime.Now))
            {
                lblDate.BackColor = Color.LightBlue;
                lblDate.ForeColor = Color.Black;
            }
            else if(dateOnly > DateOnly.FromDateTime(DateTime.Now))
            {
                lblDate.BackColor = Color.LightGray;
                lblDate.ForeColor = Color.White;
            }
            else
            {
                lblDate.BackColor = UITheme.ButtonColor;
                lblDate.ForeColor = Color.Black;
            }
        }
        public void ShowFloorplansForAmAndPM()
        {
            flowInfo.Controls.Clear();
            int amCount = SqliteDataAccess.GetFloorplanCountsForShift(dateOnly, true);
            int pmCount = SqliteDataAccess.GetFloorplanCountsForShift(dateOnly, false);
            Label lblAM = new Label()
            {
                AutoSize = false,
                Text = "AM: " + amCount,
                Height = this.flowInfo.Height / 2,
                Width = this.flowInfo.Width,
                Margin = new Padding(0),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = UITheme.MainFont

            };
            if (amCount > 0)
            {
                lblAM.BackColor = UITheme.YesColor;
            }
            else
            {
                lblAM.BackColor = UITheme.NoColor;
            }
            
            Label lblPM = new Label()
            {
                AutoSize = false,
                Text = "PM: " + pmCount,
                Height = this.flowInfo.Height / 2,
                Width = this.flowInfo.Width,
                Margin = new Padding(0),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = UITheme.MainFont

            };
            if (pmCount > 0)
            {
                lblPM.BackColor = UITheme.YesColor;
            }
            else
            {
                lblPM.BackColor = UITheme.NoColor;
            }
            flowInfo.Controls.Add(lblAM);
            flowInfo.Controls.Add(lblPM);

        }
    }
}
