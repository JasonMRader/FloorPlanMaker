using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class CsvServerRecord
    {
        public string JobCode { get; set; }
        public string EmployeeName { get; set; }
        public string Mon { get; set; }
        public string Tue { get; set; }
        public string Wed { get; set; }
        public string Thu { get; set; }
        public string Fri { get; set; }
        public string Sat { get; set; }
        public string Sun { get; set; }
    }
    public class CsvServerRecordMap : ClassMap<CsvServerRecord>
    {
        public CsvServerRecordMap()
        {
            Map(m => m.JobCode).Index(0);
            Map(m => m.EmployeeName).Index(1);
            Map(m => m.Mon).Index(2);
            Map(m => m.Tue).Index(3);
            Map(m => m.Wed).Index(4);
            Map(m => m.Thu).Index(5);
            Map(m => m.Fri).Index(6);
            Map(m => m.Sat).Index(7);
            Map(m => m.Sun).Index(8);
        }
    }
}
