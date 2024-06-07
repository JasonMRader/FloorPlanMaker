using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class FloorplanGenerator
    {
        public FloorplanGenerator() { } 
        public FloorplanGenerator(Shift shift)
        {
            this.shift = shift;
        } 
        public Shift shift { get; set; }
        public  Dictionary<DiningArea, int> GetServerDistribution()
        {
            Dictionary<DiningArea, int> DiningAreaServerCounts = new Dictionary<DiningArea, int>();
            float totalSales = shift.DiningAreasUsed.Sum(da => da.ExpectedSales);
            if (totalSales == 0)
            {
                return null;
            }
            float salesPerServer =  totalSales/ shift.ServersOnShift.Count;
            
            foreach (DiningArea area in shift.DiningAreasUsed)
            {
                
                int roundedDownServers = (int)Math.Floor(area.ExpectedSales / salesPerServer);
                DiningAreaServerCounts[area] = roundedDownServers;

            }
            return DiningAreaServerCounts;
        }
    }
}
