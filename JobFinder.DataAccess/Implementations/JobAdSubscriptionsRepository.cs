using JobFinder.Data.Models.Subscriptions;
using JobFinder.Data.Models.ViewsModels;
using JobFinder.DataAccess.Contracts;
using JobFinder.Data;
using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
using Microsoft.EntityFrameworkCore;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Common.Exceptions;
using AutoMapper;

namespace JobFinder.DataAccess.Implementations
{
    public class JobAdSubscriptionsRepository : EfCoreRepository<JobsSubscriptionEntity>, IJobAdSubscriptionsRepository
    {
        private readonly IMapper mapper;

        public JobAdSubscriptionsRepository(JobFinderDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<JobAdsSubscriptionsDbFunctionResult>> GetAll(int recurringTypeId)
        {
            return await this.Context
                .GetJobAdsSubscriptionsDbFunction(recurringTypeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<JobSubscriptionViewModel>> GetAll(string userId)
        {
            return await this.DbSet.AsNoTracking()
                .Where(js => js.UserId == userId)
                .To<JobSubscriptionViewModel>()
                .ToListAsync();
        }

        public async Task<IEnumerable<LatestJobAdsDbFunctionResult>> GetLatestAdsForSubscriptions(
            int recurringTypeId,
            JobAdsSubscriptionsDbFunctionResult subscriptionCriterias)
        {
            return await this.Context
                    .GetLatesJobAdsForSubscribersDbFunction(
                        recurringTypeId,
                        subscriptionCriterias.JobCategoryId,
                        subscriptionCriterias.JobEngagementId,
                        subscriptionCriterias.LocationId,
                        subscriptionCriterias.SearchTerm,
                        subscriptionCriterias.Intership,
                        subscriptionCriterias.SpecifiedSalary)
                    .ToListAsync();
        }

        public async Task<bool> Any(string userId, JobSubscriptionCriteriasViewModel subscription)
        {
            string trimmedSearchTerm = subscription.SearchTerm?.Trim();
            string search = trimmedSearchTerm == string.Empty ? null : trimmedSearchTerm;

            return await this.DbSet
                .AnyAsync(js => js.UserId == userId
                    && js.JobCategoryId == subscription.JobCategoryId
                    && js.JobEngagementId == subscription.JobEngagementId
                    && js.LocationId == subscription.LocationId
                    && js.Intership == subscription.Intership
                    && js.SpecifiedSalary == subscription.SpecifiedSalary
                    && js.SearchTerm == search);
        }

        public void DeleteAll(string userId)
        {
            this.DeleteWhere(js => js.UserId == userId);
        }

        public Task<JobSubscriptionViewModel> GetDetails(int subscriptionId)
        {
            return this.DbSet.AsNoTracking()
                .Where(x => x.Id == subscriptionId)
                .To<JobSubscriptionViewModel>()
                .SingleOrDefaultAsync();
        }

        public async Task Delete(int subscriptionId, string userId)
        {
            JobsSubscriptionEntity subFromDb = await this.DbSet.FindAsync(subscriptionId);

            if (userId != subFromDb.UserId)
            {
                throw new UnauthorizedException("You are not allowed to remove another users' subscriptions!");
            }

            base.Delete(subFromDb);
        }

        public async Task Add(JobSubscriptionCriteriasViewModel subscription)
        {
            JobsSubscriptionEntity subscriptionEntity = this.mapper.Map<JobsSubscriptionEntity>(subscription);

            await base.AddAsync(subscriptionEntity);
        }

        // TODO: remove after saveChanges with DTO is implemented

        public async Task<JobSubscriptionViewModel> GetLastSubscriptionDetails(string userId)
        {
            return await this.DbSet.AsNoTracking()
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .To<JobSubscriptionViewModel>()
                .FirstOrDefaultAsync();
        }
    }
}
