using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FloorplanClassLibrary
{
    public class Floorplan
    {
        public Floorplan(DiningArea diningArea, DateTime date, bool isLunch, int serverCount, int sectionCount)
        {
            this.DiningArea = diningArea;
            this.Date = date;
            this.IsLunch = isLunch;
            this.ServerCount = serverCount;
            this.SectionCount = sectionCount;
            CreateSections();
        }
        public Floorplan(FloorplanTemplate template)
        {
            this.DiningArea = template.DiningArea;
            this.Sections = template.Sections;
            this.ServerCount = template.ServerCount;
        }
        public Floorplan() { }
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public bool IsLunch { get; set; }
        public DiningArea? DiningArea { get; set; }
        public int? DiningAreaID { get; set; }
       

        private List<Section> _sections = new List<Section>();
        public List<Section> Sections
        {
            get { return _sections; }
            private set { _sections = value; }  
        }
        public void CopySectionsIntoSections(List<Section> sections)
        {
            foreach (Section section in sections)
            {
                this.AddSection(section);
            }
        }
        public void AddSection(Section section)
        {
            
            section.Number = _sections.Count + 1;

            _sections.Add(section);
        }

        public List<Server> Servers = new List<Server>();
        private int _serverCount = 0;
        public int ServerCount
        {
            get { return _serverCount; }
            set
            {
                if (value < 0) // Optional check if you don't want negative values
                    _serverCount = 0;
                else
                    _serverCount = value;
            }
        }

        private int _sectionCount = 0;
        public int SectionCount
        {
            get { return _sectionCount; }
            set
            {
                if (value < 0) // Optional check if you don't want negative values
                    _sectionCount = 0;
                else
                    _sectionCount = value;
            }
        }
        public void CreateSections ()
        {
            Sections.Clear();
            if (ServerCount == SectionCount)
            {
                for (int i = 0; i < SectionCount; i++) 
                {
                    Section s = new Section(this);
                    s.Number = i +1 ;
                    s.Name = $"Section {i +1}";
                    Sections.Add(s);
                }
            }    
        }
    }
}
