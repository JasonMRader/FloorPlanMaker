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
        private List<SectionInfoPanel> _sectionPanels = new List<SectionInfoPanel>();
        public List<SectionInfoPanel> SectionPanels { get {  return _sectionPanels; } }
        private Floorplan _floorplan { get; set; }
        public Floorplan Floorplan { get {  return _floorplan; } }
        private FlowLayoutPanel _flowLayoutPanel = new FlowLayoutPanel();
        public SectionPanelManager(Floorplan floorplan, FlowLayoutPanel flowLayoutPanel)
        {
            _floorplan = floorplan;
            floorplan.SubscribeObserver(this);
            floorplan.SectionRemoved += RemoveSection;
            floorplan.SectionAdded += AddSection;
            _flowLayoutPanel = flowLayoutPanel;
            if(floorplan != null)
            {
                AddSectionPanels();
            }
        }

        private void AddSection(Section sectionAdded, Floorplan floorplan)
        {
            throw new NotImplementedException();
        }

        private void RemoveSection(Section sectionRemoved, Floorplan floorplan)
        {
            throw new NotImplementedException();
        }

        public void ChangeFloorplan(Floorplan floorplan)
        {

        }

        public void UpdateFloorplan(Floorplan floorplan)
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
