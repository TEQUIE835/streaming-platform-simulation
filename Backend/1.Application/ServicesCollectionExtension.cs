using System.Text;
using _1.Application.Interfaces.ContentInterfaces;
using _1.Application.Interfaces.GenreInterfaces;
using _1.Application.Interfaces.SeriesInterfaces;
using _1.Application.Interfaces.WatchHistoryInterfaces;
using _1.Application.Services;
using _1.Application.Services.AuthServices;
using _1.Application.Services.HashingServices;
using _1.Application.Services.TokenServices;
using _1.Application.Services.UserServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace _1.Application;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtKey =
            configuration["Jwt:Key"] ??
            configuration["SecretKey"] ??
            Environment.GetEnvironmentVariable("SECRET_KEY");

        var jwtIssuer =
            configuration["Jwt:Issuer"] ??
            configuration["Issuer"] ??
            Environment.GetEnvironmentVariable("ISSUER");

        var jwtAudience =
            configuration["Jwt:Audience"] ??
            configuration["Audience"] ??
            Environment.GetEnvironmentVariable("AUDIENCE");

        var expirationInMinutes = int.Parse(configuration["Jwt:ExpirationInMinutes"] ?? "7");
        if (string.IsNullOrWhiteSpace(jwtKey))
            throw new InvalidOperationException("JWT Key is not configured. Set Jwt:Key or SECRET_KEY.");
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    ValidateIssuer = !string.IsNullOrWhiteSpace(jwtIssuer),
                    ValidIssuer = jwtIssuer,
                    ValidateAudience = !string.IsNullOrWhiteSpace(jwtAudience),
                    ValidAudience = jwtAudience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });

        services.AddScoped<BCryptService>();
        services.AddScoped<CreateToken>();
        services.AddScoped<AuthService>();
        services.AddScoped<UserService>(); 
        services.AddScoped<IContentService,ContentService>();  
        services.AddScoped<IGenreService, GenreService>();  
        services.AddScoped<ISeriesService, SeriesService>();  
        services.AddScoped<IWatchHistoryService, WatchHistoryService>();  
        return services;
    }
    
}