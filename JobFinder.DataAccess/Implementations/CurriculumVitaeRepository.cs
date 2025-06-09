using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.Cv;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
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

    public async Task<IEnumerable<CVListingDTO>> All(string userId)
    {
        return await this.DbSet.AsNoTracking()
            .Where(c => c.UserId == userId)
            .To<CVListingDTO>()
            .ToArrayAsync();
    }

    public async Task Create(string userId, CVCreateDTO cvData)
    {
        CurriculumVitaeEntity cvEntity = new();
        string id = cvEntity.Id;

        this.mapper.Map(cvData, cvEntity);
        cvEntity.Id = id; // TODO: fix this

        if (cvData.Skills.DrivingLicenseCategoryIds.Any())
        {
            cvEntity.Skills.HasDrivingLicense = true;
            cvEntity.Skills.SkillsInfoDrivingCategories
                .AddRange(
                    cvData.Skills.DrivingLicenseCategoryIds
                    .Select(dcId => new SkillsInfoDrivingCategoryEntity { DrivingCategoryId = dcId })
                );
        }

        cvEntity.UserId = userId;
        cvEntity.CreatedOn = DateTime.UtcNow;

        await this.DbSet.AddAsync(cvEntity);
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

    public async Task<T> GetCvData<T>(string cvId) where T: class
    {
        return await this.DbSet.AsNoTracking()
            .Where(cv => cv.Id == cvId)
            .To<T>()
            .SingleAsync();
    }

    public async Task Delete(string cvId)
    {
        CurriculumVitaeEntity cv = await this.DbSet.FindAsync(cvId);

        this.DbSet.Remove(cv);
    }
}
