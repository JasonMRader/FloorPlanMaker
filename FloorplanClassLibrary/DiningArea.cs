using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public float ExpectedSales()
        {
            return this.TableSalesManager.DiningAreaExpectedSales(this);
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
        public bool IsInside { get; set; }
        public List<Table>? Tables { get; set; }
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
