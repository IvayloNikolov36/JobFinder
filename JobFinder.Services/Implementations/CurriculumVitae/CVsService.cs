namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Services.CurriculumVitae;
    using System.Threading.Tasks;
    using JobFinder.Data.Models.CV;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using JobFinder.Services.Mappings;
    using JobFinder.Data.Repositories.Contracts;

    public class CVsService : ICVsService
    {
        private readonly IRepository<CurriculumVitae> repository;
        private readonly IRepository<PersonalDetails> personalDetailsRepo;
        private readonly IRepository<Education> educationRepo;
        private readonly IRepository<WorkExperience> workExperienceRepo;
        private readonly IRepository<Skill> skillRepo;
        private readonly IRepository<CourseCertificate> courseSertificateRepo;

        public CVsService(
            IRepository<CurriculumVitae> repository,
            IRepository<PersonalDetails> personalDetailsRepo,
            IRepository<Education> educationRepo,
            IRepository<WorkExperience> workExperienceRepo,
            IRepository<Skill> skillRepo,
            IRepository<CourseCertificate> courseSertificateRepo)
        {
            this.repository = repository;
            this.personalDetailsRepo = personalDetailsRepo;
            this.educationRepo = educationRepo;
            this.workExperienceRepo = workExperienceRepo;
            this.skillRepo = skillRepo;
            this.courseSertificateRepo = courseSertificateRepo;
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

        public async Task<string> CreateAsync(string userId, string name, string pictureUrl)
        {
            var cv = new CurriculumVitae
            {
                UserId = userId,
                Name = name,
                PictureUrl = pictureUrl
            };

            await this.repository.AddAsync(cv);
            await this.repository.SaveChangesAsync();

            return cv.Id;
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
            CurriculumVitae cv = await this.repository.FindAsync(id);

            if (cv.UserId != userId)
            {
                return false;
            }

            this.personalDetailsRepo.DeleteWhere(pd => pd.CurriculumVitaeId == cv.Id);
            this.educationRepo.DeleteWhere(ed => ed.CurriculumVitaeId == cv.Id);
            this.workExperienceRepo.DeleteWhere(we => we.CurriculumVitaeId == cv.Id);
            this.skillRepo.DeleteWhere(sk => sk.CurriculumVitaeId == cv.Id);
            this.courseSertificateRepo.DeleteWhere(cs => cs.CurriculumVitaeId == cv.Id);
            this.repository.Delete(cv);

            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
