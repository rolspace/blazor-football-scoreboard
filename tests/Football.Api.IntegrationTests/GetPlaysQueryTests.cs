using FluentAssertions;
using Football.Application.Features.Plays;
using Football.Application.Features.Plays.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Football.Api.IntegrationTests;

[Collection(ApiIntegrationTestCollection.Name)]
public class GetPlaysQueryTests
{
    private readonly IServiceProvider _services;

    public GetPlaysQueryTests(ApiIntegrationTestFixture fixture)
    {
        _services = fixture.Services;
    }

    [Fact]
    public async Task GetPlays_MatchingParams_ReturnsPlayCollection()
    {
        using IServiceScope scope = _services.CreateScope();
        ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        IEnumerable<PlayDto> result = await mediator.Send(new GetPlaysQuery
        {
            Week = 1,
            Quarter = 1,
            QuarterSecondsRemaining = 900
        });

        result.Should().HaveCount(2);
        result.Should().Contain(p => p.Id == 35);
        result.Should().Contain(p => p.Id == 50);
    }

    [Fact]
    public async Task GetPlays_NoMatchingParams_ReturnsEmpty()
    {
        using IServiceScope scope = _services.CreateScope();
        ISender mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        IEnumerable<PlayDto> result = await mediator.Send(new GetPlaysQuery
        {
            Week = 1,
            Quarter = 1,
            QuarterSecondsRemaining = 1000
        });

        result.Should().BeEmpty();
    }
}
