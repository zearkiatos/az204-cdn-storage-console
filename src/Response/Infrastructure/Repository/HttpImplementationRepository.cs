using CDNStorageConsole.Response.Domain.Repository;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CDNStorageConsole.Response.Infrastructure.Repository
{
    public class HttpImplementationRepository : HttpRepository
    {
        private readonly HttpClient httpClient;

        public HttpImplementationRepository()
        {
            this.httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> Get(string url)
        {
            var response = await httpClient.GetAsync(url);
            return response;
        }
    }
}