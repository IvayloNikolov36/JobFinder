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

        public async Task<bool> UnsubscribeFromJobs(int subscriptionId)
        {
            // TODO: refactor the code 

            //JobCategorySubscription subFromDb = await this.dbContext
            //    .JobCategorySubscriptions
            //    .FirstOrDefaultAsync(x => x.JobCategoryId == jobCategoryId
            //                      && x.UserId == userId
            //                      && x.Location == location);

            //if (subFromDb == null)
            //{
            //    return false;
            //}

            //this.dbContext.Remove(subFromDb);
            //await this.dbContext.SaveChangesAsync();

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
