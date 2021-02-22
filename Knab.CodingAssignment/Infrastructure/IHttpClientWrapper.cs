using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Knab.CodingAssignment.Infrastructure
{
    public interface IHttpClientWrapper : IDisposable
    {
        Task<HttpResponseMessage> GetAsync(string url);
        void AddHeader(string key, string value);
    }
}