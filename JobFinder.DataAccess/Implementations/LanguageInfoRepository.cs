using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class LanguageInfoRepository : EfCoreRepository<LanguageInfoEntity>, ILanguageInfoRepository
{
    public LanguageInfoRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> languageInfoIds)
    {
        LanguageInfoEntity[] languageInfos = await this.DbSet
            .Where(li => li.CurriculumVitaeId == cvId)
            .ToArrayAsync();

        foreach (LanguageInfoEntity languageInfo in languageInfos)
        {
            languageInfo.IncludeInAnonymousProfile = languageInfoIds.Contains(languageInfo.Id);
        }

        this.DbSet.UpdateRange(languageInfos);
    }


    public async Task DisassociateFromAnonymousProfile(string cvId)
    {
        LanguageInfoEntity[] languageInfos = await this.DbSet
            .Where(li => li.CurriculumVitaeId == cvId)
            .ToArrayAsync();

        foreach (LanguageInfoEntity languageInfo in languageInfos)
        {
            languageInfo.IncludeInAnonymousProfile = null;
        }

        this.DbSet.UpdateRange(languageInfos);
    }
}
