namespace _2.Domain.Entities;

public class Movie
{
    public Guid ContentId { get; set; }
    public Content Content { get; set; } = null!;
    public int? DurationInSeconds { get; set; }
    public string VideoUrl { get; set; } = null!;
    public string VideoPublicId { get; set; } = null!;
}