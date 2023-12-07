using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class FloorplanBoarderManager
    {
        public List<Section> Sections { get; set; }
        public List<Edge> Edges { get; set; } = new List<Edge>();
        public List<Edge> OverLappingEdges { get; set; } = new List<Edge> { };

        public FloorplanBoarderManager(List<Section> sections)
        {
            this.Sections = sections;
            foreach (var section in Sections)
            {
                section.SetBoarderManager();
            }
        }

        public void CalculateOverlappingSectionEdges()
        {
            // Compute bounding boxes for each section
            List<SectionBoarders> sectionBoardersList = Sections.Select(section => section.SectionBoarders).ToList();

            // Check for overlaps and create new edges
            for (int i = 0; i < sectionBoardersList.Count; i++)
            {
                for (int j = i + 1; j < sectionBoardersList.Count; j++)
                {
                    var overlappingRectangle = GetOverlappingRectangle(sectionBoardersList[i], sectionBoardersList[j]);
                    if (overlappingRectangle != null)
                    {
                        // Create edges for this overlapping rectangle
                        List<Edge> overlappingEdges = CreateEdgesForRectangle(overlappingRectangle.Value);
                        // Add these edges to the list or process as needed
                        OverLappingEdges.AddRange(overlappingEdges);
                    }
                }
            }
        }

        private List<Edge> CreateEdgesForRectangle(Rectangle rect)
        {
            Node topLeft = new Node(rect.Left, rect.Top, null);
            Node topRight = new Node(rect.Right, rect.Top, null);
            Node bottomRight = new Node(rect.Right, rect.Bottom, null);
            Node bottomLeft = new Node(rect.Left, rect.Bottom, null);

            List<Edge> edges = new List<Edge>
            {
                new Edge(topLeft, topRight),
                new Edge(topRight, bottomRight),
                new Edge(bottomRight, bottomLeft),
                new Edge(bottomLeft, topLeft)
            };

            return edges;
        }
        private Rectangle? GetOverlappingRectangle(SectionBoarders boarders1, SectionBoarders boarders2)
        {
            int left = Math.Max(boarders1.LeftEdge.StartNode.X, boarders2.LeftEdge.StartNode.X);
            int right = Math.Min(boarders1.RightEdge.StartNode.X, boarders2.RightEdge.StartNode.X);
            int top = Math.Max(boarders1.TopEdge.StartNode.Y, boarders2.TopEdge.StartNode.Y);
            int bottom = Math.Min(boarders1.BottomEdge.StartNode.Y, boarders2.BottomEdge.StartNode.Y);

            if (left < right && top < bottom)
            {
                return new Rectangle(left, top, right - left, bottom - top);
            }

            return null;
        }
        public void ModifyBordersForOverlaps()
        {
            CalculateOverlappingSectionEdges();

            foreach (Edge overlapEdge in OverLappingEdges)
            {
                foreach (Section section in Sections)
                {
                    SectionBoarders boarders = section.SectionBoarders;

                    // Check and modify the right edge of the section
                    if (overlapEdge.BoarderType == Edge.Boarder.Left && boarders.RightEdge != null)
                    {
                        // Update the right edge if it overlaps
                        UpdateEdgeForOverlap(boarders.RightEdge, overlapEdge, horizontalOverlap: false);
                    }
                    if (overlapEdge.BoarderType == Edge.Boarder.Right && boarders.LeftEdge != null)
                    {
                        // Update the right edge if it overlaps
                        UpdateEdgeForOverlap(boarders.LeftEdge, overlapEdge, horizontalOverlap: false);
                    }
                    if (overlapEdge.BoarderType == Edge.Boarder.Top && boarders.BottomEdge != null)
                    {
                        // Update the right edge if it overlaps
                        UpdateEdgeForOverlap(boarders.BottomEdge, overlapEdge, horizontalOverlap: true);
                    }
                    if (overlapEdge.BoarderType == Edge.Boarder.Bottom && boarders.TopEdge != null)
                    {
                        // Update the right edge if it overlaps
                        UpdateEdgeForOverlap(boarders.TopEdge, overlapEdge, horizontalOverlap: true);
                    }
                }
            }
        }

        private void UpdateEdgeForOverlap(Edge sectionEdge, Edge overlapEdge, bool horizontalOverlap)
        {
            // Assuming horizontalOverlap indicates if the overlap is along the horizontal axis
            if (horizontalOverlap)
            {
                // If the overlap is horizontal, adjust the Y coordinates
                if (sectionEdge.StartNode.Y < overlapEdge.StartNode.Y)
                {
                    // If the section edge is above the overlap edge
                    sectionEdge.EndNode.Y = overlapEdge.StartNode.Y;
                }
                else
                {
                    // If the section edge is below the overlap edge
                    sectionEdge.StartNode.Y = overlapEdge.EndNode.Y;
                }
            }
            else
            {
                // If the overlap is vertical, adjust the X coordinates
                if (sectionEdge.StartNode.X < overlapEdge.StartNode.X)
                {
                    // If the section edge is to the left of the overlap edge
                    sectionEdge.EndNode.X = overlapEdge.StartNode.X;
                }
                else
                {
                    // If the section edge is to the right of the overlap edge
                    sectionEdge.StartNode.X = overlapEdge.EndNode.X;
                }
            }
        }


    }

}
