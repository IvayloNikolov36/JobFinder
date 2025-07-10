using JobFinder.Transfer.DTOs.Cv.CvPreviewRequest;

namespace JobFinder.DataAccess.Contracts;

public interface ICvPreviewRequestRepository
{
    Task<IEnumerable<CvPreviewRequestListingDTO>> GetAllCvPreviewRequests(string userId);

    Task MakeRequest(CvPreviewRequestDTO request);

    Task AcceptRequest(int id);

    Task<CvPreviewRequestAcceptDataDTO> GetCvRequestAcceptData(int cvRequestId);

    Task<IEnumerable<CompanyCvPreviewRequestListingDTO>> GetCompanyCvRequests(string userId);

    Task<string> GetRequesterId(int cvRequestId);

    void DeleteAll(string cvId);
}
