namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LanguageInfoService : DbService, ILanguageInfoService
    {
        public LanguageInfoService(JobFinderDbContext dbContext) 
            : base(dbContext)
        {

        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            var languagesInfo = await this.DbContext.LanguagesInfo.AsNoTracking()
                .Where(li => li.CurriculumVitaeId == cvId)
                .To<T>()
                .ToListAsync();

            return languagesInfo;
        }

        public async Task<int> AddAsync(string cvId, LanguageType languageType, 
            LanguageLevel comprehension, LanguageLevel speaking, LanguageLevel writing)
        {
            var languageInfo = new LanguageInfo
            {
                CurriculumVitaeId = cvId,
                LanguageType = languageType,
                Comprehension = comprehension,
                Speaking = speaking,
                Writing = writing
            };

            await this.DbContext.AddAsync(languageInfo);
            await this.DbContext.SaveChangesAsync();

            return languageInfo.Id;
        }

        public async Task<bool> UpdateAsync(int languageInfoId, LanguageType languageType, 
            LanguageLevel comprehension, LanguageLevel speaking, LanguageLevel writing)
        {
            var languageInfoFromDb = await this.DbContext.FindAsync<LanguageInfo>(languageInfoId);

            if (languageInfoFromDb == null)
            {
                return false;
            }

            languageInfoFromDb.LanguageType = languageType;
            languageInfoFromDb.Comprehension = comprehension;
            languageInfoFromDb.Speaking = speaking;
            languageInfoFromDb.Writing = writing;

            await this.DbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int languageInfoId)
        {
            var languageInfoFromDb = await this.DbContext.FindAsync<LanguageInfo>(languageInfoId);

            if (languageInfoFromDb == null)
            {
                return false;
            }

            this.DbContext.Remove(languageInfoFromDb);
            await this.DbContext.SaveChangesAsync();

            return true;
        }

    }
}
