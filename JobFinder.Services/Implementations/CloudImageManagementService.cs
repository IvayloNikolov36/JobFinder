
using AutoMapper;
using CloudinaryDotNet.Actions;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Services.CloudImages;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.CloudImage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace JobFinder.Services.Implementations;

public class CloudImageManagementService : ICloudImageManagementService
{
    private readonly ICloudImageService cloudImageService;
    private readonly IEntityFrameworkUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    private readonly string imagePath;

    public CloudImageManagementService(
        IEntityFrameworkUnitOfWork unitOfWork, 
        ICloudImageService cloudImageService,
        IConfiguration configuration,
        IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.cloudImageService = cloudImageService;
        this.mapper = mapper;
        this.imagePath = configuration.GetSection("Cloudinary:ImagePath").Value;
    }

    public async Task<CloudImageViewModel> UploadImage(
        IFormFile imageFile,
        string userId,
        bool replaceCurrent = false)
    {
        ImageUploadResult imageResult = await this.cloudImageService
            .UploadImageAsync(imageFile);

        (string url, string thumbnailUrl) = this.GetImageUrls(
            imageResult.PublicId,
            GetExtension(imageFile.FileName));

        CloudImageDTO imageDto = new()
        {
            Id = 0,
            PublicId = imageResult.PublicId,
            Url = url,
            ThumbnailUrl = thumbnailUrl,
            Size = imageFile.Length,
            UserId = userId
        };

        if (replaceCurrent)
        {
            await this.unitOfWork.CloudImageRepository
                .Update(userId, imageDto);
        }
        else
        {
            await this.unitOfWork.CloudImageRepository
                .Add(imageDto);
        }

        await this.unitOfWork
            .SaveChanges<CloudImageDTO, int>(imageDto);

        return this.mapper
            .Map<CloudImageViewModel>(imageDto);
    }

     // TODO: create a method for both urls

    public async Task<string> GetUrl(int pictureId)
    {
        string url = await this.unitOfWork.CloudImageRepository.GetUrl(pictureId);
        string fullUrl = $"{this.imagePath}{url}";

        return fullUrl;
    }

    public async Task<string> GetThumbnailUrl(int pictureId)
    {
        string url = await this.unitOfWork
            .CloudImageRepository
            .GetThumbnailUrl(pictureId);

        string fullUrl = $"{this.imagePath}{url}";

        return fullUrl;
    }

    private (string, string) GetImageUrls(string publicId, string imageExtension)
    {        
        string imageUrl = this.cloudImageService
            .GetImageUrl(publicId, imageExtension)
            .Replace(this.imagePath, string.Empty);

        string imageThumbnailUrl = this.cloudImageService
            .GetImageThumbnailUrl(publicId, imageExtension)
            .Replace(this.imagePath, string.Empty);

        return (imageUrl, imageThumbnailUrl);
    }

    private static string GetExtension(string fileName)
    {
        int index = fileName.LastIndexOf('.');

        return fileName[(index + 1)..];
    }
}
