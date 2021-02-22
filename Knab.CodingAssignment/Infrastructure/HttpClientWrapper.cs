using System.Net.Http;
using System.Threading.Tasks;

namespace Knab.CodingAssignment.Infrastructure
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private readonly HttpClient _client;

        public HttpClientWrapper()
        {
            _client = new HttpClient();
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _client.GetAsync(url);
        }

        public void AddHeader(string key, string value)
        {
            _client.DefaultRequestHeaders.Add(key, value);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}