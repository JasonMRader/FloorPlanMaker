using NetTopologySuite.Algorithm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class FloorplanGenerator
    {
        public void TestAddServers(int servers)
        {
            for (int i = 0; i < servers; i++)
            {
                shift.AddNewUnassignedServer(shift.AllServers[i]);
            }
        }
        public FloorplanGenerator() { } 
        public FloorplanGenerator(Shift shift)
        {
            this.shift = shift;
        } 
        public Shift shift { get; set; }
        public Dictionary<DiningArea, int> ServerDistribution { get;set; }
        public Dictionary<DiningArea, float> AreaPerServerSales { get;set; }
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
        public Dictionary<DiningArea, int> TESTGetServerDistribution()
        {
            Dictionary<DiningArea, int> DiningAreaServerCounts = new Dictionary<DiningArea, int>();
            Dictionary<DiningArea, float> areaPerServerSales = new Dictionary<DiningArea, float>();
        float totalSales = shift.DiningAreasUsed.Sum(da => da.TestSales);
            if (totalSales == 0)
            {
                return null;
            }
            float salesPerServer = totalSales / shift.ServersOnShift.Count;
            int serversAssigned = 0;
            foreach (DiningArea area in shift.DiningAreasUsed)
            {

                int roundedDownServers = (int)Math.Floor(area.TestSales / salesPerServer);
                DiningAreaServerCounts[area] = roundedDownServers;
                serversAssigned += roundedDownServers;
                areaPerServerSales[area] = (float)(area.TestSales / roundedDownServers);

            }
            minimumServersAssigned = serversAssigned;
            ServerRemainder = ServerCount - minimumServersAssigned;
            ServerDistribution = new Dictionary<DiningArea, int>();
            ServerDistribution = DiningAreaServerCounts;
            AreaPerServerSales = new Dictionary<DiningArea, float>();
            AreaPerServerSales = areaPerServerSales;
            return DiningAreaServerCounts;
        }
        public void AssignCocktailers()
        {
            
            int CocktailersNeeded = 0;
            int CocktailAreas = 0;
            List<Server> unassignedBartenders = shift.ServersOnShift.Where(s => s.IsBartender).ToList();
            foreach(Floorplan fp in shift.Floorplans)
            {
                if (fp.DiningArea.IsCocktail)
                {
                    CocktailersNeeded += ServerDistribution[fp.DiningArea]; 
                    if(unassignedBartenders.Count > 0)
                    {
                        shift.SelectedFloorplan = fp;
                        shift.AddServerToAFloorplan(unassignedBartenders[0]);
                        shift.SelectedFloorplan.AddServerAndSection(unassignedBartenders[0]);
                        unassignedBartenders.Remove(unassignedBartenders[0]);
                    };
                    CocktailersNeeded -= fp.Servers.Count();
                    CocktailAreas ++;
                }
            }
            CocktailersNeeded -= unassignedBartenders.Count();
            List<Server> Cocktailers = shift.UnassignedServers
                .OrderByDescending(s => s.CocktailPreference)
                .Take(CocktailersNeeded)
                .ToList();
            Cocktailers.AddRange(unassignedBartenders);
            //if(CocktailAreas == 1)
            //{
            //    shift.SelectedFloorplan = shift.Floorplans.FirstOrDefault(fp => fp.DiningArea.IsCocktail);
            //    foreach(Server server in Cocktailers)
            //    {
            //        shift.AddServerToAFloorplan(server);
            //        shift.SelectedFloorplan.AddServerAndSection(server);
            //    }
            //}
            int serverIndex = 0;
            foreach (Floorplan floorplan in shift.Floorplans)
            {
               
                if(floorplan.DiningArea.IsCocktail)
                {
                    shift.SelectedFloorplan = floorplan;
                    int serversNeeded = ServerDistribution[floorplan.DiningArea] - floorplan.Servers.Count;
                    while(serversNeeded > 0)
                    {
                        shift.AddServerToAFloorplan(Cocktailers[serverIndex]);
                        shift.SelectedFloorplan.AddServerAndSection(Cocktailers[serverIndex]);
                        serverIndex++;
                        serversNeeded--;
                    }
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
            //if (insideServersAreas == 1)
            //{
            //    shift.SelectedFloorplan = shift.Floorplans.FirstOrDefault(fp => !fp.DiningArea.IsCocktail && fp.DiningArea.IsInside);
            //    foreach (Server server in insideServers)
            //    {
            //        shift.AddServerToAFloorplan(server);
            //        shift.SelectedFloorplan.AddServerAndSection(server);
            //    }
            //}
            int serverIndex = 0;
            foreach (Floorplan floorplan in shift.Floorplans)
            {

                if (floorplan.DiningArea.IsInside && !floorplan.DiningArea.IsCocktail)
                {
                    shift.SelectedFloorplan = floorplan;
                    int serversNeeded = ServerDistribution[floorplan.DiningArea] - floorplan.Servers.Count;
                    while (serversNeeded > 0)
                    {
                        shift.AddServerToAFloorplan(insideServers[serverIndex]);
                        shift.SelectedFloorplan.AddServerAndSection(insideServers[serverIndex]);
                        serverIndex++;
                        serversNeeded--;
                    }
                }
            }
        }

        public void AutoAssignDiningAreas()
        {
            if (shift.DiningAreasUsed.Count == 1)
            {
                List<Server> servers = shift.UnassignedServers.ToList();
                foreach (Server server in servers)
                {
                    shift.AddServerToAFloorplan(server);
                    shift.SelectedFloorplan.AddServerAndSection(server);
                }
            }
            else
            {
                AssignCocktailers();
                AssignOutsideServers();
                AssignInsideServers();
            }
            //if (CheckForInOutAndCocktail())
            //{
            //    AssignCocktailers();
            //    AssignOutsideServers();
            //    AssignInsideServers();
            //}
            //else if (CheckForOutAndCocktail())
            //{
            //    AssignCocktailers();
            //    AssignOutsideServers();
            //}
            //else if (CheckForInAndOutDining())
            //{
            //    AssignOutsideServers();
            //    AssignInsideServers();
            //}
            //else if(shift.DiningAreasUsed.Count == 1)
            //{
            //    List<Server> servers = shift.UnassignedServers.ToList();
            //    foreach (Server server in servers)
            //    {
            //        shift.AddServerToAFloorplan(server);
            //        shift.SelectedFloorplan.AddServerAndSection(server);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("Currently auto assign is only supported when the " +
            //        "floorplans used are: Inside Dining, Outside Dining, and " +
            //        "Outside Cocktail OR Outside Dining and Cocktail OR Inside and Outside Dining");
            //}
            

        }
        private bool CheckForInOutAndCocktail()
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
        private bool CheckForOutAndCocktail()
        {
            if (shift.DiningAreasUsed.Count != 2)
            {
                return false;
            }

            var requiredAreas = new List<string> { "Outside Dining", "Outside Cocktail" };
            foreach (var requiredArea in requiredAreas)
            {
                if (!shift.DiningAreasUsed.Any(area => area.Name == requiredArea))
                {
                    return false;
                }
            }

            return true;
        }
        private bool CheckForInAndOutDining()
        {
            if (shift.DiningAreasUsed.Count != 2)
            {
                return false;
            }

            var requiredAreas = new List<string> { "Outside Dining", "Inside Dining" };
            foreach (var requiredArea in requiredAreas)
            {
                if (!shift.DiningAreasUsed.Any(area => area.Name == requiredArea))
                {
                    return false;
                }
            }

            return true;
        }
        public FloorplanTemplate SelectIdealTemplate(List<FloorplanTemplate> templates)
        {
            templates = templates.Where(t => t.ServerCount == shift.SelectedFloorplan.Servers.Count).ToList();
            Dictionary<FloorplanTemplate, float> templateVarience = GetTemplateSectionVarience(templates);
            var idealTemplate = templateVarience.OrderBy(kv => kv.Value).FirstOrDefault().Key;
            return idealTemplate;
        }
        public Dictionary<FloorplanTemplate, float> GetTemplateSectionVarience(List<FloorplanTemplate> templates)
        {
            Dictionary<FloorplanTemplate, float> templateVarience = new Dictionary<FloorplanTemplate, float>();
            float avgSales = shift.SelectedFloorplan.AvgSalesPerServer;
            if (avgSales == 0)
            {
                return null;
            }
           
            foreach (FloorplanTemplate template in templates)
            {
                float varience = 0f;
                foreach(Section section in  template.Sections)
                {
                    varience += Math.Abs(avgSales - section.ExpectedSalesPerServer);
                }

                templateVarience[template] = varience;

            }
           
            return templateVarience;
        }

    }
}
