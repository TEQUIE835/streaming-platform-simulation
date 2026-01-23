namespace _2.Domain.Entities;

public class Episode
{
    public Guid Id { get; set; }
    public Guid SeasonId { get; set; }
    public Season Season { get; set; } = null!;
    public int EpisodeNumber { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DurationInSeconds { get; set; }
    public string VideoUrl { get; set; } = null!;
    public string VideoPublicId { get; set; } = null!;
}