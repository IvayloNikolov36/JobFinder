namespace JobFinder.Services
{
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubscriptionsService
    {
        Task SubscribeForJobs(string userId, int? jobCategoryId, string location);

        Task UnsubscribeFromJobs(int subscriptionId, string userId);

        Task<IEnumerable<JobAdsByCategoryAndLocationViewModel>> GetNewJobAdsByCategoryAsync();
    }
}
