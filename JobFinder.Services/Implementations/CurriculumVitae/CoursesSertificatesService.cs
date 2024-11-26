namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesSertificatesService : ICoursesSertificatesService
    {
        private readonly IRepository<CourseCertificate> repository;
        private readonly IMapper mapper;

        public CoursesSertificatesService(
            IRepository<CourseCertificate> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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

        public async Task UpdateAsync(string cvId, IEnumerable<CourseSertificateEditModel> coursesInfoModels)
        {
            List<CourseCertificate> courceEntitiesFromDb = await this.repository
                .AllWhere(we => we.CurriculumVitaeId == cvId)
                .ToListAsync();

            IEnumerable<CourseSertificateEditModel> coursesToAdd = coursesInfoModels
                .Where(m => !courceEntitiesFromDb.Any(ce => ce.Id == m.Id));

            if (coursesToAdd.Any())
            {
                List<CourseCertificate> entitiesToAdd = new();
                foreach (CourseSertificateEditModel model in coursesToAdd)
                {
                    CourseCertificate entityToAdd = this.mapper.Map<CourseCertificate>(model);
                    entityToAdd.Id = 0;
                    entityToAdd.CurriculumVitaeId = cvId;
                    entitiesToAdd.Add(entityToAdd);
                }

                await this.repository.AddRangeAsync(entitiesToAdd);
            }

            IEnumerable<CourseCertificate> entitiesToRemove = courceEntitiesFromDb
                .Where(ce => !coursesInfoModels.Any(m => m.Id == ce.Id));

            if (entitiesToRemove.Any())
            {
                this.repository.DeleteRange(entitiesToRemove);
            }

            IEnumerable<CourseCertificate> entitiesToUpdate = courceEntitiesFromDb
                .Where(ce => coursesInfoModels.Any(m => m.Id == ce.Id));

            if (entitiesToUpdate.Any())
            {
                foreach (CourseCertificate item in entitiesToUpdate)
                {
                    CourseSertificateEditModel correspondingModel = coursesInfoModels
                        .First(m => m.Id == item.Id);

                    this.mapper.Map(correspondingModel, item);
                }

                this.repository.UpdateRange(entitiesToUpdate);
            }

            await this.repository.SaveChangesAsync();
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
