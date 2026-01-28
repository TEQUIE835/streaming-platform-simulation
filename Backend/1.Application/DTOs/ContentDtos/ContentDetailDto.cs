using _2.Domain.Entities;


namespace _1.Application.DTOs.ContentDtos;

public class ContentDetailDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
    public ContentType Type { get; set; }
    public ContentStatus Status { get; set; }
    public IReadOnlyCollection<string> Genres { get; set; } = [];
}