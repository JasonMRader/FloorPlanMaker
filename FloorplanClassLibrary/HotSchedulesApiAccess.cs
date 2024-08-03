using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public class HotSchedulesApiAccess
    {
        private static string GetHotSchedulesUserName()
        {

            string userName = ConfigurationManager.AppSettings["HotSchedulesUserName"];
            if (string.IsNullOrEmpty(userName))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return userName;
        }
        private static string GetHotSchedulesPassword()
        {

            string password = ConfigurationManager.AppSettings["HotSchedulesPassword"];
            if (string.IsNullOrEmpty(password))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return password;
        }
        private static string GetHotSchedulesCO()
        {
            string CO = ConfigurationManager.AppSettings["HotSchedulesCO"];
            if (string.IsNullOrEmpty(CO))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return CO;
        }
        private static string GetHotSchedulesClient()
        {
            string client = ConfigurationManager.AppSettings["HotSchedulesClient"];
            if (string.IsNullOrEmpty(client))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return client;
        }
    }
}
