using JobFinder.Data.Models.Cv;
using JobFinder.Web.Models.CvModels;

namespace JobFinder.Services;

public interface ICvPreviewRequestService
{
    Task<IEnumerable<CvPreviewRequestListingViewModel>> GetAllCvPreviewRequests(string userId);

    Task MakeCvPreviewRequest(CvPreviewRequestCreateViewModel requestModel, string currentUserId);

    Task AllowCvPreview(int id, string currentUserId);

    Task<IEnumerable<CompanyCvPreviewRequestListingViewModel>> GetAllCompanyCvPreviewRequests(string userId);
}
