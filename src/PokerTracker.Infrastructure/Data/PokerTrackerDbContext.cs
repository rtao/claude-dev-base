using Microsoft.EntityFrameworkCore;
using PokerTracker.Domain.Models;

namespace PokerTracker.Infrastructure.Data;

public class PokerTrackerDbContext : DbContext
{
    public PokerTrackerDbContext(DbContextOptions<PokerTrackerDbContext> options)
        : base(options)
    {
    }

    public DbSet<Session> Sessions => Set<Session>();
    public DbSet<HandHistory> HandHistories => Set<HandHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokerTrackerDbContext).Assembly);
    }
}
