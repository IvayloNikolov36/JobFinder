using JobFinder.Data;
using JobFinder.Data.Models.Cv;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;

namespace JobFinder.DataAccess.Implementations;

public class SkillsInfoDrivingCategoryRepository : EfCoreRepository<SkillsInfoDrivingCategoryEntity>,
    ISkillsInfoDrivingCategoryRepository
{
    public SkillsInfoDrivingCategoryRepository(JobFinderDbContext context) : base(context)
    {
    }

    public void Delete(string cvId)
    {
        base.DeleteWhere(x => x.SkillsInfo.CurriculumVitaeId == cvId);
    }
}
