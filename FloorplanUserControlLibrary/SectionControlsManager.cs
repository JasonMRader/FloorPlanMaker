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
        public List<SectionLabelControl>? SectionControls { get; set; }
        public Floorplan Floorplan { get; set; }
        public List<Server>? UnassignedServers { get; set; }
        
        public void SetSelectedSection(Section section)
        {
            foreach(SectionLabelControl sc in this.SectionControls)
            {
                if (sc.Section == section)
                {
                    sc.isSelecteed = true;
                }
                else
                {
                    sc.isSelecteed = false;
                }
                sc.Invalidate();
            }
        }
        public void CreateSectionControls()
        {
            this.SectionControls = new List<SectionLabelControl>();
            foreach (Section section in Floorplan.Sections)
            {
                if (section.Tables.Count > 0)
                {
                    SectionLabelControl sectionControl = new SectionLabelControl(section, this);
                    this.SectionControls.Add(sectionControl);
                }
                if(section.Server != null)
                {
                    UnassignedServers.Remove(section.Server);
                }
                

            }
        }
    }
}
