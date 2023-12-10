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
        public FloorplanBoarderManager() { }
        public List<IntruderBox> IntruderBoxes { get; set; } = new List<IntruderBox> { };
        public Dictionary<Section, List<Section>> RightNeighbors { get; set; }
        public Dictionary<Section, List<Section>> LeftNeighbors { get; set; }
        public FloorplanBoarderManager(List<Section> sections)
        {
            this.Sections = sections;
            foreach (var section in Sections)
            {
                section.SetBoarderManager();
            }
            FindLeftRightNeighbors();
        }
       
        public void FindLeftRightNeighbors()
        {
            // Initialize the dictionaries if not already initialized
            RightNeighbors = RightNeighbors ?? new Dictionary<Section, List<Section>>();
            LeftNeighbors = LeftNeighbors ?? new Dictionary<Section, List<Section>>();

            foreach (var currentSection in Sections)
            {
                Edge currentRightEdge = currentSection.SectionBoarders.RightEdge;

                foreach (var otherSection in Sections)
                {
                    // Skip if it's the same section
                    if (otherSection == currentSection) continue;

                    Edge otherLeftEdge = otherSection.SectionBoarders.LeftEdge;

                    // Check if the other section's LeftEdge is to the right of the current section's RightEdge
                    if (otherLeftEdge.StartNode.X > currentRightEdge.EndNode.X)
                    {
                        // Check for Y overlap
                        bool isOverlapY = (otherLeftEdge.VerticleEdgeStartY() <= currentRightEdge.VerticleEdgeEndY() &&
                                           otherLeftEdge.VerticleEdgeEndY() >= currentRightEdge.VerticleEdgeStartY()) ||
                                          (currentRightEdge.VerticleEdgeStartY() <= otherLeftEdge.VerticleEdgeEndY() &&
                                           currentRightEdge.VerticleEdgeEndY() >= otherLeftEdge.VerticleEdgeStartY());

                        // Check if no other section's LeftLine is in between
                        bool isNoOtherLeftLineInBetween = !Sections.Any(s => s != currentSection && s != otherSection &&
                                                                             s.SectionBoarders.LeftEdge.StartNode.X > currentRightEdge.EndNode.X &&
                                                                             s.SectionBoarders.LeftEdge.StartNode.X < otherLeftEdge.StartNode.X);

                        if (isOverlapY && isNoOtherLeftLineInBetween)
                        {
                            // Add to the dictionaries
                            if (!RightNeighbors.ContainsKey(currentSection))
                                RightNeighbors[currentSection] = new List<Section>();
                            if (!LeftNeighbors.ContainsKey(otherSection))
                                LeftNeighbors[otherSection] = new List<Section>();

                            RightNeighbors[currentSection].Add(otherSection);
                            LeftNeighbors[otherSection].Add(currentSection);
                        }
                    }
                }
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
                        IntruderBox intruderBox = new IntruderBox(sectionBoardersList[i], sectionBoardersList[j], overlappingRectangle.Value);
                        sectionBoardersList[i].IntruderBoxes.Add(intruderBox);
                        // Add these edges to the list or process as needed
                        OverLappingEdges.AddRange(overlappingEdges);
                    }
                }
            }
        }
        //public void CalculateOverlappingSectionEdges()
        //{
        //    for (int i = 0; i < Sections.Count; i++)
        //    {
        //        for (int j = i + 1; j < Sections.Count; j++)
        //        {
        //            SectionBoarders boarders1 = Sections[i].SectionBoarders;
        //            SectionBoarders boarders2 = Sections[j].SectionBoarders;
        //            var overlappingRectangle = GetOverlappingRectangle(boarders1, boarders2);

        //            if (overlappingRectangle != null)
        //            {
        //                // Create nodes for the overlapping rectangle
        //                Node topLeft = new Node(overlappingRectangle.Value.Left, overlappingRectangle.Value.Top, null);
        //                Node topRight = new Node(overlappingRectangle.Value.Right, overlappingRectangle.Value.Top, null);
        //                Node bottomRight = new Node(overlappingRectangle.Value.Right, overlappingRectangle.Value.Bottom, null);
        //                Node bottomLeft = new Node(overlappingRectangle.Value.Left, overlappingRectangle.Value.Bottom, null);

        //                // Determine which SectionBoarders each edge belongs to
        //                SectionBoarders leftSectionBoarders = overlappingRectangle.Value.Left == boarders1.LeftEdge.StartNode.X ? boarders1 : boarders2;
        //                SectionBoarders rightSectionBoarders = overlappingRectangle.Value.Right == boarders1.RightEdge.StartNode.X ? boarders1 : boarders2;
        //                SectionBoarders topSectionBoarders = overlappingRectangle.Value.Top == boarders1.TopEdge.StartNode.Y ? boarders1 : boarders2;
        //                SectionBoarders bottomSectionBoarders = overlappingRectangle.Value.Bottom == boarders1.BottomEdge.StartNode.Y ? boarders1 : boarders2;

        //                // Assuming you want to process each side individually
        //                // You might need to adjust this based on how you want to handle the intruding sections
        //                ProcessIntruderBox(leftSectionBoarders, (leftSectionBoarders == boarders1 ? boarders2 : boarders1), new List<Node> { topLeft, bottomLeft });
        //                ProcessIntruderBox(rightSectionBoarders, (rightSectionBoarders == boarders1 ? boarders2 : boarders1), new List<Node> { topRight, bottomRight });
        //                ProcessIntruderBox(topSectionBoarders, (topSectionBoarders == boarders1 ? boarders2 : boarders1), new List<Node> { topLeft, topRight });
        //                ProcessIntruderBox(bottomSectionBoarders, (bottomSectionBoarders == boarders1 ? boarders2 : boarders1), new List<Node> { bottomLeft, bottomRight });
        //            }

        //        }
        //    }
        //}
        //private void ProcessIntruderBox(SectionBoarders sectionBoarders, SectionBoarders intruderSectionBoarders, List<Node> nodes)
        //{
        //    IntruderBox intruderBox = new IntruderBox
        //    {
        //        SectionBoarders = sectionBoarders,
        //        IntruderSectionBoarders = intruderSectionBoarders,
        //        Nodes = nodes
        //    };

        //    // Store this information in the relevant SectionBoarders
        //    intruderBox.SectionBoarders.IntrudersLocations.Add(intruderBox.IntruderSectionBoarders, intruderBox.Nodes);
        //    intruderBox.IntruderSectionBoarders.IntrudingBoarders.Add(intruderBox.SectionBoarders);
        //}

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
                Rectangle intrudingRectangle = new Rectangle(left, top, right - left, bottom - top);
                IntruderBox intruderBox = new IntruderBox(boarders1,boarders2, intrudingRectangle);
                IntruderBoxes.Add(intruderBox);

                //if (left == boarders1.LeftEdge.StartNode.X)
                //{

                //}

                return intrudingRectangle;
            }

            return null;
        }
        //public void ModifyBordersForOverlaps()
        //{
        //    CalculateOverlappingSectionEdges();

        //    foreach (Edge overlapEdge in OverLappingEdges)
        //    {
        //        foreach (Section section in Sections)
        //        {
        //            SectionBoarders boarders = section.SectionBoarders;

        //            // Check and modify the right edge of the section
        //            if (overlapEdge.BoarderType == Edge.Boarder.Left && boarders.RightEdge != null)
        //            {
        //                // Update the right edge if it overlaps
        //                UpdateEdgeForOverlap(boarders.RightEdge, overlapEdge, horizontalOverlap: false, boarders.Edges);
        //            }
        //            if (overlapEdge.BoarderType == Edge.Boarder.Right && boarders.LeftEdge != null)
        //            {
        //                // Update the right edge if it overlaps
        //                UpdateEdgeForOverlap(boarders.LeftEdge, overlapEdge, horizontalOverlap: false, boarders.Edges);
        //            }
        //            if (overlapEdge.BoarderType == Edge.Boarder.Top && boarders.BottomEdge != null)
        //            {
        //                // Update the right edge if it overlaps
        //                UpdateEdgeForOverlap(boarders.BottomEdge, overlapEdge, horizontalOverlap: true, boarders.Edges);
        //            }
        //            if (overlapEdge.BoarderType == Edge.Boarder.Bottom && boarders.TopEdge != null)
        //            {
        //                // Update the right edge if it overlaps
        //                UpdateEdgeForOverlap(boarders.TopEdge, overlapEdge, horizontalOverlap: true, boarders.Edges);
        //            }
        //        }
        //    }
        //}

        //private void UpdateEdgeForOverlap(Edge sectionEdge, Edge overlapEdge, bool horizontalOverlap, List<Edge> edgesToUpdate)
        //{
        //    if (horizontalOverlap)
        //    {
        //        // Horizontal overlap handling
        //        if (sectionEdge.StartNode.Y < overlapEdge.StartNode.Y && sectionEdge.EndNode.Y > overlapEdge.EndNode.Y)
        //        {
        //            // Splitting the horizontal edge
        //            Node newEndNode = new Node(sectionEdge.StartNode.X, overlapEdge.StartNode.Y, sectionEdge.StartNode.Section);
        //            Node newStartNode = new Node(sectionEdge.EndNode.X, overlapEdge.EndNode.Y, sectionEdge.EndNode.Section);

        //            Edge newEdge1 = new Edge(sectionEdge.StartNode, newEndNode);
        //            Edge newEdge2 = new Edge(newStartNode, sectionEdge.EndNode);

        //            edgesToUpdate.Add(newEdge1);
        //            edgesToUpdate.Add(newEdge2);
        //            edgesToUpdate.Remove(sectionEdge);
        //        }
        //        else
        //        {
        //            // Adjusting the horizontal edge
        //            if (sectionEdge.StartNode.Y < overlapEdge.StartNode.Y)
        //            {
        //                sectionEdge.EndNode.Y = overlapEdge.StartNode.Y;
        //            }
        //            else
        //            {
        //                sectionEdge.StartNode.Y = overlapEdge.EndNode.Y;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        // Vertical overlap handling
        //        if (sectionEdge.StartNode.X < overlapEdge.StartNode.X && sectionEdge.EndNode.X > overlapEdge.EndNode.X)
        //        {
        //            // Splitting the vertical edge
        //            Node newEndNode = new Node(overlapEdge.StartNode.X, sectionEdge.StartNode.Y, sectionEdge.StartNode.Section);
        //            Node newStartNode = new Node(overlapEdge.EndNode.X, sectionEdge.EndNode.Y, sectionEdge.EndNode.Section);

        //            Edge newEdge1 = new Edge(sectionEdge.StartNode, newEndNode);
        //            Edge newEdge2 = new Edge(newStartNode, sectionEdge.EndNode);

        //            edgesToUpdate.Add(newEdge1);
        //            edgesToUpdate.Add(newEdge2);
        //            edgesToUpdate.Remove(sectionEdge);
        //        }
        //        else
        //        {
        //            // Adjusting the vertical edge
        //            if (sectionEdge.StartNode.X < overlapEdge.StartNode.X)
        //            {
        //                sectionEdge.EndNode.X = overlapEdge.StartNode.X;
        //            }
        //            else
        //            {
        //                sectionEdge.StartNode.X = overlapEdge.EndNode.X;
        //            }
        //        }
        //    }
        //}



    }

}
