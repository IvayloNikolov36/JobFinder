namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.CVModels;
    using System.Threading.Tasks;

    public interface IPersonalDetailsService
    {
        Task<T> GetAsync<T>(string cvId);

        Task<bool> UpdateAsync(PersonalDetailsEditModel personalDetails);
    }
}
