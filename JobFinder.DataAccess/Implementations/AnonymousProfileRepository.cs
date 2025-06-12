using JobFinder.Data;
using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using JobFinder.Transfer.DTOs.JobAd;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class AnonymousProfileRepository : EfCoreRepository<AnonymousProfileEntity>, IAnonymousProfileRepository
{
    public AnonymousProfileRepository(JobFinderDbContext context) : base(context)
    {
    }

    public async Task<AnonymousProfileDataDTO> GetAnonymousProfile(string anonymousProfileId)
    {
        return await this.DbSet
            .Where(ap => ap.Id == anonymousProfileId)
            .Select(ap => ap.CurriculumVitae)
            .To<AnonymousProfileDataDTO>()
            .SingleOrDefaultAsync();
    }

    public async Task<AnonymousProfileDataDTO> GetAnonymousProfileData(string userId)
    {
        return await this.DbSet
            .Where(ap => ap.UserId == userId)
            .Select(ap => ap.CurriculumVitae)
            .To<AnonymousProfileDataDTO>()
            .SingleOrDefaultAsync();
    }

    public async Task Create(string cvId, string userId, AnonymousProfileCreateDTO anonymousProfileDto)
    {
        AnonymousProfileAppearanceEntity appearanceEntity = new()
        {
            JobCategoryId = anonymousProfileDto.AppearanceDto.JobCategoryId,
            PreferredPositions = anonymousProfileDto.AppearanceDto.PreferredPositions
        };

        appearanceEntity.WorkplaceTypes
            .AddRange(this.GetWorkingplaceTypesEntities(anonymousProfileDto.AppearanceDto.WorkplaceTypes));

        appearanceEntity.JobEngagements
            .AddRange(this.GetJobEngagementsEntities(anonymousProfileDto.AppearanceDto.JobEngagements));

        appearanceEntity.SoftSkills
            .AddRange(this.GetSofSkillsEntities(anonymousProfileDto.AppearanceDto.SoftSkills));

        appearanceEntity.ITAreas
            .AddRange(this.GetItAreasEntities(anonymousProfileDto.AppearanceDto.ITAreas));

        appearanceEntity.TechStacks
            .AddRange(this.GetTechStacksEntities(anonymousProfileDto.AppearanceDto.TechStacks));

        AnonymousProfileEntity anonymousProfileEntity = new()
        {
            CurriculumVitaeId = cvId,
            UserId = userId,
            Appearance = appearanceEntity
        };

        await this.DbSet.AddAsync(anonymousProfileEntity);
    }

    public async Task Delete(string id)
    {
        AnonymousProfileEntity profileEntity = await this.DbSet.FindAsync(id);

        this.DbSet.Remove(profileEntity);
    }

    public async Task<bool> HasAnonymousProfile(string userId)
    {
        return await this.DbSet.AnyAsync(ap => ap.UserId == userId);
    }

    public async Task<string> GetCvId(string id)
    {
        AnonymousProfileEntity entity = await this.DbSet.FindAsync(id);

        base.ValidateForExistence(entity, "AnonymousProfile");

        return entity.CurriculumVitaeId;
    }

    public async Task<string> GetOwnerId(string id)
    {
        AnonymousProfileEntity entity = await this.DbSet.FindAsync(id);

        base.ValidateForExistence(entity, "AnonymousProfile");

        return entity.UserId;
    }

    public async Task<IEnumerable<AnonymousProfileListingDTO>> GetProfilesRelevantToJobAd(JobAdCriteriasDTO jobAdCriterias)
    {
        IEnumerable<AnonymousProfileListingDTO> data = await this.DbSet.AsNoTracking()
            .Where(ap => ap.Appearance.JobCategoryId == jobAdCriterias.JobCategoryId)
            .Where(ap => ap.Appearance
                .JobEngagements
                .Select(je => je.JobEngagementId)
                .Contains(jobAdCriterias.JobEngagementId))
            .Where(ap => jobAdCriterias
                .SoftSkills
                .All(adSkill => ap.Appearance
                    .SoftSkills
                    .Select(ss => ss.SoftSkillId)
                    .Contains(adSkill)))
            .Select(ap => new AnonymousProfileListingDTO
            {
                Id = ap.Id,
                ActivatedDate = ap.CreatedOn
            })
            .ToListAsync();

        return data;
    }

    private IEnumerable<AnonymousProfileAppearanceWorkplaceTypeEntity> GetWorkingplaceTypesEntities(
        IEnumerable<int> workplaceTypes)
    {
        List<AnonymousProfileAppearanceWorkplaceTypeEntity> workingplaceTypeEntities = [];

        foreach (int workingplaceTypeId in workplaceTypes)
        {
            workingplaceTypeEntities.Add(new AnonymousProfileAppearanceWorkplaceTypeEntity
            {
                WorkplaceTypeId = workingplaceTypeId
            });
        }

        return workingplaceTypeEntities;
    }

    private IEnumerable<AnonymousProfileAppearanceSoftSkillEntity> GetSofSkillsEntities(IEnumerable<int> softSkills)
    {
        List<AnonymousProfileAppearanceSoftSkillEntity> softSkillsEntities = [];

        foreach (int softSkillId in softSkills)
        {
            softSkillsEntities.Add(new AnonymousProfileAppearanceSoftSkillEntity
            {
                SoftSkillId = softSkillId
            });
        }

        return softSkillsEntities;
    }

    private IEnumerable<AnonymousProfileAppearanceJobEngagementEntity> GetJobEngagementsEntities(IEnumerable<int> jobEngagements)
    {
        List<AnonymousProfileAppearanceJobEngagementEntity> jobEngagementEntities = [];

        foreach (int jobEngagementId in jobEngagements)
        {
            jobEngagementEntities.Add(new AnonymousProfileAppearanceJobEngagementEntity
            {
                JobEngagementId = jobEngagementId
            });
        }

        return jobEngagementEntities;
    }

    private List<AnonymousProfileAppearanceITAreaEntity> GetItAreasEntities(IEnumerable<int> itAreas)
    {
        List<AnonymousProfileAppearanceITAreaEntity> itAreasEntities = [];

        foreach (int itAreaId in itAreas)
        {
            itAreasEntities.Add(new AnonymousProfileAppearanceITAreaEntity
            {
                ITAreaId = itAreaId
            });
        }

        return itAreasEntities;
    }

    private List<AnonymousProfileAppearanceTechStackEntity> GetTechStacksEntities(IEnumerable<int> teckStacks)
    {
        List<AnonymousProfileAppearanceTechStackEntity> techStacksEntities = [];

        foreach (int teckStackId in teckStacks)
        {
            techStacksEntities.Add(new AnonymousProfileAppearanceTechStackEntity
            {
                TechStackId = teckStackId
            });
        }

        return techStacksEntities;
    }
}
