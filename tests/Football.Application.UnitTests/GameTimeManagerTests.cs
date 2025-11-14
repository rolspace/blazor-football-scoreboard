using Football.Application.Services;

namespace Football.Application.UnitTests;

public class GameTimeManagerTests
{
    [Fact]
    public void DefaultConstructor_InitializesWithFirstQuarterAndFullTime()
    {
        // Act
        var manager = new GameTimeManager();

        // Assert
        Assert.Equal(1, manager.GetQuarter());
        Assert.Equal(900, manager.GetQuarterSecondsRemaining());
    }

    [Theory]
    [InlineData(1, 900)]
    [InlineData(2, 450)]
    [InlineData(3, 300)]
    [InlineData(4, 0)]
    public void ParameterizedConstructor_InitializesWithProvidedValues(int quarter, int seconds)
    {
        // Act
        var manager = new GameTimeManager(quarter, seconds);

        // Assert
        Assert.Equal(quarter, manager.GetQuarter());
        Assert.Equal(seconds, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void GamesScheduled_DefaultsToZero()
    {
        // Act
        var manager = new GameTimeManager();

        // Assert
        Assert.Equal(0, manager.GamesScheduled);
    }

    [Fact]
    public void GamesScheduled_CanBeSet()
    {
        // Arrange
        var manager = new GameTimeManager();

        // Act
        manager.GamesScheduled = 16;

        // Assert
        Assert.Equal(16, manager.GamesScheduled);
    }

    [Fact]
    public void GameTimeOver_ReturnsTrueWhenGamesFinishedEqualsGamesScheduled()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 5;

        // Act
        manager.IncrementGamesFinished(5);

        // Assert
        Assert.True(manager.GamesOver);
    }

    [Fact]
    public void GameTimeOver_ReturnsFalseWhenGamesFinishedLessThanGamesScheduled()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 10;

        // Act
        manager.IncrementGamesFinished(5);

        // Assert
        Assert.False(manager.GamesOver);
    }

    [Fact]
    public void AdvanceTime_DecrementsSecondsRemaining()
    {
        // Arrange
        var manager = new GameTimeManager(1, 900);

        // Act
        manager.AdvanceTime();

        // Assert
        Assert.Equal(899, manager.GetQuarterSecondsRemaining());
        Assert.Equal(1, manager.GetQuarter());
    }

    [Fact]
    public void AdvanceTime_MultipleDecrements_ReducesSecondsCorrectly()
    {
        // Arrange
        var manager = new GameTimeManager(1, 10);

        // Act
        manager.AdvanceTime();
        manager.AdvanceTime();
        manager.AdvanceTime();

        // Assert
        Assert.Equal(7, manager.GetQuarterSecondsRemaining());
        Assert.Equal(1, manager.GetQuarter());
    }

