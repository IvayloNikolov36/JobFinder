using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services.CloudImages;

public interface ICloudImageService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile);

    Task DeleteImagesAsync(params string[] publicIds);

    string GetImageUrl(string publicId, string extension);

    string GetImageThumbnailUrl(string publicId, string extension);
}
