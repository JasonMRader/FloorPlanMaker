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
        }
        public ShiftManager() { }
        public bool IsAM { get; set; }
        public DateOnly DateOnly { get; set; }
        public DiningArea? SelectedDiningArea { get; set; }
        public Floorplan? SelectedFloorplan { get; set; }
        //public Floorplan? ViewedFloorplan { get; set; }
        public List<DiningArea> DiningAreasUsed => Floorplans.Select(fp => fp.DiningArea).Distinct().ToList();
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
        public bool ContainsFloorplan(DateOnly date, bool isLunch, int ID)
        {
            return Floorplans.Any(fp => fp.DateOnly == date &&
                                        fp.IsLunch == isLunch &&
                                        fp.DiningArea.ID == ID);
        }
        public void SetSelectedFloorplan(DateOnly date, bool isLunch, int ID)
        {
            SelectedFloorplan = Floorplans.FirstOrDefault(fp => fp.DateOnly == date &&
                                                       fp.IsLunch == isLunch &&
                                                       fp.DiningArea.ID == ID);
        }

        public List<Server> ServersNotOnShift = new List<Server>();
        public List<Server> UnassignedServers = new List<Server>();
        public List<Server> AllServers = new List<Server>();
        public List<Section> Sections = new List<Section>();
        public void UpdateUnassignedServers()
        {
            var assignedServers = new HashSet<Server>(ServersOnShift);
            UnassignedServers = AllServers.Where(server => !assignedServers.Contains(server)).ToList();
        }
        public void AddServerToFloorplan(Server server, Floorplan floorplan)
        {
            // Logic to add server to the floorplan
            // ...
            UpdateUnassignedServers(); // Update unassigned servers list
        }

        public void RemoveServerFromFloorplan(Server server, Floorplan floorplan)
        {
            // Logic to remove server from the floorplan
            // ...
            UpdateUnassignedServers(); // Update unassigned servers list
        }
        public void AddServerToAllServers(Server server)
        {
            AllServers.Add(server);
            UpdateUnassignedServers(); // Update unassigned servers list
        }

        public void RemoveServerFromAllServers(Server server)
        {
            AllServers.Remove(server);
            UpdateUnassignedServers(); // Update unassigned servers list
        }

        public Section? SectionSelected { get; set; }
        public List<Floorplan> Floorplans = new List<Floorplan>();
        public List<FloorplanTemplate> Templates = new List<FloorplanTemplate>();
        public List<Section> TemplateSections = new List<Section>();
        public FloorplanTemplate? SelectedTemplate { get; set; }
        
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
                Floorplans = new List<Floorplan>();
            }

            Floorplans.Add(newFloorplan);

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
