using FloorplanClassLibrary;
using LiveChartsCore;
using LiveChartsCore.Defaults;
//using LiveCharts.Wpf;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.Measure;
using NetTopologySuite.Algorithm;


namespace FloorPlanMakerUI
{
    public class ChartManager
    {
        private List<ShiftRecord> _shiftRecords;
        private CartesianChart _chart;

        public ChartManager(List<ShiftRecord> shiftRecords, CartesianChart chart)
        {
            _shiftRecords = shiftRecords;
            _chart = chart;
        }
        public void SetUpStackedArea(List<DiningArea> diningAreas)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Dictionary to map DiningAreaID to its corresponding StackedAreaSeries
            var seriesMap = new Dictionary<int, StackedAreaSeries<float>>();

            // Initialize series for each dining area
            foreach (DiningArea area in diningAreas) {
                if (area.ID == 6) continue; // Skip DiningAreaID 6

                var series = new StackedAreaSeries<float> {
                    Name = area.Name,
                    Fill = GetAreaColor(area), // Set the fill color
                    Stroke = GetAreaColor(area), // Set the line color
                    Values = new List<float>(), // Initialize with an empty list
                };

                seriesMap[area.ID] = series;
            }

            // Generate a distinct list of all dates from ShiftRecords for the X-Axis
            var allDates = _shiftRecords.Select(shift => shift.Date).Distinct().OrderBy(date => date).ToList();

            // Initialize X-Axis with all the dates (assuming this is the primary key to sort by)
            _chart.XAxes = new[]{
                new Axis
                {
                    Name = "Date",
                    Labels = allDates.Select(date => date.ToString("MM/dd/yy")).ToArray(),
                    LabelsRotation = 15,
                }
            };

            // Initialize Y-Axis
            _chart.YAxes = new[]{
                new Axis
                {
                    Name = "Sales",
                    Labeler = value => value.ToString("C"),
                    MinLimit = 0,// Format as currency
                }
            };

            // Populate series with data for each shift, aligning the correct date
            foreach (var series in seriesMap.Values) {
                // For each series, initialize the sales values with zero for each date in the X-axis
                series.Values = allDates.Select(_ => 0f).ToList(); // Convert to List<float> to allow indexing
            }

            // Fill the series values with sales data, ensuring correct date alignment
            foreach (ShiftRecord shiftRecord in _shiftRecords) {
                int dateIndex = allDates.IndexOf(shiftRecord.Date);
                if (dateIndex >= 0) {
                    foreach (DiningAreaRecord areaRecord in shiftRecord.DiningAreaRecords) {
                        if (seriesMap.TryGetValue(areaRecord.DiningAreaID, out var series)) {
                            // Since Values is now a List<float>, indexing will work
                            var listValues = series.Values as List<float>; // Cast to List<float>
                            listValues[dateIndex] += (float)areaRecord.Sales;
                            series.Values = listValues; // Reassign the updated list back
                        }
                    }
                }
            }

            _chart.Series = seriesMap.Values.ToArray();

            // Set legend location
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }
        public void SetUpTemperatureScatterPlot()
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Prepare the scatter series
            var scatterSeries = new ScatterSeries<ObservablePoint> {
                Values = _shiftRecords
                    .Where(sr => sr.ShiftWeather != null) // Ensure ShiftWeather exists
                    .Select(sr => new ObservablePoint(sr.ShiftWeather.FeelsLikeAvg, sr.Sales)) // X = FeelsLikeAvg, Y = Sales
                    .ToList(),
                Stroke = null,
                GeometrySize = 5
            };

            // Assign the series to the chart
            _chart.Series = new ISeries[] { scatterSeries };

