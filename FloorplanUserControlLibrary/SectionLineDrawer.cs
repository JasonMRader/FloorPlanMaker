﻿using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
   
    public class SectionLineDrawer
    {
        public float LineThickness { get; set; } = 2.0f; // Default line thickness

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
                    using (Pen pen = new Pen(edge.Section.Color, LineThickness))
                    //using (Pen pen = new Pen(Color.Black, LineThickness))
                    {
                        Point startPoint = new Point(edge.StartNode.X-3, edge.StartNode.Y);
                        Point endPoint = new Point(edge.EndNode.X-3, edge.EndNode.Y);
                        graphics.DrawLine(pen, startPoint, endPoint);
                    }
                }
            }

            return bitmap;
        }
    }

}
