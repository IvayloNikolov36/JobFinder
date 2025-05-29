using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs.CV;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class EducationInfoRepository : EfCoreRepository<EducationInfoEntity>, IEducationInfoRepository
{
    private readonly IMapper mapper;

    public EducationInfoRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<EducationInfoEditDTO>> Update(string cvId, IEnumerable<EducationInfoEditDTO> educationDtos)
    {
        List<EducationInfoEntity> educationEntitiesFromDb = await this.DbSet
            .Where(e => e.CurriculumVitaeId == cvId)
            .ToListAsync();

        IEnumerable<EducationInfoEditDTO> educationsToAdd = educationDtos
            .Where(em => !educationEntitiesFromDb.Any(ee => ee.Id == em.Id));

        List<EducationInfoEntity> educationEntitiesToAdd = null;
        if (educationsToAdd.Any())
        {
            educationEntitiesToAdd = new List<EducationInfoEntity>();
            foreach (EducationInfoEditDTO educationEditDto in educationsToAdd)
            {
                EducationInfoEntity educationEntity = this.mapper.Map<EducationInfoEntity>(educationEditDto);
                educationEntity.Id = 0;
                educationEntity.CurriculumVitaeId = cvId;
                educationEntitiesToAdd.Add(educationEntity);
            }

            await this.DbSet.AddRangeAsync(educationEntitiesToAdd);
        }

        IEnumerable<EducationInfoEntity> educationEntitiesToRemove = educationEntitiesFromDb
            .Where(ee => !educationDtos.Any(em => em.Id == ee.Id));

        if (educationEntitiesToRemove.Any())
        {
            this.DbSet.RemoveRange(educationEntitiesToRemove);
        }

        IEnumerable<EducationInfoEntity> educationsToUpdate = educationEntitiesFromDb
            .Where(ee => educationDtos.Any(em => em.Id == ee.Id));

        if (educationsToUpdate.Any())
        {
            foreach (EducationInfoEntity educationEntityToUpdate in educationsToUpdate)
            {
                EducationInfoEditDTO correspondingDto = educationDtos
                    .First(em => em.Id == educationEntityToUpdate.Id);

                this.mapper.Map(correspondingDto, educationEntityToUpdate);
            }

            this.DbSet.UpdateRange(educationsToUpdate);
        }

        return this.mapper.Map<IEnumerable<EducationInfoEditDTO>>(educationEntitiesToAdd);
    }

    public async Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> educationInfoIds)
    {
        EducationInfoEntity[] educationInfos = await this.DbSet
            .Where(e => e.CurriculumVitaeId == cvId)
            .ToArrayAsync();

        foreach (EducationInfoEntity educationInfo in educationInfos)
        {
            educationInfo.IncludeInAnonymousProfile = educationInfoIds.Contains(educationInfo.Id);
        }

        this.DbSet.UpdateRange(educationInfos);
    }

    public async Task DisassociateFromAnonymousProfile(string cvId)
    {
        EducationInfoEntity[] educationInfos = await this.DbSet
            .Where(e => e.CurriculumVitaeId == cvId)
            .ToArrayAsync();

        foreach (EducationInfoEntity educationInfo in educationInfos)
        {
            educationInfo.IncludeInAnonymousProfile = null;
        }

        this.DbSet.UpdateRange(educationInfos);
    }
}
