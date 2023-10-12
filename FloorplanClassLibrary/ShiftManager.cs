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
        public DiningArea? SelectedDiningArea { get; set; }
        public Floorplan? SelectedFloorplan { get; set; }
        public List<DiningArea> DiningAreasUsed = new List<DiningArea>();
        public List<Server> ServersOnShift = new List<Server>();
        public List<Server> ServersNotOnShift = new List<Server>();
        public List<Server> UnassignedServers = new List<Server>();
        public List<Section> Sections = new List<Section>();
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
        }
        public void SetFloorplansToPM()
        {
            foreach (var floorplan in Floorplans)
            {
                floorplan.IsLunch = false;
            }
        }
    }
}
