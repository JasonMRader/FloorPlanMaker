using MIConvexHull;
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
    }
}
