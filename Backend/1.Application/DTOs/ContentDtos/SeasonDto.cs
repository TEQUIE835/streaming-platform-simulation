namespace _1.Application.DTOs.ContentDtos;

public class SeasonDto
{
    public int SeasonNumber { get; set; }
    public IReadOnlyCollection<EpisodeDto> Episodes { get; set; } = [];
}