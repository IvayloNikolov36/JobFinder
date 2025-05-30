using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs.CV;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class LanguageInfoRepository : EfCoreRepository<LanguageInfoEntity>, ILanguageInfoRepository
{
    private readonly IMapper mapper;

    public LanguageInfoRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<LanguageInfoEditDTO>> Update(
        string cvId,
        IEnumerable<LanguageInfoEditDTO> languageInfoDtos)
    {
        languageInfoDtos = languageInfoDtos.OrderBy(li => li.Id);
        int[] languageInfoIds = [.. languageInfoDtos.Select(li => li.Id)];

        List<LanguageInfoEntity> languageInfoEntitiesFromDB = await this.DbSet
            .Where(we => we.CurriculumVitaeId == cvId)
            .ToListAsync();

        IEnumerable<LanguageInfoEditDTO> languageInfoToAdd = languageInfoDtos
            .Where(li => !languageInfoEntitiesFromDB.Any(le => le.Id == li.Id));

        List<LanguageInfoEntity> entitiesToAdd = null;

        if (languageInfoToAdd.Any())
        {
            entitiesToAdd = new List<LanguageInfoEntity>();
            foreach (LanguageInfoEditDTO dto in languageInfoToAdd)
            {
                LanguageInfoEntity entityToAdd = this.mapper.Map<LanguageInfoEntity>(dto);
                entityToAdd.Id = 0;
                entityToAdd.CurriculumVitaeId = languageInfoEntitiesFromDB.First().CurriculumVitaeId;
                entitiesToAdd.Add(entityToAdd);
            }

            await this.DbSet.AddRangeAsync(entitiesToAdd);
        }

        IEnumerable<LanguageInfoEntity> entitiesToRemove = languageInfoEntitiesFromDB
            .Where(we => !languageInfoDtos.Any(wem => wem.Id == we.Id));

        if (entitiesToRemove.Any())
        {
            this.DbSet.RemoveRange(entitiesToRemove);
        }

        IEnumerable<LanguageInfoEntity> entitiesToUpdate = languageInfoEntitiesFromDB
            .Where(we => languageInfoDtos.Any(m => m.Id == we.Id));

        if (entitiesToUpdate.Any())
        {
            foreach (LanguageInfoEntity item in entitiesToUpdate)
            {
                LanguageInfoEditDTO correspondingDto = languageInfoDtos
                    .First(m => m.Id == item.Id);

                this.mapper.Map(correspondingDto, item);
            }

            this.DbSet.UpdateRange(entitiesToUpdate);
        }

        return this.mapper.Map<IEnumerable<LanguageInfoEditDTO>>(entitiesToAdd);
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

    public void Delete(string cvId)
    {
        base.DeleteWhere(l => l.CurriculumVitaeId == cvId);
    }
}
