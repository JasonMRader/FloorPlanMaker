using FloorplanClassLibrary;
using FloorPlanMaker;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
   public class FloorplanFormManager
    {
        public Floorplan? Floorplan;
        public ShiftManager ShiftManager;
        private List<TableControl> _tableControls = new List<TableControl>();    
        private List<SectionLabelControl> _sectionLabels = new List<SectionLabelControl>();
        private List<SectionPanelControl> _sectionPanels = new List<SectionPanelControl>();
        private List<ServerControl> _serverControls = new List<ServerControl>();
        public FloorplanFormManager(ShiftManager shiftManager)
        {
            this.Floorplan = shiftManager.SelectedFloorplan;
            this.ShiftManager = shiftManager;
        }
        public List<TableControl> TableControls
        {
            get {  return _tableControls; }
            set { _tableControls = value; }                
            
        }
        public List<SectionLabelControl> SectionLabels
        {
            get { return _sectionLabels; }
            set { _sectionLabels = value; }
        }
        public List<SectionPanelControl> SectionPanels
        {
            get { return _sectionPanels; }
            set { _sectionPanels = value; }
        }
        public List<ServerControl> ServerControls
        {
            get { return _serverControls; }
            set { _serverControls = value; }
        }
        public void SetTableControls()
        {
            _tableControls.Clear();
            if (this.ShiftManager != null && this.ShiftManager.SelectedDiningArea != null)
            {
                foreach (Table table in this.ShiftManager.SelectedDiningArea.Tables)
                {
                    table.DiningArea = this.ShiftManager.SelectedDiningArea;
                    TableControl tableControl = TableControlFactory.CreateTableControl(table);
                    _tableControls.Add(tableControl);
                }
            }
        }
        public void SetSectionLabels()
        {
            _sectionLabels.Clear();
            foreach (Section section in Floorplan.Sections)
            {
                if (section.Tables.Count > 0)
                {
                    SectionLabelControl sectionControl = new SectionLabelControl(section, Floorplan.ServersWithoutSection);
                    this._sectionLabels.Add(sectionControl);
                }
                if (section.Server != null)
                {
                    Floorplan.ServersWithoutSection.Remove(section.Server);
                }
            }
        }
        public void SetSectionPanels()
        {
            _sectionPanels.Clear();
            foreach(Section section in Floorplan.Sections)
            {
                SectionPanelControl sectionPanel = new SectionPanelControl(section, this.Floorplan);
                this._sectionPanels.Add(sectionPanel);
            }
        }
        
        public void UpdateServerControls()
        {

        }
        
       
    }
}
