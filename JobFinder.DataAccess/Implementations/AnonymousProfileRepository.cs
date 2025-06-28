using JobFinder.Data;
using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.AnonymousProfile;
using JobFinder.Transfer.DTOs.Cv;
using JobFinder.Transfer.DTOs.JobAd;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
            .Select(ap => ap.Cv)
            .To<AnonymousProfileDataDTO>()
            .SingleOrDefaultAsync();
    }

    public async Task<AnonymousProfileDataDTO> GetAnonymousProfileData(string userId)
    {
        return await this.DbSet
            .Where(ap => ap.UserId == userId)
            .Select(ap => ap.Cv)
            .To<AnonymousProfileDataDTO>()
            .SingleOrDefaultAsync();
    }

    public async Task<MyAnonymousProfileDataDTO> GetMyAnonymousProfileData(string userId)
    {
        return await this.DbSet
            .Where(ap => ap.UserId == userId)
            .Select(ap => ap.Cv)
            .To<MyAnonymousProfileDataDTO>()
            .SingleOrDefaultAsync();
    }

    public async Task Create(string cvId, string userId, AnonymousProfileCreateDTO anonymousProfileDto)
    {
        DateTime now = DateTime.UtcNow;

        AnonymousProfileAppearanceEntity appearanceEntity = new()
        {
            JobCategoryId = anonymousProfileDto.AppearanceDto.JobCategoryId,
            PreferredPositions = anonymousProfileDto.AppearanceDto.PreferredPositions,
            CreatedOn = now
        };

        AnonymousProfileAppearanceDTO appearanceDto = anonymousProfileDto.AppearanceDto;

        appearanceEntity.WorkplaceTypes
            .AddRange(this.GetWorkingplaceTypesEntities(appearanceDto.WorkplaceTypes));

        appearanceEntity.JobEngagements
            .AddRange(this.GetJobEngagementsEntities(appearanceDto.JobEngagements));

        appearanceEntity.SoftSkills
            .AddRange(this.GetSofSkillsEntities(appearanceDto.SoftSkills));

        appearanceEntity.ITAreas
            .AddRange(this.GetItAreasEntities(appearanceDto.ITAreas));

        appearanceEntity.TechStacks
            .AddRange(this.GetTechStacksEntities(appearanceDto.TechStacks));

        appearanceEntity.Cities
            .AddRange(this.GetCityEntities(appearanceDto.Cities));

        AnonymousProfileEntity anonymousProfileEntity = new()
        {
            CvId = cvId,
            UserId = userId,
            Appearance = appearanceEntity,
            CreatedOn = now
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

        return entity.CvId;
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
            .Where(this.ExpressionForAnonymousProfileJobAdRelevance(jobAdCriterias))
            .Select(ap => new AnonymousProfileListingDTO
            {
                Id = ap.Id,
                ActivateDate = ap.CreatedOn
            })
            .ToListAsync();

        return data;
    }

    public async Task<bool> IsAnonymousProfileRelevantForJobAd(string id, JobAdCriteriasDTO jobAdCriterias)
    {
        bool isRelevant = await this.DbSet
            .Where(ap => ap.Id == id)
            .Where(this.ExpressionForAnonymousProfileJobAdRelevance(jobAdCriterias))
            .AnyAsync();

        return isRelevant;
    }

    public async Task<IEnumerable<CvPreviewRequestListingDTO>> GetAllCvPreviewRequests(string userId)
    {
        return await this.DbSet.AsNoTracking()
            .Where(ap => ap.UserId == userId)
            .SelectMany(ap => ap.CvPreviewRequests)
            .To<CvPreviewRequestListingDTO>()
            .ToArrayAsync();
    }

    private Expression<Func<AnonymousProfileEntity, bool>> ExpressionForAnonymousProfileJobAdRelevance(JobAdCriteriasDTO jobAdCriterias)
    {
        Expression<Func<AnonymousProfileEntity, bool>> expr = (AnonymousProfileEntity ap) =>
            ap.Appearance.JobCategoryId == jobAdCriterias.JobCategoryId
            && ap.Appearance
                .JobEngagements
                .Select(apje => apje.JobEngagementId)
                .Contains(jobAdCriterias.JobEngagementId)
            && ap.Appearance
                .Cities
                .Select(apc => apc.CityId)
                .Contains(jobAdCriterias.CityId)
            && ap.Appearance
                .JobEngagements
                .Select(je => je.JobEngagementId)
                .Contains(jobAdCriterias.JobEngagementId)
            && jobAdCriterias
                .SoftSkills
                .All(adSkill => ap.Appearance
                    .SoftSkills
                    .Select(ss => ss.SoftSkillId)
                    .Contains(adSkill))
            && ap.Appearance
                .WorkplaceTypes
                .Select(apw => apw.WorkplaceTypeId)
                .Contains(jobAdCriterias.WorkplaceTypeId);

        return expr;
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

    private IEnumerable<AnonymousProfileAppearanceCityEntity> GetCityEntities(IEnumerable<int> cities)
    {
        List<AnonymousProfileAppearanceCityEntity> cityEntities = new();

        foreach (int cityId in cities)
        {
            cityEntities.Add(new AnonymousProfileAppearanceCityEntity
            {
                CityId = cityId
            });
        }

        return cityEntities;
    }
}
