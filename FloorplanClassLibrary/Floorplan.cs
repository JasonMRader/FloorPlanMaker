using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace FloorplanClassLibrary
{
    public class Floorplan
    {
        public Floorplan(DiningArea diningArea, DateTime date, bool isLunch, int serverCount, int sectionCount)
        {
            this.DiningArea = diningArea;
            this.Date = date;
            this.IsLunch = isLunch;
            this.SectionCount = sectionCount;
            this.SectionServerMap = new Dictionary<Section, List<Server>>();
            CreateSections();
        }
        public Dictionary<Section, List<Server>> SectionServerMap { get; private set; }
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public DateOnly DateOnly => new DateOnly(Date.Year, Date.Month, Date.Day);
        public bool IsLunch { get; set; }
        public DiningArea? DiningArea { get; set; }
        public int DiningAreaID { get; set; }
        private Section? _sectionSelected { get; set; }
        public Section? SectionSelected
        {
            get
            {
                if (this.Sections.Count == 0) return null;
                if (currentFocusedSectionIndex >= this.Sections.Count) return this.Sections[this.Sections.Count - 1];
                else return this.Sections[currentFocusedSectionIndex];
            }

        }
        public List<FloorplanLine> floorplanLines = new List<FloorplanLine>();
        public bool hasBarSection
        {
            get
            {
                return this.Sections.Any(s => s.IsBarSection);
            }
        }
        public List<Server> Bartenders 
        {
            get
            {
                return this.Servers.Where(s => s.IsBartender).ToList();
            }
        }
        public int NonBartenderServerCount
        {
            get
            {
                return this.Servers.Count - this.Bartenders.Count;
            }
        }
        public int NonBartenderServerCapacity
        {
            get
            {
                return Sections.Where(s => !s.IsPickUp && !s.IsBarSection).Sum(s => s.ServerCount);
            }
        }
        
        
        public Floorplan(FloorplanTemplate template)
        {
            this.DiningArea = template.DiningArea;
            
            
            this.floorplanLines = template.floorplanLines;
            SectionServerMap = new Dictionary<Section, List<Server>>();
            CopyTemplateSections(template.Sections);    
            
        }
        public Floorplan() 
        {
            SectionServerMap = new Dictionary<Section, List<Server>>();
           
        }
       
        private int currentFocusedSectionIndex = 0;
        public void CreateBarSection()
        {
            List<Server> bartenders = this.Servers.Where(s => s.IsBartender).ToList();
            if(bartenders.Count > 0)
            {
                _sections[0].MakeBarSection(bartenders);
            }
        }
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
            currentFocusedSectionIndex = (currentFocusedSectionIndex + 1) % this.Sections.Count;            
            _sectionSelected = this.Sections[currentFocusedSectionIndex];
            HighlightSelectedSection();
           

        }
        public void MoveToPreviousSection()
        {
            if (this.Sections == null || this.Sections.Count == 0) return;
            currentFocusedSectionIndex = (currentFocusedSectionIndex - 1 + this.Sections.Count) % this.Sections.Count;
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
                var serversFromSections = Sections
                    .Where(s => !s.IsPickUp) 
                    .SelectMany(s =>
                    {
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
               
                return ServersWithoutSection
                    .Concat(serversFromSections)
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
        public List<Section> UnassignedSections { get { return _sections.Where(s => s.Server == null).ToList(); } }
        public bool HasAssignedNonBartenders
        {
            get
            {
                var nonBartenderServers = Servers.Where(s => !s.IsBartender);                
                return nonBartenderServers.Any(server => !ServersWithoutSection.Contains(server));
            }
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
        public float GetAvgSalesPerServer()
        {          
            List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(IsLunch, DateOnly.FromDateTime(Date).AddDays(0));
            float totalAreaSales = 0f;
            foreach (Table table in DiningArea.Tables)
            {
                var matchedStat = stats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                if (matchedStat != null)
                {
                    table.AverageSales = (float)matchedStat.Sales;
                    totalAreaSales += (float)matchedStat.Sales;
                   
                }
                else { table.AverageSales = 0; }
                
            }
            if(this.Servers.Count > 1) 
            {
                return totalAreaSales / this.Servers.Count;
            }
            
            return totalAreaSales;
        }
        public float GetAvgSalesPerServerByDay(int daysAgo)
        {
            List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateAndLunch(IsLunch, DateOnly.FromDateTime(Date).AddDays(daysAgo));
            float totalAreaSales = 0f;
            foreach (Table table in DiningArea.Tables)
            {
                var matchedStat = stats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                if (matchedStat != null)
                {
                    table.AverageSales = (float)matchedStat.Sales;
                    totalAreaSales += (float)matchedStat.Sales;

                }
                else { table.AverageSales = 0; }

            }
            if (this.Servers.Count > 1)
            {
                return totalAreaSales / this.Servers.Count;
            }

            return totalAreaSales;
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
                    total += section.ExpectedTotalSales;
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
            //return section.ExpectedSalesPerServer - this.AvgSalesPerServer;

            if (!section.IsTeamWait)
            {
                return section.ExpectedTotalSales - this.AvgSalesPerServer;
            }
            else if (section.PairedSection != null)
            {
                if(section.PairedSection.Server != null)
                {
                    return section.ExpectedTotalSales 
                        - (this.AvgSalesPerServer * (section.ServerCount + section.PairedSection.ServerCount));
                }
                return section.ExpectedTotalSales - (this.AvgSalesPerServer * section.ServerCount);
            }
            else
            {
                return section.ExpectedTotalSales - (this.AvgSalesPerServer * section.ServerCount);
            }
        }
        
        public void CopyTemplateSections(List<Section> sections)
        {
            this.Sections.Clear();
            foreach (Section section in sections)
            {
                Section SectionCopy = section.CopySection(this.DiningArea);
                
                Sections.Add(SectionCopy);
            }
            foreach (Section section in Sections)
            {
                if (section.IsBarSection && this.Bartenders != null)
                {                    
                    section.AddBartendersToBarSection(Bartenders);
                }
            }
            this.ServersWithoutSection.RemoveAll(s => s.IsBartender);
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
            int newSectionNumber = 1;
            if (!section.IsPickUp)
            {
                while (_sections.Any(s => s.Number == newSectionNumber))
                {
                    newSectionNumber++;
                }
            }
            if(section.IsPickUp)
            {
                newSectionNumber = 100;
                while (_sections.Any(s => s.Number == newSectionNumber))
                {
                    newSectionNumber++;
                }
            }
           
           

           
           section.SetSectionNumber(newSectionNumber);

           
            if (section.Server == null)
            {
                section.Name = $"Section {section.Number}";
            }

           
            _sections.Add(section);           
            section.ServerRemoved += RemoveServerFromSection;
            section.ServerAssigned += UpdateSectionServerMap;
           
          
        }
        public void SwapServers(Section section1, Section section2)
        {
            if(section1 == null || section2 == null) { return; }

            if( section1.IsTeamWait || section2.IsTeamWait 
                || section1.Server == null  || section2.Server == null
                || section1.IsPickUp || section2.IsPickUp) { return; }
            Server server1 = section1.Server;
            Server server2 = section2.Server;
            section1.RemoveServer(server1);
            section2.RemoveServer(server2);
            section1.AddServer(server2);
            section2.AddServer(server1);
        }

       
        public void DeleteSection(Section section)
        {
            if (section.Tables != null || section.Server != null)
            {
                ClearSection(section);
            }
            var sectionToRemove = Sections.Where(s => s.Number == section.Number).FirstOrDefault();
            Sections.Remove(sectionToRemove);
           
            
        }
        public void UnassignServerFromSection(Section section, Server serverToRemove)
        {
            if (section != null)
            {
                if (section.Server != null)
                {
                    this.ServersWithoutSection.Add(serverToRemove);
                    section.RemoveServer(serverToRemove);
                }  
            }
        }
        public void ClearSection(Section section)
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

        public void RemoveAllServersFromSections()
        {
            foreach(Section section in Sections)
            {
                ClearSection (section);
            }
        }
        public void AddServerAndSection(Server server)
        {
            if (server.IsBartender)
            {
                AddBartender(server);

            }
            else
            {
                this.ServersWithoutSection.Add(server);
                Section newSection = new Section(this);                
                AddSection(newSection);
            }
            
        }
        public void AddBartender(Server server)
        {
            if (server.IsBartender)
            {
                Section barSection = this._sections.FirstOrDefault(s => s.IsBarSection);
                if (barSection == null)
                {
                    Section newSection = new Section(this);
                    newSection.SetToBarSection();
                    newSection.AddBartender(server);
                    AddSection(newSection);
                }
                else
                {
                    barSection.AddBartender(server);
                }
            }
        }
        public void AddServerNotSection(Server server)
        {
            this.ServersWithoutSection.Add(server);
        }
        public void RemoveServerKeepSection(Server server)
        {
            if (server.CurrentSection == null)
            {
                this.ServersWithoutSection.Remove(server);
                
            }
            else if (server.CurrentSection != null && !server.IsBartender)
            {
                UnassignServerFromSection(server.CurrentSection, server);
                this.ServersWithoutSection.Remove(server);
            }
            else if (server.CurrentSection != null && server.IsBartender)
            {
                RemoveBartenderFromSection(server);
            }
        }
        public void SetTheAppropriateAmountOfSections()
        {
            while(NonBartenderServerCount > NonBartenderServerCapacity)
            {
                bool sectionRemoved = RemoveHighestNumberedEmptySection();
                if (!sectionRemoved)
                {
                    break;
                }
            }
            while(NonBartenderServerCount < NonBartenderServerCapacity)
            {
                Section newSection = new Section(this);                
                AddSection(newSection);
            }
        }
        public void RemoveServerAndSection(Server server)
        {
            if(server.CurrentSection == null)
            {
                this.ServersWithoutSection.Remove(server);
                RemoveHighestNumberedEmptySection();
            }
            else if(server.CurrentSection != null && !server.IsBartender)
            {
                DeleteSection(server.CurrentSection);
                this.ServersWithoutSection.Remove(server);
            }
            else if(server.CurrentSection != null && server.IsBartender)
            {
                RemoveBartenderFromSection(server);               
            }      
        }

        private void RemoveBartenderFromSection(Server server)
        {
            
            if (server.CurrentSection.ServerTeam.Count > 1)
            {
                server.CurrentSection.RemoveBartender(server);
                this.ServersWithoutSection.Remove(server);
                
            }
            else if (server.CurrentSection.ServerTeam.Count == 1)
            {
                DeleteSection(server.CurrentSection);
                this.ServersWithoutSection.Remove(server);
            }
        }

        public int ServerCount
        {
            get
            {
                int count = 0;
                foreach (Section section in this._sections)
                {
                    if (!section.IsPickUp)
                    {
                        count += section.ServerCount;
                    }
                }
                return count;
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
                    s.SetSectionNumber(i +1);
                    s.Name = $"Section {i +1}";
                    Sections.Add(s);
                }
            }    
        }
        public Section HighestNumberedEmptySection()
        {
            var sectionToRemove = Sections
                .Where(s => s.Server == null
                         && (s.Tables == null || !s.Tables.Any())
                         && !s.IsPickUp
                         && !s.IsTeamWait)
                .OrderByDescending(s => s.Number)
                .FirstOrDefault();
            return sectionToRemove;
        }
        public Section HighestNumberedEmptySection(Section section)
        {
            var sectionToRemove = Sections
                .Where(s => s.Server == null
                         && (s.Tables == null || !s.Tables.Any())
                         && !s.IsPickUp
                         && !s.IsTeamWait
                         && s != section)
                .OrderByDescending(s => s.Number)
                .FirstOrDefault();
            return sectionToRemove;
        }
        public bool RemoveHighestNumberedEmptySection()
        {
            var sectionToRemove = HighestNumberedEmptySection();

            if (sectionToRemove != null)
            {
                Sections.Remove(sectionToRemove);
                return true;
            }
            else
            {
                return false;
            }
            
        }
        public Section RemoveHighestNumberedEmptySection(Section section)
        {
            var sectionToRemove = HighestNumberedEmptySection(section);

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
        public void OrderSectionsByAvgSales()
        {
            var sortedSections = this._sections.OrderByDescending(s => s.ExpectedSalesPerServer).ToList();
            this._sections= sortedSections;
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
        public void MovePickupSectionsToEndOfList()
        {
            this.Sections = this.Sections.OrderBy(s => s.IsPickUp).ToList();
        }
        public override string ToString()
        {
            //string isAM = "AM";
            //if (!this.IsLunch) {
            //    isAM = "PM"; }
            //return $"{DateOnly} {isAM} {DiningArea.Name} | {this.Servers.Count} Servers";
            return DiningArea.Name;
        }

        public void ClearSectionsOfTables()
        {
            throw new NotImplementedException();
        }

        public void AutoAssignCloser()
        {
           FloorplanGenerator.SetClosersForFloorplan(this);
        }
    }
}
