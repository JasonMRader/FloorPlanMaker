using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class IntruderBox : AdjustmentBox
    {
       
        public IntruderBox(SectionBoarders sectionBoarders, SectionBoarders intrudingBoarders, Rectangle rectangle)
        {
            this.SectionBoarders = sectionBoarders;
            this.OtherSectionsBoarders = intrudingBoarders;
            CreateEdgesForRectangle(rectangle);
        }
        
        public void RemoveIntruderBoxFromBoundingBox()
        {
            // 1. Identify overlapping edges
            var overlappingEdges = new List<Edge>();
            foreach (var edge in SectionBoarders.Edges)
            {
                //if (EdgeOverlapsWithIntruderBox(edge))
                //{
                //    overlappingEdges.Add(edge);
                //}
            }

            // 2. Remove overlapping edges
            foreach (var edge in overlappingEdges)
            {
                SectionBoarders.Edges.Remove(edge);
                // Also remove from specific collections like TopEdges, BottomEdges, etc., as needed
            }

            // 3. Add new edges if necessary
            // This could involve complex geometric calculations to find intersection points
            // and create new edges that represent the modified boundary of the SectionBoarders.

            // 4. Adjust nodes
            // Update the nodes in SectionBoarders to reflect the new shape.
        }
        private void RemoveRightEdgeOverLap()
        {
            if(EdgesOverlap(this.RightEdge, SectionBoarders.BoundingBoxRightEdge))
            {

            }
        }
        private bool EdgesOverlap(Edge edge1, Edge edge2)
        {
            if (edge1.isHorizontal && edge2.isHorizontal)
            {
                // Check if they are on the same Y level
                if (edge1.StartNode.Y == edge2.StartNode.Y)
                {
                    // Check if X coordinates intersect
                    return edge1.StartNode.X < edge2.EndNode.X && edge1.EndNode.X > edge2.StartNode.X;
                }
            }
            else if (edge1.isVertical && edge2.isVertical)
            {
                // Check if they are on the same X level
                if (edge1.StartNode.X == edge2.StartNode.X)
                {
                    // Check if Y coordinates intersect
                    return edge1.StartNode.Y < edge2.EndNode.Y && edge1.EndNode.Y > edge2.StartNode.Y;
                }
            }

            // Return false if edges are not both horizontal or both vertical
            return false;
        }

        
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
