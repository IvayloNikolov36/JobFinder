namespace JobFinder.Services
{
    using System.Threading.Tasks;

    public interface ISubscriptionsService
    {
        Task<bool> SubscribeToCompanyAsync(int companyId, string userId);

        Task<bool> UnsubscribeFromCompanyAsync(int companyId, string userId);

        Task<bool> SubscribeToJobCategoryAsync(int jobCategoryId, string userId);

        Task<bool> UnsubscribeFromJobCategoryAsync(int jobCategoryId, string userId);
    }
}
