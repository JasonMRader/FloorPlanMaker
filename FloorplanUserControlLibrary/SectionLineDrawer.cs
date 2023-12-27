using FloorplanClassLibrary;
using FloorPlanMakerUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
   
    public class SectionLineDrawer
    {
        public float LineThickness { get; set; } = 5.0f; // Default line thickness

        public SectionLineDrawer(float lineThickness)
        {
            LineThickness = lineThickness;
        }

        public Bitmap CreateEdgeBitmap(Size size, IEnumerable<Edge> edges)
        {
            var bitmap = new Bitmap(size.Width, size.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var edge in edges)
                {
                    //using (Pen pen = new Pen(edge.Section.Color, LineThickness))
                    using (Pen pen = new Pen(Color.Black, LineThickness))
                    {
                        Point startPoint = new Point(edge.StartNode.X, edge.StartNode.Y);
                        Point endPoint = new Point(edge.EndNode.X, edge.EndNode.Y);
                        graphics.DrawLine(pen, startPoint, endPoint);
                    }
                }
            }

            return bitmap;
        }
        public Bitmap CreateEdgeBitmap(Size size, IEnumerable<Edge> edges, Edge specialEdge)
        {
            var bitmap = new Bitmap(size.Width, size.Height);

            using (var graphics = Graphics.FromImage(bitmap))
            {
                foreach (var edge in edges)
                {
                    // Check if the current edge matches the special edge
                    bool isSpecialEdge = edge.StartNode == specialEdge.StartNode && edge.EndNode == specialEdge.EndNode;
                    Color lineColor = isSpecialEdge ? UITheme.HighlightColor : Color.Gray;

                    using (Pen pen = new Pen(lineColor, LineThickness))
                    {
                        Point startPoint = new Point(edge.StartNode.X , edge.StartNode.Y);
                        Point endPoint = new Point(edge.EndNode.X , edge.EndNode.Y);
                        graphics.DrawLine(pen, startPoint, endPoint);
                    }
                }
            }

            return bitmap;
        }
        public void DrawEdges(Graphics graphics, IEnumerable<Edge> edges, Edge specialEdge = null)
        {
            foreach (var edge in edges)
            {
                bool isSpecialEdge = specialEdge != null && edge.StartNode == specialEdge.StartNode && edge.EndNode == specialEdge.EndNode;
                Color lineColor = isSpecialEdge ? UITheme.HighlightColor : Color.Black;

                using (Pen pen = new Pen(lineColor, LineThickness))
                {
                    Point startPoint = new Point(edge.StartNode.X , edge.StartNode.Y);
                    Point endPoint = new Point(edge.EndNode.X , edge.EndNode.Y);
                    graphics.DrawLine(pen, startPoint, endPoint);
                }
            }
        }

    }

}
