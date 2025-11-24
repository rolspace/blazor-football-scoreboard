using Football.Application.Interfaces;
using Football.Application.Options;
using Football.Infrastructure.Options;
using Football.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FootballDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("FootballDbConnection")));

        services.AddScoped<IFootballDbContext>(provider => provider.GetRequiredService<FootballDbContext>());

        services.Configure<HubOptions>(configuration.GetSection(HubOptions.Key));
        services.Configure<ScoreboardOptions>(configuration.GetSection(ScoreboardOptions.Key));

        return services;
    }
}
