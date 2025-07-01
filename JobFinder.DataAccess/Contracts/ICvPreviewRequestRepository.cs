using JobFinder.Transfer.DTOs.Cv;

namespace JobFinder.DataAccess.Contracts;

public interface ICvPreviewRequestRepository
{
    Task MakeRequest(CvPreviewRequestDTO request);

    Task AcceptRequest(int id);

    Task<CvPreviewRequestAcceptDataDTO> GetCvRequestAcceptData(int cvRequestId);
}
