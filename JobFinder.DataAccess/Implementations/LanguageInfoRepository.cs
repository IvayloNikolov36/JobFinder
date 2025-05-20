using JobFinder.Common.Exceptions;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;

namespace JobFinder.DataAccess.Implementations;

public class LanguageInfoRepository : EfCoreRepository<LanguageInfoEntity>, ILanguageInfoRepository
{
    public LanguageInfoRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task SetIncludeInAnonymousProfile(string cvId, int languageInfoId)
    {
        LanguageInfoEntity languageInfo = await this.DbSet.FindAsync(languageInfoId);

        base.ValidateForExistence(languageInfo, "LanguageInfo");

        if (languageInfo.CurriculumVitaeId != cvId)
        {
            throw new ActionableException("You can't modify foreign user cv details!");
        }

        languageInfo.IncludeInAnonymousProfile = true;

        this.DbSet.Update(languageInfo);
    }
}
