using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.Cv;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs.Cv;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class WorkExperienceRepository : EfCoreRepository<WorkExperienceInfoEntity>, IWorkExperienceRepository
{
    private readonly IMapper mapper;
    public WorkExperienceRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<IEnumerable<WorkExperienceInfoEditDTO>> Update(
        string cvId,
        IEnumerable<WorkExperienceInfoEditDTO> workExperienceModels)
    {
        List<WorkExperienceInfoEntity> workExperienceEntitiesFromDB = await this.DbSet
            .Where(we => we.CurriculumVitaeId == cvId)
            .ToListAsync();

        IEnumerable<WorkExperienceInfoEditDTO> workExperinceToAdd = workExperienceModels
            .Where(wem => !workExperienceEntitiesFromDB.Any(wee => wee.Id == wem.Id));

        List<WorkExperienceInfoEntity> entitiesToAdd = null;
        if (workExperinceToAdd.Any())
        {
            entitiesToAdd = new List<WorkExperienceInfoEntity>();
            foreach (WorkExperienceInfoEditDTO model in workExperinceToAdd)
            {
                WorkExperienceInfoEntity entityToAdd = this.mapper.Map<WorkExperienceInfoEntity>(model);
                entityToAdd.Id = 0;
                entityToAdd.CurriculumVitaeId = cvId;
                entitiesToAdd.Add(entityToAdd);
            }

            await this.DbSet.AddRangeAsync(entitiesToAdd);
        }

        IEnumerable<WorkExperienceInfoEntity> entitiesToRemove = workExperienceEntitiesFromDB
            .Where(we => !workExperienceModels.Any(wem => wem.Id == we.Id));

        if (entitiesToRemove.Any())
        {
            this.DbSet.RemoveRange(entitiesToRemove);
        }

        IEnumerable<WorkExperienceInfoEntity> entitiesToUpdate = workExperienceEntitiesFromDB
            .Where(we => workExperienceModels.Any(m => m.Id == we.Id));

        if (entitiesToUpdate.Any())
        {
            foreach (WorkExperienceInfoEntity item in entitiesToUpdate)
            {
                WorkExperienceInfoEditDTO correspondingModel = workExperienceModels
                    .First(m => m.Id == item.Id);

                this.mapper.Map(correspondingModel, item);
            }

            this.DbSet.UpdateRange(entitiesToUpdate);
        }

        return this.mapper.Map<IEnumerable<WorkExperienceInfoEditDTO>>(entitiesToAdd);
    }

    public async Task SetIncludeInAnonymousProfile(string cvId, IEnumerable<int> workExperienceIds)
    {
        WorkExperienceInfoEntity[] workExperienceEntities = await this.DbSet
            .Where(we => we.CurriculumVitaeId == cvId)
            .ToArrayAsync();

        foreach (WorkExperienceInfoEntity workExperience in workExperienceEntities)
        {
            workExperience.IncludeInAnonymousProfile = workExperienceIds.Contains(workExperience.Id);           
        }

        this.DbSet.UpdateRange(workExperienceEntities);
    }

    public async Task DisassociateFromAnonymousProfile(string cvId)
    {
        WorkExperienceInfoEntity[] workExperienceEntities = await this.DbSet
            .Where(we => we.CurriculumVitaeId == cvId)
            .ToArrayAsync();

        foreach (WorkExperienceInfoEntity workExperience in workExperienceEntities)
        {
            workExperience.IncludeInAnonymousProfile = null;
        }

        this.DbSet.UpdateRange(workExperienceEntities);
    }

    public void Delete(string cvId)
    {
        base.DeleteWhere(we => we.CurriculumVitaeId == cvId);
    }
}
