using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using JobFinder.Transfer.DTOs.CV;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CurriculumVitaeRepository : EfCoreRepository<CurriculumVitaeEntity>, ICurriculumVitaeRepository
{
    private readonly IMapper mapper;

    public CurriculumVitaeRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task<string> GetUserId(string curriculumVitaeId)
    {
        string userId = await this.DbSet
            .Where(cv => cv.Id == curriculumVitaeId)
            .Select(cv => cv.UserId)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(userId, "CurriculumVitae");

        return userId;
    }

    public async Task<AnonymousProfileCvDataDTO> GetAnonymousProfileCvData(string userId)
    {
        return await this.DbSet
            .Where(cv => cv.UserId == userId && cv.AnonymousProfileActivated)
            .To<AnonymousProfileCvDataDTO>()
            .SingleOrDefaultAsync();
    }
}
