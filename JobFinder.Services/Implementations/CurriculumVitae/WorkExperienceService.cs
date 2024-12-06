namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IRepository<WorkExperience> repository;
        private readonly IMapper mapper;

        public WorkExperienceService(
            IRepository<WorkExperience> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            return await this.repository.AllAsNoTracking()
                .Where(we => we.CurriculumVitaeId == cvId)
                .To<T>()
                .ToListAsync();
        }

        public async Task<bool> DeleteAsync(int workExperieceId)
        {
            var workExperience = await this.repository.FindAsync(workExperieceId);
            if (workExperience == null)
            {
                return false;
            }

            this.repository.Delete(workExperience);
            await this.repository.SaveChangesAsync();

            return true;
        }

        public async Task UpdateAsync(string cvId, IEnumerable<WorkExperienceEditModel> workExperienceModels)
        {
            List<WorkExperience> workExperienceEntitiesFromDB = await this.repository
                .AllWhere(we => we.CurriculumVitaeId == cvId)
                .ToListAsync();

            IEnumerable<WorkExperienceEditModel> workExperinceToAdd = workExperienceModels
                .Where(wem => !workExperienceEntitiesFromDB.Any(wee => wee.Id == wem.Id));

            if (workExperinceToAdd.Any())
            {
                List<WorkExperience> entitiesToAdd = new();
                foreach (WorkExperienceEditModel model in workExperinceToAdd)
                {
                    WorkExperience entityToAdd = this.mapper.Map<WorkExperience>(model);
                    entityToAdd.Id = 0;
                    entityToAdd.CurriculumVitaeId = cvId;
                    entitiesToAdd.Add(entityToAdd);
                }

                await this.repository.AddRangeAsync(entitiesToAdd);
            }

            IEnumerable<WorkExperience> entitiesToRemove = workExperienceEntitiesFromDB
                .Where(we => !workExperienceModels.Any(wem => wem.Id == we.Id));

            if (entitiesToRemove.Any())
            {
                this.repository.DeleteRange(entitiesToRemove);
            }

            IEnumerable<WorkExperience> entitiesToUpdate = workExperienceEntitiesFromDB
                .Where(we => workExperienceModels.Any(m => m.Id == we.Id));

            if (entitiesToUpdate.Any())
            {
                foreach (WorkExperience item in entitiesToUpdate)
                {
                    WorkExperienceEditModel correspondingModel = workExperienceModels
                        .First(m => m.Id == item.Id);

                    this.mapper.Map(correspondingModel, item);
                }

                this.repository.UpdateRange(entitiesToUpdate);
            }

            await this.repository.SaveChangesAsync();
        }
    }
}
