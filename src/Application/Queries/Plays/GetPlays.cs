using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Common.Models;
using Football.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Queries.Plays;

public record GetPlaysQuery : IRequest<IEnumerable<PlayLog>>
{
    public int Week { get; set; }

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }
}

public class GetPlaysQueryHandler : IRequestHandler<GetPlaysQuery, IEnumerable<PlayLog>>
{
    private readonly IFootballDbContext _footballDbContext;

    private readonly IMapper _mapper;

    public GetPlaysQueryHandler(IFootballDbContext footballDbContext, IMapper mapper)
    {
        _footballDbContext = footballDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PlayLog>> Handle(GetPlaysQuery request, CancellationToken cancellationToken)
    {
        return await _footballDbContext.Plays
            .Where(p => p.Week == request.Week && p.Qtr == request.Quarter && p.QuarterSecondsRemaining == request.QuarterSecondsRemaining)
            .ProjectTo<PlayLog>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
