using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerTracker.Domain.Models;

namespace PokerTracker.Infrastructure.Data.Configurations;

public class HandHistoryConfiguration : IEntityTypeConfiguration<HandHistory>
{
    public void Configure(EntityTypeBuilder<HandHistory> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedOnAdd();

        builder.Property(h => h.SessionId).IsRequired();

        builder.Property(h => h.PotSize).HasPrecision(18, 2);

        builder.HasOne(h => h.Session)
            .WithMany(s => s.Hands)
            .HasForeignKey(h => h.SessionId)
            .IsRequired();
    }
}
