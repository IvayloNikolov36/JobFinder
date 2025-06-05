using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs.AnonymousProfile;

namespace JobFinder.DataAccess.Implementations;

public class AnonymousProfileAppearanceRepository :
    EfCoreRepository<AnonymousProfileAppearanceEntity>,
    IAnonymousProfileAppearanceRepository
{
    public AnonymousProfileAppearanceRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task Create(string cvId, AnonymousProfileAppearanceCreateDTO profileAppearance)
    {
        AnonymousProfileAppearanceEntity entity = new()
        {
            CurriculumVitaeId = cvId,
            RemoteJobPreferenceId = profileAppearance.RemoteJobPreferenceId,
            JobCategoryId = profileAppearance.JobCategoryId,
            PreferredPositions = profileAppearance.PreferredPositions
        };

        entity.AnonymousProfileAppearanceJobEngagements
            .AddRange(this.GetJobEngagementsEntities(profileAppearance.JobEngagements));

        entity.AnonymousProfileAppearanceSoftSkills
            .AddRange(this.GetSofSkillsEntities(profileAppearance.SoftSkills));

        entity.AnonymousProfileAppearanceITAreas
            .AddRange(this.GetItAreasEntities(profileAppearance.ITAreas));

        entity.AnonymousProfileAppearanceTechStacks
            .AddRange(this.GetTechStacksEntities(profileAppearance.TechStacks));

        await this.DbSet.AddAsync(entity);
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
