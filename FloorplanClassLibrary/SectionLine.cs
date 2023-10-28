using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class SectionLine : Control
    {
        public int ID { get; set; }
        private Point _startPoint;
        private Point _endPoint;
        public float LineThickness { get; set; } = 15f;
        public SectionLine()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }
        public SectionLine(int startX, int startY, int endX, int endY, float thickness)
        {
            if(startY == endY)
            {
                if (startX < endX)
                {
                    this.StartPoint = new Point(startX, startY);
                    this.EndPoint = new Point(endX, endY);
                }
                else
                {
                    this.EndPoint = new Point(startX, startY);
                    this.StartPoint = new Point(endX, endY);
                }
            }
            if (startX == endX)
            {
                if (startY < endY)
                {
                    this.StartPoint = new Point(startX, startY);
                    this.EndPoint = new Point(endX, endY);
                }
                else
                {
                    this.EndPoint = new Point(startX, startY);
                    this.StartPoint = new Point(endX, endY);
                }
            }

            this.LineThickness = thickness;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }
        public SectionLine(Point firstPoint, Point secondPoint, float thickness)
        {
            if (firstPoint.Y == secondPoint.Y)
            {
                if (firstPoint.X < secondPoint.X)
                {
                    this.StartPoint = firstPoint;
                    this.EndPoint = secondPoint;
                }
                else
                {
                    this.EndPoint = firstPoint;
                    this.StartPoint = secondPoint;
                }
            }
            if (firstPoint.X == secondPoint.X)
            {
                if (firstPoint.Y < secondPoint.Y)
                {
                    this.StartPoint =firstPoint;
                    this.EndPoint = secondPoint;
                   
                }
                else
                {
                    this.EndPoint = firstPoint;
                    this.StartPoint = secondPoint;
                }
            }

            this.LineThickness = thickness;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }
        public Point StartPoint
        {
            get { return _startPoint; }
            set { _startPoint = value; SetBounds(); }
        }
        public Point EndPoint
        {
            get { return _endPoint; }
            set { _endPoint = value; SetBounds(); }
        }
        public string? currentTableNumber { get; set; }
        public string? adjacentTableNumber { get; set; }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            // Use LineThickness when creating the Pen
            using (Pen pen = new Pen(Color.Black, LineThickness))
            {
                // Convert absolute points to relative points
                var relativeStart = new Point(_startPoint.X - this.Left, _startPoint.Y - this.Top);
                var relativeEnd = new Point(_endPoint.X - this.Left, _endPoint.Y - this.Top);

                pe.Graphics.DrawLine(pen, relativeStart, relativeEnd);
            }
        }

        // Determine and set the bounding box for the line
        private void SetBounds()
        {
            int x = Math.Min(_startPoint.X, _endPoint.X);
            int y = Math.Min(_startPoint.Y, _endPoint.Y);
            int width = Math.Abs(_startPoint.X - _endPoint.X);
            int height = Math.Abs(_startPoint.Y - _endPoint.Y);

            if (width == 0) width = 1;  // Ensure it has at least a pixel to display in the case of vertical line
            if (height == 0) height = 1; // Ensure it has at least a pixel to display in the case of horizontal line

            this.SetBounds(x, y, width, height);
        }
        //public void CutLine(Point)
    }


}


