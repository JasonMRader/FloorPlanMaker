using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class CsvScheduleReader
    {

        public static List<ScheduledShift> GetScheduledShifts(string csvFilePath)
        {
            var scheduledShifts = new List<ScheduledShift>();

            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null
            }))
            {
                // Skip lines until you reach the headers of the data
                for (int i = 0; i < 13; i++)
                {
                    csv.Read();
                }

                csv.Context.RegisterClassMap<CsvServerRecordMap>();
                var records = csv.GetRecords<CsvServerRecord>().ToList();

                foreach (var record in records)
                {
                    AddScheduledShift(scheduledShifts, record.Mon, record.EmployeeName, DayOfWeek.Monday);
                    AddScheduledShift(scheduledShifts, record.Tue, record.EmployeeName, DayOfWeek.Tuesday);
                    AddScheduledShift(scheduledShifts, record.Wed, record.EmployeeName, DayOfWeek.Wednesday);
                    AddScheduledShift(scheduledShifts, record.Thu, record.EmployeeName, DayOfWeek.Thursday);
                    AddScheduledShift(scheduledShifts, record.Fri, record.EmployeeName, DayOfWeek.Friday);
                    AddScheduledShift(scheduledShifts, record.Sat, record.EmployeeName, DayOfWeek.Saturday);
                    AddScheduledShift(scheduledShifts, record.Sun, record.EmployeeName, DayOfWeek.Sunday);
                }
            }

            return scheduledShifts;
        }

        private static void AddScheduledShift(List<ScheduledShift> scheduledShifts, string timeSlot, string employeeName, DayOfWeek dayOfWeek)
        {
            if (string.IsNullOrEmpty(timeSlot)) return;

            var date = GetDateOfCurrentWeek(dayOfWeek);
            bool isAm = timeSlot.Contains("AM");

            var scheduledShift = scheduledShifts.FirstOrDefault(s => s.Date == date && s.IsAm == isAm);
            if (scheduledShift == null)
            {
                scheduledShift = new ScheduledShift
                {
                    Date = date,
                    IsAm = isAm
                };
                scheduledShifts.Add(scheduledShift);
            }

            scheduledShift.Servers.Add(employeeName);
        }

        private static DateOnly GetDateOfCurrentWeek(DayOfWeek dayOfWeek)
        {
            var today = DateOnly.FromDateTime(DateTime.Today);
            int diff = (7 + (dayOfWeek - today.DayOfWeek)) % 7;
            return today.AddDays(diff);
        }

        public static string InspectCsvFile(string csvFilePath)
        {
            var stringBuilder = new System.Text.StringBuilder();

            using (var reader = new StreamReader(csvFilePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    stringBuilder.AppendLine(line);
                }
            }

            return stringBuilder.ToString();
        }
        public static string InspectCsvHeaders(string csvFilePath)
        {
            string testString;
            using (var reader = new StreamReader(csvFilePath))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Read();
                csv.ReadHeader();
                var headers = csv.HeaderRecord;
                testString ="CSV Headers: ";
                foreach (var header in headers)
                {
                    testString += header;
                }
            }
            return testString;
        }
       



    }
}
