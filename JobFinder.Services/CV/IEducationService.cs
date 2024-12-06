namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.CVModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IEducationService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task UpdateAsync(string cvId, IEnumerable<EducationEditModel> educationsModel);

        Task<bool> DeleteAsync(int educationId);
    }
}
