namespace JobFinder.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IJobAdsService
    {
        Task<T> GetAsync<T>(int id);

        Task<T> DetailsAsync<T>(int jobId);

        Task CreateAsync(string publisherId, string position, string description, int jobCategoryId, 
            int jobEngagementId, int? minSalary, int? maxSalary, string location);

        Task<(int, IEnumerable<T>)> AllAsync<T>(
            int page, int items, string searchText = null, int? jobEngagementId = null, int? jobCategoryId = null, 
            string location = null, string sortBy = null, bool? isAscending = null);

        Task<bool> EditAsync(int jobAdId, string userId, string position, string description);

        Task<IEnumerable<T>> GetJobEngagements<T>();

        Task<IEnumerable<T>> GetJobCategories<T>();
    }
}
