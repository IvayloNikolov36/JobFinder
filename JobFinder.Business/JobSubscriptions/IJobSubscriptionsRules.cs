using JobFinder.Transfer.DTOs;

namespace JobFinder.Business.JobSubscriptions
{
    public interface IJobSubscriptionsRules
    {
        void ValidateJobsSubscriptionProperties(JobSubscriptionCriteriasDTO subscription);
    }
}
