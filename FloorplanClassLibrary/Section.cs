using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Security;
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
            Tables = new List<Table>();
        }
        public Section(Floorplan floorplan)
        {
            this.DiningAreaID = floorplan.DiningArea.ID;
            this.Tables = new List<Table>();
            
        }
        public Section(Section section)
        {
            this.Number = section.Number;
            this.IsCloser = section.IsCloser;
            this.IsPickUp = section.IsPickUp;
            this.IsPre = section.IsPre;
            this.Name = section.Name;
            this.DiningAreaID = section.DiningAreaID; 
            this.Tables = section.Tables;
        }
        public int ID {  get; set; }
        public bool IsPickUp { get; set; }
        public int DiningAreaID { get; set; }
        public string? Name { get; set; }
        public List<Table> Tables { get; set; }

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
        public Server? Server { get; set; }
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
        public Server? Server2 { get; set; }
        public bool IsCloser { get; set; }
        public bool IsPre { get; set; }
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
            if (this.IsCloser)
            {
                displayString = displayString + " (CLS)";
            }
            if (this.IsPre)
            {
                displayString = displayString + "(PRE)";
            }
            if (this.IsTeamWait &&  this.Server2 != null) 
            {
                displayString = displayString + "\n" + this.Server2.AbbreviatedName;
            }
            return displayString;
        }
        public bool IsTeamWait { get; set; }
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
            { 3, Color.FromArgb(130,9,29) },
            { 4, Color.FromArgb(242,124,5) },
            { 5, Color.FromArgb(17,100,184) },
            { 6, Color.FromArgb(70,17,122) },
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
        public List<TablePoint> GetTablePoints()
        {
            var points = new List<TablePoint>();

            foreach (var table in Tables)
            {
                switch (table.Shape)
                {
                    case Table.TableShape.Circle:
                        points.Add(new TablePoint(table.XCoordinate, table.YCoordinate));
                        break;

                    case Table.TableShape.Square:
                    case Table.TableShape.Diamond:  // For simplicity, treating diamond like square
                        points.Add(new TablePoint(table.XCoordinate, table.YCoordinate));
                        points.Add(new TablePoint(table.XCoordinate + table.Width, table.YCoordinate));
                        points.Add(new TablePoint(table.XCoordinate, table.YCoordinate + table.Height));
                        points.Add(new TablePoint(table.XCoordinate + table.Width, table.YCoordinate + table.Height));
                        break;
                }
            }

            return points;
        }

        public List<TablePoint> GetConvexHull()
        {
            var tablePoints = GetTablePoints();
            return ConvexHull.ComputeConvexHull(tablePoints);
        }
        public List<Tuple<TablePoint, TablePoint>> GetConvexHullEdges()
        {
            var convexHull = GetConvexHull();
            var edges = new List<Tuple<TablePoint, TablePoint>>();

            for (int i = 0; i < convexHull.Count; i++)
            {
                edges.Add(new Tuple<TablePoint, TablePoint>(convexHull[i], convexHull[(i + 1) % convexHull.Count]));
            }

            return edges;
        }

    }
}
