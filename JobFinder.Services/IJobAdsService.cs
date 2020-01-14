using JobFinder.Data.Models;
using JobFinder.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JobFinder.Services
{
    public interface IJobAdsService
    {
        Task<JobAd> GetAsync(int id);

        Task CreateAsync(string publisherId, string position, string description, DateTime expiration, int jobCategoryId, int jobEngagementId, int? minSalary, int? maxSalary);

        Task<IEnumerable<JobAdsListingServiceModel>> AllAsync();

        Task EditAsync(int offerId, string position, string description, int daysActive);

        Task<IDictionary<int, string>> GetJobEngagements();

        Task<IDictionary<int, string>> GetJobCategories();
    }
}
