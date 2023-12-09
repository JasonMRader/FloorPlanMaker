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
        public List<IntruderBox> IntruderBoxes { get; set; } = new List<IntruderBox>();        
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
        public void RemoveAllRightOverLapps()
        {
            foreach(IntruderBox box in IntruderBoxes)
            {
                ModifyVerticleEdge(box.RightEdge, this.RightEdge, box);
            }
        }
        public void ModifyVerticleEdge(Edge portionToRemove, Edge modifiedEdge, IntruderBox intruderBox)
        {
            if(portionToRemove.isHorizontal || modifiedEdge.isHorizontal) { return; }
            if(portionToRemove.VerticleEdgeX() != modifiedEdge.VerticleEdgeX()) { return; }
            if(!(portionToRemove.VerticleEdgeStartY() > modifiedEdge.VerticleEdgeStartY() && portionToRemove.VerticleEdgeStartY() < modifiedEdge.VerticleEdgeEndY()) &&
                !(portionToRemove.VerticleEdgeEndY() > modifiedEdge.VerticleEdgeStartY() && portionToRemove.VerticleEdgeEndY() < modifiedEdge.VerticleEdgeEndY()))
            { return; }
            if(modifiedEdge == RightEdge)
            {
                this.Edges.Remove(portionToRemove);
                this.Edges.Remove(modifiedEdge);
                Edge edge = new Edge(intruderBox.LeftEdge.StartNode, intruderBox.LeftEdge.EndNode, Edge.Boarder.Right);
                this.Edges.Add(edge);
                if (modifiedEdge.VerticleEdgeStartY() == portionToRemove.VerticleEdgeStartY() && 
                    modifiedEdge.VerticleEdgeEndY() != portionToRemove.VerticleEdgeEndY())
                {
                    Node newStart = new Node(portionToRemove.VerticleEdgeX(), portionToRemove.VerticleEdgeEndY(), Section);
                    //Node sameEnd = 
                    modifiedEdge = new Edge(newStart, modifiedEdge.EndNode, Edge.Boarder.Right );
                    this.Edges.Add(modifiedEdge);
                   
                }
                else if (modifiedEdge.VerticleEdgeEndY() == portionToRemove.VerticleEdgeEndY() &&
                    modifiedEdge.VerticleEdgeStartY() != portionToRemove.VerticleEdgeStartY())
                {
                    Node newEnd = new Node(portionToRemove.VerticleEdgeX(), portionToRemove.VerticleEdgeStartY(), Section);
                    modifiedEdge = new Edge(modifiedEdge.StartNode, newEnd, Edge.Boarder.Right);
                    this.Edges.Add(modifiedEdge);
                }
                else if (modifiedEdge.VerticleEdgeEndY() == portionToRemove.VerticleEdgeEndY() &&
                    modifiedEdge.VerticleEdgeStartY() == portionToRemove.VerticleEdgeStartY())
                {
                    
                }
                else
                {
                    Node newStart1 = new Node(modifiedEdge.VerticleEdgeX(), modifiedEdge.VerticleEdgeStartY(), Section);
                    Node newEnd1 = new Node(portionToRemove.VerticleEdgeX(), portionToRemove.VerticleEdgeStartY(), Section);
                    Node newStart2 = new Node(portionToRemove.VerticleEdgeX(), portionToRemove.VerticleEdgeEndY(), Section);
                    Node newEnd2 = new Node(modifiedEdge.VerticleEdgeX(), modifiedEdge.VerticleEdgeEndY(), Section);
                    Edge modifiedEdge1 = new Edge(newStart1, newEnd2, Edge.Boarder.Right);
                    Edge modifiedEdge2 = new Edge(newStart2, newEnd2, Edge.Boarder.Right);
                    this.Edges.Add(modifiedEdge1);
                    this.Edges.Add(modifiedEdge2);
                }
            }
        }
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
