using _1.Application.DTOs.WatchHistoryDtos;
using _1.Application.Interfaces.WatchHistoryInterfaces;
using _2.Domain.Entities;

namespace _1.Application.Services;

public class WatchHistoryService : IWatchHistoryService
{
    private readonly IWatchHistoryRepository _repository;

    public WatchHistoryService(IWatchHistoryRepository repository)
    {
        _repository = repository;
    }

    public async Task UpdateProgressAsync(
        Guid userId,
        UpdateWatchProgressDto dto)
    {
        var history = await _repository.GetActiveAsync(
            userId,
            dto.ContentId,
            dto.EpisodeId);

        if (history == null)
        {
            history = new WatchHistory
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ContentId = dto.ContentId,
                EpisodeId = dto.EpisodeId,
                LastPositionSeconds = dto.LastPositionSeconds,
                StartedAt = DateTime.UtcNow,
                LastWatchedAt = DateTime.UtcNow,
                IsCompleted = false
            };

            await _repository.AddAsync(history);
        }
        else
        {
            history.LastPositionSeconds = dto.LastPositionSeconds;
            history.LastWatchedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(history);
        }
    }
    

    public async Task<IReadOnlyCollection<WatchHistoryDto>> GetByUserAsync(Guid userId)
    {
        var histories = await _repository.GetByUserAsync(userId);

        return histories.Select(h => new WatchHistoryDto
        {
            Id = h.Id,
            ContentId = h.ContentId,
            EpisodeId = h.EpisodeId,
            LastPositionSeconds = h.LastPositionSeconds,
            IsCompleted = h.IsCompleted,
            LastWatchedAt = h.LastWatchedAt
        }).ToList();
    }

    public async Task MarkCompletedAsync(Guid historyId)
    {
        var history = await _repository.GetByIdAsync(historyId);
        if (history == null)
            throw new ArgumentException("History not found");

        history.IsCompleted = true;
        history.LastWatchedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(history);
    }
}
