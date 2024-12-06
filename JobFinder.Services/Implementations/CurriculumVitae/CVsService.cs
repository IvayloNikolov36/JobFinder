namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using System.Threading.Tasks;
    using JobFinder.Data.Models.CV;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Web.Models.Common;
    using AutoMapper;
    using JobFinder.Web.Models.CVModels;

    public class CVsService : ICVsService
    {
        private readonly IRepository<CurriculumVitae> repository;
        private readonly IRepository<PersonalInfo> personalDetailsRepo;
        private readonly IRepository<Education> educationRepo;
        private readonly IRepository<WorkExperience> workExperienceRepo;
        private readonly IRepository<LanguageInfo> languageInfoRepo;
        private readonly IRepository<Skill> skillRepo;
        private readonly IRepository<CourseCertificate> courseSertificateRepo;
        private readonly IMapper mapper;

        public CVsService(
            IRepository<CurriculumVitae> repository,
            IRepository<PersonalInfo> personalDetailsRepo,
            IRepository<Education> educationRepo,
            IRepository<WorkExperience> workExperienceRepo,
            IRepository<LanguageInfo> languageInfoRepo,
            IRepository<Skill> skillRepo,
            IRepository<CourseCertificate> courseSertificateRepo,
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

        public async Task<BasicViewModel> CreateAsync(CVCreateInputModel cvModel, string userId)
        {
            CurriculumVitae cvEntity = new();

            this.mapper.Map(cvModel, cvEntity);
            cvEntity.UserId = userId;
            cvEntity.CreatedOn = DateTime.UtcNow;

            await this.repository.AddAsync(cvEntity);
            await this.repository.SaveChangesAsync();

            return new BasicViewModel(cvEntity.Id, cvEntity.Name);
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

        // TODO: this is because the above generic method is not working - automapper throws error
        public async Task<CvDataViewModel> GetDataAsync(string cvId)
        {
            return await this.repository.All()
                .Include(cv => cv.PersonalDetails)
                    .ThenInclude(pd => pd.Gender)
                .Include(cv => cv.PersonalDetails)
                    .ThenInclude(pd => pd.Citizenship)
                .Include(cv => cv.PersonalDetails)
                    .ThenInclude(pd => pd.Country)
                .Include(cv => cv.Educations)
                    .ThenInclude(e => e.EducationLevel)
                .Include(cv => cv.CourseCertificates)
                .Include(cv => cv.LanguagesInfo)
                    .ThenInclude(li => li.LanguageType)
                .Include(cv => cv.LanguagesInfo)
                    .ThenInclude(li => li.ComprehensionLevel)
                .Include(cv => cv.LanguagesInfo)
                    .ThenInclude(li => li.SpeakingLevel)
                .Include(cv => cv.LanguagesInfo)
                    .ThenInclude(li => li.WritingLevel)
                .Include(cv => cv.WorkExperiences)
                    .ThenInclude(we => we.BusinessSector)
                .Include(cv => cv.Skills)
                .Where(cv => cv.Id == cvId)
                .Select(x => this.mapper.Map<CvDataViewModel>(x))
                .SingleOrDefaultAsync();
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
            CurriculumVitae cv = await this.repository.FindAsync(id);

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
