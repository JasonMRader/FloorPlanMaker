using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public static class TableControlFactory
    {
        public static TableControl CreateTableControl(Table table)
        {
            return new TableControl
            {
                Table = table,
                Width = table.Width,
                Height = table.Height,
                Left = table.XCoordinate,
                Top = table.YCoordinate,
                Moveable = true,  // Adjust this based on your requirements
                Shape = table.Shape,
                Location = new Point(table.XCoordinate, table.YCoordinate),
                Tag = table
            };
        }
    }

}
