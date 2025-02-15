namespace JobFinder.Services.Implementations
{
    using JobFinder.Common.Exceptions;
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Data.Models.ViewsModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CompanySubscriptionsService : ICompanySubscriptionsService
    {
        private readonly JobFinderDbContext dbContext;

        public CompanySubscriptionsService(JobFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CompaniesSubscriptionsData>> GetLatesJobAdsAsync()
        {
            IEnumerable<CompaniesSubscriptionsData> data = await this.dbContext
                .CompaniesSubscriptionsData
                .AsNoTracking()
                .ToListAsync();

            return data;
        }

        public async Task SubscribeAsync(int companyId, string userId)
        {
            CompanyEntity company = await this.dbContext.FindAsync<CompanyEntity>(companyId)
                ?? throw new ActionableException($"No company with id: {companyId}");

            bool hasSuchSubscricption = await this.dbContext.CompanySubscriptions
                .AnyAsync(cs => cs.CompanyId == companyId && cs.UserId == userId);

            if (hasSuchSubscricption)
            {
                throw new ActionableException("You can't subscribe twice for that company jobs!");
            }

            CompanySubscriptionEntity sub = new()
            {
                UserId = userId,
                CompanyId = companyId
            };

            await this.dbContext.AddAsync(sub);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UnsubscribeAsync(int companyId, string userId)
        {
            CompanySubscriptionEntity subFromDb = await this.dbContext
                .CompanySubscriptions
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CompanyId == companyId);

            if (subFromDb == null)
            {
                throw new ActionableException("No subscription found!");
            }

            this.dbContext.Remove(subFromDb);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
