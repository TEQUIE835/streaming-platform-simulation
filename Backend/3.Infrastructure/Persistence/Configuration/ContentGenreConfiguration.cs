using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3.Infrastructure.Persistence.Configuration;

public class ContentGenreConfiguration : IEntityTypeConfiguration<ContentGenre>
{
    public void Configure(EntityTypeBuilder<ContentGenre> builder)
    {
        builder.HasKey(cg => new {cg.ContentId, cg.GenreId});
        builder.HasOne(cg => cg.Genre)
            .WithMany()
            .HasForeignKey(cg => cg.GenreId);
        builder.HasOne(cg => cg.Content)
            .WithMany(c => c.Genres)
            .HasForeignKey(cg => cg.ContentId);
    }
}