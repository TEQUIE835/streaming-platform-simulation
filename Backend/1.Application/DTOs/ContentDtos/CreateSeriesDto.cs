using Microsoft.AspNetCore.Http;

namespace _1.Application.DTOs.ContentDtos;

public class CreateSeriesDto
{
    public CreateContentDto Content { get; set; }
    public IFormFile FirstEpisodeVideo { get; set; } = null!;
    public string FirstEpisodeTitle { get; set; } = null!;
    public string FirstEpisodeDescription { get; set; } = null!;
}