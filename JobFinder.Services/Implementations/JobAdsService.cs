namespace JobFinder.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using JobFinder.Data.Models;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.JobAds;
    using Microsoft.EntityFrameworkCore;

    public class JobAdsService : IJobAdsService
    {
        private readonly IRepository<JobAdvertisementEntity> jobsRepository;
        private readonly IMapper mapper;

        public JobAdsService(
            IRepository<JobAdvertisementEntity> jobsRepository,
            IMapper mapper)
        {
            this.jobsRepository = jobsRepository;
            this.mapper = mapper;
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await this.jobsRepository
                .DbSetNoTracking()
                .Where(ja => ja.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(int companyId, JobAdCreateModel model)
        {
            JobAdvertisementEntity jobAd = this.mapper.Map<JobAdvertisementEntity>(model);
            jobAd.PublisherId = companyId;
            jobAd.PublishDate = DateTime.UtcNow;
            jobAd.IsActive = true;

            await this.jobsRepository.AddAsync(jobAd);

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
            offerFromDb.Description = description;

            await this.jobsRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(string userId)
        {
            return await this.jobsRepository.DbSetNoTracking()
                .Where(ja => ja.Publisher.UserId == userId)
                .To<CompanyJobAdViewModel>()
                .OrderByDescending(j => j.PublishDate)
                .ToListAsync();
        }

        public async Task<DataListingsModel<JobListingModel>> AllAsync(JobAdsParams paramsModel)
        {
            IQueryable<JobAdvertisementEntity> jobs = this.jobsRepository.DbSetNoTracking();

            if (!string.IsNullOrEmpty(paramsModel.SearchText?.Trim()))
            {
                paramsModel.SearchText = paramsModel.SearchText.ToLower();

                jobs = jobs.Where(j => j.Position.ToLower().Contains(paramsModel.SearchText)
                        || j.Publisher.Name.ToLower().Contains(paramsModel.SearchText));
            }

            if (paramsModel.EngagementId.HasValue)
            {
                jobs = this.FilterByEngagement(jobs, paramsModel.EngagementId.Value);
            }

            if (paramsModel.CategoryId.HasValue)
            {
                jobs = this.FilteredByCategory(jobs, paramsModel.CategoryId.Value);
            }

            if (paramsModel.LocationId.HasValue)
            {
                jobs = this.FilterByLocation(jobs, paramsModel.LocationId.Value);
            }

            if (!string.IsNullOrEmpty(paramsModel.SortBy) && paramsModel.SortBy == "Salary")
            {
                jobs = this.SortBySalary(jobs, (bool)paramsModel.IsAscending);
            }

            if (!string.IsNullOrEmpty(paramsModel.SortBy) && paramsModel.SortBy == "Published")
            {
                jobs = this.SortByPublishDate(jobs, (bool)paramsModel.IsAscending);
            }

            int totalCount = await jobs.CountAsync();

            IEnumerable<JobListingModel> jobAds = await jobs.Skip((paramsModel.Page - 1) * paramsModel.Items)
               .Take(paramsModel.Items)
               .To<JobListingModel>()
               .ToListAsync();

            return new DataListingsModel<JobListingModel>(totalCount, jobAds);
        }

        // Filter methods

        private IQueryable<JobAdvertisementEntity> FilteredByCategory(
            IQueryable<JobAdvertisementEntity> jobAds,
            int jobCategoryId)
        {
            return jobAds.Where(j => j.JobCategoryId == jobCategoryId);
        }

        private IQueryable<JobAdvertisementEntity> FilterByEngagement(
            IQueryable<JobAdvertisementEntity> jobAds,
            int jobEngagementId)
        {
            return jobAds.Where(j => j.JobEngagementId == jobEngagementId);
        }

        private IQueryable<JobAdvertisementEntity> FilterByLocation(
            IQueryable<JobAdvertisementEntity> jobAds,
            int locationId)
        {
            return jobAds.Where(j => j.LocationId == locationId);
        }

        // Sort methods

        private IQueryable<JobAdvertisementEntity> SortBySalary(
            IQueryable<JobAdvertisementEntity> jobAds,
            bool isAscending)
        {
            return isAscending
                ? jobAds.OrderBy(j => j.MaxSalary)
                : jobAds.OrderByDescending(j => j.MinSalary);
        }

        private IQueryable<JobAdvertisementEntity> SortByPublishDate(
            IQueryable<JobAdvertisementEntity> jobAds,
            bool isAscending)
        {
            return isAscending
                ? jobAds.OrderBy(j => j.CreatedOn)
                : jobAds.OrderByDescending(j => j.CreatedOn);
        }
    }
}
