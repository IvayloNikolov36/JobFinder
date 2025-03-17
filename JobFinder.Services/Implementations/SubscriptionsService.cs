namespace JobFinder.Services.Implementations
{
    using AutoMapper;
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
        private readonly IRepository<JobAdsSubscriptionsDbFunctionResult> jobAdsSubscriptionDataRepository;
        private readonly IRepository<JobsSubscriptionEntity> jobsSubscriptionRepository;
        private readonly JobFinderDbContext dbContext;
        private readonly IMapper mapper;

        public SubscriptionsService(
            IRepository<JobAdsSubscriptionsDbFunctionResult> jobAdsSubscriptionDataRepository,
            IRepository<JobsSubscriptionEntity> jobsSubscriptionRepository,
            JobFinderDbContext dbContext,
            IMapper mapper)
        {
            this.jobAdsSubscriptionDataRepository = jobAdsSubscriptionDataRepository;
            this.jobsSubscriptionRepository = jobsSubscriptionRepository;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<JobSubscriptionViewModel> SubscribeForJobs(string userId, JobSubscriptionCriteriasViewModel subscription)
        {
            if (subscription.SearchTerm?.Trim() == string.Empty)
            {
                subscription.SearchTerm = null;
            }

            this.ValidateJobsSubscriptionProperties(subscription);

            bool hasSuchSubscription = await this.HasSubscriptionWithSameCriterias(userId, subscription);

            if (hasSuchSubscription)
            {
                throw new ActionableException("You already have subscription for jobs with given criterias!");
            }

            JobsSubscriptionEntity subscriptionEntity = this.mapper.Map<JobsSubscriptionEntity>(subscription);
            subscriptionEntity.UserId = userId;

            await this.jobsSubscriptionRepository.AddAsync(subscriptionEntity);

            await this.jobsSubscriptionRepository.SaveChangesAsync();

            JobSubscriptionViewModel result = await this.jobsSubscriptionRepository.DbSetNoTracking()
                .Where(x => x.Id == subscriptionEntity.Id)
                .To<JobSubscriptionViewModel>()
                .SingleOrDefaultAsync();

            return result;
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

        public async Task<IEnumerable<JobAdsSubscriptionsViewModel>> GetLatestJobAdsAsync(int recurringTypeId)
        {
            List<JobAdsSubscriptionsDbFunctionResult> jobAdsSubscriptions = await this.dbContext
                .GetJobAdsSubscriptionsDbFunction(recurringTypeId)
                .ToListAsync();

            if (jobAdsSubscriptions.Count == 0)
            {
                return [];
            }

            List<JobAdsSubscriptionsViewModel> result = [];
          
            foreach (JobAdsSubscriptionsDbFunctionResult item in jobAdsSubscriptions)
            {
                List<LatestJobAdsDbFunctionResult> latestJobAds = await this.dbContext
                    .GetLatesJobAdsForSubscribersDbFunction(
                        recurringTypeId,
                        item.JobCategoryId,
                        item.JobEngagementId,
                        item.LocationId,
                        item.SearchTerm,
                        item.Intership,
                        item.SpecifiedSalary)
                    .ToListAsync();

                if (latestJobAds.Count == 0)
                {
                    continue;
                }

                result.Add(new JobAdsSubscriptionsViewModel
                {
                    JobCategoryId = item.JobCategoryId,
                    LocationId = item.LocationId,
                    Subscribers = item.SubscribersEmails.Split(["; "], StringSplitOptions.RemoveEmptyEntries),
                    LatestJobAds = latestJobAds
                });
            }

            return result;
        }

        // TODO: candidate for a Business Rule
        private void ValidateJobsSubscriptionProperties(JobSubscriptionCriteriasViewModel subscription)
        {
            bool hasAnyCriteriaSpecified = subscription.JobCategoryId.HasValue
                || subscription.JobEngagementId.HasValue
                || subscription.LocationId.HasValue
                || subscription.Intership
                || subscription.SpecifiedSalary
                || !string.IsNullOrEmpty(subscription.SearchTerm);

            if (!hasAnyCriteriaSpecified)
            {
                throw new ActionableException("No criterias specified for a subscription!");
            }
        }

        private async Task<bool> HasSubscriptionWithSameCriterias(string userId, JobSubscriptionCriteriasViewModel subscription)
        {
            string trimmedSearchTerm = subscription.SearchTerm?.Trim();
            string search = trimmedSearchTerm == string.Empty ? null : trimmedSearchTerm;

            return await this.jobsSubscriptionRepository
                .AnyAsync(js => js.UserId == userId
                    && js.JobCategoryId == subscription.JobCategoryId
                    && js.JobEngagementId == subscription.JobEngagementId
                    && js.LocationId == subscription.LocationId
                    && js.Intership == subscription.Intership
                    && js.SpecifiedSalary == subscription.SpecifiedSalary
                    && js.SearchTerm == search);
        }
    }
}
