using FluentValidation.TestHelper;
using Football.Application.Features.Stats;

namespace Football.Application.IntegrationTests;

public class GetGameStatsQueryValidatorTest
{
    private GetGameStatsQueryValidator _getGameStatsQueryValidator;

    public GetGameStatsQueryValidatorTest()
    {
        _getGameStatsQueryValidator = new GetGameStatsQueryValidator();
    }

    [Fact]
    public void Validate_GameIdIsZero_Throw()
    {
        GetGameStatsQuery getGameStatsQuery = new() { Id = 0 };

        TestValidationResult<GetGameStatsQuery> validatorResult = _getGameStatsQueryValidator.TestValidate(getGameStatsQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Id);
    }

    [Fact]
    public void Validate_GameIdIsPositive_ValidationSuccess()
    {
        GetGameStatsQuery getGameStatsQuery = new() { Id = 1 };

        TestValidationResult<GetGameStatsQuery> validatorResult = _getGameStatsQueryValidator.TestValidate(getGameStatsQuery);

        Assert.True(validatorResult.IsValid);
    }
}
