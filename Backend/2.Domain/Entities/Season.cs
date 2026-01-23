namespace _2.Domain.Entities;

public class Season
{
    public Guid Id { get; set; }
    public Guid SeriesId { get; set; }
    public Series Series { get; set; } = null!;
    public int SeasonNumber { get; set; }
    public ICollection<Episode> Episodes { get; set; } =  new List<Episode>();
}