using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public record DiningAreaRecord
    {
        public int ID {  get; set; }
        public int DiningAreaID { get; set; }
        public DateOnly DateOnly { get; set; }        
        public float Sales { get; set; }
        public bool IsAm { get; set; }       
        public int ServerCount { get; set; }    
        public List<TableStat> TableStats { get; set; }
        public List<TablePercentageRecord> TablePercentages { get; set; } = new List<TablePercentageRecord>();
        //public List<Table> tables { get; set; } = new List<Table>();
        public float PercentageOfSales { get; set; }
        
        
    }
}
