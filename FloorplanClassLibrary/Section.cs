﻿using System.Globalization;

namespace FloorplanClassLibrary
{
    public class Section 
    {

        public Section()
        {

        }
        public Section(Floorplan floorplan)
        {
            this.DiningAreaID = floorplan.DiningArea.ID;
            this.Floorplan = floorplan;
           
        }
        public Section CopySection(DiningArea diningArea)
        {
            Section copy = new Section();
            copy._number = this.Number;
            //copy.IsCloser = this.IsCloser;
            copy.IsPickUp = this.IsPickUp;
            //copy.IsPre = this.IsPre;
            copy.Name = this.Name;
            if (this.TemplateTeamWait)
            {
                copy.MakeTeamWait();
            }
            //this.Floorplan = section.Floorplan;
            //this.ServerCount = section.ServerTeam.Count;
            copy.SetSectionPropertiesFromTemplateSection(this);
            copy.DiningAreaID = this.DiningAreaID;
            copy.SetTableList(this.Tables.ToList());
            CopyTableSalesEstimates(diningArea.Tables);
            //copy.NotifyObservers();
            return copy;
        }
        public Section CopySectionForTemplate()
        {
            Section copy = new Section();
            copy._number = this.Number;
            copy.IsCloser = false;
            copy.IsPickUp = this.IsPickUp;
            copy.IsPre = false;
            copy.Name = this.Name;
            //this.Floorplan = section.Floorplan;
            //this.ServerCount = section.ServerTeam.Count;
            copy.SetSectionPropertiesFromTemplateSection(this);
            copy.DiningAreaID = this.DiningAreaID;
            copy.SetTableList(this.Tables.ToList());
            //CopyTableSalesEstimates(diningArea.Tables);
            //copy.NotifyObservers();
            return copy;
        }
        public void CopyTableSalesEstimates(List<Table> tablesToCopy)
        {
            foreach(Table table in this.Tables)
            {
                Table tableToCopy = tablesToCopy.FirstOrDefault(t => t.TableNumber == table.TableNumber);
                table.SetTableSales(tableToCopy.AverageSales);
            }
        }
       
        public bool IsFull
        {
            get
            {
                return ServerTeam.Count == ServerCount;
            }
        }
        private SectionBoarders _SectionBoarders;
        public SectionBoarders SectionBoarders
        {
            get { return _SectionBoarders; }
        }
        public void SetBoarderManager()
        {
            this._SectionBoarders = new SectionBoarders(this);
            _SectionBoarders.SetEdgesForBoundingBox();
        }
        private SectionNodeManager _nodeManager;
        public void SetNodeManager()
        {
            this._nodeManager = new SectionNodeManager(this);
            _nodeManager.SetUpTopAndBottomEdges();
            //_nodeManager.GenerateNodesForUnblockedBottoms();

        }           
        public SectionNodeManager NodeManager
        {
            get
            {
                if (_nodeManager == null)
                {
                    _nodeManager = new SectionNodeManager(this);
                }
                return _nodeManager;
            }
        }
        //public List<Server> UnassignedServers = new List<Server>();
        public Floorplan? Floorplan { get; set; }
        public int ServerCount { get; private set; } = 1;
        public int TemplateServerCount { get; set; }
        public bool TemplateTeamWait { get; set; }
        public bool TemplateBarSection { get; private set; }
        public bool TemplatePickUp { get; set; }    
        public int ID {  get; set; }
        public bool IsPickUp { get; set; }
       
        public int DiningAreaID { get; set; }
        private bool _isBarSection {  get; set; }
        public bool IsBarSection { get { return _isBarSection; } }
        public float AdditionalPickupSales 
        { 
            get 
            {
                if (IsPickUp && ServerTeam.Count() > 0)
                {
                    return this.ExpectedTotalSales / ServerTeam.Count();
                }
                else
                {
                    return 0;
                }
            }
        }
        private float SalesFromPickps
        {
            get
            {
                float sales = 0f;
                if(_pairedSection != null)
                {
                    foreach (Table table in _pairedSection.Tables)
                    {
                        sales += table.AverageSales;
                    }
                }
               
                return sales;
            }
        }
        public List<TemplateTable> TemplateTables { get; set; } = new List<TemplateTable>();

