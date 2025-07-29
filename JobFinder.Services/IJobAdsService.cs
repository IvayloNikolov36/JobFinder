using JobFinder.Web.Models.AnonymousProfile;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.JobAds;
using JobFinder.Web.Models.Subscriptions;

namespace JobFinder.Services
{
    public interface IJobAdsService
    {
        Task<JobAdDetailsViewModel> Get(int id);

        Task<int> Create(JobAdCreateViewModel jobAd, int companyId);

        Task<IEnumerable<CompanyJobAdViewModel>> GetAllCompanyAds(string userId);

        Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(string currentUserId, bool active);

        Task<DataListingsModel<JobListingModel>> AllActiveAsync(JobAdsFilterModel model);

        Task Update(int jobAdId, string userId, JobAdEditModel editModel);

        Task<JobAdDetailsForSubscriber> GetDetails(int jobAdId);

        Task DeactivateAds();

        Task<string> GetPublisherId(int jobAdId);

        Task<IEnumerable<AnonymousProfileListingViewModel>> GetRelevantAnonymousProfiles(int jobAdId);

        Task<JobAdCriteriasViewModel> GetJobAdCriterias(int jobAdId);
    }
}
