using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace _3.Infrastructure.Persistence.Configuration;

public class SeasonConfiguration : IEntityTypeConfiguration<Season>
{
    public void Configure(EntityTypeBuilder<Season> builder)
    {
        builder.HasKey(se => se.Id);
        builder.HasOne(se => se.Series)
            .WithMany(s => s.Seasons)
            .HasForeignKey(se => se.SeriesId);
    }
}