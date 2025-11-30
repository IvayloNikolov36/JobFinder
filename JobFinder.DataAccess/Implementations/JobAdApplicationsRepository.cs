using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.JobAd;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class JobAdApplicationsRepository : EfCoreRepository<JobAdApplicationEntity>, IJobAdApplicationsRepository
{
    private readonly IMapper mapper;

    public JobAdApplicationsRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task Create(JobAddApplicationInputDTO jobAdApplication)
    {
        JobAdApplicationEntity newJobAdApplicationEntity = this.mapper
            .Map<JobAdApplicationEntity>(jobAdApplication);

        newJobAdApplicationEntity.AppliedOn = DateTime.UtcNow;

        await this.DbSet.AddAsync(newJobAdApplicationEntity);
    }

    public async Task<IEnumerable<T>> GetJobAdApplications<T>(int jobAdId)
    {
        return await this.DbSet.AsNoTracking()
            .Where(j => j.JobAdId == jobAdId)
            .OrderByDescending(j => j.AppliedOn)
            .To<T>()
            .ToListAsync();
    }

    public IAsyncEnumerable<JobAdApplicationDTO> GetUserApplications(string userId)
    {
        return this.DbSet.AsNoTracking()
            .Where(j => j.ApplicantId == userId)
            .OrderByDescending(j => j.AppliedOn)
            .To<JobAdApplicationDTO>()
            .AsAsyncEnumerable();
    }

    public async Task<bool> HasAlreadyApplied(string applicantId, int jobAdId)
    {
        return await this.DbSet
            .AnyAsync(j => j.ApplicantId == applicantId
                && j.JobAdId == jobAdId);
    }

    public async Task<bool> IsApplicationSent(string cvId, int jobAdId, string publisherId)
    {
        return await this.DbSet
            .AnyAsync(jaa => jaa.CvId == cvId
                && jaa.JobAdId == jobAdId
                && jaa.JobAd.Publisher.UserId == publisherId);
    }

    public async Task<DateTime> SetPreviewed(string cvId, int jobAdId)
    {
        JobAdApplicationEntity application = await this.DbSet
            .SingleOrDefaultAsync(a => a.CvId == cvId
                && a.JobAdId == jobAdId);

        base.ValidateForExistence(application, nameof(JobAdApplicationEntity));

        DateTime previewDate = DateTime.UtcNow;
        application.PreviewDate = previewDate;

        this.DbSet.Update(application);

        return previewDate;
    }
    public void DeleteAll(string cvId)
    {
        IQueryable<JobAdApplicationEntity> records = this.DbSet
            .Where(ja => ja.CvId == cvId);

        this.DbSet.RemoveRange(records);
    }
}
