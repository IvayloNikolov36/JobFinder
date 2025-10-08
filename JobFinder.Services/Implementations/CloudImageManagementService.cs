
using CloudinaryDotNet.Actions;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CloudImages;
using JobFinder.Transfer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace JobFinder.Services.Implementations;

public class CloudImageManagementService : IImageManagementService
{
    private readonly ICloudImageService cloudImageService;
    private readonly IEntityFrameworkUnitOfWork unitOfWork;

    private readonly string imagePath;

    public CloudImageManagementService(
        IEntityFrameworkUnitOfWork unitOfWork, 
        ICloudImageService cloudImageService,
        IConfiguration configuration)
    {
        this.unitOfWork = unitOfWork;
        this.cloudImageService = cloudImageService;
        this.imagePath = configuration.GetSection("Cloudinary:ImagePath").Value;
    }

    public async Task<int> UploadImage(IFormFile imageFile, string userId)
    {
        ImageUploadResult imageResult = await this.cloudImageService
            .UploadImageAsync(imageFile);

        string imageExtension = this.GetExtension(imageFile.ContentType);

        (string url, string thumbnailUrl) = this.GetImageUrls(
            imageResult.PublicId,
            imageExtension);

        CloudImageDTO imageDto = new()
        {
            Id = 0,
            PublicId = imageResult.PublicId,
            Url = url,
            ThumbnailUrl = thumbnailUrl,
            Extension = imageExtension,
            Size = imageFile.Length,
            UploaderId = userId
        };

        await this.unitOfWork.CloudImageRepository.Add(imageDto);

        await this.unitOfWork.SaveChanges<CloudImageDTO, int>(imageDto);

        return imageDto.Id;
    }

    private (string, string) GetImageUrls(string publicId, string imageExtension)
    {        
        string imageUrl = this.cloudImageService
            .GetImageUrl(publicId, imageExtension)
            .Replace(this.imagePath, string.Empty);

        string imageThumbnailUrl = this.cloudImageService
            .GetImageUrl(publicId, imageExtension)
            .Replace(this.imagePath, string.Empty);

        return (imageUrl, imageThumbnailUrl);
    }

    private string GetExtension(string contentType)
    {
        int index = contentType.LastIndexOf('/');
        string imageExtension = contentType[(index + 1)..];
        return imageExtension;
    }
}
