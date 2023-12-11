namespace FloorplanClassLibrary
{
    public abstract partial class AdjustmentBox
    {
        public SectionBoarders SectionBoarders { get; set; }
        public SectionBoarders OtherSectionsBoarders { get; set; }
        public List<Node> Nodes { get; set; } = new List<Node>();
        public List<Edge> Edges { get; set; } = new List<Edge> { };
        public Edge RightEdge { get; set; }
        public Edge LeftEdge { get; set; }
        public Edge TopEdge { get; set; }
        public Edge BottomEdge { get; set; }
        protected void CreateEdgesForRectangle(Rectangle rect)
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
    }
}