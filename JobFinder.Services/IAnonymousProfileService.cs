using JobFinder.Web.Models.AnonymousProfile;
using JobFinder.Web.Models.CvModels;
using JobFinder.Web.Models.JobAds;

namespace JobFinder.Services;

public interface IAnonymousProfileService
{
    Task<string> Create(string cvId, string userId, AnonymousProfileCreateViewModel profile);

    Task Delete(string cvId);

    Task<AnonymousProfileDataViewModel> GetAnonymousProfile(string anonymousProfileId);

    Task<MyAnonymousProfileDataViewModel> GetMyAnonymousProfileData(string userId);

    Task<string> GetOwnerId(string id);

    Task<bool> IsRelevant(string id, JobAdCriteriasViewModel jobAdCriterias);

    Task CreateCvPreviewRequest(CvPreviewRequestCreateViewModel requestModel);

    Task<IEnumerable<CvPreviewRequestListingViewModel>> GetAllCvPreviewRequests(string userId);
}
