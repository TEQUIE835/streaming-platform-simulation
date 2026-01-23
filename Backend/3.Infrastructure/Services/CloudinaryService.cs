using _1.Application.DTOs.CloudinaryDtos;
using _1.Application.Interfaces.ImageInterfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace _3.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration configuration)
    {
        var cloudName = configuration["Cloudinary:CloudName"];
        var apiKey = configuration["Cloudinary:ApiKey"];
        var apiSecret = configuration["Cloudinary:ApiSecret"];

        if (string.IsNullOrWhiteSpace(cloudName) ||
            string.IsNullOrWhiteSpace(apiKey) ||
            string.IsNullOrWhiteSpace(apiSecret))
        {
            throw new InvalidOperationException("Cloudinary credentials are not configured.");
        }

        var account = new Account(cloudName, apiKey, apiSecret);
        _cloudinary = new Cloudinary(account);
    }

    public async Task<CloudinaryUploadResultDto> UploadImageAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "images"
        };
        var result = await _cloudinary.UploadAsync(uploadParams);
        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Error uploading image to Cloudinary");
        return new CloudinaryUploadResultDto
        {
            Url = result.SecureUrl.ToString(),
            PublicId = result.PublicId,
            DurationInSeconds = null
        };
    }

    public async Task<CloudinaryUploadResultDto> UploadVideoAsync(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var uploadParams = new VideoUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            Folder = "videos"
        };
        var result = await _cloudinary.UploadAsync(uploadParams);
        if (result.StatusCode != System.Net.HttpStatusCode.OK)
            throw new Exception("Error uploading video to Cloudinary");
        return new CloudinaryUploadResultDto()
        {
            Url = result.SecureUrl.ToString(),
            PublicId = result.PublicId,
            DurationInSeconds = (int)Math.Floor(result.Duration)
        };
    }

    public async Task DeleteAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Result != "ok")
            throw new Exception("Error deleting image from Cloudinary");
    }
}