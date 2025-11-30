using AutoMapper;
using JobFinder.Common.Exceptions;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.JobAd;
using JobFinder.Web.Models.AdApplication;

namespace JobFinder.Services.Implementations
{
    public class JobAdsApplicationsService : IJobAdsApplicationsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICloudImageManagementService cloudImageService;

        public JobAdsApplicationsService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            ICloudImageManagementService cloudImageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.cloudImageService = cloudImageService;
        }

        public async Task Create(JobAdApplicationInputModel jobAdApplication)
        {
            bool hasAlreadyApplied = await this.unitOfWork.JobAdApplicationsRepository
                .HasAlreadyApplied(
                    jobAdApplication.ApplicantId,
                    jobAdApplication.JobAdId);

            if (hasAlreadyApplied)
            {
                throw new ActionableException("You can not apply again for this job ad!");
            }

            JobAddApplicationInputDTO jobAdApplicationDto = this.mapper
                .Map<JobAddApplicationInputDTO>(jobAdApplication);

            await this.unitOfWork.JobAdApplicationsRepository.Create(jobAdApplicationDto);

            await this.unitOfWork.SaveChanges();
        }

        public async Task<IEnumerable<JobAdApplicationViewModel>> GetUserJobsAdApplications(
            string userId,
            int jobAdId)
        {
            await this.ValidateTheUserIsTheJobAdPublisher(userId, jobAdId);

            IEnumerable<JobAdApplicationDTO> jobAdApplications = await this.unitOfWork
                .JobAdApplicationsRepository
                .GetJobAdApplications<JobAdApplicationDTO>(jobAdId);

            return this.mapper.Map<IEnumerable<JobAdApplicationViewModel>>(jobAdApplications);
        }

        public async Task<IEnumerable<JobApplicationInfoViewModel>> GetCompanyJobAdApplications(
            string userId,
            int jobAdId)
        {
            await this.ValidateTheUserIsTheJobAdPublisher(userId, jobAdId);

            IEnumerable<JobAdApplicationInfoDTO> applications = await this.unitOfWork
                .JobAdApplicationsRepository
                .GetJobAdApplications<JobAdApplicationInfoDTO>(jobAdId);

            return this.mapper.Map<IEnumerable<JobApplicationInfoViewModel>>(applications);
        }

        public async Task<IEnumerable<JobAdApplicationViewModel>> GetAllMine(string userId)
        {
            IAsyncEnumerable<JobAdApplicationDTO> applications = this.unitOfWork
                .JobAdApplicationsRepository
                .GetUserApplications(userId);

            Queue<JobAdApplicationViewModel> models = new();

            await foreach (JobAdApplicationDTO application in applications)
            {
                JobAdApplicationViewModel model = this.mapper.Map<JobAdApplicationViewModel>(application);

                if (application.CompanyLogoId.HasValue)
                {
                    model.CompanyLogo = await this.cloudImageService
                        .GetThumbnailUrl(application.CompanyLogoId.Value);
                }

                models.Enqueue(model);
            }

            return models;
        }

        public async Task<PreviewInfoViewModel> SetPreviewInfo(string cvId, int jobAdId)
        {
            DateTime previewDate = await this.unitOfWork.JobAdApplicationsRepository
                .SetPreviewed(cvId, jobAdId);

            await this.unitOfWork.SaveChanges();

            return new PreviewInfoViewModel(previewDate);
        }

        private async Task ValidateTheUserIsTheJobAdPublisher(string userId, int jobAdId)
        {            
            string jobAdPublisherId = await this.unitOfWork
                .JobAdRepository
                .GetPublisher(jobAdId);

            if (jobAdPublisherId == null)
            {
                throw new ActionableException($"Job Advertisement with id {jobAdId} does not exist!");
            }

            if (jobAdPublisherId != userId)
            {
                throw new UnauthorizedAccessException(
                    "You are not authorized to access data for other users job advertisements!");
            }
        }
    }
}
