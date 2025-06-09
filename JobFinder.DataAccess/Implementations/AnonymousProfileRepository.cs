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
            RemoteJobPreferenceId = anonymousProfileDto.AppearanceDto.RemoteJobPreferenceId,
            JobCategoryId = anonymousProfileDto.AppearanceDto.JobCategoryId,
            PreferredPositions = anonymousProfileDto.AppearanceDto.PreferredPositions
        };

        appearanceEntity.AnonymousProfileAppearanceJobEngagements
            .AddRange(this.GetJobEngagementsEntities(anonymousProfileDto.AppearanceDto.JobEngagements));

        appearanceEntity.AnonymousProfileAppearanceSoftSkills
            .AddRange(this.GetSofSkillsEntities(anonymousProfileDto.AppearanceDto.SoftSkills));

        appearanceEntity.AnonymousProfileAppearanceITAreas
            .AddRange(this.GetItAreasEntities(anonymousProfileDto.AppearanceDto.ITAreas));

        appearanceEntity.AnonymousProfileAppearanceTechStacks
            .AddRange(this.GetTechStacksEntities(anonymousProfileDto.AppearanceDto.TechStacks));

        AnonymousProfileEntity anonymousProfileEntity = new AnonymousProfileEntity
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

    public Task<IEnumerable<AnonymousProfileListingDTO>> GetProfilesRelevantToJobAd(JobAdCriteriasDTO jobAdCriterias)
    {
        throw new NotImplementedException();
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
