using AutoMapper;
using JobFinder.Business.JobSubscriptions;
using JobFinder.Common.Exceptions;
using JobFinder.Data.Models.Subscriptions;
using JobFinder.Data.Models.ViewsModels;
using JobFinder.DataAccess.Generic;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.Subscriptions;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;

namespace JobFinder.Services.Implementations
{
    public class SubscriptionsService : ISubscriptionsService
    {
        private readonly IJobAdsService jobAdsService;
        private readonly INomenclatureService nomenclatureService;
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IJobSubscriptionsRules jobSubscriptionsRules;
        private readonly IMapper mapper;

        public SubscriptionsService(
            IJobAdsService jobAdsService,
            INomenclatureService nomenclatureService,
            IRepository<JobsSubscriptionEntity> jobsSubscriptionRepository,
            IEntityFrameworkUnitOfWork unitOfWork,
            IJobSubscriptionsRules jobSubscriptionsRules,
            IMapper mapper)
        {
            this.jobAdsService = jobAdsService;
            this.nomenclatureService = nomenclatureService;
            this.unitOfWork = unitOfWork;
            this.jobSubscriptionsRules = jobSubscriptionsRules;
            this.mapper = mapper;
        }

        public async Task<JobSubscriptionViewModel> SubscribeForJobs(string userId, JobSubscriptionCriteriasViewModel subscription)
        {
            JobSubscriptionCriteriasDTO subscriptionDto = this.mapper.Map<JobSubscriptionCriteriasDTO>(subscription);

            this.jobSubscriptionsRules.ValidateJobsSubscriptionProperties(subscriptionDto);

            bool hasSuchSubscription = await this.unitOfWork.JobAdSubscriptionsRepository.Any(userId, subscription);
            if (hasSuchSubscription)
            {
                throw new ActionableException("You already have subscription for jobs with given criterias!");
            }
            
            subscription.UserId = userId;

            await this.unitOfWork.JobAdSubscriptionsRepository.Add(subscription);

            await this.unitOfWork.SaveChanges();

            // TODO: create when saveChanges is invoked with passed DTO, to return get the created id from the DTO
            // return await this.unitOfWork.JobAdSubscriptionsRepository.GetDetails(subscription.Id);
            return await this.unitOfWork.JobAdSubscriptionsRepository.GetLastSubscriptionDetails(userId);
        }

        public async Task UnsubscribeFromJobsWithCriterias(int subscriptionId, string userId)
        {
            await this.unitOfWork.JobAdSubscriptionsRepository.Delete(subscriptionId, userId);
            await this.unitOfWork.SaveChanges();
        }

        public async Task UnsubscribeFromAllJobsWithCriterias(string userId)
        {
            this.unitOfWork.JobAdSubscriptionsRepository.DeleteAll(userId);
            await this.unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<JobSubscriptionViewModel>> GetAllJobSubscriptions(string userId)
        {
            return await this.unitOfWork.JobAdSubscriptionsRepository.GetAll(userId);          
        }

        public async Task<IDictionary<string, List<JobAdsSubscriptionsViewModel>>> GetLatestJobAdsAsync(int recurringTypeId)
        {
            IEnumerable<JobAdsSubscriptionsDbFunctionResult> jobAdsSubscriptions = await this.unitOfWork.JobAdSubscriptionsRepository
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

            foreach (JobAdsSubscriptionsDbFunctionResult item in jobAdsSubscriptions)
            {
                IEnumerable<LatestJobAdsDbFunctionResult> latestJobAds = await this.unitOfWork.JobAdSubscriptionsRepository
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

        private async Task<List<JobAdDetailsForSubscriber>> GetJobAdsDetails(int[] jobAdsIds, Dictionary<int, JobAdDetailsForSubscriber> jobDetailsById)
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
    }
}
