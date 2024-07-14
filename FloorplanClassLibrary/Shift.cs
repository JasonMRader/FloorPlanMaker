using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FloorplanClassLibrary
{
    public class Shift
    {
        
        public Shift(DiningArea diningArea, DateOnly date, bool isAm)
        {
            this.DateOnly = date;
            this.IsAM = isAm;
            SelectedDiningArea = diningArea;
            InitializeServers();
        }
        public Shift()
        {
            InitializeServers();
        }
        public Shift(DateOnly dateOnly, bool isAM)
        {
            this.DateOnly = dateOnly;
            this.IsAM = isAM;
            InitializeServers();
        }
        public Shift(DateOnly date, bool isAm, List<Floorplan> floorplans)
        {
            this.DateOnly = date;
            this.IsAM = isAm;
            InitializeServers();
            foreach (Floorplan floorplan in floorplans)
            {
                AddFloorplanAndServers(floorplan);
            }
        }

        private void InitializeServers()
        {
            _allServers = SqliteDataAccess.LoadActiveServers();
           
            _serversNotOnShift = new List<Server>(_allServers);
           

        }
        public void CopyServersAndDiningAreas(Shift shiftToCopy)
        {
            this._unassignedServers = shiftToCopy.ServersOnShift;
            this._serversNotOnShift = shiftToCopy.ServersNotOnShift;
           
            foreach (Floorplan floorplan in shiftToCopy.Floorplans)
            {
                if(!this.DiningAreasUsed.Contains(floorplan.DiningArea))
                {
                    CreateFloorplanForDiningArea(floorplan.DiningArea,0,0);
                }
            }
        }
        public int BartenderCount
        {
            get 
            {
                int bartenderCount = 0;
                foreach (Server server in _serversOnShift)
                {
                    if(server.IsBartender)
                    {
                        bartenderCount++;
                    }
                }
                return bartenderCount;
            }
        }
        public List<Server> allBartenders(List<Server> servers)
        {
           
            Regex regex = new Regex(@"^BAR\d+$");
            return servers.Where(s => regex.IsMatch(s.Name)).ToList();

        }
        public void SetBartendersToShift(int bartenderCount)
        {
           
            List<Server> currentBartenders = ServersOnShift
                .Where(s => s.IsBartender)
                .OrderBy(s => s.Name)
                .ToList();            
            int currentCount = currentBartenders.Count;
            if (bartenderCount > currentCount)            {
                
                for (int i = currentCount + 1; i <= bartenderCount; i++)
                {
                    string bartenderName = $"BAR{i}";
                    Server newBartender = AllServers.FirstOrDefault(s => s.Name == bartenderName);
                    if (newBartender != null)
                    {
                       
                        AddNewUnassignedServer(newBartender);
                    }
                }
            }
            else if (bartenderCount < currentCount)
            {               
                for (int i = currentCount; i > bartenderCount; i--)
                {
                    string bartenderName = $"BAR{i}";
                    Server bartenderToRemove = ServersOnShift.FirstOrDefault(s => s.Name == bartenderName);
                    if (bartenderToRemove != null)
                    {
                        RemoveServerFromShift(bartenderToRemove);
                    }
                }
            }
            
        }
              
       

        public bool IsAM { get; set; }
        public DateOnly DateOnly { get; set; }
        public DateTime DateTime
        {
            get
            {
                
                return DateOnly.ToDateTime(TimeOnly.MinValue);
            }
        }
        public DiningArea? SelectedDiningArea { get; set; }
        public Floorplan? SelectedFloorplan { get; set; }
       
        public List<DiningArea> DiningAreasUsed => Floorplans.Select(fp => fp.DiningArea).Distinct().ToList();
       
        public List<Server> _serversOnShift
        {
            get
            {
                var serversFromFloorplans = _floorplans                    
                    .SelectMany(f =>
                    {
                        var combinedServers = new List<Server>();                        
                        combinedServers.AddRange(f.Servers);                       

                        return combinedServers;
                    })
                    .Where(f => f != null)
                    .Distinct()
                    .ToList();

                return _unassignedServers
                    .Concat(serversFromFloorplans)
                    .Distinct()
                    .ToList();
            }
        }
        private List<Server> _serversNotOnShift = new List<Server>();
        private List<Server> _unassignedServers = new List<Server>();
        private List<Server> _allServers = new List<Server>();
        private List<Section> _pickUpSections = new List<Section>();
        private List<Floorplan> _floorplans = new List<Floorplan>();
        public IReadOnlyList<Floorplan> Floorplans => _floorplans.AsReadOnly();
        public List<FloorplanTemplate> Templates = new List<FloorplanTemplate>();
        public List<Section> TemplateSections = new List<Section>();
        public FloorplanTemplate? SelectedTemplate { get; set; }
        public void SetDoubles()
        {
            if (!IsAM)
            {
                List<Floorplan> amFloorplans = SqliteDataAccess.LoadFloorplansByDateAndShift(DateOnly, true);
                if (amFloorplans == null) return;
                List<Server> amServers = amFloorplans.SelectMany(f => f.Servers).ToList();
                foreach (Server s in ServersOnShift)
                {
                    if (amServers.Contains(s))
                    {
                        s.isDouble = true;
                    }
                }
            }
        }
        public void PickupSectionUpdate()
        {
            List<Server> serversWithPickupSections = new List<Server>();
            foreach(Server server in _serversOnShift)
            {
                server.pickUpSections.Clear();
                foreach(Section section in _pickUpSections)
                {
                    if (section.ServerTeam.Contains(server))
                    {
                        server.pickUpSections.Add(section);
                        serversWithPickupSections.Add(server);
                    }
                }
            }
            foreach (Floorplan floorplan in _floorplans)
            {
                foreach (Section section in floorplan.Sections)
                {
                    if(section.IsPickUp)
                    {
                        continue;
                    }
                    if (serversWithPickupSections.Contains(section.Server))
                    {
                        foreach (Section pickUpSection in _pickUpSections)
                        {
                            if (pickUpSection.ServerTeam.Contains(section.Server))
                            {
                                section.AssignPickupSection(pickUpSection);
                                //pickUpSection.AssignPickupSection(section);
                            }
                        }
                    }
                    
                }
            }
        }
        public List<Server> ServersNotOnShift
        {
            get { return _serversNotOnShift; }
            private set { _serversNotOnShift = value; }
        }
        public List<Server> ServersOnShift
        {
            get { return _serversOnShift; }
           
        }

        public List<Server> UnassignedServers
        {
            get { return _unassignedServers; }
            private set { _unassignedServers = value; }
        }
        public List<Server> AllServers
        {
            get { return _allServers; }
            private set { _allServers = value; }
        }
        public List<Section> Sections = new List<Section>();
        public void ReloadAllServerList()
        {
            _allServers.Clear();
            _allServers = SqliteDataAccess.LoadActiveServers();
        }
        public void AddNewUnassignedServer(Server server)
        {
            if (!UnassignedServers.Contains(server))
            {
                this._unassignedServers.Add(server);
            }
            
            this._serversNotOnShift.Remove(server);
            if (!ServersOnShift.Contains(server))
            {
                this._serversOnShift.Add(server);
            }
           
        }
        public void RemoveServerFromShift(Server server)
        {
            if (!ServersNotOnShift.Contains((Server)server))
            {
                this._serversNotOnShift.Add(server);
            }
            
            this._serversOnShift.Remove(server);
            this._unassignedServers.Remove(server);
            if(this.Floorplans != null)
            {
                foreach(Floorplan fp in this.Floorplans)
                {
                    if (fp.Servers.Contains(server))
                    {
                        fp.RemoveServerAndSection(server);
                    }
                }
            }
        }
        public void UnassignServer(Server server)
        {
            if (this.Floorplans != null)
            {
                foreach (Floorplan fp in this.Floorplans)
                {
                    if (fp.Servers.Contains(server))
                    {
                        fp.RemoveServerAndSection(server);
                    }
                }
            }
            AddNewUnassignedServer(server);
        }
        public void AddServerToAFloorplan(Server server)
        {
            this._unassignedServers.Remove(server);

        }
        public void AddServerToSelectedFloorplan(Server server)
        {
            AddNewUnassignedServer(server);
            AddServerToAFloorplan(server);
            this.SelectedFloorplan.AddServerAndSection(server);
        }
        //public bool ShiftContainsServer(Server server)
        //{
        //    return ServersOnShift.Contains(server);
        //}
        public Section SectionSelected
        {
            get
            {
                if(this.SelectedFloorplan != null)
                {
                    return this.SelectedFloorplan.SectionSelected;
                }
                return null;
               
            }
        }
        public void SetSelectedSection(Section section)
        {
            if(this.SelectedFloorplan != null)
            {
                this.SelectedFloorplan.SetSelectedSection(section);
            }
        }
        
         
        
        public void AddFloorplanToShift(Floorplan floorplan)
        {
            Floorplan existingFloorplan = this.Floorplans.FirstOrDefault(fp => fp.DiningArea == floorplan.DiningArea);
            if(existingFloorplan != null)
            {
                this._floorplans.Remove(existingFloorplan);
            }
            else
            {
                this._floorplans.Add(floorplan);
            }
            
            
        }
        public void RemoveAssignedServers()
        {
            List<Server> servers = new List<Server>();
            foreach(Floorplan floorplan in this.Floorplans)
            {
                if(floorplan.Servers.Count > 0)
                    servers.AddRange(floorplan.Servers);
            }

            _unassignedServers.RemoveAll(server => servers.Contains(server)); 
            _serversOnShift.RemoveAll(server => servers.Contains(server));
            _serversNotOnShift = _allServers;
           
        }
        public void AddFloorplanAndServers(Floorplan floorplan)
        {
            this._floorplans.Add(floorplan);

            // Create a hash set of server IDs for quicker lookup
            var assignedServerIds = new HashSet<int>(floorplan.Servers.Select(s => s.ID));

            // Remove all servers by ID that are in the floorplan's server list
            this._serversNotOnShift.RemoveAll(server => assignedServerIds.Contains(server.ID));

            // Assuming ServersOnShift is not a HashSet, and you want to avoid duplicates:
            foreach (var server in floorplan.Servers)
            {
                if (!this._serversOnShift.Any(s => s.ID == server.ID))
                {
                    this._serversOnShift.Add(server);
                }
            }
            foreach (var section in floorplan.Sections)
            {
                if (section.IsPickUp)
                {
                    _pickUpSections.Add(section);
                }
            }
            //TODO unnecisary / redundant assignment below?
            this.DateOnly = floorplan.DateOnly;
            this.IsAM = floorplan.IsLunch;
            //ServersNotOnShift = ServersNotOnShift.OrderBy(s => s.Name).ToList();
        }

        public void RemoveFloorplan(Floorplan floorplan)
        {
            if (floorplan != null)
            {
                this._floorplans.Remove(floorplan);
                var assignedServersSet = new HashSet<Server>(floorplan.Servers);
               
                // Remove all servers that are in the floorplan's server list
                this._serversOnShift.RemoveAll(assignedServersSet.Contains);
                this._serversNotOnShift.AddRange(floorplan.Servers);
                foreach (var section in floorplan.Sections)
                {
                    if (section.IsPickUp)
                    {
                        _pickUpSections.Remove(section);
                    }
                }
            }
            
            
        }
        public void ClearFloorplans()
        {
            RemoveAssignedServers();
            this._floorplans.Clear();
        }
        public bool ContainsFloorplan(DateOnly date, bool isLunch, int ID)
        {
            return Floorplans.Any(fp => fp.DateOnly == date &&
                                        fp.IsLunch == isLunch &&
                                        fp.DiningArea.ID == ID);
        }
        public void SetSelectedFloorplan(DateOnly date, bool isLunch, int ID)
        {
            SelectedFloorplan = _floorplans.FirstOrDefault(fp => fp.DateOnly == date &&
                                                       fp.IsLunch == isLunch &&
                                                       fp.DiningArea.ID == ID);
        }


        public void RemoveServerFromFloorplanByDiningArea(Server server, Floorplan targetFloorplan)
        {
           
            var floorplan = _floorplans.FirstOrDefault(fp => fp.DiningArea.ID == targetFloorplan.DiningArea.ID);
            if (floorplan != null)
            {

                floorplan.RemoveServerAndSection(server);

               
            }
        }



        public void AssignSectionNumbers(List<Section> sections)
        {
            int sectionNumber = 1;
            int pickupNumber = 100;
            foreach (var section in sections)
            {
                if (!section.IsPickUp)
                {
                    section.SetSectionNumber(sectionNumber);
                    sectionNumber++;
                }
                if (section.IsPickUp)
                {
                    section.SetSectionNumber(pickupNumber);
                    pickupNumber++;
                }
            }
        }
        
        public void SetSectionsToTemplate(FloorplanTemplate template)
        {
            this.TemplateSections.Clear();
            this.TemplateSections.AddRange(template.Sections);
        }
        public Floorplan CreateFloorplanForDiningArea(DiningArea diningArea, int serverCount, int sectionCount)
        {
            DateTime date = new DateTime(this.DateOnly.Year, this.DateOnly.Month, this.DateOnly.Day);
            Floorplan newFloorplan = new Floorplan(diningArea, date, this.IsAM ,0,0);
            _floorplans.Add(newFloorplan);
            //if(newFloorplan != null)
            //{
            //    this.AddFloorplanAndServers(newFloorplan);
                
            //}
            //if(newFloorplan == null)
            //{
            //    newFloorplan = new Floorplan(diningArea, date, isLunch, serverCount, sectionCount);
            //    if (Floorplans == null)
            //    {
            //        _floorplans = new List<Floorplan>();
            //    }

            //    _floorplans.Add(newFloorplan);
            //}         
                      

            return newFloorplan;
        }
        public void SetFloorplansToAM()
        {
            foreach (var floorplan in Floorplans)
            {
                floorplan.IsLunch = true;
            }
            this.IsAM = true;
        }
        public void SetFloorplansToPM()
        {
            foreach (var floorplan in Floorplans)
            {
                floorplan.IsLunch = false;
            }
            this.IsAM = false;
        }

        public void AutoAssignCloser()
        {
            throw new NotImplementedException();
        }
    }
}
