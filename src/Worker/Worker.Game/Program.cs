using Core.Entities;
using Core.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Worker.Game
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
                    services.AddDbContext<FootballDbContext>(options =>
                        options.UseMySQL(hostContext.Configuration.GetConnectionString("FootballDbContext")),
                        ServiceLifetime.Scoped);
                    services.AddScoped<IRepository<Play>, Repository<Play>>();
                });
    }
}
