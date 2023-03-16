using Football.Application.Interfaces;
using Football.Infrastructure.Communication;
using Football.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var mySqlServerVersion = new MySqlServerVersion(new Version(8, 0, 28));

        services.AddDbContext<FootballDbContext>(options =>
            options.UseMySql(configuration.GetConnectionString("FootballDbConnection"), mySqlServerVersion));

        services.AddScoped<IFootballDbContext>(provider => provider.GetRequiredService<FootballDbContext>());

        services.AddTransient<IHubManager>((serviceProvider) =>
        {
            var hubSettings = configuration.GetSection(HubSettings.Hub).Get<HubSettings>();

            var hubUri = new Uri(hubSettings != null ? hubSettings.Endpoint : string.Empty);
            return new HubManager(hubUri);
        });

        return services;
    }
}
