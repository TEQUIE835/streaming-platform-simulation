using _2.Domain.Entities;
using _3.Infrastructure.Persistence;

namespace _3.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (!context.users.Any())
        {
            var users = new List<User>
            {
                new User
                {
                    Email = "admin@test.com",
                    Username = "admin",
                    Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                    Role = UserRole.Admin
                },
                new User
                {
                    Email = "user@test.com",
                    Username = "usuario",
                    Password = BCrypt.Net.BCrypt.HashPassword("User123"),
                    Role = UserRole.User
                }
                
            };
            context.users.AddRange(users);
            await context.SaveChangesAsync();
        }
        if (!context.genres.Any())
        {
            var genres = new List<Genre>
            {
                new() { Id = Guid.NewGuid(), Name = "Documental" },
                new() { Id = Guid.NewGuid(), Name = "Acción" },
                new() { Id = Guid.NewGuid(), Name = "Drama" },
                new() { Id = Guid.NewGuid(), Name = "Comedia" },
                new() { Id = Guid.NewGuid(), Name = "Ciencia Ficcion"},
                new() {Id = Guid.NewGuid(), Name = "Suspenso/Thriller"}
            };

            context.genres.AddRange(genres);
            await context.SaveChangesAsync();
        }
    }
}