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

    [Fact]
    public void Validate_GameIdIsZero_Throw()
    {
        GetGameQuery getGameQuery = new() { Id = 0 };

        TestValidationResult<GetGameQuery> validatorResult = _getGameQueryValidator.TestValidate(getGameQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Id);
    }

    [Fact]
    public void Validate_GameIdIsPositive_ValidationSuccess()
    {
        GetGameQuery getGameQuery = new() { Id = 1 };

        TestValidationResult<GetGameQuery> validatorResult = _getGameQueryValidator.TestValidate(getGameQuery);

        Assert.True(validatorResult.IsValid);
    }
}