        public bool IsSelected { get; private set; } = false;
        public void SetServerCount()
        {
            ServerCount = this.ServerTeam.Count;
        }
        public void SetToSelected()
        {
            if (!IsSelected)
            {
                this.IsSelected = true;
                NotifyObservers();
            }
           
        } 
        public void NotSelected()
        {
            if(this.IsSelected)
            {
                this.IsSelected = false;
                NotifyObservers();
            }
           
        }

        public string? Name { get; set; }
        private List<Table> _tables = new List<Table>();
        public IReadOnlyList<Table> Tables => _tables.AsReadOnly();
        public event Action<Server, Section> ServerAssigned;
        public event Action<Server, Section> ServerRemoved;

        public Server? Server
        {
            get => ServerTeam.FirstOrDefault();
            set
            {
                if (value != null)
                {
                    ServerTeam.Clear();
                    ServerTeam.Add(value);
                    NotifyObservers();
                    
                }
            }
        }


        public int? ServerID
        {
            get
            {
                if (this.Server != null)
                {
                    return this.Server.ID;
                }
                else
                {
                    return null;
                }
            }
        }
        //public Server? Server2 { get; set; }
        public List<Server> ServerTeam { get; private set; } = new List<Server>();
        public bool IsCloser { get; set; }
        public bool IsPre { get; set; }
        private bool _isTeamWait { get; set; }
        public bool IsTeamWait { get { return _isTeamWait; } }
        public bool IsEmpty()
        {
            if (ServerTeam.Count == 0 && Tables.Count == 0)
            {
                return true;
            }
            return false;
        }
        public void SetToClose()
        {
            this.IsCloser = true;
            this.IsPre = false;
            NotifyObservers();
        }
        public void SetToPre()
        {
            this.IsCloser = false;
            this.IsPre = true;
            NotifyObservers();
        }
        public void SetToCut()
        {
            this.IsCloser = false;
            this.IsPre = false;
            NotifyObservers();
        }
       
        public void MakeBarSection(List<Server> bartenders)
        { 
            if(bartenders.Count == 1)
            {
                AddServer(bartenders[0]);
            }
            if (bartenders.Count > 1)
            {
                _isTeamWait = true;
                TemplateTeamWait = true;
                ServerCount = bartenders.Count;
                this.ServerTeam.AddRange(bartenders);
                foreach (Server b in bartenders)
                {
                    NotifyServerAssigned(b);
                }
                NotifyObservers();

            }
            _isBarSection = true;
        }
        public void SetToBarSection()
        {
            this._isBarSection = true;
        }
        public void AddBartendersToBarSection(List<Server> bartenders)
        {
            this.ServerTeam.Clear();
            this._isBarSection = true;
            if (bartenders.Count == 1)
            {
                this._isTeamWait = false;
                this.ServerCount = 1;
                this.AddServer(bartenders[0]);
            }
            else if(bartenders.Count > 1)
            {
                this._isTeamWait = true;
                this.ServerCount = bartenders.Count;
                foreach (Server b in bartenders)
                {
                    this.AddServer(b);
                }
            }

        }
        public void AddBartender(Server bartender)
        {

            int bartenderCount = this.ServerTeam.Count;
            if(bartenderCount == 0)
            {
                AddServer(bartender);
                return;
                
            }
            else if(bartenderCount == 1)
            {
                MakeTeamWait();
                AddServer(bartender);
                return;
                
            }
            else if(bartenderCount > 1)
            {
               
                IncreaseServerCount();
                AddServer(bartender);
                return;
            }
        }
        public void RemoveBartender(Server bartender)
        {
            int bartenderCount = this.ServerTeam.Count;
            if (bartenderCount == 1)
            {
                RemoveServer(bartender);
                return;

            }
            else if (bartenderCount == 2)
            {
                MakeSoloSection();
                RemoveServer(bartender);
                return;

            }
            else if (bartenderCount > 2)
            {
                RemoveServer(bartender);
                DecreaseServerCount();                
                return;
            }
        }
        private void SetSectionPropertiesFromTemplateSection(Section sectionToCopy)
        {
            
            if(sectionToCopy.TemplateTeamWait && sectionToCopy.ServerCount > this.ServerCount)
            {              
                _isTeamWait = true;
                ServerCount = sectionToCopy.ServerCount;
            }
            if(sectionToCopy.IsBarSection)
            {
                _isBarSection = true;
            }

        }
        public void ToggleTeamWait()
        {
            if (_isTeamWait)
            {
                this.MakeSoloSection();
            }
            else
            {
                this.MakeTeamWait();
            }
        }
        public void IncreaseServerCount()
        {
            if (!_isTeamWait)
            {
                MakeTeamWait();
                return;
            }
            ServerCount++;
            NotifyObservers();
            //Potential redundant notification
        }
        public bool DecreaseServerCount()
        {
            if (ServerCount <= ServerTeam.Count)
            {
                //MessageBox.Show("You must choose a server to remove to decrease the size of the team.");
                return false;
            }   
            if(this.ServerCount == 2)
            {
                MakeSoloSection();
                return true;
            }
            if(this.ServerCount > 1 && !this.IsPickUp)
            {
                ServerCount--;
            }
            
            
            NotifyObservers();
            return true;
            //Potential redundant notification
        }

