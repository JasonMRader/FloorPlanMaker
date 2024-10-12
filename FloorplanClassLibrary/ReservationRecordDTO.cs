using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ReservationRecordDTO
    {
        public long Id { get; set; }
        public int Covers { get; set; }
        public string DateTime { get; set; }
        public string TimeCreated { get; set; }
        public string TimeUpdated { get; set; }
        public float? CheckTotal { get; set; }
        public string Server { get; set; }
        public string Request { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public string State { get; set; }
    }

    public class TableNumberDTO
    {
        public long ReservationId { get; set; }
        public string TableNumber { get; set; }
    }

    public class VisitTagDTO
    {
        public long ReservationId { get; set; }
        public string Tag { get; set; }
    }

}
