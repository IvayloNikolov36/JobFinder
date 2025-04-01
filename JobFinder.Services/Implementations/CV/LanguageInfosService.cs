using AutoMapper;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.CV;
using JobFinder.Web.Models.Common;
using JobFinder.Web.Models.CVModels;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Services.Implementations.CV
{
    public class LanguageInfosService : ILanguageInfosService
    {
        private readonly IRepository<LanguageInfoEntity> repository;
        private readonly IMapper mapper;

        public LanguageInfosService(
            IRepository<LanguageInfoEntity> languageInfoRepository,
            IMapper mapper)
        {
            this.repository = languageInfoRepository;
            this.mapper = mapper;
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
    }
}
