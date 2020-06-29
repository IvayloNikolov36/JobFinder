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

    public class WorkExperienceService : DbService, IWorkExperienceService
    {
        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            var workExperiences = await this.DbContext.WorkExperiences.AsNoTracking()
                .Where(we => we.CurriculumVitaeId == cvId)
                .To<T>()
                .ToListAsync();

            return workExperiences;
        }

        public WorkExperienceService(JobFinderDbContext dbContext) 
            : base(dbContext)
        {

        }

        public async Task<int> CreateAsync(string cvId, DateTime fromDate, DateTime? toDate, string jobTitle, string organization, BusinessSector businessSector, string location, string additionalDetails)
        {
            var workExperience = new WorkExperience
            {
                CurriculumVitaeId = cvId,
                FromDate = fromDate,
                ToDate = toDate,
                JobTitle = jobTitle,
                Organization = organization,
                BusinessSector = businessSector,
                Location = location,
                AditionalDetails = additionalDetails
            };

            await this.DbContext.AddAsync(workExperience);
            await this.DbContext.SaveChangesAsync();

            return workExperience.Id;
        }

        public async Task<bool> UpdateAsync(int workExperieceId, DateTime fromDate, DateTime? toDate, string jobTitle, string organization, BusinessSector businessSector, string location, string additionalDetails)
        {
            var workExperience = await this.DbContext.FindAsync<WorkExperience>(workExperieceId);
            if (workExperience == null)
            {
                return false;
            }

            workExperience.FromDate = fromDate;
            workExperience.ToDate = toDate;
            workExperience.JobTitle = jobTitle;
            workExperience.Organization = organization;
            workExperience.BusinessSector = businessSector;
            workExperience.Location = location;
            workExperience.AditionalDetails = additionalDetails;

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int workExperieceId)
        {
            var workExperience = await this.DbContext.FindAsync<WorkExperience>(workExperieceId);
            if (workExperience == null)
            {
                return false;
            }

            this.DbContext.Remove(workExperience);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

    }
}
