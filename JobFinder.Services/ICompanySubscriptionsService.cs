namespace JobFinder.Services
{
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanySubscriptionsService
    {
        Task<IEnumerable<CompanyJobAdsForSubscribersViewData>> GetLatesJobAds();

        Task Subscribe(int companyId, string userId);

        Task Unsubscribe(int companyId, string userId);

        Task UnsubscribeAll(string userId);

        Task<IEnumerable<CompanySubscriptionViewModel>> GetMySubscriptions(string userId);
    }
}
