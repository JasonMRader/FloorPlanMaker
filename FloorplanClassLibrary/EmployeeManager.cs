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
            ActiveServers = SqliteDataAccess.LoadActiveServers();
            InactiveServers = SqliteDataAccess.LoadArchivedServers();
        }
        public List<Server> ActiveServers { get; set; }
        public List<Server> InactiveServers { get; set; }
        public List<Server> AllServers { get; set; }
        public List<Server>? ServersOnShift { get; set; }
    }
}
