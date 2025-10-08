using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services.CloudImages;

public interface ICloudImageService
{
    Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile);

    Task DeleteImagesAsync(params string[] publicIds);

    string GetImageUrl(string imagePublicId, string extension);

    string GetImageThumbnailUrl(string imageThumbnailPublicId, string extension);
}
