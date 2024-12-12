namespace JobFinder.Services.Implementations.CV
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CV;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class WorkExperienceInfosService : IWorkExperienceInfosService
    {
        private readonly IRepository<WorkExperienceInfoEntity> repository;
        private readonly IMapper mapper;

        public WorkExperienceInfosService(
            IRepository<WorkExperienceInfoEntity> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<UpdateResult> UpdateAsync(string cvId, IEnumerable<WorkExperienceEditModel> workExperienceModels)
        {
            List<WorkExperienceInfoEntity> workExperienceEntitiesFromDB = await this.repository
                .Where(we => we.CurriculumVitaeId == cvId)
                .ToListAsync();

            IEnumerable<WorkExperienceEditModel> workExperinceToAdd = workExperienceModels
                .Where(wem => !workExperienceEntitiesFromDB.Any(wee => wee.Id == wem.Id));

            List<WorkExperienceInfoEntity> entitiesToAdd = null;
            if (workExperinceToAdd.Any())
            {
                entitiesToAdd = new List<WorkExperienceInfoEntity>();
                foreach (WorkExperienceEditModel model in workExperinceToAdd)
                {
                    WorkExperienceInfoEntity entityToAdd = this.mapper.Map<WorkExperienceInfoEntity>(model);
                    entityToAdd.Id = 0;
                    entityToAdd.CurriculumVitaeId = cvId;
                    entitiesToAdd.Add(entityToAdd);
                }

                await this.repository.AddRangeAsync(entitiesToAdd);
            }

            IEnumerable<WorkExperienceInfoEntity> entitiesToRemove = workExperienceEntitiesFromDB
                .Where(we => !workExperienceModels.Any(wem => wem.Id == we.Id));

            if (entitiesToRemove.Any())
            {
                this.repository.DeleteRange(entitiesToRemove);
            }

            IEnumerable<WorkExperienceInfoEntity> entitiesToUpdate = workExperienceEntitiesFromDB
                .Where(we => workExperienceModels.Any(m => m.Id == we.Id));

            if (entitiesToUpdate.Any())
            {
                foreach (WorkExperienceInfoEntity item in entitiesToUpdate)
                {
                    WorkExperienceEditModel correspondingModel = workExperienceModels
                        .First(m => m.Id == item.Id);

                    this.mapper.Map(correspondingModel, item);
                }

                this.repository.UpdateRange(entitiesToUpdate);
            }

            await this.repository.SaveChangesAsync();

            return new UpdateResult(entitiesToAdd);
        }
    }
}