        public void MakeSoloSection()
        {
            _isTeamWait = false;
            TemplateTeamWait = false;
            if(ServerTeam.Count > 1)
            {
                for(int i = ServerTeam.Count - 1; i >= 1; i--)
                {
                    RemoveServer(ServerTeam[i]);
                }
            }
            ServerTeam = ServerTeam.Take(1).ToList();
            this.ServerCount = 1;
            NotifyObservers();
        }
        public void MakeTeamWait()
        {
            _isTeamWait = true;
            TemplateTeamWait = true;
            ServerCount++;
            NotifyObservers();
        }
        public void AddServer(Server server)
        {           
            if(_isTeamWait && ServerCount == ServerTeam.Count )
            {
                MessageBox.Show("You already have " + ServerCount.ToString() + " Servers. Remove a Server" +
                    " Or Increase the Team Size");
                return;
            }
            if (server.CurrentSection != null && server.CurrentSection != this && !this.IsPickUp)
            {
                server.CurrentSection.RemoveServer(server);
                //NotifyRemovedFromSection(server);
            }

            if (!IsTeamWait)
            {
                if(Server != null)
                {
                    RemoveServer(Server);
                }
            }

            if (!ServerTeam.Contains(server))
            {
                ServerTeam.Add(server);
            }
            if (this.IsPickUp)
            {
                server.pickUpSections.Add(this);
            }
            if (!this.IsPickUp)
            {
                server.CurrentSection = this;
            }
        
            
            NotifyServerAssigned(server);
            NotifyObservers();
        }

        public void RemoveServer(Server server)
        {
            if (ServerTeam.Contains(server))
            {
                ServerTeam.Remove(server);
                if(!this.IsPickUp)
                {
                    server.CurrentSection = null;
                }
                if(this.IsPickUp)
                {
                    server.pickUpSections.Remove(this);
                }
               
                
                
                NotifyRemovedFromSection(server);
                NotifyObservers();
            }
        }
        public void ClearAllServers()
        {
            foreach (Server server in ServerTeam)
            {
                //server.SalesFromPickupSection = 0;
                NotifyRemovedFromSection(server);

            }
            this.ServerTeam.Clear();
            NotifyObservers();
        }
        public void NotifyServerAssigned(Server server)
        {
            ServerAssigned?.Invoke(server, this);
        }
        public void NotifyRemovedFromSection(Server server)
        {
            ServerRemoved?.Invoke(server, this);
        }

        public void AddTable(Table table)
        {
            this._tables.Add(table);
            table.SalesChanged += NotifySalesChanged;
            NotifyObservers();
        }

        private void NotifySalesChanged()
        {
            //NotifyObservers();
        }

