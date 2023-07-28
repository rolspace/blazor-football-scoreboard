using Football.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class TestDatabaseFixture
{
    private const string ConnectionString = @"Server=localhost;Port=3307;Database=football_testdb;Uid=root;Pwd=password;";

    public FootballDbContext CreateContext()
        => new FootballDbContext(
            new DbContextOptionsBuilder<FootballDbContext>()
                .UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 28)))
                .Options);
}
