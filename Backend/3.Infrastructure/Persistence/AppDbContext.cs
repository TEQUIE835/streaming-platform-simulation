using System.Reflection;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<User> users { get; set; }
    public DbSet<RefreshToken> refreshTokens { get; set; }
    public DbSet<Content> contents { get; set; } 
    public DbSet<Genre> genres { get; set; }
    public DbSet<ContentGenre> contentGenres { get; set; }
    public DbSet<Movie> movies { get; set; }
    public DbSet<Series> series { get; set; }
    public DbSet<Season> seasons { get; set; }
    public DbSet<Episode> episodes { get; set; }
    public DbSet<WatchHistory> WatchHistories { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}