            // Configure the X-Axis for FeelsLikeAvg temperature
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Name = "Feels Like Avg Temperature (°F)",
                    Labeler = value => $"{value:F1}°F", // Format temperature with 1 decimal place
                    MinLimit = _shiftRecords.Min(sr => sr.ShiftWeather?.FeelsLikeAvg ?? 0) - 5, // Optional: Define a min limit with buffer
                    MaxLimit = _shiftRecords.Max(sr => sr.ShiftWeather?.FeelsLikeAvg ?? 100) + 5 // Optional: Define a max limit with buffer
                }
            };

            // Configure the Y-Axis for sales
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Sales",
                    Labeler = value => value.ToString("C"), // Format sales as currency
                    MinLimit = 0, // Sales typically start from 0
                    MaxLimit = _shiftRecords.Max(sr => sr.Sales) * 1.1 // Add a small buffer above the max sales
                }
            };

            // Set legend position (optional for scatter plot)
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }

        public void SetUpStackedBarChartByMonth(List<DiningArea> diningAreas)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Dictionary to map DiningAreaID to its corresponding StackedColumnSeries
            var seriesMap = new Dictionary<int, StackedColumnSeries<float>>();

            // Initialize series for each dining area
            foreach (DiningArea area in diningAreas) {
                if (area.ID == 6) continue; // Skip DiningAreaID 6

                var series = new StackedColumnSeries<float> {
                    Name = area.Name,
                    Fill = GetAreaColor(area), // Set the fill color
                    Stroke = GetAreaColor(area), // Set the line color
                    Values = new List<float>(), // Initialize with a List<float> to allow adding values
                    StackGroup = 0 // Group all dining areas in the same stack
                };

                seriesMap[area.ID] = series;
            }

            // Group ShiftRecords by month
            var groupedByMonth = _shiftRecords
                .GroupBy(shift => new { Year = shift.Date.Year, Month = shift.Date.Month })
                .OrderBy(group => new DateTime(group.Key.Year, group.Key.Month, 1)) // Order by month
                .ToList();

            // Initialize X-Axis labels with months
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Name = "Month",
                    Labels = groupedByMonth
                        .Select(group => new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMM yyyy"))
                        .ToArray(),
                    LabelsRotation = -15
                }
            };

            // Calculate the average sales for each dining area per month
            foreach (var monthGroup in groupedByMonth) {
                foreach (var diningArea in diningAreas) {
                    if (diningArea.ID == 6) continue; // Skip DiningAreaID 6

                    // Get all area records for this dining area within the current month
                    var areaSalesForMonth = monthGroup
                        .SelectMany(shift => shift.DiningAreaRecords)
                        .Where(record => record.DiningAreaID == diningArea.ID)
                        .Select(record => record.Sales)
                        .ToList();

                    // Calculate the average sales for this dining area in this month
                    float averageSales = areaSalesForMonth.Any() ? areaSalesForMonth.Average() : 0f;

                    // Add the average sales to the corresponding series (cast to List<float> to allow adding values)
                    if (seriesMap.TryGetValue(diningArea.ID, out var series)) {
                        (series.Values as List<float>)?.Add(averageSales); // Ensure it's a List<float> and add the value
                    }
                }
            }

            // Assign series to the chart
            _chart.Series = seriesMap.Values.ToArray();

            // Initialize Y-Axis for sales values
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Average Sales",
                    Labeler = value => value.ToString("C"),
                     MinLimit = 0,// Format as currency
                }
            };

            // Set legend location
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }

        public void SetUpStackedBarChartByDayOfWeek(List<DiningArea> diningAreas)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Dictionary to map DiningAreaID to its corresponding StackedColumnSeries
            var seriesMap = new Dictionary<int, StackedColumnSeries<float>>();

            // Initialize series for each dining area
            foreach (DiningArea area in diningAreas) {
                if (area.ID == 6) continue; // Skip DiningAreaID 6

                var series = new StackedColumnSeries<float> {
                    Name = area.Name,
                    Fill = GetAreaColor(area), // Set the fill color
                    Stroke = GetAreaColor(area), // Set the line color
                    Values = new List<float>(), // Initialize with a List<float> to allow adding values
                    StackGroup = 0 // Group all dining areas in the same stack
                };

                seriesMap[area.ID] = series;
            }

            // Group ShiftRecords by day of the week
            var groupedByDayOfWeek = _shiftRecords
                .GroupBy(shift => shift.DayOfWeek)
                .OrderBy(group => group.Key) // Order by day of the week (Monday-Sunday)
                .ToList();

            // Initialize X-Axis labels with days of the week
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Name = "Day of the Week",
                    Labels = groupedByDayOfWeek
                        .Select(group => group.Key.ToString()) // Convert DayOfWeek enum to string (e.g., "Monday")
                        .ToArray(),
                    LabelsRotation = -15
                }
            };

            // Calculate the average sales for each dining area per day of the week
            foreach (var dayGroup in groupedByDayOfWeek) {
                foreach (var diningArea in diningAreas) {
                    if (diningArea.ID == 6) continue; // Skip DiningAreaID 6

                    // Get all area records for this dining area within the current day
                    var areaSalesForDay = dayGroup
                        .SelectMany(shift => shift.DiningAreaRecords)
                        .Where(record => record.DiningAreaID == diningArea.ID)
                        .Select(record => record.Sales)
                        .ToList();

                    // Calculate the average sales for this dining area on this day
                    float averageSales = areaSalesForDay.Any() ? areaSalesForDay.Average() : 0f;

                    // Add the average sales to the corresponding series (cast to List<float> to allow adding values)
                    if (seriesMap.TryGetValue(diningArea.ID, out var series)) {
                        (series.Values as List<float>)?.Add(averageSales); // Ensure it's a List<float> and add the value
                    }
                }
            }

            // Assign series to the chart
            _chart.Series = seriesMap.Values.ToArray();

            // Initialize Y-Axis for sales values
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Average Sales",
                    Labeler = value => value.ToString("C"),
                     MinLimit = 0,// Format as currency
                }
            };

            // Set legend location
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }



        public void SetUpMiniStackedArea(List<DiningArea> diningAreas)
        {
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            var seriesMap = new Dictionary<int, StackedAreaSeries<float>>();

            foreach (DiningArea area in diningAreas) {
                if (area.ID == 6) continue; // Skip DiningAreaID 6

                var series = new StackedAreaSeries<float> {
                    Name = area.Name,
                    Fill = GetAreaColor(area), // Set the fill color
                    Stroke = GetAreaColor(area), // Set the line color
                    Values = new List<float>(), // Initialize with an empty list
                    LineSmoothness = 0, // Set to 0 for straight lines (or adjust as needed)
                    GeometrySize = 0, // Remove points (set to a small value if needed)
                    //StrokeThickness = 1 // Thin the line for a smaller appearance
                };

                seriesMap[area.ID] = series;
            }

            // Initialize X-Axis (assuming dates are involved)
            _chart.XAxes = new[]
            {
        new Axis
            {
                //Name = "Date",
                //Labels = _shiftRecords.Select(shift => shift.Date.ToString("MM/dd/yy")).ToArray(),
                //LabelsRotation = 15, // Rotate labels for better readability
                TextSize = 8, // Smaller font size for labels
                Padding = new LiveChartsCore.Drawing.Padding(0), // Reduce padding
                IsVisible = false,
            }
        };

            // Initialize Y-Axis
            _chart.YAxes = new[]
            {
        new Axis
            {
                //Name = "Sales",
                //Labeler = value => value.ToString("C"), // Format as currency
                 Labeler = value => (value/1000).ToString("C0") + "K",
                 LabelsRotation = 45,
                 TextSize = 8,
                 MinLimit = 0,// Smaller font size for labels
                //Padding = new LiveChartsCore.Drawing.Padding(0),
                
            }
        };

            // Populate series with data
            foreach (ShiftRecord shiftRecord in _shiftRecords) {
                foreach (DiningAreaRecord areaRecord in shiftRecord.DiningAreaRecords) {
                    if (seriesMap.TryGetValue(areaRecord.DiningAreaID, out var series)) {
                        series.Values = series.Values.Append((float)areaRecord.Sales).ToList();
                    }
                }
            }

            _chart.Series = seriesMap.Values.ToArray();
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Hidden;

            // Reduce chart margins and padding for a more compact display
            _chart.DrawMargin = new LiveChartsCore.Measure.Margin(5); // Adjusted to correct type
            _chart.Padding = new System.Windows.Forms.Padding(0);
            _chart.LegendTextSize = 8; // Smaller font size for legend if shown
        }

        private void OnPointerHover(IChartView chart, ChartPoint<ShiftRecord, RoundedRectangleGeometry, LabelGeometry>? point)
        {
            //if (point?.Visual is null) return;
            //point.Visual.Fill = new SolidColorPaint(SKColors.Yellow);
            //chart.Invalidate();
            //Trace.WriteLine($"Pointer entered on {point.Model?.Name}");
        }
        public void SetupLineChart(List<DiningArea> diningAreas)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Dictionary to map DiningAreaID to its corresponding LineSeries
            var seriesMap = new Dictionary<int, ISeries>();

            // Initialize series for each dining area
            foreach (DiningArea area in diningAreas) {
                if (area.ID == 6) {
                    continue; // Skip DiningAreaID 6
                }

                var series = new LineSeries<float, SVGPathGeometry> {
                    Name = area.Name,
                    GeometrySvg = GetAreaShape(area), // Use the SVG path string
                    GeometrySize = 10,
                    Stroke = GetAreaColor(area),
                    GeometryStroke = GetAreaColor(area),
                    GeometryFill = GetAreaColor(area),
                    Fill = null, // No fill for the line
                    Values = new List<float>() // Initialize with an empty list
                };

                seriesMap[area.ID] = series;
            }

            // Initialize X-Axis (assuming dates are involved)
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Name = "Date",
                    Labels = _shiftRecords.Select(shift => shift.Date.ToString("MM/dd")).ToArray(),
                    LabelsRotation = 15, // Rotate labels for better readability
                }
            };

            // Initialize Y-Axis
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Sales",
                    Labeler = value => value.ToString("C"),
                    MinLimit = 0,// Format as currency
                }
            };

            // Populate series with data
            foreach (ShiftRecord shiftRecord in _shiftRecords) {
                foreach (DiningAreaRecord areaRecord in shiftRecord.DiningAreaRecords) {
                    if (seriesMap.TryGetValue(areaRecord.DiningAreaID, out var series)) {
                        ((LineSeries<float, SVGPathGeometry>)series).Values = ((LineSeries<float, SVGPathGeometry>)series).Values.Append((float)areaRecord.Sales).ToList();
                    }
                }
            }

            // Assign the series collection to the chart
            _chart.Series = seriesMap.Values.ToArray();

            // Set legend location
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }
        public void SetUpBarChartForShiftSales()
        {
            // Get the list of total sales from each shift record
            List<float> shiftSales = _shiftRecords.Select(shift => shift.Sales).ToList();

            if (shiftSales.Count == 0) {
                return; // No records to display
            }

            // Determine the range of sales
            var minSales = shiftSales.Min();
            var maxSales = shiftSales.Max();

            // Define the bin size (e.g., 1000)
            int binSize = 1000;

            // Adjust minSales to the nearest lower bin
            minSales = (int)(minSales / binSize) * binSize;

            // Create bins starting from the nearest lower bin
            var bins = new List<int>();
            for (int i = (int)minSales; i <= (int)maxSales; i += binSize) {
                bins.Add(i);
            }

            // Count the number of shift records in each bin
            var binCounts = new List<int>();
            foreach (var binStart in bins) {
                int count = shiftSales.Count(sales => sales >= binStart && sales < binStart + binSize);
                binCounts.Add(count);
            }

            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Create the bar series with no space between bars
            var barSeries = new ColumnSeries<int> {
                Values = binCounts,
                Name = "Sales Distribution for Shifts",
                Stroke = new SolidColorPaint(SKColors.Blue),
                Fill = new SolidColorPaint(SKColors.LightBlue),
                Padding = 0, // No space between bars
                MaxBarWidth = double.PositiveInfinity // Ensure bars take up full width
            };

            // Assign the series to the chart
            _chart.Series = new ISeries[] { barSeries };

            // Set up the X-Axis with the bin labels
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Labels = bins.Select(binStart => $"{binStart / 1000}-{(binStart + binSize) / 1000}k").ToArray(),
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    ForceStepToMin = true,
                    MinStep = 1
                }
            };

            // Set up the Y-Axis
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Number of Shifts",
                    Labeler = value => value.ToString("N0"),
                    MinLimit = 0,// Format as integer
                    ForceStepToMin = true,
                    MinStep = 1
                }
            };

            // Set legend position (optional)
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }

        public void SetUpBarChart(int diningAreaID)
        {
            var filteredRecords = new List<DiningAreaRecord>();
            
           
            List<DiningAreaRecord> diningAreaRecords = _shiftRecords.SelectMany(s => s.DiningAreaRecords).ToList();
            filteredRecords = diningAreaRecords.Where(record => record.DiningAreaID == diningAreaID).ToList();

            if (filteredRecords.Count == 0) {
                return; // No records to display
            }

            // Determine the range of sales
            var minSales = filteredRecords.Min(record => record.Sales);
            var maxSales = filteredRecords.Max(record => record.Sales);

            // Define the bin size (e.g., 1000)
            int binSize = 1000;

            minSales = (int)(minSales / binSize) * binSize;

            // Create bins starting from the nearest lower bin
            var bins = new List<int>();
            for (int i = (int)minSales; i <= (int)maxSales; i += binSize) {
                bins.Add(i);
            }

            // Count the number of records in each bin
            var binCounts = new List<int>();
            foreach (var binStart in bins) {
                int count = filteredRecords.Count(record => record.Sales >= binStart && record.Sales < binStart + binSize);
                binCounts.Add(count);
            }

            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Create the bar series with no space between bars
            var barSeries = new ColumnSeries<int> {
                Values = binCounts,
                Name = $"Sales Distribution for Dining Area {diningAreaID}",
                Stroke = new SolidColorPaint(SKColors.Blue),
                Fill = new SolidColorPaint(SKColors.LightBlue),
                Padding = 0, // No space between bars
                MaxBarWidth = double.PositiveInfinity // Ensure bars take up full width
            };

            // Assign the series to the chart
            _chart.Series = new ISeries[] { barSeries };

            // Set up the X-Axis with the bin labels
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Labels = bins.Select(binStart => $"{binStart / 1000}-{(binStart + binSize) / 1000}k").ToArray(),
                    LabelsRotation = 0,
                    SeparatorsPaint = new SolidColorPaint(new SKColor(200, 200, 200)),
                    ForceStepToMin = true,
                    MinStep = 1
                }
            };

            // Set up the Y-Axis
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Number of Shifts",
                    Labeler = value => value.ToString("N0"),
                    MinLimit = 0,// Format as integer
                    ForceStepToMin = true,
                    MinStep = 1
                }
            };

            // Set legend position (optional)
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }

        public void SetUpBoxChart(List<DiningArea> diningAreas)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Dictionary to map DiningAreaID to its corresponding BoxSeries
            var seriesMap = new Dictionary<int, BoxSeries<BoxValue>>();

            // Initialize series for each dining area
            foreach (DiningArea area in diningAreas) {
                if (area.ID == 6) continue; // Skip DiningAreaID 6

                // Calculate the min, lower quartile, median, upper quartile, and max for sales
                var areaRecords = _shiftRecords
                    .SelectMany(shift => shift.DiningAreaRecords)
                    .Where(record => record.DiningAreaID == area.ID)
                    .Select(record => record.Sales)
                    .OrderBy(sales => sales)
                    .ToArray();

                if (areaRecords.Length == 0) continue;

                int count = areaRecords.Length;
                double min = areaRecords.First();
                double max = areaRecords.Last();
                double median = areaRecords[count / 2];
                double lowerQuartile = areaRecords[count / 4];
                double upperQuartile = areaRecords[3 * count / 4];

                // Create a BoxSeries for this dining area
                var series = new BoxSeries<BoxValue> {
                    Name = area.Name,
                    Values = new BoxValue[]
                    {
                new BoxValue(max, upperQuartile, median, lowerQuartile, min)
                    },
                    Stroke = GetAreaColor(area),
                    Fill = new SolidColorPaint(GetAreaColor(area).Color, 0.3f) // Set fill color with transparency
                };

                seriesMap[area.ID] = series;
            }

            // Assign the series collection to the chart
            _chart.Series = seriesMap.Values.ToArray();

            // Initialize X-Axis
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Name = "Dining Areas",
                    Labels = diningAreas.Where(area => area.ID != 6).Select(area => area.Name).ToArray(),
                    LabelsRotation = 15, // Rotate labels for better readability
                }
            };

            // Initialize Y-Axis
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Sales",
                    Labeler = value => value.ToString("C"),
                     MinLimit = 0,// Format as currency
                }
            };

            // Set legend location
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }


        public static string GetAreaShape(DiningArea area)
        {
            switch (area.ID) {
                case 1:
                    return SVGPoints.Circle;
                case 2:
                    return SVGPoints.Square;
                case 3:
                    return SVGPoints.Star;
                case 4:
                    return SVGPoints.Diamond;
                case 5:
                    return SVGPoints.Cross;
                // Add more cases as needed
                default:
                    return SVGPoints.Pin; // Default shape
            }
        }

        // Method to get the color based on DiningAreaID
        public static SolidColorPaint GetAreaColor(DiningArea area)
        {
            switch (area.ID) {
                case 1:
                    return new SolidColorPaint(SKColors.Red);
                case 2:
                    return new SolidColorPaint(SKColors.Green);
                case 3:
                    return new SolidColorPaint(SKColors.Blue);
                case 4:
                    return new SolidColorPaint(SKColors.Orange);
                case 5:
                    return new SolidColorPaint(SKColors.Yellow);
                // Add more cases as needed
                default:
                    return new SolidColorPaint(SKColors.Gray); // Default color
            }
        }
        public void SetupLineChartForArea(DiningArea diningArea)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Skip if the area is DiningAreaID 6 (or any other logic you want to handle)
            if (diningArea.ID == 6) {
                return; // Skip if DiningAreaID is 6
            }

            // Initialize series for the provided dining area
            var series = new LineSeries<float, SVGPathGeometry> {
                Name = diningArea.Name,
                GeometrySvg = GetAreaShape(diningArea), // Use the SVG path string
                GeometrySize = 10,
                Stroke = GetAreaColor(diningArea),
                GeometryStroke = GetAreaColor(diningArea),
                GeometryFill = GetAreaColor(diningArea),
                Fill = null, // No fill for the line
                Values = new List<float>() // Initialize with an empty list
            };

            // Initialize X-Axis (assuming dates are involved)
            _chart.XAxes = new[]
            {
            new Axis
                {
                    Name = "Date",
                    Labels = _shiftRecords.Select(shift => shift.Date.ToString("MM/dd")).ToArray(),
                    LabelsRotation = 15, // Rotate labels for better readability
                }
            };

            // Initialize Y-Axis
            _chart.YAxes = new[]
            {
            new Axis
                {
                    Name = "Sales",
                    Labeler = value => value.ToString("C"),
                    MinLimit = 0,
                    
                }
            };

            // Populate the series with data for the provided dining area
            foreach (ShiftRecord shiftRecord in _shiftRecords) {
                foreach (DiningAreaRecord areaRecord in shiftRecord.DiningAreaRecords) {
                    if (areaRecord.DiningAreaID == diningArea.ID) {
                        series.Values = series.Values.Append((float)areaRecord.Sales).ToList();
                    }
                }
            }

            // Assign the series collection to the chart
            _chart.Series = new[] { series };

            // Set legend location
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }
        public void SetUpBarChartByMonthForArea(int diningAreaID, List<DiningArea> diningAreas)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Get the dining area by ID
            var diningArea = diningAreas.FirstOrDefault(area => area.ID == diningAreaID);
            if (diningArea == null || diningArea.ID == 6) return; // Skip if not found or if DiningAreaID is 6

            // Initialize series for the selected dining area
            var series = new ColumnSeries<float> {
                Name = diningArea.Name,
                Fill = GetAreaColor(diningArea), // Set the fill color
                Stroke = GetAreaColor(diningArea), // Set the line color
                Values = new List<float>() // Initialize with a List<float> to allow adding values
            };

            // Group ShiftRecords by month
            var groupedByMonth = _shiftRecords
                .GroupBy(shift => new { Year = shift.Date.Year, Month = shift.Date.Month })
                .OrderBy(group => new DateTime(group.Key.Year, group.Key.Month, 1)) // Order by month
                .ToList();

            // Initialize X-Axis labels with months
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Name = "Month",
                    Labels = groupedByMonth
                        .Select(group => new DateTime(group.Key.Year, group.Key.Month, 1).ToString("MMM yyyy"))
                        .ToArray(),
                    LabelsRotation = -15 // Rotate labels for better readability
                }
            };

            // Calculate the average sales for the specific dining area per month
            foreach (var monthGroup in groupedByMonth) {
                // Get all area records for this dining area within the current month
                var areaSalesForMonth = monthGroup
                    .SelectMany(shift => shift.DiningAreaRecords)
                    .Where(record => record.DiningAreaID == diningAreaID)
                    .Select(record => record.Sales)
                    .ToList();

                // Calculate the average sales for this dining area in this month
                float averageSales = areaSalesForMonth.Any() ? areaSalesForMonth.Average() : 0f;

                // Add the average sales to the series
                (series.Values as List<float>)?.Add(averageSales); // Ensure it's a List<float> and add the value
            }

            // Assign the series to the chart
            _chart.Series = new ISeries[] { series };

            // Initialize Y-Axis for sales values
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Average Sales",
                    Labeler = value => value.ToString("C"), // Format as currency
                    MinLimit = 0 // Ensure Y-axis starts at $0
                }
            };

            // Set legend location (optional)
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }
        public void SetUpBarChartByDayOfWeekForArea(int diningAreaID, List<DiningArea> diningAreas)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Get the dining area by ID
            var diningArea = diningAreas.FirstOrDefault(area => area.ID == diningAreaID);
            if (diningArea == null || diningArea.ID == 6) return; // Skip if not found or if DiningAreaID is 6

            // Initialize series for the selected dining area
            var series = new ColumnSeries<float> {
                Name = diningArea.Name,
                Fill = GetAreaColor(diningArea), // Set the fill color
                Stroke = GetAreaColor(diningArea), // Set the line color
                Values = new List<float>() // Initialize with a List<float> to allow adding values
            };

            // Group ShiftRecords by day of the week
            var groupedByDayOfWeek = _shiftRecords
                .GroupBy(shift => shift.DayOfWeek)
                .OrderBy(group => group.Key) // Order by day of the week (Monday-Sunday)
                .ToList();

            // Initialize X-Axis labels with days of the week
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Name = "Day of the Week",
                    Labels = groupedByDayOfWeek
                        .Select(group => group.Key.ToString()) // Convert DayOfWeek enum to string (e.g., "Monday")
                        .ToArray(),
                    LabelsRotation = -15 // Rotate labels for better readability
                }
            };

            // Calculate the average sales for the specific dining area per day of the week
            foreach (var dayGroup in groupedByDayOfWeek) {
                // Get all area records for this dining area within the current day
                var areaSalesForDay = dayGroup
                    .SelectMany(shift => shift.DiningAreaRecords)
                    .Where(record => record.DiningAreaID == diningAreaID)
                    .Select(record => record.Sales)
                    .ToList();

                // Calculate the average sales for this dining area on this day
                float averageSales = areaSalesForDay.Any() ? areaSalesForDay.Average() : 0f;

                // Add the average sales to the series
                (series.Values as List<float>)?.Add(averageSales); // Ensure it's a List<float> and add the value
            }

            // Assign the series to the chart
            _chart.Series = new ISeries[] { series };

            // Initialize Y-Axis for sales values
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Average Sales",
                    Labeler = value => value.ToString("C"), // Format as currency
                    MinLimit = 0 // Ensure Y-axis starts at $0
                }
            };

            // Set legend location (optional)
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }
        public void SetUpTemperatureScatterPlotForArea(int diningAreaID)
        {
            // Clear existing series and axes
            _chart.Series = Array.Empty<ISeries>();
            _chart.XAxes = Array.Empty<Axis>();
            _chart.YAxes = Array.Empty<Axis>();

            // Prepare the scatter series for the specific dining area
            var scatterSeries = new ScatterSeries<ObservablePoint> {
                Values = _shiftRecords
                    .Where(sr => sr.ShiftWeather != null) // Ensure ShiftWeather exists
                    .SelectMany(sr => sr.DiningAreaRecords
                        .Where(record => record.DiningAreaID == diningAreaID) // Filter by the specific dining area
                        .Select(record => new ObservablePoint(sr.ShiftWeather.FeelsLikeAvg, record.Sales)) // X = FeelsLikeAvg, Y = Sales
                    )
                    .ToList(),
                Stroke = null,
                GeometrySize = 5
            };

            // Assign the series to the chart
            _chart.Series = new ISeries[] { scatterSeries };

            // Configure the X-Axis for FeelsLikeAvg temperature
            _chart.XAxes = new[]
            {
                new Axis
                {
                    Name = "Feels Like Avg Temperature (°F)",
                    Labeler = value => $"{value:F1}°F", // Format temperature with 1 decimal place
                    MinLimit = _shiftRecords.Min(sr => sr.ShiftWeather?.FeelsLikeAvg ?? 0) - 5, // Optional: Define a min limit with buffer
                    MaxLimit = _shiftRecords.Max(sr => sr.ShiftWeather?.FeelsLikeAvg ?? 100) + 5 // Optional: Define a max limit with buffer
                }
            };

            // Configure the Y-Axis for sales
            _chart.YAxes = new[]
            {
                new Axis
                {
                    Name = "Sales",
                    Labeler = value => value.ToString("C"), // Format sales as currency
                    MinLimit = 0, // Sales typically start from 0
                    MaxLimit = _shiftRecords
                        .SelectMany(sr => sr.DiningAreaRecords.Where(record => record.DiningAreaID == diningAreaID))
                        .Max(record => record.Sales) * 1.1 // Add a small buffer above the max sales for this area
                }
            };

            // Set legend position (optional for scatter plot)
            _chart.LegendPosition = LiveChartsCore.Measure.LegendPosition.Right;
        }

    }
}
