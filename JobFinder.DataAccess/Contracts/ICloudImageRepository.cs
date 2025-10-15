using JobFinder.Transfer.DTOs;

namespace JobFinder.DataAccess.Contracts;

public interface ICloudImageRepository
{
    Task Add(CloudImageDTO image);

    Task<string> GetUrl(int pictureId);

    Task<string> GetThumbnailUrl(int pictureId);
}
