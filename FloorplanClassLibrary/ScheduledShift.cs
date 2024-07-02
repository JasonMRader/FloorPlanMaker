using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class ScheduledShift
    {
        public DateOnly Date { get; set; }
        public bool IsAm { get; set; }
        public List<string> Servers { get; set; } = new List<string>();

        public List<string> GetMissedServerNames(List<Server> allServers)
        {
            List<string> missedServers = new List<string>();
            foreach (string s in Servers)
            {
                string normalizedServerName = s.ToLower();
                bool matchFound = false;
                foreach (Server server in allServers)
                {
                    if (server.Name.ToLower() == normalizedServerName)
                    {
                        matchFound = true;
                    }
                }
                if (!matchFound)
                {
                    missedServers.Add(s);
                }
            }
            return missedServers;
        }

        public List<Server> GetServersFromRecord(List<Server> allServers)
        {
            List<Server> scheduledServers = new List<Server>();
            foreach(string s in Servers)
            {
                string normalizedServerName = s.ToLower();

                foreach (Server server in allServers)
                {
                    if (server.Name.ToLower() == normalizedServerName)
                    {
                        scheduledServers.Add(server);
                    }
                }
            }
            return scheduledServers;
        }
        public override string ToString()
        {
            string s = Date.ToString();
            if (IsAm)
            {
                s += "  " + "AM";
            }
            else
            {
                s += "  " + "PM";
            }
            foreach(var server in  Servers)
            {
                s +=  " | " + server ;
            }
            return s;
        }
    }

}
