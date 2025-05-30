using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.Cv;
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

    public async Task<AnonymousProfileCvDataDTO> GetAnonymousProfileCvData(string userId)
    {
        return await this.DbSet
            .Where(cv => cv.UserId == userId && cv.AnonymousProfileActivated)
            .To<AnonymousProfileCvDataDTO>()
            .SingleOrDefaultAsync();
    }

    public async Task SetAnonymousProfileActivated(string cvId)
    {
        CurriculumVitaeEntity cvEntity = await this.DbSet.FindAsync(cvId);

        cvEntity.AnonymousProfileActivated = true;

        this.DbSet.Update(cvEntity);
    }

    public async Task DeactivateAnonymousProfile(string cvId)
    {
        CurriculumVitaeEntity cvEntity = await this.DbSet.FindAsync(cvId);

        cvEntity.AnonymousProfileActivated = false;

        this.DbSet.Update(cvEntity);
    }

    public async Task<bool> HasAnyCvWithActivatedAnonymousProfile(string userId)
    {
        return await this.DbSet
            .AnyAsync(cv => cv.UserId == userId && cv.AnonymousProfileActivated);
    }

    public async Task<bool> HasActivatedAnonymousProfile(string cvId)
    {
        bool? hasAnonymousProfile = await this.DbSet
            .Where(cv => cv.Id == cvId)
            .Select(cv => (bool?)cv.AnonymousProfileActivated)
            .SingleOrDefaultAsync();

        base.ValidateForExistence(hasAnonymousProfile, "Curriculum Vitae");

        return hasAnonymousProfile.Value;
    }

    public async Task<T> GetCvData<T>(string cvId) where T: class
    {
        return await this.DbSet.AsNoTracking()
            .Where(cv => cv.Id == cvId)
            .To<T>()
            .SingleAsync();
    }

    public async Task<byte[]> GetCvData(string cvId)
    {
        return await this.DbSet
            .Where(cv => cv.Id == cvId)
            .Select(cv => cv.Data)
            .FirstOrDefaultAsync();
    }

    public async Task SetData(string cvId, byte[] data)
    {
        CurriculumVitaeEntity cvFromDb = await this.DbSet.FindAsync(cvId);

        base.ValidateForExistence(cvFromDb, "CurriculumVitae");

        cvFromDb.Data = data;

        this.DbSet.Update(cvFromDb);
    }

    public async Task Delete(string cvId)
    {
        CurriculumVitaeEntity cv = await this.DbSet.FindAsync(cvId);

        this.DbSet.Remove(cv);
    }
}
