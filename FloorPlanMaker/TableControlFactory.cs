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
                Moveable = false,  
                Shape = table.Shape,
                Location = new Point(table.XCoordinate, table.YCoordinate),

                Tag = table
            };
        }
        public static void RedrawTableControl(TableControl tableControl, Panel panel)
        {
            panel.Controls.Remove(tableControl);
            CreateTableControl(tableControl.Table);
            panel.Controls.Add(tableControl);
        }
    }

}
