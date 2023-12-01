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
            //this.Name = name;
            this.DiningArea = floorplan.DiningArea;
            this.ServerCount = floorplan.ServerCount;
            this.Sections = floorplan.Sections;
            //this.SectionLines = sectionLines;
            this.DiningAreaID = floorplan.DiningArea.ID;
        }
        public FloorplanTemplate(Floorplan floorplan)
        {
            
            this.DiningArea = floorplan.DiningArea;

            GetSectionCopies(floorplan.Sections);
           
            this.DiningAreaID = floorplan.DiningArea.ID;
        }
        public FloorplanTemplate(DiningArea diningArea, string name, List<Section> sections, List<SectionLine> sectionLines)
        {
            //this.Name = name;
            this.DiningArea = diningArea;
            
            this.Sections = sections;
            //this.SectionLines = sectionLines;
            this.DiningAreaID = diningArea.ID;
        }
        public FloorplanTemplate(DiningArea diningArea, int serverCount,string name, List<Section> sections)
        {
            this.DiningArea = diningArea;
            this.ServerCount = serverCount;
            //this.Name = name;
            this.Sections = sections;
        }
        public FloorplanTemplate() { }
        public int ID { get; set; }
        private string _name;
        private int _teamWaitSections;
        private int _serverCount;
        private void GetSectionCopies(List<Section> sectionsToCopy)
        {
            foreach (Section section in sectionsToCopy)
            {
                Section sectionCopy = new Section(section);
                this.Sections.Add(sectionCopy);
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = "Servers: " + ServerCount.ToString("F0") + ", Teamwait Sections: "
                        + TeamWaitSections.ToString("F0");
            }
        }

        public int TeamWaitSections
        {
            get { return _teamWaitSections; }
            set
            {
                int teamSections = 0;
                if (this.Sections == null) { return; }
                foreach (var section in this.Sections)
                {
                    if (section.IsTeamWait)
                    {
                        teamSections++;
                    }
                }
                _teamWaitSections = teamSections;
            }
        }
        public DiningArea DiningArea { get; set; }
        public int DiningAreaID { get; set; }

        public int ServerCount
        {
            get { return _serverCount; }
            set
            {
                int servers = 0;
                if (this.Sections != null)
                {
                    foreach (Section section in this.Sections)
                    {
                        servers += section.ServerTeam.Count;
                    }
                }
                _serverCount = servers;
            }
        }
        public List<Section> Sections { get; set; }
        //public List<SectionLine> SectionLines = new List<SectionLine>();
    }
}
