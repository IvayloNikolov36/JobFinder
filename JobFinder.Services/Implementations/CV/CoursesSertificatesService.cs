namespace JobFinder.Services.Implementations.CV
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.DataAccess.Generic;
    using JobFinder.Services.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CoursesSertificatesService : ICoursesSertificatesService
    {
        private readonly IRepository<CourseCertificateEntity> repository;
        private readonly IMapper mapper;

        public CoursesSertificatesService(
            IRepository<CourseCertificateEntity> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            return await this.repository.DbSetNoTracking()
                .Where(cs => cs.CurriculumVitaeId == cvId)
                .To<T>()
                .ToListAsync();
        }

        public async Task<UpdateResult> UpdateAsync(string cvId, IEnumerable<CourseSertificateEditModel> coursesInfoModels)
        {
            List<CourseCertificateEntity> courceEntitiesFromDb = await this.repository
                .Where(we => we.CurriculumVitaeId == cvId)
                .ToListAsync();

            IEnumerable<CourseSertificateEditModel> coursesToAdd = coursesInfoModels
                .Where(m => !courceEntitiesFromDb.Any(ce => ce.Id == m.Id));

            List<CourseCertificateEntity> entitiesToAdd = null;
            if (coursesToAdd.Any())
            {
                entitiesToAdd = new List<CourseCertificateEntity>();
                foreach (CourseSertificateEditModel model in coursesToAdd)
                {
                    CourseCertificateEntity entityToAdd = this.mapper.Map<CourseCertificateEntity>(model);
                    entityToAdd.Id = 0;
                    entityToAdd.CurriculumVitaeId = cvId;
                    entitiesToAdd.Add(entityToAdd);
                }

                await this.repository.AddRangeAsync(entitiesToAdd);
            }

            IEnumerable<CourseCertificateEntity> entitiesToRemove = courceEntitiesFromDb
                .Where(ce => !coursesInfoModels.Any(m => m.Id == ce.Id));

            if (entitiesToRemove.Any())
            {
                this.repository.DeleteRange(entitiesToRemove);
            }

            IEnumerable<CourseCertificateEntity> entitiesToUpdate = courceEntitiesFromDb
                .Where(ce => coursesInfoModels.Any(m => m.Id == ce.Id));

            if (entitiesToUpdate.Any())
            {
                foreach (CourseCertificateEntity item in entitiesToUpdate)
                {
                    CourseSertificateEditModel correspondingModel = coursesInfoModels
                        .First(m => m.Id == item.Id);

                    this.mapper.Map(correspondingModel, item);
                }

                this.repository.UpdateRange(entitiesToUpdate);
            }

            await this.repository.SaveChangesAsync();

            return new UpdateResult(entitiesToAdd);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            CourseCertificateEntity courseCertificateFromDb = await this.repository.FindAsync(id);

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
