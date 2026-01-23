using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3.Infrastructure.Persistence.Configuration;

public class EpisodeConfiguration : IEntityTypeConfiguration<Episode>
{
    public void Configure(EntityTypeBuilder<Episode> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.VideoPublicId)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(e => e.VideoUrl)
            .IsRequired()
            .HasMaxLength(500);
        builder.HasOne(e => e.Season)
            .WithMany(se => se.Episodes)
            .HasForeignKey(e => e.SeasonId);
        builder.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(e => e.Description)
            .IsRequired()
            .HasMaxLength(2000);
    }
}