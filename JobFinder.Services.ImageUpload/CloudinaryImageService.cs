using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace JobFinder.Services.CloudImages;

public class CloudinaryImageService : ICloudImageService
{
    private readonly Cloudinary cloudinary;

    public const string CropThumb = "thumb";
    public const string CropLimit = "limit";
    public const int ThumbnailHeight = 200;
    public const int ThumbnailWidth = 200;
    public const string ImageUrl = "{0}.{1}";

    public CloudinaryImageService(Cloudinary cloudinary)
    {
        this.cloudinary = cloudinary;
    }

    public async Task<ImageUploadResult> UploadImageAsync(IFormFile imageFile)
    {
        using (Stream memoryStream = imageFile.OpenReadStream())
        {
            ImageUploadParams uploadParams = new()
            {
                File = new FileDescription(imageFile.Name, memoryStream),
                PublicId = Guid.NewGuid().ToString(),
                Transformation = new Transformation().Crop(CropLimit).Width(800).Height(600),
                EagerTransforms = [GetThumbnailTransformation()]
            };

            ImageUploadResult uploadResult = await cloudinary
                .UploadAsync(uploadParams);

            return uploadResult;
        }
    }

    public async Task DeleteImagesAsync(params string[] publicIds)
    {
        DelResParams delParams = new()
        {
            PublicIds = [.. publicIds],
            Invalidate = true
        };

        await cloudinary.DeleteResourcesAsync(delParams);
    }

    public string GetImageUrl(string publicId, string extension)
    {
        string pictureUrl = this.cloudinary
            .Api
            .UrlImgUp
            .BuildUrl(string.Format(ImageUrl, publicId, extension));

        return pictureUrl;
    }

    public string GetImageThumbnailUrl(string publicId, string extension)
    {
        string pictureUrl = cloudinary
            .Api
            .UrlImgUp
            .Transform(GetThumbnailTransformation())
            .BuildUrl(string.Format(ImageUrl, publicId, extension));

        return pictureUrl;
    }

    private static Transformation GetThumbnailTransformation()
    {
        return new Transformation()
            .Width(ThumbnailWidth)
            .Height(ThumbnailHeight)
            .Crop(CropThumb);
    }
}