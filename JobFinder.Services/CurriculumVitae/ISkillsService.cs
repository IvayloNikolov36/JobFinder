namespace JobFinder.Services.CurriculumVitae
{
    using System.Threading.Tasks;

    public interface ISkillsService
    {
        Task<T> GetAsync<T>(int skillsId);

        Task<int> AddAsync(string cvId, string computerSkills, string skills, bool hasManagedPeople, bool hasDrivingLicense);

        Task<bool> UpdateAsync(int skillId, string computerSkills, string skills, bool hasManagedPeople, bool hasDrivingLicense);

        Task<bool> DeleteAsync(int skillId);
    }
}
