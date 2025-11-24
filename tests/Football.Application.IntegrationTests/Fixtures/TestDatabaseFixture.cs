using System.Reflection;
using Football.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Football.Application.IntegrationTests.Fixtures;

public class TestDatabaseFixture
{
    private readonly string? _connectionString = string.Empty;

    public TestDatabaseFixture()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddUserSecrets(Assembly.GetExecutingAssembly(), true)
            .AddEnvironmentVariables().Build();

        _connectionString = configuration.GetConnectionString("FootballDbConnection");
    }

    public FootballDbContext CreateContext()
        => new(
            new DbContextOptionsBuilder<FootballDbContext>()
                .UseNpgsql(_connectionString)
                .Options);
}
