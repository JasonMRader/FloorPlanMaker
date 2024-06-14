using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class FloorplanLine
    {
        public FloorplanLine() { }
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int EndX { get; set; }
        public int EndY { get; set; }

        public Point StartPoint
        {
            get => new Point(StartX, StartY);
            set
            {
                StartX = value.X;
                StartY = value.Y;
            }
        }

        public Point EndPoint
        {
            get => new Point(EndX, EndY);
            set
            {
                EndX = value.X;
                EndY = value.Y;
            }
        }

        public Color LineColor { get; set; } = Color.Black;
        public float LineThickness { get; set; } = 2.0f;

        public FloorplanLine(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }

}
