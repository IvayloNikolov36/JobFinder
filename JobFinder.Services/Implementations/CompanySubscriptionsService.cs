using AutoMapper;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;
using static JobFinder.Services.Constants.CacheConstants;

namespace JobFinder.Services.Implementations
{
    public class CompanySubscriptionsService : ICompanySubscriptionsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;
        private readonly ICloudImageManagementService cloudImageService;

        public CompanySubscriptionsService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            IDistributedCache distributedCache,
            ICloudImageManagementService cloudImageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
            this.cloudImageService = cloudImageService;
        }

        public async Task<IEnumerable<CompanyJobAdsForSubscribersViewModel>> GetLatesJobAds()
        {
            IEnumerable<CompanyJobAdsForSubscribersDTO> jobAds = await this.unitOfWork
                .CompanySubscriptionsRepository
                .GetCompanyAdsBySubscriptions();

            return this.mapper
                .Map<IEnumerable<CompanyJobAdsForSubscribersViewModel>>(jobAds);
        }

        public async Task Subscribe(int companyId, string userId)
        {
            await this.unitOfWork.CompanySubscriptionsRepository
                .Subscribe(userId, companyId);

            await this.unitOfWork.SaveChanges();

            await this.InvalidateCache(userId);
        }

        public async Task Unsubscribe(int companyId, string userId)
        {
            await this.unitOfWork.CompanySubscriptionsRepository
                .Delete(userId, companyId);

            await this.unitOfWork.SaveChanges();

            await this.InvalidateCache(userId);
        }

        public async Task UnsubscribeAll(string userId)
        {
            this.unitOfWork.CompanySubscriptionsRepository
                .DeleteSubscriptions(userId);

            await this.unitOfWork.SaveChanges();

            await this.InvalidateCache(userId);
        }

        public async Task<IEnumerable<CompanySubscriptionViewModel>> GetMySubscriptions(
            string userId)
        {
            string cacheKey = GetCompanySubscriptionCacheKey(userId);

            byte[] subscriptionsCache = await this.distributedCache
                .GetAsync(cacheKey);

            if (subscriptionsCache == null)
            {
                IEnumerable<CompanySubscriptionDTO> subscriptions = await this.unitOfWork
                    .CompanySubscriptionsRepository
                    .GetMySubscriptions(userId);

                foreach (CompanySubscriptionDTO subscription in subscriptions)
                {
                    if (subscription.CompanyLogoImageId.HasValue)
                    {
                        subscription.CompanyLogo = await this.cloudImageService
                            .GetThumbnailUrl(subscription.CompanyLogoImageId.Value);
                    }
                }

                var subscriptionModels = this.mapper
                    .Map<IEnumerable<CompanySubscriptionViewModel>>(subscriptions);

                await this.SetDataInCache(subscriptionModels, cacheKey);

                return subscriptionModels;
            }

            return this.DeserializeData(subscriptionsCache);
        }

        private async Task SetDataInCache(
            IEnumerable<CompanySubscriptionViewModel> data,
            string cacheKey)
        {
            string serializedData = JsonSerializer.Serialize(data);

            await this.distributedCache
                .SetAsync(cacheKey, Encoding.UTF8.GetBytes(serializedData));
        }

        private async Task InvalidateCache(string userId)
        {
            string cacheKey = GetCompanySubscriptionCacheKey(userId);

            await this.distributedCache.RemoveAsync(cacheKey);
        }

        private IEnumerable<CompanySubscriptionViewModel> DeserializeData(
            byte[] subscriptionsData)
        {
            string dataString = Encoding.UTF8.GetString(subscriptionsData);

            return JsonSerializer
                .Deserialize<IEnumerable<CompanySubscriptionViewModel>>(dataString);
        }

        private static string GetCompanySubscriptionCacheKey(string userId)
        {
            return string.Format(CompanySubscriptionsCacheKey, userId);
        }
    }
}
