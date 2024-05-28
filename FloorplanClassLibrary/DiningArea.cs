using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FloorplanClassLibrary
{
    public class DiningArea
    {
        public DiningArea(string name, bool isInside) 
        {
            Name = name;
            IsInside = isInside;
        
        }
        public DiningArea() { }
        public TableSalesManager TableSalesManager = new TableSalesManager();
        public float ExpectedSales { get; private set; }
       
        public int ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public bool IsInside { get; set; }
        public List<Table>? Tables { get; set; }
        public void SetTableSales(List<TableStat> tableStats)
        {
            float totalSales = 0f;
            var insideBarSales = tableStats
               .Where(t => t.TableStatNumber.CompareTo("120") >= 0 && t.TableStatNumber.CompareTo("155") <= 0)
               .Sum(t => t.Sales);
            var outsideBarSales = tableStats
                .Where(t => t.TableStatNumber.CompareTo("1300") >= 0 && t.TableStatNumber.CompareTo("1699") <= 0)
                .Sum(t => t.Sales);
            foreach (Table table in this.Tables)
            {
                if (table.TableNumber == "INSIDE BAR")
                {
                    table.AverageSales = (float)insideBarSales;
                    totalSales += (float)insideBarSales;
                    //test += $"\n{table.TableNumber} : {insideBarSales} : {totalAreaSales}";
                }
                if (table.TableNumber == "OUTSIDE BAR")
                {
                    table.AverageSales = (float)outsideBarSales;
                    totalSales += (float)outsideBarSales;
                    //test += $"\n{table.TableNumber} : {insideBarSales} : {totalAreaSales}";
                }
                else
                {
                    var matchedStat = tableStats.FirstOrDefault(t => t.TableStatNumber == table.TableNumber);
                    if (matchedStat != null)
                    {
                        table.AverageSales = (float)matchedStat.Sales;
                        totalSales += (float)matchedStat.Sales;
                        //test += $"\n{table.TableNumber} : {matchedStat.Sales} : {totalAreaSales}";
                    }
                    else
                    {
                        table.AverageSales = 0;
                    }
                }
            }
            ExpectedSales = totalSales;
        }
        public int GetMaxCovers()
        {
            if (Tables == null)
            {
                return 0;
            }
            int maxCovers = 0;  
            foreach(Table table in Tables)
            {
                maxCovers += table.MaxCovers;
            }
            return maxCovers;
        }
        public float GetAverageSales()
        {
            if (Tables == null)
            {
                return 0;
            }
            float avgSales = 0;
            foreach (Table table in Tables)
            {
                avgSales += table.AverageSales;
            }
            return avgSales;
        }
        public enum SubArea
        {
            Awning,
            Extended,
            Dungeon,
            Fireplace,
            VIP

        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            DiningArea other = (DiningArea)obj;
            // Assuming ID is a unique identifier for DiningArea
            return this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            // Assuming ID is an int and a unique identifier for DiningArea
            return this.ID.GetHashCode();
        }
    }
}
