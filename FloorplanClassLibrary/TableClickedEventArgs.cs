using FloorplanClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorPlanMaker
{
    public class TableClickedEventArgs : EventArgs
    {
        public Table ClickedTable { get; }
        public bool IsMoveable { get; }
        public MouseButtons MouseButton { get; set; }

        public TableClickedEventArgs(Table table, bool isMoveable)
        {
            ClickedTable = table;
            IsMoveable = isMoveable;
            
        }
    }

}
