using Microsoft.AspNetCore.Http;

namespace _1.Application.DTOs.ContentDtos;

public class CreateMovieDto
{
    public CreateContentDto Content { get; set; }
    public IFormFile VideoFile { get; set; } = null!;
}