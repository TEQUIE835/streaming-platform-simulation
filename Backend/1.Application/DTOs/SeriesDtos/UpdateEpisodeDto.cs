using Microsoft.AspNetCore.Http;

namespace _1.Application.DTOs.SeriesDtos;

public class UpdateEpisodeDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public int? DurationInSeconds { get; set; }

    public IFormFile? VideoFile { get; set; }
}