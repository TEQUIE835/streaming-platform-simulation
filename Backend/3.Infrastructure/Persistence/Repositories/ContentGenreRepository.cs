using _1.Application.Interfaces.ContentGenreInterfaces;
using _2.Domain.Entities;

namespace _3.Infrastructure.Persistence.Repositories;

public class ContentGenreRepository : IContentGenreInterface
{
    private readonly AppDbContext _dbContext;
    public ContentGenreRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddContentGenre(ContentGenre genre)
    {
        _dbContext.contentGenres.Add(genre);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteContentGenre(ContentGenre genre)
    {
        _dbContext.contentGenres.Remove(genre);
        await _dbContext.SaveChangesAsync();
    }
}