using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanUserControlLibrary
{
    public class ListBoxDateItem
    {
        public DateOnly DateValue { get; set; }
        public string DisplayDate { get; set; }

        public ListBoxDateItem(DateOnly date)
        {
            DateValue = date;
            DisplayDate = date.ToString("dddd, MMMM dd");
        }

        public override string ToString()
        {
            return DisplayDate;
        }
    }

}
