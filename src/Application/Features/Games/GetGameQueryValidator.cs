using FluentValidation;

namespace Football.Application.Features.Games;

public class GetGameQueryValidator : AbstractValidator<GetGameQuery>
{
    public GetGameQueryValidator()
    {
        RuleFor(g => g.GameId)
            .Must(gameId => gameId > 0)
            .WithMessage("GameId must never be zero or negative");
    }
}
