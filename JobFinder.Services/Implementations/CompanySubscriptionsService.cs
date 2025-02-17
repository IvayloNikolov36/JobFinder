namespace JobFinder.Services.Implementations
{
    using JobFinder.Common.Exceptions;
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Subscriptions.CompanySubscriptions;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanySubscriptionsService : ICompanySubscriptionsService
    {
        private readonly JobFinderDbContext dbContext;
        private readonly IRepository<CompanySubscriptionEntity> companySubscriptionRepository;

        public CompanySubscriptionsService(
            JobFinderDbContext dbContext,
            IRepository<CompanySubscriptionEntity> companySubscriptionRepository)
        {
            this.dbContext = dbContext;
            this.companySubscriptionRepository = companySubscriptionRepository;
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

            bool hasSuchSubscricption = await this.companySubscriptionRepository
                .ExistAsync(cs => cs.CompanyId == companyId && cs.UserId == userId);

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
            CompanySubscriptionEntity subFromDb = await this.companySubscriptionRepository
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CompanyId == companyId);

            if (subFromDb == null)
            {
                throw new ActionableException("No subscription found!");
            }

            this.dbContext.Remove(subFromDb);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task UnsubscribeAllAsync(string userId)
        {
            this.companySubscriptionRepository.DeleteWhere(cs => cs.UserId == userId);

            await this.companySubscriptionRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanySubscriptionViewModel>> GetMySubscriptions(string userId)
        {
            return await this.companySubscriptionRepository.AllAsNoTracking()
                .Where(cs => cs.UserId == userId)
                .To<CompanySubscriptionViewModel>()
                .ToListAsync();
        }
    }
}
