namespace JobFinder.Services.Implementations
{
    using JobFinder.Data;
    using JobFinder.Data.Models.Nomenclature;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly JobFinderDbContext dbContext;

        public SubscriptionsService(JobFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> SubscribeToJobCategoryAsync(int jobCategoryId, string userId, string location)
        {
            JobCategoryEntity jobCategory = await this.dbContext.FindAsync<JobCategoryEntity>(jobCategoryId);
            if (jobCategory == null)
            {
                return false;
            }

            JobCategorySubscription sub = new()
            {
                UserId = userId,
                JobCategoryId = jobCategoryId,
                Location = location
            };

            await this.dbContext.AddAsync(sub);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnsubscribeFromJobCategoryAsync(int jobCategoryId, string userId, string location)
        {
            JobCategorySubscription subFromDb = await this.dbContext
                .JobCategorySubscriptions
                .FirstOrDefaultAsync(x => x.JobCategoryId == jobCategoryId
                                  && x.UserId == userId
                                  && x.Location == location);

            if (subFromDb == null)
            {
                return false;
            }

            this.dbContext.Remove(subFromDb);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<JobAdsByCategoryAndLocationViewModel>> GetNewJobAdsByCategoryAsync()
        {
            List<JobCategoriesSubscriptionsData> jobCategoriesSubs = await this.dbContext.JobCategoriesSubscriptionsData
                .AsNoTracking()
                .ToListAsync();

            List<JobAdsByCategoryAndLocationViewModel> result = new();

            foreach (JobCategoriesSubscriptionsData item in jobCategoriesSubs)
            {
                List<LatestCompanyJobAds> latestCompanyJobAds = await this.dbContext
                    .GetLatesJobAdsForSubscribers(item.JobCategoryId, item.Location)
                    .ToListAsync();

                if (latestCompanyJobAds.Count == 0)
                {
                    continue;
                }

                result.Add(new JobAdsByCategoryAndLocationViewModel
                {
                    JobCategoryId = item.JobCategoryId,
                    JobCategory = item.JobCategory,
                    Location = item.Location,
                    Subscribers = item.Subscribers.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries),
                    LatestCompanyJobAds = latestCompanyJobAds
                });
            }

            return result;
        }
    }
}
