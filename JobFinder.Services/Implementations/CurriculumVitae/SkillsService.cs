namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SkillsService : ISkillsService
    {
        private readonly IRepository<Skill> repository;

        public SkillsService(IRepository<Skill> skillRepository)
        {
            this.repository = skillRepository;
        }

        public async Task<T> GetAsync<T>(int skillsId)
        {
            T skill = await this.repository.AllAsNoTracking()
                .Where(s => s.Id == skillsId)
                .To<T>()
                .FirstOrDefaultAsync();

            return skill;
        }

        public async Task<int> AddAsync(string cvId, string computerSkills, string skills, bool hasManagedPeople, bool hasDrivingLicense)
        {
            var skill = new Skill
            {
                CurriculumVitaeId = cvId,
                ComputerSkills = computerSkills,
                Skills = skills,
                HasManagedPeople = hasManagedPeople,
                HasDrivingLicense = hasDrivingLicense
            };

            await this.repository.AddAsync(skill);
            await this.repository.SaveChangesAsync();

            return skill.Id;
        }

        public async Task<bool> UpdateAsync(int skillId, string computerSkills, string skills, bool hasManagedPeople, bool hasDrivingLicense)
        {
            var skillFromDb = await this.repository.FindAsync(skillId);

            if (skillFromDb == null)
            {
                return false;
            }

            skillFromDb.ComputerSkills = computerSkills;
            skillFromDb.Skills = skills;
            skillFromDb.HasManagedPeople = hasManagedPeople;
            skillFromDb.HasDrivingLicense = hasDrivingLicense;

            await this.repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int skillId)
        {
            var skillFromDb = await this.repository.FindAsync(skillId);

            if (skillFromDb == null)
            {
                return false;
            }

            this.repository.Delete(skillFromDb);
            await this.repository.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<T>> GetDrivingCategories<T>()
        {
            return await this.repository.AllAsNoTracking()
                .To<T>()
                .ToListAsync();
        }
    }
}
