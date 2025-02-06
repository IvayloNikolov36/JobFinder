namespace JobFinder.Services
{
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubscriptionsService
    {
        Task<bool> SubscribeForJobs(string userId, int? jobCategoryId, string location);

        Task<bool> UnsubscribeFromJobs(int subscriptionId);

        Task<IEnumerable<JobAdsByCategoryAndLocationViewModel>> GetNewJobAdsByCategoryAsync();
    }
}
