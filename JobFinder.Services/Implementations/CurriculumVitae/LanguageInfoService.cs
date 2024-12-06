namespace JobFinder.Services.Implementations.CurriculumVitae
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
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

        public async Task UpdateAsync(IEnumerable<LanguageInfoEditModel> languagesInfoModels)
        {
            languagesInfoModels = languagesInfoModels.OrderBy(li => li.Id);
            int[] languageInfoIds = languagesInfoModels.Select(li => li.Id).ToArray();

            List<LanguageInfo> languageInfoEntitiesFromDB = await this.repository
                .AllWhere(we => languageInfoIds.Contains(we.Id))
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
                    entityToAdd.CurriculumVitaeId = languageInfoEntitiesFromDB.First().CurriculumVitaeId;
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
            LanguageInfo languageInfoFromDb = await this.repository.FindAsync(languageInfoId);
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
