using Microsoft.AspNetCore.Http;

namespace _1.Application.DTOs.ContentDtos;

public class UpdateContentDto
{
       public string? Title { get; set; }
    public string? Description { get; set; }
    public IReadOnlyCollection<Guid>? GenreIds { get; set; }
    public IFormFile? Thumbnail { get; set; }
}