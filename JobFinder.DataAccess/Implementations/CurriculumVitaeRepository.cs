using JobFinder.Data;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Services.Mappings;
using JobFinder.Transfer.DTOs;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class CurriculumVitaeRepository : EfCoreRepository<CurriculumVitaeEntity>, ICurriculumVitaeRepository
{
    public CurriculumVitaeRepository(JobFinderDbContext context) : base(context)
    {
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

    public async Task SetAnonymousProfileCreated(string cvId)
    {
        CurriculumVitaeEntity cvEntity = await this.DbSet.FindAsync(cvId);

        cvEntity.AnonymousProfileActivated = true;

        this.DbSet.Update(cvEntity);
    }
}
