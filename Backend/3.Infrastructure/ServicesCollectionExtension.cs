using _1.Application.Interfaces.ContentGenreInterfaces;
using _1.Application.Interfaces.ContentInterfaces;
using _1.Application.Interfaces.EpisodeInterfaces;
using _1.Application.Interfaces.GenreInterfaces;
using _1.Application.Interfaces.CloudinaryInterfaces;
using _1.Application.Interfaces.MovieInterfaces;
using _1.Application.Interfaces.SeasonInterfaces;
using _1.Application.Interfaces.SeriesInterfaces;
using _1.Application.Interfaces.TokenInterfaces;
using _1.Application.Interfaces.UserInterfaces;
using _1.Application.Interfaces.WatchHistoryInterfaces;
using _3.Infrastructure.Data;
using _3.Infrastructure.Persistence;
using _3.Infrastructure.Persistence.Repositories;
using _3.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace _3.Infrastructure;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<AppDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ICloudinaryService, CloudinaryService>();
        services.AddScoped<IContentGenreInterface, ContentGenreRepository>();
        services.AddScoped<IContentRepository, ContentRepository>();
        services.AddScoped<IEpisodeRepository, EpisodeRepository>();
        services.AddScoped<IGenreRepository, GenreRepository>();
        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddScoped<ISeasonRepository, SeasonRepository>();
        services.AddScoped<ISeriesRepository, SeriesRepository>();
        services.AddScoped<IWatchHistoryRepository, WatchHistoryRepository>();
        return services;
    }
    public static async Task SeedAsync(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        await DbSeeder.SeedAsync(context);
    }
}

