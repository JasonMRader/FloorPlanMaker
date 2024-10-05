using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class ReservationDataAccess
    {
        private static string GetOpenTableClientID_UAT() {
            string id = ConfigurationManager.AppSettings["OpenTableClientID_UAT"];
            if (string.IsNullOrEmpty(id)) {
                throw new InvalidOperationException("API key is missing.");
            }
            return id;
        }
        private static string GetOpenTableClientID()
        {
            string id = ConfigurationManager.AppSettings["OpenTableClientID"];
            if (string.IsNullOrEmpty(id)) {
                throw new InvalidOperationException("API key is missing.");
            }
            return id;
        }
        private static string GetOpenTableSecretID_UAT()
        {
            string id = ConfigurationManager.AppSettings["OpenTableSecretID_UAT"];
            if (string.IsNullOrEmpty(id)) {
                throw new InvalidOperationException("API key is missing.");
            }
            return id;
        }
        private static string GetOpenTableSecretID()
        {
            string id = ConfigurationManager.AppSettings["OpenTableSecretID"];
            if (string.IsNullOrEmpty(id)) {
                throw new InvalidOperationException("API key is missing.");
            }
            return id;
        }
    }
}
