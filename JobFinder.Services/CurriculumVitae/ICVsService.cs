namespace JobFinder.Services.CurriculumVitae
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICVsService
    {
        Task<string> CreateAsync(string userId, string name, string pictureUrl);

        Task<IEnumerable<T>> AllAsync<T>(string userId);
    }
}
