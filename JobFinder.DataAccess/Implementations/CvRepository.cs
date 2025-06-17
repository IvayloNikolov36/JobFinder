using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.Cv;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs.Cv;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CvRepository : EfCoreRepository<CurriculumVitaeEntity>, ICvRepository
{
    private readonly IMapper mapper;

    public CvRepository(JobFinderDbContext context, IMapper mapper) : base(context)
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

        this.mapper.Map(cvData, cvEntity);

        if (cvData.Skills.DrivingLicenseCategoryIds.Any())
        {
            cvEntity.Skills.SkillsInfoDrivingCategories
                .AddRange(
                    cvData.Skills
                        .DrivingLicenseCategoryIds
                        .Select(dcId => new SkillsInfoDrivingCategoryEntity
                        {
                            DrivingCategoryId = dcId
                        })
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

        base.ValidateForExistence(userId, "CV");

        return userId;
    }

    public async Task<T> GetCvData<T>(string cvId) where T : class
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
