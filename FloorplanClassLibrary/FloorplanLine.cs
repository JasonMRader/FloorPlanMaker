using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class FloorplanLine
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Color LineColor { get; set; } = Color.Black;
        public float LineThickness { get; set; } = 2.0f;

        public FloorplanLine(Point startPoint, Point endPoint)
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }

}
