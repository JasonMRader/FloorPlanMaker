using FloorplanClassLibrary;
using FloorplanUserControlLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class TemplateManager
    {
        public DiningArea DiningArea { get; set; }
        public int serverCount = 5;
        public List<MiniTableControl> MiniTableControls { get; set; } = new List<MiniTableControl>();
        public Panel[] DisplayPanels {  get; set; }
       

        private bool _filterTeamYes { get; set; } = false;        
        private bool _filterPickYes { get; set; } = false;
        private bool _hasTeamFilter { get; set; } = false;
        private bool _hasPickFilter { get; set; } = false;
        public Dictionary<FloorplanTemplate, List<MiniTableControl>> TemplateMiniTables { get; set; } 
            = new Dictionary<FloorplanTemplate, List<MiniTableControl>>();
        public bool HasTeamFilter
        {
            get { return _hasTeamFilter; }
            set
            {
                _hasTeamFilter = value;
                SetFilter();
            }
        }

        public bool FilterTeamYes
        {
            get { return _filterTeamYes; }
            set
            {
                _filterTeamYes = value;
                if (_hasTeamFilter)
                    SetFilter();
            }
        }

        public bool HasPickFilter
        {
            get { return _hasPickFilter; }
            set
            {
                _hasPickFilter = value;
                SetFilter();
            }
        }

        public bool FilterPickYes
        {
            get { return _filterPickYes; }
            set
            {
                _filterPickYes = value;
                if (_hasPickFilter)
                    SetFilter();
            }
        }
        public void SetFilter()
        {
            FilterOption option = FilterOption.None;

            if (_hasTeamFilter && _hasPickFilter)
            {
                if (_filterTeamYes && _filterPickYes)
                    option = FilterOption.TeamTrue_And_PickTrue;
                else if (_filterTeamYes && !_filterPickYes)
                    option = FilterOption.TeamTrue_And_PickFalse;
                else if (!_filterTeamYes && _filterPickYes)
                    option = FilterOption.TeamFalse_And_PickTrue;
                else
                    option = FilterOption.TeamFalse_And_PickFalse;
            }
            else if (_hasTeamFilter)
            {
                option = _filterTeamYes ? FilterOption.TeamWaitTrueOnly : FilterOption.TeamWaitFalseOnly;
            }
            else if (_hasPickFilter)
            {
                option = _filterPickYes ? FilterOption.PickUpTrueOnly : FilterOption.PickUpFalseOnly;
            }

            SetFilters(option);
        }
        public TemplateManager(DiningArea area)
        {
            this.DiningArea = area;
        }
        public List<FloorplanTemplate> Templates = new List<FloorplanTemplate>();
        public List<FloorplanTemplate> FilteredList = new List<FloorplanTemplate>();
        //public void GetMiniTableControls()
        //{
        //    foreach(Panel panel in DisplayPanels)
        //    {
        //        foreach (Table table in DiningArea.Tables)
        //        {
        //            MiniTableControl miniTable = new MiniTableControl(table, .4f, 27);
        //            MiniTableControls.Add(miniTable);
        //            panel.Controls.Add(miniTable);
        //        }
        //    }
            
        //}
        public void InitializeMiniTableControls(FloorplanTemplate template)
        {
            //MiniTableControls = new List<MiniTableControl>();
            //TemplateMiniTables = new Dictionary<FloorplanTemplate, List<MiniTableControl>>();
            List<MiniTableControl> miniTableControls = new List<MiniTableControl>();
            template.GetTemplateTables();

            foreach (var table in template.Tables)
            {
                // Assuming 'factor' and 'yAdjustment' are determined based on your UI layout
                float factor = .4f; // Example scaling factor
                int yAdjustment = 27; // Example Y-coordinate adjustment

                MiniTableControl miniTableControl = new MiniTableControl(table, factor, yAdjustment);
                miniTableControls.Add(miniTableControl);
            }
            TemplateMiniTables[template] = miniTableControls;
            //AddMiniTableControlsToDisplayPanels();
        }
        public void DisplayMiniTableControls(FloorplanTemplate selectedTemplate, Panel panel)
        {
            if (TemplateMiniTables.TryGetValue(selectedTemplate, out List<MiniTableControl> miniTables))
            {
                foreach (var miniTable in miniTables)
                {
                    panel.Controls.Add(miniTable);
                }
            }
        }
        public void AddToTemplateMiniTables(FloorplanTemplate template)
        {
            List<MiniTableControl> miniTableControls = new List<MiniTableControl>();
            foreach (var table in template.Tables)
            {
                // Assuming 'factor' and 'yAdjustment' are determined based on your UI layout
                float factor = .4f; // Example scaling factor
                int yAdjustment = 27; // Example Y-coordinate adjustment

                MiniTableControl miniTableControl = new MiniTableControl(table, factor, yAdjustment);
                miniTableControls.Add(miniTableControl);
            }
            TemplateMiniTables[template] = miniTableControls;
        }

        private void AddMiniTableControlsToDisplayPanels()
        {
            // Clear existing controls in the display panels
            foreach (var panel in DisplayPanels)
            {
                panel.Controls.Clear();
            }

            // Add new mini table controls to the display panels
            foreach (var miniTableControl in MiniTableControls)
            {
                // Determine which panel to add the control to based on some logic
                var targetPanel = DisplayPanels[0]; // Example assignment
                targetPanel.Controls.Add(miniTableControl);
            }
        }

        public enum FilterOption
        {
            None,
            TeamWaitTrueOnly,
            TeamWaitFalseOnly,
            PickUpTrueOnly,
            PickUpFalseOnly,
            TeamTrue_And_PickFalse,
            TeamTrue_And_PickTrue,
            TeamFalse_And_PickFalse,
            TeamFalse_And_PickTrue
        }
        public void GetTemplatesForFloorplan(Floorplan floorplan)
        {
            this.Templates.Clear();
            this.Templates = SqliteDataAccess.LoadTemplatesByDiningAreaAndServerCount(floorplan.DiningArea, floorplan.Servers.Count);
            UpdateSectionNumbers();
        }
        public void GetAllFloorplanTemplates()
        {
            this.Templates.Clear();
            this.Templates = SqliteDataAccess.LoadAllFloorplanTemplates();
            UpdateSectionNumbers();

        }
        private void UpdateSectionNumbers()
        {
            foreach(FloorplanTemplate template in  this.Templates)
            {
                template.AssignSectionNumbers();
                //template.GetTemplateTables();
                InitializeMiniTableControls(template);
            }
        }
        
        public void SetFilters(FilterOption option)
        {
            switch(option)
            {
                case FilterOption.TeamWaitTrueOnly:
                    FilterTemplates(serverCount, hasTeamWait: true);
                    break;
                case FilterOption.TeamWaitFalseOnly:
                    FilterTemplates(serverCount, hasTeamWait: false);
                    break;
                case FilterOption.PickUpTrueOnly:
                    FilterTemplates(serverCount, hasPickUp: true);
                    break;
                case FilterOption.PickUpFalseOnly:
                    FilterTemplates(serverCount, hasPickUp: false); 
                    break;
                case FilterOption.TeamTrue_And_PickFalse:
                    FilterTemplates(serverCount, hasTeamWait: true, hasPickUp: false);
                    break;
                case FilterOption.TeamTrue_And_PickTrue:
                    FilterTemplates(serverCount, hasTeamWait: true, hasPickUp: true);
                    break;
                case FilterOption.TeamFalse_And_PickFalse:
                    FilterTemplates(serverCount, hasTeamWait: false, hasPickUp: false);
                    break;
                case FilterOption.TeamFalse_And_PickTrue:
                    FilterTemplates(serverCount, hasTeamWait: false, hasPickUp: true);
                    break;
                case FilterOption.None:
                default:
                    FilterTemplates(serverCount);
                    break;





            }
        }
        public void FilterTemplates(int serverCount, bool? hasTeamWait = null, bool? hasPickUp = null)
        {
            // Start with all templates
            FilteredList.Clear();
            var filteredTemplates = this.Templates.AsQueryable();
            filteredTemplates = filteredTemplates.Where(t => t.ServerCount == serverCount);

            // Filter by HasTeamWait if specified
            if (hasTeamWait.HasValue)
            {
                filteredTemplates = filteredTemplates.Where(t => t.HasTeamWait == hasTeamWait.Value);
            }

            // Filter by HasPickUp if specified
            if (hasPickUp.HasValue)
            {
                filteredTemplates = filteredTemplates.Where(t => t.HasPickUp == hasPickUp.Value);
            }

            FilteredList = filteredTemplates.ToList();
        }

    }
}
