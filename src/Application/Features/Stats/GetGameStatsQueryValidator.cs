using FluentValidation;

namespace Football.Application.Features.Stats;

public class GetGameStatsQueryValidator : AbstractValidator<GetGameStatsQuery>
{
    public GetGameStatsQueryValidator()
    {
        RuleFor(g => g.Id)
            .Must(id => id > 0)
            .WithMessage("Id must never be zero or negative");
    }
}
