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
            
            this.ServerCount = template.ServerCount;
            CopyTemplateSections(template.Sections);
        }
        public Floorplan() { }
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public DateOnly DateOnly => new DateOnly(Date.Year, Date.Month, Date.Day);
        public bool IsLunch { get; set; }
        public DiningArea? DiningArea { get; set; }
        public int? DiningAreaID { get; set; }

        public List<Server> Servers
        {
            get
            {
                // Fetch servers from all sections
                var serversFromSections = Sections.SelectMany(s => new List<Server> { s.Server })
                                                  .Where(s => s != null)
                                                  .Distinct()
                                                  .ToList();

                // Concatenate with UnassignedServers and ensure uniqueness
                return ServersWithoutSection.Concat(serversFromSections)
                                        .Distinct()
                                        .ToList();
            }
        }
        public List<Server> ServersWithoutSection 
        { 
            get
            {
                return _serversWithoutSection;
            } 
            private set { _serversWithoutSection = value; }
        }
        private List<Server> _serversWithoutSection = new List<Server>();
        private List<Section> _sections = new List<Section>();
        public List<Section> Sections
        {
            get { return _sections; }
            private set { _sections = value; }  
        }
        public float MaxCoversPerServer
        {
            get
            {
                if (DiningArea != null && Servers.Count > 0)
                {
                    return (float)this.DiningArea.GetMaxCovers() / this.Servers.Count;
                }
                else
                {
                    return 0;
                }
               
            }
        }

        public float AvgSalesPerServer
        {
            get
            {
                if (DiningArea != null && Servers.Count > 0)
                    return DiningArea.GetAverageCovers() / this.Servers.Count;
                return 0;
            }
        }
        public void CopyTemplateSections(List<Section> sections)
        {
            this.Sections.Clear();
            foreach (Section section in sections)
            {
                Section SectionCopy = new Section(section);
                Sections.Add(SectionCopy);
            }
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

            // Find the first missing section number
            int newSectionNumber = 1;
            while (_sections.Any(s => s.Number == newSectionNumber))
            {
                newSectionNumber++;
            }

            // Assign the missing number to the new section
            section.Number = newSectionNumber;

            // Set the section name if the server is null
            if (section.Server == null)
            {
                section.Name = $"Section {section.Number}";
            }

            // Add the section to the list
            _sections.Add(section);
        }

        public void CreateSectionsForServers()
        {
            if (this.Servers != null)
            {
                foreach (Server server in Servers)
                {
                    Section newSection = new Section(this);
                   
                    AddSection(newSection);
                }
            }
            
        }
        public void DeleteSection(Section section)
        {
            if (section.Tables != null || section.Server != null)
            {
                UnassignSection(section);
            }
            var sectionToRemove = Sections.Where(s => s.Number == section.Number).FirstOrDefault();
            Sections.Remove(sectionToRemove);
        }
        public void UnassignSection(Section section)
        {
            if (section != null)
            {
                if(section.IsTeamWait == false)
                {
                    if (section.Server != null)
                    {
                        this.ServersWithoutSection.Add(section.Server);
                        section.RemoveServer(section.Server);
                    }
                }
                else
                {
                    foreach(Server server in section.ServerTeam)
                    {
                        this.ServersWithoutSection.Add(server);
                    }
                    if (section.Server != null && !ServersWithoutSection.Contains(section.Server))
                    {
                        this.ServersWithoutSection.Add(section.Server);
                        section.RemoveServer(section.Server);
                    }
                }
               
                    
                
                section.ClearTables();               
                section.IsCloser = false;
                section.IsPre = false;
            }
        }
        public void AddServerAndSection(Server server)
        {
            this.ServersWithoutSection.Add(server);
            Section newSection = new Section(this);

            AddSection(newSection);
        }
        public void RemoveServerAndSection(Server server)
        {
            this.ServersWithoutSection.Remove(server);
            RemoveHighestNumberedEmptySection();
        }

       
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
        public Section RemoveHighestNumberedEmptySection()
        {
            var sectionToRemove = Sections
                .Where(s => s.Server == null
                         && (s.Tables == null || !s.Tables.Any())
                         && !s.IsPickUp
                         && !s.IsTeamWait)
                .OrderByDescending(s => s.Number)
                .FirstOrDefault();

            if (sectionToRemove != null)
            {
                Sections.Remove(sectionToRemove);
            }
            return sectionToRemove;
        }
        public bool CheckIfCloserIsAssigned()
        {
            bool closerAssigned = false;
            foreach (var s in Sections)
            {
                if(s.IsCloser)
                {
                    closerAssigned = true;
                }
            }
            return closerAssigned;
        }
        public bool CheckIfAllSectionsAssigned()
        {
            bool allSectionsAssigned = true;
            foreach (var section in Sections)
            {
                if (!section.IsPickUp && section.Server == null)
                {
                    allSectionsAssigned = false;
                }

            }
            return allSectionsAssigned;
        }
    }
}