    [Fact]
    public void AdvanceTime_WhenQuarterEndsBeforeFourthQuarter_AdvancesToNextQuarter()
    {
        // Arrange
        var manager = new GameTimeManager(2, 1);

        // Act
        manager.AdvanceTime(); // 2nd quarter, 0 seconds
        manager.AdvanceTime(); // Should advance to 3rd quarter

        // Assert
        Assert.Equal(3, manager.GetQuarter());
        Assert.Equal(900, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void AdvanceTime_WhenFirstQuarterEnds_AdvancesToSecondQuarter()
    {
        // Arrange
        var manager = new GameTimeManager(1, 0);

        // Act
        manager.AdvanceTime();

        // Assert
        Assert.Equal(2, manager.GetQuarter());
        Assert.Equal(900, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void AdvanceTime_WhenThirdQuarterEnds_AdvancesToFourthQuarter()
    {
        // Arrange
        var manager = new GameTimeManager(3, 0);

        // Act
        manager.AdvanceTime();

        // Assert
        Assert.Equal(4, manager.GetQuarter());
        Assert.Equal(900, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void AdvanceTime_WhenFourthQuarterEnds_AdvancesToOvertime()
    {
        // Arrange
        var manager = new GameTimeManager(4, 0);

        // Act
        manager.AdvanceTime();

        // Assert
        Assert.Equal(5, manager.GetQuarter());
        Assert.Equal(600, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void AdvanceTime_WhenAllGamesFinished_SetsQuarterAndSecondsToNegativeOne()
    {
        // Arrange
        var manager = new GameTimeManager(3, 500);
        manager.GamesScheduled = 16;
        manager.IncrementGamesFinished(16);

        // Act
        manager.AdvanceTime();

        // Assert
        Assert.Equal(-1, manager.GetQuarter());
        Assert.Equal(-1, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void AdvanceTime_WhenAllGamesFinished_DoesNotChangeTimeOnSubsequentCalls()
    {
        // Arrange
        var manager = new GameTimeManager(2, 300);
        manager.GamesScheduled = 16;
        manager.IncrementGamesFinished(16);

        // Act
        manager.AdvanceTime();
        manager.AdvanceTime();
        manager.AdvanceTime();

        // Assert
        Assert.Equal(-1, manager.GetQuarter());
        Assert.Equal(-1, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void IncrementGamesFinished_IncrementsCountByOne()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 10;

        // Act
        manager.IncrementGamesFinished(1);

        // Assert
        Assert.False(manager.GamesOver);
    }

    [Fact]
    public void IncrementGamesFinished_IncrementsCountByMultiple()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 10;

        // Act
        manager.IncrementGamesFinished(3);

        // Assert
        Assert.False(manager.GamesOver);
    }

    [Fact]
    public void IncrementGamesFinished_MultipleIncrements_AccumulatesCorrectly()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 10;

        // Act
        manager.IncrementGamesFinished(2);
        manager.IncrementGamesFinished(3);
        manager.IncrementGamesFinished(5);

        // Assert
        Assert.True(manager.GamesOver);
    }

    [Fact]
    public void IncrementGamesFinished_ThrowsException_WhenCountExceedsGamesScheduled()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 10;

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            manager.IncrementGamesFinished(11));

        Assert.Equal("Cannot increment games finished beyond games scheduled.", exception.Message);
    }

    [Fact]
    public void IncrementGamesFinished_ThrowsException_WhenAccumulatedCountExceedsGamesScheduled()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 10;
        manager.IncrementGamesFinished(8);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() =>
            manager.IncrementGamesFinished(3));

        Assert.Equal("Cannot increment games finished beyond games scheduled.", exception.Message);
    }

    [Fact]
    public void IncrementGamesFinished_DoesNotThrow_WhenCountEqualsRemainingScheduled()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 10;
        manager.IncrementGamesFinished(7);

        // Act & Assert - Should not throw
        manager.IncrementGamesFinished(3);

        Assert.True(manager.GamesOver);
    }

    [Fact]
    public void AdvanceTime_FullGameSimulation_ProgressesThroughAllQuarters()
    {
        // Arrange
        var manager = new GameTimeManager(1, 3);

        // Act & Assert - 1st quarter ending
        manager.AdvanceTime(); // 2 seconds
        manager.AdvanceTime(); // 1 second
        manager.AdvanceTime(); // 0 seconds
        Assert.Equal(1, manager.GetQuarter());
        Assert.Equal(0, manager.GetQuarterSecondsRemaining());

        // Advance to 2nd quarter
        manager.AdvanceTime();
        Assert.Equal(2, manager.GetQuarter());
        Assert.Equal(900, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void GetQuarter_ReturnsCurrentQuarter()
    {
        // Arrange
        var manager = new GameTimeManager(3, 450);

        // Act
        var quarter = manager.GetQuarter();

        // Assert
        Assert.Equal(3, quarter);
    }

    [Fact]
    public void GetQuarterSecondsRemaining_ReturnsCurrentSeconds()
    {
        // Arrange
        var manager = new GameTimeManager(2, 720);

        // Act
        var seconds = manager.GetQuarterSecondsRemaining();

        // Assert
        Assert.Equal(720, seconds);
    }

    [Fact]
    public void AdvanceTime_ThreadSafety_HandlesMultipleIncrements()
    {
        // Arrange
        var manager = new GameTimeManager(1, 100);

        // Act - Simulate concurrent calls
        Parallel.For(0, 50, _ => manager.AdvanceTime());

        // Assert
        Assert.Equal(50, manager.GetQuarterSecondsRemaining());
    }

    [Fact]
    public void IncrementGamesFinished_ThreadSafety_HandlesMultipleIncrements()
    {
        // Arrange
        var manager = new GameTimeManager();
        manager.GamesScheduled = 100;

        // Act - Simulate concurrent calls
        Parallel.For(0, 50, _ => manager.IncrementGamesFinished(1));

        // Assert - All 50 increments should succeed since we have room for 100
        Assert.False(manager.GamesOver);
    }
}
