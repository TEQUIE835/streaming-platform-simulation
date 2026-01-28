using _1.Application.DTOs.WatchHistoryDtos;
using _2.Domain.Entities;

namespace _1.Application.Interfaces.WatchHistoryInterfaces;

public interface IWatchHistoryService
{
    Task UpdateProgressAsync(
        Guid userId,
        UpdateWatchProgressDto updateWatchProgressDto);

    Task<IReadOnlyCollection<WatchHistoryDto>> GetByUserAsync(Guid userId);

    Task MarkCompletedAsync(Guid historyId);
}