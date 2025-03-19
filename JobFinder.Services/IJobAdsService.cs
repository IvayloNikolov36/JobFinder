namespace JobFinder.Services
{
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.JobAds;
    using JobFinder.Web.Models.Subscriptions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJobAdsService
    {
        Task<T> GetAsync<T>(int id);

        Task CreateAsync(int companyId, JobAdCreateModel model);

        Task<IEnumerable<CompanyJobAdViewModel>> GetAllCompanyAds(string userId);

        Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(string currentUserId, bool active);

        Task<DataListingsModel<JobListingModel>> AllActiveAsync(JobAdsFilterModel model);

        Task EditAsync(int jobAdId, string userId, JobAdEditModel editModel);

        Task<IEnumerable<JobAdDetailsForSubscriber>> GetDetails(IEnumerable<int> ids);

        Task DeactivateAds();     
    }
}
