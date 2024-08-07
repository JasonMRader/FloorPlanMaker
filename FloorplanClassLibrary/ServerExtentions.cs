using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class ServerExtensions
    {
        public static IEnumerable<Server> OrderByFirstLetter(this IEnumerable<Server> servers)
        {
            return servers.OrderBy(server => server.ToString());
        }
        public static IEnumerable<HotSchedulesEmployee> OrderByFirstLetter(this IEnumerable<HotSchedulesEmployee> employees)
        {
            return employees.OrderBy(e => e.FName);
        }
    }
}
