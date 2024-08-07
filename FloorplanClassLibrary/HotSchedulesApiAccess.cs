using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FloorplanClassLibrary
{
    public static class HotSchedulesApiAccess
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
        //This is CompanyID confirmed
        private static string GetHotSchedulesCompanyID()
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
        private static string GetHotSchedulesConceptID()
        {
            string conceptID = ConfigurationManager.AppSettings["HotSchedulesConceptID"];
            if (string.IsNullOrEmpty(conceptID))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return conceptID;
        }
        private static string GetHotSchedulesStoreID()
        {
            string storeID = ConfigurationManager.AppSettings["HotSchedulesStoreID"];
            if (string.IsNullOrEmpty(storeID))
            {
                throw new InvalidOperationException("API key is missing.");
            }
            return storeID;
        }
        private static async Task<string> GenerateBearerToken()
        {
            using (var client = new HttpClient())
            {
                var loginUrl = "https://agent-pos-na1.fourth.com/login";
                var credentials = new
                {
                    user_id = GetHotSchedulesUserName(),
                    password = GetHotSchedulesPassword()
                };

                var json = JsonConvert.SerializeObject(credentials);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(loginUrl, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Failed to generate token.");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                dynamic responseObject = JsonConvert.DeserializeObject(responseBody);

                return responseObject.Bearer;
            }
        }

        public static async Task<string> GetScheduledServers()
        {
            string token = await GenerateBearerToken();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string conceptId = GetHotSchedulesCompanyID();
                string storeId = GetHotSchedulesClient();
                string url = $"https://agent-pos-na1.fourth.com/{conceptId}/{storeId}/shifts?daysInPast=0&daysInFuture=6";
                //string url = $"https://agent-pos-na1.fourth.com/{GetHotSchedulesCO()}" +
                //    $"/controllers/vertx/hotschedules/{conceptId}/{storeId}/" +
                //    $"shifts?daysInPast=0&daysInFuture=6";

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    throw new InvalidOperationException("Failed to retrieve scheduled servers.");
                }

                var responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
        }
        //private static async Task<string> GenerateBearerToken()
        //{
        //    string userName = GetHotSchedulesUserName();
        //    string password = GetHotSchedulesPassword();

        //    var httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    var json = JsonConvert.SerializeObject(new { user_id = userName, password = password });
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await httpClient.PostAsync("https://agent-posna1.fourth.com/login", content);
        //    response.EnsureSuccessStatusCode();

        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    dynamic result = JsonConvert.DeserializeObject(responseContent);

        //    return result.Bearer;
        //}

        public static async Task<string> GetSchedule()
        {
            string bearerToken = await GenerateBearerToken();
            string co = GetHotSchedulesCompanyID();
           
            string start_date = "2024-08-05";
            string end_date = "2024-08-11";
            string concept_id = GetHotSchedulesConceptID(); 
            string store_id = GetHotSchedulesStoreID();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = $"https://agent-pos-na1.fourth.com/{co}/controllers/vertx/hotschedules/{concept_id}/{store_id}/getScheduleV3?start_day=5&start_month=8&start_year=2024&end_day=11&end_month=8&end_year=2024";
           

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return responseContent; 
        }
        //public static async Task<string> GetAllEmployees()
        //{
        //    string bearerToken = await GenerateBearerToken();
        //    string co = GetHotSchedulesCompanyID();  
        //    string concept_id = GetHotSchedulesConceptID(); 
        //    string store_id = GetHotSchedulesStoreID(); 

        //    var httpClient = new HttpClient();
        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
        //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //    string url = $"https://agent-pos-na1.fourth.com/{co}/controllers/vertx/hotschedules/{concept_id}/{store_id}/getStoreEmployees?active_only=true";

        //    var response = await httpClient.GetAsync(url);
        //    response.EnsureSuccessStatusCode();

        //    var responseContent = await response.Content.ReadAsStringAsync();
        //    return responseContent;
        //}
        public static async Task<List<HotSchedulesEmployee>> GetAllEmployees()
        {
            string bearerToken = await GenerateBearerToken();
            string co = GetHotSchedulesCompanyID();
            string concept_id = GetHotSchedulesConceptID();
            string store_id = GetHotSchedulesStoreID();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = $"https://agent-pos-na1.fourth.com/{co}/controllers/vertx/hotschedules/{concept_id}/{store_id}/getStoreEmployees?active_only=true";

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            List<HotSchedulesEmployee> employees = JsonConvert.DeserializeObject<List<HotSchedulesEmployee>>(responseContent);

            return employees;
        }
        public static async Task<List<HotSchedulesEmployee>> GetAllEmployeeJobs()
        {
            string bearerToken = await GenerateBearerToken();
            string co = GetHotSchedulesCompanyID();
            string concept_id = GetHotSchedulesConceptID();
            string store_id = GetHotSchedulesStoreID();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = $"https://agent-pos-na1.fourth.com/{co}/controllers/vertx/hotschedules/{concept_id}/{store_id}/getStoreEmployees?active_only=true";

            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            List<HotSchedulesEmployee> employees = JsonConvert.DeserializeObject<List<HotSchedulesEmployee>>(responseContent);

            return employees;
        }
        public static async Task<string> Test()
        {
            string username = GetHotSchedulesUserName();
            string password = GetHotSchedulesPassword();
            string clientId = GetHotSchedulesClient();
            string companyId = GetHotSchedulesCompanyID();
            string storeId = GetHotSchedulesStoreID();
            string conceptID = GetHotSchedulesConceptID();
            
            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/shifts?daysInPast=0&daysInFuture=6";
            string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/{conceptID}/{storeId}/shifts?fromDate=20240805&toDate=20240811";
            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/getConcepts";
            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/{companyId}/{storeId}/getEmpInfo?active_only=true";

            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/{storeId}/{clientId}/getEmpInfo?active_only=true";
            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/{clientId}/{storeId}/getEmpInfo?active_only=true";
            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/{storeId}/{conceptID}/getEmpInfo?active_only=true";
            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/{conceptID}/{clientId}/getEmpInfo?active_only=true";
            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/{conceptID}/{storeId}/getEmpInfo?active_only=true";
            //string url = $"https://agent-pos-na1.fourth.com/{companyId}/controllers/vertx/hotschedules/{clientId}/{conceptID}/getEmpInfo?active_only=true";

            using (HttpClient client = new HttpClient())
            {
                var authToken = Encoding.ASCII.GetBytes($"{username}:{password}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Send GET request
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    // Read and process the response
                    return await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("Response content:");
                    //Console.WriteLine(content);
                }
                else
                {
                    return $"Error: {response.StatusCode}";
                   
                }
            }
        }
    }
}
