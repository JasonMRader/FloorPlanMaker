using FloorplanClassLibrary;
using LiveChartsCore;
//using LiveCharts.Wpf;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

        public void SetupChart(List<DiningArea> diningAreas)
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
                    Stroke = GetAreaColor(area), // Line color
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
