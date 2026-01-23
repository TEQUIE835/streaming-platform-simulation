namespace _1.Application.DTOs.CloudinaryDtos;

public class CloudinaryUploadResultDto
{
    public string Url { get; set; } 
    public string PublicId { get; set; } 
    /// <summary>
    /// Duration in seconds (only for video uploads).
    /// Null for images.
    /// </summary>
    public int? DurationInSeconds { get; set; }
}