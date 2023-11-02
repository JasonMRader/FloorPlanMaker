using FloorplanClassLibrary;
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
        public override string ToString()
        {
            return _startPoint.ToString() + _endPoint.ToString();
        }
        public int ID { get; set; }
        private Point _startPoint;
        private Point _endPoint;
        public Color LineColor { get; set; } = Color.Black;
        public float LineThickness { get; set; } = 15f;
        public Section Section = new Section();
        public enum BorderEdge
        {
            None,
            Top,
            Right,
            Bottom,
            Left
        }
        public BorderEdge Edge { get; set; }
        public SectionLine()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }
        public SectionLine(Node startNode, Node endNode)
        {
            this.StartPoint = new Point(startNode.X, startNode.Y);
            this.EndPoint = new Point(endNode.X, endNode.Y);

            if (startNode.Y == endNode.Y)
            {
                if (startNode.X < endNode.X)
                {
                    this.StartPoint = new Point(startNode.X, startNode.Y);
                    this.EndPoint = new Point(endNode.X, endNode.Y);
                }
                else
                {
                    this.EndPoint = new Point(startNode.X, startNode.Y);
                    this.StartPoint = new Point(endNode.X, endNode.Y);
                }
            }
            if (startNode.X == endNode.X)
            {
                if (startNode.Y < endNode.Y)
                {
                    this.StartPoint = new Point(startNode.X, startNode.Y);
                    this.EndPoint = new Point(endNode.X, endNode.Y);
                }
                else
                {
                    this.EndPoint = new Point(startNode.X, startNode.Y);
                    this.StartPoint = new Point(endNode.X, endNode.Y);
                }
            }

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }

        public SectionLine(int startX, int startY, int endX, int endY)
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

            
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }
        public SectionLine(int startX, int startY, int endX, int endY, Section section)
        {
            if (startY == endY)
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
            this.Section = section;


            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }
        public SectionLine(int startX, int startY, int endX, int endY, float thickness)
        {
            if (startY == endY)
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
        public SectionLine(Point firstPoint, Point secondPoint)
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

            
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                          ControlStyles.UserPaint |
                          ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
            this.Invalidate();
        }
        public SectionLine(Point firstPoint, Point secondPoint, Section section)
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
                    this.StartPoint = firstPoint;
                    this.EndPoint = secondPoint;

                }
                else
                {
                    this.EndPoint = firstPoint;
                    this.StartPoint = secondPoint;
                }
            }
            this.Section = section;


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

            // Use LineThickness and LineColor when creating the Pen
            using (Pen pen = new Pen(LineColor, LineThickness))
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


