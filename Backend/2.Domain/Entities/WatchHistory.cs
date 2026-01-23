namespace _2.Domain.Entities;

public class WatchHistory
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public User User { get; set; }

    public Guid ContentId { get; set; }
    public Content Content { get; set; }

    public Guid? EpisodeId { get; set; }
    public Episode? Episode { get; set; }

    public int LastPositionSeconds { get; set; }
    public bool IsCompleted { get; set; }

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime LastWatchedAt { get; set; } = DateTime.UtcNow;
}