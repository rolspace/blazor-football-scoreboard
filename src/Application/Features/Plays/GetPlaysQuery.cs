using AutoMapper;
using AutoMapper.QueryableExtensions;
using Football.Application.Features.Plays.Models;
using Football.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Features.Plays;

public record GetPlaysQuery : IRequest<IEnumerable<PlayDto>>
{
    public int Week { get; set; }

    public int Quarter { get; set; }

    public int QuarterSecondsRemaining { get; set; }
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
            .Where(p => p.Week == request.Week
                && p.Quarter == request.Quarter
                && p.QuarterSecondsRemaining.HasValue
                && p.QuarterSecondsRemaining == request.QuarterSecondsRemaining)
            .ProjectTo<PlayDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}
