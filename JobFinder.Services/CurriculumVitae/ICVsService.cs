namespace JobFinder.Services.CurriculumVitae
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICVsService
    {
        Task<bool> ExistsAsync(string id);

        Task<string> CreateAsync(string userId, string name, string pictureUrl);

        Task<IEnumerable<T>> AllAsync<T>(string userId);

        Task<T> GetDataAsync<T>(string cvId);

        Task<bool> SetDataAsync(string cvId, byte[] data);

        Task<byte[]> GetCvDataAsync(string cvId);
    }
}
