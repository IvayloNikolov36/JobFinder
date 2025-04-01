namespace JobFinder.Services.Implementations.CV
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.DataAccess.Generic;  
    using JobFinder.Services.CV;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EducationInfosService : IEducationInfosService
    {
        private readonly IRepository<EducationInfoEntity> repository;
        private readonly IMapper mapper;

        public EducationInfosService(
            IRepository<EducationInfoEntity> educationRepository,
            IMapper mapper) 
        {
            this.repository = educationRepository;
            this.mapper = mapper;
        }
        
        public async Task<UpdateResult> UpdateAsync(string cvId, IEnumerable<EducationEditModel> educationModels)
        {
            List<EducationInfoEntity> educationEntitiesFromDb = await this.repository
                .Where(e => e.CurriculumVitaeId == cvId)
                .ToListAsync();

            IEnumerable<EducationEditModel> educationsToAdd = educationModels
                .Where(em => !educationEntitiesFromDb.Any(ee => ee.Id == em.Id));

            List<EducationInfoEntity> educationEntitiesToAdd = null;
            if (educationsToAdd.Any())
            {
                educationEntitiesToAdd = new List<EducationInfoEntity>();
                foreach (EducationEditModel educationEditModel in educationsToAdd)
                {
                    EducationInfoEntity educationEntity = this.mapper.Map<EducationInfoEntity>(educationEditModel);
                    educationEntity.Id = 0;
                    educationEntity.CurriculumVitaeId = cvId;
                    educationEntitiesToAdd.Add(educationEntity);
                }

                await this.repository.AddRangeAsync(educationEntitiesToAdd);
            }

            IEnumerable<EducationInfoEntity> educationEntitiesToRemove = educationEntitiesFromDb
                .Where(ee => !educationModels.Any(em => em.Id == ee.Id));

            if (educationEntitiesToRemove.Any())
            {
                this.repository.DeleteRange(educationEntitiesToRemove);
            }

            IEnumerable<EducationInfoEntity> educationsToUpdate = educationEntitiesFromDb
                .Where(ee => educationModels.Any(em => em.Id == ee.Id));

            if (educationsToUpdate.Any())
            {
                foreach (EducationInfoEntity educationEntityToUpdate in educationsToUpdate)
                {
                    EducationEditModel correspondingModel = educationModels
                        .First(em => em.Id == educationEntityToUpdate.Id);

                    this.mapper.Map(correspondingModel, educationEntityToUpdate);
                }

                this.repository.UpdateRange(educationsToUpdate);
            }

            await this.repository.SaveChangesAsync();

            return new UpdateResult(educationEntitiesToAdd);
        }
    }
}
