using System.Net;
using FluentAssertions;
using Football.Application.Features.Stats.Models;

namespace Football.Api.IntegrationTests;

[Collection(ApiIntegrationTestCollection.Name)]
public class GetStatsByIdTests
{
    private readonly HttpClient _httpClient;

    public GetStatsByIdTests(ApiIntegrationTestFixture fixture)
    {
        _httpClient = fixture.HttpClient;
    }

    [Fact]
    public async Task GetStatsById_ExistingId_Returns200WithStats()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games/2019090500/stats");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        GameStatDto? stats = await response.Content.ReadFromJsonAsync<GameStatDto>();
        stats.Should().NotBeNull();
        stats!.GameId.Should().Be(2019090500);
        stats.Stats.Should().HaveCount(2);
        stats.Stats.Should().Contain(s => s.Team == "CHI");
        stats.Stats.Should().Contain(s => s.Team == "GB");
    }

    [Fact]
    public async Task GetStatsById_NonExistentId_Returns404()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games/1/stats");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetStatsById_IdZero_Returns400()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games/0/stats");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetStatsById_PositiveId_DoesNotReturn400()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games/1/stats");

        response.StatusCode.Should().NotBe(HttpStatusCode.BadRequest);
    }
}
