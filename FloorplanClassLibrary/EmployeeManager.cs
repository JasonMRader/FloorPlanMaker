using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class EmployeeManager
    {
        public EmployeeManager() 
        {
            AllServers = SqliteDataAccess.LoadServers();
        }
        public List<Server> AllServers { get; set; }
        public List<Server>? ServersOnShift { get; set; }
    }
}
