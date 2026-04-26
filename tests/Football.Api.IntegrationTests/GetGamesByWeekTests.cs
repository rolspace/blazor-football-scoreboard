using System.Net;
using FluentAssertions;
using Football.Application.Features.Games.Models;

namespace Football.Api.IntegrationTests;

[Collection(ApiIntegrationTestCollection.Name)]
public class GetGamesByWeekTests
{
    private readonly HttpClient _httpClient;

    public GetGamesByWeekTests(ApiIntegrationTestFixture fixture)
    {
        _httpClient = fixture.HttpClient;
    }

    [Fact]
    public async Task GetGamesByWeek_ValidWeekWithData_Returns200WithGames()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games?week=1");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        IEnumerable<GameDto>? games = await response.Content.ReadFromJsonAsync<IEnumerable<GameDto>>();
        games.Should().ContainSingle(g => g.Id == 2019090500);
    }

    [Fact]
    public async Task GetGamesByWeek_WeekZero_Returns400()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games?week=0");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetGamesByWeek_WeekTwenty_Returns400()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games?week=20");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetGamesByWeek_WeekTen_Returns200()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games?week=10");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
