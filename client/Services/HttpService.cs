using MangaReader.Config;
using MangaReader.Services.Authentication;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MangaReader.Services
{
    internal class HttpService
    {
        static public StringContent CreateRequestBody(object? body)
        {
            string json = JsonConvert.SerializeObject(body);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        static public async Task<T?> Get<T>(string endpoint)
        {
            using HttpClient client = GetClientWithSessionId();

            HttpResponseMessage response = await client.GetAsync($"{Server.url}/{endpoint}");

            if (!response.IsSuccessStatusCode) return default;

            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse)!;
        }

        static public async Task<HttpStatusCode> Post(string endpoint, object? body = null, Action<dynamic>? callback = null)
        {
            using HttpClient client = GetClientWithSessionId();

            HttpResponseMessage response = await client.PostAsync($"{Server.url}/{endpoint}", CreateRequestBody(body));
            if (response.IsSuccessStatusCode && callback is not null)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                dynamic responseData = JsonConvert.DeserializeObject(responseBody) ?? new { };
                callback.Invoke(responseData);
            }
            return response.StatusCode;
        }

        static public async Task<HttpStatusCode> Delete(string endpoint)
        {
            using HttpClient client = GetClientWithSessionId();

            HttpResponseMessage response = await client.DeleteAsync($"{Server.url}/{endpoint}");
            return response.StatusCode;
        }

        static private HttpClient GetClientWithSessionId()
        {
            HttpClient client = new();

            client.DefaultRequestHeaders.Add("User-Session-Id", LoginService.sessionId);
            return client;
        }
    }
}
