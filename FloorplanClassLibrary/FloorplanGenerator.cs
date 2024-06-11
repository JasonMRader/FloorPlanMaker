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
            foreach(Floorplan fp in shift.Floorplans)
            {
                if (fp.DiningArea.IsCocktail)
                {
                    CocktailersNeeded += ServerDistribution[fp.DiningArea];
                    CocktailersNeeded -= fp.Servers.Count();
                    CocktailAreas ++;
                }
            }
            List<Server> Cocktailers = shift.UnassignedServers
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
        public void AssignOutsideServers()
        {
            int OutsideServersNeeded = 0;
            int OutsideServersAreas = 0;
            foreach(Floorplan fp in shift.Floorplans)
            {
                if(!fp.DiningArea.IsInside && !fp.DiningArea.IsCocktail)
                {
                    OutsideServersNeeded += ServerDistribution[fp.DiningArea];
                    OutsideServersNeeded -= fp.Servers.Count();
                    OutsideServersAreas ++;
                }
            }
            List<Server> outsideServers = shift.UnassignedServers
               .OrderByDescending(s => s.AdjustedOutsidePriority)
               .Take(OutsideServersNeeded)
               .ToList();
            if (OutsideServersAreas == 1)
            {
                shift.SelectedFloorplan = shift.Floorplans.FirstOrDefault(fp => !fp.DiningArea.IsCocktail && !fp.DiningArea.IsInside);
                foreach (Server server in outsideServers)
                {
                    shift.AddServerToAFloorplan(server);
                    shift.SelectedFloorplan.AddServerAndSection(server);
                }
            }
        }
        public void AssignInsideServers()
        {
            int insideServersNeeded = 0;
            int insideServersAreas = 0;
            foreach (Floorplan fp in shift.Floorplans)
            {
                if (fp.DiningArea.IsInside && !fp.DiningArea.IsCocktail)
                {
                    insideServersNeeded += ServerDistribution[fp.DiningArea];
                    insideServersNeeded -= fp.Servers.Count();
                    insideServersAreas++;
                }
            }
            List<Server> insideServers = shift.UnassignedServers
               .OrderBy(s => s.AdjustedOutsidePriority)
               .Take(insideServersNeeded)
               .ToList();
            if (insideServersAreas == 1)
            {
                shift.SelectedFloorplan = shift.Floorplans.FirstOrDefault(fp => !fp.DiningArea.IsCocktail && fp.DiningArea.IsInside);
                foreach (Server server in insideServers)
                {
                    shift.AddServerToAFloorplan(server);
                    shift.SelectedFloorplan.AddServerAndSection(server);
                }
            }
        }

        public void AutoAssignDiningAreas()
        {
            if(CheckDiningAreas())
            {
                AssignCocktailers();
                AssignOutsideServers();
                AssignInsideServers();
            }
            else
            {
                MessageBox.Show("Currently auto assign is only supported when the " +
                    "floorplans used are: Inside Dining, Outside Dining, and " +
                    "Outside Cocktail");
            }
            

        }
        private bool CheckDiningAreas()
        {
            if (shift.DiningAreasUsed.Count != 3)
            {
                return false;
            }

            var requiredAreas = new List<string> { "Inside Dining", "Outside Dining", "Outside Cocktail" };
            foreach (var requiredArea in requiredAreas)
            {
                if (!shift.DiningAreasUsed.Any(area => area.Name == requiredArea))
                {
                    return false;
                }
            }

            return true;
        }

    }
}
