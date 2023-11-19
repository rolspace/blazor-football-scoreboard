using FluentValidation.TestHelper;
using Football.Application.Features.Plays;

namespace Football.Application.IntegrationTests;

public class GetPlaysQueryValidatorTest
{
    private GetPlaysQueryValidator _getPlaysQueryValidator;

    public GetPlaysQueryValidatorTest()
    {
        _getPlaysQueryValidator = new GetPlaysQueryValidator();
    }

    [Fact]
    public void Validate_WeekIsZero_Throw()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 0, Quarter = 1, QuarterSecondsRemaining = 1000 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Week);
    }

    [Fact]
    public void Validate_WeekIsTwenty_Throw()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 20, Quarter = 1, QuarterSecondsRemaining = 1000 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Week);
    }

    [Fact]
    public void Validate_WeekIsTen_ValidationSuccess()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 10, Quarter = 1, QuarterSecondsRemaining = 1000 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        Assert.True(validatorResult.IsValid);
    }

    [Fact]
    public void Validate_QuarterIsZero_Throw()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 1, Quarter = 0, QuarterSecondsRemaining = 1000 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Quarter);
    }

    [Fact]
    public void Validate_QuarterIsTwenty_Throw()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 1, Quarter = 20, QuarterSecondsRemaining = 1000 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Quarter);
    }

    [Fact]
    public void Validate_QuarterIsTwo_ValidationSuccess()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 1, Quarter = 2, QuarterSecondsRemaining = 1000 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        Assert.True(validatorResult.IsValid);
    }

    [Fact]
    public void Validate_QuarterSecondsRemainingIsNegative_Throw()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 1, Quarter = 1, QuarterSecondsRemaining = -1 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.QuarterSecondsRemaining);
    }

    [Fact]
    public void Validate_QuarterSecondsRemainingIs3700_Throw()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 1, Quarter = 1, QuarterSecondsRemaining = 3700 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        validatorResult.ShouldHaveValidationErrorFor(query => query.QuarterSecondsRemaining);
    }

    [Fact]
    public void Validate_QuarterSecondsRemainingIs1000_ValidationSuccess()
    {
        GetPlaysQuery getPlaysQuery = new() { Week = 1, Quarter = 1, QuarterSecondsRemaining = 1000 };

        TestValidationResult<GetPlaysQuery> validatorResult = _getPlaysQueryValidator.TestValidate(getPlaysQuery);

        Assert.True(validatorResult.IsValid);
    }
}
