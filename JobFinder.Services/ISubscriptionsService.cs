namespace JobFinder.Services
{
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubscriptionsService
    {
        Task<bool> SubscribeToCompanyAsync(int companyId, string userId);

        Task<bool> UnsubscribeFromCompanyAsync(int companyId, string userId);

        Task<bool> SubscribeToJobCategoryAsync(int jobCategoryId, string userId, string location);

        Task<bool> UnsubscribeFromJobCategoryAsync(int jobCategoryId, string userId, string location);

        Task<List<CompaniesSubscriptionsData>> GetCompaniesNewJobAdsAsync();

        Task<List<JobAdsByCategoryAndLocationViewModel>> GetNewJobAdsByCategoryAsync();

    }
}
