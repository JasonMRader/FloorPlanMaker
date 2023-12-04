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

        public TemplateTable(Table table, Section section, float factor, int yAdjustment) : base()
        {
            this.ID = table.ID;
            this.TableNumber = table.TableNumber;
            this.MaxCovers = table.MaxCovers;
            this.AverageCovers = table.AverageCovers;
            this.DiningArea = table.DiningArea;
            this.DiningAreaId = table.DiningAreaId;
            this.XCoordinate = (int)(table.XCoordinate * factor); 
            this.YCoordinate = (int)(table.YCoordinate * factor) + yAdjustment;
            this.Shape = table.Shape;
            this.Width = (int)(table.Width * factor);
            this.Height = Height = (int)(table.Height * factor);
            this.Section = section;
            section.TemplateTables.Add(this);   

            // Initialize the Section property if needed
            // this.Section = new Section(); // Or however you want to handle the Section
        }
    }
}
