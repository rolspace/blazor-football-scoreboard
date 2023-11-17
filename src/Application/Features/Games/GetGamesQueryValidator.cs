using FluentValidation;

namespace Football.Application.Features.Games;

public class GetGamesQueryValidator : AbstractValidator<GetGamesQuery>
{
    public GetGamesQueryValidator()
    {
        RuleFor(g => g.Week)
            .Must(week => week > 0 && week < 18)
            .WithMessage("Week must be between 1 and 17");
    }
}
