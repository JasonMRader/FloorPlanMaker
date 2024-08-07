using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class HotSchedulesEmployee
    {

        public string ZipCode { get; set; }
        public DateTime HireDate { get; set; }
        public string Address { get; set; }
        public int ClientId { get; set; }
        public string LName { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string Mobile { get; set; }
        public string NName { get; set; }
        public int AltId { get; set; }
        public string FName { get; set; }
        public int EmpNum { get; set; }
        public string Phone { get; set; }
        public int HsId { get; set; }
        public DateTime Dob { get; set; }
        public string State { get; set; }
        public int StoreNum { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public override string ToString()
        {
            return $"{FName} {LName} {HsId}";
        }
    }
}
