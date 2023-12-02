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
        
        public List<FloorplanTemplate> FilterTemplates(List<FloorplanTemplate> templates, bool? hasTeamWait = null, bool? hasPickUp = null)
        {
            // Start with all templates
            var filteredTemplates = templates.AsQueryable();

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

            return filteredTemplates.ToList();
        }

    }
}
