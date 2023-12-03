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
        
       
        public FloorplanTemplate(Floorplan floorplan)
        {
            
            this.DiningArea = floorplan.DiningArea;
            this.ServerCount = floorplan.Servers.Count;
            GetSectionCopies(floorplan.Sections);
           
            this.DiningAreaID = floorplan.DiningArea.ID;
            //AssignSectionNumbers();
        }
        public FloorplanTemplate() 
        {
            
        }
        public List<Section> Sections = new List<Section>();
        public int ID { get; set; }

        private int _teamWaitSections;
                   
       
        private bool _hasTeamWait = false;
        private bool _hasPickUp = false;
        public List<Table> Tables = new List<Table>();
        public bool HasPickUp
        {
            get { return _hasPickUp; }
        }
        public bool HasTeamWait
        {
            get { return _hasTeamWait; }           
        }
        public DiningArea DiningArea { get; set; }
        public int DiningAreaID { get; set; }
        private void GetSectionCopies(List<Section> sectionsToCopy)
        {
            this.Sections = new List<Section>();
            foreach (Section originalSection in sectionsToCopy)
            {  
                Section sectionCopy = new Section(originalSection);
                this.Sections.Add(sectionCopy);
            }
            UpdateTeamWaitAndPickUp();
            //AssignSectionNumbers();
        }

        private void UpdateTeamWaitAndPickUp()
        {
            int teamSections = 0;            
            foreach (var section in this.Sections)            {
                if (section.ServerCount > 1)
                {
                    section.MakeTeamWait();
                    section.DecreaseServerCount();
                    teamSections++;
                    this._hasTeamWait = true;
                }       
                if(section.IsPickUp)
                {
                    this._hasPickUp = true;
                }
            }
            _teamWaitSections = teamSections;           
        }
        public string Name { get; set; } = "";
        public void AssignSectionNumbers()
        {
            int sectionNumber = 1;
            foreach (var section in this.Sections)
            {
                section.Number = sectionNumber;
                sectionNumber++;
            }
        }

        public int TeamWaitSections
        {
            get { return _teamWaitSections; }            
        }
        
        public int ServerCount { get; }      
        
        public string GetDisplay()
        {
            return "Servers: " + this.ServerCount.ToString();
        }
    }
}
