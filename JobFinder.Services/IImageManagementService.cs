using Microsoft.AspNetCore.Http;

namespace JobFinder.Services;

public interface IImageManagementService
{
    Task<int> UploadImage(IFormFile imageFile, string userId);
}
