using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class JobAdRepository : EfCoreRepository<JobAdvertisementEntity>, IJobAdRepository
{
    public JobAdRepository(JobFinderDbContext context) : base(context)
    {
    }

    public async Task<string> GetPublisher(string userId, int jobAdId)
    {
        string jobAdPublisherId = await this.DbSet
            .Where(ja => ja.Id == jobAdId)
            .Select(ja => ja.Publisher.UserId)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(jobAdPublisherId, "JobAdvertisement");

        return jobAdPublisherId;
    }
}