        public void RemoveTable(Table table)
        { 
            this._tables.RemoveAll(t => t.ID == table.ID);
            table.SalesChanged -= NotifySalesChanged;
            NotifyObservers();
        }
        public void SetTableList(List<Table> tables)
        {
            this._tables = tables;
            foreach (Table table in tables) {
                table.SalesChanged += NotifySalesChanged;
            }

            NotifyObservers();
        }
        public void ClearTables()
        {
            foreach (Table table in this._tables) {
                table.SalesChanged -= NotifySalesChanged;
            }
            this._tables?.Clear();
            NotifyObservers();
        }
        private List<ISectionObserver> observers = new List<ISectionObserver>();

        public void Notify()
        {
            this.NotifyObservers();
        }

        public void RemoveObserver(ISectionObserver observer)
        {
            observers.Remove(observer);
        }

        public void SubscribeObserver(ISectionObserver observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
        }
        protected void NotifyObservers()
        {
            var observersSnapshot = observers.ToList();
            foreach (var observer in observersSnapshot)
            {
                observer.UpdateSection(this);
            }
        }

        public int MaxCovers
        {
            get
            {
                if (Tables == null) return 0;                               
                return Tables.Sum(table => table.MaxCovers);               
            }
        }
        public Section PairedSection { get { return this._pairedSection; } }
        private Section _pairedSection { get; set; }
        public void PairBarSections(Section sectionToPair)
        {
            if(this.IsBarSection && sectionToPair.IsBarSection)
            {
                this._pairedSection = sectionToPair;
                
                if (sectionToPair.PairedSection == null)
                {
                    sectionToPair.PairBarSections(this);
                }
                NotifyObservers();
            }
        }
        public void AssignPickupSection(Section section)
        {
            if (this.IsPickUp)
            {
                if(_pairedSection != null)
                {
                    this._pairedSection.RemovePairedSection();

                }
                this._pairedSection = section;
                this._isTeamWait = section.IsTeamWait;
                this.ServerCount = section.ServerCount;
                this.ServerTeam = section.ServerTeam;
            }
            if (!this.IsPickUp)
            {
                this._pairedSection = section;
            }
            NotifyObservers();
        }
        public void RemovePairedSection()
        {
            if (_pairedSection != null)
            {
                Section tempPairedSection = _pairedSection;
                this._pairedSection = null;
                
                if (this.IsPickUp)
                {
                    this.ServerTeam = new List<Server>();
                    this.ServerCount = 1;
                    this._isTeamWait = false;
                }
                NotifyObservers();


            }
        }
        public float ExpectedTotalSales
        {
            get
            {
                if (Tables == null || !Tables.Any()) return 0;
                if (!this.IsPickUp)
                {
                    return Tables.Sum(table => table.AverageSales) + SalesFromPickps;
                }
                else 
                {
                    return Tables.Sum(table => table.AverageSales);
                }
               
                
            }
        }
        public float ExpectedSalesPerServer
        {
            get
            {
                if(ServerCount > 0)
                {
                    if (this.IsBarSection)
                    {
                        int totalServers = this.ServerCount;
                        if(this.PairedSection != null)
                        {
                            totalServers += PairedSection.ServerCount;
                        }
                        return ExpectedTotalSales / totalServers;
                    }
                    else
                    {
                        return ExpectedTotalSales / ServerCount;
                    }
                   
                }
                else { return ExpectedTotalSales; }
            }
        }

        public string AverageSalesDisplay()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;

            // If the value is negative, prepend a minus sign and format the absolute value.
            // Otherwise, just format the value as currency.
            string formattedValue = this.ExpectedSalesPerServer >= 0
                ? this.ExpectedSalesPerServer.ToString("C0", culture)
                : "-" + Math.Abs(this.ExpectedTotalSales).ToString("C0", culture);

            return formattedValue;
        }
        public string ExpectedSalesDisplay()
        {
            return this.ExpectedTotalSales.ToString("C0");
        }
        public static string FormatAsCurrencyWithoutParentheses(float value)
        {
            // Using current culture's currency format
            CultureInfo culture = CultureInfo.CurrentCulture;

            // If the value is negative, prepend a minus sign and format the absolute value.
            // Otherwise, just format the value as currency.
            string formattedValue = value >= 0
                ? value.ToString("C0", culture)
                : "-" + Math.Abs(value).ToString("C0", culture);

            return formattedValue;
        }
        
