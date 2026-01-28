using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace _1.Application.DTOs.ContentDtos;

public class CreateContentDto
{
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public ContentType Type { get; set; }

    public IReadOnlyCollection<Guid> GenreIds { get; set; } = [];

    public IFormFile Thumbnail { get; set; } = null!;
}