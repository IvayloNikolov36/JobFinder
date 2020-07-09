namespace JobFinder.Services.Implementations
{
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Data.Models.ViewsModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class SubscriptionsService : DbService, ISubscriptionsService
    {
        public SubscriptionsService(JobFinderDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> SubscribeToCompanyAsync(int companyId, string userId)
        {
            var company = await this.DbContext.FindAsync<Company>(companyId);
            if (company == null)
            {
                return false;
            }

            try
            {
                var sub = new CompanySubscription
                {
                    UserId = userId,
                    CompanyId = companyId
                };

                await this.DbContext.AddAsync(sub);
                await this.DbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> SubscribeToJobCategoryAsync(int jobCategoryId, string userId)
        {
            var jobCategory = await this.DbContext.FindAsync<JobCategory>(jobCategoryId);
            if (jobCategory == null)
            {
                return false;
            }

            try
            {
                var sub = new JobCategorySubscription
                {
                    UserId = userId,
                    JobCategoryId = jobCategoryId
                };

                await this.DbContext.AddAsync(sub);
                await this.DbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UnsubscribeFromCompanyAsync(int companyId, string userId)
        {
            var subFromDb = await this.DbContext
                .FindAsync<CompanySubscription>(userId, companyId);

            if (subFromDb == null)
            {
                return false;
            }

            this.DbContext.Remove(subFromDb);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnsubscribeFromJobCategoryAsync(int jobCategoryId, string userId)
        {
            var subFromDb = await this.DbContext
                .FindAsync<JobCategorySubscription>(userId, jobCategoryId);

            if (subFromDb == null)
            {
                return false;
            }

            this.DbContext.Remove(subFromDb);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<CompaniesSubscriptionsData>> GetNewJobAdsForSubscribersAsync()
        {
            List<CompaniesSubscriptionsData> data = await this.DbContext
                .CompaniesSubscriptionsData
                .AsNoTracking()
                .ToListAsync();

            return data;
        }
    }
}
