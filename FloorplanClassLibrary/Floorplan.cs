using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace FloorplanClassLibrary
{
    public class Floorplan 
    {
        
        // TODO: remove server count, use Servers.Count, clean up logic
        public Floorplan(DiningArea diningArea, DateTime date, bool isLunch, int serverCount, int sectionCount)
        {
            this.DiningArea = diningArea;
            this.Date = date;
            this.IsLunch = isLunch;
            this.ServerCount = serverCount;
            this.SectionCount = sectionCount;
            this.SectionServerMap = new Dictionary<Section, List<Server>>();
            
            CreateSections();
            //MoveToNextSection();
            foreach (var section in Sections)
            {
                //section.SubscribeObserver(this);
            }
        }
        public FloorplanEdgesManager LinesManager { get; private set; }
        
        public Floorplan(FloorplanTemplate template)
        {
            this.DiningArea = template.DiningArea;
            
            this.ServerCount = template.ServerCount;
            SectionServerMap = new Dictionary<Section, List<Server>>();
            CopyTemplateSections(template.Sections);
            //MoveToNextSection();
            foreach (var section in Sections)
            {
                //section.SubscribeObserver(this);
            }
        }
        public Floorplan() 
        {
            SectionServerMap = new Dictionary<Section, List<Server>>();
            //MoveToNextSection();
            foreach (var section in Sections)
            {
                //section.SubscribeObserver(this);
            }
        }
        public Dictionary<Section, List <Server>> SectionServerMap { get; private set; }
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public DateOnly DateOnly => new DateOnly(Date.Year, Date.Month, Date.Day);
        public bool IsLunch { get; set; }
        public DiningArea? DiningArea { get; set; }
        public int? DiningAreaID { get; set; }
        private Section _sectionSelected { get; set; }
        public Section? SectionSelected
        {
            get 
            {
                if (this.Sections.Count == 0) return null;
                else return this.Sections[currentFocusedSectionIndex];
            }

        }
        private int currentFocusedSectionIndex = 0;
        public void SetSelectedSection(Section selectedSection)
        {
            foreach (Section section in this.Sections)
            {
                if (selectedSection == section)
                {
                    _sectionSelected = selectedSection;
                    currentFocusedSectionIndex = this.Sections.IndexOf(selectedSection);
                    HighlightSelectedSection();
                }
                else
                {
                    section.NotSelected();
                }
            }

        }
        public void RefreshSectionServerMap()
        {
            foreach(Section section in this.Sections)
            {
                foreach(Server server in section.ServerTeam)
                {
                    UpdateSectionServerMap(server, section);
                }
            }
        }
        public void UpdateSectionServerMap(Server server, Section section)
        {
            // Remove the server from any current section
            foreach (var entry in SectionServerMap)
            {
                if (entry.Value.Contains(server))
                {
                    entry.Value.Remove(server);
                    break; // Assuming a server can only be in one section at a time
                }
            }

            // Add the server to the new section
            if (!SectionServerMap.ContainsKey(section))
            {
                SectionServerMap[section] = new List<Server>();
            }
            if (!SectionServerMap[section].Contains(server))
            {
                SectionServerMap[section].Add(server);
            }
        }

        public void RemoveServerFromSection(Server server, Section section)
        {
            // Find the section that contains the server and remove the server from that section
            foreach (var entry in SectionServerMap)
            {
                if (entry.Value.Contains(server))
                {
                    entry.Value.Remove(server);
                    break; // Assuming a server can only be in one section at a time
                }
            }
        }

        public void MoveToNextSection()
        {

            if (this.Sections == null || this.Sections.Count == 0) return;

            // Increment the index and wrap around if needed
            currentFocusedSectionIndex = (currentFocusedSectionIndex + 1) % this.Sections.Count;

            // Directly set the selected section using the index
            _sectionSelected = this.Sections[currentFocusedSectionIndex];
            HighlightSelectedSection();
           

        }
        private void HighlightSelectedSection()
        {
            foreach (var section in this.Sections)
            {
                if (section == _sectionSelected)
                {
                    section.SetToSelected();
                }
                else
                {
                    section.NotSelected();
                }
            }
        }
        public List<Server> Servers
        {
            get
            {
                var serversFromSections = Sections.SelectMany(s =>
                {
                    // Create a list combining Server and ServerTeam
                    var combinedServers = new List<Server>();
                   
                    if (s.ServerTeam != null)
                    {
                        combinedServers.AddRange(s.ServerTeam);
                    }

                    return combinedServers;
                })
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
                    return (DiningArea.GetMaxCovers() / this.Servers.Count) - (TotalPickUpSectionCovers() / this.Servers.Count);
                    //return ((float)this.DiningArea.GetMaxCovers() - TotalPickUpSectionCovers()) / this.Servers.Count;
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
                    return (DiningArea.GetAverageSales()  / this.Servers.Count) - (TotalPickUpSectionSales() / this.Servers.Count);
                return 0;
            }
        }
        private float TotalPickUpSectionCovers()
        {
            float total = 0;
            foreach(var section in Sections)
            {
                if (section.IsPickUp)
                {
                    total += section.MaxCovers;
                }
            }
            return total;
        }
        private float TotalPickUpSectionSales()
        {
            float total = 0;
            foreach (var section in Sections)
            {
                if (section.IsPickUp)
                {
                    total += section.AverageSales;
                }
            }
            return total;
        }
        public float GetCoverDifferenceForSection(Section section)
        {
            if (!section.IsTeamWait)
            {
                return section.MaxCovers - this.MaxCoversPerServer;
            }            
            else
            {
                return section.MaxCovers - (this.MaxCoversPerServer * section.ServerCount);
            }
        }
        public float GetSalesDifferenceForSection(Section section)
        {
            if (!section.IsTeamWait)
            {
                return section.AverageSales - this.AvgSalesPerServer;
            }            
            else
            {
                return section.AverageSales - (this.AvgSalesPerServer * section.ServerCount);
            }
        }
        
        public void CopyTemplateSections(List<Section> sections)
        {
            this.Sections.Clear();
            foreach (Section section in sections)
            {
                Section SectionCopy = section.CopySection();
                
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
            section.ServerRemoved += RemoveServerFromSection;
            section.ServerAssigned += UpdateSectionServerMap;
           
            //section.SubscribeObserver(this);
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
           // sectionToRemove.Notify();
            
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
            newSection.ServerAssigned += UpdateSectionServerMap;
            newSection.ServerRemoved += RemoveServerFromSection;
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
        public bool NotEnoughUnassignedServersCheck(Section section)
        {
            if (section.ServerCount - section.ServerTeam.Count >= this.ServersWithoutSection.Count)
            {
                return true;
            }
            else { return false; }
        }
        public void Update(Section section)
        {
            //UpdateSectionServerMap();
        }
    }
}
