using System;
using System.Collections.Generic;
using System.Linq;
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
            string publisherId, string position, string description, DateTime expiration, int jobCategoryId, int jobEngagementId, int? minSalary, int? maxSalary, string location)
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

        public async Task<IEnumerable<JobAdsListingServiceModel>> AllAsync(int page, int items)
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

        public async Task EditAsync(int offerId, string position, string description, int daysActive)
        {
            var offerFromDb = await this.dbContext.FindAsync<JobAd>(offerId);

            offerFromDb.Position = position;
            offerFromDb.Desription = description;
            offerFromDb.Expiration = DateTime.UtcNow.AddDays(daysActive);

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<int> TotalCountAsync()
        {
            return await this.dbContext.JobAds.CountAsync();
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

    }
}
