using JobFinder.Common.Exceptions;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.Data.Models.AnonymousProfile;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CompanyRepository : EfCoreRepository<CompanyEntity>, ICompanyRepository
{
    public CompanyRepository(JobFinderDbContext context) : base(context)
    {
    }

    public async Task<CompanyProfileDataDTO> Get(string userId)
    {
        return await this.DbSet.AsNoTracking()
            .Where(c => c.UserId == userId)
            .To<CompanyProfileDataDTO>()
            .SingleOrDefaultAsync();
    }

    public async Task<int> GetCompanyId(string userId)
    {
        int? companyId = await this.DbSet
                .Where(c => c.UserId == userId)
                .Select(c => (int?)c.Id)
                .SingleOrDefaultAsync();

        if (!companyId.HasValue)
        {
            throw new ActionableException($"There is no company related to user with id {userId}!");
        }

        return companyId.Value;
    }

    public async Task<CompanyDetailsUserDTO> GetDetails(int companyId, string currentUserId)
    {
        CompanyDetailsUserDTO companyDetails = await this.DbSet.AsNoTracking()
            .Where(c => c.Id == companyId)
            .To<CompanyDetailsUserDTO>(new { currentUserId })
            .SingleOrDefaultAsync();

        base.ValidateForExistence(companyDetails, nameof(CompanyEntity));

        return companyDetails;
    }
}
