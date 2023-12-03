using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMakerUI
{
    public class TemplateManager
    {

        public TemplateManager() { }
        public List<FloorplanTemplate> Templates = new List<FloorplanTemplate>();
        public List<FloorplanTemplate> FilteredList = new List<FloorplanTemplate>();
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
            }
        }
        
        public void FilterTemplates(int serverCount, bool? hasTeamWait = null, bool? hasPickUp = null)
        {
            // Start with all templates
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
