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
        public float GetAverageCovers()
        {
            if (Tables == null)
            {
                return 0;
            }
            float avgCovers = 0;
            foreach (Table table in Tables)
            {
                avgCovers += table.AverageCovers;
            }
            return avgCovers;
        }
        public enum SubArea
        {
            Awning,
            Extended,
            Dungeon,
            Fireplace,
            VIP

        }
    }
}
