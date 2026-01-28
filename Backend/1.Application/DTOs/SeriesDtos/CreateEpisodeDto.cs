using Microsoft.AspNetCore.Http;

namespace _1.Application.DTOs.SeriesDtos;

public class CreateEpisodeDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DurationInSeconds { get; set; }

    public IFormFile VideoFile { get; set; } = null!;
}