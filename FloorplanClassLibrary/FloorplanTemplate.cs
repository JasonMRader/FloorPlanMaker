using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class FloorplanTemplate
    {
        public FloorplanTemplate(Floorplan floorplan, string name, List<SectionLine> sectionLines)
        {
            this.Name = name;
            this.DiningArea = floorplan.DiningArea;
            this.ServerCount = floorplan.ServerCount;
            this.Sections = floorplan.Sections;
            this.SectionLines = sectionLines;
            this.DiningAreaID = floorplan.DiningArea.ID;
        }
        public FloorplanTemplate(DiningArea diningArea, string name, List<Section> sections, List<SectionLine> sectionLines)
        {
            this.Name = name;
            this.DiningArea = diningArea;
            
            this.Sections = sections;
            this.SectionLines = sectionLines;
            this.DiningAreaID = diningArea.ID;
        }
        public FloorplanTemplate(DiningArea diningArea, int serverCount,string name, List<Section> sections)
        {
            this.DiningArea = diningArea;
            this.ServerCount = serverCount;
            this.Name = name;
            this.Sections = sections;
        }
        public FloorplanTemplate() { }
        public int ID { get; set; }
        public string Name { get; set; }
        public DiningArea DiningArea { get; set; }
        public int DiningAreaID { get; set; }

        public int ServerCount
        {
            get
            {
                return this.ServerCount;
            }
            set
            {
                int servers = 0;
                if(this.Sections.Count > 0)
                {
                    foreach(Section section in this.Sections)
                    {
                        servers++;
                        if(section.IsTeamWait)
                        {
                            servers++;
                        }
                    }
                }
            }
        }
        public List<Section> Sections { get; set; }
        public List<SectionLine> SectionLines = new List<SectionLine>();
    }
}
