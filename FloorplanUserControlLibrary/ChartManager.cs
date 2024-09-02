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

            // Initialize X-Axis (assuming dates are involved)
            _chart.XAxes = new[]
            {
            new Axis
            {
                Name = "Date",
                Labels = _shiftRecords.Select(shift => shift.Date.ToString("MM/dd/yy")).ToArray(),
                LabelsRotation = 15, // Rotate labels for better readability
            }
        };

            // Initialize Y-Axis
            _chart.YAxes = new[]
            {
            new Axis
            {
                Name = "Sales",
                Labeler = value => value.ToString("C"), // Format as currency
            }
        };

            // Populate series with data
            foreach (ShiftRecord shiftRecord in _shiftRecords) {
                foreach (DiningAreaRecord areaRecord in shiftRecord.DiningAreaRecords) {
                    if (seriesMap.TryGetValue(areaRecord.DiningAreaID, out var series)) {
                        (series).Values =
                            (series).Values.Append((float)areaRecord.Sales).ToList();
                        //series.YToolTipLabelFormatter = point => $"{(float)areaRecord.Sales} {areaRecord.DiningAreaID}";
                        //series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                        //series.DataLabelsFormatter = point => $"{(float)areaRecord.Sales} {areaRecord.DiningAreaID}";
                        //series.DataLabelsPosition = DataLabelsPosition.End;
                        //// use the SalesPerDay property in this in the Y axis 
                        //// and the index of the fruit in the array in the X axis 
                        //series.Mapping = (areaRecord, index) => new(index, areaRecord.Sales);
                        //series.ChartPointPointerHover += OnPointerHover;
                        //series.Values.Add((float)areaRecord.Sales);
                    }
                }
            }
            //_chart.Series. += OnPointerDown;
           
            //salesPerDaysSeries.ChartPointPointerHoverLost += OnPointerHoverLost;
            // Assign the series collection to the chart
            _chart.Series = seriesMap.Values.ToArray();

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
                TextSize = 8, // Smaller font size for labels
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
                    Labeler = value => value.ToString("C"), // Format as currency
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
        public void SetUpBarChart(int diningAreaID)
        {
            // Filter the records by DiningAreaID
            List<DiningAreaRecord> diningAreaRecords = _shiftRecords.SelectMany(s => s.DiningAreaRecords).ToList();
            var filteredRecords = diningAreaRecords.Where(record => record.DiningAreaID == diningAreaID).ToList();

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
                    Labeler = value => value.ToString("N0"), // Format as integer
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
                    Labeler = value => value.ToString("C"), // Format as currency
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
    }
}
