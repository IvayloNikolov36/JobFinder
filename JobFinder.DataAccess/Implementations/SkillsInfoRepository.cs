using AutoMapper;
using JobFinder.Data;
using JobFinder.Data.Models.Cv;
using JobFinder.Data.Models.CV;
using JobFinder.DataAccess.Contracts;
using JobFinder.DataAccess.Generic;
using JobFinder.Transfer.DTOs.CV;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.DataAccess.Implementations;

public class SkillsInfoRepository : EfCoreRepository<SkillsInfoEntity>, ISkillsInfoRepository
{
    private readonly IMapper mapper;

    public SkillsInfoRepository(JobFinderDbContext context, IMapper mapper) : base(context)
    {
        this.mapper = mapper;
    }

    public async Task Update(SkillsInfoEditDTO skillsInfoDto)
    {
        SkillsInfoEntity skillFromDb = await this.DbSet
            .Where(x => x.Id == skillsInfoDto.Id)
            .Include(x => x.SkillsInfoDrivingCategories)
            .FirstOrDefaultAsync();

        base.ValidateForExistence(skillFromDb, "SkillsInfo");

        this.mapper.Map(skillsInfoDto, skillFromDb);

        this.UpdateDrivingLicenseCategories(
            skillsInfoDto.DrivingLicenseCategoryIds,
            skillFromDb.SkillsInfoDrivingCategories);

        this.DbSet.Update(skillFromDb);
    }

    public async Task Delete(string cvId)
    {
        SkillsInfoEntity skillsInfoEntity = await this.DbSet
            .FirstOrDefaultAsync(s => s.CurriculumVitaeId == cvId);

        base.ValidateForExistence(skillsInfoEntity, "SkillsInfo");

        this.DbSet.Remove(skillsInfoEntity);
    }

    private void UpdateDrivingLicenseCategories(
        IEnumerable<int> drivingCategoryIds,
        List<SkillsInfoDrivingCategoryEntity> skillsInfoDrivingCategoryEntities)
    {
        IEnumerable<int> licenseCategoriesToAdd = drivingCategoryIds
            .Where(id => !skillsInfoDrivingCategoryEntities
                .Any(x => x.DrivingCategoryId == id));

        List<SkillsInfoDrivingCategoryEntity> licenseCategoriesToRemove = skillsInfoDrivingCategoryEntities
            .Where(x => !drivingCategoryIds.Contains(x.DrivingCategoryId))
            .ToList();

        if (licenseCategoriesToAdd.Any())
        {
            skillsInfoDrivingCategoryEntities
                .AddRange(licenseCategoriesToAdd
                    .Select(id => new SkillsInfoDrivingCategoryEntity { DrivingCategoryId = id }));
        }

        if (licenseCategoriesToRemove.Count > 0)
        {
            licenseCategoriesToRemove
                .ForEach(entityToRemove => skillsInfoDrivingCategoryEntities
                    .Remove(entityToRemove));
        }
    }
}
