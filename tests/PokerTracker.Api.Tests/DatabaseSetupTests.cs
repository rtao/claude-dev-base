using FluentAssertions;
using Microsoft.Data.Sqlite;
using Xunit;
using Microsoft.EntityFrameworkCore;
using PokerTracker.Infrastructure.Data;

namespace PokerTracker.Api.Tests;

public class DatabaseSetupTests : IDisposable
{
    private readonly SqliteConnection _connection;
    private readonly PokerTrackerDbContext _context;

    public DatabaseSetupTests()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        var options = new DbContextOptionsBuilder<PokerTrackerDbContext>()
            .UseSqlite(_connection)
            .Options;

        _context = new PokerTrackerDbContext(options);
        _context.Database.EnsureCreated();
    }

    [Fact]
    public void EnsureCreated_AppliesSchemaCleanly()
    {
        // Act - schema was created in constructor
        // Assert - verify we can query without errors
        var sessions = _context.Sessions.ToList();
        sessions.Should().BeEmpty();

        var hands = _context.HandHistories.ToList();
        hands.Should().BeEmpty();
    }

    [Fact]
    public void Schema_ContainsSessionsTable()
    {
        // Act
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='Sessions'";
        var result = command.ExecuteScalar();

        // Assert
        result.Should().Be("Sessions");
    }

    [Fact]
    public void Schema_ContainsHandHistoriesTable()
    {
        // Act
        using var command = _connection.CreateCommand();
        command.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='HandHistories'";
        var result = command.ExecuteScalar();

        // Assert
        result.Should().Be("HandHistories");
    }

    public void Dispose()
    {
        _context.Dispose();
        _connection.Dispose();
    }
}
