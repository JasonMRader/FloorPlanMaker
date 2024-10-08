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
    public static class ReservationDataAccess
    {
        private static string _accessToken;
        private static DateTime _tokenExpiry;
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
        public static async Task<string> GetAccessTokenAsync()
        {
            // Check if the token is still valid
            if (!string.IsNullOrEmpty(_accessToken) && DateTime.UtcNow < _tokenExpiry) {
                return _accessToken;
            }

            // Get client credentials
            string clientId = GetOpenTableClientID();
            string clientSecret = GetOpenTableSecretID();

            // Concatenate and base64 encode credentials
            string credentials = $"{clientId}:{clientSecret}";
            string base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            // Prepare the request
            using (var client = new HttpClient()) {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
                string tokenUrl = "https://oauth.opentable.com/api/v2/oauth/token?grant_type=client_credentials";

                var response = await client.GetAsync(tokenUrl);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(content);

                    _accessToken = tokenResponse.AccessToken;
                    _tokenExpiry = DateTime.UtcNow.AddSeconds(tokenResponse.ExpiresIn);

                    return _accessToken;
                }
                else {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to get access token: {response.StatusCode} {errorContent}");
                }
            }
        }

        private class TokenResponse
        {
            [JsonProperty("access_token")]
            public string AccessToken { get; set; }
            [JsonProperty("token_type")]
            public string TokenType { get; set; }
            [JsonProperty("expires_in")]
            public int ExpiresIn { get; set; }
            [JsonProperty("scope")]
            public string Scope { get; set; }
        }
        public static async Task<List<Reservation>> GetReservationsAsync(string rid, DateTime scheduledTimeFrom, DateTime scheduledTimeTo)
        {
            string accessToken = await GetAccessTokenAsync();

            string url = "https://platform.opentable.com/sync/v2/reservations";

            var queryParams = new Dictionary<string, string>
            {
                { "rid", rid },
                { "scheduled_time_from", scheduledTimeFrom.ToString("yyyy-MM-ddTHH:mm:ss") },
                { "scheduled_time_to", scheduledTimeTo.ToString("yyyy-MM-ddTHH:mm:ss") }
            };

            var queryString = string.Join("&", queryParams.Select(p => $"{p.Key}={Uri.EscapeDataString(p.Value)}"));
            string requestUrl = $"{url}?{queryString}";

            using (var client = new HttpClient()) {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode) {
                    string content = await response.Content.ReadAsStringAsync();

                    // Assuming the response is a JSON array of reservations
                    var reservations = JsonConvert.DeserializeObject<List<Reservation>>(content);
                    return reservations;
                }
                else {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to get reservations: {response.StatusCode} {errorContent}");
                }
            }
        }


    }
}
