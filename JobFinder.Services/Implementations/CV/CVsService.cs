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

    public class CVsService : ICVsService
    {
        private readonly IRepository<CurriculumVitaeEntity> repository;
        private readonly IRepository<PersonalInfoEntity> personalDetailsRepo;
        private readonly IRepository<EducationInfoEntity> educationRepo;
        private readonly IRepository<WorkExperienceInfoEntity> workExperienceRepo;
        private readonly IRepository<LanguageInfoEntity> languageInfoRepo;
        private readonly IRepository<SkillsInfoEntity> skillRepo;
        private readonly IRepository<CourseCertificateEntity> courseSertificateRepo;
        private readonly IMapper mapper;

        public CVsService(
            IRepository<CurriculumVitaeEntity> repository,
            IRepository<PersonalInfoEntity> personalDetailsRepo,
            IRepository<EducationInfoEntity> educationRepo,
            IRepository<WorkExperienceInfoEntity> workExperienceRepo,
            IRepository<LanguageInfoEntity> languageInfoRepo,
            IRepository<SkillsInfoEntity> skillRepo,
            IRepository<CourseCertificateEntity> courseSertificateRepo,
            IMapper mapper)
        {
            this.repository = repository;
            this.personalDetailsRepo = personalDetailsRepo;
            this.educationRepo = educationRepo;
            this.workExperienceRepo = workExperienceRepo;
            this.languageInfoRepo = languageInfoRepo;
            this.skillRepo = skillRepo;
            this.courseSertificateRepo = courseSertificateRepo;
            this.mapper = mapper;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var entity = await this.repository.FindAsync(id);

            return entity != null;
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string userId)
        {
            return await this.repository.AllAsNoTracking()
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
            byte[] data = await this.repository.AllAsNoTracking()
                .Where(cv => cv.Id == cvId)
                .Select(cv => cv.Data)
                .FirstOrDefaultAsync();

            return data;
        }

        public async Task<T> GetDataAsync<T>(string cvId)
        {
            T data = await this.repository.AllAsNoTracking()
                .Where(cv => cv.Id == cvId)
                .To<T>()
                .SingleOrDefaultAsync();

            return data;
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

        public async Task<bool> DeleteCvAsync(string id, string userId)
        {
            CurriculumVitaeEntity cv = await this.repository.FindAsync(id);

            if (cv.UserId != userId)
            {
                return false;
            }

            this.personalDetailsRepo.DeleteWhere(pd => pd.CurriculumVitaeId == cv.Id);
            this.educationRepo.DeleteWhere(ed => ed.CurriculumVitaeId == cv.Id);
            this.workExperienceRepo.DeleteWhere(we => we.CurriculumVitaeId == cv.Id);
            this.skillRepo.DeleteWhere(sk => sk.CurriculumVitaeId == cv.Id);
            this.languageInfoRepo.DeleteWhere(li => li.CurriculumVitaeId == cv.Id);
            this.courseSertificateRepo.DeleteWhere(cs => cs.CurriculumVitaeId == cv.Id);
            this.repository.Delete(cv);

            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
