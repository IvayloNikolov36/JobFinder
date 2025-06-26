using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.DataAccess.Contracts;

public interface ICvPreviewRequestRepository
{
    Task CreateRequest(CvPreviewRequestDTO request);
}
