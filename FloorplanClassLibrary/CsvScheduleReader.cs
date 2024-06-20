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

            using (var reader = new StreamReader(csvFilePath, Encoding.UTF8))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                MissingFieldFound = null,
                IgnoreBlankLines = true,
                TrimOptions = TrimOptions.Trim,
                BadDataFound = null
            }))
            {
                // Skip lines until you reach the headers of the data
                for (int i = 0; i < 11; i++)
                {
                    csv.Read();
                }

                // Read the header line with dates
                csv.Read();
                var headers = csv.Parser.Record.Select(h => h.Clean()).ToArray();

                Console.WriteLine("Headers:");
                foreach (var header in headers)
                {
                    Console.WriteLine(header);
                }

                csv.Context.RegisterClassMap<CsvServerRecordMap>();
                var records = csv.GetRecords<CsvServerRecord>().ToList();

                Console.WriteLine("Records:");
                foreach (var record in records)
                {
                    // Clean special characters in JobCode and EmployeeName
                    record.JobCode = record.JobCode.Clean();
                    record.EmployeeName = record.EmployeeName.Clean();
                    Console.WriteLine($"{record.JobCode}, {record.EmployeeName}");
                }

                records = records
                          .Where(r => r.JobCode == "Server" || r.JobCode == "Bartender")
                          .ToList();

                foreach (var record in records)
                {
                    AddScheduledShifts(scheduledShifts, record, headers);
                }
            }

            return scheduledShifts;
        }

        private static void AddScheduledShifts(List<ScheduledShift> scheduledShifts, CsvServerRecord record, string[] headers)
        {
            // Ensure headers array has the expected number of elements
            if (headers.Length < 9)
            {
                Console.WriteLine("Error: Headers array does not have the expected number of elements.");
                return;
            }

            try
            {
                AddScheduledShift(scheduledShifts, record.Mon, record.EmployeeName, DateOnly.Parse(headers[2].Split(',')[1].Trim()));
                AddScheduledShift(scheduledShifts, record.Tue, record.EmployeeName, DateOnly.Parse(headers[3].Split(',')[1].Trim()));
                AddScheduledShift(scheduledShifts, record.Wed, record.EmployeeName, DateOnly.Parse(headers[4].Split(',')[1].Trim()));
                AddScheduledShift(scheduledShifts, record.Thu, record.EmployeeName, DateOnly.Parse(headers[5].Split(',')[1].Trim()));
                AddScheduledShift(scheduledShifts, record.Fri, record.EmployeeName, DateOnly.Parse(headers[6].Split(',')[1].Trim()));
                AddScheduledShift(scheduledShifts, record.Sat, record.EmployeeName, DateOnly.Parse(headers[7].Split(',')[1].Trim()));
                AddScheduledShift(scheduledShifts, record.Sun, record.EmployeeName, DateOnly.Parse(headers[8].Split(',')[1].Trim()));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AddScheduledShifts: " + ex.Message);
                Console.WriteLine("Headers:");
                foreach (var header in headers)
                {
                    Console.WriteLine(header);
                }
            }
        }


        private static void AddScheduledShift(List<ScheduledShift> scheduledShifts, string timeSlot, string employeeName, DateOnly date)
        {
            if (string.IsNullOrEmpty(timeSlot)) return;

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
                testString = "CSV Headers: ";
                foreach (var header in headers)
                {
                    testString += header;
                }
            }
            return testString;
        }


    }

    
}
