namespace JobFinder.Services.CurriculumVitae
{
    using System.Threading.Tasks;

    public interface ICVsService
    {
        Task<string> CreateAsync(string userId, string name, string pictureUrl);
    }
}
