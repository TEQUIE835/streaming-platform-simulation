using _1.Application.Interfaces.ContentInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Repositories;

public class ContentRepository : IContentRepository
{
    private readonly AppDbContext _dbContext;
    public ContentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IReadOnlyCollection<Content>> GetAllAsync()
    {
        return await _dbContext.contents
            .Where(c => c.Status == ContentStatus.Published)
            .Include(c => c.Genres)
            .ThenInclude(cg => cg.Genre)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Content>> GetByTypeAsync(ContentType type)
    {
        return await _dbContext.contents
            .Where(c => c.Type == type && c.Status == ContentStatus.Published)
            .Include(c => c.Genres)
            .ThenInclude(cg => cg.Genre)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyCollection<Content>> GetByGenreAsync(Guid genreId)
    {
        return await _dbContext.contents
            .Where(c =>
                c.Status == ContentStatus.Published &&
                c.Genres.Any(cg => cg.GenreId == genreId))
            .Include(c => c.Genres)
            .ThenInclude(cg => cg.Genre)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Content?> GetByIdAsync(Guid id)
    {
        return await _dbContext.contents.FindAsync(id);
    }

    public async Task<Content?> GetByIdAsync(Guid id, ContentType type)
    {
        IQueryable<Content> query = _dbContext.contents;

        query = type switch
        {
            ContentType.Movie => query
                .Include(c => c.Movie),

            ContentType.Series => query
                .Include(c => c.Series)
                .ThenInclude(s => s.Seasons)
                .ThenInclude(se => se.Episodes),

            _ => query
        };

        return await query
            .Include(c => c.Genres)
            .ThenInclude(cg => cg.Genre)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Content?> AddContent(Content content)
    {
        _dbContext.contents.Add(content);
        await _dbContext.SaveChangesAsync();
        return content;
    }

    public async Task<Content?> UpdateContent(Content content)
    {
        _dbContext.contents.Update(content);
        await _dbContext.SaveChangesAsync();
        return content;
    }

    public async Task DeleteContent(Content content)
    {
        _dbContext.contents.Remove(content);
        await _dbContext.SaveChangesAsync();
    }
}