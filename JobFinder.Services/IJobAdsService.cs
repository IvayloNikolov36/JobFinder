namespace JobFinder.Services
{
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.JobAds;
    using System.Threading.Tasks;

    public interface IJobAdsService
    {
        Task<T> GetAsync<T>(int id);

        Task CreateAsync(int companyId, JobAdCreateModel model);

        Task<DataListingsModel<JobListingModel>> AllAsync(JobAdsParams paramsModel);

        Task<bool> EditAsync(int jobAdId, string userId, string position, string description);

    }
}
