using FluentValidation;
using FluentValidation.TestHelper;
using Football.Application.Features.Games;

namespace Football.Application.IntegrationTests;

public class GetGamesQueryValidatorTest
{
    private GetGamesQueryValidator _getGamesQueryValidator;

    public GetGamesQueryValidatorTest()
    {
        _getGamesQueryValidator = new GetGamesQueryValidator();
    }

    [Fact]
    public void Validate_WeekIsZero_Throw()
    {
        GetGamesQuery getGamesQuery = new() { Week = 0 };

        var validatorResult = _getGamesQueryValidator.TestValidate(getGamesQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Week);
    }
}
