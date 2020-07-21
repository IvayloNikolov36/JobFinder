namespace JobFinder.Services
{
    using JobFinder.Data.Models.ViewsModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanySubscriptionsService
    {
        Task<List<CompaniesSubscriptionsData>> GetLatesJobAdsAsync();

        Task<bool> SubscribeAsync(int companyId, string userId);

        Task<bool> UnsubscribeAsync(int companyId, string userId);
    }
}
