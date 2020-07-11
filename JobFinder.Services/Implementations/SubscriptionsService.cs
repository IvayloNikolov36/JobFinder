namespace JobFinder.Services.Implementations
{
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Data.Models.Subscriptions;
    using JobFinder.Data.Models.ViewsModels;
    using JobFinder.Web.Models.Subscriptions.JobCategoriesSubscriptions;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
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

        public async Task<bool> SubscribeToJobCategoryAsync(int jobCategoryId, string userId, string location)
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
                    JobCategoryId = jobCategoryId,
                    Location = location
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

        public async Task<bool> UnsubscribeFromJobCategoryAsync(int jobCategoryId, string userId, string location)
        {
            var subFromDb = await this.DbContext.JobCategorySubscriptions
                .FirstOrDefaultAsync(x => x.JobCategoryId == jobCategoryId
                                  && x.UserId == userId
                                  && x.Location == location);

            if (subFromDb == null)
            {
                return false;
            }

            this.DbContext.Remove(subFromDb);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<CompaniesSubscriptionsData>> GetCompaniesNewJobAdsAsync()
        {
            List<CompaniesSubscriptionsData> data = await this.DbContext
                .CompaniesSubscriptionsData
                .AsNoTracking()
                .ToListAsync();

            return data;
        }

        public async Task<List<JobAdsByCategoryAndLocationViewModel>> GetNewJobAdsByCategoryAsync()
        {
            List<JobCategoriesSubscriptionsData> data = await this.DbContext
                .JobCategoriesSubscriptionsData
                .AsNoTracking()
                .ToListAsync();

            List<JobAdsByCategoryAndLocationViewModel> result = new List<JobAdsByCategoryAndLocationViewModel>();

            foreach (var item in data)
            {
                string location = item.Location;
                int jobCategoryId = item.JobCategoryId;

                result.Add(new JobAdsByCategoryAndLocationViewModel
                {
                    JobCategoryId = jobCategoryId,
                    JobCategory = item.JobCategory,
                    Location = location,
                    Subscribers = item.Subscribers.Split(new[] { "; " }, StringSplitOptions.RemoveEmptyEntries),
                    LatestCompanyJobAds = await this.DbContext.GetLatesJobAdsForSubscribers(jobCategoryId, location).ToListAsync()
                });
            }

            return result;
        }
    }
}
