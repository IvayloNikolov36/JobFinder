using AutoMapper;
using JobFinder.Business.JobAds;
using JobFinder.Common.Exceptions;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.JobAds;
using JobFinder.Web.Models.Subscriptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobFinder.Services.Implementations
{
    public class JobAdsService : IJobAdsService
    {
        private readonly IRepository<JobAdvertisementEntity> jobAdsRepository;
        private readonly IJobAdvertisementsRules jobAdsRules;
        private readonly IMapper mapper;

        public JobAdsService(
            IRepository<JobAdvertisementEntity> jobAdsRepository,
            IJobAdvertisementsRules jobAdsRules,
            IMapper mapper)
        {
            this.jobAdsRepository = jobAdsRepository;
            this.jobAdsRules = jobAdsRules;
            this.mapper = mapper;
        }

        public async Task<T> GetAsync<T>(int id)
        {
            return await this.jobAdsRepository
                .DbSetNoTracking()
                .Where(ja => ja.Id == id)
                .To<T>()
                .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(int companyId, JobAdCreateModel model)
        {
            this.jobAdsRules.ValidateSalaryProperties(model.MinSalary, model.MaxSalary, model.CurrencyId);
            this.jobAdsRules.ValidateIntership(model.Intership, model.JobEngagementId);

            JobAdvertisementEntity jobAd = this.mapper.Map<JobAdvertisementEntity>(model);
            jobAd.PublisherId = companyId;
            jobAd.PublishDate = DateTime.UtcNow;
            jobAd.IsActive = true;

            await this.jobAdsRepository.AddAsync(jobAd);

            await this.jobAdsRepository.SaveChangesAsync();
        }

        public async Task EditAsync(int jobAdId, string userId, JobAdEditModel editModel)
        {
            JobAdvertisementEntity offerFromDb = await this.jobAdsRepository.FindAsync(jobAdId);

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

            this.jobAdsRepository.Update(offerFromDb);

            await this.jobAdsRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanyJobAdViewModel>> GetAllCompanyAds(string userId)
        {
            return await this.GetFilteredCompanyAds(userId, null);
        }

        public async Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(string userId, bool active)
        {
            return await this.GetFilteredCompanyAds(userId, active);
        }

        public async Task<DataListingsModel<JobListingModel>> AllActiveAsync(JobAdsFilterModel model)
        {
            IQueryable<JobAdvertisementEntity> jobs = this.jobAdsRepository
                .DbSetNoTracking()
                .Where(ja => ja.IsActive);

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

            if (model.Intership)
            {
                jobs = jobs.Where(ja => ja.Intership);
            }

            if (model.SpecifiedSalary)
            {
                jobs = jobs.Where(ja => ja.MinSalary.HasValue && ja.MaxSalary.HasValue);
            }

            if (!string.IsNullOrEmpty(model.SortBy) && model.SortBy == "Salary")
            {
                jobs = this.SortBySalary(jobs, model.IsAscending);
            }

            if (!string.IsNullOrEmpty(model.SortBy) && model.SortBy == "Published")
            {
                jobs = this.SortByPublishDate(jobs, model.IsAscending);
            }

            int totalCount = await jobs.CountAsync();

            IEnumerable<JobListingModel> jobAds = await jobs.Skip((model.Page - 1) * model.Items)
               .Take(model.Items)
               .To<JobListingModel>()
               .ToListAsync();

            return new DataListingsModel<JobListingModel>(totalCount, jobAds);
        }

        public async Task<JobAdDetailsForSubscriber> GetDetails(int jobAdId)
        {
            JobAdDetailsForSubscriber details = await this.jobAdsRepository.DbSetNoTracking()
                .Where(ja => ja.Id == jobAdId)
                .To<JobAdDetailsForSubscriber>()
                .SingleOrDefaultAsync();

            return details ?? throw new ActionableException($"There is no job ad with id: {jobAdId}!");
        }

        public async Task DeactivateAds()
        {
            int expirationAfterDays = this.jobAdsRules.GetDaysExpiration();

            DateTime date = DateTime.UtcNow.AddDays(-expirationAfterDays);

            await this.jobAdsRepository.All()
                .Where(ja => ja.IsActive && ja.PublishDate <= date)
                .ExecuteUpdateAsync(s => s.SetProperty(ja => ja.IsActive, ja => !ja.IsActive));
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

        private async Task<IEnumerable<CompanyJobAdViewModel>> GetFilteredCompanyAds(string userId, bool? active)
        {
            IQueryable<JobAdvertisementEntity> query = this.jobAdsRepository.DbSetNoTracking()
                .Where(ja => ja.Publisher.UserId == userId);

            if (active.HasValue)
            {
                query = query.Where(ja => ja.IsActive == active);
            }

            return await query.OrderByDescending(j => j.PublishDate)
                .To<CompanyJobAdViewModel>()
                .ToListAsync();
        }
    }
}
