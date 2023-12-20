using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class OverriddenTablePairs
    {
        public OverriddenTablePairs() { }
        public Dictionary<string, string> hardcodedTopBottomNeighbors = new Dictionary<string, string>()
        {
            {"42", "41"},
            {"52", "61"},
            {"61", "52"},
            {"441","300" }

        };
        public Dictionary<string, string> hardcodedRightLeftNeighbors = new Dictionary<string, string>()
        {
            {"34", "42" },
            {"42","34" }

        };

        public HashSet<string> ignorePairs = new HashSet<string>()
        {
            "61-53",
            "51-63",
            "61-63",
            "51-53",
            "445-300",
            "65-54",
            "434-445",
            "441-418",
            "418-441"



        };
        public string GetPairKey(string tableNumberOne, string tableNumberTwo)
        {
            return tableNumberOne.CompareTo(tableNumberTwo) < 0
                ? tableNumberOne + "-" + tableNumberTwo
                : tableNumberTwo + "-" + tableNumberOne;
        }
    }
}
