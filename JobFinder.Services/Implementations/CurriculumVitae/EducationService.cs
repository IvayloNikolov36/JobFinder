namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EducationService : IEducationService
    {
        private readonly IRepository<Education> repository;
        private readonly IMapper mapper;

        public EducationService(
            IRepository<Education> educationRepository,
            IMapper mapper) 
        {
            this.repository = educationRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            var educations = await this.repository.AllAsNoTracking()
                .Where(e => e.CurriculumVitaeId == cvId)
                .To<T>()
                .ToListAsync();

            return educations;
        }

        public async Task<int> CreateAsync(string cvId, DateTime fromDate, DateTime? toDate, string organization,
            string location, EducationLevel educationLevel, string major, string mainSubjects)
        {
            var education = new Education
            {
                CurriculumVitaeId = cvId,
                FromDate = fromDate,
                ToDate = toDate,
                Organization = organization,
                Location = location,
                EducationLevel = educationLevel,
                Major = major,
                MainSubjects = mainSubjects
            };

            await this.repository.AddAsync(education);
            await this.repository.SaveChangesAsync();

            return education.Id;
        }
        
        public async Task UpdateAsync(string cvId, IEnumerable<EducationEditModel> educationModels)
        {
            List<Education> educationEntitiesFromDb = await this.repository
                .AllWhere(e => e.CurriculumVitaeId == cvId)
                .ToListAsync();

            IEnumerable<EducationEditModel> educationsToAdd = educationModels
                .Where(em => !educationEntitiesFromDb.Any(ee => ee.Id == em.Id));

            if (educationsToAdd.Any())
            {
                List<Education> educationEntitiesToAdd = new();
                foreach (EducationEditModel educationEditModel in educationsToAdd)
                {
                    Education educationEntity = this.mapper.Map<Education>(educationEditModel);
                    educationEntity.Id = 0;
                    educationEntity.CurriculumVitaeId = cvId;
                    educationEntitiesToAdd.Add(educationEntity);
                }

                await this.repository.AddRangeAsync(educationEntitiesToAdd);
            }

            IEnumerable<Education> educationEntitiesToRemove = educationEntitiesFromDb
                .Where(ee => !educationModels.Any(em => em.Id == ee.Id));

            if (educationEntitiesToRemove.Any())
            {
                this.repository.DeleteRange(educationEntitiesToRemove);
            }

            IEnumerable<Education> educationsToUpdate = educationEntitiesFromDb
                .Where(ee => educationModels.Any(em => em.Id == ee.Id));

            if (educationsToUpdate.Any())
            {
                foreach (Education educationEntityToUpdate in educationsToUpdate)
                {
                    EducationEditModel correspondingModel = educationModels
                        .First(em => em.Id == educationEntityToUpdate.Id);

                    this.mapper.Map(correspondingModel, educationEntityToUpdate);
                }

                this.repository.UpdateRange(educationsToUpdate);
            }

            await this.repository.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int educationId)
        {
            var educationFromDb = await this.repository.FindAsync(educationId);
            if (educationFromDb == null)
            {
                return false;
            }

            this.repository.Delete(educationFromDb);
            await this.repository.SaveChangesAsync();

            return true;
        }
    }
}
