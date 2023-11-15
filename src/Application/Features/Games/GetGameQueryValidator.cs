using FluentValidation;

namespace Football.Application.Features.Games;

public class GetGameQueryValidator : AbstractValidator<GetGameQuery>
{
    public GetGameQueryValidator()
    {
        RuleFor(g => g.Id)
            .Must(id => id <= 0)
            .WithMessage("Id must never be zero or negative");
    }
}
