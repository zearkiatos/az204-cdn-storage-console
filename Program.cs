using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using CDNStorageConsole.Configuration;
using CDNStorageConsole.Response.Application;
using CDNStorageConsole.Response.Infrastructure.Repository;

class Program
{
    static async Task Main(string[] args)
    {
        var getResponseUseCases = new GetResponseUseCases(
            new HttpImplementationRepository(),
            new HttpImplementationRepository()
        );
        await getResponseUseCases.GetResponse();
    }
}
