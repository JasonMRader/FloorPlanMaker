using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public record AreaHistory
    {
        public AreaHistory(DiningArea area, DateOnly dateOnly, bool isAm)
        {
            DiningArea = area;
            DateOnly = dateOnly;
            Sales = DiningArea.GetTotalSalesForDateAndIsAm(isAm, dateOnly);
            IsAm = isAm;
            ServerCount = GetServerCount();
        }
        public DiningArea DiningArea { get; set; }
        public DateOnly DateOnly { get; set; }
        public float Sales { get; set; }
        public bool IsAm { get; set; }
        public int ServerCount { get; set; }       
        private int GetServerCount()
        {
            Floorplan matchedFP = SqliteDataAccess.LoadFloorplanByCriteria(DiningArea, DateOnly, IsAm);
            if(matchedFP != null)
            {
                return matchedFP.Servers.Count();
            }
            return 0;            
        }
    }
}
