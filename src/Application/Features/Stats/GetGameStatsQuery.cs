using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Features.Stats.Models;
using Football.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Features.Stats;

public record GetGameStatsQuery : IRequest<GameStatDto>
{
    public int GameId { get; init; }
}

public class GetGameStatsQueryHandler : IRequestHandler<GetGameStatsQuery, GameStatDto?>
{
    private readonly IFootballDbContext _footballDbContext;

    private readonly IMapper _mapper;
    public GetGameStatsQueryHandler(IFootballDbContext footballDbContext, IMapper mapper)
    {
        _footballDbContext = footballDbContext;
        _mapper = mapper;
    }

    public async Task<GameStatDto?> Handle(GetGameStatsQuery request, CancellationToken cancellationToken)
    {
        List<StatDto> gameStats = await _footballDbContext.Stats.Where(s => s.GameId == request.GameId)
            .ProjectTo<StatDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new GameStatDto()
        {
            GameId = request.GameId,
            Stats = gameStats
        };
    }
}
