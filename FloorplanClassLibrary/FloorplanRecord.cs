using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public record FloorplanRecord
    {
        public int ID {  get; set; }
        public int DiningAreaID { get; set; }
        public DateOnly DateOnly { get; set; }        
        public float Sales { get; set; }
        public bool IsAm { get; set; }       
        public int ServerCount { get; set; }    
        public List<TableStat> tableStats { get; set; }
        
        
    }
}
