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
        public Dictionary<DiningArea, int> ServerDistribution { get;set; }
        public int ServerCount
        {
            get
            {
                return shift.ServersOnShift.Count;
            }
        }
        public int ServerRemainder { get; set; }
       
        public int minimumServersAssigned { get; set; } 
        public  Dictionary<DiningArea, int> GetServerDistribution()
        {
            Dictionary<DiningArea, int> DiningAreaServerCounts = new Dictionary<DiningArea, int>();
            float totalSales = shift.DiningAreasUsed.Sum(da => da.ExpectedSales);
            if (totalSales == 0)
            {
                return null;
            }
            float salesPerServer =  totalSales/ shift.ServersOnShift.Count;
            int serversAssigned = 0;
            foreach (DiningArea area in shift.DiningAreasUsed)
            {
                
                int roundedDownServers = (int)Math.Floor(area.ExpectedSales / salesPerServer);
                DiningAreaServerCounts[area] = roundedDownServers;
                serversAssigned += roundedDownServers;

            }
            minimumServersAssigned = serversAssigned;
            ServerRemainder = ServerCount - minimumServersAssigned; 
            ServerDistribution = new Dictionary<DiningArea, int>();
            ServerDistribution = DiningAreaServerCounts;
            return DiningAreaServerCounts;
        }
        public void AssignCocktailers()
        {
            
            int CocktailersNeeded = 0;
            int CocktailAreas = 0;
            foreach(DiningArea area in shift.DiningAreasUsed)
            {
                if (area.IsCocktail)
                {
                    CocktailersNeeded += ServerDistribution[area];
                    CocktailAreas ++;
                }
            }
            List<Server> Cocktailers = shift.ServersOnShift
                .OrderByDescending(s => s.CocktailPreference)
                .Take(CocktailersNeeded)
                .ToList();
            if(CocktailAreas == 1)
            {
                shift.SelectedFloorplan = shift.Floorplans.FirstOrDefault(fp => fp.DiningArea.IsCocktail);
                foreach(Server server in Cocktailers)
                {
                    shift.AddServerToAFloorplan(server);
                    shift.SelectedFloorplan.AddServerAndSection(server);
                }
            }
            

            
        }

    }
}
