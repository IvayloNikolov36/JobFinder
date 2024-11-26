namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Web.Models.CVModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWorkExperienceService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task<int> CreateAsync(string cvId, DateTime fromDate, DateTime? toDate, string jobTitle, string organization, BusinessSector businessSector, string location, string additionalDetails);

        Task UpdateAsync(string cvId, IEnumerable<WorkExperienceEditModel> workExperienceModels);

        Task<bool> DeleteAsync(int workExperieceId);
    }
}
