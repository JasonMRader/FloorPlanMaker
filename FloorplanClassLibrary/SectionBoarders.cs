using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SectionBoarders
    {
        public Section Section { get; set; }
        public SectionBoarders(Section section) 
        {
            Section = section;
        }
        public List<Edge> Edges { get; set; } = new List<Edge>();
        public List<Edge> TopEdges { get; set; } = new List<Edge>();
        public List<Edge> BottomEdges { get; set; } = new List<Edge>();
        public List<Edge> LeftEdges { get; set; } = new List<Edge>();
        public List<Edge> RightEdges { get; set; } = new List<Edge>();
        public List<Node> Nodes { get; set; } = new List<Node>();
        public Edge LeftEdge { get; set; }
        public Edge RightEdge { get; set; } //=> RightEdges.OrderByDescending(e => e.EndNode.X).FirstOrDefault();
        public Edge TopEdge { get; set; } //=> TopEdges.OrderBy(e => e.StartNode.Y).FirstOrDefault();
        public Edge BottomEdge { get; set; }// => BottomEdges.OrderByDescending(e => e.EndNode.Y).FirstOrDefault();
        public void SetEdgesForBoundingBox() {
            this.Nodes = ConvexHull.GetBoundingBox(Section);
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < Nodes.Count; i++) {
                Node parent = Nodes[i];
                Node child = Nodes[(i + 1) % Nodes.Count];
                Edge edge = new Edge(parent, child);              
                if (edge.isHorizontal) {
                    if (parent.Y == Nodes.Min(n => n.Y)) {
                        edge.BoarderType = Edge.Boarder.Top;
                    }
                    else {
                        edge.BoarderType = Edge.Boarder.Bottom;
                    }
                }
                else {
                    if (parent.X == Nodes.Min(n => n.X)) {            
                        edge.BoarderType = Edge.Boarder.Left;
                    }
                    else  {
                        edge.BoarderType = Edge.Boarder.Right;
                    }
                }
                edges.Add(edge);
            }
            foreach (Edge edge in edges) {
                Edges.Add(edge);
                if (edge.BoarderType == Edge.Boarder.Top) {
                    TopEdges.Add(edge);
                    TopEdge = edge;
                }
                if(edge.BoarderType == Edge.Boarder.Right) {
                    RightEdges.Add(edge);
                    RightEdge = edge;
                }
                if(edge.BoarderType == Edge.Boarder.Bottom) {
                    BottomEdges.Add(edge);
                    BottomEdge = edge;
                }
                if(edge.BoarderType == Edge.Boarder.Left) {
                    LeftEdges.Add(edge);
                    LeftEdge = edge;
                }
            }
        }


    }
}
