using System.Net;
using FluentAssertions;
using Football.Application.Features.Games.Models;

namespace Football.Api.IntegrationTests;

[Collection(ApiIntegrationTestCollection.Name)]
public class GetGameByIdTests
{
    private readonly HttpClient _httpClient;

    public GetGameByIdTests(ApiIntegrationTestFixture fixture)
    {
        _httpClient = fixture.HttpClient;
    }

    [Fact]
    public async Task GetGameById_ExistingId_Returns200WithGame()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games/2019090500");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        GameDto? game = await response.Content.ReadFromJsonAsync<GameDto>();
        game.Should().NotBeNull();
        game!.Id.Should().Be(2019090500);
        game.HomeTeam.Should().Be("CHI");
        game.AwayTeam.Should().Be("GB");
    }

    [Fact]
    public async Task GetGameById_NonExistentId_Returns404()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games/999999999");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GetGameById_IdZero_Returns400()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games/0");

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GetGameById_PositiveId_DoesNotReturn400()
    {
        HttpResponseMessage response = await _httpClient.GetAsync("/api/v1/games/1");

        response.StatusCode.Should().NotBe(HttpStatusCode.BadRequest);
    }
}
