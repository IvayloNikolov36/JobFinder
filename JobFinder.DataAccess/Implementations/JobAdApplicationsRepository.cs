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

    public async Task<IEnumerable<JobAdApplicationDTO>> GetUserApplications(string userId)
    {
        return await this.DbSet.AsNoTracking()
            .Where(j => j.ApplicantId == userId)
            .OrderByDescending(j => j.AppliedOn)
            .To<JobAdApplicationDTO>()
            .ToListAsync();
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
            .AnyAsync(jaa => jaa.CurriculumVitaeId == cvId
                && jaa.JobAdId == jobAdId
                && jaa.JobAd.Publisher.UserId == publisherId);
    }

    public async Task<DateTime> SetPreviewed(string cvId, int jobAdId)
    {
        JobAdApplicationEntity application = await this.DbSet
            .SingleOrDefaultAsync(a => a.CurriculumVitaeId == cvId
                && a.JobAdId == jobAdId);

        base.ValidateForExistence(application, "JobAdApplication");

        DateTime previewDate = DateTime.UtcNow;
        application.PreviewDate = previewDate;

        this.DbSet.Update(application);

        return previewDate;
    }
}
