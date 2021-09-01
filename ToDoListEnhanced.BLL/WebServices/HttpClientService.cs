using System.Net.Http;

namespace ToDoListEnhanced.ClientBLL.WebServices
{
    public static class HttpClientService
    {
        public static HttpClient CreateClient(string accessToken = "")
        {
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            }
            return client;
        }
    }
}
