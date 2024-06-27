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
        public void SetDatesToLastFourWeekdays()
        {
           


            var previousWeekdays = new List<DateOnly>();


            for (int i = 1; i <= 4; i++)
            {

                previousWeekdays.Add(DateOnly.AddDays(-7 * i));
            }


            List<TableStat> stats = SqliteDataAccess.LoadTableStatsByDateListAndLunch(IsAm, previousWeekdays);
            float totalAreaSales = 0f;
            foreach (Table table in DiningArea.Tables)
            {
                var matchedStat = stats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                if (matchedStat != null)
                {
                    table.AverageSales = (float)matchedStat.Sales;
                    totalAreaSales += (float)matchedStat.Sales;

                }
                else { table.AverageSales = 0; }

            }
            this.Sales = totalAreaSales;
        }
    }
}
