using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.Data.Models.Enums;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Transfer.DTOs.JobAd;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class JobAdRepository : EfCoreRepository<JobAdEntity>, IJobAdRepository
{
    private readonly IMapper mapper;

    public JobAdRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<CompanyJobAdDetailsDTO> Get(int id)
    {
        CompanyJobAdDetailsDTO jobAd = await this.DbSet.AsNoTracking()
            .Where(ja => ja.Id == id)
            .To<CompanyJobAdDetailsDTO>()
            .SingleOrDefaultAsync();

        base.ValidateForExistence(jobAd, nameof(JobAdEntity));

        return jobAd;
    }

    public async Task Create(JobAdCreateDTO jobAd, int companyId)
    {
        JobAdEntity jobAdEntity = new();

        this.mapper.Map(jobAd, jobAdEntity);

        jobAdEntity.PublisherId = companyId;
        jobAdEntity.PublishDate = DateTime.UtcNow;
        jobAdEntity.LifecycleStatusId = (int)LifecycleStatusEnum.Draft;

        if (jobAd.SoftSkills.Any())
        {
            jobAdEntity.JobAdSoftSkills
                .AddRange(this.GetJobAdSoftSkillsEntities(jobAd.SoftSkills));
        }

        if (jobAd.ITAreas.Any())
        {
            jobAdEntity.JobAdITAreas
                .AddRange(this.GetJobAdITAreasEntities(jobAd.ITAreas));
        }

        if (jobAd.TechStacks.Any())
        {
            jobAdEntity.JobAdTechStacks
                .AddRange(this.GetJobAdTechStackEntities(jobAd.TechStacks));
        }

        await this.DbSet.AddAsync(jobAdEntity);
    }

    public async Task<string> GetPublisher(int jobAdId)
    {
        string jobAdPublisherId = await this.DbSet
            .Where(ja => ja.Id == jobAdId)
            .Select(ja => ja.Publisher.UserId)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(jobAdPublisherId, nameof(JobAdEntity));

        return jobAdPublisherId;
    }

    public async Task Update(int id, JobAdEditDTO jobAdDto)
    {
        JobAdEntity jobAdFromDb = await this.DbSet
            .Include(ja => ja.JobAdSoftSkills)
            .Include(ja => ja.JobAdITAreas)
            .Include(ja => ja.JobAdTechStacks)
            .Where(ja => ja.Id == id)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(jobAdFromDb, nameof(JobAdEntity));

        this.mapper.Map(jobAdDto, jobAdFromDb);

        this.UpdateSoftSkills(jobAdDto.SoftSkills, jobAdFromDb);

        this.UpdateItAreas(jobAdDto.ITAreas, jobAdFromDb);

        this.UpdateTechStack(jobAdDto.TechStacks, jobAdFromDb);

        this.DbSet.Update(jobAdFromDb);
    }

    public async Task<DataListingDTO<JobAdListingDTO>> AllActive(JobAdFilterDTO filter)
    {
        IQueryable<JobAdEntity> jobs = this.DbSet
            .AsNoTracking()
            .Where(ja => ja.LifecycleStatusId == (int)LifecycleStatusEnum.Active);

        if (!string.IsNullOrEmpty(filter.SearchText?.Trim()))
        {
            filter.SearchText = filter.SearchText.ToLower();

            jobs = jobs.Where(j => j.Position.ToLower().Contains(filter.SearchText)
                || j.Publisher.Name.ToLower().Contains(filter.SearchText));
        }

        if (filter.EngagementId.HasValue)
        {
            jobs = this.FilterByEngagement(jobs, filter.EngagementId.Value);
        }

        if (filter.CategoryId.HasValue)
        {
            jobs = this.FilteredByCategory(jobs, filter.CategoryId.Value);
        }

        if (filter.LocationId.HasValue)
        {
            jobs = this.FilterByLocation(jobs, filter.LocationId.Value);
        }

        if (filter.Intership)
        {
            jobs = jobs.Where(ja => ja.Intership);
        }

        if (filter.SpecifiedSalary)
        {
            jobs = jobs.Where(ja => ja.MinSalary.HasValue && ja.MaxSalary.HasValue);
        }

        if (!string.IsNullOrEmpty(filter.SortBy) && filter.SortBy == "Salary")
        {
            jobs = this.SortBySalary(jobs, filter.IsAscending);
        }

        if (!string.IsNullOrEmpty(filter.SortBy) && filter.SortBy == "Published")
        {
            jobs = this.SortByPublishDate(jobs, filter.IsAscending);
        }

        int totalCount = await jobs.CountAsync();

        IEnumerable<JobAdListingDTO> jobAds = await jobs.Skip((filter.Page - 1) * filter.Items)
           .Take(filter.Items)
           .To<JobAdListingDTO>()
           .ToListAsync();

        return new DataListingDTO<JobAdListingDTO>(totalCount, jobAds);
    }

    public async Task<JobAdDetailsForSubscriberDTO> GetDetailsForSubscriber(int jobAdId)
    {
        JobAdDetailsForSubscriberDTO data = await this.DbSet.AsNoTracking()
            .Where(ja => ja.Id == jobAdId)
            .To<JobAdDetailsForSubscriberDTO>()
            .SingleOrDefaultAsync();

        base.ValidateForExistence(data, nameof(JobAdEntity));

        return data;
    }

    public async Task<JobAdCriteriasDTO> GetJobAdCriterias(int jobAdId)
    {
        JobAdCriteriasDTO jobAdCriterias = await this.DbSet.AsNoTracking()
            .Where(ja => ja.Id == jobAdId)
            .To<JobAdCriteriasDTO>()
            .SingleOrDefaultAsync();

        base.ValidateForExistence(jobAdCriterias, nameof(JobAdEntity));

        return jobAdCriterias;
    }

    private void UpdateItAreas(IEnumerable<int> itAreaIds, JobAdEntity adEntity)
    {
        var itAreasFromDb = adEntity.JobAdITAreas;

        List<int> itAreasToAdd = itAreaIds
            .Where(id => !itAreasFromDb.Any(a => a.ItAreaId == id))
            .ToList();

        itAreasToAdd.ForEach(id => adEntity.JobAdITAreas
            .Add(new JobAdItAreaEntity
            {
                ItAreaId = id
            }));

        List<JobAdItAreaEntity> itAreaEntitiesToRemove = itAreasFromDb
            .Where(a => !itAreaIds.Contains(a.ItAreaId))
            .ToList();

        if (itAreaEntitiesToRemove.Count > 0)
        {
            itAreaEntitiesToRemove.ForEach(e => adEntity.JobAdITAreas.Remove(e));
        }
    }

    private void UpdateTechStack(IEnumerable<int> techStackIds, JobAdEntity adEntity)
    {
        List<JobAdTechStackEntity> teckStackFromDb = adEntity.JobAdTechStacks;

        List<int> techStackToAdd = techStackIds
            .Where(id => !teckStackFromDb.Any(ts => ts.TechStackId == id))
            .ToList();

        techStackToAdd.ForEach(id => adEntity.JobAdTechStacks
            .Add(new JobAdTechStackEntity
            {
                TechStackId = id,
            }));

        List<JobAdTechStackEntity> techStackEntitiesToRemove = teckStackFromDb
            .Where(x => !techStackIds.Contains(x.TechStackId))
            .ToList();

        if (techStackEntitiesToRemove.Count > 0)
        {
            techStackEntitiesToRemove.ForEach(e => adEntity.JobAdTechStacks.Remove(e));
        }
    }

    private void UpdateSoftSkills(IEnumerable<int> softSkillIds, JobAdEntity adEntity)
    {
        List<JobAdSoftSkillEntity> adSoftskillsFromDB = adEntity.JobAdSoftSkills;

        IEnumerable<int> softSkillsFromDb = adSoftskillsFromDB
            .Select(ss => ss.SoftSkillId);

        List<int> softSkillsIdsToAdd = softSkillIds
            .Where(ss => !softSkillsFromDb.Contains(ss))
            .ToList();

        softSkillsIdsToAdd.ForEach(id => adEntity.JobAdSoftSkills
            .Add(new JobAdSoftSkillEntity()
            {
                SoftSkillId = id
            }));

        List<JobAdSoftSkillEntity> softSkillsToRemove = adSoftskillsFromDB
            .Where(jass => !softSkillIds.Contains(jass.SoftSkillId))
            .ToList();

        if (softSkillsToRemove.Count > 0)
        {
            softSkillsToRemove.ForEach(ss => adEntity.JobAdSoftSkills.Remove(ss));
        }
    }

    private IQueryable<JobAdEntity> FilteredByCategory(
        IQueryable<JobAdEntity> jobAds,
        int jobCategoryId)
    {
        return jobAds.Where(j => j.JobCategoryId == jobCategoryId);
    }

    private IQueryable<JobAdEntity> FilterByEngagement(
        IQueryable<JobAdEntity> jobAds,
        int jobEngagementId)
    {
        return jobAds.Where(j => j.JobEngagementId == jobEngagementId);
    }

    private IQueryable<JobAdEntity> FilterByLocation(
        IQueryable<JobAdEntity> jobAds,
        int locationId)
    {
        return jobAds.Where(j => j.LocationId == locationId);
    }

    private IQueryable<JobAdEntity> SortBySalary(
        IQueryable<JobAdEntity> jobAds,
        bool isAscending)
    {
        return isAscending
            ? jobAds.OrderBy(j => j.MaxSalary)
            : jobAds.OrderByDescending(j => j.MinSalary);
    }

    private IQueryable<JobAdEntity> SortByPublishDate(
        IQueryable<JobAdEntity> jobAds,
        bool isAscending)
    {
        return isAscending
            ? jobAds.OrderBy(j => j.CreatedOn)
            : jobAds.OrderByDescending(j => j.CreatedOn);
    }

    public async Task ExecuteJobAdsDeactivate(DateTime publishDateTreshold)
    {
        await this.DbSet
            .Where(ja => ja.LifecycleStatusId == (int)LifecycleStatusEnum.Active
                && ja.PublishDate <= publishDateTreshold)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ja => ja.LifecycleStatusId, (int)LifecycleStatusEnum.Retired));
    }

    public async Task<IEnumerable<CompanyJobAdDTO>> GetFilteredCompanyAds(
        string userId,
        int? lifecycleStatus)
    {
        IQueryable<JobAdEntity> query = this.DbSet.AsNoTracking()
            .Where(ja => ja.Publisher.UserId == userId);

        if (lifecycleStatus.HasValue)
        {
            query = query.Where(ja => ja.LifecycleStatusId == lifecycleStatus);
        }

        return await query
            .OrderByDescending(j => j.PublishDate)
            .To<CompanyJobAdDTO>()
            .ToListAsync();
    }

    public async Task<LifecycleStatusEnum> GetLifecycleStatus(int id)
    {
        int? status = await this.DbSet.Where(a => a.Id == id)
            .Select(a => (int?)a.LifecycleStatusId)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(status, nameof(JobAdEntity));

        return (LifecycleStatusEnum)status.Value;
    }

    public async Task Activate(int id)
    {
        JobAdEntity ad = await this.DbSet.FindAsync(id);
        base.ValidateForExistence(ad, nameof(JobAdEntity));
        ad.LifecycleStatusId = (int)LifecycleStatusEnum.Active;
        this.DbSet.Update(ad);
    }

    public async Task Retire(int id)
    {
        JobAdEntity jobAd = await this.DbSet.FindAsync(id);

        base.ValidateForExistence(jobAd, nameof(JobAdEntity));

        jobAd.LifecycleStatusId = (int)LifecycleStatusEnum.Retired;

        this.DbSet.Update(jobAd);
    }

    private IEnumerable<JobAdTechStackEntity> GetJobAdTechStackEntities(IEnumerable<int> techStacks)
    {
        List<JobAdTechStackEntity> entities = [];

        foreach (int techStackId in techStacks)
        {
            entities.Add(new JobAdTechStackEntity { TechStackId = techStackId });
        }

        return entities;
    }

    private IEnumerable<JobAdItAreaEntity> GetJobAdITAreasEntities(IEnumerable<int> itAreas)
    {
        List<JobAdItAreaEntity> entities = [];

        foreach (int itAreaId in itAreas)
        {
            entities.Add(new JobAdItAreaEntity { ItAreaId = itAreaId });
        }

        return entities;
    }

    private IEnumerable<JobAdSoftSkillEntity> GetJobAdSoftSkillsEntities(IEnumerable<int> softSkills)
    {
        List<JobAdSoftSkillEntity> entities = [];

        foreach (int softSkillId in softSkills)
        {
            entities.Add(new JobAdSoftSkillEntity { SoftSkillId = softSkillId });
        }

        return entities;
    }
}
