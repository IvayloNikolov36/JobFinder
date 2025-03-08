namespace JobFinder.Services.Implementations
{
    using JobFinder.Common.Exceptions;
    using JobFinder.Data;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IRepository<JobAdsSubscriptionsDbVewData> jobAdsSubscriptionDataRepository;
        private readonly IRepository<JobsSubscriptionEntity> jobsSubscriptionRepository;
        private readonly JobFinderDbContext dbContext;

        public SubscriptionsService(
            IRepository<JobAdsSubscriptionsDbVewData> jobAdsSubscriptionDataRepository,
            IRepository<JobsSubscriptionEntity> jobsSubscriptionRepository,
            JobFinderDbContext dbContext)
        {
            this.jobAdsSubscriptionDataRepository = jobAdsSubscriptionDataRepository;
            this.jobsSubscriptionRepository = jobsSubscriptionRepository;
            this.dbContext = dbContext;
        }

        public async Task SubscribeForJobs(string userId, int? jobCategoryId, int? locationId)
        {
            bool hasSubscription = await this.jobsSubscriptionRepository
                .AnyAsync(js => js.UserId == userId
                    && js.LocationId == locationId
                    && js.JobCategoryId == jobCategoryId);

            if (hasSubscription)
            {
                throw new ActionableException("You already have subscription for jobs with given criterias!");
            }

            JobsSubscriptionEntity sub = new()
            {
                UserId = userId,
                JobCategoryId = jobCategoryId,
                LocationId = locationId
            };

            await this.jobsSubscriptionRepository.AddAsync(sub);
            await this.jobsSubscriptionRepository.SaveChangesAsync();
        }

        public async Task UnsubscribeFromJobsWithCriterias(int subscriptionId, string userId)
        {
            JobsSubscriptionEntity subFromDb = await this.jobsSubscriptionRepository.FindAsync(subscriptionId)
                ?? throw new ActionableException("Invalid subscription id!");

            if (userId != subFromDb.UserId)
            {
                throw new UnauthorizedException("You are not allowed to remove another users' subscriptions!");
            }

            this.jobsSubscriptionRepository.Delete(subFromDb);
            await this.jobsSubscriptionRepository.SaveChangesAsync();
        }

        public async Task UnsubscribeFromAllJobsWithCriterias(string userId)
        {
            this.jobsSubscriptionRepository.DeleteWhere(js => js.UserId == userId);

            await this.jobsSubscriptionRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobSubscriptionViewModel>> GetAllJobSubscriptions(string userId)
        {
            return await this.jobsSubscriptionRepository.DbSetNoTracking()
                .Where(js => js.UserId == userId)
                .To<JobSubscriptionViewModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<JobAdsSubscriptionsViewModel>> GetLatestJobAdsAsync()
        {
            List<JobAdsSubscriptionsDbVewData> jobAdsSubscriptions = await this.jobAdsSubscriptionDataRepository
                .DbSetNoTracking()
                .ToListAsync();

            List<JobAdsSubscriptionsViewModel> result = [];

            foreach (JobAdsSubscriptionsDbVewData item in jobAdsSubscriptions)
            {
                List<LatestJobAdsDbFunctionResult> latestJobAds = await this.dbContext
                    .GetLatesJobAdsForSubscribers(item.JobCategoryId, item.LocationId)
                    .ToListAsync();

                if (latestJobAds.Count == 0)
                {
                    continue;
                }

                result.Add(new JobAdsSubscriptionsViewModel
                {
                    JobCategoryId = item.JobCategoryId,
                    JobCategory = item.JobCategory,
                    LocationId = item.LocationId,
                    Location = item.Location,
                    Subscribers = item.SubscribersEmails.Split(["; "], StringSplitOptions.RemoveEmptyEntries),
                    LatestJobAds = latestJobAds
                });
            }

            return result;
        }
    }
}
