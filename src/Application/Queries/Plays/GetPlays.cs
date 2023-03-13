using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Common.Interfaces;
using Football.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Queries.Plays;

public record GetPlaysQuery : IRequest<IEnumerable<PlayDto>>
{
    public int Week { get; set; }

    public int SecondsRemainingStart { get; set; }

    public int SecondsRemainingEnd { get; set; }
}

public class GetPlaysQueryHandler : IRequestHandler<GetPlaysQuery, IEnumerable<PlayDto>>
{
    private readonly IFootballDbContext _footballDbContext;

    private readonly IMapper _mapper;

    public GetPlaysQueryHandler(IFootballDbContext footballDbContext, IMapper mapper)
    {
        _footballDbContext = footballDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlayDto>> Handle(GetPlaysQuery request, CancellationToken cancellationToken)
    {
        return await _footballDbContext.Plays
            .Where(p => p.Week == request.Week && p.GameSecondsRemaining > request.SecondsRemainingStart && p.GameSecondsRemaining <= request.SecondsRemainingEnd)
            .ProjectTo<PlayDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
