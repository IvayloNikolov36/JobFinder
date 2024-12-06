namespace JobFinder.Services.Implementations
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JobFinder.Data.Models;
    using JobFinder.Data.Repositories;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;

    public class JobAdsService : IJobAdsService
    {
        private readonly IRepository<JobAdvertisementEntity> jobsRepository;

        public JobAdsService(IRepository<JobAdvertisementEntity> jobsRepository)
        {
            this.jobsRepository = jobsRepository;
        }

        public async Task<T> GetAsync<T>(int id)
         => await this.jobsRepository
                .AllAsNoTracking()
                .Where(ja => ja.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();


        public async Task CreateAsync(
            int companyId, string position, string description, int jobCategoryId,
            int jobEngagementId, int? minSalary, int? maxSalary, string location)
        {
            var offer = new JobAdvertisementEntity
            {
                Position = position,
                Desription = description,
                PublisherId = companyId,
                JobCategoryId = jobCategoryId,
                JobEngagementId = jobEngagementId,
                MinSalary = minSalary,
                MaxSalary = maxSalary,
                Location = location
            };

            await this.jobsRepository.AddAsync(offer);
            await this.jobsRepository.SaveChangesAsync();
        }

        public async Task<bool> EditAsync(int jobAdId, string userId, string position, string description)
        {
            JobAdvertisementEntity offerFromDb = await this.jobsRepository.FindAsync(jobAdId);
            bool isUserPublisher = userId == offerFromDb.Publisher.User.Id;

            if (offerFromDb == null || !isUserPublisher)
            {
                return false;
            }

            offerFromDb.Position = position;
            offerFromDb.Desription = description;

            await this.jobsRepository.SaveChangesAsync();
            return true;
        }

        public async Task<(int, IEnumerable<T>)> AllAsync<T>(
        int page, int items, string searchText, int engagementId, int categoryId, string location,
        string sortBy, bool? isAscending)
        {
            IQueryable<JobAdvertisementEntity> jobs = this.jobsRepository.AllAsNoTracking();

            if (!string.IsNullOrEmpty(searchText))
            {
                searchText = searchText.ToLower();

                jobs = jobs.Where(j => j.Position.ToLower().Contains(searchText)
                        || j.Publisher.Name.ToLower().Contains(searchText));
            }

            if (engagementId != 0)
            {
                jobs = this.FilterByEngagement(jobs, engagementId);
            }

            if (categoryId != 0)
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

        private IQueryable<JobAdvertisementEntity> FilteredByCategory(IQueryable<JobAdvertisementEntity> jobAds, int? jobCategoryId)
        {
            return jobAds.Where(j => j.JobCategoryId == jobCategoryId);
        }

        private IQueryable<JobAdvertisementEntity> FilterByEngagement(IQueryable<JobAdvertisementEntity> jobAds, int? jobEngagementId)
        {
            return jobAds.Where(j => j.JobEngagementId == jobEngagementId);
        }

        private IQueryable<JobAdvertisementEntity> FilterByLocation(IQueryable<JobAdvertisementEntity> jobAds, string location)
        {
            return jobAds.Where(j => j.Location == location);
        }

        //Sort methods

        private IQueryable<JobAdvertisementEntity> SortBySalary(IQueryable<JobAdvertisementEntity> jobAds, bool isAscending)
        {
            return isAscending
                ? jobAds.OrderBy(j => j.MaxSalary)
                : jobAds.OrderByDescending(j => j.MinSalary);
        }

        private IQueryable<JobAdvertisementEntity> SortByPublishDate(IQueryable<JobAdvertisementEntity> jobAds, bool isAscending)
        {
            return isAscending
                ? jobAds.OrderBy(j => j.CreatedOn)
                : jobAds.OrderByDescending(j => j.CreatedOn);
        }
    }
}
