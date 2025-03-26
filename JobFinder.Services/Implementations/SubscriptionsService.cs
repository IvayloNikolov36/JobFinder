namespace JobFinder.Services.Implementations
{
    using AutoMapper;
    using JobFinder.Business.JobSubscriptions;
    using JobFinder.Common.Exceptions;
    using JobFinder.Data;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.Subscriptions;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IJobAdsService jobAdsService;
        private readonly INomenclatureService nomenclatureService;
        private readonly IRepository<JobsSubscriptionEntity> jobsSubscriptionRepository;
        private readonly IJobSubscriptionsRules jobSubscriptionsRules;
        private readonly JobFinderDbContext dbContext;
        private readonly IMapper mapper;

        public SubscriptionsService(
            IJobAdsService jobAdsService,
            INomenclatureService nomenclatureService,
            IRepository<JobsSubscriptionEntity> jobsSubscriptionRepository,
            IJobSubscriptionsRules jobSubscriptionsRules,
            JobFinderDbContext dbContext,
            IMapper mapper)
        {
            this.jobAdsService = jobAdsService;
            this.nomenclatureService = nomenclatureService;
            this.jobsSubscriptionRepository = jobsSubscriptionRepository;
            this.jobSubscriptionsRules = jobSubscriptionsRules;
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<JobSubscriptionViewModel> SubscribeForJobs(string userId, JobSubscriptionCriteriasViewModel subscription)
        {
            this.jobSubscriptionsRules.ValidateJobsSubscriptionProperties(subscription);

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

        public async Task<IDictionary<string, List<JobAdsSubscriptionsViewModel>>> GetLatestJobAdsAsync(int recurringTypeId)
        {
            List<JobAdsSubscriptionsDbFunctionResult> jobAdsSubscriptions = await this.dbContext
                .GetJobAdsSubscriptionsDbFunction(recurringTypeId)
                .ToListAsync();

            if (jobAdsSubscriptions.Count == 0)
            {
                return new Dictionary<string, List<JobAdsSubscriptionsViewModel>>(0);
            }

            IEnumerable<BasicViewModel> locations = await this.nomenclatureService.GetCities();
            IEnumerable<BasicViewModel> jobCategoris = await this.nomenclatureService.GetJobCategories();
            IEnumerable<BasicViewModel> jobEngagements = await this.nomenclatureService.GetJobEngagements();

            Dictionary<string, List<JobAdsSubscriptionsViewModel>> result = [];

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

                string[] subscribers = item.SubscribersEmails.Split(["; "], StringSplitOptions.RemoveEmptyEntries);

                IEnumerable<int> jobAdsIds = latestJobAds
                    .SelectMany(x => x.JobAdsIds
                        .Split([";"], StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse));

                // TODO: create memoization for jobAds
                IEnumerable<JobAdDetailsForSubscriber> jobAds = await this.jobAdsService.GetDetails(jobAdsIds);
                string jobCategory = jobCategoris.FirstOrDefault(jc => jc.Id == item.JobCategoryId)?.Name;
                string jobEngagement = jobEngagements.FirstOrDefault(je => je.Id == item.JobEngagementId)?.Name;
                string location = locations.FirstOrDefault(l => l.Id == item.LocationId)?.Name;

                foreach (string subscriberEmail in subscribers)
                {
                    if (!result.ContainsKey(subscriberEmail))
                    {
                        result.Add(subscriberEmail, new List<JobAdsSubscriptionsViewModel>());
                    }

                    JobAdsSubscriptionsViewModel model = new()
                    {
                        JobCategory = jobCategoris.FirstOrDefault(jc => jc.Id == item.JobCategoryId)?.Name,
                        JobEngagement = jobEngagements.FirstOrDefault(je => je.Id == item.JobEngagementId)?.Name,
                        Location = locations.FirstOrDefault(l => l.Id == item.LocationId)?.Name,
                        SearchTerm = item.SearchTerm,
                        SpecifiedSalary = item.SpecifiedSalary,
                        Intership = item.Intership,
                        JobAds = jobAds
                    };

                    result[subscriberEmail].Add(model);
                }
            }

            return result;
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
