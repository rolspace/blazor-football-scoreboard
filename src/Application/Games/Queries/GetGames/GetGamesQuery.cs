
using Football.Application.Common.Interfaces;
using Football.Application.Common.Models;
using MediatR;

namespace Football.Application.Games.Queries.GetGames;

public record GetGamesQuery : IRequest<IEnumerable<GameDto>>;

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<GameDto>>
{
    private IFootballDbContext _footballDbContext;

    public GetGamesQueryHandler(IFootballDbContext footballDbContext)
    {
        _footballDbContext = footballDbContext;
    }

    public Task<IEnumerable<GameDto>> Handle(GetGamesQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
