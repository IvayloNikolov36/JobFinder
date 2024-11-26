namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Models.Enums;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CurriculumVitae;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LanguageInfoService : ILanguageInfoService
    {
        private readonly IRepository<LanguageInfo> repository;
        private readonly IMapper mapper;

        public LanguageInfoService(
            IRepository<LanguageInfo> languageInfoRepository,
            IMapper mapper) 
        {
            this.repository = languageInfoRepository;
            this.mapper = mapper;
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

        public async Task UpdateAsync(string cvId, IEnumerable<LanguageInfoEditModel> languagesInfoModels)
        {
            List<LanguageInfo> languageInfoEntitiesFromDB = await this.repository
                .AllWhere(we => we.CurriculumVitaeId == cvId)
                .ToListAsync();

            IEnumerable<LanguageInfoEditModel> languageInfoToAdd = languagesInfoModels
                .Where(li => !languageInfoEntitiesFromDB.Any(le => le.Id == li.Id));

            if (languageInfoToAdd.Any())
            {
                List<LanguageInfo> entitiesToAdd = new();
                foreach (LanguageInfoEditModel model in languageInfoToAdd)
                {
                    LanguageInfo entityToAdd = this.mapper.Map<LanguageInfo>(model);
                    entityToAdd.Id = 0;
                    entityToAdd.CurriculumVitaeId = cvId;
                    entitiesToAdd.Add(entityToAdd);
                }

                await this.repository.AddRangeAsync(entitiesToAdd);
            }

            IEnumerable<LanguageInfo> entitiesToRemove = languageInfoEntitiesFromDB
                .Where(we => !languagesInfoModels.Any(wem => wem.Id == we.Id));

            if (entitiesToRemove.Any())
            {
                this.repository.DeleteRange(entitiesToRemove);
            }

            IEnumerable<LanguageInfo> entitiesToUpdate = languageInfoEntitiesFromDB
                .Where(we => languagesInfoModels.Any(m => m.Id == we.Id));

            if (entitiesToUpdate.Any())
            {
                foreach (LanguageInfo item in entitiesToUpdate)
                {
                    LanguageInfoEditModel correspondingModel = languagesInfoModels
                        .First(m => m.Id == item.Id);

                    this.mapper.Map(correspondingModel, item);
                }

                this.repository.UpdateRange(entitiesToUpdate);
            }

            await this.repository.SaveChangesAsync();
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
