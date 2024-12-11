namespace JobFinder.Services.Implementations.CV
{
    using AutoMapper;
    using JobFinder.Data.Models.CV;
    using JobFinder.Data.Repositories.Contracts;
    using JobFinder.Services.CV;
    using JobFinder.Services.Mappings;
    using JobFinder.Web.Models.Common;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class LanguageInfoService : ILanguageInfoService
    {
        private readonly IRepository<LanguageInfoEntity> repository;
        private readonly IMapper mapper;

        public LanguageInfoService(
            IRepository<LanguageInfoEntity> languageInfoRepository,
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

        public async Task<UpdateResult> UpdateAsync(string cvId, IEnumerable<LanguageInfoEditModel> languagesInfoModels)
        {
            languagesInfoModels = languagesInfoModels.OrderBy(li => li.Id);
            int[] languageInfoIds = languagesInfoModels.Select(li => li.Id).ToArray();

            List<LanguageInfoEntity> languageInfoEntitiesFromDB = await this.repository
                .Where(we => we.CurriculumVitaeId == cvId)
                .ToListAsync();

            IEnumerable<LanguageInfoEditModel> languageInfoToAdd = languagesInfoModels
                .Where(li => !languageInfoEntitiesFromDB.Any(le => le.Id == li.Id));

            List<LanguageInfoEntity> entitiesToAdd = null;

            if (languageInfoToAdd.Any())
            {
                entitiesToAdd = new List<LanguageInfoEntity>();
                foreach (LanguageInfoEditModel model in languageInfoToAdd)
                {
                    LanguageInfoEntity entityToAdd = this.mapper.Map<LanguageInfoEntity>(model);
                    entityToAdd.Id = 0;
                    entityToAdd.CurriculumVitaeId = languageInfoEntitiesFromDB.First().CurriculumVitaeId;
                    entitiesToAdd.Add(entityToAdd);
                }

                await this.repository.AddRangeAsync(entitiesToAdd);
            }

            IEnumerable<LanguageInfoEntity> entitiesToRemove = languageInfoEntitiesFromDB
                .Where(we => !languagesInfoModels.Any(wem => wem.Id == we.Id));

            if (entitiesToRemove.Any())
            {
                this.repository.DeleteRange(entitiesToRemove);
            }

            IEnumerable<LanguageInfoEntity> entitiesToUpdate = languageInfoEntitiesFromDB
                .Where(we => languagesInfoModels.Any(m => m.Id == we.Id));

            if (entitiesToUpdate.Any())
            {
                foreach (LanguageInfoEntity item in entitiesToUpdate)
                {
                    LanguageInfoEditModel correspondingModel = languagesInfoModels
                        .First(m => m.Id == item.Id);

                    this.mapper.Map(correspondingModel, item);
                }

                this.repository.UpdateRange(entitiesToUpdate);
            }

            await this.repository.SaveChangesAsync();

            return new UpdateResult(entitiesToAdd);
        }

        public async Task<bool> DeleteAsync(int languageInfoId)
        {
            LanguageInfoEntity languageInfoFromDb = await this.repository.FindAsync(languageInfoId);
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
