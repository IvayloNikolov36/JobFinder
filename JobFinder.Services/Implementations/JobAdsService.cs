﻿namespace JobFinder.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using JobFinder.Common.Exceptions;
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
            bool hasSalaryValue = model.MinSalary.HasValue || model.MaxSalary.HasValue;

            if (hasSalaryValue && !model.CurrencyId.HasValue)
            {
                throw new ActionableException("You have to specify currency type!");
            }

            JobAdvertisementEntity jobAd = this.mapper.Map<JobAdvertisementEntity>(model);
            jobAd.PublisherId = companyId;
            jobAd.PublishDate = DateTime.UtcNow;
            jobAd.IsActive = true;

            await this.jobsRepository.AddAsync(jobAd);

            await this.jobsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int jobAdId, string userId, JobAdEditModel editModel)
        {
            JobAdvertisementEntity offerFromDb = await this.jobsRepository.FindAsync(jobAdId);

            if (offerFromDb == null)
            {
                throw new ActionableException("Job ad with such id is not found!");
            }

            bool isUserPublisher = userId == offerFromDb.Publisher.User.Id;

            if (!isUserPublisher)
            {
                throw new ActionableException("You can edit only your own ads!");
            }

            this.mapper.Map(editModel, offerFromDb);

            this.jobsRepository.Update(offerFromDb);

            await this.jobsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(string userId)
        {
            return await this.jobsRepository.DbSetNoTracking()
                .Where(ja => ja.Publisher.UserId == userId)
                .To<CompanyJobAdViewModel>()
                .OrderByDescending(j => j.PublishDate)
                .ToListAsync();
        }

        public async Task<DataListingsModel<JobListingModel>> AllAsync(JobAdsFilterModel model)
        {
            IQueryable<JobAdvertisementEntity> jobs = this.jobsRepository.DbSetNoTracking();

            if (!string.IsNullOrEmpty(model.SearchText?.Trim()))
            {
                model.SearchText = model.SearchText.ToLower();

                jobs = jobs.Where(j => j.Position.ToLower().Contains(model.SearchText)
                        || j.Publisher.Name.ToLower().Contains(model.SearchText));
            }

            if (model.EngagementId.HasValue)
            {
                jobs = this.FilterByEngagement(jobs, model.EngagementId.Value);
            }

            if (model.CategoryId.HasValue)
            {
                jobs = this.FilteredByCategory(jobs, model.CategoryId.Value);
            }

            if (model.LocationId.HasValue)
            {
                jobs = this.FilterByLocation(jobs, model.LocationId.Value);
            }

            if (!string.IsNullOrEmpty(model.SortBy) && model.SortBy == "Salary")
            {
                jobs = this.SortBySalary(jobs, (bool)model.IsAscending);
            }

            if (!string.IsNullOrEmpty(model.SortBy) && model.SortBy == "Published")
            {
                jobs = this.SortByPublishDate(jobs, (bool)model.IsAscending);
            }

            int totalCount = await jobs.CountAsync();

            IEnumerable<JobListingModel> jobAds = await jobs.Skip((model.Page - 1) * model.Items)
               .Take(model.Items)
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
