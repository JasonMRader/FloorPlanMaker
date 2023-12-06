using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class FloorplanEdgesManager
    {
        public List<Section> Sections { get; set; }
        FloorplanEdgesManager(List<Section> sections)
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
            
            if (edge1.StartNode.Y != edge1.EndNode.Y || edge2.StartNode.Y != edge2.EndNode.Y ||
                edge1.StartNode.Y != edge2.StartNode.Y)
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
        //public List<Edge> Edges
    }
}
