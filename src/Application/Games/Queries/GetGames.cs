using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Common.Interfaces;
using Football.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Games.Queries;

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
                .Include(game => game.Time)
                .Include(game => game.Stats)
                .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}