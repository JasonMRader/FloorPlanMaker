using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TablePercentageRecord
    {
        public int ID { get; set; }
        public string TableNumber { get; set; }
        public int DiningAreaID { get; set; }
        public bool IsIncluded { get; set; } = true;
        public double LessThan1k { get; set; } = 0.0;
        public double _1kTo2k { get; set; } = 0.0;
        public double _2kTo3k { get; set; } = 0.0;
        public double _3kTo4k { get; set; } = 0.0;
        public double _4kTo5k { get; set; } = 0.0;
        public double _5kTo6k { get; set; } = 0.0;
        public double _6kTo7k { get; set; } = 0.0;
        public double _7kTo8k { get; set; } = 0.0;
        public double _8kTo9k { get; set; } = 0.0;
        public double _9kTo10k { get; set; } = 0.0;
        public double _10kTo11k { get; set; } = 0.0;
        public double _11kTo12k { get; set; } = 0.0;
        public double _12kTo13k { get; set; } = 0.0;
        public double _13kTo14k { get; set; } = 0.0;
        public double _14kTo15k { get; set; } = 0.0;
        public double _15kTo16k { get; set; } = 0.0;
        public double _16kTo17k { get; set; } = 0.0;
        public double _17kTo18k { get; set; } = 0.0;
        public double _18kTo19k { get; set; } = 0.0;
        public double _19kTo20k { get; set; } = 0.0;
        public double _20kTo21k { get; set; } = 0.0;
        public double _21kTo22k { get; set; } = 0.0;
        public double _22kTo23k { get; set; } = 0.0;
        public double _23kTo24k { get; set; } = 0.0;
        public double _24kTo25k { get; set; } = 0.0;
        public double _25kTo26k { get; set; } = 0.0;
        public double _26kTo27k { get; set; } = 0.0;
        public double _27kTo28k { get; set; } = 0.0;
        public double _28kTo29k { get; set; } = 0.0;
        public double _29kTo30k { get; set; } = 0.0;
        public double GreaterThan30k { get; set; } = 0.0;
        public Dictionary<string, List<TableStat>> tableStatPercentagesByCategory = new Dictionary<string, List<TableStat>>{
                { "LessThan1k", new List<TableStat>() },
                { "1kTo2k", new List<TableStat>() },
                { "2kTo3k", new List<TableStat>() },
                { "3kTo4k", new List<TableStat>() },
                { "4kTo5k", new List<TableStat>() },
                { "5kTo6k", new List<TableStat>() },
                { "6kTo7k", new List<TableStat>() },
                { "7kTo8k", new List<TableStat>() },
                { "8kTo9k", new List<TableStat>() },
                { "9kTo10k", new List<TableStat>() },
                { "10kTo11k", new List<TableStat>() },
                { "11kTo12k", new List<TableStat>() },
                { "12kTo13k", new List<TableStat>() },
                { "13kTo14k", new List<TableStat>() },
                { "14kTo15k", new List<TableStat>() },
                { "15kTo16k", new List<TableStat>() },
                { "16kTo17k", new List<TableStat>() },
                { "17kTo18k", new List<TableStat>() },
                { "18kTo19k", new List<TableStat>() },
                { "19kTo20k", new List<TableStat>() },
                { "20kTo21k", new List<TableStat>() },
                { "21kTo22k", new List<TableStat>() },
                { "22kTo23k", new List<TableStat>() },
                { "23kTo24k", new List<TableStat>() },
                { "24kTo25k", new List<TableStat>() },
                { "25kTo26k", new List<TableStat>() },
                { "26kTo27k", new List<TableStat>() },
                { "27kTo28k", new List<TableStat>() },
                { "28kTo29k", new List<TableStat>() },
                { "29kTo30k", new List<TableStat>() },
                { "GreaterThan30k", new List<TableStat>() }
            };
        public double EstimatedSales { get; private set; }
        public double PercentageForSpecificEstimate(float salesEstimate)
        {
            string range = SalesRange.GetSalesCategory(salesEstimate);
            string propertyName = range;
            if (char.IsDigit(propertyName[0])) {
                propertyName = "_" + propertyName;
            }

            // Use reflection to get the property value
            var propertyInfo = this.GetType().GetProperty(propertyName);
            if (propertyInfo != null && propertyInfo.CanRead) {
                EstimatedSales = ((double)propertyInfo.GetValue(this) * salesEstimate)*.01;
                return (double)propertyInfo.GetValue(this);
            }
            EstimatedSales = 0;
            return 0;
        }
        public void ComputeAverageSalesPercentages()
        {
            //foreach (var kvp in tableStatPercentagesByCategory) {
            //    string category = kvp.Key;
            //    List<TableStat> tableStats = kvp.Value;

            //    // Calculate the average SalesPercentage for the category
            //    double averageSalesPercentage = 0.0;
            //    if (tableStats.Count > 0) {
            //        averageSalesPercentage = tableStats.Average(ts => ts.SalesPercentage);
            //    }

            //    // Map category to property name (add underscore if the category starts with a digit)
            //    string propertyName = category;
            //    if (char.IsDigit(propertyName[0])) {
            //        propertyName = "_" + propertyName;
            //    }

            //    // Use reflection to set the property value
            //    var propertyInfo = this.GetType().GetProperty(propertyName);
            //    if (propertyInfo != null && propertyInfo.CanWrite) {
            //        propertyInfo.SetValue(this, averageSalesPercentage);
            //    }
            //    else {
            //        // Handle cases where the property is not found or not writable
            //        // Optionally log a warning or throw an exception
            //        Console.WriteLine($"Property '{propertyName}' not found or not writable.");
            //    }
            //}
            foreach (var kvp in tableStatPercentagesByCategory) {
                string category = kvp.Key;
                List<TableStat> tableStats = kvp.Value;

                // Calculate the average SalesPercentage for the category
                //float totalSales = tableStats.Sum
                double averageSalesPercentage = 0.0;
                if (tableStats.Count > 0) {
                    averageSalesPercentage = tableStats.Average(ts => ts.SalesPercentage);
                }

                // Map category to property name (add underscore if the category starts with a digit)
                string propertyName = category;
                if (char.IsDigit(propertyName[0])) {
                    propertyName = "_" + propertyName;
                }

                // Use reflection to set the property value
                var propertyInfo = this.GetType().GetProperty(propertyName);
                if (propertyInfo != null && propertyInfo.CanWrite) {
                    propertyInfo.SetValue(this, averageSalesPercentage);
                }
                else {
                    // Handle cases where the property is not found or not writable
                    // Optionally log a warning or throw an exception
                    Console.WriteLine($"Property '{propertyName}' not found or not writable.");
                }
            }
        }
    }

}
