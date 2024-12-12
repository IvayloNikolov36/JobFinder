namespace JobFinder.Services.CV
{
    using JobFinder.Web.Models.CVModels;
    using System.Threading.Tasks;

    public interface ISkillsInfosService
    {
        Task<bool> UpdateAsync(SkillsEditModel skills);
    }
}
