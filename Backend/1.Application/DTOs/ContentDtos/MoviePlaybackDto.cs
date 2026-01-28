namespace _1.Application.DTOs.ContentDtos;

public class MoviePlaybackDto
{
    public Guid ContentId { get; set; }
    public string VideoUrl { get; set; } = null!;
    public int? DurationInSeconds { get; set; }
    
}