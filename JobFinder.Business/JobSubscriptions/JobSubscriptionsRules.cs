using JobFinder.Common.Exceptions;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;

namespace JobFinder.Business.JobSubscriptions
{
    public class JobsubscriptionsRules : IJobSubscriptionsRules
    {
        public void ValidateJobsSubscriptionProperties(JobSubscriptionCriteriasViewModel subscription)
        {
            bool hasAnyCriteriaSpecified = subscription.JobCategoryId.HasValue
                || subscription.JobEngagementId.HasValue
                || subscription.LocationId.HasValue
                || subscription.Intership
                || subscription.SpecifiedSalary
                || !string.IsNullOrEmpty(subscription.SearchTerm);

            if (!hasAnyCriteriaSpecified)
            {
                throw new ActionableException("No criterias specified for a subscription!");
            }
        }
    }
}
