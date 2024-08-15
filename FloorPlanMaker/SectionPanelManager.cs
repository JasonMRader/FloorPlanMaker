using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class SectionPanelManager : IFloorplanObserver
    {
        private List<SectionPanelControl> _sectionPanels = new List<SectionPanelControl>();
        public List<SectionPanelControl> SectionPanels { get {  return _sectionPanels; } }
        private Floorplan _floorplan { get; set; }
        public Floorplan Floorplan { get {  return _floorplan; } }
        private FlowLayoutPanel _flowLayoutPanel = new FlowLayoutPanel();
        public SectionPanelManager(Floorplan floorplan, FlowLayoutPanel flowLayoutPanel)
        {
            _floorplan = floorplan;
            
            _flowLayoutPanel = flowLayoutPanel;
            if(floorplan != null)
            {
                AddSectionPanels();
            }
        }
        public void ChangeFloorplan(Floorplan floorplan)
        {

        }

        public void UpdateFloorplan(Section section)
        {
            throw new NotImplementedException();
        }
        public void AddSectionPanels()
        {
            foreach (SectionPanelControl sectionPanel in this._sectionPanels)
            {
                sectionPanel.UpdateLabels();
            }
        }
    }
}
