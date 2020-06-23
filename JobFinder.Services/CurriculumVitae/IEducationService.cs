namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEducationService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task<int> CreateAsync(string cvId, DateTime fromDate, DateTime? toDate, string location, 
            EducationLevel educationLevel, string major, string mainSubjects);

        Task<bool> UpdateAsync(int educationId, DateTime fromDate, DateTime? toDate, string location,
            EducationLevel educationLevel, string major, string mainSubjects);

        Task<bool> DeleteAsync(int educationId);
    }
}
