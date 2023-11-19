using FluentValidation;

namespace Football.Application.Features.Plays;

public class GetPlaysQueryValidator : AbstractValidator<GetPlaysQuery>
{
    public GetPlaysQueryValidator()
    {
        RuleFor(g => g.Week)
            .Must(week => week >= 1 && week <= 17)
            .WithMessage("Week must be between 1 and 17");

        RuleFor(g => g.Quarter)
            .Must(week => week >= 1 && week <= 5)
            .WithMessage("Quarter must be between 1 and 5");

        RuleFor(g => g.QuarterSecondsRemaining)
            .Must(week => week >= 0 && week <= 3600)
            .WithMessage("Quarter must be between 0 and 3600");
    }
}
