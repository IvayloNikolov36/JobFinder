using JobFinder.Data.Models.Cv;

namespace JobFinder.Services;

public interface ICvPreviewRequestService
{
    Task AllowCvPreview(int id, string currentUserId);

    Task<IEnumerable<CompanyCvPreviewRequestListingViewModel>> GetAllCompanyCvPreviewRequests(string userId);
}
