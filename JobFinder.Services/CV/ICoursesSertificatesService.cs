namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.CVModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesSertificatesService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task UpdateAsync(string cvId, IEnumerable<CourseSertificateEditModel> coursesInfo);

        Task<bool> DeleteAsync(int id);
    }
}
