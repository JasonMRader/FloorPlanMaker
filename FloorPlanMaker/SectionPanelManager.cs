using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class SectionPanelManager
    {
        private List<SectionPanelControl> _sectionPanels = new List<SectionPanelControl>();
        public List<SectionPanelControl> SectionPanels { get {  return _sectionPanels; } }
        private Floorplan _floorplan { get; set; }
        public Floorplan Floorplan { get {  return _floorplan; } }
    }
}
