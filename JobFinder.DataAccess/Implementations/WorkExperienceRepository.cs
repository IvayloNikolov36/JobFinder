using JobFinder.Common.Exceptions;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;

namespace JobFinder.DataAccess.Implementations;

public class WorkExperienceRepository : EfCoreRepository<WorkExperienceInfoEntity>, IWorkExperienceRepository
{
    public WorkExperienceRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task SetIncludeInAnonymousProfile(string cvId, int workExperienceId)
    {
        WorkExperienceInfoEntity workExperience = await this.DbSet.FindAsync(workExperienceId);

        base.ValidateForExistence(workExperience, "WorkExperienceInfo");

        if (workExperience.CurriculumVitaeId != cvId)
        {
            throw new ActionableException("You can't modify foreign user cv details!");
        }

        workExperience.IncludeInAnonymousProfile = true;

        this.DbSet.Update(workExperience);
    }
}
