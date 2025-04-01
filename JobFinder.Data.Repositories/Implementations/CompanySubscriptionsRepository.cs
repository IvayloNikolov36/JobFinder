using JobFinder.Common.Exceptions;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.Data.Models.Subscriptions;
using JobFinder.Data.Models.ViewsModels;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations
{
    public class CompanySubscriptionsRepository : EfCoreRepository<CompanySubscriptionEntity>, ICompanySubscriptionsRepository
    {
        public CompanySubscriptionsRepository(JobFinderDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<CompanyJobAdsForSubscribersViewData>> GetCompanyAdsBySubscriptions()
        {
            return await this.Context
                .CompanyJobAdsForSubscribersView
                .ToListAsync();
        }

        public async Task<IEnumerable<CompanySubscriptionViewModel>> GetMySubscriptions(string userId)
        {
            return await this.DbSet.AsNoTracking()
                .Where(cs => cs.UserId == userId)
                .To<CompanySubscriptionViewModel>()
                .ToListAsync();
        }

        public async Task Subscribe(string userId, int companyId)
        {
            CompanyEntity company = await this.Context.FindAsync<CompanyEntity>(companyId)
                ?? throw new ActionableException($"No company with id: {companyId}");

            bool hasSuchSubscription = await this.Exist(userId, companyId);
            if (hasSuchSubscription)
            {
                throw new ActionableException("You can't subscribe twice for that company ads!");
            }

            CompanySubscriptionEntity sub = new()
            {
                UserId = userId,
                CompanyId = companyId
            };

            await this.DbSet.AddAsync(sub);
        }

        public void DeleteSubscriptions(string userId)
        {
            this.DeleteWhere(cs => cs.UserId == userId);
        }

        public async Task Delete(string userId, int companyId)
        {
            CompanySubscriptionEntity companysubscription = await this.Getsubscription(userId, companyId);

            base.Delete(companysubscription);
        }

        private async Task<CompanySubscriptionEntity> Getsubscription(string userId, int companyId)
        {
            CompanySubscriptionEntity companySubscription = await this.DbSet
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CompanyId == companyId);

            base.ValidateForExistence(companySubscription, nameof(CompanySubscriptionEntity));

            return companySubscription;
        }

        public async Task<bool> Exist(string userId, int companyId)
        {
            return await this.DbSet
                .AnyAsync(cs => cs.UserId == userId && cs.CompanyId == companyId);
        }
    }
}
