using Football.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Football.Application.Queries.Plays;

public record CheckOvertimeQuery : IRequest<bool>
{
    public int Week { get; set; }

    public int Quarter { get; } = 5;

    public int QuarterSecondsRemaining { get; } = 600;
}

public class CheckOvertimeQueryHandler : IRequestHandler<CheckOvertimeQuery, bool>
{
    private readonly IFootballDbContext _footballDbContext;

    public CheckOvertimeQueryHandler(IFootballDbContext footballDbContext)
    {
        _footballDbContext = footballDbContext;
    }

    public async Task<bool> Handle(CheckOvertimeQuery request, CancellationToken cancellationToken)
    {
        return await _footballDbContext.Plays
            .AnyAsync(p => p.Week == request.Week && p.Qtr == request.Quarter && p.QuarterSecondsRemaining == request.QuarterSecondsRemaining);
    }
}
