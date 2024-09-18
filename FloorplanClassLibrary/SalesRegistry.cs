using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class SalesRegistry
    {
        private static Dictionary<int, float> diningAreaSales = new Dictionary<int, float>();
        private static Dictionary<int, float> tableSales = new Dictionary<int, float>();

        public static float GetDiningAreaExpectedSales(int id)
        {
            return diningAreaSales.TryGetValue(id, out var sales) ? sales : 0f;
        }

        public static void SetDiningAreaExpectedSales(int id, float sales)
        {
            diningAreaSales[id] = sales;
        }

        public static float GetTableAverageSales(int id)
        {
            return tableSales.TryGetValue(id, out var sales) ? sales : 0f;
        }

        public static void SetTableAverageSales(int id, float sales)
        {
            tableSales[id] = sales;
        }
    }

}
