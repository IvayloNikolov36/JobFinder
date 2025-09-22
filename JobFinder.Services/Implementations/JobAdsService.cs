using AutoMapper;
using JobFinder.Business.JobAds;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Transfer.DTOs.JobAd;
using JobFinder.Web.Models.AnonymousProfile;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.JobAds;
using JobFinder.Web.Models.Subscriptions;

namespace JobFinder.Services.Implementations
{
    public class JobAdsService : IJobAdsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IJobAdsRules jobAdsRules;
        private readonly IMapper mapper;

        public JobAdsService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IJobAdsRules jobAdsRules,
            IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.jobAdsRules = jobAdsRules;
            this.mapper = mapper;
        }

        public async Task<JobAdDetailsViewModel> Get(int id)
        {
            CompanyJobAdDetailsDTO jobAd = await this.unitOfWork.JobAdRepository.Get(id);

            return this.mapper.Map<JobAdDetailsViewModel>(jobAd);
        }

        public async Task<IdentityViewModel<int>> Create(JobAdCreateViewModel jobAd, string userId)
        {
            Task<int> companyId = this.unitOfWork.CompanyRepository.GetCompanyId(userId);

            this.ValidateJobAdProperties(jobAd);

            JobAdCreateDTO jobAdDto = this.mapper.Map<JobAdCreateDTO>(jobAd);

            await this.unitOfWork.JobAdRepository.Create(jobAdDto, await companyId);

            await this.unitOfWork.SaveChanges<JobAdCreateDTO, int>(jobAdDto);

            return new IdentityViewModel<int>(jobAdDto.Id);
        }

        public async Task Update(int jobAdId, JobAdEditModel jobAd)
        {
            this.ValidateJobAdProperties(jobAd);

            JobAdEditDTO jobAdDto = this.mapper.Map<JobAdEditDTO>(jobAd);

            await this.unitOfWork.JobAdRepository.Update(jobAdId, jobAdDto);

            await this.unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<CompanyJobAdViewModel>> GetCompanyAds(
            string userId,
            int? lifecycleStatus = null)
        {
            IEnumerable<CompanyJobAdDTO> jobAds = await this.unitOfWork
                .JobAdRepository
                .GetFilteredCompanyAds(userId, lifecycleStatus);

            return this.mapper.Map<IEnumerable<CompanyJobAdViewModel>>(jobAds);
        }

        public async Task<DataListingsModel<JobListingViewModel>> AllActiveAsync(JobAdsFilterModel filter)
        {
            JobAdFilterDTO filterDto = this.mapper.Map<JobAdFilterDTO>(filter);

            DataListingDTO<JobAdListingDTO> result = await this.unitOfWork
                .JobAdRepository
                .AllActive(filterDto);

            return new DataListingsModel<JobListingViewModel>(
                result.TotalCount,
                this.mapper.Map<IEnumerable<JobListingViewModel>>(result.Data));
        }

        public async Task<JobAdDetailsForSubscriber> GetDetails(int jobAdId)
        {
            JobAdDetailsForSubscriberDTO detailsDto = await this.unitOfWork
                .JobAdRepository
                .GetDetailsForSubscriber(jobAdId);

            JobAdDetailsForSubscriber model = this.mapper.Map<JobAdDetailsForSubscriber>(detailsDto);

            model.Salary = this.jobAdsRules.GenerateSalaryText(
                detailsDto.MinSalary,
                detailsDto.MaxSalary,
                detailsDto.CurrencyName);

            return model;
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

        public async Task<IEnumerable<AnonymousProfileListingViewModel>> GetRelevantAnonymousProfiles(int jobAdId)
        {
            JobAdCriteriasDTO jobAdCriterias = await this.unitOfWork
                .JobAdRepository
                .GetJobAdCriterias(jobAdId);

            IEnumerable<AnonymousProfileListingDTO> anonymousProfiles = await this.unitOfWork
                .AnonymousProfileRepository
                .GetProfilesRelevantToJobAd(jobAdCriterias);

            return this.mapper
                .Map<IEnumerable<AnonymousProfileListingViewModel>>(anonymousProfiles);
        }

        public async Task<JobAdCriteriasViewModel> GetJobAdCriterias(int jobAdId)
        {
            JobAdCriteriasDTO jobAdCriterias = await this.unitOfWork
                .JobAdRepository
                .GetJobAdCriterias(jobAdId);

            return this.mapper.Map<JobAdCriteriasViewModel>(jobAdCriterias);
        }

        public async Task Retire(int id)
        {
            await this.unitOfWork.JobAdRepository.Retire(id);

            await this.unitOfWork.SaveChanges();
        }

        private void ValidateJobAdProperties<T>(T jobAd) where T : JobAdBaseViewModel
        {
            SalaryPropertiesDTO salaryProperties = this.mapper.Map<SalaryPropertiesDTO>(jobAd);

            this.jobAdsRules.ValidateSalaryProperties(salaryProperties);

            this.jobAdsRules.ValidateIntership(jobAd.Intership, jobAd.JobEngagementId);

            JobAdCategoryDTO adCategoryDto = this.mapper.Map<JobAdCategoryDTO>(jobAd);

            this.jobAdsRules.ValidateJobCategoryAndRelatedData(adCategoryDto);
        }   
    }
}
