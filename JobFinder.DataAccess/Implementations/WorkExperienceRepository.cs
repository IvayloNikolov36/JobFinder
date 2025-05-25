using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class WorkExperienceRepository : EfCoreRepository<WorkExperienceInfoEntity>, IWorkExperienceRepository
{
    public WorkExperienceRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> workExperienceIds)
    {
        WorkExperienceInfoEntity[] workExperienceEntities = await this.DbSet
            .Where(we => workExperienceIds.Contains(we.Id))
            .ToArrayAsync();

        foreach (WorkExperienceInfoEntity workExperience in workExperienceEntities)
        {
            workExperience.IncludeInAnonymousProfile = true;
        }

        this.DbSet.UpdateRange(workExperienceEntities);
    }
}
