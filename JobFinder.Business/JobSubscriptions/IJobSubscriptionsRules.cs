using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;

namespace JobFinder.Business.JobSubscriptions
{
    public interface IJobSubscriptionsRules
    {
        void ValidateJobsSubscriptionProperties(JobSubscriptionCriteriasViewModel subscription);
    }
}
