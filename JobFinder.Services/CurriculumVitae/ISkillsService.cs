namespace JobFinder.Services.CurriculumVitae
{
    using JobFinder.Web.Models.CVModels;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ISkillsService
    {
        Task<T> GetAsync<T>(int skillsId);

        Task<bool> UpdateAsync(SkillsEditModel skills);

        Task<bool> DeleteAsync(int skillId);

        Task<IEnumerable<T>> GetDrivingCategories<T>();
    }
}
