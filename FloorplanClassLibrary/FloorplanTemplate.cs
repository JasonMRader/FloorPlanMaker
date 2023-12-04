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
            //AssignSectionNumbers();
            //GetTemplateTables();
        }
        public List<Section> Sections = new List<Section>();
        public Dictionary<Section, List<Control>> 
            SectionMiniTableControls = new Dictionary<Section, List<Control>>(); 
        public int ID { get; set; }

        private int _teamWaitSections;
                   
       
        private bool _hasTeamWait = false;
        private bool _hasPickUp = false;
        public void SetTeamWaitValue(bool teamWait)
        {
            _hasTeamWait = teamWait;
        }
        public void SetPickUpValue(bool picUp)
        {
            _hasPickUp = picUp;
        }
        public List<TemplateTable> Tables { get; private set; }
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
            this.Tables = new List<TemplateTable>();
            foreach (Section originalSection in sectionsToCopy)
            {  
                Section sectionCopy = new Section(originalSection);
                this.Sections.Add(sectionCopy);
                //foreach (Table table in sectionCopy.Tables)
                //{
                //    TemplateTable templateTable = new TemplateTable(table, sectionCopy);
                //    this.Tables.Add(templateTable);
                //}
            }
            UpdateTeamWaitAndPickUp();
            
        }
        public void GetTemplateTables()
        {
            this.Tables = new List<TemplateTable>();
            this.Tables.Clear();
            foreach (Section section in this.Sections) 
            {
                foreach (Table table in section.Tables)
                {
                    TemplateTable templateTable = new TemplateTable(table, section,.4f,27);
                    this.Tables.Add(templateTable);
                }
            }
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
            //GetTemplateTables();
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
