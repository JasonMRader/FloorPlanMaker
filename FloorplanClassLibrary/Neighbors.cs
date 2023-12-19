using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class Neighbors
    {
        public Neighbors(TableEdgeBorders rightNeighbor, TableEdgeBorders leftNeighbor) 
        {
            RightNeighbor = rightNeighbor;
            LeftNeighbor = leftNeighbor;
            MidPoint = (LeftNeighbor.Table.Right + RightNeighbor.Table.Left)/2;
            StartNode = new Node(MidPoint, Math.Max(RightNeighbor.TopBorderY, LeftNeighbor.TopBorderY));
            EndNode = new Node(MidPoint, Math.Max(RightNeighbor.BottomBorderY, LeftNeighbor.BottomBorderY));
        }
        public Neighbors(TableEdgeBorders topNeighbor, TableEdgeBorders bottomNeighbor, bool isUpDown) 
        {
            TopNeighbor = topNeighbor;
            BottomNeighbor = bottomNeighbor;
            IsUpDown = isUpDown;
            MidPoint = (TopNeighbor.Table.Bottom + BottomNeighbor.Table.Top)/2;
        }
        public bool IsUpDown { get; set; }
        public TableEdgeBorders? RightNeighbor { get; set; }
        public TableEdgeBorders? LeftNeighbor { get; set; }
        public TableEdgeBorders? TopNeighbor { get; set; }
        public TableEdgeBorders? BottomNeighbor { get; set; }
        public int MidPoint { get; set; }
        public Node StartNode { get; set; }
        public Node EndNode { get; set; }   
        public Edge Edge { get; set; } 
        
        
    }
}
