namespace _1.Application.DTOs.WatchHistoryDtos;

public class WatchHistoryDto
{
    public Guid Id { get; set; }

    public Guid ContentId { get; set; }
    public Guid? EpisodeId { get; set; }

    public int LastPositionSeconds { get; set; }
    public bool IsCompleted { get; set; }

    public DateTime LastWatchedAt { get; set; }

}