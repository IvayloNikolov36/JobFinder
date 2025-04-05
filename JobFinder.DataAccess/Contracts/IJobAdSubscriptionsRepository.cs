using JobFinder.Transfer.DTOs;

namespace JobFinder.DataAccess.Contracts
{
    public interface IJobAdSubscriptionsRepository
    {
        Task<IEnumerable<JobAdsSubscriptionDTO>> GetAll(int recurringTypeId);

        Task<IEnumerable<JobSubscriptionDTO>> GetAll(string userId);

        Task<IEnumerable<LatestJobAdsDTO>> GetLatestAdsForSubscriptions(
            int recurringTypeId,
            JobAdsSubscriptionDTO subscriptionCriterias);

        Task<bool> Any(JobSubscriptionCriteriasDTO subscription);

        void DeleteAll(string userId);

        Task Delete(int subscriptionId, string userId);

        Task Add(JobSubscriptionCriteriasDTO subscription);

        Task<JobSubscriptionDTO> GetDetails(int subscriptionId);
    }
}
