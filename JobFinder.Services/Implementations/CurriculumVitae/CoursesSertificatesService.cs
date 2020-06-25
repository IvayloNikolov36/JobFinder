namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesSertificatesService : DbService, ICoursesSertificatesService
    {
        public CoursesSertificatesService(JobFinderDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            var coursesSertificates = await this.DbContext.CoursesCertificates.AsNoTracking()
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

            await this.DbContext.AddAsync(courseSertificate);
            await this.DbContext.SaveChangesAsync();

            return courseSertificate.Id;
        }

        public async Task<bool> UpdateAsync(int id, string courseName, string certificateUrl)
        {
            var courseCertificateFromDb = await this.DbContext.FindAsync<CourseCertificate>(id);

            if (courseCertificateFromDb == null)
            {
                return false;
            }

            courseCertificateFromDb.CourseName = courseName;
            courseCertificateFromDb.CertificateUrl = certificateUrl;
            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var courseCertificateFromDb = await this.DbContext.FindAsync<CourseCertificate>(id);

            if (courseCertificateFromDb == null)
            {
                return false;
            }

            this.DbContext.Remove(courseCertificateFromDb);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

    }
}
