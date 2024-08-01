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
    public partial class frmWeatherData : Form
    {
        public frmWeatherData()
        {
            InitializeComponent();
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            refreshMissingDateDisplay();
        }
        
        private void refreshMissingDateDisplay()
        {
            lbMissingDates.Items.Clear();
            DateOnly maxEndDate = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
            DateOnly endDate = DateOnly.FromDateTime(dtpEnd.Value);
            DateOnly startDate = DateOnly.FromDateTime(dtpStart.Value);
            if (endDate > maxEndDate)
            {
                endDate = maxEndDate;
            }
            List<DateOnly> missingDates = SqliteDataAccess.GetMissingWeatherDates(startDate, endDate);
            List<string> missingDateRanges = new List<string>();

            DateOnly? rangeStart = null;
            for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (missingDates.Contains(date))
                {
                    // Start of a new range
                    if (rangeStart == null)
                    {
                        rangeStart = date;
                    }
                }
                else if (rangeStart != null)
                {
                    if (date.AddDays(-1) != rangeStart)
                    {
                        // End of a current range
                        string dateRange = $"{rangeStart.Value.ToString("MMM dd")} - {date.AddDays(-1).ToString("MMM dd")}";
                        missingDateRanges.Add(dateRange);
                        rangeStart = null; // Reset for the next range
                    }
                    else
                    {
                        string dateRange = $"{rangeStart.Value.ToString("MMM dd")}";
                        missingDateRanges.Add(dateRange);
                        rangeStart = null; // Reset for the next range
                    }

                }
            }
            if (rangeStart != null)
            {

                if (rangeStart == endDate)
                {
                    string dateRange = $"{rangeStart.Value.ToString("MMM dd")}";
                    missingDateRanges.Add(dateRange);
                }
                else
                {
                    string dateRange = $"{rangeStart.Value.ToString("MMM dd")} - {endDate.ToString("MMM dd")}";
                    missingDateRanges.Add(dateRange);
                }


            }

            foreach (string dateRange in missingDateRanges)
            {
                lbMissingDates.Items.Add(dateRange);
            }
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            refreshMissingDateDisplay();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
             WeatherDataHistoryUpdater.SaveMissingDatesToDatabase(dtpStart.Value, dtpEnd.Value);
            
        }
    }
}
