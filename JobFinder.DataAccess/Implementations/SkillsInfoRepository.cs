using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class SkillsInfoRepository : EfCoreRepository<SkillsInfoEntity>, ISkillsInfoRepository
{
    public SkillsInfoRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task Delete(string cvId)
    {
        SkillsInfoEntity skillsInfoEntity = await this.DbSet
            .FirstOrDefaultAsync(s => s.CurriculumVitaeId == cvId);

        base.ValidateForExistence(skillsInfoEntity, "SkillsInfo");

        this.DbSet.Remove(skillsInfoEntity);
    }
}
