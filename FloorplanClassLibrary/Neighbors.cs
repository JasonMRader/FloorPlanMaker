using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class Neighbors
    {
        public Neighbors() { }
        public TableEdgeBorders? RightNeighbor { get; set; }
        public TableEdgeBorders? LeftNeighbor { get; set; }
        public TableEdgeBorders? TopNeighbor { get; set; }
        public TableEdgeBorders? BottomNeighbor { get; set; }
        public int MidPoint { get; set; }
        public Edge Edge { get; set; } 
        
        
    }
}
