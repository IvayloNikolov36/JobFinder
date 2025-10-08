using JobFinder.Transfer.DTOs;

namespace JobFinder.DataAccess.Contracts;

public interface ICloudImageRepository
{
    Task Add(CloudImageDTO image);
}
