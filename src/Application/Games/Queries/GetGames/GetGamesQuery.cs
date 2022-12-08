using MediatR;

namespace Football.Application.Games.Queries.GetGames;

public record GetGamesQuery : IRequest<GamesVm>;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, GamesVm>
{
    public Task<GamesVm> Handle(GetGamesQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
