using FluentValidation;

namespace Football.Application.Features.Stats;

public class SaveGameStatsCommandValidator : AbstractValidator<SaveGameStatsCommand>
{
    public SaveGameStatsCommandValidator()
    {
        RuleFor(s => s.GameId)
            .Must(gameId => gameId > 0)
            .WithMessage("GameId must never be zero or negative");

        RuleFor(s => s.Quarter)
            .Must(quarter => quarter >= 1 && quarter <= 5)
            .WithMessage("Quarter must be between 1 and 5");

        RuleFor(s => s.QuarterSecondsRemaining)
            .Must(q => q >= 0 && q <= 3600)
            .WithMessage("QuarterSecondsRemaining must be between 0 and 3600");

        RuleFor(s => s.SaveGameStatCommandItems)
            .NotNull()
            .WithMessage("SaveGameStatCommandItems must not be null");
    }
}
