using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Services.Implementations
{
    public class JobAdsService : IJobAdsService
    {
        private readonly JobFinderDbContext dbContext;

        public JobAdsService(JobFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<JobAd> GetAsync(int id)
        {
            return await this.dbContext.FindAsync<JobAd>(id);
        }

        public async Task CreateAsync(
            string publisherId, string position, string description, DateTime expiration, 
            int jobCategoryId, int jobEngagementId, int? minSalary, int? maxSalary, string location)
        {
            var offer = new JobAd
            {
                Position = position,
                Desription = description,
                PostedOn = DateTime.UtcNow,
                Expiration = expiration,
                PublisherId = publisherId,
                JobCategoryId = jobCategoryId,
                JobEngagementId = jobEngagementId,
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Location = location
            };

            await this.dbContext.AddAsync(offer);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(int offerId, string position, string description, int daysActive)
        {
            var offerFromDb = await this.dbContext.FindAsync<JobAd>(offerId);

            offerFromDb.Position = position;
            offerFromDb.Desription = description;
            offerFromDb.Expiration = DateTime.UtcNow.AddDays(daysActive);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IDictionary<int, string>> GetJobEngagements()
        {
            var dbEngagements = await this.dbContext.JobEngagements.ToListAsync();

            IDictionary<int, string> engagements = dbEngagements.ToDictionary(x => x.Id, x => x.Type);

            return engagements;

        }

        public async Task<IDictionary<int, string>> GetJobCategories()
        {
            var dbCategories = await this.dbContext.JobCategories.ToListAsync();

            IDictionary<int, string> categories = dbCategories.ToDictionary(x => x.Id, x => x.Type);

            return categories;
        }

        public async Task<IEnumerable<JobAdsListingServiceModel>> AllAsync(
            int page, int items, int? engagementId, int? categoryId, string location)
        {
            bool hasFiltering = engagementId != null | categoryId != null || !string.IsNullOrEmpty(location);

            if (!hasFiltering)
            {
                return await this.AllAsync(page, items);
            }

            IQueryable<JobAd> jobAds = this.dbContext.JobAds;

            if (engagementId != null)
            {
                jobAds = this.FilterByEngagement(jobAds, engagementId);
            }

            if (categoryId != null)
            {
                jobAds = this.FilteredByCategory(jobAds, categoryId);
            }

            if (!string.IsNullOrEmpty(location))
            {
                jobAds = this.FilterByLocation(jobAds, location);
            }

            return await jobAds.Skip((page - 1) * items)
                .Take(items)
                .Select(ad => new JobAdsListingServiceModel
                {
                    Id = ad.Id,
                    Position = ad.Position,
                    PostedOn = ad.PostedOn.ToShortDateString(),
                    JobCategory = ad.JobCategory.Type,
                    JobEngagement = ad.JobEngagement.Type,
                    CompanyLogo = ad.Publisher.Company.CompanyLogo,
                    CompanyName = ad.Publisher.Company.CompanyName,
                    Location = ad.Location,
                    Salary = ad.MinSalary.ToString() + " - " + ad.MaxSalary.ToString()
                })
                .ToListAsync();
        }

        private async Task<IEnumerable<JobAdsListingServiceModel>> AllAsync(int page, int items)
        {
            var ads = await this.dbContext
                .JobAds
                .OrderByDescending(j => j.PostedOn)
                .Skip((page - 1) * items)
                .Take(items)
                .Select(ad => new JobAdsListingServiceModel
                {
                    Id = ad.Id,
                    Position = ad.Position,
                    PostedOn = ad.PostedOn.ToShortDateString(),
                    JobCategory = ad.JobCategory.Type,
                    JobEngagement = ad.JobEngagement.Type,
                    CompanyLogo = ad.Publisher.Company.CompanyLogo,
                    CompanyName = ad.Publisher.Company.CompanyName,
                    Location = ad.Location,
                    Salary = ad.MinSalary.ToString() + " - " + ad.MaxSalary.ToString()
                })
                .ToListAsync();

            return ads;
        }

        //Filter methods

        private IQueryable<JobAd> FilteredByCategory(IQueryable<JobAd> jobAds, int? jobCategoryId)
        {
            return jobAds.Where(j => j.JobCategoryId == jobCategoryId);
        }

        private IQueryable<JobAd> FilterByEngagement(IQueryable<JobAd> jobAds, int? jobEngagementId)
        {
            return jobAds.Where(j => j.JobEngagementId == jobEngagementId);
        }

        private IQueryable<JobAd> FilterByLocation(IQueryable<JobAd> jobAds, string location)
        {
            return jobAds.Where(j => j.Location == location);
        }
    }
}
