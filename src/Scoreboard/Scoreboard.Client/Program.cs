using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scoreboard.Client.Config;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scoreboard.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var appSettings = new AppSettings();

            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Configuration.Bind(appSettings);
            builder.Services.AddSingleton(appSettings);

            builder.Services.AddTransient(sp =>
                new HttpClient {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                });

            await builder.Build().RunAsync();
        }
    }
}
