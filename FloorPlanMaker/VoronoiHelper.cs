using FloorplanClassLibrary;
using FloorPlanMaker;
using MIConvexHull;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public static class VoronoiHelper
    {
        public static List<(Vertex, Vertex)> ComputeVoronoiEdges(List<(double, double)> centroids)
        {
            var vertices = centroids.Select(pt => new Vertex { Position = new double[] { pt.Item1, pt.Item2 } }).ToList();

            var voronoi = VoronoiMesh.Create<Vertex, VoronoiCell>(vertices);

            // Extracting the unique edges
            var edges = new HashSet<(Vertex, Vertex)>();
            //foreach (var cell in voronoi.Cells)
            //{
            //    foreach (var edge in cell.Vertices)
            //    {
            //        var sortedEdge = edge.Position[0] < cell.Circumcenter[0] ?
            //            (edge, new Vertex { Position = cell.Circumcenter }) :
            //            (new Vertex { Position = cell.Circumcenter }, edge);

            //        edges.Add(sortedEdge);
            //    }
            //}

            return edges.ToList();
        }
        public static Coordinate GetCentroid(TableControl tableControl)
        {
            double x = 0, y = 0;

            switch (tableControl.Shape)
            {
                case Table.TableShape.Circle:
                case Table.TableShape.Square:
                    // For Circle and Square, centroid is just the center of the rectangle
                    x = tableControl.Left + tableControl.Width / 2.0;
                    y = tableControl.Top + tableControl.Height / 2.0;
                    break;

                case Table.TableShape.Diamond:
                    // For diamond, use vertices to compute centroid
                    System.Drawing.Point[] diamondPoints = {
                    new System.Drawing.Point(tableControl.Left + tableControl.Width / 2, tableControl.Top),
                    new System.Drawing.Point(tableControl.Left + tableControl.Width, tableControl.Top + tableControl.Height / 2),
                    new System.Drawing.Point(tableControl.Left + tableControl.Width / 2, tableControl.Top + tableControl.Height),
                    new System.Drawing.Point(tableControl.Left, tableControl.Top + tableControl.Height / 2)
                };

                    x = (diamondPoints[0].X + diamondPoints[1].X + diamondPoints[2].X + diamondPoints[3].X) / 4.0;
                    y = (diamondPoints[0].Y + diamondPoints[1].Y + diamondPoints[2].Y + diamondPoints[3].Y) / 4.0;
                    break;

                default:
                    throw new InvalidOperationException("Unknown table shape");
            }

            return new Coordinate(x, y);

        }
    }
}
