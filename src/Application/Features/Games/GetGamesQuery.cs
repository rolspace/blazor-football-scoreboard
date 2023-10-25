using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Features.Games.Models;
using Football.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Features.Games;

public record GetGamesQuery : IRequest<IEnumerable<GameDto>>
{
    public int Week { get; init; }
}

public class GetGamesQueryHandler : IRequestHandler<GetGamesQuery, IEnumerable<GameDto>>
{
    private readonly IFootballDbContext _footballDbContext;

    private readonly IMapper _mapper;

    public GetGamesQueryHandler(IFootballDbContext footballDbContext, IMapper mapper)
    {
        _footballDbContext = footballDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GameDto>> Handle(GetGamesQuery request, CancellationToken cancellationToken)
    {
        return await _footballDbContext.Games
                .Where(game => game.Week == request.Week)
                .Include(game => game.Stats)
                .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken: cancellationToken);
    }
}
