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
            .Where(e => educationInfoIds.Contains(e.Id))
            .ToArrayAsync();

        foreach (EducationInfoEntity educationInfo in educationInfos)
        {
            educationInfo.IncludeInAnonymousProfile = true;
        }

        this.DbSet.UpdateRange(educationInfos);
    }
}
