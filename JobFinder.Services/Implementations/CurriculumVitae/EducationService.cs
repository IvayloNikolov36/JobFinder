namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EducationService : DbService, IEducationService
    {

        public EducationService(JobFinderDbContext dbContext) 
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            var educations = await this.DbContext.Educations.AsNoTracking()
                .Where(e => e.CurriculumVitaeId == cvId)
                .To<T>()
                .ToListAsync();

            return educations;
        }

        public async Task<int> CreateAsync(string cvId, DateTime fromDate, DateTime? toDate, string location, 
            EducationLevel educationLevel, string major, string mainSubjects)
        {
            var education = new Education
            {
                CurriculumVitaeId = cvId,
                FromDate = fromDate,
                ToDate = toDate,
                Location = location,
                EducationLevel = educationLevel,
                Major = major,
                MainSubjects = mainSubjects
            };

            await this.DbContext.AddAsync(education);
            await this.DbContext.SaveChangesAsync();

            return education.Id;
        }
        
        public async Task<bool> UpdateAsync(int educationId, DateTime fromDate, DateTime? toDate, string location, 
            EducationLevel educationLevel, string major, string mainSubjects)
        {
            var educationFromDb = await this.DbContext.FindAsync<Education>(educationId);

            if (educationFromDb == null)
            {
                return false;
            }

            educationFromDb.FromDate = fromDate;
            educationFromDb.ToDate = toDate;
            educationFromDb.Location = location;
            educationFromDb.EducationLevel = educationLevel;
            educationFromDb.Major = major;
            educationFromDb.MainSubjects = mainSubjects;

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int educationId)
        {
            var educationFromDb = await this.DbContext.FindAsync<Education>(educationId);

            if (educationFromDb == null)
            {
                return false;
            }

            this.DbContext.Educations.Remove(educationFromDb);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

    }
}
