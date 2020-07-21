namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Data.Repositories;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LanguageInfoService : ILanguageInfoService
    {
        private readonly IRepository<LanguageInfo> repository;

        public LanguageInfoService(IRepository<LanguageInfo> languageInfoRepository) 
        {
            this.repository = languageInfoRepository;
        }

        public async Task<IEnumerable<T>> AllAsync<T>(string cvId)
        {
            var languagesInfo = await this.repository.AllAsNoTracking()
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

            await this.repository.AddAsync(languageInfo);
            await this.repository.SaveChangesAsync();

            return languageInfo.Id;
        }

        public async Task<bool> UpdateAsync(int languageInfoId, LanguageType languageType, 
            LanguageLevel comprehension, LanguageLevel speaking, LanguageLevel writing)
        {
            var languageInfoFromDb = await this.repository.FindAsync(languageInfoId);

            if (languageInfoFromDb == null)
            {
                return false;
            }

            languageInfoFromDb.LanguageType = languageType;
            languageInfoFromDb.Comprehension = comprehension;
            languageInfoFromDb.Speaking = speaking;
            languageInfoFromDb.Writing = writing;

            await this.repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int languageInfoId)
        {
            var languageInfoFromDb = await this.repository.FindAsync(languageInfoId);
            if (languageInfoFromDb == null)
            {
                return false;
            }

            this.repository.Delete(languageInfoFromDb);
            await this.repository.SaveChangesAsync();

            return true;
        }

    }
}
