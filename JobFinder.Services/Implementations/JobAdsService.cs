﻿using AutoMapper;
using JobFinder.Business.JobAds;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Transfer.DTOs.JobAd;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.JobAds;
using JobFinder.Web.Models.Subscriptions;

namespace JobFinder.Services.Implementations
{
    public class JobAdsService : IJobAdsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IJobAdvertisementsRules jobAdsRules;
        private readonly IMapper mapper;

        public JobAdsService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IJobAdvertisementsRules jobAdsRules,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.jobAdsRules = jobAdsRules;
            this.mapper = mapper;
        }

        public async Task<JobAdDetailsViewModel> Get(int id)
        {
            JobAdDetailsDTO jobAd = await this.unitOfWork.JobAdRepository.Get(id);

            return this.mapper.Map<JobAdDetailsViewModel>(jobAd);
        }

        public async Task Create(JobAdCreateViewModel jobAd, int companyId)
        {
            SalaryPropertiesDTO salaryProperties = this.mapper.Map<SalaryPropertiesDTO>(jobAd);

            this.jobAdsRules.ValidateSalaryProperties(salaryProperties);

            this.jobAdsRules.ValidateIntership(jobAd.Intership, jobAd.JobEngagementId);

            JobAdCreateDTO jobAdDto = this.mapper.Map<JobAdCreateDTO>(jobAd);

            await this.unitOfWork.JobAdRepository.Create(jobAdDto, companyId);

            await this.unitOfWork.SaveChanges();
        }

        public async Task Update(int jobAdId, string userId, JobAdEditModel editModel)
        {
            JobAdEditDTO jobAdDto = this.mapper.Map<JobAdEditDTO>(editModel);

            await this.unitOfWork.JobAdRepository.Update(jobAdId, jobAdDto);

            await this.unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<CompanyJobAdViewModel>> GetAllCompanyAds(string userId)
        {
            return await this.GetFilteredCompanyAds(userId, null);
        }

        public async Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(string userId, bool active)
        {
            return await this.GetFilteredCompanyAds(userId, active);
        }

        public async Task<DataListingsModel<JobListingModel>> AllActiveAsync(JobAdsFilterModel filter)
        {
            JobAdFilterDTO filterDto = this.mapper.Map<JobAdFilterDTO>(filter);

            DataListingDTO<JobAdListingDTO> result = await this.unitOfWork
                .JobAdRepository
                .AllActive(filterDto);

            return new DataListingsModel<JobListingModel>(
                result.TotalCount,
                this.mapper.Map<IEnumerable<JobListingModel>>(result.Data));
        }

        public async Task<JobAdDetailsForSubscriber> GetDetails(int jobAdId)
        {
            JobAdDetailsForSubscriberDTO detailsDto = await this.unitOfWork
                .JobAdRepository
                .GetDetailsForSubscriber(jobAdId);

            return this.mapper.Map<JobAdDetailsForSubscriber>(detailsDto);
        }

        public async Task DeactivateAds()
        {
            int expirationAfterDays = this.jobAdsRules.GetDaysExpiration();

            DateTime dateTreshold = DateTime.UtcNow.AddDays(-expirationAfterDays);

            await this.unitOfWork.JobAdRepository.ExecuteJobAdsDeactivate(dateTreshold);
        }

        public async Task<string> GetPublisherId(int jobAdId)
        {
            return await this.unitOfWork.JobAdRepository.GetPublisher(jobAdId);
        }

        private async Task<IEnumerable<CompanyJobAdViewModel>> GetFilteredCompanyAds(string userId, bool? active)
        {
            IEnumerable<CompanyJobAdDTO> jobAds = await this.unitOfWork
                .JobAdRepository
                .GetFilteredCompanyAds(userId, active);

            return this.mapper.Map<IEnumerable<CompanyJobAdViewModel>>(jobAds);
        }
    }
}
