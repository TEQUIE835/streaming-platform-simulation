using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3.Infrastructure.Persistence.Configuration;

public class ContentConfiguration : IEntityTypeConfiguration<Content>
{
    public void Configure(EntityTypeBuilder<Content> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Type).HasConversion<string>();
        builder.Property(c => c.Status).HasConversion<string>();
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(c => c.ThumbnailUrl)
            .IsRequired();

        builder.Property(c => c.ThumbnailPublicId)
            .IsRequired();
    }
}