using Football.Core.Persistence.Interfaces.DataProviders;
using Football.Core.Persistence.MySql;
using Football.Core.Persistence.MySql.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Football.Workers.GameWorker
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

                    var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 28));
                    services.AddDbContext<FootballDbContext>(options =>
                        options.UseMySql(hostContext.Configuration.GetConnectionString("FootballDbContext"), mySqlServerVersion));

                    services.AddScoped<IFootballDataProvider, MySqlFootballDataProvider>();
                });
    }
}
