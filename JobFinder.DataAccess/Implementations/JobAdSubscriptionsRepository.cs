using AutoMapper;
using JobFinder.Data.Models.Subscriptions;
using JobFinder.DataAccess.Contracts;
using JobFinder.Data;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Common.Exceptions;
using JobFinder.Transfer.DTOs;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations
{
    public class JobAdSubscriptionsRepository : EfCoreRepository<JobsSubscriptionEntity>, IJobAdSubscriptionsRepository
    {
        private readonly IMapper mapper;

        public JobAdSubscriptionsRepository(JobFinderDbContext context, IMapper mapper) : base(context)
        {
            this.mapper = mapper;
        }

        public async Task<IEnumerable<JobAdsSubscriptionDTO>> GetAll(int recurringTypeId)
        {
            return await this.Context
                .GetJobAdsSubscriptionsDbFunction(recurringTypeId)
                .To<JobAdsSubscriptionDTO>()
                .ToListAsync();
        }

        public async Task<IEnumerable<JobSubscriptionDTO>> GetAll(string userId)
        {
            return await this.DbSet.AsNoTracking()
                .Where(js => js.UserId == userId)
                .To<JobSubscriptionDTO>()
                .ToListAsync();
        }

        public async Task<IEnumerable<LatestJobAdsDTO>> GetLatestAdsForSubscriptions(
            int recurringTypeId,
            JobAdsSubscriptionDTO subscriptionCriterias)
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
                    .To<LatestJobAdsDTO>()
                    .ToListAsync();
        }

        public async Task<bool> Any(JobSubscriptionCriteriasDTO subscription)
        {
            string trimmedSearchTerm = subscription.SearchTerm?.Trim();
            string search = trimmedSearchTerm == string.Empty ? null : trimmedSearchTerm;

            return await this.DbSet
                .AnyAsync(js => js.UserId == subscription.UserId
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

        public Task<JobSubscriptionDTO> GetDetails(int subscriptionId)
        {
            return this.DbSet.AsNoTracking()
                .Where(x => x.Id == subscriptionId)
                .To<JobSubscriptionDTO>()
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

        public async Task Add(JobSubscriptionCriteriasDTO subscription)
        {
            JobsSubscriptionEntity subscriptionEntity = this.mapper.Map<JobsSubscriptionEntity>(subscription);

            await base.AddAsync(subscriptionEntity);
        }
    }
}
