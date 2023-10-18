using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ConvexHull
    {
        public static List<TablePoint> ComputeConvexHull(List<TablePoint> points)
        {
            TablePoint anchor = points.First();

            // Find the bottom-most point
            foreach (var point in points)
            {
                if (point.Y < anchor.Y || (point.Y == anchor.Y && point.X < anchor.X))
                {
                    anchor = point;
                }
            }

            // Sort by polar angle
            var orderedPoints = points.OrderBy(p => Math.Atan2(p.Y - anchor.Y, p.X - anchor.X)).ToList();

            var hull = new Stack<TablePoint>();
            hull.Push(orderedPoints[0]);
            hull.Push(orderedPoints[1]);

            for (int i = 2; i < orderedPoints.Count; i++)
            {
                TablePoint top = hull.Pop();

                while (CrossProduct(orderedPoints[i], top, hull.Peek()) <= 0)
                {
                    top = hull.Pop();
                }

                hull.Push(top);
                hull.Push(orderedPoints[i]);
            }

            return hull.ToList();
        }

        private static double CrossProduct(TablePoint a, TablePoint b, TablePoint c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }
        }
    }
