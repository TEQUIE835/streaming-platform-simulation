namespace _1.Application.DTOs.ContentDtos;

public class EpisodeDto
{
    public Guid Id { get; set; }
    public Guid ContentId { get; set; }
    public int EpisodeNumber { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int? DurationInSeconds { get; set; }
}