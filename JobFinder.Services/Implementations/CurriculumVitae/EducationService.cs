namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Data.Repositories;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EducationService : IEducationService
    {
        private readonly IRepository<Education> repository;

        public EducationService(IRepository<Education> educationRepository) 
        {
            this.repository = educationRepository;
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
        
        public async Task<bool> UpdateAsync(int educationId, DateTime fromDate, DateTime? toDate, string organization,
            string location, EducationLevel educationLevel, string major, string mainSubjects)
        {
            var educationFromDb = await this.repository.FindAsync(educationId);

            if (educationFromDb == null)
            {
                return false;
            }

            educationFromDb.FromDate = fromDate;
            educationFromDb.ToDate = toDate;
            educationFromDb.Organization = organization;
            educationFromDb.Location = location;
            educationFromDb.EducationLevel = educationLevel;
            educationFromDb.Major = major;
            educationFromDb.MainSubjects = mainSubjects;

            await this.repository.SaveChangesAsync();

            return true;
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
