using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class DrawingHandler
    {
        private Panel targetPanel;
        private List<System.Drawing.Point> points;

        private bool isDragging = false;         // 1. Track if you're dragging the mouse
        private System.Drawing.Point? startPoint = null;
        private System.Drawing.Point? endPoint = null;
        private List<LineString> _voronoiEdges = new List<LineString>();

        public void SetVoronoiEdges(List<LineString> voronoiEdges)
        {
            _voronoiEdges = voronoiEdges;
            targetPanel.Invalidate();  // Trigger a redraw
        }
        public bool DrawSectionLinesMode { get; set; }

        public DrawingHandler(Panel panel)
        {
            this.targetPanel = panel;
            this.points = new List<System.Drawing.Point>();

            this.targetPanel.MouseDown += Panel_MouseDown;
            this.targetPanel.MouseMove += Panel_MouseMove;     // Add MouseMove event
            this.targetPanel.MouseUp += Panel_MouseUp;         // Add MouseUp event
            this.targetPanel.Paint += Panel_Paint;
        }

        private void Panel_MouseDown(object sender, MouseEventArgs e)
        {
            if (DrawSectionLinesMode && e.Button == MouseButtons.Left)
            {
                isDragging = true;

                startPoint = GetNearbyPoint(e.Location) ?? e.Location;    // 5. Search for nearby points
                endPoint = e.Location;
            }
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                endPoint = e.Location;
                targetPanel.Invalidate();
            }
        }

        //private void Panel_MouseUp(object sender, MouseEventArgs e)
        //{
        //    if (isDragging)
        //    {
        //        isDragging = false;
        //        points.Add(startPoint.Value);
        //        points.Add(endPoint.Value);
        //        startPoint = null;
        //        endPoint = null;
        //        targetPanel.Invalidate();
        //    }
        //}
        private List<SectionLine> _sectionLines = new List<SectionLine>();
        public List<SectionLine> GetSectionLines()
        {
            return _sectionLines;
        }
        public event EventHandler<SectionLine> LineDrawn;

        private void Panel_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                isDragging = false;

                SectionLine newLine = new SectionLine
                {
                    StartPoint = startPoint.Value,
                    EndPoint = endPoint.Value,
                };
                _sectionLines.Add(newLine);

                LineDrawn?.Invoke(this, newLine); // Raise the event

                startPoint = null;
                endPoint = null;
                targetPanel.Invalidate();
            }
        }
        public static void Printer_Paint(Graphics g, SectionLine line, System.Drawing.Point adjustedStart, System.Drawing.Point adjustedEnd)
        {
            using (Pen pen = new Pen(Color.Black, 3))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                g.DrawLine(pen, adjustedStart, adjustedEnd);
                g.FillEllipse(brush, adjustedStart.X - 2, adjustedStart.Y - 2, 5, 5);
                g.FillEllipse(brush, adjustedEnd.X - 2, adjustedEnd.Y - 2, 5, 5);
            }
        }
        private void Panel_Paint(object sender, PaintEventArgs e)
        {
            if (!DrawSectionLinesMode || _sectionLines.Count == 0)
                return;

            using (Pen pen = new Pen(Color.Black, 5))
            using (Brush brush = new SolidBrush(Color.Black))
            {
                foreach (var line in _sectionLines)
                {
                    e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);
                }

                if (isDragging && startPoint.HasValue && endPoint.HasValue)
                {
                    e.Graphics.DrawLine(pen, startPoint.Value, endPoint.Value);  // Draw the currently dragged line
                }

                foreach (var line in _sectionLines)
                {
                    e.Graphics.FillEllipse(brush, line.StartPoint.X - 2, line.StartPoint.Y - 2, 5, 5);
                    e.Graphics.FillEllipse(brush, line.EndPoint.X - 2, line.EndPoint.Y - 2, 5, 5);
                }
            }
            using (Pen voronoiPen = new Pen(Color.Red, 3))  // Use a different color or style for distinction, if needed.
            {
                foreach (var edge in _voronoiEdges)
                {
                    var points = edge.Coordinates.Select(coord => new System.Drawing.Point((int)coord.X, (int)coord.Y)).ToArray();
                    e.Graphics.DrawLines(voronoiPen, points);
                }
            }
        }

        private System.Drawing.Point? GetNearbyPoint(System.Drawing.Point currentPoint)
        {
            foreach (System.Drawing.Point point in points)
            {
                if (Math.Abs(currentPoint.X - point.X) <= 10 && Math.Abs(currentPoint.Y - point.Y) <= 10)
                    return point;
            }
            return null;
        }
        
        //private void Panel_Paint(object sender, PaintEventArgs e)
        //{
        //    if (!DrawSectionLinesMode || points.Count == 0)
        //        return;

        //    using (Pen pen = new Pen(Color.Black, 5))
        //    using (Brush brush = new SolidBrush(Color.Black))
        //    {
        //        for (int i = 0; i < points.Count - 1; i += 2)
        //        {
        //            e.Graphics.DrawLine(pen, points[i], points[i + 1]);
        //        }

        //        if (isDragging && startPoint.HasValue && endPoint.HasValue)
        //        {
        //            e.Graphics.DrawLine(pen, startPoint.Value, endPoint.Value);  // Draw the currently dragged line
        //        }

        //        foreach (Point point in points)
        //        {
        //            e.Graphics.FillEllipse(brush, point.X - 2, point.Y - 2, 5, 5);
        //        }
        //    }
        //}

        public void ClearLines()
        {
            points.Clear();
            targetPanel.Invalidate();
        }

        public void UndoLastPoint()
        {
            if (points.Count >= 2)
            {
                points.RemoveAt(points.Count - 1);
                points.RemoveAt(points.Count - 1);
                targetPanel.Invalidate();
            }
        }
        public List<SectionLine> GetDrawnLines()
        {
            return new List<SectionLine>(_sectionLines);
        }

    }

    //public class DrawingHandler
    //{
    //    private bool lastPointWasRightClick = false;
    //    private Panel targetPanel;
    //    private List<Point> points;

    //    public bool DrawSectionLinesMode { get; set; }

    //    public DrawingHandler(Panel panel)
    //    {
    //        this.targetPanel = panel;
    //        this.points = new List<Point>();

    //        this.targetPanel.MouseDown += Panel_MouseDown;
    //        this.targetPanel.Paint += Panel_Paint;
    //    }

    //    //private void Panel_MouseDown(object sender, MouseEventArgs e)
    //    //{
    //    //    if (DrawSectionLinesMode && e.Button == MouseButtons.Left)
    //    //    {
    //    //        points.Add(e.Location);
    //    //        targetPanel.Invalidate();
    //    //    }
    //    //}
    //    private void Panel_MouseDown(object sender, MouseEventArgs e)
    //    {
    //        if (DrawSectionLinesMode)
    //        {
    //            if (e.Button == MouseButtons.Left)
    //            {
    //                if (lastPointWasRightClick)
    //                {
    //                    // If last point was a right-click, clear the last point added
    //                    points.RemoveAt(points.Count - 1);
    //                    lastPointWasRightClick = false;
    //                }
    //                points.Add(e.Location);
    //                targetPanel.Invalidate();
    //            }
    //            else if (e.Button == MouseButtons.Right)
    //            {
    //                lastPointWasRightClick = true;
    //                points.Add(e.Location); // This is a breakpoint, and might be removed later
    //                targetPanel.Invalidate();
    //            }
    //        }
    //    }


    //    private void Panel_Paint(object sender, PaintEventArgs e)
    //    {
    //        if (!DrawSectionLinesMode || points.Count == 0)
    //            return;

    //        using (Pen pen = new Pen(Color.Black))
    //        using (Brush brush = new SolidBrush(Color.Black))
    //        {
    //            for (int i = 0; i < points.Count - 1; i++)
    //            {
    //                e.Graphics.DrawLine(pen, points[i], points[i + 1]);
    //            }

    //            foreach (Point point in points)
    //            {
    //                e.Graphics.FillEllipse(brush, point.X - 2, point.Y - 2, 5, 5);
    //            }
    //        }
    //    }

    //    public void ClearLines()
    //    {
    //        points.Clear();
    //        targetPanel.Invalidate();
    //    }
    //    public void UndoLastPoint()
    //    {
    //        if (points.Count > 0)
    //        {
    //            points.RemoveAt(points.Count - 1);
    //            targetPanel.Invalidate();
    //        }
    //    }

    //}

}
