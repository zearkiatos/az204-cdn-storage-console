using System.Net.Http;
using System.Threading.Tasks;
namespace CDNStorageConsole.Response.Domain.Repository
{
    interface HttpRepository
    {
        Task<HttpResponseMessage> Get(string url);
    }
}