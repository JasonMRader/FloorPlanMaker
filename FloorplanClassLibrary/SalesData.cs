using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SalesData
    {
        private TableSalesManager tableSalesManager { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, float> SalesByDiningArea { get; set; }
        public float TotalSales { get; set; }
    }
}
