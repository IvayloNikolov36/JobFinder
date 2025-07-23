using AutoMapper;
using JobFinder.Business.JobSubscriptions;
using JobFinder.Common.Exceptions;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.Subscriptions;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IJobAdsService jobAdsService;
        private readonly INomenclatureService nomenclatureService;
        private readonly IJobSubscriptionsRules jobSubscriptionsRules;
        private readonly IDistributedCache distributedCache;

        public SubscriptionsService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            IJobAdsService jobAdsService,
            INomenclatureService nomenclatureService,
            IJobSubscriptionsRules jobSubscriptionsRules,
            IDistributedCache distributedCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.jobAdsService = jobAdsService;
            this.nomenclatureService = nomenclatureService;
            this.jobSubscriptionsRules = jobSubscriptionsRules;
            this.distributedCache = distributedCache;
        }

        public async Task<JobSubscriptionViewModel> SubscribeForJobs(
            string userId,
            JobSubscriptionCriteriasViewModel subscription)
        {
            JobSubscriptionCriteriasDTO subscriptionDto = this.mapper
                .Map<JobSubscriptionCriteriasDTO>(subscription);

            subscriptionDto.UserId = userId;

            this.jobSubscriptionsRules.ValidateJobsSubscriptionProperties(subscriptionDto);

            bool hasSuchSubscription = await this.unitOfWork
                .JobAdSubscriptionsRepository
                .Any(subscriptionDto);

            if (hasSuchSubscription)
            {
                throw new ActionableException("You already have subscription for jobs with given criterias!");
            }

            await this.unitOfWork.JobAdSubscriptionsRepository
                .Add(subscriptionDto);

            await this.unitOfWork
                .SaveChanges<JobSubscriptionCriteriasDTO, int>(subscriptionDto);

            await this.RemoveJobSubscriptionsCache(userId);

            JobSubscriptionDTO subscriptionDetails = await this.unitOfWork.JobAdSubscriptionsRepository
                .GetDetails(subscriptionDto.Id);

            return this.mapper.Map<JobSubscriptionViewModel>(subscriptionDetails);
        }

        public async Task UnsubscribeFromJobsWithCriterias(int subscriptionId, string userId)
        {
            await this.unitOfWork.JobAdSubscriptionsRepository.Delete(subscriptionId, userId);
            await this.unitOfWork.SaveChanges();

            await this.RemoveJobSubscriptionsCache(userId);
        }

        public async Task UnsubscribeFromAllJobsWithCriterias(string userId)
        {
            this.unitOfWork.JobAdSubscriptionsRepository.DeleteAll(userId);
            await this.unitOfWork.SaveChanges();

            await this.RemoveJobSubscriptionsCache(userId);
        }

        public async Task<IEnumerable<JobSubscriptionViewModel>> GetAllJobSubscriptions(string userId)
        {
            string cacheKey = string.Format(JobSubscriptionsCacheKey, userId);
            byte[] subscriptionsCache = await this.distributedCache.GetAsync(cacheKey);

            if (subscriptionsCache == null)
            {
                IEnumerable<JobSubscriptionDTO> subscriptions = await this.unitOfWork
                    .JobAdSubscriptionsRepository
                    .GetAll(userId);

                var subscriptionModels = this.mapper
                    .Map<IEnumerable<JobSubscriptionViewModel>>(subscriptions);

                string serializedData = JsonSerializer.Serialize(subscriptionModels);

                await this.distributedCache
                    .SetAsync(cacheKey, Encoding.UTF8.GetBytes(serializedData));

                return subscriptionModels;
            }

            return JsonSerializer
                .Deserialize<IEnumerable<JobSubscriptionViewModel>>(
                    Encoding.UTF8.GetString(subscriptionsCache));
        }

        public async Task<IDictionary<string, List<JobAdsSubscriptionsViewModel>>> GetLatestJobAdsAsync(
            int recurringTypeId)
        {
            IEnumerable<JobAdsSubscriptionDTO> jobAdsSubscriptions = await this.unitOfWork
                .JobAdSubscriptionsRepository
                .GetAll(recurringTypeId);

            if (!jobAdsSubscriptions.Any())
            {
                return new Dictionary<string, List<JobAdsSubscriptionsViewModel>>(0);
            }

            IEnumerable<BasicViewModel> locations = await this.nomenclatureService.GetCities();
            IEnumerable<BasicViewModel> jobCategories = await this.nomenclatureService.GetJobCategories();
            IEnumerable<BasicViewModel> jobEngagements = await this.nomenclatureService.GetJobEngagements();

            Dictionary<string, List<JobAdsSubscriptionsViewModel>> result = [];
            Dictionary<int, JobAdDetailsForSubscriber> jobDetailsById = [];

            foreach (JobAdsSubscriptionDTO item in jobAdsSubscriptions)
            {
                IEnumerable<LatestJobAdsDTO> latestJobAds = await this.unitOfWork
                    .JobAdSubscriptionsRepository
                    .GetLatestAdsForSubscriptions(recurringTypeId, item);

                if (!latestJobAds.Any())
                {
                    continue;
                }

                string[] subscribers = item.SubscribersEmails.Split(["; "], StringSplitOptions.RemoveEmptyEntries);

                int[] jobAdsIds = latestJobAds
                    .SelectMany(x => x.JobAdsIds
                        .Split([";"], StringSplitOptions.RemoveEmptyEntries)
                        .Select(int.Parse))
                    .ToArray();

                foreach (string subscriberEmail in subscribers)
                {
                    if (!result.ContainsKey(subscriberEmail))
                    {
                        result.Add(subscriberEmail, new List<JobAdsSubscriptionsViewModel>());
                    }

                    JobAdsSubscriptionsViewModel model = new()
                    {
                        JobCategory = jobCategories.FirstOrDefault(jc => jc.Id == item.JobCategoryId)?.Name,
                        JobEngagement = jobEngagements.FirstOrDefault(je => je.Id == item.JobEngagementId)?.Name,
                        Location = locations.FirstOrDefault(l => l.Id == item.LocationId)?.Name,
                        SearchTerm = item.SearchTerm,
                        SpecifiedSalary = item.SpecifiedSalary,
                        Intership = item.Intership,
                        JobAds = await this.GetJobAdsDetails(jobAdsIds, jobDetailsById)
                    };

                    result[subscriberEmail].Add(model);
                }
            }

            return result;
        }

        private async Task<List<JobAdDetailsForSubscriber>> GetJobAdsDetails(
            int[] jobAdsIds,
            Dictionary<int,
                JobAdDetailsForSubscriber> jobDetailsById)
        {
            List<JobAdDetailsForSubscriber> jobAdsDetails = new(jobAdsIds.Length);

            foreach (int adId in jobAdsIds)
            {
                JobAdDetailsForSubscriber jobAdDetails;

                if (!jobDetailsById.ContainsKey(adId))
                {
                    jobAdDetails = await this.jobAdsService.GetDetails(adId);
                    jobDetailsById.Add(adId, jobAdDetails);
                }
                else
                {
                    jobAdDetails = jobDetailsById[adId];
                }

                jobAdsDetails.Add(jobAdDetails);
            }

            return jobAdsDetails;
        }

        private async Task RemoveJobSubscriptionsCache(string userId)
        {
            string subscriptionsCacheKey = string.Format(JobSubscriptionsCacheKey, userId);
            await this.distributedCache.RemoveAsync(subscriptionsCacheKey);
        }
    }
}
