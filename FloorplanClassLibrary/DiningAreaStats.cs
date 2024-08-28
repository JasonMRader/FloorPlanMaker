using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class DiningAreaStats
    {
        public int DiningAreaID { get; set; }
        public string DiningAreaName { get; set; }
        public float MaxSales { get; set; }
        public float MinSales { get; set; }
        public float AvgSales { get; set; }
        public float TotalSales { get; set; }
        public float PercentageOfTotalSales { get; set; }
        public float MaxPercentage { get; set; }
        public float MinPercentage { get; set; }
    }

}
