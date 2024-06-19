using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ScheduledShift
    {
        public DateOnly Date { get; set; }
        public bool IsAm { get; set; }
        public List<string> Servers { get; set; } = new List<string>();

        public override string ToString()
        {
            string s = Date.ToString();
            if (IsAm)
            {
                s += "  " + "AM";
            }
            else
            {
                s += "  " + "PM";
            }
            foreach(var server in  Servers)
            {
                s +=  " | " + server ;
            }
            return s;
        }
    }

}
