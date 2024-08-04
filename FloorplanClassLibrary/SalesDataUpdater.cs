using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SalesDataUpdater
    {
        public SalesDataUpdater() 
        {
            //refreshMissingDates();
        }
        public List<DateOnly> DatesMissing = new List<DateOnly>();
        public List<DateOnly> DatesMissingLastWeek
        {
            get
            {
                var startDate = _dateReferenced.AddDays(-7);
                var endDate = _dateReferenced.AddDays(-1);
                return DatesMissing.Where(d => d >= startDate && d <= endDate).ToList();
            }
        }
        public List<DateOnly> DatesMissingBeforeLastWeek
        {
            get
            {
                var startDate = _dateReferenced.AddDays(-7);
                return DatesMissing.Where(d => d <= startDate).ToList();
            }
        }
        public string DatesMissingDisplay
        {
            get
            {
                if(AllMissingRanges.Count == 0)
                {
                    return "";
                }
                if(AllMissingRanges.Count == 1)
                {
                    return $"({AllMissingRanges[0]})";
                }
                else
                {
                    return $"{AllMissingRanges.Count} Ranges Missing";
                }
            }
        }
        private DateOnly _dateReferenced = DateOnly.FromDateTime(DateTime.Today);
        public List<string> AllMissingRanges { get; private set; } = new List<string>();
        public void SetNewDate(DateOnly newDateOnly)
        {
            _dateReferenced = newDateOnly;
            refreshMissingDates();
        }
        public void refreshMissingDates()
        {
           
           
            DateOnly endDate = _dateReferenced.AddDays(-1);

            DateOnly startDate = DateOnly.FromDateTime(DateTime.Today.AddDays(-150));

            List<DateOnly> missingDates = SqliteDataAccess.GetMissingDates(startDate, endDate);
            DatesMissing = missingDates;
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

            // Handle case where the last date is part of a missing range
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
            AllMissingRanges.Clear();
            foreach (string dateRange in missingDateRanges)
            {
                AllMissingRanges.Add(dateRange);
            }
        }
    }
}