        public string GetDisplayString()
        {
            string displayString = $"Section {this.Number}";
            
            if (this.Name != null)
            {
                displayString = this.Name;
            }
            if (this.IsPickUp && this.Server == null)
            {
                displayString = "Pickup";
                return displayString;                      
            }
            if (this.Server != null)
            {                
                if (Server.isDouble) {
                    displayString = Server.ToString() + "*";                 
                }
                else {
                    displayString = Server.ToString();
                }
                if (this._pairedSection != null) { 
                    displayString += " ++";
                }
            }      
            if(this.Server == null && !this.IsTeamWait && !this.IsBarSection)
            {
                return "Unassigned";
            }
            if (this.IsTeamWait)
            {
                displayString = "";
                if(this.Server == null)
                {
                    displayString = $"Team {this.Number}";
                }
                foreach (Server server in this.ServerTeam)
                {
                    if (Server.isDouble)
                    {
                        displayString += server.ToString() + "*";
                    }
                    else
                    {
                        displayString += server.ToString();
                    }
                    if (IsPickUp || this.PairedSection != null)
                    {
                        displayString += " ++";
                    }

                    if (server != ServerTeam.Last())
                    {
                        displayString += "\n ";
                    }            
                }               
            }
            if (this.IsCloser)
            {
                displayString = displayString + " (CLS)";
            }
            if (this.IsPre)
            {
                displayString = displayString + "(PRE)";
            }
            if (this.IsBarSection)
            {
                displayString = "BAR";
            }           
            return displayString;
            
        }

        public string GetDisplayForServer(Server server)
        {
            string displayString = "";
            if (server != null)
            {
                if (Server.isDouble)
                {
                    displayString = server.ToString() + "*";
                }
                else
                {
                    displayString = server.ToString();
                }
                if (this._pairedSection != null)
                {
                    displayString += " ++";
                }

                
            }
            return displayString.ToString();
        }
        

       
        public int Number { get { return _number; } }
        private int _number {  get; set; }
        public void SetSectionNumber(int number)
        {
            this._number = number;
        }
       
        public Color MuteColor(float amount)
        {
            // 'amount' is a value between 0 and 1, where 0 is completely grey and 1 is the original color

            // Calculate the greyscale value of the original color
            float greyScale = (this.Color.R * 0.3f + this.Color.G * 0.59f + this.Color.B * 0.11f) / 255;

            // Interpolate between the greyscale and the original color
            int muteR = (int)(this.Color.R * amount + greyScale * (1 - amount) * 255);
            int muteG = (int)(this.Color.G * amount + greyScale * (1 - amount) * 255);
            int muteB = (int)(this.Color.B * amount + greyScale * (1 - amount) * 255);

            // Ensure the RGB values are within the valid range
            muteR = Math.Min(255, Math.Max(0, muteR));
            muteG = Math.Min(255, Math.Max(0, muteG));
            muteB = Math.Min(255, Math.Max(0, muteB));

            return Color.FromArgb(this.Color.A, muteR, muteG, muteB);
        }
        public Color Color {
            get {
                if (this.IsBarSection) {
                    return Color.FromArgb(23, 26, 33);
                }
                //if (Colors.ContainsKey(Number)) {
                //    return Colors[Number].BackgroundColor;
                //}
                return SectionColorManager.GetColorPair(Number).BackgroundColor;

                //return Color.White;

            }

        }
        public Color FontColor
        {
            get
            {
                if (this.IsBarSection)
                {
                    return Color.White;
                }

                //if (Colors.ContainsKey(Number))
                //{
                //    return Colors[Number].FontColor;
                //}

                //return Color.White;
                return SectionColorManager.GetColorPair(Number).FontColor;
            }
        }        
        public Dictionary<int, ColorPair> Colors { get; } = new Dictionary<int, ColorPair>
        {
             { 1, new ColorPair(Color.FromArgb(17,100,184), Color.White) },
            { 2, new ColorPair(Color.FromArgb(105,209,0), Color.Black) },
            { 3, new ColorPair(Color.FromArgb(176,46,12), Color.White) },
            { 4, new ColorPair(Color.FromArgb(103,178,216), Color.Black) },
            { 5, new ColorPair(Color.ForestGreen, Color.White) },
            { 6, new ColorPair(Color.FromArgb(240,246,0), Color.Black) },


            { 7, new ColorPair(Color.FromArgb(70,17,122), Color.White) },
            { 8, new ColorPair(Color.FromArgb(65, 234, 212), Color.Black) },
            { 9, new ColorPair(Color.FromArgb(244,192,149), Color.Black) },
            { 10, new ColorPair(Color.FromArgb(130,9,29), Color.White) },
            { 11, new ColorPair(Color.FromArgb(194, 178, 180), Color.White) },
            { 12, new ColorPair(Color.FromArgb(7,79,87), Color.White) },
            { 13, new ColorPair(Color.FromArgb(250,127,127), Color.Black) },
            { 14, new ColorPair(Color.FromArgb(84,92,82), Color.White) },
            { 15, new ColorPair(Color.FromArgb(180,134,159), Color.Black) },
            { 100, new ColorPair(Color.LightGray, Color.Black) },
            { 101, new ColorPair(Color.Gray, Color.White) },
            { 102, new ColorPair(Color.DarkGray, Color.White) }
           


        };

