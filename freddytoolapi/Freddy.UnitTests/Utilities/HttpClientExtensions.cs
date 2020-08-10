using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Freddy.Application.UnitTests.Utilities
{
    public static class HttpClientExtensions
    {
        public static TObject Deserialize<TObject>(this string str)
        {
            return str.Any() ? JsonSerializer.Deserialize<TObject>(str, Options) : default;
        }

        private static readonly JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public static async Task<TObject> GetObjectAsync<TObject>(this HttpClient client, string url)
        {
            var response = await client.GetStringAsync(url);

            return response.Deserialize<TObject>();
        }

        public static async Task<HttpResponseMessage> PostObjectAsync<TObject>(this HttpClient client, string url, TObject o)
        {
            var payload = JsonSerializer.Serialize(o, Options);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            return response;
        }

        public static async Task<HttpResponseMessage> PutObjectAsync<TObject>(this HttpClient client, string url, TObject o)
        {
            var payload = JsonSerializer.Serialize(o, Options);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, content);
            return response;
        }
    }
}