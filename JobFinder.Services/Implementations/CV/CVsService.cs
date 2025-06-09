using JobFinder.Services.CV;
using AutoMapper;
using JobFinder.Web.Models.CVModels;
using JobFinder.Common.Exceptions;
using JobFinder.DataAccess.UnitOfWork;
using JobFinder.Transfer.DTOs.CV;
using JobFinder.Business.CourseCertificatesInfo;

namespace JobFinder.Services.Implementations.CV
{
    public class CVsService : ICVsService
    {
        private readonly IEntityFrameworkUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ICourseCertificateInfoRules courceCertificateInfoRules;

        public CVsService(
            IEntityFrameworkUnitOfWork unitOfWork,
            IMapper mapper,
            ICourseCertificateInfoRules courceCertificateInfoRules)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.courceCertificateInfoRules = courceCertificateInfoRules;
        }

        public async Task<IEnumerable<CvListingModel>> All(string userId)
        {
            IEnumerable<CVListingDTO> cvDtos = await this.unitOfWork
                .CurriculumVitaeRepository
                .All(userId);

            return this.mapper.Map<IEnumerable<CvListingModel>>(cvDtos);
        }

        public async Task<string> CreateAsync(CVCreateInputModel cvModel, string userId)
        {
            IEnumerable<CourseCertificateInputDTO> courceCertificateInfoDtos = this.mapper
                .Map<IEnumerable<CourseCertificateInputDTO>>(cvModel.CourseCertificates);

            this.courceCertificateInfoRules.ValidateData(courceCertificateInfoDtos);

            CVCreateDTO cvDataDto = this.mapper.Map<CVCreateDTO>(cvModel);

            await this.unitOfWork.CurriculumVitaeRepository.Create(userId, cvDataDto);

            await this.unitOfWork.SaveChanges<CVCreateDTO, string>(cvDataDto);

            return cvDataDto.Id;
        }

        public async Task<MyCvDataViewModel> GetOwnCvData(string cvId, string userId)
        {
            MyCvDataDTO cvDataDto = await this.unitOfWork.CurriculumVitaeRepository
                .GetCvData<MyCvDataDTO>(cvId);

            MyCvDataViewModel cvData = this.mapper.Map<MyCvDataViewModel>(cvDataDto);

            bool hasAnyAnonymousProfileActivated = await this.unitOfWork.AnonymousProfileRepository
                .HasAnonymousProfile(userId);

            cvData.CanActivateAnonymousProfile = !hasAnyAnonymousProfileActivated;

            return cvData;
        }

        public async Task<CvPreviewDataViewModel> GetUserCvData(string cvId)
        {
            CvPreviewDataViewModel cvData = await this.unitOfWork
                .CurriculumVitaeRepository
                .GetCvData<CvPreviewDataViewModel>(cvId);

            return cvData;
        }

        public async Task Delete(string cvId)
        {
            // TODO: if it is has been sent as an application???

            this.unitOfWork.PersonalInfoRepository.Delete(cvId);
            this.unitOfWork.EducationInfoRepository.Delete(cvId);
            this.unitOfWork.WorkExperienceRepository.Delete(cvId);
            this.unitOfWork.LanguageInfoRepository.Delete(cvId);
            this.unitOfWork.CoursesCertificateInfoRepository.Delete(cvId);
            this.unitOfWork.SkillsInfoDrivingCategoryRepository.Delete(cvId);
            await this.unitOfWork.SkillsInfoRepository.Delete(cvId);

            // TODO: check it
            //await this.unitOfWork.AnonymousProfileRepository.Delete(cvId);

            await this.unitOfWork.CurriculumVitaeRepository.Delete(cvId);

            await this.unitOfWork.SaveChanges();
        }

        public async Task<string> GetOwnerId(string cvId)
        {
            return await this.unitOfWork.CurriculumVitaeRepository.GetUserId(cvId);
        }

        public async Task ValidateApplicationIsSentForCurrentUserJobAd(string cvId, int jobAdId, string currentUserId)
        {
            bool isCvSentForCurrentUsersJobAd = await this.unitOfWork
                .JobAdApplicationsRepository
                .IsApplicationSent(cvId, jobAdId, currentUserId);

            if (!isCvSentForCurrentUsersJobAd)
            {
                throw new ActionableException("You are not allowed to access data for job applications sent to other companies ads!");
            }

            return;
        }
    }
}
