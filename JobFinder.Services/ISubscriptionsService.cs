namespace JobFinder.Services
{
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubscriptionsService
    {
        Task<bool> SubscribeToJobCategoryAsync(int jobCategoryId, string userId, string location);

        Task<bool> UnsubscribeFromJobCategoryAsync(int jobCategoryId, string userId, string location);

        Task<IEnumerable<JobAdsByCategoryAndLocationViewModel>> GetNewJobAdsByCategoryAsync();
    }
}
