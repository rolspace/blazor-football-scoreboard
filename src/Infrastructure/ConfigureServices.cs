using Football.Application.Interfaces;
using Football.Application.Options;
using Football.Infrastructure.Hub;
using Football.Infrastructure.Options;
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

        services.Configure<ScoreboardOptions>(configuration.GetSection(ScoreboardOptions.Key));
        services.Configure<HubOptions>(configuration.GetSection(HubOptions.Key));
        services.AddTransient<IHubProvider, HubProvider>();

        return services;
    }
}
