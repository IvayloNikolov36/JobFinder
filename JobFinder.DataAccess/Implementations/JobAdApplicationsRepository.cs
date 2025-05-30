using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class JobAdApplicationsRepository : EfCoreRepository<JobAdApplicationEntity>, IJobAdApplicationsRepository
{
    public JobAdApplicationsRepository(JobFinderDbContext context) : base(context)
    {

    }

    public async Task<bool> IsApplicationSent(string cvId, int jobAdId, string publisherId)
    {
        return await this.DbSet
            .AnyAsync(jaa => jaa.CurriculumVitaeId == cvId
                && jaa.JobAdId == jobAdId
                && jaa.JobAd.Publisher.UserId == publisherId);
    }
}
