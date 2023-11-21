using System;
using System.Collections.Generic;
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
        private SectionNodeManager _nodeManager;

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
        public List<Server>? ServerTeam { get ; set; } 
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
            if (this._isTeamWait &&  this.Server2 != null) 
            {
                displayString = displayString + "\n" + this.Server2.AbbreviatedName;
            }
            return displayString;
        }
        private bool _isTeamWait { get; set; }
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
        

    }
}
