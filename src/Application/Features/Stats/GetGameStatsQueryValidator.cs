using FluentValidation;

namespace Football.Application.Features.Stats;

public class GetGameStatsQueryValidator : AbstractValidator<GetGameStatsQuery>
{
    public GetGameStatsQueryValidator()
    {
        RuleFor(g => g.GameId)
            .Must(gameId => gameId > 0)
            .WithMessage("GameId must never be zero or negative");
    }
}
