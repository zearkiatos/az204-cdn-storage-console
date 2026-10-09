using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CDNStorageConsole.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        string originUrl = AppConfiguration.originUrl;
        
        string cdnUrl = AppConfiguration.cdnUrl;

        Console.WriteLine("Initializing test times...");
        Console.WriteLine("----------------------------------");

        using (HttpClient httpClient = new HttpClient())
        {
            Console.WriteLine("Accessing the file directly from the origin (Storage Account)...");
            var stopwatchOrigin = Stopwatch.StartNew();
            var originResponse = await httpClient.GetAsync(originUrl);
            stopwatchOrigin.Stop();

            if (originResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Direct access to origin time: {stopwatchOrigin.ElapsedMilliseconds} ms");
            }
            else
            {
                Console.WriteLine($"Error accessing the origin: {originResponse.StatusCode}");
            }
        }

        Console.WriteLine("----------------------------------");

        using (HttpClient httpClient = new HttpClient())
        {
            Console.WriteLine("Accessing the file through the CDN (Azure Front Door)...");
            var stopwatchCdn = Stopwatch.StartNew();
            var cdnResponse = await httpClient.GetAsync(cdnUrl);
            stopwatchCdn.Stop();

            if (cdnResponse.IsSuccessStatusCode)
            {
                Console.WriteLine($"Access through CDN time: {stopwatchCdn.ElapsedMilliseconds} ms");
            }
            else
            {
                Console.WriteLine($"Error accessing the CDN: {cdnResponse.StatusCode}");
            }
        }

        Console.WriteLine("----------------------------------");
        Console.WriteLine("Test completed.");
    }
}
