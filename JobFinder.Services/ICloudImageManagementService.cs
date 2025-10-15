using JobFinder.Web.Models.CloudImage;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services;

public interface ICloudImageManagementService
{
    Task<CloudImageViewModel> UploadImage(IFormFile imageFile, string userId);

    Task<string> GetUrl(int pictureId);

    Task<string> GetThumbnailUrl(int pictureId);
}
