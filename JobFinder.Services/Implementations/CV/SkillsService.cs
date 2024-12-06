namespace JobFinder.Services.Implementations.CV
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SkillsService : ISkillsService
    {
        private readonly IRepository<SkillsInfoEntity> repository;
        private readonly IMapper mapper;

        public SkillsService(
            IRepository<SkillsInfoEntity> skillRepository,
            IMapper mapper)
        {
            this.repository = skillRepository;
            this.mapper = mapper;
        }

        public async Task<T> GetAsync<T>(int skillsId)
        {
            T skill = await this.repository.AllAsNoTracking()
                .Where(s => s.Id == skillsId)
                .To<T>()
                .FirstOrDefaultAsync();

            return skill;
        }

        public async Task<bool> UpdateAsync(SkillsEditModel skillsModel)
        {
            SkillsInfoEntity skillFromDb = await this.repository.FindAsync(skillsModel.Id);

            if (skillFromDb == null)
            {
                return false;
            }

            this.mapper.Map(skillsModel, skillFromDb);

            this.repository.Update(skillFromDb);

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
