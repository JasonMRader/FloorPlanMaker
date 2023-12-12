using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace FloorplanClassLibrary
{
    public class SectionBoarders
    {
        public Section Section { get; set; }
        public SectionBoarders(Section section) 
        {
            Section = section;
            //this.Nodes = ConvexHull.GetBoundingBox(Section);
        }
        public List<IntruderBox> IntruderBoxes { get; set; } = new List<IntruderBox>();        
        public List<Edge> Edges { get; set; } = new List<Edge>();
        
        public List<Edge> TopEdges { get; set; } = new List<Edge>();
        public List<Edge> BottomEdges { get; set; } = new List<Edge>();
        public List<Edge> LeftEdges { get; set; } = new List<Edge>();
        public List<Edge> RightEdges { get; set; } = new List<Edge>();
        public List<Node> Nodes { get; set; } = new List<Node>();
        public Edge BoundingBoxLeftEdge { get; set; }
        public Edge BoundingBoxRightEdge { get; set; } //=> RightEdges.OrderByDescending(e => e.EndNode.X).FirstOrDefault();
        public Edge BoundingBoxTopEdge { get; set; } //=> TopEdges.OrderBy(e => e.StartNode.Y).FirstOrDefault();
        public Edge BoundingBoxBottomEdge { get; set; }// => BottomEdges.OrderByDescending(e => e.EndNode.Y).FirstOrDefault();
        public void RefreshEdges()
        {
            Edges.Clear();
            Edges.AddRange(TopEdges);
            Edges.AddRange(BottomEdges);
            Edges.AddRange(LeftEdges);
            Edges.AddRange(RightEdges);
        }
        public void RemoveAllRightOverLapps()
        {
            foreach (IntruderBox box in IntruderBoxes)
            {
                ModifyVerticleEdgeForIntruders(box.RightEdge, this.BoundingBoxRightEdge, box);
            }
        }
        public void ModifyVerticleEdgeForIntruders(Edge portionToRemove, Edge modifiedEdge, IntruderBox intruderBox)
        {
            bool wasRightEdge = false;
            if (modifiedEdge == BoundingBoxRightEdge)
            {
                wasRightEdge = true;
            }
            List<Edge> edgesAdded = new List<Edge>();
            if (portionToRemove.isHorizontal || modifiedEdge.isHorizontal) { return; }
            if (portionToRemove.VerticleEdgeX() != modifiedEdge.VerticleEdgeX()) { return; }
            if (!(portionToRemove.VerticleEdgeStartY() > modifiedEdge.VerticleEdgeStartY() && portionToRemove.VerticleEdgeStartY() < modifiedEdge.VerticleEdgeEndY()) &&
                !(portionToRemove.VerticleEdgeEndY() > modifiedEdge.VerticleEdgeStartY() && portionToRemove.VerticleEdgeEndY() < modifiedEdge.VerticleEdgeEndY()))
            { return; }

            this.Edges.Remove(portionToRemove);
            this.Edges.Remove(modifiedEdge);
            Edge edge = new Edge(intruderBox.LeftEdge.StartNode, intruderBox.LeftEdge.EndNode, Edge.Boarder.Right);
            this.Edges.Add(edge);
            edgesAdded.Add(edge);
            //When overlaps with top
            if (modifiedEdge.VerticleEdgeStartY() == portionToRemove.VerticleEdgeStartY() &&
                modifiedEdge.VerticleEdgeEndY() != portionToRemove.VerticleEdgeEndY())
            {
                Node newStart = new Node(portionToRemove.VerticleEdgeX(), portionToRemove.VerticleEdgeEndY(), Section);

                modifiedEdge = new Edge(newStart, modifiedEdge.EndNode, Edge.Boarder.Right);
                this.Edges.Add(modifiedEdge);
                edgesAdded.Add(modifiedEdge);
                Edge newTopEdge = Edge.CopyIntruderEdge(intruderBox.BottomEdge);
                this.Edges.Add(newTopEdge);
                AdjustEdgeForNewAddedEdge(newTopEdge);

            }
            //overlaps with bottom
            else if (modifiedEdge.VerticleEdgeEndY() == portionToRemove.VerticleEdgeEndY() &&
                modifiedEdge.VerticleEdgeStartY() != portionToRemove.VerticleEdgeStartY())
            {
                Node newEnd = new Node(portionToRemove.VerticleEdgeX(), portionToRemove.VerticleEdgeStartY(), Section);
                modifiedEdge = new Edge(modifiedEdge.StartNode, newEnd, Edge.Boarder.Right);
                this.Edges.Add(modifiedEdge);
                edgesAdded.Add(modifiedEdge);
                Edge newBottomEdge = Edge.CopyIntruderEdge(intruderBox.TopEdge);
                this.Edges.Add(newBottomEdge);
            }
            //overlaps the entire line
            else if (modifiedEdge.VerticleEdgeEndY() == portionToRemove.VerticleEdgeEndY() &&
                modifiedEdge.VerticleEdgeStartY() == portionToRemove.VerticleEdgeStartY())
            {
                //Add new Right line (INtruder LeftLine) remove top and bottom boarders where X > new right Line x
            }
            //overlaps the middle
            else
            {
                Node newStart1 = new Node(modifiedEdge.VerticleEdgeX(), modifiedEdge.VerticleEdgeStartY(), Section);
                Node newEnd1 = new Node(portionToRemove.VerticleEdgeX(), portionToRemove.VerticleEdgeStartY(), Section);
                Node newStart2 = new Node(portionToRemove.VerticleEdgeX(), portionToRemove.VerticleEdgeEndY(), Section);
                Node newEnd2 = new Node(modifiedEdge.VerticleEdgeX(), modifiedEdge.VerticleEdgeEndY(), Section);
                Edge modifiedEdge1 = new Edge(newStart1, newEnd1, Edge.Boarder.Right);
                Edge modifiedEdge2 = new Edge(newStart2, newEnd2, Edge.Boarder.Right);
                this.Edges.Add(modifiedEdge1);
                this.Edges.Add(modifiedEdge2);
                edgesAdded.Add(modifiedEdge1);
                edgesAdded.Add(modifiedEdge2);
                Edge newTopEdge = Edge.CopyIntruderEdge(intruderBox.BottomEdge);
                this.Edges.Add(newTopEdge);
                Edge newBottomEdge = Edge.CopyIntruderEdge(intruderBox.TopEdge);
                this.Edges.Add(newBottomEdge);

            }
            if (!wasRightEdge)
            {
                foreach (Edge edgeToChange in edgesAdded)
                {
                    edgeToChange.BoarderType = Edge.Boarder.Left;
                }
            }




        }
        protected void AdjustEdgeForNewAddedEdge(Edge newTopEdge)
        {
            //Add logic to remove to at same places that it was added
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
                //Edges.Add(edge);
                if (edge.BoarderType == Edge.Boarder.Top) {
                    TopEdges.Add(edge);
                    BoundingBoxTopEdge = edge;
                }
                if(edge.BoarderType == Edge.Boarder.Right) {
                    RightEdges.Add(edge);
                    BoundingBoxRightEdge = edge;
                }
                if(edge.BoarderType == Edge.Boarder.Bottom) {
                    BottomEdges.Add(edge);
                    BoundingBoxBottomEdge = edge;
                }
                if(edge.BoarderType == Edge.Boarder.Left) {
                    LeftEdges.Add(edge);
                    BoundingBoxLeftEdge = edge;
                }
            }
             
        }
        public void DrawEdgesFromNodes()
        {
            List<Edge> edges = new List<Edge>();
            for (int i = 0; i < Nodes.Count; i++)
            {
                Node parent = Nodes[i];
                Node child = Nodes[(i + 1) % Nodes.Count];
                Edge edge = new Edge(parent, child);
                if (edge.isHorizontal)
                {
                    if (parent.Y == Nodes.Min(n => n.Y))
                    {
                        edge.BoarderType = Edge.Boarder.Top;
                    }
                    else
                    {
                        edge.BoarderType = Edge.Boarder.Bottom;
                    }
                }
                else
                {
                    if (parent.X == Nodes.Min(n => n.X))
                    {
                        edge.BoarderType = Edge.Boarder.Left;
                    }
                    else
                    {
                        edge.BoarderType = Edge.Boarder.Right;
                    }
                }
                edges.Add(edge);
            }
            foreach (Edge edge in edges)
            {
                Edges.Add(edge);
                if (edge.BoarderType == Edge.Boarder.Top)
                {
                    TopEdges.Add(edge);
                    //TopEdge = edge;
                }
                if (edge.BoarderType == Edge.Boarder.Right)
                {
                    RightEdges.Add(edge);
                    //RightEdge = edge;
                }
                if (edge.BoarderType == Edge.Boarder.Bottom)
                {
                    BottomEdges.Add(edge);
                    //BottomEdge = edge;
                }
                if (edge.BoarderType == Edge.Boarder.Left)
                {
                    LeftEdges.Add(edge);
                    //LeftEdge = edge;
                }
            }
        }
        public void InsertNodesAndEdges(Node ParentNode,  Point start, Point end)
        {
            if(ParentNode.isTopNode && ParentNode.IsRightNode)
            {
                ParentNode.InsertNodeAfter(ParentNode.X, start.Y, this.Section);
                Node firstNode = ParentNode.Child;
                firstNode.InsertNodeAfter(start.X, start.Y, this.Section);
                Node secondNode = firstNode.Child;
                secondNode.InsertNodeAfter(end.X, end.Y, this.Section);
                Node thirdNode = secondNode.Child;
                thirdNode.InsertNodeAfter(ParentNode.X, thirdNode.Y, this.Section);
            }
            
        }
        public void MoveRightEdgeOut(Edge oldEdge, Point start, Point end)
        {
            Node ParentNode = oldEdge.StartNode;
            
            ParentNode.InsertNodeAfter(ParentNode.X, start.Y, this.Section);
            Node firstNode = ParentNode.Child;
            firstNode.InsertNodeAfter(start.X, start.Y, this.Section);
            Node secondNode = firstNode.Child;
            secondNode.InsertNodeAfter(end.X, end.Y, this.Section);
            Node thirdNode = secondNode.Child;
            thirdNode.InsertNodeAfter(ParentNode.X, thirdNode.Y, this.Section);
            Node fourthNode = thirdNode.Child;
            this.Nodes.Add(firstNode);
            this.Nodes.Add(secondNode);
            this.Nodes.Add(thirdNode);
            this.Nodes.Add(fourthNode);
           
            
        }
        public void MoveRightEdgeIn(Edge oldEdge, Edge edgeAdded)
        {

        }
        public void MoveLeftEdgeOut()
        {

        }
        public void MoveLeftEdgeIn()
        {

        }
        public void MoveTopEdgeOut()
        {

        }
        public void MoveTopEdgeIn()
        {

        }
        public void MoveBottomEdgeOut() 
        {

        }
        public void MoveBottomEdgeIn()
        {

        }
    }
}
