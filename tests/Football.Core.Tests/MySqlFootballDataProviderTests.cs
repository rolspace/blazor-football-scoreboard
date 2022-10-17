using FluentAssertions;
using Football.Core.Models;
using Football.Core.Persistence.MySql;

namespace Football.Core.Tests;

public class MySqlFootballDataProviderTests : IClassFixture<TestDatabaseFixture>
{
    public TestDatabaseFixture Fixture { get; }

    public MySqlFootballDataProviderTests(TestDatabaseFixture testDatabaseFixture)
        => Fixture = testDatabaseFixture;

    [Fact]
    public async Task GetGame_IdDoesNotExist_ReturnsNull()
    {
        using var dbContext = Fixture.CreateContext();

        MySqlFootballDataProvider mySqlFootballDataProvider = new MySqlFootballDataProvider(dbContext);

        Game game = await mySqlFootballDataProvider.GetGame(0);

        game.Should().BeNull();
    }
}
