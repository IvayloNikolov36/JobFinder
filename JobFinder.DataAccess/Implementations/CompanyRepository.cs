using AutoMapper;
using JobFinder.Common.Exceptions;
using JobFinder.Data;
using JobFinder.Data.Models;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Company;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CompanyRepository : EfCoreRepository<CompanyEntity>, ICompanyRepository
{
    private readonly IMapper mapper;

    public CompanyRepository(JobFinderDbContext context, IMapper mapper)
        : base(context)
    {
        this.mapper = mapper;
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

    public async Task<string> GetUserId(int companyId)
    {
        string companyUserId = await this.DbSet
            .Where(c => c.Id == companyId)
            .Select(c => c.UserId)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(companyUserId, nameof(CompanyEntity));

        return companyUserId;
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

    public async Task<CompanyJobAdsListingDTO> AllActiveAds(int companyId)
    {
        CompanyJobAdsListingDTO companyAds = await this.DbSet.AsNoTracking()
            .Where(c => c.Id == companyId)
            .To<CompanyJobAdsListingDTO>()
            .SingleOrDefaultAsync();

        base.ValidateForExistence(companyAds, nameof(CompanyEntity));

        return companyAds;
    }

    public IAsyncEnumerable<CompanyListingDTO> GetAll(string userId)
    {
        return this.DbSet.AsNoTracking()
            .To<CompanyListingDTO>( new { userId })
            .AsAsyncEnumerable();
    }

    public async Task SetLogoImageId(int companyId, int imageId)
    {
        CompanyEntity company = await this.DbSet.FindAsync(companyId);

        company.LogoImageId = imageId;

        this.DbSet.Update(company);
    }

    public async Task Update(int id, CompanyEditDTO companyDto)
    {
        CompanyEntity company = await this.DbSet.FindAsync(id);

        base.ValidateForExistence(company, nameof(CompanyEntity));

        this.mapper.Map(companyDto, company);

        this.DbSet.Update(company);
    }
}
