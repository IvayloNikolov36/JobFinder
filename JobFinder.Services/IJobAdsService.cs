namespace JobFinder.Services
{
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.JobAds;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJobAdsService
    {
        Task<T> GetAsync<T>(int id);

        Task CreateAsync(int companyId, JobAdCreateModel model);

        Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(string userId);

        Task<DataListingsModel<JobListingModel>> AllAsync(JobAdsFilterModel model);

        Task EditAsync(int jobAdId, string userId, JobAdEditModel editModel);
    }
}
