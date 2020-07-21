namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class WorkExperienceService : IWorkExperienceService
    {
        private readonly IRepository<WorkExperience> repository;

        public WorkExperienceService(IRepository<WorkExperience> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            var workExperiences = await this.repository.AllAsNoTracking()
                .Where(we => we.CurriculumVitaeId == cvId)
                .To<T>()
                .ToListAsync();

            return workExperiences;
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

            await this.repository.AddAsync(workExperience);
            await this.repository.SaveChangesAsync();

            return workExperience.Id;
        }

        public async Task<bool> UpdateAsync(int workExperieceId, DateTime fromDate, DateTime? toDate, string jobTitle, 
            string organization, BusinessSector businessSector, string location, string additionalDetails)
        {
            var workExperience = await this.repository.FindAsync(workExperieceId);
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

            await this.repository.SaveChangesAsync();

            return true;
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

    }
}
