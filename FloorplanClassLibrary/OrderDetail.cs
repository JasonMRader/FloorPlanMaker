using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class OrderDetail
    {
        [Name("Order #")]
        public int OrderNumber { get; set; }

        public DateTime Opened { get; set; }

        [Name("# of Guests")]
        public int NumberOfGuests { get; set; }

        public string Server { get; set; }

        public int? Table { get; set; } // Nullable, in case there are missing values

        [Name("Discount Amount")]
        public double DiscountAmount { get; set; }

        public double Amount { get; set; }
        public double Tax { get; set; }
        public double Tip { get; set; }

        public double Gratuity { get; set; }
    }

}
