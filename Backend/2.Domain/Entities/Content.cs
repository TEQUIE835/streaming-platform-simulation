namespace _2.Domain.Entities;

public class Content
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string ThumbnailUrl { get; set; } = null!;
    public string ThumbnailPublicId { get; set; } = null!;
    public ContentType Type { get; set; }
    public ContentStatus Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public ICollection<ContentGenre> Genres { get; set; } = new List<ContentGenre>();
    public Movie? Movie { get; set; }
    public Series? Series { get; set; }
}

public enum ContentType
{
    Movie = 1,
    Series = 2,
}

public enum ContentStatus
{
    Draft = 1,
    Published = 2,
    Deleted = 3,
}