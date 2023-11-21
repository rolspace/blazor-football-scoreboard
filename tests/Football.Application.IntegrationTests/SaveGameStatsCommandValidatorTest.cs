using FluentValidation.TestHelper;
using Football.Application.Features.Stats;

namespace Football.Application.IntegrationTests;

public class SaveGameStatsCommandValidatorTest
{
    private SaveGameStatsCommandValidator _saveGameStatsCommandValidator;

    public SaveGameStatsCommandValidatorTest()
    {
        _saveGameStatsCommandValidator = new SaveGameStatsCommandValidator();
    }

    [Fact]
    public void Validate_GameIdIsZero_Throw()
    {
        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 0,
            Quarter = 1,
            QuarterSecondsRemaining = 1000
        };

        TestValidationResult<SaveGameStatsCommand> validatorResult = _saveGameStatsCommandValidator.TestValidate(saveGameStatsCommand);

        validatorResult.ShouldHaveValidationErrorFor(query => query.GameId);
    }

    [Fact]
    public void Validate_GameIdIsPositive_ValidationSuccess()
    {
        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 1,
            Quarter = 1,
            QuarterSecondsRemaining = 1000
        };

        TestValidationResult<SaveGameStatsCommand> validatorResult = _saveGameStatsCommandValidator.TestValidate(saveGameStatsCommand);

        Assert.True(validatorResult.IsValid);
    }

    [Fact]
    public void Validate_QuarterIsZero_Throw()
    {
        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 1,
            Quarter = 0,
            QuarterSecondsRemaining = 1000
        };

        TestValidationResult<SaveGameStatsCommand> validatorResult = _saveGameStatsCommandValidator.TestValidate(saveGameStatsCommand);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Quarter);
    }

    [Fact]
    public void Validate_QuarterIsTwenty_Throw()
    {
        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 1,
            Quarter = 20,
            QuarterSecondsRemaining = 1000
        };

        TestValidationResult<SaveGameStatsCommand> validatorResult = _saveGameStatsCommandValidator.TestValidate(saveGameStatsCommand);

        validatorResult.ShouldHaveValidationErrorFor(query => query.Quarter);
    }

    [Fact]
    public void Validate_QuarterIsTwo_ValidationSuccess()
    {
        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 1,
            Quarter = 2,
            QuarterSecondsRemaining = 1000
        };

        TestValidationResult<SaveGameStatsCommand> validatorResult = _saveGameStatsCommandValidator.TestValidate(saveGameStatsCommand);

        Assert.True(validatorResult.IsValid);
    }

    [Fact]
    public void Validate_QuarterSecondsRemainingIsNegative_Throw()
    {
        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 1,
            Quarter = 1,
            QuarterSecondsRemaining = -1
        };

        TestValidationResult<SaveGameStatsCommand> validatorResult = _saveGameStatsCommandValidator.TestValidate(saveGameStatsCommand);

        validatorResult.ShouldHaveValidationErrorFor(query => query.QuarterSecondsRemaining);
    }

    [Fact]
    public void Validate_QuarterSecondsRemainingIs3700_Throw()
    {
        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 1,
            Quarter = 1,
            QuarterSecondsRemaining = 3700
        };

        TestValidationResult<SaveGameStatsCommand> validatorResult = _saveGameStatsCommandValidator.TestValidate(saveGameStatsCommand);

        validatorResult.ShouldHaveValidationErrorFor(query => query.QuarterSecondsRemaining);
    }

    [Fact]
    public void Validate_QuarterSecondsRemainingIs1000_ValidationSuccess()
    {
        SaveGameStatsCommand saveGameStatsCommand = new()
        {
            GameId = 1,
            Quarter = 1,
            QuarterSecondsRemaining = 1000
        };

        TestValidationResult<SaveGameStatsCommand> validatorResult = _saveGameStatsCommandValidator.TestValidate(saveGameStatsCommand);

        Assert.True(validatorResult.IsValid);
    }
}
