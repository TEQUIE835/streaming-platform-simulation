using _1.Application.Interfaces.WatchHistoryInterfaces;
using _2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _3.Infrastructure.Persistence.Repositories;

public class WatchHistoryRepository : IWatchHistoryRepository
{
    private readonly AppDbContext _dbContext;
    public WatchHistoryRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<WatchHistory?> GetByIdAsync(Guid id)
    {
        return await _dbContext.WatchHistories
            .FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<WatchHistory?> GetActiveAsync(Guid userId, Guid contentId, Guid? episodeId)
    {
        return await _dbContext.WatchHistories
            .Where(h =>
                h.UserId == userId &&
                h.ContentId == contentId &&
                h.EpisodeId == episodeId &&
                !h.IsCompleted)
            .OrderByDescending(h => h.LastWatchedAt)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<WatchHistory>> GetByUserAsync(Guid userId)
    {
        return await _dbContext.WatchHistories
            .Where(h => h.UserId == userId)
            .OrderByDescending(h => h.LastWatchedAt)
            .ToListAsync();
    }

    public async Task AddAsync(WatchHistory history)
    {
        _dbContext.WatchHistories.Add(history);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(WatchHistory history)
    {
        _dbContext.WatchHistories.Update(history);
        await _dbContext.SaveChangesAsync();
    }
}