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
            .Must(quarter => quarter >= 1 && quarter <= 5)
            .WithMessage("Quarter must be between 1 and 5");

        RuleFor(g => g.QuarterSecondsRemaining)
            .Must(q => q >= 0 && q <= 3600)
            .WithMessage("QuarterSecondsRemaining must be between 0 and 3600");
    }
}
