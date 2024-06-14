using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class SalesData
    {
        private TableSalesManager tableSalesManager { get; set; }
        public DateTime Date { get; set; }
        public DateOnly DateOnly => DateOnly.FromDateTime(Date);
        public Dictionary<string, float> SalesByDiningArea { get; set; }
        public float TotalSales { get; set; }
        public string DateDisplay()
        {
            SpecialEventDate specialEventDate = SqliteDataAccess.GetEventByDate(DateOnly);
            if (specialEventDate != null)
            {
               return specialEventDate.Name;
            }
            return Date.ToString("ddd, M/d");
        }
    }
}
