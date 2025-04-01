using JobFinder.Data.Models.ViewsModels;
using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;

namespace JobFinder.DataAccess.Contracts
{
    public interface ICompanySubscriptionsRepository
    {
        // TODO: create all to work with DTOs instead of view models

        Task<IEnumerable<CompanyJobAdsForSubscribersViewData>> GetCompanyAdsBySubscriptions();

        Task<IEnumerable<CompanySubscriptionViewModel>> GetMySubscriptions(string userId);

        Task Subscribe(string userId, int companyId);

        Task Delete(string userId, int companyId);
        
        void DeleteSubscriptions(string userId);

        Task<bool> Exist(string userId, int companyId);
    }
}
