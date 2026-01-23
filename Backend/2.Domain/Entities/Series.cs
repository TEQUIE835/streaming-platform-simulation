namespace _2.Domain.Entities;

public class Series
{
    public Guid ContentId { get; set; }
    public Content Content { get; set; }= null!;

    public ICollection<Season> Seasons { get; set; } = new List<Season>();
}