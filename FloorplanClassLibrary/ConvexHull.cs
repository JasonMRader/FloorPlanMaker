using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace FloorplanClassLibrary
{
    public class ConvexHull
    {
        public static List<Point> GetConvexHull(List<Point> points)
        {
            if (points.Count <= 3) return points;

            // Find the pivot (point with lowest Y - or leftmost in case of tie)
            Point pivot = points[0];
            foreach (var point in points)
            {
                if (point.Y < pivot.Y || (point.Y == pivot.Y && point.X < pivot.X))
                    pivot = point;
            }

            // Sort points by polar angle with pivot
            points.Sort((a, b) => PolarAngleCompare(a, b, pivot));

            Stack<Point> hull = new Stack<Point>();
            hull.Push(points[0]);
            hull.Push(points[1]);

            for (int i = 2; i < points.Count; i++)
            {
                Point top = hull.Pop();
                while (hull.Count != 0 && CrossProduct(hull.Peek(), top, points[i]) <= 0)
                    top = hull.Pop();
                hull.Push(top);
                hull.Push(points[i]);
            }

            return new List<Point>(hull);
        }
        public static List<Node> GetBoundingBox(Section section)
        {
            if (section.Tables.Count == 0)
                return new List<Node>();

            int minX = int.MaxValue, minY = int.MaxValue;
            int maxX = int.MinValue, maxY = int.MinValue;

            foreach (var table in section.Tables)
            {
                foreach (var point in table.GetCornerPoints())
                {
                    if (point.X < minX) minX = point.X;
                    if (point.Y < minY) minY = point.Y;
                    if (point.X > maxX) maxX = point.X;
                    if (point.Y > maxY) maxY = point.Y;
                }
            }

            Node topLeft = new Node(minX, minY, section, true, false);
            Node topRight = new Node(maxX, minY, section, true, true);
            Node bottomRight = new Node(maxX, maxY, section, false, true);
            Node bottomLeft = new Node(minX, maxY, section, false, false);

            // Optionally, you can set the parent-child relationship between the nodes
            topLeft.Parent = bottomLeft;
            topLeft.Child = topRight;
            topRight.Parent = topLeft;
            topRight.Child = bottomRight;
            bottomRight.Parent = topRight;
            bottomRight.Child = bottomLeft;
            bottomLeft.Parent = bottomRight;
            bottomLeft.Child = topLeft;

            return new List<Node> { topLeft, topRight, bottomRight, bottomLeft };
        }
        private static int PolarAngleCompare(Point a, Point b, Point pivot)
        {
            int order = CrossProduct(pivot, a, b);
            if (order == 0)
                return Distance(pivot, a) - Distance(pivot, b);
            return order > 0 ? -1 : 1;
        }

        private static int CrossProduct(Point a, Point b, Point c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }

        private static int Distance(Point a, Point b)
        {
            return (b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y);
        }

    }
}
