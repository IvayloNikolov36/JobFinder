namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWorkExperienceSerive
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task<int> CreateAsync(DateTime fromDate, DateTime? toDate, string jobTitle, string organization, BusinessSector businessSector, string location, string additionalDetails);

        Task<int> AddAsync(string cvId, DateTime fromDate, DateTime? toDate, string jobTitle, string organization, BusinessSector businessSector, string location, string additionalDetails);

        Task<bool> UpdateAsync(int workExperienceId, DateTime fromDate, DateTime? toDate, string jobTitle, string organization, BusinessSector businessSector, string location, string additionalDetails);

        Task<bool> DeleteAsync(int workExperieceId);
    }
}
