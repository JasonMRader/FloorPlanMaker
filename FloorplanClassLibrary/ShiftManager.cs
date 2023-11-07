using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ShiftManager
    {
        public ShiftManager(DiningArea diningArea)
        {
            SelectedDiningArea = diningArea;
            AllServers = SqliteDataAccess.LoadServers();
            ServersNotOnShift = this.AllServers;
        }
        public ShiftManager() 
        {
            AllServers = SqliteDataAccess.LoadServers();
            ServersNotOnShift = this.AllServers;
        }
        public bool IsAM { get; set; }
        public DateOnly DateOnly { get; set; }
        public DiningArea? SelectedDiningArea { get; set; }
        public Floorplan? SelectedFloorplan { get; set; }
        //public Floorplan? ViewedFloorplan { get; set; }
        public List<DiningArea> DiningAreasUsed => Floorplans.Select(fp => fp.DiningArea).Distinct().ToList();
        public List<Server> ServersNotOnShift = new List<Server>();
        public List<Server> UnassignedServers = new List<Server>();
        public List<Server> AllServers = new List<Server>();
        public List<Section> Sections = new List<Section>();
        public Section? SectionSelected { get; set; }
        private List<Floorplan> floorplans = new List<Floorplan>();

        public IReadOnlyList<Floorplan> Floorplans => floorplans.AsReadOnly();
        public List<FloorplanTemplate> Templates = new List<FloorplanTemplate>();
        public List<Section> TemplateSections = new List<Section>();
        public FloorplanTemplate? SelectedTemplate { get; set; }
        public List<Server> ServersOnShift
        {
            get
            {
                // Get all servers from floorplans.
                var serversFromFloorplans = Floorplans.SelectMany(fp => fp.Servers);

                // Concatenate servers from floorplans and unassigned servers.
                return serversFromFloorplans.Concat(UnassignedServers).Distinct().ToList();
            }
        }
        public void AddFloorplanAndServers(Floorplan floorplan)
        {
            this.floorplans.Add(floorplan);

            // Create a hash set of server IDs for quicker lookup
            var assignedServerIds = new HashSet<int>(floorplan.Servers.Select(s => s.ID));

            // Remove all servers by ID that are in the floorplan's server list
            this.ServersNotOnShift.RemoveAll(server => assignedServerIds.Contains(server.ID));

            // Assuming ServersOnShift is not a HashSet, and you want to avoid duplicates:
            foreach (var server in floorplan.Servers)
            {
                if (!this.ServersOnShift.Any(s => s.ID == server.ID))
                {
                    this.ServersOnShift.Add(server);
                }
            }
            //ServersNotOnShift = ServersNotOnShift.OrderBy(s => s.Name).ToList();
        }

        public void RemoveFloorplan(Floorplan floorplan)
        {
            this.floorplans.Remove(floorplan);
            var assignedServersSet = new HashSet<Server>(floorplan.Servers);

            // Remove all servers that are in the floorplan's server list
            this.ServersOnShift.RemoveAll(assignedServersSet.Contains);
            this.ServersNotOnShift.AddRange(floorplan.Servers);
            //ServersNotOnShift = ServersNotOnShift.OrderBy(s => s.Name).ToList();

        }
        public void ClearFloorplans()
        {
            this.floorplans.Clear();
        }
        public bool ContainsFloorplan(DateOnly date, bool isLunch, int ID)
        {
            return Floorplans.Any(fp => fp.DateOnly == date &&
                                        fp.IsLunch == isLunch &&
                                        fp.DiningArea.ID == ID);
        }
        public void SetSelectedFloorplan(DateOnly date, bool isLunch, int ID)
        {
            SelectedFloorplan = floorplans.FirstOrDefault(fp => fp.DateOnly == date &&
                                                       fp.IsLunch == isLunch &&
                                                       fp.DiningArea.ID == ID);
        }

       
        
        
       
        
        public void AssignSectionNumbers(List<Section> sections)
        {
            int sectionNumber = 1;
            foreach (var section in sections)
            {
                section.Number = sectionNumber;
                sectionNumber++;
            }
        }
        
        public void SetSectionsToTemplate(FloorplanTemplate template)
        {
            this.TemplateSections.Clear();
            this.TemplateSections.AddRange(template.Sections);
        }
        public Floorplan CreateFloorplanForDiningArea(DiningArea diningArea,DateTime date, bool isLunch, int serverCount, int sectionCount)
        {
            Floorplan newFloorplan = new Floorplan(diningArea, date, isLunch, serverCount, sectionCount);
            //this.Sections.AddRange(newFloorplan.Sections);
             

            // Add to the Floorplans list
            if (Floorplans == null)
            {
                floorplans = new List<Floorplan>();
            }

            floorplans.Add(newFloorplan);

            return newFloorplan;
        }
        public void SetFloorplansToAM()
        {
            foreach (var floorplan in Floorplans)
            {
                floorplan.IsLunch = true;
            }
            this.IsAM = true;
        }
        public void SetFloorplansToPM()
        {
            foreach (var floorplan in Floorplans)
            {
                floorplan.IsLunch = false;
            }
            this.IsAM = false;
        }
    }
}
