using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3.Infrastructure.Persistence.Configuration;

public class WatchHistoryConfiguration : IEntityTypeConfiguration<WatchHistory>
{
    public void Configure(EntityTypeBuilder<WatchHistory> builder)
    {
        builder.HasKey(h => new { h.ContentId, h.UserId });
        builder.HasOne(h => h.User)
            .WithMany(u => u.WatchHistories)
            .HasForeignKey(h => h.UserId);
        builder.HasOne(h => h.Content)
            .WithMany()
            .HasForeignKey(h => h.ContentId);
        builder.HasOne(h => h.Episode)
            .WithMany()
            .HasForeignKey(h => h.EpisodeId)
            .OnDelete(DeleteBehavior.SetNull);
        
    }
}