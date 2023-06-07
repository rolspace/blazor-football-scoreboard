using Football.Application.Models;
using MediatR;

namespace Football.Application.Features.Stats;

public record GetGameStatsQuery : IRequest<GameStatDto>
{
    public int GameId { get; init; }
}

public class GetGameStatsQueryHandler : IRequestHandler<GetGameStatsQuery, GameStatDto>
{
    public GetGameStatsQueryHandler()
    {
    }

    public async Task<GameStatDto> Handle(GetGameStatsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
