namespace JobFinder.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJobAdsService
    {
        Task<T> GetAsync<T>(int id);

        Task CreateAsync(int companyId, string position, string description, int jobCategoryId, 
            int jobEngagementId, int? minSalary, int? maxSalary, string location);

        Task<(int, IEnumerable<T>)> AllAsync<T>(
            int page, int items, string searchText = null, int jobEngagementId = 0, int jobCategoryId = 0, 
            string location = null, string sortBy = null, bool? isAscending = null);

        Task<bool> EditAsync(int jobAdId, string userId, string position, string description);

    }
}
