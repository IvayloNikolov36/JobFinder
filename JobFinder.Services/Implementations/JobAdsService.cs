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
            JobAdDetailsDTO jobAd = await this.unitOfWork.JobAdRepository.Get(id);

            return this.mapper.Map<JobAdDetailsViewModel>(jobAd);
        }

        public async Task<int> Create(JobAdCreateViewModel jobAd, string userId)
        {
            Task<int> companyId = this.unitOfWork.CompanyRepository.GetCompanyId(userId);

            SalaryPropertiesDTO salaryProperties = this.mapper.Map<SalaryPropertiesDTO>(jobAd);

            this.jobAdsRules.ValidateSalaryProperties(salaryProperties);

            this.jobAdsRules.ValidateIntership(jobAd.Intership, jobAd.JobEngagementId);

            JobAdCategoryDTO adCategoryDto = this.mapper.Map<JobAdCategoryDTO>(jobAd);

            this.jobAdsRules.ValidateJobCategoryAndRelatedData(adCategoryDto);

            JobAdCreateDTO jobAdDto = this.mapper.Map<JobAdCreateDTO>(jobAd);

            await this.unitOfWork.JobAdRepository.Create(jobAdDto, await companyId);

            await this.unitOfWork.SaveChanges<JobAdCreateDTO, int>(jobAdDto);

            return jobAdDto.Id;
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

        private async Task<IEnumerable<CompanyJobAdViewModel>> GetFilteredCompanyAds(string userId, bool? active)
        {
            IEnumerable<CompanyJobAdDTO> jobAds = await this.unitOfWork
                .JobAdRepository
                .GetFilteredCompanyAds(userId, active);

            return this.mapper.Map<IEnumerable<CompanyJobAdViewModel>>(jobAds);
        }
    }
}
