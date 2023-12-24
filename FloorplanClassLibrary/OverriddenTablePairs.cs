using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class OverriddenTablePairs
    {
        public OverriddenTablePairs() 
        {
            ignorePairs = SqliteDataAccess.LoadAllIgnoredPairs();
            TopBottomNeighbors = SqliteDataAccess.LoadAllTopBottomNeighbors();
            RightLeftNeighbors = SqliteDataAccess.LoadAllRightLeftNeighbors();
        }
        public Dictionary<string, string> TopBottomNeighbors = new Dictionary<string, string>()
        {
            {"42", "41"},
            {"52", "61"},
            {"61", "52"},
            {"441","300" }

        };
        public Dictionary<string, string> RightLeftNeighbors = new Dictionary<string, string>()
        {
            {"34", "42" },
            {"42","34" }

        };
        public Dictionary<string, string> RightLeftNeighborsCustomEdge = new Dictionary<string, string>()
        {
            {"34", "42" },
            {"42","34" }

        };
        public Dictionary<string, string> TopBottomNeighborsCustomEdge = new Dictionary<string, string>()
        {
            {"34", "42" },
            {"42","34" }

        };

        public HashSet<string> ignorePairs = new HashSet<string>();
       
        public Dictionary<HashSet<string>, Edge> CustomPairs = new Dictionary<HashSet<string>, Edge>();
        public string GetPairKey(string tableNumberOne, string tableNumberTwo)
        {
            return tableNumberOne.CompareTo(tableNumberTwo) < 0
                ? tableNumberOne + "-" + tableNumberTwo
                : tableNumberTwo + "-" + tableNumberOne;
        }
        public RightLeftNeighbor CustomRightLeftEdge(int midLocation, int startPoint, int endPoint, TableEdgeBorders rightBorder, TableEdgeBorders leftBorder)
        {
            RightLeftNeighbor customNeighbor = new RightLeftNeighbor(midLocation, startPoint, endPoint, rightBorder, leftBorder);   
            return customNeighbor;
        }
        public TopBottomNeighbor CustomTopBottomEdge(int midLocation, int startPoint, int endPoint, TableEdgeBorders TopBorder, TableEdgeBorders BottomBorder)
        {
            TopBottomNeighbor customNeighbor = new TopBottomNeighbor(midLocation, startPoint, endPoint, TopBorder, BottomBorder);
            return customNeighbor;
        }
    }
}
