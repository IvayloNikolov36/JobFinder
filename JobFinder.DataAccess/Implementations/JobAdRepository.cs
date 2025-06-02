using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.Company;
using JobFinder.Transfer.DTOs.JobAd;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class JobAdRepository : EfCoreRepository<JobAdvertisementEntity>, IJobAdRepository
{
    private readonly IMapper mapper;

    public JobAdRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<JobAdDetailsDTO> Get(int id)
    {
        JobAdDetailsDTO jobAd = await this.DbSet.AsNoTracking()
            .Where(ja => ja.Id == id)
            .To<JobAdDetailsDTO>()
            .SingleOrDefaultAsync();

        base.ValidateForExistence(jobAd, "JobAdvertisement");

        return jobAd;
    }

    public async Task Create(JobAdCreateDTO jobAd, int companyId)
    {
        JobAdvertisementEntity jobAdEntity = new JobAdvertisementEntity();

        this.mapper.Map(jobAd, jobAdEntity);

        jobAdEntity.PublisherId = companyId;
        jobAdEntity.PublishDate = DateTime.UtcNow;
        // TODO: create JobAdvertisementEntity IsActive default value true
        jobAdEntity.IsActive = true;

        await this.DbSet.AddAsync(jobAdEntity);
    }

    public async Task<string> GetPublisher(int jobAdId)
    {
        string jobAdPublisherId = await this.DbSet
            .Where(ja => ja.Id == jobAdId)
            .Select(ja => ja.Publisher.UserId)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(jobAdPublisherId, "JobAdvertisement");

        return jobAdPublisherId;
    }

    public async Task Update(int id, JobAdEditDTO jobAdDto)
    {
        JobAdvertisementEntity jobAdFromDb = await this.DbSet.FindAsync(id);

        base.ValidateForExistence(jobAdFromDb, "JobAdvertisement");

        this.mapper.Map(jobAdDto, jobAdFromDb);

        this.DbSet.Update(jobAdFromDb);
    }

    public Task Update(JobAdEditDTO jobAdDto)
    {
        throw new NotImplementedException();
    }

    public async Task<DataListingDTO<JobAdListingDTO>> AllActive(JobAdFilterDTO filter)
    {
        IQueryable<JobAdvertisementEntity> jobs = this.DbSet
            .AsNoTracking()
            .Where(ja => ja.IsActive);

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

        base.ValidateForExistence(data, "JobAdvertisement");

        return data;
    }

    private IQueryable<JobAdvertisementEntity> FilteredByCategory(
        IQueryable<JobAdvertisementEntity> jobAds,
        int jobCategoryId)
    {
        return jobAds.Where(j => j.JobCategoryId == jobCategoryId);
    }

    private IQueryable<JobAdvertisementEntity> FilterByEngagement(
        IQueryable<JobAdvertisementEntity> jobAds,
        int jobEngagementId)
    {
        return jobAds.Where(j => j.JobEngagementId == jobEngagementId);
    }

    private IQueryable<JobAdvertisementEntity> FilterByLocation(
        IQueryable<JobAdvertisementEntity> jobAds,
        int locationId)
    {
        return jobAds.Where(j => j.LocationId == locationId);
    }

    private IQueryable<JobAdvertisementEntity> SortBySalary(
        IQueryable<JobAdvertisementEntity> jobAds,
        bool isAscending)
    {
        return isAscending
            ? jobAds.OrderBy(j => j.MaxSalary)
            : jobAds.OrderByDescending(j => j.MinSalary);
    }

    private IQueryable<JobAdvertisementEntity> SortByPublishDate(
        IQueryable<JobAdvertisementEntity> jobAds,
        bool isAscending)
    {
        return isAscending
            ? jobAds.OrderBy(j => j.CreatedOn)
            : jobAds.OrderByDescending(j => j.CreatedOn);
    }

    public async Task ExecuteJobAdsDeactivate(DateTime publishDateTreshold)
    {
        await this.DbSet
            .Where(ja => ja.IsActive && ja.PublishDate <= publishDateTreshold)
            .ExecuteUpdateAsync(s => s
                .SetProperty(ja => ja.IsActive, ja => !ja.IsActive));
    }

    public async Task<IEnumerable<CompanyJobAdDTO>> GetFilteredCompanyAds(string userId, bool? active)
    {
        IQueryable<JobAdvertisementEntity> query = this.DbSet.AsNoTracking()
            .Where(ja => ja.Publisher.UserId == userId);

        if (active.HasValue)
        {
            query = query.Where(ja => ja.IsActive == active);
        }

        return await query
            .OrderByDescending(j => j.PublishDate)
            .To<CompanyJobAdDTO>()
            .ToListAsync();
    }
}
