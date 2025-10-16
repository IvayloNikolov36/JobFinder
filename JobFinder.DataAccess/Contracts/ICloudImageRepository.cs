using JobFinder.Transfer.DTOs;

namespace JobFinder.DataAccess.Contracts;

public interface ICloudImageRepository
{
    Task Add(CloudImageDTO image);

    Task Update(string userId, CloudImageDTO imageDto);

    Task<string> GetUrl(int pictureId);

    Task<string> GetThumbnailUrl(int pictureId);
}
