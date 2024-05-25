using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ShiftManager
    {
        public ShiftManager() { }
        public Shift SelectedShift { get; set; }
        public DateOnly DateOnly { get; set; }
        public bool IsAM {  get; set; } 
        public Shift NewShift { get; set; }
    }
}
