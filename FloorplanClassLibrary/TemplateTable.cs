using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class TemplateTable : Table
    {
        public Section Section { get; set; }

        public TemplateTable(Table table, Section section) : base()
        {
            this.ID = table.ID;
            this.TableNumber = table.TableNumber;
            this.MaxCovers = table.MaxCovers;
            this.AverageCovers = table.AverageCovers;
            this.DiningArea = table.DiningArea;
            this.DiningAreaId = table.DiningAreaId;
            this.XCoordinate = table.XCoordinate;
            this.YCoordinate = table.YCoordinate;
            this.Shape = table.Shape;
            this.Width = table.Width;
            this.Height = table.Height;
            this.Section = section;

            // Initialize the Section property if needed
            // this.Section = new Section(); // Or however you want to handle the Section
        }
    }
}
