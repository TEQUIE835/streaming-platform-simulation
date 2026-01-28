using _1.Application.DTOs.CloudinaryDtos;
using Microsoft.AspNetCore.Http;

namespace _1.Application.Interfaces.CloudinaryInterfaces;

public interface ICloudinaryService
{
    Task<CloudinaryUploadResultDto> UploadImageAsync(IFormFile file);
    Task<CloudinaryUploadResultDto> UploadVideoAsync(IFormFile file);
    Task DeleteAsync(string publicId);
    
    
}