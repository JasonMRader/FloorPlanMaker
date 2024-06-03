using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class FloorplanGenerator
    {
        public static Dictionary<DiningArea, int> GetServerDistribution(List<DiningArea> diningAreas, int ServerCount)
        {
            Dictionary<DiningArea, int> DiningAreaServerCounts = new Dictionary<DiningArea, int>();
            float totalSales = diningAreas.Sum(da => da.ExpectedSales);
            if (totalSales == 0)
            {
                return null;
            }
            float salesPerServer =  totalSales/ ServerCount;
            
            foreach (DiningArea area in diningAreas)
            {
                
                int roundedDownServers = (int)Math.Floor(area.ExpectedSales / salesPerServer);
                DiningAreaServerCounts[area] = roundedDownServers;

            }
            return DiningAreaServerCounts;
        }
    }
}
