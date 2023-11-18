using FluentValidation;

namespace Football.Application.Features.Games;

public class GetGamesQueryValidator : AbstractValidator<GetGamesQuery>
{
    public GetGamesQueryValidator()
    {
        RuleFor(g => g.Week)
            .Must(week => week >= 1 && week <= 17)
            .WithMessage("Week must be between 1 and 17");
    }
}
