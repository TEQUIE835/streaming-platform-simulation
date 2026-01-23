using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3.Infrastructure.Persistence.Configuration;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.ContentId);
        builder.Property(m => m.VideoPublicId)
            .IsRequired()
            .HasMaxLength(500);
        builder.Property(m => m.VideoPublicId)
            .IsRequired()
            .HasMaxLength(500);
    }
}