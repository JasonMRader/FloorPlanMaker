using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            SetUnblockedRightSides();
            SetUnblockedLeftSides();
            SetUnblockedBottomSides();
            SetUnblockedTopSides();
        }
        public List<IntruderBox> IntruderBoxes { get; set; } = new List<IntruderBox>();        
        public List<Edge> Edges { get; set; } = new List<Edge>();
        
        public List<Edge> TopEdges { get; set; } = new List<Edge>();
        public List<Edge> BottomEdges { get; set; } = new List<Edge>();
        public List<Edge> LeftEdges { get; set; } = new List<Edge>();
        public List<Edge> RightEdges { get; set; } = new List<Edge>();
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Edge> UnblockedRightEdges { get; set; } = new List<Edge>();
        public List<Edge> UnblockedLeftEdges { get; set; } = new List<Edge>();
        public List<Edge> UnblockedTopEdges { get; set; } = new List<Edge>();
        public List<Edge> UnblockedBottomEdges { get; set; } = new List<Edge>();
        public List<Edge> TopEdgeBoarders { get; set; } = new List<Edge>();
        public List<Edge>RightEdgeBoarders { get; set; } = new List<Edge>();
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
    
       
        public void SetUnblockedRightSides()
        {
            

            foreach (Table table in this.Section.Tables)
            {
                bool hasTableToRight = false;

                foreach (Table otherTable in this.Section.Tables)
                {
                    if (otherTable == table)
                        continue;

                    bool isDirectlyToRight = otherTable.Left >= table.Right;
                    bool isVerticallyOverlapping = (otherTable.Top < table.Bottom) && (otherTable.Bottom > table.Top);

                    if (isDirectlyToRight && isVerticallyOverlapping)
                    {
                        hasTableToRight = true;
                        break; // No need to check other tables, as we found a table to the right
                    }
                }

                if (!hasTableToRight) // Add an edge only if there's no table directly to the right
                {
                    Node topRightNode = new Node(table.Right, table.Top, Section);
                    Node bottomRightNode = new Node(table.Right, table.Bottom, Section);
                    UnblockedRightEdges.Add(new Edge(topRightNode, bottomRightNode));
                }
            }
            ExtendRightEdges();

            
        }
        public void RemoveUnwantedRightBoarders()
        {
            List<Edge> filteredEdges = new List<Edge>();

            foreach (Edge edge in RightEdgeBoarders)
            {
                bool isRightOfAnotherEdge = false;
                foreach (Edge edgeCompared in TopEdgeBoarders)
                {
                    if (edgeCompared == edge)
                        continue;
                   // bool isDirectlyToRight = edge.VerticleEdgeXPosition >= edgeCompared.VerticleEdgeXPosition;
                   // bool isHorizontallyOverlapping = (edgeCompared.VerticleEdgeBottomY > edge.VerticleEdgeTopY) && (edgeCompared.VerticleEdgeTopY < edge.VerticleEdgeBottomY);
                    bool isToLeftOfEdgeCompared = edge.VerticleEdgeXPosition < edgeCompared.VerticleEdgeXPosition;

                    // Check for vertical overlap
                    bool isVerticallyOverlapping = (edge.VerticleEdgeBottomY > edgeCompared.VerticleEdgeTopY) && (edge.VerticleEdgeTopY < edgeCompared.VerticleEdgeBottomY);

                    if (isToLeftOfEdgeCompared && isVerticallyOverlapping)
                    {
                        isRightOfAnotherEdge = true;
                        break; // Found a table above, no need to check further
                    }

                }
                if (!isRightOfAnotherEdge)
                {
                    filteredEdges.Add(edge);
                }
            }
            RightEdgeBoarders = filteredEdges;
        }
        public void SetUnblockedLeftSides()
        {
            

            foreach (Table table in this.Section.Tables)
            {
                bool hasTableToLeft = false;

                foreach (Table otherTable in this.Section.Tables)
                {
                    if (otherTable == table)
                        continue;

                    bool isDirectlyToLeft = otherTable.Right <= table.Left;
                    bool isVerticallyOverlapping = (otherTable.Top < table.Bottom) && (otherTable.Bottom > table.Top);

                    if (isDirectlyToLeft && isVerticallyOverlapping)
                    {
                        hasTableToLeft = true;
                        break; // No need to check other tables, as we found a table to the right
                    }
                }

                if (!hasTableToLeft) // Add an edge only if there's no table directly to the right
                {
                    Node topLeftNode = new Node(table.Left, table.Top, Section);
                    Node bottomLeftNode = new Node(table.Left, table.Bottom, Section);
                    UnblockedLeftEdges.Add(new Edge(topLeftNode, bottomLeftNode));
                }
            }
            ExtendLeftEdges();
           
        }
        public void SetUnblockedTopSides()
        {
            foreach (Table table in this.Section.Tables)
            {
                bool hasTableAbove = false;

                foreach (Table otherTable in this.Section.Tables)
                {
                    if (otherTable == table)
                        continue;

                    bool isDirectlyAbove = otherTable.Bottom <= table.Top;
                    bool isHorizontallyOverlapping = (otherTable.Left < table.Right) && (otherTable.Right > table.Left);

                    if (isDirectlyAbove && isHorizontallyOverlapping)
                    {
                        hasTableAbove = true;
                        break; // Found a table above, no need to check further
                    }
                }

                if (!hasTableAbove) // Add an edge only if there's no table directly above
                {
                    Node topLeftNode = new Node(table.Left, table.Top, Section);
                    Node topRightNode = new Node(table.Right, table.Top, Section);
                    UnblockedTopEdges.Add(new Edge(topLeftNode, topRightNode));
                }
            }
            ExtendTopEdges();
        }
        public void RemoveUnwantedTopBoarders()
        {
            List<Edge> filteredEdges = new List<Edge>();    
            
            foreach(Edge edge in TopEdgeBoarders)
            {
                bool isAboveAnotherEdge = false;
                foreach(Edge edgeCompared in TopEdgeBoarders)
                {
                    if (edgeCompared == edge)
                        continue;
                    bool isDirectlyAbove = edge.HorizontalEdgeYPosition <= edgeCompared.HorizontalEdgeYPosition;
                    bool isHorizontallyOverlapping = (edgeCompared.HorizontalEdgeXLeft < edge.HorizontalEdgeXRight) && (edgeCompared.HorizontalEdgeXRight > edge.HorizontalEdgeXLeft);

                    if (isDirectlyAbove && isHorizontallyOverlapping)
                    {
                        isAboveAnotherEdge = true;
                        break; // Found a table above, no need to check further
                    }                  

                }
                if (!isAboveAnotherEdge)
                {
                    filteredEdges.Add(edge);
                }
            }
            TopEdgeBoarders = filteredEdges;
        }
        public void SetUnblockedBottomSides()
        {
            foreach (Table table in this.Section.Tables)
            {
                bool hasTableBelow = false;

                foreach (Table otherTable in this.Section.Tables)
                {
                    if (otherTable == table)
                        continue;

                    bool isDirectlyBelow = otherTable.Top >= table.Bottom;
                    bool isHorizontallyOverlapping = (otherTable.Left < table.Right) && (otherTable.Right > table.Left);

                    if (isDirectlyBelow && isHorizontallyOverlapping)
                    {
                        hasTableBelow = true;
                        break; // Found a table below, no need to check further
                    }
                }

                if (!hasTableBelow) // Add an edge only if there's no table directly below
                {
                    Node bottomLeftNode = new Node(table.Left, table.Bottom, Section);
                    Node bottomRightNode = new Node(table.Right, table.Bottom, Section);
                    UnblockedBottomEdges.Add(new Edge(bottomRightNode,bottomLeftNode));
                    
                }
            }
            ExtendBottomEdges();
        }
        private void ExtendTopEdges()
        {
            UnblockedTopEdges = UnblockedTopEdges.OrderBy(e => e.HorizontalEdgeXLeft).ToList();
            for(int i = 0; i < UnblockedTopEdges.Count-1; i++)
            {
                if (UnblockedTopEdges[i].HorizontalEdgeYPosition >= UnblockedTopEdges[i + 1].HorizontalEdgeYPosition)
                {
                    UnblockedTopEdges[i].ExtendHorizontalEdge(UnblockedTopEdges[i].HorizontalEdgeXLeft, UnblockedTopEdges[i+1].HorizontalEdgeXLeft);
                }
                if(UnblockedTopEdges[i+1].HorizontalEdgeYPosition >= UnblockedTopEdges[i].HorizontalEdgeYPosition)
                {
                    UnblockedTopEdges[i+1].ExtendHorizontalEdge(UnblockedTopEdges[i].HorizontalEdgeXRight, UnblockedTopEdges[i+1].HorizontalEdgeXRight);
                }
            }
        }
        private void ExtendBottomEdges()
        {
            UnblockedBottomEdges = UnblockedBottomEdges.OrderByDescending(e => e.HorizontalEdgeXLeft).ToList();
            for (int i = 0; i < UnblockedBottomEdges.Count - 1; i++)
            {
                if (UnblockedBottomEdges[i].HorizontalEdgeYPosition >= UnblockedBottomEdges[i + 1].HorizontalEdgeYPosition)
                {
                    UnblockedBottomEdges[i].ExtendHorizontalEdge(UnblockedBottomEdges[i + 1].HorizontalEdgeXRight, UnblockedBottomEdges[i].HorizontalEdgeXRight);
                }
                if (UnblockedBottomEdges[i+1].HorizontalEdgeYPosition >= UnblockedBottomEdges[i].HorizontalEdgeYPosition)
                {
                    UnblockedBottomEdges[i+1].ExtendHorizontalEdge(UnblockedBottomEdges[i + 1].HorizontalEdgeXLeft, UnblockedBottomEdges[i].HorizontalEdgeXLeft);
                }

            }
        }
        private void ExtendRightEdges()
        {
            UnblockedRightEdges = UnblockedRightEdges.OrderBy(e => e.VerticleEdgeTopY).ToList();
            for (int i = 0; i < UnblockedRightEdges.Count - 1; i++) 
            {
                if (UnblockedRightEdges[i].VerticleEdgeXPosition <= UnblockedRightEdges[i + 1].VerticleEdgeXPosition)
                {
                    UnblockedRightEdges[i].ExtendVerticalEdge(UnblockedRightEdges[i].VerticleEdgeTopY, UnblockedRightEdges[i + 1].VerticleEdgeTopY);
                }
                if (UnblockedRightEdges[i + 1].VerticleEdgeXPosition <= UnblockedRightEdges[i].VerticleEdgeXPosition)
                {
                    UnblockedRightEdges[i + 1].ExtendVerticalEdge(UnblockedRightEdges[i].VerticleEdgeBottomY, UnblockedRightEdges[i + 1].VerticleEdgeBottomY);
                }
            }
        }
        private void ExtendLeftEdges()
        {
            UnblockedLeftEdges = UnblockedLeftEdges.OrderBy(e => e.VerticleEdgeTopY).ToList();
            for (int i = 0; i < UnblockedLeftEdges.Count - 1; i++)
            {
                if (UnblockedLeftEdges[i].VerticleEdgeXPosition <= UnblockedLeftEdges[i + 1].VerticleEdgeXPosition)
                {
                    UnblockedLeftEdges[i].ExtendVerticalEdge(UnblockedLeftEdges[i].VerticleEdgeTopY, UnblockedLeftEdges[i+1].VerticleEdgeTopY);
                }
                if (UnblockedLeftEdges[i + 1].VerticleEdgeXPosition <= UnblockedLeftEdges[i].VerticleEdgeXPosition)
                {
                    UnblockedLeftEdges[i + 1].ExtendVerticalEdge(UnblockedLeftEdges[i].VerticleEdgeBottomY, UnblockedLeftEdges[i+1].VerticleEdgeBottomY);
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
            DrawEdgesFromNodes();
             
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
