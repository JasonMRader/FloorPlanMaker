using NetTopologySuite.Triangulate;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

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
        public Section(Section section)
        {
            this.Number = section.Number;
            this.IsCloser = section.IsCloser;
            this.IsPickUp = section.IsPickUp;
            this.IsPre = section.IsPre;
            this.Name = section.Name;
            //this.Floorplan = section.Floorplan;
            this.ServerCount = section.ServerTeam.Count;
            this.DiningAreaID = section.DiningAreaID; 
            this.SetTableList( section.Tables.ToList());
        }
        public Section CopySection()
        {
            Section copy = new Section();
            copy.Number = this.Number;
            copy.IsCloser = this.IsCloser;
            copy.IsPickUp = this.IsPickUp;
            copy.IsPre = this.IsPre;
            copy.Name = this.Name;
            //this.Floorplan = section.Floorplan;
            //this.ServerCount = section.ServerTeam.Count;
            copy.SetSectionPropertiesFromTemplateSection(this);
            copy.DiningAreaID = this.DiningAreaID;
            copy.SetTableList(this.Tables.ToList());
            return copy;
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

        public Floorplan? Floorplan { get; set; }
        public int ServerCount { get; private set; } = 1;
        public int TemplateServerCount { get; set; }
        public bool TemplateTeamWait { get; set; }
        public bool TemplatePickUp { get; set; }    
        public int ID {  get; set; }
        public bool IsPickUp { get; set; }
        public int DiningAreaID { get; set; }
        public List<TemplateTable> TemplateTables { get; set; } = new List<TemplateTable>();

        public bool IsSelected { get; private set; } = false;
        public void SetToSelected()
        {
            this.IsSelected = true;
            NotifyObservers();
        } 
        public void NotSelected()
        {
            this.IsSelected = false;
            NotifyObservers();
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
        public void MakeTeamWait()
        {
            _isTeamWait = true;
            TemplateTeamWait = true;
            ServerCount++;
            NotifyObservers();
        }
        private void SetSectionPropertiesFromTemplateSection(Section sectionToCopy)
        {
            
            if(sectionToCopy.TemplateTeamWait)
            {
                _isTeamWait = true;
                ServerCount = sectionToCopy.ServerCount;
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
            ServerCount--;
            
            NotifyObservers();
            return true;
            //Potential redundant notification
        }

        public void MakeSoloSection()
        {
            _isTeamWait = false;
            TemplateTeamWait = false;
            ServerTeam = ServerTeam.Take(1).ToList();
            this.ServerCount = 1;
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
            if (server.CurrentSection != null && server.CurrentSection != this)
            {
                server.CurrentSection.RemoveServer(server);
                //NotifyRemovedFromSection(server);
            }

            if (!IsTeamWait)
            {
                ServerTeam.Clear();
            }

            if (!ServerTeam.Contains(server))
            {
                ServerTeam.Add(server);
            }
            
            server.CurrentSection = this;
            NotifyServerAssigned(server);
            NotifyObservers();
        }

        public void RemoveServer(Server server)
        {
            if (ServerTeam.Contains(server))
            {
                ServerTeam.Remove(server);
                server.CurrentSection = null;
                //if (ServerCount > ServerTeam.Count)
                NotifyRemovedFromSection(server);
                NotifyObservers();
            }
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
            NotifyObservers();
        }
        public void RemoveTable(Table table)
        {
            this._tables.RemoveAll(t => t.ID == table.ID);
            NotifyObservers();
        }
        public void SetTableList(List<Table> tables)
        {
            this._tables = tables;
            NotifyObservers();
        }
        public void ClearTables()
        {
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

        public float AverageCovers
        {
            get
            {
                if (Tables == null || !Tables.Any()) return 0;
                
                return Tables.Sum(table => table.AverageCovers);
            }
        }
        public string AverageCoversDisplay()
        {
            CultureInfo culture = CultureInfo.CurrentCulture;

            // If the value is negative, prepend a minus sign and format the absolute value.
            // Otherwise, just format the value as currency.
            string formattedValue = this.AverageCovers >= 0
                ? this.AverageCovers.ToString("C0", culture)
                : "-" + Math.Abs(this.AverageCovers).ToString("C0", culture);

            return formattedValue;
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
            string displayString = "";
            //if (this.Server == null)
            //{
            //    displayString =
            //}
            if (this.Name != null)
            {
                displayString = this.Name;
            }
            if (this.Server != null)
            {
                displayString = Server.AbbreviatedName;
            }
            
            if (this.IsTeamWait)
            {
                displayString = "";
                foreach (Server server in this.ServerTeam)
                {
                    displayString += server.AbbreviatedName;
                    if(server != ServerTeam.Last())
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
            if(this.IsPickUp)
            {
                displayString = "PickUp";
            }
            //NotifyObservers();
            return displayString;
            
        }

        

       
        public int Number { get; set; }
        public Color Color
        {
            get
            {
                if (this.IsPickUp)
                {
                    return Color.DarkGray;
                }
                
                if (Colors.TryGetValue(Number, out Color value))
                {
                    return value;
                }
                
                return Color.White;
            }
            set
            {
                
            }
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

        public Color FontColor
        {
            get
            {
                int[] whiteFontNumbers = { 3, 5, 6,9,10,12,13,17,19 };
                if (whiteFontNumbers.Contains(Number))
                {
                    return Color.White;
                }
                else
                {
                    return Color.Black;
                }
            }
        }
        public Dictionary<int, Color> Colors { get; } = new Dictionary<int, Color>
        {
            { 1, Color.FromArgb(103,178,216) },
            { 2, Color.FromArgb(105,209,0) },
            { 6, Color.FromArgb(130,9,29) },
            { 3, Color.FromArgb(242,124,5) },
            { 4, Color.FromArgb(17,100,184) },
            { 5, Color.FromArgb(70,17,122) },
            { 7, Color.FromArgb(240,246,0) },
            { 8, Color.FromArgb(250,127,127) },
            { 9, Color.FromArgb(87,61,28) },
            { 10, Color.FromArgb(26,83,92) },
            { 11, Color.FromArgb(194,178,180) },
            { 12, Color.FromArgb(23,26,33) },
            { 13, Color.FromArgb(84,92,82) },
            { 14, Color.FromArgb(243,227,124) },
            { 15, Color.FromArgb(244,192,149) },
            { 16, Color.FromArgb(180,134,159) },
            { 17, Color.FromArgb(7,79,87) },
            { 18, Color.FromArgb(65,234,212) },
            { 19, Color.FromArgb(88,44,77) },
            { 20, Color.FromArgb(176,46,12) }
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

        internal void SetTeamWait(bool teamWaitValue)
        {
            this.TemplateTeamWait = teamWaitValue;
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

    }
}

