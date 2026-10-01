using FluentAssertions;
using PokerTracker.Domain.Models;
using Xunit;

namespace PokerTracker.Domain.Tests;

public class SessionTests
{
    [Fact]
    public void NewSession_HasEmptyHandsCollection()
    {
        // Arrange & Act
        var session = new Session();

        // Assert
        session.Hands.Should().NotBeNull();
        session.Hands.Should().BeEmpty();
    }

    [Theory]
    [InlineData(100, 250, 150)]
    [InlineData(500, 300, -200)]
    [InlineData(200, 200, 0)]
    public void Profit_ReturnsCashOutMinusBuyIn(decimal buyIn, decimal cashOut, decimal expectedProfit)
    {
        // Arrange
        var session = new Session
        {
            BuyIn = buyIn,
            CashOut = cashOut
        };

        // Act
        var profit = session.Profit;

        // Assert
        profit.Should().Be(expectedProfit);
    }
}
