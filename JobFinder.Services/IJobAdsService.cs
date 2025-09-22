using JobFinder.Web.Models.AnonymousProfile;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.JobAds;
using JobFinder.Web.Models.Subscriptions;

namespace JobFinder.Services
{
    public interface IJobAdsService
    {
        Task<JobAdDetailsViewModel> Get(int id);

        Task<IdentityViewModel<int>> Create(JobAdCreateViewModel jobAd, string userId);

        Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(
            string currentUserId,
            int? lifecycleStatus = null);

        Task<DataListingsModel<JobListingViewModel>> AllActiveAsync(JobAdsFilterModel model);

        Task Update(int jobAdId, JobAdEditModel editModel);

        Task<JobAdDetailsForSubscriber> GetDetails(int jobAdId);

        Task DeactivateAds();

        Task<string> GetPublisherId(int jobAdId);

        Task<IEnumerable<AnonymousProfileListingViewModel>> GetRelevantAnonymousProfiles(int jobAdId);

        Task<JobAdCriteriasViewModel> GetJobAdCriterias(int jobAdId);

        Task Retire(int id);
    }
}
