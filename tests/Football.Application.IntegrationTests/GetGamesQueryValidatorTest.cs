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

        TestValidationResult<GetGamesQuery> validatorResult = _getGamesQueryValidator.TestValidate(getGamesQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Week);
    }

    [Fact]
    public void Validate_WeekIsTwenty_Throw()
    {
        GetGamesQuery getGamesQuery = new() { Week = 20 };

        TestValidationResult<GetGamesQuery> validatorResult = _getGamesQueryValidator.TestValidate(getGamesQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Week);
    }

    [Fact]
    public void Validate_WeekIsTen_ValidationSuccess()
    {
        GetGamesQuery getGamesQuery = new() { Week = 10 };

        TestValidationResult<GetGamesQuery> validatorResult = _getGamesQueryValidator.TestValidate(getGamesQuery);

        Assert.True(validatorResult.IsValid);
    }
}
