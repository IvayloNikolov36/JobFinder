using JobFinder.Common.Exceptions;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Business.JobSubscriptions
{
    public class JobSubscriptionsRules : IJobSubscriptionsRules
    {
        public void ValidateJobsSubscriptionProperties(JobSubscriptionCriteriasViewModel subscription)
        {
            bool hasAnyCriteriaSpecified = subscription.JobCategoryId.HasValue
                || subscription.JobEngagementId.HasValue
                || subscription.LocationId.HasValue
                || subscription.Intership
                || subscription.SpecifiedSalary
                || !string.IsNullOrEmpty(subscription.SearchTerm?.Trim());

            if (!hasAnyCriteriaSpecified)
            {
                throw new ActionableException(NoSubscriptionCriterias);
            }
        }
    }
}
