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

    [Fact]
    public async Task Get_Game_IdExists_ReturnsGame()
    {
        using var dbContext = Fixture.CreateContext();

        MySqlFootballDataProvider mySqlFootballDataProvider = new MySqlFootballDataProvider(dbContext);

        Game game = await mySqlFootballDataProvider.GetGame(2019090500);
        Game expected = new()
        {
            Id = 2019090500,
            Week = 1,
            HomeTeam = "CHI",
            AwayTeam = "GB",
            Time = null,
            Stats = Enumerable.Empty<Stat>().ToList(),
        };

        game.Should().BeEquivalentTo(expected);
    }
}
