using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ConvexHull
    {

        public static List<TablePoint> OLDComputeConvexHull(List<TablePoint> points)
        {
            if (points.Count < 3)
                return points; // Can't form a polygon

            // Find the bottom-most point
            TablePoint anchor = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();

            // Sort the rest based on polar angle relative to anchor
            var orderedPoints = points.OrderBy(p => Math.Atan2(p.Y - anchor.Y, p.X - anchor.X)).ToList();

            List<TablePoint> hull = new List<TablePoint>
            {
                orderedPoints[0],
                orderedPoints[1]
            };

            for (int i = 2; i < orderedPoints.Count; i++)
            {
                while (hull.Count > 1 && CrossProduct(hull[hull.Count - 2], hull[hull.Count - 1], orderedPoints[i]) <= 0)
                {
                    hull.RemoveAt(hull.Count - 1);
                }

                hull.Add(orderedPoints[i]);
            }

            return hull;
        }
        public static List<TablePoint> ComputeConvexHull(List<TablePoint> points)
        {
            if (points.Count < 3)
                return points; // Can't form a polygon

            // Find the bottom-most point
            TablePoint anchor = points.OrderBy(p => p.Y).ThenBy(p => p.X).First();

            // Sort the rest based on polar angle relative to anchor
            var orderedPoints = points.OrderBy(p => Math.Atan2(p.Y - anchor.Y, p.X - anchor.X)).ToList();

            List<TablePoint> hull = new List<TablePoint>
            {
                orderedPoints[0],
                orderedPoints[1]
            };

            int allowedRightTurns = 2; // Number of allowed right turns
            List<TablePoint> backupHull = null;
            for (int i = 2; i < orderedPoints.Count; i++)
            {
                while (hull.Count > 1 && CrossProduct(hull[hull.Count - 2], hull[hull.Count - 1], orderedPoints[i]) <= 0)
                {
                    if (allowedRightTurns > 0)
                    {
                        if (backupHull == null)
                        {
                            backupHull = new List<TablePoint>(hull); // Backup the current hull state
                        }
                        allowedRightTurns--;
                        break;  // Don't remove the point, just break out and continue
                    }
                    else
                    {
                        hull.RemoveAt(hull.Count - 1);
                    }
                }
                hull.Add(orderedPoints[i]);

                // If we've made right turns, check if all points are inside the shape
                if (backupHull != null && allowedRightTurns < 2 && !AllPointsInsideHull(points, hull))
                {
                    // Restore the hull to the state before making the right turns and reset allowed right turns
                    hull = backupHull;
                    allowedRightTurns = 2;
                    backupHull = null;
                }
            }
            return hull;
        
        }


        private static double CrossProduct(TablePoint a, TablePoint b, TablePoint c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }
        private static bool AllPointsInsideHull(List<TablePoint> points, List<TablePoint> hull)
        {
            foreach (var point in points)
            {
                if (!IsPointInsidePolygon(point, hull))
                {
                    return false;
                }
            }
            return true;
        }
        private static bool IsPointInsidePolygon(TablePoint p, List<TablePoint> polygon)
        {
            bool isInside = false;

            // Loop through each pair of vertices
            int j = polygon.Count - 1;
            for (int i = 0; i < polygon.Count; i++)
            {
                // If the point lies on an edge, it's considered inside
                if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                    p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    isInside = !isInside;
                }
                j = i;
            }

            return isInside;
        }

    }
}
