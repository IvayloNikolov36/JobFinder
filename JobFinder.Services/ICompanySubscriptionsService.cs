namespace JobFinder.Services
{
    using JobFinder.Data.Models.ViewsModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICompanySubscriptionsService
    {
        Task<IEnumerable<CompaniesSubscriptionsData>> GetLatesJobAdsAsync();

        Task SubscribeAsync(int companyId, string userId);

        Task UnsubscribeAsync(int companyId, string userId);
    }
}