        public Point MidPoint
        {
            get
            {
                int totalX = 0;
                int totalY = 0;
                foreach (Table table in this.Tables)
                {
                    totalX += table.XCoordinate + (table.Width / 2);  // X-coordinate of control's center
                    totalY += table.YCoordinate + (table.Height / 2); // Y-coordinate of control's center
                }
                return new Point(totalX / this.Tables.Count, totalY / this.Tables.Count);
            }
        }
        public Point MiniMidPoint
        {
            get
            {
                int totalX = 0;
                int totalY = 0;
                foreach (TemplateTable table in this.TemplateTables)
                {
                    totalX += table.XCoordinate + (table.Width / 2);  // X-coordinate of control's center
                    totalY += table.YCoordinate + (table.Height / 2); // Y-coordinate of control's center
                }
                return new Point(totalX / this.Tables.Count, totalY / this.Tables.Count);
            }
        }

       

        public override bool Equals(object obj)
        {
            // Check for null and compare run-time types.
            if (obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            Section other = (Section)obj;
            return this.Number == other.Number;
        }

        public override int GetHashCode()
        {
            return this.Number.GetHashCode();
        }

        internal void SetTemplateTeamWait(bool teamWaitValue)
        {
            this.TemplateTeamWait = teamWaitValue;
        }
        internal void SetTemplateBarSection(bool barSectionValue)
        {
            this.TemplateBarSection = barSectionValue;
        }
        public bool HasSameTables(Section sectionCompared)
        {
            
            var currentSectionTableNumbers = this.Tables.Select(t => t.TableNumber).OrderBy(n => n).ToList();

          
            var comparedSectionTableNumbers = sectionCompared.Tables.Select(t => t.TableNumber).OrderBy(n => n).ToList();

           
            return currentSectionTableNumbers.SequenceEqual(comparedSectionTableNumbers);
        }
        public List<Point> ConvexHullPoints()
        {
            List<Point> allPoints = new List<Point>();
            foreach (var table in this.Tables)
            {
                allPoints.AddRange(table.GetCornerPoints());
            }

            return ConvexHull.GetConvexHull(allPoints);
        }
        public override string ToString()
        {
            string servers = "";
            foreach (Server server in ServerTeam)
            {
                servers += " | " + server.ToString();
            }
            string pairedSection = "";
            if (this._pairedSection != null)
            {
                pairedSection = "Paired: " + _pairedSection.Number + " " + _pairedSection.ExpectedTotalSales;
            }
            return $"{Number} {ExpectedTotalSales} {servers} {pairedSection}";
        }

       
    }
}

