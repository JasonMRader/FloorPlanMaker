using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TableEdgeBoarders
    {
        public TableEdgeBoarders(Table table)
        {
            this.Table = table;            
            initEdgesAndNodes();
        }
        public Table Table { get; set; }
        public List<Table> RightNeighbors { get; set; } = new List<Table>();
        public List<Table> LeftNeighbors { get; set; } = new List<Table>();
        public List<Table> TopNeighbors { get; set; } = new List<Table>();
        public List<Table> BottomNeighbors { get; set; } = new List<Table>();
        public Edge RightSide { get; set; } 
        public Edge LeftSide { get; set; }
        public Edge TopSide { get; set; }
        public Edge BottomSide { get; set;}
        public Node TopLeft { get; set; }
        public Node TopRight { get; set; }
        public Node BottomRight { get; set; }
        public Node BottomLeft { get; set;}
        private void initEdgesAndNodes()
        {
            this.TopLeft = new Node(Table.TopLeft.X, Table.TopLeft.Y);
            this.TopRight = new Node(Table.TopRight.X, Table.TopRight.Y);
            this.BottomRight = new Node(Table.BottomRight.X, Table.BottomRight.Y);
            this.BottomLeft = new Node(Table.BottomLeft.X, Table.BottomLeft.Y);
            TopLeft.Parent = BottomLeft;
            TopLeft.Child = TopRight;
            TopRight.Parent = TopLeft;
            TopRight.Child = BottomRight;
            BottomRight.Parent = TopRight;
            BottomRight.Child = BottomLeft;
            BottomLeft.Parent = BottomRight;
            BottomLeft.Child = TopLeft;
            TopSide = new Edge(TopLeft, TopRight, Edge.Boarder.Top);
            RightSide = new Edge(TopRight, BottomRight, Edge.Boarder.Right);
            BottomSide = new Edge(BottomRight, BottomLeft, Edge.Boarder.Bottom);
            LeftSide = new Edge(BottomLeft, TopLeft, Edge.Boarder.Left);

        }
        public override string ToString()
        {
            return Table.TableNumber;
        }
    }
}
