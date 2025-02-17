namespace JobFinder.Services
{
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISubscriptionsService
    {
        Task SubscribeForJobs(string userId, int? jobCategoryId, string location);

        Task UnsubscribeFromJobsWithCriterias(int subscriptionId, string userId);

        Task UnsubscribeFromAllJobsWithCriterias(string userId);

        Task<IEnumerable<JobSubscriptionViewModel>> GetAllJobSubscriptions(string userId);

        Task<IEnumerable<JobAdsByCategoryAndLocationViewModel>> GetNewJobAdsByCategoryAsync();
    }
}
