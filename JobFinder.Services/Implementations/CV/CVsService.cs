namespace JobFinder.Services.Implementations.CV
{
    using JobFinder.Services.CV;
    using System.Threading.Tasks;
    using JobFinder.Data.Models.CV;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Repositories.Contracts;
    using AutoMapper;
    using JobFinder.Web.Models.CVModels;
    using JobFinder.Data.Models.Cv;
    using JobFinder.Common.Exceptions;
    using JobFinder.Data.Models;

    public class CVsService : ICVsService
    {
        private readonly IRepository<CurriculumVitaeEntity> repository;
        private readonly IRepository<PersonalInfoEntity> personalDetailsRepo;
        private readonly IRepository<EducationInfoEntity> educationRepo;
        private readonly IRepository<WorkExperienceInfoEntity> workExperienceRepo;
        private readonly IRepository<LanguageInfoEntity> languageInfoRepo;
        private readonly IRepository<SkillsInfoEntity> skillRepo;
        private readonly IRepository<SkillsInfoDrivingCategoryEntity> skillsDrivingCategoriesRepo;
        private readonly IRepository<CourseCertificateEntity> courseSertificateRepo;
        private readonly IRepository<JobAdApplicationEntity> jobAdsApplicationsRepo;
        private readonly IMapper mapper;

        public CVsService(
            IRepository<CurriculumVitaeEntity> repository,
            IRepository<PersonalInfoEntity> personalDetailsRepo,
            IRepository<EducationInfoEntity> educationRepo,
            IRepository<WorkExperienceInfoEntity> workExperienceRepo,
            IRepository<LanguageInfoEntity> languageInfoRepo,
            IRepository<SkillsInfoEntity> skillRepo,
            IRepository<SkillsInfoDrivingCategoryEntity> skillsDrivingCategoriesRepo,
            IRepository<CourseCertificateEntity> courseSertificateRepo,
            IRepository<JobAdApplicationEntity> jobAdsApplicationsRepo,
            IMapper mapper)
        {
            this.repository = repository;
            this.personalDetailsRepo = personalDetailsRepo;
            this.educationRepo = educationRepo;
            this.workExperienceRepo = workExperienceRepo;
            this.languageInfoRepo = languageInfoRepo;
            this.skillRepo = skillRepo;
            this.skillsDrivingCategoriesRepo = skillsDrivingCategoriesRepo;
            this.courseSertificateRepo = courseSertificateRepo;
            this.jobAdsApplicationsRepo = jobAdsApplicationsRepo;
            this.mapper = mapper;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await this.repository.AnyAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string userId)
        {
            return await this.repository.DbSetNoTracking()
                .Where(c => c.UserId == userId)
                .To<T>()
                .ToListAsync();
        }

        public async Task<string> CreateAsync(CVCreateInputModel cvModel, string userId)
        {
            CurriculumVitaeEntity cvEntity = new();

            this.mapper.Map(cvModel, cvEntity);

            if (cvModel.Skills.DrivingLicenseCategoryIds.Any())
            {
                cvEntity.Skills.HasDrivingLicense = true;
                cvEntity.Skills.SkillsInfoDrivingCategories
                    .AddRange(
                        cvModel.Skills.DrivingLicenseCategoryIds
                        .Select(dcId => new SkillsInfoDrivingCategoryEntity { DrivingCategoryId = dcId })
                    );
            }

            cvEntity.UserId = userId;
            cvEntity.CreatedOn = DateTime.UtcNow;

            await this.repository.AddAsync(cvEntity);
            await this.repository.SaveChangesAsync();

            return cvEntity.Id;
        }

        public async Task<byte[]> GetCvDataAsync(string cvId)
        {
            byte[] data = await this.repository.DbSetNoTracking()
                .Where(cv => cv.Id == cvId)
                .Select(cv => cv.Data)
                .FirstOrDefaultAsync();

            return data;
        }

        public async Task<T> GetOwnCvDataAsync<T>(string cvId, string currentUserId)
        {
            string cvUserId = await this.repository
                .Where(cv => cv.Id == cvId)
                .Select(cv => cv.UserId)
                .SingleOrDefaultAsync();

            if (cvUserId == null)
            {
                throw new ActionableException("There is no cv with such id.");
            }

            if (cvUserId != currentUserId)
            {
                throw new UnauthorizedException("You are not allowed to access data for other user's cvs.");
            }

            T data = await this.repository.DbSetNoTracking()
                .Where(cv => cv.Id == cvId)
                .To<T>()
                .SingleOrDefaultAsync();

            return data;
        }

        public async Task<CvPreviewDataViewModel> GetUserCvData(string cvId, int jobAdId, string currentUserId)
        {
            await this.ValidateCvIsSentForCurrentUsersJobAd(cvId, jobAdId, currentUserId);

            CvPreviewDataViewModel cvData = await this.repository.DbSetNoTracking()
                .Where(cv => cv.Id == cvId)
                .To<CvPreviewDataViewModel>()
                .SingleOrDefaultAsync();

            return cvData;
        }

        public async Task<bool> SetDataAsync(string cvId, byte[] data)
        {
            var cvFromDb = await this.repository.FindAsync(cvId);
            if (cvFromDb == null)
            {
                return false;
            }

            cvFromDb.Data = data;
            await this.repository.SaveChangesAsync();

            return true;
        }

        public async Task DeleteCvAsync(string id)
        {
            CurriculumVitaeEntity cv = await this.repository.FindAsync(id);

            this.personalDetailsRepo.DeleteWhere(pd => pd.CurriculumVitaeId == cv.Id);
            this.educationRepo.DeleteWhere(ed => ed.CurriculumVitaeId == cv.Id);
            this.workExperienceRepo.DeleteWhere(we => we.CurriculumVitaeId == cv.Id);
            await this.DeleteSkillsInfo(cvId: id);
            this.languageInfoRepo.DeleteWhere(li => li.CurriculumVitaeId == cv.Id);
            this.courseSertificateRepo.DeleteWhere(cs => cs.CurriculumVitaeId == cv.Id);
            this.repository.Delete(cv);

            await this.repository.SaveChangesAsync();
        }

        public async Task<string> GetOwnerId(string cvId)
        {
            return await this.repository.DbSetNoTracking()
                .Where(cv => cv.Id == cvId)
                .Select(cv => cv.UserId)
                .FirstOrDefaultAsync();
        }

        public async Task ValidateCvIsSentForCurrentUsersJobAd(string cvId, int jobAdId, string currentUserId)
        {
            bool isCvSentForCurrentUsersJobAd = await this.jobAdsApplicationsRepo
                .AnyAsync(jaa => jaa.CurriculumVitaeId == cvId
                    && jaa.JobAdId == jobAdId
                    && jaa.JobAd.Publisher.UserId == currentUserId);

            if (!isCvSentForCurrentUsersJobAd)
            {
                throw new UnauthorizedAccessException("You are not allowed to view cv that has not been sent for one of your job ads.");
            }
        }

        private async Task DeleteSkillsInfo(string cvId)
        {
            SkillsInfoEntity skillsInfoEntity = await this.skillRepo
                .FirstOrDefaultAsync(s => s.CurriculumVitaeId == cvId);

            this.skillsDrivingCategoriesRepo
                .DeleteWhere(sdc => sdc.SkillsInfoId == skillsInfoEntity.Id);

            this.skillRepo.Delete(skillsInfoEntity);
        }
    }
}
