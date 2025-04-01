namespace JobFinder.Services.Implementations.CV
{
    using AutoMapper;
    using JobFinder.Data.Models.Cv;
    using JobFinder.Data.Models.CV;
    using JobFinder.DataAccess.Generic;
    using JobFinder.Services.CV;
    using JobFinder.Web.Models.CVModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SkillsInfosService : ISkillsInfosService
    {
        private readonly IRepository<SkillsInfoEntity> repository;
        private readonly IMapper mapper;

        public SkillsInfosService(
            IRepository<SkillsInfoEntity> skillRepository,
            IMapper mapper)
        {
            this.repository = skillRepository;
            this.mapper = mapper;
        }

        public async Task<bool> UpdateAsync(SkillsEditModel skillsModel)
        {
            SkillsInfoEntity skillFromDb = await this.repository
                .Where(x => x.Id == skillsModel.Id)
                .Include(x => x.SkillsInfoDrivingCategories)
                .FirstOrDefaultAsync();

            if (skillFromDb == null)
            {
                return false;
            }

            this.mapper.Map(skillsModel, skillFromDb);

            skillFromDb.HasDrivingLicense = skillsModel.DrivingLicenseCategoryIds.Any();

            this.UpdateDrivingLicenseCategories(
                skillsModel.DrivingLicenseCategoryIds,
                skillFromDb.SkillsInfoDrivingCategories);

            this.repository.Update(skillFromDb);

            await this.repository.SaveChangesAsync();

            return true;
        }

        private void UpdateDrivingLicenseCategories(
            IEnumerable<int> drivingCategoryIds,
            List<SkillsInfoDrivingCategoryEntity> skillsInfoDrivingCategoryEntities)
        {
            IEnumerable<int> licenseCategoriesToAdd = drivingCategoryIds
                .Where(id => !skillsInfoDrivingCategoryEntities.Any(x => x.DrivingCategoryId == id));

            List<SkillsInfoDrivingCategoryEntity> licenseCategoriesToRemove = skillsInfoDrivingCategoryEntities
                .Where(x => !drivingCategoryIds.Contains(x.DrivingCategoryId))
                .ToList();

            if (licenseCategoriesToAdd.Any())
            {
                skillsInfoDrivingCategoryEntities.AddRange(licenseCategoriesToAdd.Select(id => new SkillsInfoDrivingCategoryEntity { DrivingCategoryId = id }));
            }

            if (licenseCategoriesToRemove.Count > 0)
            {
                licenseCategoriesToRemove
                    .ForEach(entityToRemove => skillsInfoDrivingCategoryEntities.Remove(entityToRemove));
            }
        }
    }
}
