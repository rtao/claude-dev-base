using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerTracker.Domain.Models;

namespace PokerTracker.Infrastructure.Data.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();

        builder.Property(s => s.UserId).IsRequired();
        builder.Property(s => s.Location).IsRequired();
        builder.Property(s => s.Stake).IsRequired();
        builder.Property(s => s.Date).IsRequired();

        builder.Property(s => s.GameType).HasConversion<string>();
        builder.Property(s => s.Currency).HasConversion<string>();

        builder.Property(s => s.BuyIn).HasPrecision(18, 2);
        builder.Property(s => s.CashOut).HasPrecision(18, 2);

        builder.Ignore(s => s.Profit);

        builder.HasMany(s => s.Hands)
            .WithOne(h => h.Session)
            .HasForeignKey(h => h.SessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(s => s.UserId);
    }
}
