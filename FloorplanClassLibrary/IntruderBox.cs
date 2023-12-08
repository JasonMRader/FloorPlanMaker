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

            List<Edge> edges = new List<Edge>
            {
                new Edge(topLeft, topRight, Edge.Boarder.Top),
                new Edge(topRight, bottomRight, Edge.Boarder.Right),
                new Edge(bottomRight, bottomLeft, Edge.Boarder.Bottom),
                new Edge(bottomLeft, topLeft, Edge.Boarder.Left)
            };

            this.Edges = edges; 
        }
        public void RemoveIntruderBoxFromBoundingBox()
        {

        }

    }
}
