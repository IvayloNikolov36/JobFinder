using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class EducationInfoRepository : EfCoreRepository<EducationInfoEntity>, IEducationInfoRepository
{
    public EducationInfoRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> educationInfoIds)
    {
        EducationInfoEntity[] educationInfos = await this.DbSet
            .Where(e => e.CurriculumVitaeId == cvId)
            .ToArrayAsync();

        foreach (EducationInfoEntity educationInfo in educationInfos)
        {
            educationInfo.IncludeInAnonymousProfile = educationInfoIds.Contains(educationInfo.Id);
        }

        this.DbSet.UpdateRange(educationInfos);
    }


    public async Task DisassociateFromAnonymousProfile(string cvId)
    {
        EducationInfoEntity[] educationInfos = await this.DbSet
            .Where(e => e.CurriculumVitaeId == cvId)
            .ToArrayAsync();

        foreach (EducationInfoEntity educationInfo in educationInfos)
        {
            educationInfo.IncludeInAnonymousProfile = null;
        }

        this.DbSet.UpdateRange(educationInfos);
    }
}
