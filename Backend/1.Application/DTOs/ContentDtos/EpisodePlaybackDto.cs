namespace _1.Application.DTOs.ContentDtos;

public class EpisodePlaybackDto
{
    public Guid EpisodeId { get; set; }
    public string VideoUrl { get; set; } = null!;
    public int? DurationInSeconds { get; set; }
    public int? LastPositionSeconds { get; set; }
}