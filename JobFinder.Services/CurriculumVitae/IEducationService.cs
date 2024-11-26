namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Data.Models.Enums;
    using JobFinder.Web.Models.CVModels;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEducationService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task<int> CreateAsync(string cvId, DateTime fromDate, DateTime? toDate, string organization,
            string location, EducationLevel educationLevel, string major, string mainSubjects);

        Task UpdateAsync(string cvId, IEnumerable<EducationEditModel> educationsModel);

        Task<bool> DeleteAsync(int educationId);
    }
}
