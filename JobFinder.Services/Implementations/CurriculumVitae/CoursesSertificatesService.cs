namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesSertificatesService : ICoursesSertificatesService
    {
        private readonly IRepository<CourseCertificate> repository;

        public CoursesSertificatesService(IRepository<CourseCertificate> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            var coursesSertificates = await this.repository.AllAsNoTracking()
                .Where(cs => cs.CurriculumVitaeId == cvId)
                .To<T>()
                .ToListAsync();

            return coursesSertificates;
        }

        public async Task<int> AddAsync(string cvId, string courseName, string certificateUrl)
        {
            var courseSertificate = new CourseCertificate
            {
                CurriculumVitaeId = cvId,
                CourseName = courseName,
                CertificateUrl = certificateUrl
            };

            await this.repository.AddAsync(courseSertificate);
            await this.repository.SaveChangesAsync();

            return courseSertificate.Id;
        }

        public async Task<bool> UpdateAsync(int id, string courseName, string certificateUrl)
        {
            var courseCertificateFromDb = await this.repository.FindAsync(id);

            if (courseCertificateFromDb == null)
            {
                return false;
            }

            courseCertificateFromDb.CourseName = courseName;
            courseCertificateFromDb.CertificateUrl = certificateUrl;
            await this.repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var courseCertificateFromDb = await this.repository.FindAsync(id);

            if (courseCertificateFromDb == null)
            {
                return false;
            }

            this.repository.Delete(courseCertificateFromDb);
            await this.repository.SaveChangesAsync();

            return true;
        }

    }
}
