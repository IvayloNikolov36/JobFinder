namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.CVModels;
    using System.Threading.Tasks;

    public interface ISkillsService
    {
        Task<T> GetAsync<T>(int skillsId);

        Task<bool> UpdateAsync(SkillsEditModel skills);
    }
}
