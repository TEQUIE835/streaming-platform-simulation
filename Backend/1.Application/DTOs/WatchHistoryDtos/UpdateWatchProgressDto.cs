namespace _1.Application.DTOs.WatchHistoryDtos;

public class UpdateWatchProgressDto
{
    public Guid ContentId { get; set; }
    public Guid? EpisodeId { get; set; }
    public int LastPositionSeconds { get; set; }
}