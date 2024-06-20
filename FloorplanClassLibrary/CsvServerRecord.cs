using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public override string ToString()
        {
            return $"{EmployeeName} {JobCode} ";
        }
    }


    public class CsvServerRecordMap : ClassMap<CsvServerRecord>
    {
        public CsvServerRecordMap()
        {
            Map(m => m.JobCode).Index(0).Name("Job Code");
            Map(m => m.EmployeeName).Index(1).Name("Employee name");
            Map(m => m.Mon).Index(2).Name("Mon, 11/13/23");
            Map(m => m.Tue).Index(3).Name("Tue, 11/14/23");
            Map(m => m.Wed).Index(4).Name("Wed, 11/15/23");
            Map(m => m.Thu).Index(5).Name("Thu, 11/16/23");
            Map(m => m.Fri).Index(6).Name("Fri, 11/17/23");
            Map(m => m.Sat).Index(7).Name("Sat, 11/18/23");
            Map(m => m.Sun).Index(8).Name("Sun, 11/19/23");
        }
    }

}

