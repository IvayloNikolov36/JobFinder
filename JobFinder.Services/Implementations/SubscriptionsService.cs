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
        private readonly IRepository<JobsSubscription> jobsSubscriptionRepository;
        private readonly IRepository<JobCategoriesSubscriptionsData> jobCategoriesSubscriptionDataRepository;
        private readonly JobFinderDbContext dbContext;

        public SubscriptionsService(
            IRepository<JobsSubscription> jobsSubscriptionRepository,
            IRepository<JobCategoriesSubscriptionsData> jobCategoriesSubscriptionDataRepository,
            JobFinderDbContext dbContext)
        {
            this.jobsSubscriptionRepository = jobsSubscriptionRepository;
            this.jobCategoriesSubscriptionDataRepository = jobCategoriesSubscriptionDataRepository;
            this.dbContext = dbContext;
        }

        public async Task SubscribeForJobs(string userId, int? jobCategoryId, string location)
        {
            bool hasSubscription = await this.jobsSubscriptionRepository
                .ExistAsync(js => js.UserId == userId
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

            await this.jobsSubscriptionRepository.AddAsync(sub);
            await this.jobsSubscriptionRepository.SaveChangesAsync();
        }

        public async Task UnsubscribeFromJobsWithCriterias(int subscriptionId, string userId)
        {
            JobsSubscription subFromDb = await this.jobsSubscriptionRepository.FindAsync(subscriptionId)
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
            return await this.jobsSubscriptionRepository.AllAsNoTracking()
                .Where(js => js.UserId == userId)
                .To<JobSubscriptionViewModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<JobAdsByCategoryAndLocationViewModel>> GetNewJobAdsByCategoryAsync()
        {
            List<JobCategoriesSubscriptionsData> jobCategoriesSubs = await this.jobCategoriesSubscriptionDataRepository
                .AllAsNoTracking()
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
