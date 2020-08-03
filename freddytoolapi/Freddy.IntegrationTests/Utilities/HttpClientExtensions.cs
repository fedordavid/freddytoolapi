using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Freddy.IntegrationTests.Utilities
{
    public static class HttpClientExtensions
    {
        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        
        public static async Task<TObject> GetObjectAsync<TObject>(this HttpClient client, string url)
        {
            var response = await client.GetStringAsync(url);

            return response.Any() ? JsonSerializer.Deserialize<TObject>(response, Options) : default;
        }
        
        public static async Task<HttpResponseMessage> PostObjectAsync<TObject>(this HttpClient client, string url, TObject o)
        {
            var payload = JsonSerializer.Serialize(o, Options);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public static async Task<HttpResponseMessage> PutObjectAsync<TObject>(this HttpClient client, string url, TObject o)
        {
            var payload = JsonSerializer.Serialize(o, Options);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, content);
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}