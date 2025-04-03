using JobFinder.Common.Exceptions;
using JobFinder.Transfer.DTOs;
using static JobFinder.Common.MessageConstants;

namespace JobFinder.Business.JobSubscriptions
{
    public class JobSubscriptionsRules : IJobSubscriptionsRules
    {
        public void ValidateJobsSubscriptionProperties(JobSubscriptionCriteriasDTO criteriasDto)
        {
            bool hasAnyCriteriaSpecified = criteriasDto.JobCategoryId.HasValue
                || criteriasDto.JobEngagementId.HasValue
                || criteriasDto.LocationId.HasValue
                || criteriasDto.Intership
                || criteriasDto.SpecifiedSalary
                || !string.IsNullOrEmpty(criteriasDto.SearchTerm?.Trim());

            if (!hasAnyCriteriaSpecified)
            {
                throw new ActionableException(NoSubscriptionCriterias);
            }
        }
    }
}
