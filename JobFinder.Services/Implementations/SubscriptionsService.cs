namespace JobFinder.Services.Implementations
{
    using JobFinder.Common.Exceptions;
    using JobFinder.Data;
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

        // TODO: refactor all methods to use repository methods for db calls

        public async Task SubscribeForJobs(string userId, int? jobCategoryId, string location)
        {
            bool hasSubscription = await this.dbContext.JobsSubscriptions
                .AnyAsync(js => js.UserId == userId
                    && js.Location == location
                    && js.JobCategoryId == jobCategoryId);

            if (hasSubscription)
            {
                throw new ActionableException("You already have subscription for jobs with given criterias!");
            }

            JobsSubscription sub = new()
            {
                UserId = userId,
                JobCategoryId = jobCategoryId,
                Location = location
            };

            await this.dbContext.AddAsync(sub);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UnsubscribeFromJobs(int subscriptionId, string userId)
        {
            JobsSubscription subFromDb = await this.dbContext.FindAsync<JobsSubscription>(subscriptionId)
                ?? throw new ActionableException("Invalid subscription id!");

            if (userId != subFromDb.UserId)
            {
                throw new UnauthorizedException("You are not allowed to remove another users' subscriptions!");
            }

            this.dbContext.Remove(subFromDb);
            await this.dbContext.SaveChangesAsync();
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
