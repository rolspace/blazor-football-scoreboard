using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Features.Games.Models;
using Football.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Features.Games;

public record GetGameQuery : IRequest<GameDto>
{
    public int Id { get; init; }
}

public class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameDto?>
{
    private readonly IFootballDbContext _footballDbContext;

    private readonly IMapper _mapper;

    public GetGameQueryHandler(IFootballDbContext footballDbContext, IMapper mapper)
    {
        _footballDbContext = footballDbContext;
        _mapper = mapper;
    }

    public async Task<GameDto?> Handle(GetGameQuery request, CancellationToken cancellationToken)
    {
        return await _footballDbContext.Games
                .Where(game => game.Id == request.Id)
                .Include(game => game.Stats)
                .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken: cancellationToken);
    }
}
