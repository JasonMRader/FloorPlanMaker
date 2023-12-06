using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace FloorplanClassLibrary
{
    public class FloorplanEdgesManager
    {
        public List<Section> Sections { get; set; }
        public List<Edge> Edges { get; set; } = new List<Edge>();
        public List<string> testData { get; set; } = new List<string>();
        public FloorplanEdgesManager(List<Section> sections)
        {
            this.Sections = sections;
        }
        public void SetSectionNodeManagers()
        {
            foreach (var section in Sections)
            {
                section.SetNodeManager();
            }
        }
        
        public static (bool, Edge)AreHorizontalAndOverlapping(Edge edge1, Edge edge2)
        {
            
            if (edge1.StartNode.Y != edge1.EndNode.Y || edge2.StartNode.Y != edge2.EndNode.Y)              
            {
                return (false, null);
            }

            
            int line1LeftX = Math.Min(edge1.StartNode.X, edge1.EndNode.X);
            int line1RightX = Math.Max(edge1.StartNode.X, edge1.EndNode.X);
            int line2LeftX = Math.Min(edge2.StartNode.X, edge2.EndNode.X);
            int line2RightX = Math.Max(edge2.StartNode.X, edge2.EndNode.X);

            // Check if one line's X-coordinates overlap with the other's
            bool isOverlapping = line1LeftX <= line2RightX && line2LeftX <= line1RightX;

            if (!isOverlapping)
            {
                return (false, null);
            }

            // Calculate overlap coordinates
            int overlapStartX = Math.Max(line1LeftX, line2LeftX);
            int overlapEndX = Math.Min(line1RightX, line2RightX);
            int averageY = (edge1.StartNode.Y + edge2.StartNode.Y) / 2;

            Node startNode = new Node(overlapStartX, averageY, edge1.Section); // Assuming Section is required
            Node endNode = new Node(overlapEndX, averageY, edge1.Section);
            Edge overlapEdge = new Edge(startNode, endNode);

            return (true, overlapEdge);
        }
        // TODO for effecincy, once a line has been compared, do not compare it again?
        public void SetAllSectionsTopBoarders()
        {
            foreach (var section in Sections)
            {
                GetSectionsTopBoarder(section);
            }
        }
        public List<SectionLine> GetTopSectionLines()
        {
            List<SectionLine> sectionLines = new List<SectionLine>();
            foreach (Edge edge in Edges)
            {
                SectionLine line = new SectionLine(edge.StartNode, edge.EndNode);
                if (edge.isVertical)
                {
                    line.Edge = SectionLine.BorderEdge.Left;  // or Right, decide based on your logic
                }
                else if (edge.isHorizontal)
                {
                    line.Edge = SectionLine.BorderEdge.Top;  // or Bottom, decide based on your logic
                }
                sectionLines.Add(line);
            }
            return sectionLines;
        }
        private string GetTestData(Edge topEdge, Edge bottomEdge, Edge newEdge)
        {
            string result = "";

            result = "Top: (" + topEdge.startPoint().X.ToString() + ", " + topEdge.endPoint().X.ToString() + ") | Bottom: (" +
                bottomEdge.startPoint().X.ToString() + ", " + bottomEdge.endPoint().X.ToString() + ") | New: (" +
                newEdge.startPoint().X.ToString() + ", " + newEdge.endPoint().X.ToString();

            return result;
        }   
        private void GetSectionsTopBoarder(Section section)
        {
            List<Edge> overlappingEdges = new List<Edge>();
            foreach (Section s in Sections)
            {
                if (s == section)
                    continue;

                foreach (Edge topEdge in section.NodeManager.TopEdges)
                {
                    foreach (Edge bottomEdge in s.NodeManager.BottomEdges)
                    {
                        var (isOverlapping, overlapEdge) = AreHorizontalAndOverlapping(topEdge, bottomEdge);
                        if (isOverlapping && overlapEdge != null)
                        {
                            overlappingEdges.Add(overlapEdge);
                            testData.Add(GetTestData(topEdge, bottomEdge, overlapEdge));
                        }
                    }
                }
            }
            Edges.AddRange(overlappingEdges);
                        
            //ProcessOverlappingEdges(overlappingEdges);
        }

        private void ProcessOverlappingEdges(List<Edge> overlappingEdges)
        {
            throw new NotImplementedException();
        }
    }
}
