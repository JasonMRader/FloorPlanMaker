using FloorplanClassLibrary;
using FloorPlanMaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
    public class MiniTableControl : TableControl
    {
       
        public MiniTableControl(Table table, float factor, int yAdjustment)
        {
            Table = table;
            Width = (int)(table.Width * factor);
            Height = (int)(table.Height * factor);
            Left = (int)(table.XCoordinate * factor);
            Top = (int)(table.YCoordinate * factor) + yAdjustment;
            Moveable = false;
            Shape = table.Shape;
            Location = new Point((int)(table.XCoordinate * factor), (int)(table.YCoordinate * factor) + yAdjustment);
            Tag = table;
        }
    }
}
