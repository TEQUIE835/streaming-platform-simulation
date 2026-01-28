namespace _1.Application.DTOs.ContentDtos;

public class MovieDetailDto : ContentDetailDto
{
    public int? DurationInSeconds { get; set; }
    public string VideoUrl { get; set; } = null!;
}