using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class IntruderBox
    {
        public SectionBoarders SectionBoarders { get; set; }
        public SectionBoarders IntruderSectionBoarders { get; set; }
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Edge> Edges { get; set; } = new List<Edge> { };
        public Edge RightEdge { get; set; }
        public Edge LeftEdge { get; set; }
        public Edge TopEdge { get; set; }
        public Edge BottomEdge { get; set; }
        public IntruderBox(SectionBoarders sectionBoarders, SectionBoarders intrudingBoarders, Rectangle rectangle)
        {
            this.SectionBoarders = sectionBoarders;
            this.IntruderSectionBoarders = intrudingBoarders;
            CreateEdgesForRectangle(rectangle);
        }
        private void CreateEdgesForRectangle(Rectangle rect)
        {
            Node topLeft = new Node(rect.Left, rect.Top, this.SectionBoarders.Section);
            Node topRight = new Node(rect.Right, rect.Top, this.SectionBoarders.Section);
            Node bottomRight = new Node(rect.Right, rect.Bottom, this.SectionBoarders.Section);
            Node bottomLeft = new Node(rect.Left, rect.Bottom, this.SectionBoarders.Section);

            this.Nodes.Add(topLeft); this.Nodes.Add(topRight); this.Nodes.Add(bottomRight); this.Nodes.Add(bottomLeft);
            this.TopEdge = new Edge(topLeft, topRight, Edge.Boarder.Top);
            this.RightEdge = new Edge(topRight, bottomRight, Edge.Boarder.Right);
            this.BottomEdge = new Edge(bottomRight, bottomLeft, Edge.Boarder.Bottom);
            this.LeftEdge = new Edge(bottomLeft, topLeft, Edge.Boarder.Left);
            List<Edge> edges = new List<Edge>
            {
                TopEdge, RightEdge, BottomEdge, LeftEdge               
            };

            this.Edges = edges; 
        }
        //public void RemoveIntruderBoxFromBoundingBox()
        //{
        //    // 1. Identify overlapping edges
        //    var overlappingEdges = new List<Edge>();
        //    foreach (var edge in SectionBoarders.Edges)
        //    {
        //        if (EdgeOverlapsWithIntruderBox(edge))
        //        {
        //            overlappingEdges.Add(edge);
        //        }
        //    }

        //    // 2. Remove overlapping edges
        //    foreach (var edge in overlappingEdges)
        //    {
        //        SectionBoarders.Edges.Remove(edge);
        //        // Also remove from specific collections like TopEdges, BottomEdges, etc., as needed
        //    }

        //    // 3. Add new edges if necessary
        //    // This could involve complex geometric calculations to find intersection points
        //    // and create new edges that represent the modified boundary of the SectionBoarders.

        //    // 4. Adjust nodes
        //    // Update the nodes in SectionBoarders to reflect the new shape.
        //}

        //private bool EdgeOverlapsWithIntruderBox(Edge edge)
        //{
        //    // Implement logic to determine if the edge overlaps with the IntruderBox.
        //    // This might involve checking if any part of the edge is within the rectangle
        //    // defined by the IntruderBox.
        //}
        //public bool DoesIntruderBoxRightEdgeOverlap(SectionBoarders sectionBoarders, IntruderBox intruderBox)
        //{
        //    if (SectionBoarders.RightEdge.StartNode.X == this.RightEdge.StartNode.X)
        //    {

        //    }
        //    // Identifying the right edge of the IntruderBox
        //    Node intruderTopRight = new Node(intruderBox.Rectangle.Right, intruderBox.Rectangle.Top);
        //    Node intruderBottomRight = new Node(intruderBox.Rectangle.Right, intruderBox.Rectangle.Bottom);

        //    // Identifying the right edge of the SectionBoarders
        //    Edge sectionRightEdge = sectionBoarders.RightEdge;

        //    // Check if the IntruderBox's right edge is within the vertical bounds of the SectionBoarders' right edge
        //    return IsNodeWithinVerticalBounds(intruderTopRight, sectionRightEdge) ||
        //           IsNodeWithinVerticalBounds(intruderBottomRight, sectionRightEdge);
        //}

        //private bool IsNodeWithinVerticalBounds(Node node, Edge edge)
        //{
        //    // Assuming the edge is vertical. Modify as needed for horizontal or slanted edges.
        //    int minY = Math.Min(edge.StartNode.Y, edge.EndNode.Y);
        //    int maxY = Math.Max(edge.StartNode.Y, edge.EndNode.Y);

        //    return node.Y >= minY && node.Y <= maxY;
        //}


    }
}
