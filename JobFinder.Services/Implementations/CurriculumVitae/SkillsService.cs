namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data;
    using JobFinder.Data.Models.CV;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class SkillsService : DbService, ISkillsService
    {
        public SkillsService(JobFinderDbContext dbContext)
            : base(dbContext)
        {

        }

        public async Task<T> GetAsync<T>(int skillsId)
        {
            T skill = await this.DbContext.Skills.AsNoTracking()
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

            await this.DbContext.AddAsync(skill);
            await this.DbContext.SaveChangesAsync();

            return skill.Id;
        }

        public async Task<bool> UpdateAsync(int skillId, string computerSkills, string skills, bool hasManagedPeople, bool hasDrivingLicense)
        {
            var skillFromDb = await this.DbContext.FindAsync<Skill>(skillId);

            if (skillFromDb == null)
            {
                return false;
            }

            skillFromDb.ComputerSkills = computerSkills;
            skillFromDb.Skills = skills;
            skillFromDb.HasManagedPeople = hasManagedPeople;
            skillFromDb.HasDrivingLicense = hasDrivingLicense;

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int skillId)
        {
            var skillFromDb = await this.DbContext.FindAsync<Skill>(skillId);

            if (skillFromDb == null)
            {
                return false;
            }

            this.DbContext.Remove(skillFromDb);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

    }
}
