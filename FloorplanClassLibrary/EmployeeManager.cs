﻿using System;
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
            ActiveServers = SqliteDataAccess.LoadActiveServers().OrderByFirstLetter().ToList();
            InactiveServers = SqliteDataAccess.LoadArchivedServers().OrderByFirstLetter().ToList();

        }
        public List<Server> ActiveServers { get; set; }
        public List<Server> InactiveServers { get; set; }
        public void LoadShiftsForActiveServers()
        {
            foreach (var server in ActiveServers)
            {
                server.Shifts = SqliteDataAccess.GetShiftsForServer(server);
            }
        }
      
       
    }
}
