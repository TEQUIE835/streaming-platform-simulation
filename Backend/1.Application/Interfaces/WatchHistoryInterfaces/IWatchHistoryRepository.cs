using _2.Domain.Entities;

namespace _1.Application.Interfaces.WatchHistoryInterfaces;

public interface IWatchHistoryRepository
{
    Task<WatchHistory?> GetByIdAsync(Guid id);

    Task<WatchHistory?> GetActiveAsync(
        Guid userId,
        Guid contentId,
        Guid? episodeId);

    Task<IEnumerable<WatchHistory>> GetByUserAsync(Guid userId);

    Task AddAsync(WatchHistory history);

    Task UpdateAsync(WatchHistory history);
}