using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class SectionControlsManager
    {
        public SectionControlsManager(Floorplan floorplan)
        {
            Floorplan = floorplan;
            UnassignedServers = floorplan.Servers;
            CreateSectionControls();
        }
        public List<SectionControl>? SectionControls { get; set; }
        public Floorplan Floorplan { get; set; }
        public List<Server>? UnassignedServers { get; set; }
       
        public void CreateSectionControls()
        {
            this.SectionControls = new List<SectionControl>();
            foreach (Section section in Floorplan.Sections)
            {
                SectionControl sectionControl = new SectionControl(section, this);
                this.SectionControls.Add(sectionControl);

            }
        }
    }
}
