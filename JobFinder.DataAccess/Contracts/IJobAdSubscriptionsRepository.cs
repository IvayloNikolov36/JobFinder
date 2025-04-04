﻿using JobFinder.Data.Models.ViewsModels;
using JobFinder.Transfer.DTOs;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;

namespace JobFinder.DataAccess.Contracts
{
    public interface IJobAdSubscriptionsRepository
    {
        // TODO: create all to work with DTOs instead of view models

        Task<IEnumerable<JobAdsSubscriptionsDbFunctionResult>> GetAll(int recurringTypeId);

        Task<IEnumerable<JobSubscriptionViewModel>> GetAll(string userId);

        Task<IEnumerable<LatestJobAdsDbFunctionResult>> GetLatestAdsForSubscriptions(
            int recurringTypeId,
            JobAdsSubscriptionsDbFunctionResult subscriptionCriterias);

        Task<bool> Any(JobSubscriptionCriteriasDTO subscription);

        void DeleteAll(string userId);

        Task Delete(int subscriptionId, string userId);

        Task Add(JobSubscriptionCriteriasDTO subscription);

        Task<JobSubscriptionViewModel> GetDetails(int subscriptionId);
    }
}
