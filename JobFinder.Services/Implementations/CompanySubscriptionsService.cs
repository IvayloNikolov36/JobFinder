namespace JobFinder.Services.Implementations
{
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

        public async Task<List<CompaniesSubscriptionsData>> GetLatesJobAdsAsync()
        {
            List<CompaniesSubscriptionsData> data = await this.dbContext.CompaniesSubscriptionsData
                .AsNoTracking()
                .ToListAsync();

            return data;
        }

        public async Task<bool> SubscribeAsync(int companyId, string userId)
        {
            var company = await this.dbContext.FindAsync<Company>(companyId);
            if (company == null)
            {
                return false;
            }

            var sub = new CompanySubscription
            {
                UserId = userId,
                CompanyId = companyId
            };

            await this.dbContext.AddAsync(sub);
            await this.dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnsubscribeAsync(int companyId, string userId)
        {
            var subFromDb = await this.dbContext.CompanySubscriptions
                .FirstOrDefaultAsync(c => c.UserId == userId && c.CompanyId == companyId);
            if (subFromDb == null)
            {
                return false;
            }

            this.dbContext.Remove(subFromDb);
            await this.dbContext.SaveChangesAsync();

            return true;
        }
    }
}
