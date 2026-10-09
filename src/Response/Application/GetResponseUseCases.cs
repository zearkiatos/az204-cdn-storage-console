using System.Diagnostics;
using CDNStorageConsole.Configuration;
using CDNStorageConsole.Response.Application.Ports.Output;
using CDNStorageConsole.Response.Infrastructure.Repository;

namespace CDNStorageConsole.Response.Application
{
    public class GetResponseUseCases : GetResponseOutputPort
    {
        private readonly HttpImplementationRepository httpStorageAccountRepository;
        private readonly HttpImplementationRepository httpCdnRepository;
        public GetResponseUseCases(HttpImplementationRepository httpStorageAccountRepository, HttpImplementationRepository httpCdnRepository)
        {
            this.httpStorageAccountRepository = httpStorageAccountRepository;
            this.httpCdnRepository = httpCdnRepository;
        }
        public async Task GetResponse()
        {
            Console.WriteLine("Initializing test times...");
            Console.WriteLine("----------------------------------");
            string originUrl = AppConfiguration.originUrl;
            string cdnUrl = AppConfiguration.cdnUrl;

            Console.WriteLine("Accessing the file directly from the origin (Storage Account)...");
            var stopwatchOrigin = Stopwatch.StartNew();
            var originResponse = await httpStorageAccountRepository.Get(originUrl);
            stopwatchOrigin.Stop();
            if (originResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Direct access to origin time: {stopwatchOrigin.ElapsedMilliseconds} ms");
            }
            else
            {
                Console.WriteLine($"Error accessing the origin: {originResponse.StatusCode}");
            }
            Console.WriteLine("----------------------------------");

            Console.WriteLine("Accessing the file through the CDN...");
            var stopwatchCdn = Stopwatch.StartNew();
            var cdnResponse = await httpCdnRepository.Get(cdnUrl);
            stopwatchCdn.Stop();
            if (cdnResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Access through CDN time: {stopwatchCdn.ElapsedMilliseconds} ms");
            }
            else
            {
                Console.WriteLine($"Error accessing the CDN: {cdnResponse.StatusCode}");
            }

            Console.WriteLine("----------------------------------");
            Console.WriteLine("Test completed.");
        }
    }
}
