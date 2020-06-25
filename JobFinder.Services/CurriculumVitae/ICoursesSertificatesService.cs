namespace JobFinder.Services.CurriculumVitae
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICoursesSertificatesService
    {
        Task<IEnumerable<T>> AllAsync<T>(string cvId);

        Task<int> AddAsync(string cvId, string courseName, string certificateUrl);

        Task<bool> UpdateAsync(int id, string courseName, string certificateUrl);

        Task<bool> DeleteAsync(int id);
    }
}
