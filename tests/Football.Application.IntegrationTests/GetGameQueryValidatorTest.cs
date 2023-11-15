using FluentValidation;
using FluentValidation.TestHelper;
using Football.Application.Features.Games;

namespace Football.Application.IntegrationTests;

public class GetGameQueryValidatorTest
{
    private GetGameQueryValidator _getGameQueryValidator;

    public GetGameQueryValidatorTest()
    {
        _getGameQueryValidator = new GetGameQueryValidator();
    }

    public void Validate_GameIdIsZero_Throw()
    {
        GetGameQuery getGameQuery = new() { Id = 0 };

        var validatorResult = _getGameQueryValidator.TestValidate(getGameQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Id);
    }
}
