using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;

namespace JobFinder.Services;

public interface ICompanySubscriptionsService
{
    Task<IEnumerable<CompanyJobAdsForSubscribersViewModel>> GetLatesJobAds();

    Task Subscribe(int companyId, string userId);

    Task Unsubscribe(int companyId, string userId);

    Task UnsubscribeAll(string userId);

    Task<IEnumerable<CompanySubscriptionViewModel>> GetMySubscriptions(string userId);
}
