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
        public static void SetClosersForFloorplan(Floorplan floorplan)
        {
            List<Server> potentialClosingServers = new List<Server>();
            List<Section> potentialClosingSections = floorplan.Sections.Where(s => !s.IsBarSection && !s.IsTeamWait && !s.IsPickUp).ToList();
            if(potentialClosingSections.Count == 0)
            {
                MessageBox.Show("No Available Closing Sections\n \n" +
                    "By Default, Teamwait Sections or Sections with Doubles\n" +
                    " are Not Auto Assigned as Closers");
                return;
            }
            if(potentialClosingSections.Count == 1)
            {
                potentialClosingSections.First().IsCloser = true;
                return;
            }
            potentialClosingServers = floorplan.Servers.Where(s => s.isDouble == false).ToList();
            Dictionary<Server, int> closingServersWeight = new Dictionary<Server, int>();
            potentialClosingServers = potentialClosingServers.OrderByDescending(s => s.CloseFrequency).ToList();
            int highestCloseRank = potentialClosingServers[0].CloseFrequency;
            potentialClosingServers.RemoveAll(s => s.CloseFrequency < highestCloseRank - 4);
            
            Random random = new Random(); 
            foreach (Server server in potentialClosingServers)
            {
                int closingRank = random.Next(0, server.CloseFrequency);
                closingServersWeight.Add(server, closingRank);
            }
            Server closer = null;
            Server precloser = null;
            if (closingServersWeight.Count > 0)
            {
                closer = closingServersWeight.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                closingServersWeight.Remove(closer);
                precloser = closingServersWeight.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            }

            if (closer != null)
            {
                Section closingSection = potentialClosingSections.FirstOrDefault(s => s.Server.ID == closer.ID);
                if (closingSection != null)
                {
                    
                    closingSection.IsCloser = true;
                    
                }                
            }
            if(precloser != null)
            {
                Section preclosingSection = potentialClosingSections.FirstOrDefault(s => s.Server.ID == precloser.ID);
                if (preclosingSection != null)
                {

                    preclosingSection.IsPre = true;

                }
            }
           
        }

        public Dictionary<DiningArea, int> GetServerDistribution()
        {
            Dictionary<DiningArea, int> DiningAreaServerCounts = new Dictionary<DiningArea, int>();
            Dictionary<DiningArea, float> areaPerServerSales = new Dictionary<DiningArea, float>();
            float totalSales = shift.DiningAreasUsed.Sum(da => da.ExpectedSales);
            if (totalSales == 0)
            {
                return null;
            }
            float shiftSalesPerServer = totalSales / shift.ServersOnShift.Count;
            int serversAssigned = 0;
            foreach (DiningArea area in shift.DiningAreasUsed)
            {

                int roundedDownServers = (int)Math.Floor(area.ExpectedSales / shiftSalesPerServer);
                DiningAreaServerCounts[area] = roundedDownServers;
                serversAssigned += roundedDownServers;
                if (roundedDownServers == 0)
                {
                    areaPerServerSales[area] = -1f;
                }
                else
                {
                    areaPerServerSales[area] = (float)(area.ExpectedSales / roundedDownServers);
                }


            }
            minimumServersAssigned = serversAssigned;
            ServerRemainder = ServerCount - minimumServersAssigned;
            for (int i = 0; i < ServerRemainder; i++)
            {
                float bestSales = float.MinValue;
                DiningArea areaToAddTo = null;

                foreach (DiningArea diningArea in shift.DiningAreasUsed)
                {
                    float currentDifferenceFromAvg = areaPerServerSales[diningArea] - shiftSalesPerServer;
                    float salesIfServerAdded = diningArea.ExpectedSales / (DiningAreaServerCounts[diningArea] + 1);
                    //float newSalesPerServer = Math.Abs(salesIfServerAdded - shiftSalesPerServer);
                    //float newDifferenceFromAvg = salesIfServerAdded - shiftSalesPerServer;
                    if(areaPerServerSales[diningArea] > (shiftSalesPerServer * 1.6))
                    {
                        bestSales = salesIfServerAdded;
                        areaToAddTo = diningArea;
                        
                    }
                    else if (salesIfServerAdded > bestSales)
                    {
                        bestSales = salesIfServerAdded;
                        areaToAddTo = diningArea;
                    }
                }

                if (areaToAddTo != null)
                {
                    DiningAreaServerCounts[areaToAddTo]++;
                    areaPerServerSales[areaToAddTo] = areaToAddTo.ExpectedSales / DiningAreaServerCounts[areaToAddTo];
                }
            }
            ServerDistribution = new Dictionary<DiningArea, int>();
            ServerDistribution = DiningAreaServerCounts;
            AreaPerServerSales = new Dictionary<DiningArea, float>();
            AreaPerServerSales = areaPerServerSales;
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
            float shiftSalesPerServer = totalSales / shift.ServersOnShift.Count;
            int serversAssigned = 0;
            foreach (DiningArea area in shift.DiningAreasUsed)
            {

                int roundedDownServers = (int)Math.Floor(area.TestSales / shiftSalesPerServer);
                DiningAreaServerCounts[area] = roundedDownServers;
                serversAssigned += roundedDownServers;
                if(roundedDownServers == 0)
                {
                    areaPerServerSales[area] = -1f;
                }
                else
                {
                    areaPerServerSales[area] = (float)(area.TestSales / roundedDownServers);
                }
               

            }
            minimumServersAssigned = serversAssigned;
            ServerRemainder = ServerCount - minimumServersAssigned;
            for (int i = 0; i < ServerRemainder; i++)
            {
                float smallestImpact = float.MaxValue;
                DiningArea areaToAddTo = null;

                foreach (DiningArea diningArea in shift.DiningAreasUsed)
                {
                    float currentDifferenceFromAvg = areaPerServerSales[diningArea] - shiftSalesPerServer;
                    float salesIfServerAdded = diningArea.TestSales / (DiningAreaServerCounts[diningArea] + 1);
                    float newSalesPerServer = Math.Abs(salesIfServerAdded - shiftSalesPerServer);

                    if (newSalesPerServer < smallestImpact)
                    {
                        smallestImpact = newSalesPerServer;
                        areaToAddTo = diningArea;
                    }
                }

                if (areaToAddTo != null)
                {
                    DiningAreaServerCounts[areaToAddTo]++;
                    areaPerServerSales[areaToAddTo] = areaToAddTo.TestSales / DiningAreaServerCounts[areaToAddTo];
                }
            }
            ServerDistribution = new Dictionary<DiningArea, int>();
            ServerDistribution = DiningAreaServerCounts;
            AreaPerServerSales = new Dictionary<DiningArea, float>();
            AreaPerServerSales = areaPerServerSales;
            return DiningAreaServerCounts;
        }
        private void assignBartenders(List<Server> bartenders, List<Floorplan> floorplans)
        {
            int j = 0;
            for(int i = 0; i < bartenders.Count; i++)
            {               
                shift.SelectedFloorplan = floorplans[j];
                Server s = bartenders[i];
                Floorplan f = floorplans[j];    
                if ((ServerDistribution[floorplans[j].DiningArea] - floorplans[j].Servers.Count) > 0)
                {
                    shift.AddServerToSelectedFloorplan(bartenders[i]);
                   
                }
                else
                {
                    i--;
                }
                j++;
                if (j >= floorplans.Count)
                {
                    j = 0;
                }

            }
        }
        public void AssignCocktailers()
        {            
            int CocktailersNeeded = 0;            
            List<Server> unassignedBartenders = shift.UnassignedServers.Where(s => s.IsBartender).ToList();
            List<Floorplan> cocktailAreas = shift.Floorplans.Where(fp => fp.DiningArea.IsCocktail).ToList();
            assignBartenders(unassignedBartenders, cocktailAreas);
            foreach(Floorplan fp in cocktailAreas)
            {
                    CocktailersNeeded += ServerDistribution[fp.DiningArea];                    
                    CocktailersNeeded -= fp.Servers.Count();
            }
           
            List<Server> Cocktailers = shift.UnassignedServers
                .OrderByDescending(s => s.CocktailPreference)
                .Take(CocktailersNeeded)
                .ToList();
            
           
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
        public static void AssignServerSections(Floorplan floorplan)
        {
            floorplan.OrderSectionsByAvgSales();
            List<Server> orderedServers = floorplan.ServersWithoutSection.OrderByDescending(s => s.PreferedSectionWeight).ToList();
            List<Section> unassignedSections = floorplan.UnassignedSections;
            int serverIndex = 0;
            int openSectionSpots = unassignedSections.Sum(s => s.ServerCount);
            orderedServers = ShuffleServersWithSameSectionRanking(orderedServers);
            for (int i = 0; i < openSectionSpots && serverIndex < orderedServers.Count; i++)
            {
                if (unassignedSections[i].IsPickUp)
                {
                    continue;
                }
                while (unassignedSections[i].ServerTeam.Count < unassignedSections[i].ServerCount)
                {
                    unassignedSections[i].AddServer(orderedServers[serverIndex]);
                    serverIndex++;
                }
               
            }
        }
        private static List<Server> ShuffleServersWithSameSectionRanking(List<Server> servers)
        {
            Dictionary<int, List<Server>> sortedServers = new Dictionary<int, List<Server>>();
            foreach(Server server in servers)
            {
                if(!sortedServers.TryGetValue(server.PreferedSectionWeight, out List<Server> serverList))
                {
                    serverList = new List<Server>();
                    sortedServers[server.PreferedSectionWeight] = serverList;
                }
               
                sortedServers[server.PreferedSectionWeight].Add(server);
                
            }
            List<Server> shuffledOrderedServers = new List<Server>();
            foreach (int key in sortedServers.Keys)
            {
                Shuffle(sortedServers[key]);
                shuffledOrderedServers.AddRange(sortedServers[key]);
            }
            return shuffledOrderedServers;


        }
        static void Shuffle<T>(List<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

    }
}
