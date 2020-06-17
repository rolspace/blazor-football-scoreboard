using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Games.Entities;
using Games.Services.Interfaces;
using Games.Services.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Games.Worker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddDbContext<GamesContext>(options =>
                        options.UseMySQL(hostContext.Configuration.GetConnectionString("GamesContext")),
                        ServiceLifetime.Singleton);
                    services.AddSingleton<IAsyncRepository<Play>, Repository<Play>>();
                });
    }
}
