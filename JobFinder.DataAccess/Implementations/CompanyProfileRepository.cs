using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CompanyProfileRepository : EfCoreRepository<CompanyEntity>, ICompanyProfileRepository
{
    public CompanyProfileRepository(JobFinderDbContext context) : base(context)
    {
    }

    public async Task<CompanyProfileDataDTO> Get(string userId)
    {
        return await this.DbSet.AsNoTracking()
            .Where(c => c.UserId == userId)
            .To<CompanyProfileDataDTO>()
            .SingleOrDefaultAsync();
    }
}
