namespace JobFinder.Services
{
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanySubscriptionsService
    {
        Task<IEnumerable<CompaniesSubscriptionsFunctionResult>> GetLatesJobAdsAsync(int recurringTypeId);

        Task SubscribeAsync(int companyId, string userId);

        Task UnsubscribeAsync(int companyId, string userId);

        Task UnsubscribeAllAsync(string userId);

        Task<IEnumerable<CompanySubscriptionViewModel>> GetMySubscriptions(string userId);
    }
}
