using JobFinder.Data.Models.ViewsModels;
using JobFinder.Transfer.DTOs;

namespace JobFinder.DataAccess.Contracts
{
    public interface ICompanySubscriptionsRepository
    {
        Task<IEnumerable<CompanyJobAdsForSubscribersDTO>> GetCompanyAdsBySubscriptions();

        Task<IEnumerable<CompanySubscriptionDTO>> GetMySubscriptions(string userId);

        Task Subscribe(string userId, int companyId);

        Task Delete(string userId, int companyId);
        
        void DeleteSubscriptions(string userId);

        Task<bool> Exist(string userId, int companyId);
    }
}
