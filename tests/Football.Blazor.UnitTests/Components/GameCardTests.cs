using FluentAssertions;
using Football.Application.Features.Plays.Models;
using Football.Blazor.Components;

namespace Football.Blazor.UnitTests.Components;

public class GameCardTests : TestContext
{
    [Fact]
    public void GameCard_RendersWithValidData()
    {
        // Arrange
        var play = new PlayDto
        {
            Id = 1,
            GameId = 123,
            HomeTeam = "Chiefs",
            HomeScore = 21,
            AwayTeam = "Bills",
            AwayScore = 14,
            Quarter = 2,
            QuarterSecondsRemaining = 180, // 3 minutes
            Description = "Pass complete for 15 yards"
        };

        // Act
        IRenderedComponent<GameCard> cut = RenderComponent<GameCard>(parameters => parameters
            .Add(p => p.GameId, 123)
            .Add(p => p.Play, play));

        // Assert
        cut.Find(".score-card").Should().NotBeNull();
        cut.Find("a[href='/game/123']").Should().NotBeNull();

        // Check team names are displayed
        IRefreshableElementCollection<AngleSharp.Dom.IElement> teamElements = cut.FindAll(".mdc-typography--body2");
        teamElements.Should().Contain(e => e.TextContent.Contains("Bills"));
        teamElements.Should().Contain(e => e.TextContent.Contains("Chiefs"));

        // Check scores are displayed
        teamElements.Should().Contain(e => e.TextContent.Contains("14"));
        teamElements.Should().Contain(e => e.TextContent.Contains("21"));

        // Check quarter and time are displayed
        var captionElements = cut.FindAll(".mdc-typography--caption");
        captionElements.Should().Contain(e => e.TextContent.Contains("2nd - 03:00"));
        captionElements.Should().Contain(e => e.TextContent.Contains("Pass complete for 15 yards"));
    }

    [Theory]
    [InlineData(1, "1st")]
    [InlineData(2, "2nd")]
    [InlineData(3, "3rd")]
    [InlineData(4, "4th")]
    [InlineData(5, "OT")]
    [InlineData(6, "")]
    public void GameCard_DisplaysCorrectQuarterText(int quarter, string expected)
    {
        // Arrange
        var play = new PlayDto
        {
            Quarter = quarter,
            QuarterSecondsRemaining = 900, // 15 minutes
            HomeTeam = "Home",
            AwayTeam = "Away",
            Description = "Test play"
        };

        // Act
        var cut = RenderComponent<GameCard>(parameters => parameters
            .Add(p => p.GameId, 1)
            .Add(p => p.Play, play));

        // Assert
        var quarterText = cut.Find(".mdc-typography--caption").TextContent;
        quarterText.Should().StartWith(expected);
    }

    [Fact]
    public void GameCard_RendersWithEmptyPlayDto()
    {
        // Arrange
        var emptyPlay = new PlayDto();

        // Act
        var cut = RenderComponent<GameCard>(parameters => parameters
            .Add(p => p.GameId, 1)
            .Add(p => p.Play, emptyPlay));

        // Assert
        cut.Find(".score-card").Should().NotBeNull();
        cut.Find(".mdc-card").Should().NotBeNull();
    }

    [Fact]
    public void GameCard_NavLinkUsesCorrectGameId()
    {
        // Arrange
        var gameId = 456;
        var play = new PlayDto
        {
            HomeTeam = "Test Home",
            AwayTeam = "Test Away"
        };

        // Act
        var cut = RenderComponent<GameCard>(parameters => parameters
            .Add(p => p.GameId, gameId)
            .Add(p => p.Play, play));

        // Assert
        var navLink = cut.Find("a");
        navLink.GetAttribute("href").Should().Be("/game/456");
    }

    [Fact]
    public void GameCard_DisplaysFormattedTime()
    {
        // Arrange
        var play = new PlayDto
        {
            Quarter = 4,
            QuarterSecondsRemaining = 75, // 1 minute 15 seconds
            HomeTeam = "Home",
            AwayTeam = "Away",
            Description = "Final drive"
        };

        // Act
        var cut = RenderComponent<GameCard>(parameters => parameters
            .Add(p => p.GameId, 1)
            .Add(p => p.Play, play));

        // Assert
        var timeText = cut.Find(".mdc-typography--caption").TextContent;
        timeText.Should().Contain("4th - 01:15");
    }
}
