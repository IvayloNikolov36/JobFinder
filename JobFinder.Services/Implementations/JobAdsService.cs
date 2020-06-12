namespace JobFinder.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JobFinder.Data;
    using JobFinder.Data.Models;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;

    public class JobAdsService : IJobAdsService
    {
        private readonly JobFinderDbContext dbContext;

        public JobAdsService(JobFinderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await this.dbContext.JobAds.AsNoTracking()
                .Where(ja => ja.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task<T> DetailsAsync<T>(int jobId)
        {
            return await this.dbContext.JobAds.AsNoTracking()
                .Where(j => j.Id == jobId)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(
            string publisherId, string position, string description, int jobCategoryId, 
            int jobEngagementId, int? minSalary, int? maxSalary, string location)
        {
            var offer = new JobAd
            {
                Position = position,
                Desription = description,
                PostedOn = DateTime.UtcNow,
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

        public async Task<bool> EditAsync(int jobAdId, string userId, string position, string description)
        {
            var offerFromDb = await this.dbContext.FindAsync<JobAd>(jobAdId);
            bool isUserPublisher = userId == offerFromDb.PublisherId;

            if (offerFromDb == null || !isUserPublisher)
            {
                return false;
            }

            offerFromDb.Position = position;
            offerFromDb.Desription = description;

            await this.dbContext.SaveChangesAsync();
            return true;
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

        public async Task<(int, IEnumerable<T>)> AllAsync<T>(
        int page, int items, string searchText, int? engagementId, int? categoryId, string location,
        string sortBy, bool? isAscending)
        {
            IQueryable<JobAd> jobs = this.dbContext.JobAds.AsNoTracking();

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();

                jobs = jobs.Where(j => j.Position.ToLower().Contains(searchText)
                        || j.Publisher.Company.Name.ToLower().Contains(searchText));
            }

            if (engagementId != null)
            {
                jobs = this.FilterByEngagement(jobs, engagementId);
            }

            if (categoryId != null)
            {
                jobs = this.FilteredByCategory(jobs, categoryId);
            }

            if (!string.IsNullOrEmpty(location))
            {
                jobs = this.FilterByLocation(jobs, location);
            }

            if (!string.IsNullOrEmpty(sortBy) && sortBy == "Salary")
            {
                jobs = this.SortBySalary(jobs, (bool)isAscending);
            }

            if (!string.IsNullOrEmpty(sortBy) && sortBy == "Published")
            {
                jobs = this.SortByPublishDate(jobs, (bool)isAscending);
            }

            int totalCount = await jobs.CountAsync();

            List<T> jobAds = await jobs.Skip((page - 1) * items)
               .Take(items)
               .To<T>()
               .ToListAsync();

            return (totalCount, jobAds);
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

        //Sort methods

        private IQueryable<JobAd> SortBySalary(IQueryable<JobAd> jobAds, bool isAscending)
        {
            return isAscending
                ? jobAds.OrderBy(j => j.MaxSalary)
                : jobAds.OrderByDescending(j => j.MinSalary);
        }

        private IQueryable<JobAd> SortByPublishDate(IQueryable<JobAd> jobAds, bool isAscending)
        {
            return isAscending
                ? jobAds.OrderBy(j => j.PostedOn)
                : jobAds.OrderByDescending(j => j.PostedOn);
        }
    }
}
