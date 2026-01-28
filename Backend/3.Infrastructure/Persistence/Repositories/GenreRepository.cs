using _1.Application.Interfaces.GenreInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _dbContext;

    public GenreRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<Genre>?> GetAllGenres()
    {
        return await _dbContext.genres.ToListAsync();
    }

    public async Task<Genre?> GetGenreById(Guid id)
    {
        return await _dbContext.genres.FirstOrDefaultAsync(genre => genre.Id == id);
    }

    public async Task<Genre?> GetGenreByName(string name)
    {
        return await _dbContext.genres.FirstOrDefaultAsync(genre => genre.Name == name);
    }

    public async Task<Genre?> AddGenre(Genre genre)
    {
        _dbContext.genres.Add(genre);
        await _dbContext.SaveChangesAsync();
        return genre;
    }

    public async Task<Genre?> UpdateGenre(Genre genre)
    {
        _dbContext.genres.Update(genre);
        await _dbContext.SaveChangesAsync();
        return genre;
    }

    public async Task DeleteGenre(Genre genre)
    {
        _dbContext.genres.Remove(genre);
        await _dbContext.SaveChangesAsync();
    }
}